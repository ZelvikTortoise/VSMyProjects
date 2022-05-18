using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWorld
{
    interface I1
    {
        void m();
    }

    interface I2 : I1
    {
        void n();
    }

    class A : I2
    {
        // Třída A musí implementovat jak rozhraní I2, tak rozhraní I1, protože rozhraní I2 je potomek I1.
        public void m()
        {
            Console.WriteLine("Calling m() from A.");
        }

        public void n()
        {
            Console.WriteLine("Calling n() from A.");
        }
    }

    class B : I1
    {
        // Třída B musí implementovat jenom rozhraní I1.
        public void m() // Nutné public.
        {
            Console.WriteLine("Calling m() from B.");
        }
    }
    
    class C : B
    {
        // Třída C nemusí implementovat žádné rozhraní, dědí m() z B. Kdyby si vytvořila svoji m(), zakryla by m() z B.
        public new void m()
        {
            Console.WriteLine("Calling m() from C hiding m() from B.");
        }
    }

    class D : B, I1
    {
        // Třída C nemusí implementovat žádné rozhraní, dědí m() z B. Kdyby si vytvořila svoji m(), zakryla by m() z B.
        public new void m()
        {
            Console.WriteLine("Calling m() from D hiding m() from B.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            A a = new A();
            B b = new B();
            C c = new C();

            a.m();
            a.n();
            b.m();
            c.m();

            I1 i = new C(); // i doesn't have C.m() because C doesn't implement I1, that's why it calls B.m().
            i.m();
            i = new D();
            i.m();  // D implements I1, that's why D.m() can be called by i.

            Console.ReadKey();
        }
    }
}
