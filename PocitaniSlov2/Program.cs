using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;    // Obsahuje StreamReader, StreamWriter. Nutné.

namespace PocitaniSlov2
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
                int slov = 0;   // Počet slov v souboru.
                try
                {
                    using (StreamReader sr = new StreamReader(args[0]))  // Cesta k souboru bude zadána na příkazové řádce jako 1 string.
                    {
                        bool cele = false;  // False, pokud byl poslední znak bílý. True, pokud jsme na začátku, uprostřed či na konci slova.
                        int znak = 0;   // Inicializační hodnota nehraje roli. Nesmí být to ale -1.

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
                                        slov++;
                                    }
                                    cele = false;
                                    break;
                                default:
                                    cele = true;
                                    break;
                            }
                        }
                        Console.WriteLine(slov);
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
