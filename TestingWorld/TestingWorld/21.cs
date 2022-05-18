using System;

class A<T>
{
    public class B : A<long>
    {
        public void f()
        {
            Console.WriteLine(
            typeof(T).ToString());
        }
        public class C : A<long>.B { }
    }
}
class Prg5
{
    static void Main()
    {
        var c = new A<float>.B.C();
        c.f();

        Console.ReadKey();
    }
}