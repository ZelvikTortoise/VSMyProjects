using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWorld
{
    class A
    {
        public int x = 0;  // 4 B
        int[] p = new int[3];   // (3*4 + 4 + (4 + 8)) = 28 B (64-bit)
    }

    class Color
    {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        public Color(int r, int g, int b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            A ou = new A();   
            Console.WriteLine(ou.ToString());

            Color color = new Color(255, 255, 255);
            Console.WriteLine(color.ToString());

            object a = new A(); // 32 B
            ((A)a).x = 3;

            // 0 B + overhead (12 B)
            a = new Object();   // Pozor na novou třídu Object!
            a = new object();   // Lepší.            

            object x = 2;   // Unboxing.
            x = new object();   // Boxing.

            Console.ReadKey();
        }
    }
}
