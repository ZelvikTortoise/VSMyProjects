using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TextReaderTests
{

    class NumberReader  // upravená třída Ctecka od pana profesora Holana.
    {
        public static int ReadNum()
        {
            bool negative = false;
            int z = Console.Read();            
            while ((z < '0') || (z > '9'))
            {
                if (z == '-')
                    negative = true;    // Another z should break the cycle.
                
                z = Console.Read();
            }
                
            int x = 0;
            while ((z >= '0') && (z <= '9'))
            {
                x = 10 * x + z - '0';
                z = Console.Read();
            }

            if (negative)
                return -x;
            else
                return x;
        }
    }

    class Program
    {
        private static char[] White = new char[] { ' ', '\n', '\t', '\r' };
        private static void ReadToEnd(TextReader reader, out int[] nums)
        {
            int n;
            string s = reader.ReadToEnd();
            string[] numsString = s.Split(White, StringSplitOptions.RemoveEmptyEntries);
            List<int> numbers = new List<int>();
            for (int i = 0; i < numsString.Length; i++)
            {
                n = int.Parse(numsString[i]);
                numbers.Add(n);
            }

            numbers.RemoveAt(numbers.Count - 1);    // Last is -1.
            nums = numbers.ToArray();
        }
        static void Main(string[] args)
        {
            string path = @"C:\Users\luk19\OneDrive\Desktop\test.txt";
            int[] numbers;
            using (StreamReader sr = new StreamReader(path))
            {
                ReadToEnd(sr, out numbers);
            }

            int max = int.MinValue;

            for (int i = 0; i < numbers.Length; i++)
                if (max < numbers[i])
                    max = numbers[i];

            Console.Write(max);

            Console.ReadKey();
        }
    }
}
