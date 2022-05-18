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
            object x = 1.3;
            int y;

            // x is int y;  // C# 7.0

            if (x is int)
            {
                y = (int)x;
            }
            else
                y = 0;
            Console.WriteLine(y);
                

            object z = "Value";
            string s = z as string;
            Console.WriteLine("s = " + s);

            Console.ReadKey();
        }
    }
}
