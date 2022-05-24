using System;
using System.IO;

namespace DidaktikaAnalytickaParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathIn = @"C:\Users\lolhe\Desktop\Komenský.txt";
            string pathOut = @"C:\Users\lolhe\Desktop\Komenský_parsed.txt";

            using (StreamReader sr = new StreamReader(pathIn))
            using (StreamWriter sw = new StreamWriter(pathOut))
            {
                String input;
                int arab = 1;
                string number;
                bool copy = false;
                while ((input = sr.ReadLine()) != null)
                {
                    if (copy && input == "")
                    {
                        sw.WriteLine();
                        copy = false;
                    }
                    else if (copy)
                    {
                        sw.WriteLine(input);
                    }
                    else if (input != "" && input.ToUpper() == input)
                    {
                        sw.WriteLine(input);
                        sw.WriteLine();
                    }
                    else
                    {
                        number = input.Split('.')[0];                        
                        if (number.StartsWith(arab.ToString()))
                        {
                            sw.WriteLine(input);
                            arab++;
                            copy = true;
                        }
                        else if ((number.StartsWith('I') || number.StartsWith('V') || number.StartsWith('X') || number.StartsWith('L') || number.StartsWith('C')) && number.Length <= 8)
                        {
                            if (input.StartsWith("LVI.,") || input.StartsWith("LXXVI.)") || input.StartsWith("Viz"))
                            {
                                continue;
                            }
                            sw.WriteLine(input);
                            copy = true;
                        }                        
                    }                    
                }
            }
        }
    }
}
