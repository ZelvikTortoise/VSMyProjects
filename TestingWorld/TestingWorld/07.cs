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
            string q = nameof(q);
            string r = nameof(r);
            string s = q ?? r;
            Console.WriteLine(s);

            int? y = 5;
            int? z = null;
            int? x = z ?? y;
            Console.WriteLine(x);

            y = null;
            x = y ?? z;
            Console.WriteLine(x);

            bool? a = null;
            bool? b = false;


            // Careful:
            bool? c = a & b ?? a;
            Console.WriteLine(c);

            b = true;
            c = a | b ?? a;
            Console.WriteLine(c);

            if (typeof(int) == typeof(Int32))
                Console.WriteLine(true);
            else
                Console.WriteLine(false);

            object o = 5;   // Boxing.
            // Erorr: long notBox = o; // Unboxing doesn't perform any conversions!
            long notBox = (int)o;   // Explicit conversion after boxing and then using implicit conversion to long.

            o = 10L;
            int i = (int)(long)o;   // Unboxing o into long, then explicitly converting to int.


            // Note: NULLABLE TYPES like int? are boxed as int. (If there was null, then no boxing needed, just null reference.)
            // Unboxing int? as null -> int? + HasValue == false
            // Unboxing int? as NOTnull -> int? + HasValue == true

            Console.ReadKey();
        }
    }
}
