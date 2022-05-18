using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWorld
{
    class A
    {
        public A Clone()
        {
            return (A)this.MemberwiseClone();   // protected object.MemberwiseClone() usage.
        }
    }

    class B : A
    {

    }

    class C : B
    {

    }

    class X
    {

    }

    class Program
    {
        static void Main(string[] args)
        {
            A a = new B();  // Polymorfismus referenčních typů. (Struktury nemají!)

            // is sleduje celý "podstrom". (Od typu dané instance nahoru.)
            if (a is object)
                Console.WriteLine(nameof(a) + " is " + nameof(Object));
            else
                Console.WriteLine(nameof(a) + " is NOT " + nameof(Object));
            if (a is A)
                Console.WriteLine(nameof(a) + " is " + nameof(A));
            else
                Console.WriteLine(nameof(a) + " is NOT " + nameof(A));
            if (a is B)
                Console.WriteLine(nameof(a) + " is " + nameof(B));
            else
                Console.WriteLine(nameof(a) + " is NOT " + nameof(B));
            if (a is C)
                Console.WriteLine(nameof(a) + " is " + nameof(C));
            else
                Console.WriteLine(nameof(a) + " is NOT " + nameof(C));
            if (a is X)
                Console.WriteLine(nameof(a) + " is " + nameof(X));
            else
                Console.WriteLine(nameof(a) + " is NOT " + nameof(X));
            Console.Write("Remember: ");
            if (a is null)
                Console.WriteLine(nameof(a) + " is null!");
            else
                Console.WriteLine(nameof(a) + " is NOT null!");

            // GetType() sleduje instanci objektu!!! Ne typ proměnné.
            if (a.GetType() == typeof(Object))
                Console.WriteLine(nameof(a) + "'s type is " + nameof(Object));
            else
                Console.WriteLine(nameof(a) + "'s type is NOT " + nameof(Object));
            if (a.GetType() == typeof(A))
                Console.WriteLine(nameof(a) + "'s type is " + nameof(A));
            else
                Console.WriteLine(nameof(a) + "'s type is NOT " + nameof(A));
            if (a.GetType() == typeof(B))
                Console.WriteLine(nameof(a) + "'s type is " + nameof(B));
            else
                Console.WriteLine(nameof(a) + "'s type is NOT " + nameof(B));
            if (a.GetType() == typeof(C))
                Console.WriteLine(nameof(a) + "'s type is " + nameof(C));
            else
                Console.WriteLine(nameof(a) + "'s type is NOT " + nameof(C));
            if (a.GetType() == typeof(X))
                Console.WriteLine(nameof(a) + "'s type is " + nameof(X));
            else
                Console.WriteLine(nameof(a) + "'s type is NOT " + nameof(X));
            // Null nemá typ.

            Console.ReadKey();
        }
    }
}
