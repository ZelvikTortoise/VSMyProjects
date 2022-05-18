using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWorld
{
    class A
    {
        public virtual void M()
        {
            Console.WriteLine("A.M()");
        }
    }

    class B : A
    {
        public override void M()
        {
            Console.WriteLine("B.M()");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            A a = new A();
            B b = new B();
            a.M();
            b.M();
            a = new B();
            a.M();
            

            Console.ReadKey();
        }
    }
}
