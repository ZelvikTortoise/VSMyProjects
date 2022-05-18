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
            char a = ' ';
            ushort num = (ushort)a; // Implicit conversion.
            Console.WriteLine(num);

            num = 65;
            a = (char)num;  // Explicit conversion.
            Console.WriteLine(a);

            // Char represents UTF-16 2-byte code of a character.
            // Ushort represents 2-byte unsigned number.
            // That's why this works:
            Console.WriteLine((char)66);

            // Nothing can be converted to or from bool in C#.

            // Int -> float is implicit but can lose information! (32 b vs. 23 + 1 b)
            // Long -> double is implicit but can lose information! (64 b vs. 52 + 1 b)

            // Decimal from or to float / double doesn't have implicit conversion. (0,3 easy vs. infinite and the other way some numbers exist too)


            // Integer constants are int / long / ulong.
            // You can use literals: L, UL, U (long, ulong or long, ulong) ... possible small letters (lower case)

            long l = 28L;
            Console.WriteLine(l);

            // Floating point constants are double.
            // You can use literals: f, d, m (float, double, decimal) ... possible capital letters

            // Error: float f = 2.5;
            float f = (float)2.5;
            float g = 2.5f;
            Console.WriteLine(f);
            Console.WriteLine(g);

            // Careful:
            int aa = 5;
            double d = aa / 2;
            Console.WriteLine(d);

            d = aa / 2.0;
            Console.WriteLine(d);

            d = aa / 2d;
            Console.WriteLine(d);

            // Careful:
            decimal m = (decimal)0.1000000000000000000000001;   // Saving double into decimal -> losing information.
            Console.WriteLine(m);

            m = 0.1000000000000000000000001m;   // Saving as decimal constant -> no information loss.
            Console.WriteLine(m);

            // Note: Decimal numbers are denormalized! (Double and float are normalized.)
            decimal n = 0.1m;
            Console.WriteLine(n);
            n = 0.10m;
            Console.WriteLine(n);
            n = 0.100m;
            Console.WriteLine(n);

            // Careful:
            double e = -0;  // 0 is int -> -0 in int -> 0.
            Console.WriteLine(d / e);

            e = -0.0;   // 0.0 is double -> -0.0 (negative zero) in double is different from 0.0
            Console.WriteLine(d / e);

            // Possible constant notation:
            const int b = 0b1010_1000_0111;  // binary
            Console.WriteLine(b);
            const int h = 0xAB_CD_E3;   // hexadecimal
            Console.WriteLine(h);
            const int dek = 123_546; // decadic
            Console.WriteLine(dek); 
            // '_' ... separator (only for visual purpose)
            Console.WriteLine(b + h + dek);


            Console.ReadKey();
        }
    }
}
