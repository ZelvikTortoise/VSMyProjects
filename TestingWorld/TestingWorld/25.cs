using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWorld
{
    class Program
    {
        static int f(int a, int b) { return a + b; }

        static void Main(string[] args)
        {
            int x = 0x7F_FF_FF_FF;
            int y = x;
            checked
            {
                x = f(x, 1);    // Because of JIT, this arithmetic overflow isn't checked.
                x -= 1;         // Arithmetic underflow.
                y += x;
            }
            Console.Write(y);

            Console.ReadKey();
        }
    }
}
