using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWorld
{
    class A
    {
        private readonly double x = 0;
        private double y = 0;

        public double X
        {
            get;
        } = -5; // Possible inicialization.

        public double Y
        {
            get
            {
                return y;
            }
        }

        public A()
        {
            x = 6;
            y = 10;
        }

        public void ChangeX()
        {
            // x = 3;   // Error because of readonly.
        }

        public void ChangeY()
        {
            y = 3;  // Possible.
        }
    }
    class Program   // internal
    {
        static void Main(string[] args)
        {
            // A a = new A();   // Would be possible, classes are INTERNAL by defualt. (If 14.cs is included.)
            // Error -> only 1 class Program, 1 Main(...)!

            // Inside interfaces: Always public.
            // Inside classes or structs: Private as default.
            // Classes can be also public but it's not used often.
        }
    }
}
