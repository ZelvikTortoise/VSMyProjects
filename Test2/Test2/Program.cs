using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2
{
    interface I
    {
        int Length { get; } // Pouze metoda.
    }

    class Object : System.Object    // !!! Proto radši používat object! (Objekt může vést na nějakou divnou třídu.)
    {
        int blbost = 0;
    }

    class A : I
    {
        public int Length { get; }  // Má backing field.
        // public int x = 5;    // ... field
        // public int x => 5;   // ... property

        public string m()
        {
            Console.WriteLine("My instance isn't null!");
            return "Something.";
        }
    }

    class B : I
    {
        private int length = 0;
        public int Length { get { return Length; } }    // Přesně to stejné jako v A.
    }

    class C : I
    {
        public int Length => Length;    // To stejné.
    }

    class D : I
    {
        public int Length { get; } = 1; // To stejné, pak backing field "length" = 1.
    }

    class Program
    {                
        static void Main(string[] args)
        {
            /*/
             
            // I.

            A a = null;
            A b = new A();

            // b ??= new A();   // Lze od C# 8.0: Pokud b není null, zůstane stejné, pokud je null, přiřadí se a.

            b = a ?? new A();   // Pokud a je null, přiřadí se new A().
            b = a != null ? a : new A();    // To stejné.

            string c = "Nothing.";

            c = a?.m(); // If a is null, c = null, else m() will be called and returns c's type. (Variable c must be of nullable type!)

            /**/

            // II.

            // If:
            if (!int.TryParse("nope", out int x))
            {
                Console.WriteLine("Cannot be parsed.");
            }
            Console.WriteLine(x);   // If couldn't be parsed, then x == 0.
            x = 1;  // Exists.
            Console.WriteLine(x);

            // For:
            for (int i = 1; i < 2; i++)
            {
                Console.WriteLine("Iterating...");
            }
            // i = 0;   // Error, i doesn't exist in current context.

            /**/
        }
    }
}
