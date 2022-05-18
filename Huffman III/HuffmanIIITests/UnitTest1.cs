using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Huffman_III;

namespace HuffmanIIITests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Node node;
            while (htree.CreateNode(out node))
            {
                if (node is InternalNode)
                {
                    Console.WriteLine("Internal.");
                    Console.WriteLine("Weight: " + node.Weight);
                    Console.WriteLine("Left: " + ((InternalNode)node).Left);
                    Console.WriteLine("Right: " + ((InternalNode)node).Right);

                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Leaf.");
                    Console.WriteLine("Weight: " + node.Weight);
                    Console.WriteLine("CharValue: " + ((LeafNode)node).CharValue.ToString());

                    Console.WriteLine();
                }
            }

            {
                Console.WriteLine("An error occured in node parsing.");
            }

            Console.ReadKey();
        }
    }
}
