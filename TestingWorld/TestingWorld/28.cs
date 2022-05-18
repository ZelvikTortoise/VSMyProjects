using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 3;
            int y = 4;
            double z = (x + y) / 2d;

            if (z == 3.5)
                Console.Write("Yes.");
            else
                Console.Write("No.");

            Console.ReadKey();
        }
    }
}
