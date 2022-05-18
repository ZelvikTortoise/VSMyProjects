using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Huffman_III
{
    abstract class Node
    {
        public ulong Weight { get; set; }    // Weight of a current node.       
        // public set – so we can save some memory decompressing the file.        
    }
    class InternalNode : Node
    {
        public Node Left { get; set; }      // Left child node.
        public Node Right { get; set; }     // Right child node.

        /// <summary>
        /// Creates a new internal node.
        /// </summary>
        /// <param name="weight">Weight of the new node.</param>
        public InternalNode(ulong weight)
        {
            Left = null;
            Right = null;
            Weight = weight;
        }
    }
    class LeafNode : Node
    {
        public byte CharValue { get; }  // Values 0–255. If an object is an internal (non-leaf) node, it has no char stored in itself. Then the CharValue == -1.

        /// <summary>
        /// Creates a new leaf node.
        /// </summary>
        /// <param name="weight">Weight of the new node.</param>
        /// <param name="charValue">CharValue of the new node.</param>
        public LeafNode(ulong weight, byte charValue)
        {
            Weight = weight;
            CharValue = charValue;
        }
    }

    class Tests
    {
        public FileStream Stream { get; private set; }
        public Tests(FileStream fs)
        {
            this.Stream = fs;
        }

        public void ShowNodeStream()
        {
            HuffmanTree htree = new HuffmanTree(this.Stream);
            InternalNode inner;
            LeafNode leaf;
            bool go = true;

            while (go)
            {
                htree.CreateNode(out Node node);
                if (node is InternalNode)
                {
                    Console.WriteLine("Internal node:");
                    inner = (InternalNode)node;
                    Console.WriteLine("Weight: " + inner.Weight.ToString());
                    Console.WriteLine();

                    if (inner.Weight == 0)
                        go = false;
                }
                else
                {
                    Console.WriteLine("Leaf node:");
                    leaf = (LeafNode)node;
                    Console.WriteLine("Weight: " + leaf.Weight.ToString());
                    Console.WriteLine("CharValue: " + leaf.CharValue.ToString());
                    Console.WriteLine("Character: " + leaf.CharValue);
                    Console.WriteLine();
                }
            }
        }
    }

    class Decompression
    {
        public FileStream ByteReader { get; private set; }
        public BinaryWriter BinWriter { get; private set; }
        public Decompression(BinaryWriter writer, FileStream reader)
        {
            this.BinWriter = writer;
            this.ByteReader = reader;
        }

        public const int bufferSize = 512;
        /// <summary>
        /// Using BinaryWriter, creates a decompressed file by reading coded bit stream in FileStream.
        /// Uses a 2KiB buffer and the method EmptyBuffer which does the whole algorithm.
        /// Algorithm in a nutshell: For 0 go to a left child, for 1 go to a right child until hitting a leaf.
        /// </summary>
        /// <param name="root">Root of the valid Huffman tree with at least 2 leaves.</param>
        /// <returns>True for succesful decompression, false for unsuccesful decompression.</returns>
        public bool DecompressStream(Node root)
        {
            int intNewByte;
            byte[] buffer = new byte[bufferSize];
            int i = 0;
            Node startNode = root;

            while ((intNewByte = ByteReader.ReadByte()) != -1)
            {
                buffer[i] = (byte)intNewByte;
                if (i < bufferSize - 1) // i hasn't acquired the max possible index size yet
                {
                    i++;
                }
                else   // buffer is full
                {
                    try
                    {
                        EmptyBuffer(ref buffer, i, root, ref startNode);
                    }
                    catch
                    {
                        return false;   // Bit stream is incompatible with the Huffman tree.
                    }
                    i = 0;  // Starting from first index again.
                }
            }

            if (i - 1 >= 0)     // i is index of 1st non-valid index (if i == 0 then buffer is empty)
            {
                EmptyBuffer(ref buffer, i - 1, root, ref startNode);
            }

            // We're not gonna check that. Who cares anyways.
            /*// If the bit stream was excatly a code of some data, then last visited Node had to be LeafNode.
            // If so, then the method EmptyBuffer would leave startNode as root. (Always leaves startNode as InternalNode.)
            if (!startNode.Equals(root))
            {
                return false;   // We emptied everything but we ended in a non-leaf node.
                // (If we ended in a leaf, the algorithm would jump back to the root and stop there.)
            }*/

            // Everything went well, the file has been decompressed succesfully.
            return true;
        }

        /// <summary>
        /// Empties the buffer in two parts – all bytes except the last one and then the last byte.
        /// Goes down the valid Huffman tree and whenever hits a leaf, writers its CharValue and sets start to root.
        /// Therefore, always leaves start as InternalNode.
        /// Buffer emptying starts at index 0 and finishes at index lastIndex (including it).
        /// Buffer is NEVER changed, start CAN be changed.
        /// If the tree is incompatible with the bit stream, it throws an Exception (NullReference probably).
        /// Note: Catch this possible exception to know the input is invalid.
        /// </summary>
        /// <param name="buffer">Array of bytes starting at 0 and finishing with valid data at lastIndex (including it). Note: Ref for faster usage.</param>
        /// <param name="lastIndex">Last index of valid data in buffer. Note: Buffer has to be byte[max] where max >= lastIndex.</param>
        /// <param name="root">Root of a valid Huffman tree compatible with the bit stream.</param>
        /// <param name="start">The node in the Huffman tree where the algorithm starts decoding. (Continuing previous decoding, etc.)</param>
        private void EmptyBuffer(ref byte[] codeBuffer, int lastIndex, Node root, ref Node start)
        {
            // Calling with negative lastIndex is interpreted as calling with an empty buffer.
            /*if (lastIndex < 0)
            {
                return;
            }*/
            // Note: We're not using this because it'd slow down the whole decompressing for no reason.
            // Also, we're not checking that Node start is actually an InternalNode for the same reason.

            byte[] outBuffer = new byte[8*(lastIndex + 1)];
            int firstInvalidIndex = 0;
            byte selector;
            LeafNode leaf;
            // For example bytes 0x4B 0x58 0x07 are coded as a sequence 1101 0010 0001 1010 1110 0000. (No spaces.)
            // That's why we have to start at LSb and continue to MSb.

            // All bytes except the last one.
            for (int i = 0; i < lastIndex; i++)
            {
                selector = 0x01;    // Has all bits on 0 and one selected bit on 1.
                for (int j = 0; j <= 7; j++)
                {
                    if (((selector << j) & codeBuffer[i]) == (byte)0)   // Using the code rule (leftChildPath = '0', righChildPath = '1').
                    {
                        // Selected bit in buffer[i] was 0.
                        start = ((InternalNode)start).Left;
                    }
                    else
                    {
                        // Selected bit in buffer[i] was 1.
                        start = ((InternalNode)start).Right;
                    }

                    // We've decoded a symbol.
                    if (start is LeafNode)
                    {
                        leaf = (LeafNode)start;
                        start = root;   // Sending everything back to the root. (We can't stop in LeafNode!)
                        outBuffer[firstInvalidIndex] = leaf.CharValue;
                        firstInvalidIndex++;
                        leaf.Weight--;
                    }
                }
            }

            // The last byte.
            selector = 0x01;    // Has all bits on 0 and one selected bit on 1.
            bool onlyZeros = true;
            for (int j = 0; j <= 7; j++)
            {
                if (((selector << j) & codeBuffer[lastIndex]) == (byte)0)   // Using the code rule (leftChildPath = '0', righChildPath = '1').
                {
                    // Selected bit in buffer[i] was 0.
                    start = ((InternalNode)start).Left;
                }
                else
                {
                    // Selected bit in buffer[i] was 1.
                    start = ((InternalNode)start).Right;
                    onlyZeros = false;
                }

                // We've decoded a symbol.
                if (start is LeafNode)
                {
                    leaf = (LeafNode)start;
                    start = root;   // Sending everything back to the root. (We can't stop in LeafNode!)
                    if (onlyZeros && leaf.Weight == 0)
                        break;  // The bit stream has ended, we're just reading the zeros filling up the last byte.
                    else
                    {
                        outBuffer[firstInvalidIndex] = leaf.CharValue;
                        firstInvalidIndex++;
                        leaf.Weight--;
                    }

                    // Out-buffer and Write(buffer);
                    onlyZeros = true;
                }
            }

            // THIS WOULD DESTROY THE WHOLE PATHING THING!
            // If zeroes at the end of the last byte don't lead to a leaf node, the input is still valid.
            /*if (onlyZeros)
            {
                start = root;
            }*/

            // Finally writing decoded text to the output file.
            BinWriter.Write(outBuffer, 0, firstInvalidIndex);
        }
    }

    class CheckHuffmanTree
    {
        /// <summary>
        /// Checks if all LeafNodes' Weights in a valid Huffman tree are 0.
        /// </summary>
        /// <param name="root">The root of the valid Huffman tree that is being checked.</param>
        /// <returns>True if all LeafNodes' Weights are 0. False otherwise.</returns>
        public bool IsAllLeavesWeightZero(ref Node root)
        {
            if (root is InternalNode)
            {
                InternalNode iRoot = (InternalNode)root;
                Node lNode = iRoot.Left;
                Node rNode = iRoot.Right;
                return (IsAllLeavesWeightZero(ref lNode) && IsAllLeavesWeightZero(ref rNode));
            }
            else if (root.Weight != 0)  // root is LeafNode
                    return false;       // we found LeafNode which Weight != 0
            // else
            return true;
        }
    }

    class HuffmanTree
    {
        public FileStream FileStream { get; }
        public HuffmanTree(FileStream filestream)
        {
            // Data in fs are saved in LE.
            this.FileStream = filestream;
        }

        public enum Result { Empty, Error, JustRoot, Tree }

        /// <summary>
        /// Checks head's validity.                                           .
        /// The head is a sequence of 8 bytes: 0x7B, 0x68, 0x75, 0x7C, 0x6D, 0x7D, 0x66, 0x66.
        /// Uses the class's FileStream.
        /// </summary>
        /// <returns>True for valid head, false for invalid head.</returns>
        public bool CheckHead()
        {
            // Head consists of these 8 bytes in this order.
            byte[] head = new byte[8] { 0x7B, 0x68, 0x75, 0x7C, 0x6D, 0x7D, 0x66, 0x66 };   // i-th byte has  i-th index.
            int intValue;
            byte value;
            bool headOk = true;

            for (int i = 0; i <= 7; i++)
            {
                intValue = this.FileStream.ReadByte();
                if (intValue == -1)
                {
                    headOk = false;
                    break;
                }
                value = (byte)intValue;
                if (value != head[i])
                {
                    headOk = false;
                    break;
                }
            }

            return headOk;
        }

        /// <summary>
        /// Creates a leaf node or an internal node with its coded values.     .
        /// Indicates if it was succesful.
        /// Uses the class's FileStream.
        /// </summary>
        /// <param name="node"></param>
        /// <returns>True if a valid Node instance was created. False if there the notation is invalid or the filestream ends.</returns>
        public bool CreateNode(out Node node)
        {
            bool leaf = false;
            int intThisByte;
            ulong weight;
            ulong weightFood;

            node = null;    // Not assigned properly yet.

            if ((intThisByte = this.FileStream.ReadByte()) != -1)
            {
                // Checking the kind of a node.
                if ((intThisByte | (int)1) == intThisByte)
                {
                    // 0th bit of 0th byte is 1 which implies a leaf node.
                    leaf = true;
                }

                // Getting the weight of a node.
                weight = ((ulong)intThisByte >> 1);
                for (int i = 0; i <= 5; i++)
                {
                    if ((intThisByte = this.FileStream.ReadByte()) != -1)
                    {
                        weightFood = (ulong)intThisByte << (8 * i + 7);
                        weight = (weight | weightFood);
                    }
                    else
                        // Error in parsing weight.
                        return false;
                }

                if (leaf)   // Leaf node.
                {
                    if ((intThisByte = this.FileStream.ReadByte()) != -1)
                    {
                        // Valid leaf node.
                        node = new LeafNode(weight, (byte)intThisByte);
                    }
                    else
                        return false;
                }
                else   // Internal node.
                {
                    if ((intThisByte = this.FileStream.ReadByte()) != -1)
                    {
                        if (intThisByte == (int)0)
                        {
                            // Valid internal node.
                            node = new InternalNode(weight);
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
            }
            else
                return false;

            return true;
        }   // public for testing

        /// <summary>
        /// Recursion that creates and connects all the nodes correctly.
        /// Checks the validity of all the Node Weights.
        /// </summary>
        /// <returns>Node obtained by CreateNode(out node) method. If something goes wrong, throws an Exception.</returns>
        private Node ConnectTree()
        {
            Node node;
            InternalNode inNode;
            if (CreateNode(out node))
            {
                if (node is InternalNode)
                {
                    inNode = (InternalNode)node;
                    inNode.Left = ConnectTree();
                    inNode.Right = ConnectTree();

                    // Internals: valid Weight checking.
                    if (inNode.Weight != inNode.Left.Weight + inNode.Right.Weight)
                    {
                        throw new Exception("Invalid InternalNode weights found in method CreateNode(...).");
                    }
                }
                else
                {
                    // Leaves: valid Weight checking.
                    if (node.Weight <= 0)
                    {
                        throw new Exception("Invalid LeafNode weights found in method CreateNode(...).");
                    }
                }

                return node;
            }
            else
            {
                throw new Exception("Error in method CreateNode(...) occured.");
            }
        }

        /// <summary>
        /// Checks all outcome possibilities and returns it as Result value.  .
        /// Creates a root and then by calling ConnectTree() method creates and connects the whole tree.
        /// ALWAYS checks there's the stop sign ((ulong)0) right after the tree – if there isn't, returns .Error.
        /// </summary>
        /// <param name="root">The root of the Huffman tree. Root is null for empty tree.</param>
        /// <returns>Result value: .JustRoot for 1-node (LeafNode) tree; .Empty for 0-node tree (root == null);
        ///                        .Tree for normal Huffman tree (with more than 1 node); .Error for anything invalid.</returns>
        public Result CreateRootTree(out Node root)
        {
            Node node;
            root = null;

            if (CreateNode(out node))
            {
                if (node is LeafNode)
                {
                    if (this.FileStream.ReadByte() == (byte)0)
                    {
                        root = node;
                        return Result.JustRoot;
                    }
                    else
                        return Result.Error;
                }
                else if (((InternalNode)node).Weight == 0)
                {
                    // All 8 bytes are 0x00.
                    // root == null;
                    return Result.Empty;
                }
                // Else continues THERE.

            }
            else
                return Result.Error;

            // Else continues HERE.
            // First node is an internal node with weight different from zero.

            try
            {
                root = node;
                InternalNode iRoot = (InternalNode)root;

                iRoot.Left = ConnectTree();
                iRoot.Right = ConnectTree();

                if (iRoot.Weight != (iRoot.Left.Weight + iRoot.Right.Weight))
                {
                    root = null;
                    return Result.Error;    // Invalid Weights in the tree.
                }                    
            }
            catch
            {
                root = null;
                return Result.Error;
            }

            // Reading the finishing 0x00_00_00_00_00_00_00_00.
            if (CreateNode(out node))
            {
                if (node is InternalNode && node.Weight == 0)   // We've read the stop sign: (ulong)0.
                {
                    return Result.Tree;
                }
                else
                {
                    root = null;
                    return Result.Error;
                }
            }
            else
            {
                root = null;
                return Result.Error;
            }
        }


        // Not used.
        static readonly char leftChildPath = '0';
        static readonly char rightChildPath = '1';
        /// <summary>
        /// Initializes codeTable, freqTable using an EMPTY StringBuilder. Destroys the tree which the parameter node is a root of.
        /// </summary>
        /// <param name="codeTable">A table of Huffman codes of all characters present in the compressed file. (Not present means 0.)</param>
        /// <param name="freqTable">A table of frequencies of all characters in the compressed file.</param>
        /// <param name="sb">StringBuilder with Length == 0. (IMPORTANT!)</param>
        /// <param name="node">Call this method with a root of a valid Huffman tree.</param>
        public void GetCodeTableAndDestroyTree(ref string[] codeTable, ref ulong[] freqTable, ref StringBuilder sb, ref Node node)
        {
            if (node is InternalNode)
            {
                InternalNode innode = (InternalNode)node;

                // Left child recursion.
                sb.Append(leftChildPath);
                Node child = innode.Left;
                GetCodeTableAndDestroyTree(ref codeTable, ref freqTable, ref sb, ref child);
                sb.Remove(sb.Length - 1, 1);    // Removing the last character.

                // Right child recursion.
                sb.Append(rightChildPath);
                child = innode.Right;
                GetCodeTableAndDestroyTree(ref codeTable, ref freqTable, ref sb, ref child);
                sb.Remove(sb.Length - 1, 1);    // Removing the last character.

                node = null;    // Both its children have been processed so we can destroy the node.
            }
            else   // node is LeafNode
            {
                // Making codeTable.
                LeafNode leaf = (LeafNode)node;
                codeTable[leaf.CharValue] = sb.ToString();
                freqTable[leaf.CharValue] = leaf.Weight;

                node = null;    // The node has been processed so we can destroy it.
            }
        }
    }
    
    class Program
    {        
        public static readonly string fileSuffix = ".huff";
        public static readonly string argError = "Argument Error";
        public static readonly string fileError = "File Error";

        static void Main(string[] args)
        {
            // Checking arguments.
            if (args.Length != 1)
            {
                Console.WriteLine(argError);
                return;
            }

            string pathIn = args[0];

            // Getting pathOut.
            if (!pathIn.EndsWith(fileSuffix))
            {
                Console.WriteLine(argError);
                return;
            }

            string pathOut = pathIn.Remove(pathIn.Length - fileSuffix.Length, fileSuffix.Length);

            if (pathOut == "" || pathOut.EndsWith("\\"))
            {
                Console.WriteLine(argError);
                return;
            }

            // IMPLEMENT: TRY-CATCH BLOCK FOR FILE_ERROR. !!!
            // Now, we have pathIn, pathOut and can start doing something.
            try
            {
                using (FileStream stream = new FileStream(pathIn, FileMode.Open))
                {
                    HuffmanTree htree = new HuffmanTree(stream);

                    // 1) Checking head's validity.
                    if (!htree.CheckHead())
                    {
                        Console.WriteLine(fileError);
                        return;
                    }

                    /*Tests test = new Tests(stream);
                    test.ShowNodeStream();

                    Console.WriteLine(stream.ReadByte());
                    Console.WriteLine(stream.ReadByte());
                    Console.WriteLine(stream.ReadByte());
                    Console.WriteLine(stream.ReadByte());
                    Console.WriteLine(stream.ReadByte());

                    Console.ReadKey();*/

                    // 2) Creating a Huffman tree. (+ reading (ulong)0)
                    Node root;
                    HuffmanTree.Result result = htree.CreateRootTree(out root);
                    bool justRoot;

                    switch (result)
                    {
                        case HuffmanTree.Result.Empty:
                            // Empty tree but not empty file.
                            if (stream.ReadByte() != -1)
                            {
                                Console.WriteLine(fileError);
                                return;
                            }
                            // Empty tree and empty file means end of program.
                            else
                            {
                                justRoot = false;
                                break;
                            }                                
                        case HuffmanTree.Result.Error:
                            Console.WriteLine(fileError);
                            return;
                        case HuffmanTree.Result.JustRoot:
                            justRoot = true;
                            break;
                        case HuffmanTree.Result.Tree:
                            justRoot = false;
                            break;
                        default:
                            throw new Exception("Unhlandled HuffmanTree.Result enum value.");
                    }

                    // Possible ending.
                    // If the Huffman tree's only node is its root, then stream should be at the end of the compressed file.
                    if (justRoot)
                    {
                        if (stream.ReadByte() == -1)
                        {
                            // Decompressing the file that contains only characters of 1 exact byte value.
                            using (BinaryWriter bw = new BinaryWriter(File.Open(pathOut, FileMode.Create)))
                            {
                                // We're sure root is LeafNode.
                                LeafNode onlyNode = (LeafNode)root;
                                for (ulong i = 0; i < onlyNode.Weight; i++)
                                {
                                    bw.Write(onlyNode.CharValue);
                                }
                                // We're done here.
                                return;
                            }
                        }
                        else
                        {
                            // Coded text should be empty and it isn't.
                            Console.WriteLine(fileError);
                            return;
                        }
                    }

                    // NOT VALID, UNNECESSARY SEGMENT:

                    // x) Getting a code table.                
                    /* Programmer's note:
                     * At this point I realized I didn't need to build the tree and could've just made the codeTable.
                     * I'm at least making all node instances reference-free as I'm making the codeTable so we don't waste more memory.
                     */
                    // htree.GetCodeTableAndDestroyTree(ref codeTable, ref charFrequency, ref builder, ref root);
                    // Now the whole tree should be destroyed, stream should be at the beginning of the coded text itself,
                    // codeTable and charFrequency table are both inicialized (to the needed point) and ready.
                    // Note: If a LeafNode.CharValue hasn't occured in the tree, charFrequency at that index will be 0.                

                    /* Programmer's note 2:
                     * Since decompressing the file needs the Huffman tree, I commented out the part above
                     * and removed the codeTable, the charFrequency and the builder
                     * since I'm not using the GetCodeTableAndDestroyTree method.
                     */

                    // END OF THIS SEGMENT.


                    // Probable situation (the Huffman tree has definitely more nodes than one).
                    // 3) Decompressing the file.
                    using (BinaryWriter bw = new BinaryWriter(File.Open(pathOut, FileMode.Create)))
                    {
                        Decompression dcomp = new Decompression(bw, stream);
                        if (!dcomp.DecompressStream(root))
                        {
                            Console.WriteLine(fileError);
                            return;
                        }
                        // The file has been decompressed succesfully.

                        // Checking the LeafNode Weights (if all are equal to 0).
                        CheckHuffmanTree check = new CheckHuffmanTree();
                        if (!(root is null))
                        {
                            if (!check.IsAllLeavesWeightZero(ref root))
                            {
                                Console.WriteLine(fileError);
                                return;
                            }
                        }                        
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine(fileError);   // We had problems reading from the compressed file.
                // Note: The problem might have formed in phase 3) using BinaryWriter on file with pathOut.
                return;
            }
        }
    }
}