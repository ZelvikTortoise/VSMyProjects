using System;
using System.IO;

namespace ČíselnéPexeso
{    
    class Program
    {
        static void Losuj(TextWriter writer, int velikost)
        {
            Random random = new Random();            

            int[] losovacíPole = new int[velikost];
            int[] novéPole = new int[velikost];
            int mojeDélkaLosvoacíhoPole;
            int index, pom;

            // Inicializace losovacíPole:
            for (int i = 0; i < velikost; i++)
            {
                losovacíPole[i] = i + 1;
            }

            for (int k = 0; k < velikost; k++)
            {
                mojeDélkaLosvoacíhoPole = velikost;
                for (int i = 0; i < velikost; i++)
                {
                    index = random.Next(0, mojeDélkaLosvoacíhoPole);
                    novéPole[i] = losovacíPole[index];
                    // Aktualizace losovacího pole:
                    pom = losovacíPole[index];
                    losovacíPole[index] = losovacíPole[mojeDélkaLosvoacíhoPole - 1];
                    losovacíPole[mojeDélkaLosvoacíhoPole - 1] = pom;
                    mojeDélkaLosvoacíhoPole--;
                }

                // Výpis:
                int mocninDesíti = 0;
                int číslo = velikost / 10;  // Celočíselné dělení.
                int mocnina;
                while (číslo > 0)
                {
                    mocninDesíti++;
                    číslo = číslo / 10;
                }

                for (int i = 0; i < velikost - 1; i++)
                {
                    // Úprava výpisu ve sloupcích:
                    mocnina = (int)Math.Pow(10, mocninDesíti);
                    for (int j = mocninDesíti; j > 0; j--)
                    {
                        if (novéPole[i] < mocnina)
                        {
                            writer.Write(" ");
                        }
                        mocnina = mocnina / 10;
                    }
                    writer.Write(novéPole[i] + " ");
                }

                // Poslední iterace předchozího vnějšího foru pro i == velikost - 1.
                // Nechceme totiž mezery na koncích řádků!
                mocnina = (int)Math.Pow(10, mocninDesíti);
                for (int j = mocninDesíti; j > 0; j--)
                {
                    if (novéPole[velikost - 1] < mocnina)
                    {
                        writer.Write(" ");
                    }
                    mocnina = mocnina / 10;
                }
                // Jiný způsob ošetření, tentokrát nechceme na konci souboru prázdný řádek.
                if (k < velikost - 1)
                    writer.WriteLine(novéPole[velikost - 1]);   // Výpis posledního prvku v neposledním řádku.
                else
                writer.Write(novéPole[velikost - 1]);   // Výpis posledního prvku v posledním řádku.
            }
        }
        static void Main(string[] args)
        {
            const int n = 256;  // Při maximalizaci se vejde na řádek pěkně nejvýše 256 čísel.
            const string cesta = @"matice.txt";            
            using(StreamWriter sw = new StreamWriter(cesta))
            {
                Losuj(sw, n);
                Console.WriteLine("Vygenerování souboru s náhodnout maticí proběhlo úspěšně.");
                Console.WriteLine("Cesta: {0}", cesta);
            }
           
            Console.WriteLine();
            Console.Write("Zmáčknutím libovolné kláevesy ukončíte program... ");
            Console.ReadKey();
        }
    }
}
