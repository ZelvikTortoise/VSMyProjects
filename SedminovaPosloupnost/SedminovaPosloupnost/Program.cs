using System;

namespace SedminovaPosloupnost
{
    class Program
    {
        static long NumSeq(int n, int num)
        {
            int m = num - 1;
            long an, exp = 10;

            // Taking the decimal base.
            decimal b = ((n % m) + 1) / (1.0m * num);
            

            // Skipping the gap at the beginning.
            for (int i = m / 10; i > 0; i /= 10)
            {
                exp *= 10;
            }

            // Taking proper exponent.
            int max = n / m;
            for (int i = 1; i <= max; i++)
            {
                exp *= 10;
            }

            an = (long)(b * exp);

            return an;
        }

        static long SedmPosl(int n)
        {
            decimal b = ((n % 6) + 1) / 7.0m;
            long an, exp = 10;

            int max = n / 6;
            for (int i = 1; i <= max; i++)
            {
                exp *= 10;
            }

            an = (long)(b * exp);

            return an;
        }

        static void Main(string[] args)
        {
            //  Console.WriteLine($"a_{55} = {SedmPosl(55)}");
            
            const int max = 150;
            const int maxNum = 20;


            /*/
            Console.WriteLine("Sedminová posloupnost:");
            for (int i = 0; i < max; i++)
            {
                Console.WriteLine($"a_{i + 1} = {SedmPosl(i)}");
            }
            /*/

            /**/
            for (int num = 2; num <= maxNum; num++)
            {
                Console.WriteLine("Posloupnost čísel v desetinném rozvoji čísla 1/{0}:", num);
                for (int i = 0; i < max; i++)
                {
                    Console.WriteLine($"a_{i + 1} = {NumSeq(i, num)}");
                }
                Console.WriteLine();
            }
            /**/

            /*/
            for (int i = 0; i < max; i++)
            {
                Console.WriteLine($"a_{i + 1} = {NumSeq(i, 123)}");
            }
            /*/            
        }
    }
}

// TODO: Why are the values dropping (eg. 12 elements down for 123)?
// TODO: Create another function so we get all possible numbers from the decimal thingy? (0, 1, 2, 3, ...)



