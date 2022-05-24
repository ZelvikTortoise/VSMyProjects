using System;
using System.Text;
using System.IO;

namespace RandomNumberFileGenerator
{
    internal class Program
    {       
        static void RandomNumbers(Random random)
        {
            string path = @"C:\Users\lolhe\Desktop\numbers.txt";
            using (StreamWriter sw = new StreamWriter(path))
            {
                int amount = random.Next(50, 101);
                sw.WriteLine(amount);
                for (int i = 0; i < amount - 1; i++)
                {
                    sw.WriteLine(random.Next(-100, 101));
                }
                sw.Write(random.Next(-100, 101));
            }
        }
        static void RandomMatrices(Random random)
        {
            string path = @"C:\Users\lolhe\Desktop\matrices.txt";
            StringBuilder sb = new StringBuilder();

            using (StreamWriter sw = new StreamWriter(path))
            {
                int m1, m2, n1, n2;
                m1 = random.Next(1, 11);
                n1 = random.Next(1, 11);
                m2 = n1;
                n2 = random.Next(1, 11);

                sw.WriteLine(m1 + " " + n1);
                for (int i = 0; i < m1; i++)
                {
                    for (int j = 0; j < n1 - 1; j++)
                    {
                        sb.Append(random.Next(0, 10));
                        sb.Append(" ");
                    }
                    sb.Append(random.Next(0, 10));
                    sw.WriteLine(sb.ToString());
                    sb.Clear();
                }

                sw.WriteLine(m2 + " " + n2);
                for (int i = 0; i < m2 - 1; i++)
                {
                    for (int j = 0; j < n2 - 1; j++)
                    {
                        sb.Append(random.Next(0, 10));
                        sb.Append(" ");
                    }
                    sb.Append(random.Next(0, 10));
                    sw.WriteLine(sb.ToString());
                    sb.Clear();
                }
                for (int j = 0; j < n2 - 1; j++)
                {
                    sb.Append(random.Next(0, 10));
                    sb.Append(" ");
                }
                sb.Append(random.Next(0, 10));
                sw.Write(sb.ToString());
                sb.Clear();
            }
        }

        static void Main(string[] args)
        {
            Random random = new Random();

            // RandomNumbers(random);
            // RandomMatrices(random);

            Console.WriteLine("Everything went as expected.");
            Console.Write("Press any key to exit the program... ");
            Console.ReadKey();
            Console.WriteLine();
        }
    }
}