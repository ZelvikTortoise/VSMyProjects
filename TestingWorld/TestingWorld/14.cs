using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWorld
{
    interface I
    {
        int X
        {
            get;
            set;
        }

        int Y
        {
            get;
        }
    }

    class A : I
    {
        private int x;  // Backing field of X property. (Otherwise StackOverflowException.)

        public int X
        {
            get => x;
            set { if (value == int.MinValue) { throw new ArgumentException("Cannot set this value."); } x = value; }    // It's a method so we can put real code in it.
        }

        public int Y => 8;  // No backing field, only getter. (Always returns 8.)

        public int Z { get; set; }  // Autoimplemented property. (Has hidden backing field.)
    }

    class Program
    {
        static void Main(string[] args)
        {
            A a = new A();
            int x;
            // x = a.get_X();   // Error.
            // a.set_X(2);  // Error.
            x = a.X;
            a.X = 2;


            Console.WriteLine(nameof(x) + ": " + x);
            Console.WriteLine(nameof(a) + "." + nameof(a.X) + ": " + a.X);
            x = a.Y;
            Console.WriteLine(nameof(x) + ": " + x);
            Console.WriteLine(nameof(a) + "." + nameof(a.Y) + ": " + a.Y);
            a.Z = -7;
            x = a.Z + 2;
            Console.WriteLine(nameof(x) + ": " + x);
            Console.WriteLine(nameof(a) + "." + nameof(a.Z) + ": " + a.Z);
            Console.WriteLine();

            try
            {
                a.X = int.MinValue;
            }
            catch (ArgumentException argExc)
            {
                Console.WriteLine(argExc.Message);
                Console.WriteLine("Exception caught.");
            }

            Console.ReadKey();
        }
    }
}
