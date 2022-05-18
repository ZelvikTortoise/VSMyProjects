using System;
using System.IO;

namespace QuestionRandomizer
{
    internal class Program
    {
        public static void PickQuestion(TextReader reader, bool justNumbers)
        {
            if (justNumbers)
            {

            }
            else
            {

            }
        }

        static void Main(string[] args)
        {
            const string path = @"";            
            using (StreamReader sr = new StreamReader(path))
            {
                // PickQuestion(Console.In, true);
                PickQuestion(sr, false);
            }

            Console.WriteLine();
            Console.Write("Press any key to end the program.");
            Console.ReadKey();
        }
    }
}