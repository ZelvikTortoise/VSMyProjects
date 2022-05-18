using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;    // Nutné přidat pro práci se soubory.

namespace PocitaniSlov
{
    class Program
    {
        static void Main(string[] args)
        {
            // Vstup musí obsahovat právě 1 argument.
            if (args.Length != 1)
            {
                Console.WriteLine("Argument Error");
            }
            else // Na vstupu je právě 1 argument.
            {
                try
                {
                    using (StreamReader sr = new StreamReader(args[0])) // Cesta k souboru je uložena v args[0].
                    {
                        string input = sr.ReadToEnd();
                        string[] text = input.Split(' ', '\n', '\t');  // Výpis možných bílých znaků dle zadání.

                        List<string> words = new List<string>();
                        foreach (string w in text)
                        {
                            if (w != "")
                            {
                                words.Add(w);
                            }
                        }

                        Console.WriteLine(words.Count);    // Argument words.Lenght je typu int a dle zadání bude < 2 000 000 000, tedy se vejde.
                    }
                }
                catch
                {
                    Console.WriteLine("File Error");
                }
            }
        }
    }
}
