using System.Runtime.CompilerServices;
using System.Text;

namespace KombinatorikaGenerovani
{
    internal class Program
    {
        static int n;
        static int k;
        static int pocet;
        static int[] cislo = new int[k];  // k je délka čísla
        static int[] zbyva = new int[n];  // n je počet číslic
        // Pozor na posun:
        // zbyva[0] říká, kolikrát ještě můžeme použít číslici 1
        // zbyva[n - 1] říká, kolikrát ještě můžeme použít číslici n

        // Lze vylepšit:
        // seznam číslic typu char
        // cislo změnit na typ char

        static void VBO(int pozice)
        {
            // Máme číslo.
            if (pozice == k)
            {
                VypisCislo();
                pocet++;
            }
            else
            {
                for (int i = 1; i <= n; i++)
                {
                    if (zbyva[i - 1] > 0)
                    {
                        cislo[pozice] = i;
                        zbyva[i - 1]--; // Použili jsme číslici.
                        VBO(pozice + 1);
                        zbyva[i - 1]++; // Nezapomenout – v jiné větvi číslici nepoužíváme.
                    }                    
                }
            }
        }
        static void VariaceBezOpakovani()
        {
            // Inicializace:
            Console.WriteLine("VARIACE BEZ OPAKOVÁNÍ:");
            Console.WriteLine("Vstup:");
            pocet = 0;
            ZvolK();
            ZvolN();
            ResetujCislo(k);            
            zbyva = new int[n];            
            for (int i = 0; i < n; i++)
            {
                zbyva[i] = 1; // Každá číslice může být použita nejvýše jednou.
            }
            Console.WriteLine();

            // Program:
            Console.WriteLine("Zadání:");
            Console.WriteLine("V({0}, {1}) = {2}!/{3}!", k, n, n, n - k);
            Console.WriteLine("---------------------------------");
            VBO(0);
            Console.WriteLine();
            VypisPocet();
            Console.WriteLine("---------------------------------");
        }

        static void VSO(int pozice)
        {
            // Máme číslo.
            if (pozice == k)
            {
                VypisCislo();
                pocet++;
            }
            else
            {
                for (int i = 1; i <= n; i++)
                {
                    cislo[pozice] = i;
                    VSO(pozice + 1);
                }
            }
        }
        static void VariaceSOpakovanim()
        {
            // Inicializace:            
            Console.WriteLine("VARIACE S OPAKOVÁNÍM:");
            Console.WriteLine("Vstup:");
            pocet = 0;
            ZvolK();
            ZvolN();
            ResetujCislo(k);
            Console.WriteLine();

            // Program:            
            Console.WriteLine("Zadání:");
            Console.WriteLine("V'({0},{1}) = {2}", k, n, (int)Math.Pow(n, k));
            Console.WriteLine("---------------------------------");
            VSO(0);
            Console.WriteLine();
            VypisPocet();
            Console.WriteLine("---------------------------------");
        }

        static void PermutaceBezOpakovani()
        {
            // Inicializace:
            Console.WriteLine("PERMUTACE BEZ OPAKOVÁNÍ:");
            Console.WriteLine("Vstup:");
            pocet = 0;
            ZvolN();
            if (k != n)
                k = n;
            ResetujCislo(n);
            zbyva = new int[n];
            for (int i = 0; i < n; i++)
            {
                zbyva[i] = 1; // Každá číslice může být použita nejvýše jednou.
            }
            Console.WriteLine();

            // Program:            
            Console.WriteLine("Zadání:");
            Console.WriteLine("P({0}) = {1}!", n, n);
            Console.WriteLine("---------------------------------");
            VBO(0);
            Console.WriteLine();
            VypisPocet();
            Console.WriteLine("---------------------------------");
        }

        static void PSO(int pozice)
        {
            // Máme číslo.
            if (pozice == k)
            {
                VypisCislo();
                pocet++;
            }
            else
            {
                for (int i = 1; i <= n; i++)
                {
                    if (zbyva[i - 1] > 0)
                    {
                        cislo[pozice] = i;
                        zbyva[i - 1]--; // Použili jsme číslici.
                        PSO(pozice + 1);
                        zbyva[i - 1]++; // Nezapomenout – v jiné větvi tohoto větvení číslici na této pozici nepoužíváme.
                    }
                }
            }
        }
        static void PermutaceSOpakovanim()
        {
            // Inicializace:
            Console.WriteLine("PERMUTACE S OPAKOVÁNÍM:");
            Console.WriteLine("Vstup:");
            pocet = 0;
            k = 0;  // Musí se teprve vypočítat.
            ZvolN();    // Počet druhů číslic.
            zbyva = new int[n];
            ZvolHodnotyZbyva(n);
            for (int i = 0; i < n; i++)
            {                
                k += zbyva[i];  // Výpočet k – aby se jednalo o permutace, musí platit, že číslo má k = k1 + ... + kn cifer.
            }
            ResetujCislo(k);
            Console.WriteLine();
            // Poznámka:
            // Povolujeme, aby některá z číslic v číslu byla zakázána tím, že jí dáme do zbyva[i - 1] hodnotu 0.

            // Program:
            Console.WriteLine("Zadání:");
            Console.WriteLine("P'({0}) = {1}", ArgumentProPermutaceSOpakovanim(), PocetPermutaciSOpakovanim());
            Console.WriteLine("---------------------------------");
            PSO(0);
            Console.WriteLine();
            VypisPocet();
            Console.WriteLine("---------------------------------");
        }

