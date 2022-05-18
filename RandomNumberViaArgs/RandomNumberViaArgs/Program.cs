using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomNumberViaArgs
{
    class Program
    {
        static void Main(string[] args)
        {
            const string badUsage = "Bad usage.";
            const int minValue = 0;
            int maxValue = 0;   // Inicialized for compiler.

            if (args.Length <= 0)   // Considering more arguments as a misstype and just throwing them away.
            {
                Console.WriteLine(badUsage);
                return;
            }

            try
            {
                 maxValue = int.Parse(args[0]);
                if (maxValue < 0)   // Considering 0 a natural number.
                {
                    Console.WriteLine(badUsage);
                    return;
                }
            }
            catch
            {
                Console.WriteLine(badUsage);
                return;
            }

            Random random = new Random();
            Console.WriteLine(random.Next(minValue, maxValue + 1));
        }
    }
}
