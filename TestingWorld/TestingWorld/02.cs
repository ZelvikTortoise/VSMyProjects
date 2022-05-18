using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWorld
{
    class A
    {
        public int x = 0;
    }

    class Program
    {
        static void Main(string[] args)
        {
            A a = new A();
            A b = new A();

            // Checking default object.Equals() which checks instances.
            if (a.Equals(b))
                Console.WriteLine("Equals.");
            else
                Console.WriteLine("Doesn't equal.");

            // == should be the same as Equals()
            if (a == b)
                Console.WriteLine("Equals.");
            else
                Console.WriteLine("Doesn't equal.");

            // Checking ValueType.Equals() override which checks values byte by byte (if contains only value types) or Reflection (if contains a reference type like string).
            // Note: Reflection is +- 20 times slower but always safe.
            if (a.x.Equals(b.x))
                Console.WriteLine("Equals.");
            else
                Console.WriteLine("Doesn't equal.");            

            // == should be the same as Equals()
            if (a.x == b.x)
                Console.WriteLine("Equals.");
            else
                Console.WriteLine("Doesn't equal.");

            Console.ReadKey();
        }
    }
}
