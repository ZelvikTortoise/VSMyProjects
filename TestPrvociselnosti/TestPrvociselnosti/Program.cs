using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestPrvociselnosti
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    namespace DruheNejvetsiCislo
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
                bool primary = true;
                const string pozitiveAnswer = "ano";
                const string negativeAnswer = "ne";
                int n = NumberReader.ReadNum(); // Natural number.
                int max = (int)Math.Sqrt(n);    // The floor of the square root of the number.

                if (n != 1)
                {
                    for (int i = 2; i <= max; i++)
                        if (n % i == 0)
                        {
                            primary = false;
                            break;
                        }
                }
                else
                    primary = false;


                if (primary)
                    Console.Write(pozitiveAnswer);
                else
                    Console.Write(negativeAnswer);
            }
        }
    }
}