        static void KBO(int pozice)
        {
            // Máme číslo.
            if (pozice == k)
            {
                VypisCislo();
                pocet++;
            }
            // Speciální případ (cislo[-1] = 0).
            else if (pozice == 0)
            {
                for (int i = 1; i <= n - (k - 1); i++)
                {
                    cislo[pozice] = i;
                    KBO(pozice + 1);
                }
            }
            else
            {
                // 1. Posloupnost číslic bude rostoucí.
                // 2. Konečná podmínka ošetří, že pro zbytek čísla máme dost číslic v zásobě.
                for (int i = cislo[pozice - 1] + 1; i <= n - (k - pozice - 1); i++)
                {
                    cislo[pozice] = i;
                    KBO(pozice + 1);
                }
            }
        }        
        static void KombinaceBezOpakovani()
        {
            // Inicializace:
            Console.WriteLine("KOMBINACE BEZ OPAKOVÁNÍ:");
            pocet = 0;
            ZvolK();
            ZvolN();
            ResetujCislo(k);
            Console.WriteLine();

            // Program:
            Console.WriteLine("Zadání:");
            Console.WriteLine("C({0},{1}) = ({2} nad {3})", k, n, n, k);
            Console.WriteLine("---------------------------------");
            KBO(0);
            Console.WriteLine();
            VypisPocet();
            Console.WriteLine("---------------------------------");
        }

        static void KSO(int pozice)
        {
            // Máme číslo.
            if (pozice == k)
            {
                VypisCislo();
                pocet++;
            }
            // Speciální případ (cislo[-1] = 1).
            else if (pozice == 0)
            {
                for (int i = 1; i <= n; i++)
                {
                    cislo[pozice] = i;
                    KSO(pozice + 1);
                }
            }
            else
            {
                // Posloupnost číslic bude neklesající.
                for (int i = cislo[pozice - 1]; i <= n; i++)
                {
                    cislo[pozice] = i;
                    KSO(pozice + 1);
                }
            }
        }
        static void KombinaceSOpakovanim()
        {
            // Inicializace:
            Console.WriteLine("KOMBINACE S OPAKOVÁNÍM:");
            pocet = 0;
            ZvolK();
            ZvolN();            
            ResetujCislo(k);
            Console.WriteLine();

            // Program:
            Console.WriteLine("Zadání:");
            Console.WriteLine("C'({0},{1}) = ({2} nad {3})", k, n, n + k - 1, n - 1);
            Console.WriteLine("---------------------------------");
            KSO(0);
            Console.WriteLine();
            VypisPocet();
            Console.WriteLine("---------------------------------");

            // Další využití:
            // Cyklem přes k (od 1 do n) lze volat C'(k, n) ... vyřeší se problém rozkladů čísla n na součet přirozených čísel ("parciace").
        }

        // Vstup:
        static void ZvolN()
        {
            Console.Write("n = ");
            string? s = Console.ReadLine();
            n = Math.Max(int.Parse(s != null ? s : "0"), 0);
            // Vždy n >= 0.
        }
        static void ZvolK()
        {
            Console.Write("k = ");
            string? s = Console.ReadLine();
            k = Math.Max(int.Parse(s != null ? s : "0"), 0);
            // Vždy k >= 0.
        }
        static void ResetujCislo(int pocetCislicNového)
        {
            cislo = new int[pocetCislicNového];
        }
        static void ZvolHodnotyZbyva(int pocetCislicKVyberu)
        {
            zbyva = new int[pocetCislicKVyberu];
            Console.WriteLine("Zvolte, kolikrát se každá z číslic má v čísle vyskytnout:");
            for (int i = 0; i < zbyva.Length; i++)
            {
                Console.Write("číslice {0}: ", i + 1);
                string? s = Console.ReadLine();
                zbyva[i] = Math.Max(int.Parse(s != null ? s : "0"), 0);
                // Vždy zbyva[i] >= 0.
            }

        }
        delegate void Kombinatorika();
        static void Nic()
        { }

