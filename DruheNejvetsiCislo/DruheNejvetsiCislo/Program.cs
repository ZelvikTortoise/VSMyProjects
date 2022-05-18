using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DruheNejvetsiCislo
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
            const int endValue = -1;
            int max = int.MinValue;
            int secondMax = int.MinValue;
            int num;
            while ((num = NumberReader.ReadNum()) != endValue)
            {
                // List: 9 9 8 -1 would print 9.
                // We want the second highest number (not value), therefore 9 is wanted in the given example.
                if (max < num)
                {
                    secondMax = max;
                    max = num;                    
                }                    
                else if (secondMax < num)
                    secondMax = num;

            }

            Console.Write(secondMax);
        }
    }
}
