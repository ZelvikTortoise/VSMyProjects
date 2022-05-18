using System;

namespace MetodyPrvni
{
    class Program
    {
        public static void ProceduraVypišZnak(int kolik, char znak)
        {
            int i;
            for (i = 1; i <= kolik; i++)
            {
                Console.Write(znak);
            }
            Console.WriteLine();
        }

        public static double AbsolutníHodnota(double x)
        {
            if (x < 0)
            {
                return -x;
            }
            else
            {
                return x;
            }
        }

        static void Main(string[] args)
        {
            
            Console.Write("Kolik znaků chceš vypsat? n = ");
            int n = int.Parse(Console.ReadLine());
            Console.Write("Jaký znak chceš vypsat? Znak: ");
            char co = char.Parse(Console.ReadLine());

            ProceduraVypišZnak(n, co);
            

            Console.Write("Absolutní hodnotu kterého čísla chceš zkoumat? m = ");
            double number = double.Parse(Console.ReadLine());

            Console.WriteLine();

            Console.WriteLine("|{0}| = {1}", number, AbsolutníHodnota(number));
            Console.WriteLine("Absolutní hodnota čísla {0} je {1}.", number, AbsolutníHodnota(number));

            Console.WriteLine();

            Console.WriteLine("Teď vypíšeme m vykřičníků, je-li m celé číslo.");
            n = (int)AbsolutníHodnota(number);

            Console.WriteLine();

            ProceduraVypišZnak(n, '!');
            
            Console.WriteLine();
            Console.Write("Press any key to continue... ");
            Console.ReadKey();
        }
    }
}
