using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWorld
{
    class A
    {

    }
    class B : A
    {

    }
    class C : B
    {

    }
    class D : C
    {

    }
    class B2 : A
    {

    }
    class Int32
    {
        public int X { get; set; }
        public Int32(int a)
        {
            this.X = a;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            A a = new A();
            B b = new B();
            // b = (D)((C)((B2)(a)));   // Error.

            int x = 5;
            Int32 y = new Int32(5); // Zakrytí typu Int32!
            System.Int32 z = 5;

            // Char is always 2-byte:
            char letter = 'č';  // UTF-16 2-byte
            // Error: letter = '𝕄';   // UTF-16 4-byte

            Console.Write(nameof(letter) + " – separated: ");   // string.Concat()
            Console.WriteLine(letter);  // letter.ToString()
            Console.WriteLine(nameof(letter) + ": " + letter);  // string.Concat()
            Console.WriteLine(nameof(letter) + " + 0: " + letter + 0);  // String.Concat()
            Console.Write(nameof(letter) + " + 0 – separated: ");   // string.Concat()
            Console.WriteLine(letter + 0);  // Add
            Console.WriteLine(x + z + letter);
            Console.WriteLine(x + z + letter.ToString());
            Console.WriteLine(x.ToString() + z.ToString() + letter);
            Console.WriteLine(x.ToString() + z.ToString() + letter.ToString());
            Console.ReadKey();
        }
    }
}
