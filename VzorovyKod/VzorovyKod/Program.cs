using System;   // Důležité.

namespace VzorovyKod
{
    class Program
    {
        // Vlastní procedura:
        static void Prohod(ref int a, ref int b)
        {
            // Prohodí hodnoty dvou proměnných.
            // U parametrů musí být ref, aby se jejich hodnoty po volání opravdu změnily. (Předávají se hodnotou.)

            // Na prohození hodnoty vždy potřebujeme alespoň 3 proměnné!
            int pomocna = a;
            a = b;
            b = pomocna;        
        }

        // Metoda Main():
        static void Main(string[] args)
        {
            // Convert, Parse
            // Console.
            // if, switch
            // cykly

            // Proměnné:
            int a, b, c;      // Deklarace proměnných
            int d = 1;        // Deklarace s inicializací
            int e = 2, f = 3;   // Deklarace s inicializací
            double g = 3.1;     // U desetinných čísel je desetinná TEČKA
            string h = "text";   // Stringové konstanty (texty) se dávají do ""
            char i = 'z';       // Charové konstanty (znaky) se dávají do ''

            // Výstup:
            Console.Write("Petr chodí.");   // Text bez enteru.
            Console.WriteLine();            // Pouze enter.
            Console.WriteLine("Pořád chodí.");  // Text s enterem.

            // Vstup:
            int j = int.Parse(Console.ReadLine());  // String se musí přeparsovat na int, protože Console.ReadLine() vrací string. Popis: Využije se metoda Parse() datového typu int, která ze stringu udělá int, je-li to možné. (Jinak vyhodí výjimku.)
            int k = Convert.ToInt32(Console.ReadLine());    // Jiný zápis. Popis: String Console.ReadLine() se překonvertuje na int pomocí metody ToInt32() ze třídy Convert.
            string l = Console.ReadLine();  // Zde není potřeba parsování / konvertování.

            // Podmíněný příkaz:
            int m = 5;
            if (m > 5 || m < 5) // Pozn.: Řetězec || znamená "nebo". Pokud chcete konjunkci ("a"), použijte &&.
            {
                Console.WriteLine("Platí, že " + nameof(m) + " != 5, tedy hodnota zmíněné proměnné není rovna 5.");
            }
            else;
            {
                Console.WriteLine("Platí, že {0} == 5, tedy hodnota zmíněné proměnné je rovna 5.", nameof(m));
            }
            // Poznámka: Používám 2 zápisy, jak vložit hodnotu některé proměnné přímo do stringu Console.WriteLine().

            string n = "abcdef";
            if (n.Length < 3)
            {
                Console.WriteLine("Délka řetězce " + n + " je menší než 3.");
            }
            else if (n.Length == 3)     // Porovnávání na rovnost se provádí pomocí dvou rovnítek!!! (Jinak se jedná o přiřazování.)
            {
                Console.WriteLine("Délka řetězce " + n + " je rovna 3.");
            }
            else
            {
                Console.WriteLine("Délka řetězce " + n + " je větší než 3.");
            }


            // Switch:
            int o = 4;
            switch (o)
            {
                case 1:
                    Console.WriteLine("Výborné.");
                    break;  // Break je nutný za každou sérii caseů pro dané tělo.
                case 2:
                case 3:
                    Console.WriteLine("Chvalitebné nebo dobré.");
                    break;
                case 5: // Pozn.: Na pořadí caseů nezáleží.
                    Console.WriteLine("Nedostatečné.");
                    break;
                case 4:
                    Console.WriteLine("Dostatečné.");
                    break;
                default:
                    Console.WriteLine("Neznámá známka.");
                    break; // I zde je break nutný.
            }

            // For cyklus:
            // Poznámka: Proměnná je lokální, proto je možné ji pak použít znovu.
            for (int p = 1; p <= 10; p++)
            {
                Console.Write("*");     // Cyklus napíše 10 hvězdiček za sebou.
            }
            Console.WriteLine();    // A odřádkuje.

            for (int p = 50; p > 1; p--)    // Dekrementace. (U předchozího cyklu dochází k inkremetaci.)
            {
                if (p != 1)
                    Console.Write(p + " ");
                else
                    Console.WriteLine(p);

                // Poznámka: Od VS 2017 lze if psát bez  {}, pokud tělo obsahuje pouze 1 příkaz.
            }

            // While cyklus:
            int q = 5;
            while (q > 0)
            {
                Console.WriteLine(q + ": #");
                q--;    // Bez tohoto řádku by cyklus byl nekonečný!
            }


            // Volání vlastní metody:
            int r = 1;
            int s = 2;
            Prohod(ref r, ref s);
            Console.WriteLine("r: {0}", r);
            Console.WriteLine("s: {0}", s);


            // Hezké zakončení programu, pokud se spouští pomocí F5. (Implicitně je zabudované pomocí Ctrl + F5.)
            Console.WriteLine();
            Console.Write("Press any key to continue... ");
            Console.ReadKey();
        }
    }
}
