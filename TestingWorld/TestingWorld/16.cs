using System;
using System.Runtime.CompilerServices;  // Příkazy pro JIT.

namespace TestingWorld
{    
    class A
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Foo()
        {
            byte b = 0;
            b--;
            Console.WriteLine(b);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void CheckedFoo()
        {
            byte b = 0;
            checked     // Checked block must be inside the method's body to work. (Because of JITint the method.)
            {
                b--;
            }
            Console.WriteLine(b);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            A a = new A();
            checked
            {               
                a.Foo();
            }

            try
            {
                a.CheckedFoo();
            }
            catch (ArithmeticException)
            {
                Console.WriteLine("Arithmetic underflow or overflow.");
            }


            Console.ReadKey();
        }
    }
}
