using System;
using System.IO;

namespace Test
{
    class Program
    {
        public static void Main(string[] args)
        {
            string path = @"C:\Users\luk19\OneDrive\Desktop\1.txt";

            using (BinaryReader br = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                byte[] data = br.ReadBytes(4);

                for (int i = 0; i <= 3; i++)
                {
                    Console.Write((char)data[i]);   // nestačí .ToString() ani samotný byte (interně se volá .ToString()
                }
            }

            Console.ReadKey();
        }
    }
}
    