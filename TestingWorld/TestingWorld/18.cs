using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWorld
{
    class A
    {
        public void M()
        {
            Console.WriteLine("M() from A.");
        }
    }

    class B : A
    {
        public new void M()
        {
            Console.WriteLine("M() from B.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            A a = new B();
            a.M();  // Variable a's type is A, even though a.GetType() would be typeof(B).

            Console.ReadKey();
        }
    }
}
