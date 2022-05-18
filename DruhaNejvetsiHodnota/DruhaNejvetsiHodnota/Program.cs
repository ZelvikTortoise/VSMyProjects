using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DruhaNejvetsiHodnota
{
    class NumberReader  // upravená třída Ctecka od pana profesora Holana.
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
                if (max < num)
                {
                    secondMax = max;
                    max = num;
                }
                else if (num == max)
                    continue;   // Do nothing.
                else if (secondMax < num)
                    secondMax = num;
            }

            Console.Write(secondMax);

            Console.ReadKey();
        }
    }
}
