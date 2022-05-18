using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWorld
{
    class X
    {
        public X(int x)
        {
            Console.Write("1");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i <= 3; i++)
            {
                X[] a = new X[10];  // Alokuje se pole, ale jeho položky budou neplatné reference (null).
            }

            Console.ReadKey();
        }
    }
}
