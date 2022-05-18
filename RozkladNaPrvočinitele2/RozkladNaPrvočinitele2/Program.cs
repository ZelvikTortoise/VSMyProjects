using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RozkladNaPrvocinitele1
{
    class NumberReader  // upravená třída Ctecka od pana doktora Holana.
    {
        public static int ReadNum()
        {
            bool negative = false;
            int z = Console.Read();
            while ((z < '0') || (z > '9'))
            {
                if (z == '-')
                    negative = true;    // Another z should break the cycle.

                z = Console.Read();
            }

            int x = 0;
            while ((z >= '0') && (z <= '9'))
            {
                x = 10 * x + z - '0';
                z = Console.Read();
            }

            if (negative)
                return -x;
            else
                return x;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            long x = NumberReader.ReadNum();
            if (x <= 1)
                throw new ArgumentException("The input shall be a natural number greater than 1.");

            long p = 2;
            const char separator = ' ';

            while (true)    // Ending when x == 1.
            {
                if (x % p == 0)
                {
                    Console.Write(p);
                    x = x / p;
                    if (x != 1)
                        Console.Write(separator);
                    else
                        break;
                }
                else
                    p++;
            }
        }
    }
}
