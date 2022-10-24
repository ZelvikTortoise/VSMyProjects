using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
// using c = System.Console;    // alias

namespace TeachingClassesCode1
{
    // modifikátor přístupu - statická/instanční - sealed/lze dědit - class - identifikátor - předek/interfacy
    public class MyClass
    {
        public int number = 58;
        public string name = "Lukáš";

        public void HelloClass()
        {
            Console.WriteLine("Hello world from my class called {0}.", nameof(MyClass));
            Console.WriteLine("My number is {0} and my name is {1}.", number, name);
        }

        public const double almostPie = 3.14d;
        public static int numberOfSayingIt = 0;

        public static void SayIt()
        {
            Console.WriteLine("I am your class.");
            numberOfSayingIt++;
        }
    }

    internal static class Program
    {
        static void Main(string[] args)
        {
            MyClass cl = new MyClass();    // tvorba instance
            cl.HelloClass();
            Console.WriteLine();

            Console.WriteLine("Nové číslo:");
            cl.number = 91;
            cl.HelloClass();
            Console.WriteLine();

            Console.WriteLine("Nová instance:");
            cl = new MyClass();
            cl.HelloClass();
            Console.WriteLine("Konec instančních metod.");
            Console.WriteLine();
            
            Console.WriteLine("Začátek statických metod.");
            Console.WriteLine("It said it " + MyClass.numberOfSayingIt + " times.");
            MyClass.SayIt();
            MyClass.SayIt();
            MyClass.SayIt();
            MyClass.SayIt();
            Console.WriteLine("It said it " + MyClass.numberOfSayingIt + " times.");
            Console.WriteLine(MyClass.almostPie + 3);


            Console.ReadKey();
        }
    }
}