using System;
using System.IO;

namespace WebTutorials
{
    class Program
    {
        static void Main(string[] args)
        {            
            string pathIn = @"C:\Users\lolhe\Desktop\PROCESS.txt";
            string pathOut = @"C:\Users\lolhe\Desktop\Tutorials.txt";

            if (!File.Exists(pathIn))
            {
                Console.WriteLine("The file with path {0} doesn't exist!", pathIn);
                Console.WriteLine("Exiting...");
                return;
            }

            if (!File.Exists(pathOut))
                Console.WriteLine("The file with path {0} was created.", pathIn);
            else
                Console.WriteLine("The file with path {0} was rewritten.", pathIn);

            using (StreamReader sr = new StreamReader(pathIn))
            using (StreamWriter sw = new StreamWriter(pathOut))
            {
                string row, desc;
                string[] parts;

                sw.WriteLine("<ul>");
                while((row = sr.ReadLine()) != null)
                {
                    parts = row.Split(' ');
                    desc = parts[1];

                    for (int i = 2; i < parts.Length; i++)
                    {
                        desc += " ";
                        desc += parts[i];
                    }
                    sw.WriteLine("<li><a href=\"{0}\">{1}</a></li>", parts[0], desc);
                }
                sw.WriteLine("</ul>");
            }
        }
    }
}
