using System;
using System.IO;
using System.Collections.Generic;

namespace MaximumCislo
{
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
            int[] numbers;
            ReadToEnd(Console.In, out numbers);
            int max = int.MinValue;

            for (int i = 0; i < numbers.Length; i++)
                if (max < numbers[i])
                    max = numbers[i];

            Console.Write(max);
        }
    }
}
