using System;
using System.Text;
using System.Collections.Generic;
// using c = System.Console;    // alias

namespace TeachingClassesCode1
{
    abstract class Animal
    {
        public readonly string? Name = "John Smith";
        public abstract void MakeSound();

        public virtual void IntroduceYourself()
        {
            Console.WriteLine("My name is " + Name + ".");
        }

        public void SayHi()
        {
            Console.WriteLine("Hello world!");
        }
    }

    class Dog : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine("Woof.");
        }

        public sealed override void IntroduceYourself()     // tahle metoda už nemůže být overridována (přepsána)
        {
            Console.WriteLine("Woof. My name is " + Name + ". Woof.");
        }
    }

    sealed class Dalmatian : Dog
    {
        // vše se dědí až na konstruktor
        // konstruktor je implicitní bezparametrický
        
        // public override Intro... // nelze
    }

    sealed class Doberman : Dog
    {
        // vše se dědí až na konstruktor
        // konstruktor je implicitní bezparametrický
    }

    class Cat : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine("Meow.");
        }

        public override void IntroduceYourself()
        {
            Console.WriteLine("Meow name is " + Name + ".");
        }
    }

    class Duck : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine("Quack.");
        }

        public new void SayHi() // Překrytí => new, jinak warning
        {
            Console.WriteLine("Hello, I am a duck!");
        }
    }

    internal static class Program
    {       
        static void Main(string[] args)
        {            
            Animal[] zoo = {
            new Dog(),
            new Dalmatian(),
            new Doberman(),
            new Cat(),
            new Duck()
            };

            foreach (Animal animal in zoo)
            {
                Console.WriteLine(animal.GetType());
                animal.IntroduceYourself();
                animal.SayHi();
                animal.MakeSound();                
                Console.WriteLine();
            }
            Console.WriteLine("Konec představení.");
            Console.ReadKey();
            Console.WriteLine();


            // Pozor na překrytí - záleží na typu proměnné:
            Animal duck1 = new Duck();
            Console.WriteLine(duck1.GetType());
            duck1.IntroduceYourself();
            duck1.SayHi();  // !!!
            duck1.MakeSound();
            Console.WriteLine();

            Duck duck2 = new Duck();
            Console.WriteLine(duck2.GetType());
            duck2.IntroduceYourself();
            duck2.SayHi();  // !!!
            duck2.MakeSound();
            Console.WriteLine();


            Console.ReadKey();
        }
    }
}