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
            int i = 5;
            object o = i;

            Console.WriteLine(o.ToString());    // int má overriden To.String()
            Console.WriteLine(o);
            if (o.GetType() == typeof(int))
                Console.WriteLine("int");
            if (o.GetType() == typeof(object))  // Sleduje instanci, nikoliv typ proměnné.
                Console.WriteLine("object");

            long k = (int)o;    // Unboxing to int, then implicitly converting to long.
            // long x = (long)o;    // Throws error during unboxing: int -> long: cannot unbox.

            Console.ReadKey();
        }
    }
}
