using System;
using System.Collections.Generic;
using System.IO;

namespace DODBinary
{
    internal class Program
    {
        /// <summary>
        /// Extracts all the numbers that in binary have at most maxDigits and have 1 in the position of digitPlace.
        /// </summary>
        /// <param name="digitPlace">0 for units, 1 for twos, 2 for fours, ..., n for 2^n digit</param>
        /// <param name="maxDigits">Defines the right boundary of the range (0 to 2^maxDigits - 1, both included).</param>
        /// <returns>List of numbers that have the specified digit in binary equal to 1.</returns>
        static List<int> GetNumbersWithDigitOne(int digitPlace, int maxDigits)
        {
            int max = (int)Math.Pow(2, maxDigits) - 1;
            int digitValue = (int)Math.Pow(2, digitPlace);
            List<int> numbers = new List<int>();

            for (int num = 0; num <= max; num++)
            {
                if ((num & digitValue) == digitValue)
                {
                    numbers.Add(num);
                }
            }

            return numbers;
        }

        static void WriteNumberRow(TextWriter writer, List<int> numbers, string separator)
        {
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                writer.Write(numbers[i]);
                writer.Write(separator);
            }
            writer.WriteLine(numbers[numbers.Count - 1]);
        }

        static void Main()
        {
            int maxDigits = 8;
            string path = "output.txt";
            string sep = "   ";

            using(StreamWriter sw = new StreamWriter(path))
            {
                for (int digitPlace = maxDigits - 1; digitPlace >= 0; digitPlace--)
                {
                    WriteNumberRow(sw, GetNumbersWithDigitOne(digitPlace, maxDigits), sep);
                }
            }

            Console.Write("Press any key to continue... ");
            Console.ReadKey();
        }
    }
}
