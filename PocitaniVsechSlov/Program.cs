using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;    // StreamReader

namespace PocitaniVsechSlov
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Argument Error");
            }
            else
            {
                Dictionary<string, int> slovnik = new Dictionary<string, int>();
                try
                {
                    using (StreamReader sr = new StreamReader(args[0]))  // Cesta k souboru bude zadána na příkazové řádce jako 1 string.
                    {
                        bool cele = false;  // False, pokud byl poslední znak bílý. True, pokud jsme na začátku, uprostřed či na konci slova.
                        int znak = 0;   // Inicializační hodnota nehraje roli. Nesmí být to ale -1.
                        string slovo = "";

                        while (znak != -1)
                        {
                            znak = sr.Read();
                            switch (znak)
                            {
                                case (int)' ':
                                case (int)'\n':
                                case (int)'\t':
                                case -1:
                                    if (cele)
                                    {
                                        if (!slovnik.ContainsKey(slovo))
                                        {
                                            slovnik.Add(slovo, 1);
                                        }
                                        else
                                        {
                                            slovnik[slovo]++;
                                        }
                                        cele = false;
                                        slovo = "";
                                    }
                                    break;
                                default:
                                    slovo += (char)znak;
                                    cele = true;
                                    break;
                            }
                        }
                        List<string> friend = slovnik.Keys.ToList();    // Setřídíme klíče v Listu.
                        friend.Sort();

                        foreach(string w in friend)
                        {
                            Console.WriteLine(w + ": " + slovnik[w]);
                        }                        
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
