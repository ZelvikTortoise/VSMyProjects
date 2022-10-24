using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
// using c = System.Console;    // alias

namespace TeachingClassesCode1
{
    static class StaticClass
    {
        public static int num = 7;
        // int num ... nelze
        public static void SayHi()
        {
            Console.WriteLine("Saying hi...");
        }

        public static int GiveNum()
        {
            return num;
        }
        
        static StaticClass()
        {
            Console.WriteLine("Greetings from the constructor of " + nameof(StaticClass) + "!");
        }
        /*
        public StaticClass()
        {
            // NELZE
        }

        public void SayBye() {  }   // NELZE
        */
    }

    class NotStaticClass
    {
        public static string myFavouriteString = "Želva";
        public int notStaticNumber = 20;
        public const int constantNumber = 7;    // konstanty nelze měnit, při volání fungují jako statické atributy
        // static const int wrongNumber = 5;    // konstanty nemůžou být statické

        public static void ClassIntroduction()
        {
            Console.WriteLine("This class is amazing.");
            Console.WriteLine("My favourite string is \"{0}\".", myFavouriteString);
            // Console.WriteLine("This is my non-static number: {0}", notStaticNumber);    // NELZE ... jaké instance?
            // Console.WriteLine("This is my non-static number: {0}", this.notStaticNumber);   // this nepomůže
        }

        public void InstanceIntroduction()
        {
            Console.WriteLine("My class is amazing. However, I am just its instance.");
            Console.WriteLine("Favourite string of my class is \"{0}\".", myFavouriteString);
            Console.WriteLine("This is my non-static number: {0}", notStaticNumber);
        }
        
        public NotStaticClass(int notStaticNumber)
        {
            this.notStaticNumber = notStaticNumber;
        }
        public NotStaticClass()
        {
            // jinak nelze bezparametrický, protože už jsme napsali parametrický!
        }
    }

    internal static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hi from {0}!", nameof(Program));
            Console.ReadKey();

            // StaticClass x = new StaticClass();   // nelze, protože třída je statická!
            StaticClass.SayHi();
            StaticClass.num++;
            Console.WriteLine("The number is {0}.", StaticClass.GiveNum());
            Console.WriteLine();
            Console.ReadKey();

            NotStaticClass variable = new NotStaticClass(); // tvorba instance
            NotStaticClass.ClassIntroduction();
            Console.WriteLine();
            variable.InstanceIntroduction();
            Console.WriteLine();
            variable = new NotStaticClass(343); // nová instance
            variable.InstanceIntroduction();
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}