        // Výpis:
        static void VypisCislo()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < cislo.Length; i++)
            {
                sb.Append(cislo[i]);
            }
            Console.WriteLine(sb.ToString());
        }
        static void VypisPocet()
        {
            Console.WriteLine("Celkem: " + pocet);
        }

        // Permutace s opakováním – pomocné stringové funkce:
        private static string ArgumentProPermutaceSOpakovanim()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < zbyva.Length; i++)
            {
                sb.Append(zbyva[i]);
                sb.Append(',');
            }

            // Odstranění poslední čárky:
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
                
            return sb.ToString();
        }
        private static string PocetPermutaciSOpakovanim()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(CitatelProPermutaceSOpakovanim());
            sb.Append('/');
            sb.Append(JmenovatelProPermutaceSOpakovanim());

            return sb.ToString();
        }
        private static string CitatelProPermutaceSOpakovanim()
        {
            StringBuilder sb = new StringBuilder();
            
            // Začátek:
            sb.Append('(');

            // Součet:
            for (int i = 0; i < zbyva.Length; i++)
            {
                sb.Append(zbyva[i]);
                sb.Append('+');
            }

            // Odstranění posledního krát:
            if (sb.Length > 1)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            

            // Konec:
            sb.Append(')');
            sb.Append('!');

            return sb.ToString();
        }
        private static string JmenovatelProPermutaceSOpakovanim()
        {
            StringBuilder sb = new StringBuilder();

            // Začátek:
            sb.Append('(');

            // Součin:
            for (int i = 0; i < zbyva.Length; i++)
            {
                sb.Append(zbyva[i]);
                sb.Append('!');
                sb.Append('*');
            }

            // Odstranění posledního krát:
            if (sb.Length > 1)
            {
                sb.Remove(sb.Length - 1, 1);
            }

            // Konec:
            sb.Append(')');

            return sb.ToString();
        }

        static void Main(string[] args)
        {
            // Pomocné proměnné:
            Kombinatorika vbo = VariaceBezOpakovani;
            Kombinatorika vso = VariaceSOpakovanim;
            Kombinatorika pbo = PermutaceBezOpakovani;
            Kombinatorika pso = PermutaceSOpakovanim;
            Kombinatorika kbo = KombinaceBezOpakovani;
            Kombinatorika kso = KombinaceSOpakovanim;
            Kombinatorika nic = Nic;
            Kombinatorika komb = nic;   // V prvním běhu cyklu nic nedělám.
            string? s;
            bool pokracovat = true;
            bool zvoleno;
            int volba;

            // Hlavní rozhraní:
            Console.WriteLine("*.*.*.*.*.*.*.*.*.*.*.*.*.*.*");
            Console.WriteLine("KOMBINATORIKA střední školy");
            Console.WriteLine("*.*.*.*.*.*.*.*.*.*.*.*.*.*.*");
            while (pokracovat)
            {
                // Volání kombinatorické metody:
                komb();
                Console.WriteLine();

                zvoleno = false;
                while (!zvoleno)
                {
                    Console.WriteLine("Zvolte možnost:");
                    Console.WriteLine("1 - Variace bez opakování");
                    Console.WriteLine("2 - Variace s opakováním");
                    Console.WriteLine("3 - Permutace bez opakování");
                    Console.WriteLine("4 - Permutace s opakováním");
                    Console.WriteLine("5 - Kombinace bez opakování");
                    Console.WriteLine("6 - Kombinace s opakováním");
                    Console.WriteLine("7 - KONEC");
                    Console.WriteLine("----------------------------");
                    Console.Write("Vaše volba: ");
                    s = Console.ReadLine();
                    if (!int.TryParse(s != null ? s : "0", out volba))
                    {
                        volba = 0;
                    }

                    zvoleno = true;
                    switch (volba)
                    {
                        case 1:
                            komb = vbo;
                            break;

                        case 2:
                            komb = vso;
                            break;

                        case 3:
                            komb = pbo;
                            break;

                        case 4:
                            komb = pso;
                            break;

                        case 5:
                            komb = kbo;
                            break;
                        case 6:
                            komb = kso;
                            break;
                        case 7:
                            pokracovat = false;
                            break;
                        default:
                            zvoleno = false;
                            Console.WriteLine("Tato možnost neexistuje. Zkuste to znovu.");
                            Console.WriteLine();
                            break;                            
                    }

                    if (zvoleno)
                    {
                        Console.WriteLine("Vaše volba byla úspěšně zaznamenána!");
                        Console.WriteLine();
                    }
                }                
            }
            
            Console.WriteLine("Děkujeme za návštěvu! :)");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Zmáčkněte libovolnou klávesu pro ukončení programu... ");
            Console.ReadKey();
        }
    }
}