using System;
using System.Text;

namespace SedminovaPosloupnost
{
    class Program
    {
        static long NumSeq(int n, int num)
        {
            const int overflow = -1;
            int m = num - 1;
            long an, exp = 10;

            // Taking the decimal base.
            decimal b = ((n % m) + 1) / (1.0m * num);


            // Skipping the gap at the beginning.
            for (int i = m / 10; i > 0; i /= 10)
            {
                exp *= 10;
                if (exp <= 0)
                {
                    return overflow;
                }
            }            

            // Taking proper exponent.
            int max = n / m;
            for (int i = 1; i <= max; i++)
            {
                exp *= 10;
                if (exp <= 0)
                {
                    return overflow;
                }
            }
            
            an = (long)(b * exp);
            if (an < 0)
            {
                return overflow;
            }

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

        static string GetGap(int totalSpaces, char space, int index)
        {
            int spacesToAdd = totalSpaces;
            int remaining = index;

            if (totalSpaces <= 0)
            {
                return "";
            }

            if (index == 10)
            {
                Console.Write("");
            }

            if (index == 0)
            {
                // Number 0 is a one-digit number.
                spacesToAdd = totalSpaces - 1;
            }            
            else if (index < 0)
            {
                // Creating space for the minus sign, then interpreting index as positive.
                spacesToAdd = -1;
                index = -index;
            }

            // Ignoring only 0, other values are accepted.
            while (remaining > 0)
            {
                remaining /= 10;
                spacesToAdd--;
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= spacesToAdd; i++)
            {
                sb.Append(space);
            }

            return sb.ToString();
        }

        static void Main(string[] args)
        {
            //  Console.WriteLine($"a_{55} = {SedmPosl(55)}");

            const int max = 163;
            const int maxNum = 10;
            const int indexSpaces = 3;

            /*/
            Console.WriteLine("Sedminová posloupnost:");
            for (int i = 0; i < max; i++)
            {
                Console.WriteLine($"a_{i + 1} = {SedmPosl(i)}");
            }
            /*/

            /**/
            long result;
            const string overflow = "OVERFLOW";
            string output;
            int j;
            for (int num = 2; num <= maxNum; num++)
            {
                Console.WriteLine("Posloupnost čísel v desetinném rozvoji čísla 1/{0}:", num);
                for (int i = 0; i < max; i++)
                {
                    result = NumSeq(i, num);
                    output = result != -1 ? result.ToString() : overflow;
                    j = i + 1;  // So indeces start at 1, not 0.
                    Console.WriteLine($"{GetGap(indexSpaces, ' ', j)}a_{j} = {output}");
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

            Console.Write("Press any key to close the program... ");
            Console.ReadKey();
        }
    }
}

// TODO: Why are the values dropping (eg. 12 elements down for 123)?
// TODO: Create another function so we get all possible numbers from the decimal thingy? (0, 1, 2, 3, ...)
