using System;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
// using c = System.Console;    // alias

namespace TeachingClassesCode1
{
    class A
    {
        public string? Name { get; set; }
        public int year;
        public static double number = 3d;

        public void SaySeven()
        {
            if (number == 7)
                Console.WriteLine(".neveS");
            else
                Console.WriteLine("Seven.");
        }
        public int GetNumber()
        {
            return 7;
        }

        public A()
        {
            Console.WriteLine("Hello from A.");
        }

        public A(string name, int year) : this()
        {
            Name = name;
            this.year = year;
        }
    }

    class B : A
    {
        protected bool niceAttribute;

        public B()
        {
            Console.WriteLine("Hello from B.");
        }

        public B(string name, int year) : this()
        {
            Name = name;
            this.year = year;
        }

        public B(string name, int year, bool nice) : this(name, year)
        {
            this.niceAttribute = nice;
        }

        // Nechceme:
        public B(int year, string name) : base(name, year)  // Volá se parametrický konstruktor z A.
        {
            Name = name;
            this.year = year;
        }        
    }

    sealed class C : B  // nemůže mít další potomky
    {
        public void ChangeNice()
        {
            niceAttribute = !niceAttribute;
        }

        public C()
        {
            Console.WriteLine("Hello from C.");
        }

        public C(string name, int year) : this()
        {
            Name = name;
            this.year = year;
        }

        public C(string name, int year, bool nice) : this(name, year)
        {
            this.niceAttribute = nice;
        }

        // Nechceme:
        /**/
        public C(int year, string name) : base(name, year)  // Volá se parametrický konstruktor z B.
        {
            Name = name;
            this.year = year;
        }
        /**/

        /*/
        // Nechceme:
        public C(int year, string name) : base(year, name)  // Volá se parametrický konstruktor z A.
        {
            Name = name;
            this.year = year;
        }
        /*/
    }

    /*
    class D : C // error, protože C je sealed
    {
        // ...
    }
    */

    class X : A
    {
        public static readonly char bestChar = 'x';

        public X()
        {
            Console.WriteLine("Hello from X.");
        }

        public X(string name, int year) : this()
        {
            Name = name;
            this.year = year;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            #region 1
            /*/
            A a = new A();
            Console.WriteLine();
            B b = new B();
            Console.WriteLine();
            C c = new C();
            Console.WriteLine();
            X x = new X();
            Console.WriteLine();
            /*/
            #endregion
            #region 2
            /*/
            A testVar;
            testVar = new A("Petr", 2000);
            Console.WriteLine("End of A.");
            Console.WriteLine();

            testVar = new B("Petr", 2000);
            Console.WriteLine();
            testVar = new B(2000, "Petr");
            Console.WriteLine();
            testVar = new B("Petr", 2000, true);
            Console.WriteLine("End of B.");
            Console.WriteLine();

            testVar = new C("Petr", 2000);
            Console.WriteLine();
            testVar = new C(2000, "Petr");
            Console.WriteLine();
            testVar = new C("Petr", 2000, true);
            Console.WriteLine("End of C.");
            Console.WriteLine();

            testVar = new X("Petr", 2000);
            Console.WriteLine("End of X.");
            Console.WriteLine();
            /*/
            #endregion
            #region 3
            /*/
            A a = new A();
            B b = new B();
            a = b;  // funguje (implicitní konverze)
            // b = a;  // nefunguje (nutná explicitní konverze)
            long n = 50;
            int m = (int)n; // ořezání

            b = new B();    // pro A b = new ...() lze měnit.
            Console.WriteLine("Konec pozdravů.");
            Console.WriteLine();

            if (b is A) // taky ... pozoruje celý podstom!
                Console.WriteLine("b is A");
            else
                Console.WriteLine("b is not A");
            if (b is B) // jasné
                Console.WriteLine("b is B");
            else
                Console.WriteLine("b is not B");
            if (b is C) // odhalí až za běhu
                Console.WriteLine("b is C");
            else
                Console.WriteLine("b is not C");
            if (b is X) // odhalí už při překladu
                Console.WriteLine("b is X");
            else
                Console.WriteLine("b is not X");
            Console.WriteLine("Konec isů.");
            Console.WriteLine();
            // Lze pomocí nameof(A), ...

            if (b.GetType() == typeof(A))   // už ne, protože B b!!!
                Console.WriteLine("b je typu A");
            else
                Console.WriteLine("b není typu A");
            if (b.GetType() == typeof(B)) // jasné
                Console.WriteLine("b je typu B");
            else
                Console.WriteLine("b není typu B");
            if (b.GetType() == typeof(C)) // odhalí až za běhu
                Console.WriteLine("b je typu C");
            else
                Console.WriteLine("b není typu C");
            if (b.GetType() == typeof(X)) // odhalí už při překladu
                Console.WriteLine("b je typu X");
            else
                Console.WriteLine("b není typu X");
            Console.WriteLine("Konec typoefů.");
            Console.WriteLine();
            // => is trochu pomaljší než typeof, ale projde celý podstrom.
            // null -> is -> vždy false

            // pro konverzi lze používat B b = a as B; -> nespadne (když nelze, vrátí null)
            A a2 = new A();
            B b2 = new B("Petr", 2000);
            if (b2 == null)
                Console.WriteLine("b2 je null před as");
            else
                Console.WriteLine("b2 není null před as");
            b2 = a2 as B;
            if (b2 == null)
                Console.WriteLine("b2 je null po as");
            else
                Console.WriteLine("b2 není null po as");
            /*/
            #endregion

            Console.ReadKey();
        }
    }
}