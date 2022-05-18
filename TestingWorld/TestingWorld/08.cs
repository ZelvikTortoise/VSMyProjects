using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWorld
{
    class Program
    {
        /* Error: public sealed virtual void m()
        {

        }*/

        static void Main(string[] args)
        {
            {
                int x = 5;
                Console.WriteLine(x);
            }            
            {
                int x;
                // Error: Console.WriteLine(x);
                x = 0;
                Console.WriteLine(x);
            }

            Console.ReadKey();
        }
    }
}
