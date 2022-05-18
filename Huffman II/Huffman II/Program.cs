using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Huffman_II
{
    // Out: in + ".huff"
    // Hlavička: "0x7B 0x68 0x75 0x7C 0x6D 0x7D 0x66 0x66"
    // Hlavička + strom + zakódovaný text
    class Node
    {
        public Node Left { get; set; }      // Left child node.
        public Node Right { get; set; }     // Right child node.
        public ulong Weight { get; }    // Weight of a current node.
        public int CharValue { get; }  // Values 0–255. If an object is an internal (non-leaf) node, it has no char stored in itself. Then the CharValue == -1.

        /// <summary>
        /// Creates a new INTERNAL NODE. If you intend to create a new leaf node, use other overload.
        /// </summary>
        /// <param name="weight">Weight of the new node.</param>
        public Node(ulong weight)
        {
            Left = null;
            Right = null;
            Weight = weight;
            CharValue = -1;
        }
        /// <summary>
        /// Creates a new LEAF NODE. If you intend to create a new internal node, use other overload or call this overload with ch == -1.
        /// </summary>
        /// <param name="weight">Weight of the new node.</param>
        /// <param name="charValue">CharValue of the new node.</param>
        public Node(ulong weight, int charValue)
        {
            Left = null;
            Right = null;
            Weight = weight;
            CharValue = charValue;
        }
    }

    class BinaryTree
    {
        public List<Node> MakeForest(FileStream fs)
        {
            ulong[] characters = new ulong[256];
            byte index = 0;
            int intIndex = 0;

            // Going through the input file.
            while ((intIndex = fs.ReadByte()) != -1)
            {
                index = (byte)intIndex;
                characters[index]++;
            }

            // Creating the actual forest (List<Node>). 
            List<Node> forest = new List<Node>();
            for (int i = 0; i < 256; i++)
            {
                // We don't want nodes with Weight == 0 in our forest.
                if (characters[i] != 0)
                {
                    Node node = new Node(characters[i], i);
                    forest.Add(node);
                }
            }

            return forest;
        }

        /// <summary>
        /// Returns byte value from the list indeces according to the following priorities:
        /// 1) leaves before internal nodes, 2) between leaves: first lower CharValue, 3) between internals: first with lower index in the nodes list
        /// List nodes is a forest of binary trees. Indeces in the list indeces are refering to the list nodes.
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="indeces"></param>
        /// <returns></returns>
        public byte FindNode(List<Node> nodes, List<byte> indeces)
        {
            byte index = 0;

            if (indeces.Count == 1)
            {
                index = indeces[0];
            }
            else
            {
                // Checking all indeces of nodes for leaves.
                List<byte> leaves = new List<byte>();
                foreach (byte i in indeces)
                {
                    if (nodes[i].CharValue != -1)
                    {
                        leaves.Add(i);
                    }
                }
                if (leaves.Count == 1)
                {
                    index = leaves[0];
                }
                else if (leaves.Count > 1)
                {
                    byte minCharValue = 255;
                    // We know that for all inequal i, j: nodes[i].CharValue != nodes[j].CharValue.
                    foreach (byte i in leaves)
                    {
                        if (nodes[i].CharValue < minCharValue)
                        {
                            minCharValue = (byte)nodes[i].CharValue;    // We know CharValues here are 0–255. (There are no internals with CharValue == -1.)
                            index = i;  // Between leaves: The lower CharValue, the higher priority.
                        }
                    }
                }
                else   // All nodes with indeces from the list are internal nodes.
                {
                    // This little algorithm is probably not needed. By having it we make sure it does what we want it to do.
                    byte minIndex = 255;
                    foreach (byte i in indeces)
                    {
                        if (i < minIndex)
                        {
                            minIndex = i;
                        }
                    }

                    index = minIndex;
                    // index = indeces[0]; // Should work too since the list indeces is Sorted thanks to the construction of the algorithm.
                }
            }

            // Cleaning the index from the list of indeces so it never can be found again.
            indeces.RemoveAt(indeces.IndexOf(index));
            return index;
        }

        /// <summary>
        /// Chooses two nodes from a list of nodes according to a list of indeces of nodes with the lowest weight.
        /// Another priorities: 1) leaves before internal nodes, 2) between leaves: first lower CharValue, 3) between internals: first with lower index in the nodes list.
        /// Always call this method with nodes.Count >= 2.
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="indeces"></param>
        /// <returns></returns>
        public Tuple<byte, byte> ChooseTwoNodes(List<Node> nodes, List<byte> indeces)
        {
            byte firstIndex = 0;
            byte secondIndex = 0;
            bool firstFound = false;

            // If there are nodes with different weight in the list, there is only one node with lower weight than others and it's placed last in the list.
            if (nodes[indeces[indeces.Count - 1]].Weight != nodes[indeces[0]].Weight)   // Comparing the last index node's weight with the first index node's weight.
            {
                // The last index node has lower Weight.
                firstIndex = indeces[indeces.Count - 1];
                firstFound = true;
                indeces.RemoveAt(indeces.Count - 1);

                // If we had only 2 indeces where the second one had lower weight, we can just return these and end.
                if (indeces.Count == 1)
                {
                    secondIndex = indeces[0];
                    Tuple<byte, byte> easyPair = new Tuple<byte, byte>(firstIndex, secondIndex);
                    return easyPair;
                }
            }

            // Using priorities to find the correct two nodes.
            if (!firstFound)
            {
                firstIndex = FindNode(nodes, indeces);
            }
            secondIndex = FindNode(nodes, indeces);

            Tuple<byte, byte> pair = new Tuple<byte, byte>(firstIndex, secondIndex);
            return pair;
        }

        public List<byte> FindValueIndeces(List<Node> forest, ulong value)
        {
            List<byte> valueIndeces = new List<byte>();
            foreach (Node tree in forest)
            {
                if (tree.Weight == value)
                {
                    byte ind = (byte)forest.IndexOf(tree);  // Always: 1 <= forest.Count <= 256, therefore we don't lose any data.
                    valueIndeces.Add(ind);
                }
            }

            return valueIndeces;
        }

        public ulong FindMinumumWeight(List<Node> forest)
        {
            ulong min = forest[0].Weight;
            for (int i = 1; i < forest.Count; i++)     // Starting from 1 because 0th node was handled a line above.
            {
                if (forest[i].Weight < min)
                {
                    min = forest[i].Weight;
                }
            }

            return min;
        }

        public Node MakeTree(List<Node> forest)
        {
            while (forest.Count != 1)
            {
                ulong minWeight = FindMinumumWeight(forest);
                List<byte> minValueIndeces = FindValueIndeces(forest, minWeight);
                Tuple<byte, byte> nodePair;

                // If we we have only 1 index after searching.
                if (minValueIndeces.Count == 1)
                {
                    // Remembering the index of our nodePair.Item1.
                    byte nodeIndex1 = minValueIndeces[0];

                    // Remembering the reference to the node and removing it temporarly from forest.
                    Node substituteNode = forest[nodeIndex1];
                    forest.RemoveAt(nodeIndex1);
                    minValueIndeces.Remove(nodeIndex1);     // Not needed.

                    // Finding the new lowest weight in our forest and then all indeces of nodes with such weight.
                    minWeight = FindMinumumWeight(forest);
                    minValueIndeces = FindValueIndeces(forest, minWeight);

                    // Adding our node back (it will be the last tree in forest) and then adding it to the choosing-pair algorithm.
                    forest.Add(substituteNode);
                    nodeIndex1 = (byte)(forest.Count - 1);
                    minValueIndeces.Add(nodeIndex1);
                }

                // Getting the pair of nodes in a correct order.
                nodePair = ChooseTwoNodes(forest, minValueIndeces);

                // Connecting two chosen nodes.
                Node internalNode = new Node(forest[nodePair.Item1].Weight + forest[nodePair.Item2].Weight);
                // The node with higher priority is the left child, the other is the right child.
                internalNode.Left = forest[nodePair.Item1];
                internalNode.Right = forest[nodePair.Item2];

                // Removing children from forest in correct order.
                if (nodePair.Item1 > nodePair.Item2)
                {
                    forest.RemoveAt(nodePair.Item1);
                    forest.RemoveAt(nodePair.Item2);
                }
                else
                {
                    forest.RemoveAt(nodePair.Item2);
                    forest.RemoveAt(nodePair.Item1);
                }
                // Adding the new-made tree to forest.
                forest.Add(internalNode);
            }

            // Returning the root. (forest.Count == 1)
            return forest[0];
        }
    }

    class DephtFirstSearch
    {
        public void PrintPrefix(TextWriter writer, Node node)
        {
            if (node.CharValue == -1)   // Internal node.
            {
                writer.Write(node.Weight.ToString() + " ");
            }
            else   // Leaf node.
            {
                writer.Write("*" + node.CharValue.ToString() + ":" + node.Weight.ToString() + " ");
            }

            if (node.Left != null)
            {
                PrintPrefix(writer, node.Left);
            }

            if (node.Right != null)
            {
                PrintPrefix(writer, node.Right);
            }
        }

        /// <summary>
        /// Gets you a coding table in last parameter. Clear the table first before calling this method.
        /// Prints a coded tree to a binary file. Code form:
        /// bit 0: 0 (internal node) or 1 (leaf node)
        /// bits 1–55: lower 55 bits of the node
        /// bits: 56–63: (byte)0 (internal node) or (byte)charValue (leaf node)
        /// Using LE encoding.
        /// Left son gains a value 0 at the end of the current code. Right son gains a value 1.
        /// </summary>
        /// <param name="writer">Where you want to print the coded tree.</param>
        /// <param name="node">The root of your code tree.</param>
        /// <param name="sb">Any StringBuilder needed just for speeding up this method.</param>
        /// <param name="table">Parameter in which you'll get the coding table. Note: Clear your table first before calling this method!</param>
        public void CodePrefixAndGetCodeTable(BinaryWriter writer, Node node, StringBuilder sb, ref string[] table)
        {
            // Another block so the program frees the space after using ulong local variables.
            {
                ulong tempCode = node.Weight << 1;
                tempCode = tempCode & 0x00_FF_FF_FF_FF_FF_FF_FE;   // Getting format: 0th bit: 0; bits 1–55: node.Weight; 7th byte: 0.
                                                            // Internal node is done.
                if (node.CharValue != -1)   // Leaf node modifications.
                {
                    ulong val = (ulong)node.CharValue << 56;
                    tempCode = tempCode | (ulong)1 | val;        // 0th bit is 1, bits 1–55 are node.Weight, 7th byte is the node's charValue.

                    table[node.CharValue] = sb.ToString();  // Making the code table.
                }
                writer.Write(tempCode);     // BinaryWriter saves data in LE.
            }

            if (node.Left != null)
            {
                sb.Append("0");     // Left son gets 0.
                CodePrefixAndGetCodeTable(writer, node.Left, sb, ref table);
                sb.Remove(sb.Length - 1, 1);    // Returning from a left child.
            }

            if (node.Right != null)
            {
                sb.Append("1");     // Right son gets 1.
                CodePrefixAndGetCodeTable(writer, node.Right, sb, ref table);
                sb.Remove(sb.Length - 1, 1);    // Returning from a right child.
            }
        }
    }

    class Huffman
    {
        public void Encode(FileStream reader, BinaryWriter writer, string[] codeTable)
        {
            StringBuilder builder = new StringBuilder();
            byte index = 0;
            int intIndex = 0;
            // Potencionally very long cycle.
            while ((intIndex = reader.ReadByte()) != -1)
            {
                index = (byte)intIndex;
                builder.Append(codeTable[index]);

                while (builder.Length >= 8)
                {
                    byte output = 0;
                    byte multiplier = 1;
                    for (byte i = 0; i <= 7; i++)
                    {
                        if (builder[i] == '1')
                        {
                            output += multiplier;
                        }
                        multiplier *= 2;    // After last iteration: multiplier == 0.
                    }
                    builder.Remove(0, 8);
                    writer.Write(output);
                }
            }

            // Rest of our bit stream.
            if (builder.Length > 0) // builder.Length < 8
            {
                byte output = 0;
                byte multiplier = 1;
                for (byte i = 0; i <= builder.Length - 1; i++)
                {
                    if (builder[i] == '1')
                    {
                        output += multiplier;
                    }
                    multiplier *= 2;
                }
                builder.Remove(0, builder.Length);  // Not needed.
                writer.Write(output);   // (8 - builder.Length) times 0 from the MSb, then binary output number
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Argument Error");
            }
            else
            {
                //TextWriter tw = Console.Out;
                byte[] head = new byte[8] { 0x7B, 0x68, 0x75, 0x7C, 0x6D, 0x7D, 0x66, 0x66 };
                string pathIn = args[0];
                string pathOut = pathIn + ".huff";
                BinaryTree myTrees = new BinaryTree();
                DephtFirstSearch dfs = new DephtFirstSearch();
                List<Node> forest;
                Node root;
                string[] codeTable = new string[256];
                Huffman huffman = new Huffman();

                try
                {
                    using (FileStream stream = new FileStream(pathIn, FileMode.Open))
                    {
                        forest = myTrees.MakeForest(stream);
                    }

                    using (BinaryWriter bw = new BinaryWriter(File.Open(pathOut, FileMode.Create)))
                    {
                        // ***Starting output***
                        // 1) Printing out the head.
                        foreach (byte b in head)
                        {
                            bw.Write(b);
                        }

                        if (forest.Count != 0)
                        {
                            root = myTrees.MakeTree(forest);                            

                            StringBuilder builder = new StringBuilder();    // Creating this before calling the method. (We don't want it to be called over and over with every recursion iteration.)
                            // 2) Printing out the code tree.
                            dfs.CodePrefixAndGetCodeTable(bw, root, builder, ref codeTable);    // The variable codeTable is inicialized in this method. (Like 'out' type of parameter.)                            
                        }
                        bw.Write((ulong)0); // End of pritning tree.

                        // Note: codeTable is now inicialized.
                        // 3) Printing out the coded text.
                        using (FileStream stream = new FileStream(pathIn, FileMode.Open))
                        {
                            huffman.Encode(stream, bw, codeTable);
                        }
                    }                        
                }
                catch (IOException)
                {
                    Console.WriteLine("File Error");
                }
            }
        }
    }
}
