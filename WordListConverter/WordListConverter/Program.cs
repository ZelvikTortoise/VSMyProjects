using System;
using System.IO;

namespace WordListConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathIn = @"C:\Users\lolhe\Desktop\words.txt";
            string pathOut = @"C:\Users\lolhe\Desktop\JailBreak.txt";

            using (StreamReader sr = new StreamReader(pathIn))
            using (StreamWriter sw = new StreamWriter(pathOut))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    sw.WriteLine(line.Split('"')[1].ToUpper());
                }
            }
        }
    }
}
