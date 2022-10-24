using System;
using System.Text;
using System.Collections.Generic;
// using c = System.Console;    // alias

namespace TeachingClassesCode1
{
    public struct Fraction
    {
        int numerator;
        int denominator;
        bool valid;

        public Fraction(int num, int den)
        {
            if (den == 0)   // nelze
            {
                numerator = 0;
                denominator = 0;
                valid = false;
                invalidFractionsCreated++;
            }
            else
            {
                if (den < 0)
                {
                    numerator = -num;
                    denominator = -den;
                }
                else
                {
                    numerator = num;
                    denominator = den;
                }                
                valid = true;
                fractionsCreated++;
            }            
        }

        public static Fraction operator +(Fraction fraction1, Fraction fraction2)
        {
            if (!fraction1.valid || !fraction2.valid)
                return new Fraction(0, 0);  // invalid
            else
                // a/b + c/d = (ad + bc)/(bd)            
                return new Fraction(
                    fraction1.numerator * fraction2.denominator + fraction2.numerator * fraction1.denominator,
                    fraction1.denominator * fraction2.denominator
                    );
            // Pozor na overflow!
        }

        public static Fraction operator -(Fraction fraction1, Fraction fraction2)
        {
            if (!fraction1.valid || !fraction2.valid)
                return new Fraction(0, 0);  // invalid
            else
                // a - b = a + (-b)
                return fraction1 + (-fraction2);
        }

        public static Fraction operator -(Fraction fraction)
        {
            if (!fraction.valid)
                return new Fraction(0, 0);  // invalid
            else
                // -(a/b) = (-a)/b        
                return new Fraction(-fraction.numerator, fraction.denominator);
        }

        public static Fraction operator *(Fraction fraction1, Fraction fraction2)
        {
            if (!fraction1.valid || !fraction2.valid)
                return new Fraction(0, 0);  // invalid
            else
                // a/b * c/d = (ac)/(bd)
                return new Fraction(
                fraction1.numerator * fraction2.numerator,
                fraction1.denominator*fraction2.denominator
                ); ;
        }

        public static Fraction operator /(Fraction fraction1, Fraction fraction2)
        {
            if (!fraction1.valid || !fraction2.valid)
                return new Fraction(0, 0);  // invalid
            else
                // a/b : c/d = a/b * d/c
                return fraction1 * Invert(fraction2);
        }

        public static Fraction Invert(Fraction fraction)
        {
            if (!fraction.valid || fraction.numerator == 0)
                return new Fraction(0, 0);  // invalid            
            else
                return new Fraction(fraction.denominator, fraction.numerator);
        }

        static int fractionsCreated;
        static int invalidFractionsCreated;
        static Fraction()
        {
            fractionsCreated = 0;
            invalidFractionsCreated = 0;
        }

        public override string ToString()
        {
            // Lze přidat zobrazování celých čísel pro násobky, ...
            return $"{this.numerator}/{this.denominator}";
        }

        // Lze přidat metodu pro krácení (Simplify(Fraction fraction) {...}), apod.
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Fraction f1 = new Fraction(-2, -5);
            Fraction f2 = new Fraction(3, -8);

            Console.WriteLine("Operations with fractions:");
            Console.WriteLine("Fraction #1: " + f1.ToString());
            Console.WriteLine("Fraction #2: " + f2.ToString());
            Console.WriteLine();
            Console.WriteLine("{0} + {1} = {2}", f1.ToString(), f2.ToString(), (f1+f2).ToString());
            Console.WriteLine("{0} - {1} = {2}", f1.ToString(), f2.ToString(), (f1-f2).ToString());
            Console.WriteLine("{0} * {1} = {2}", f1.ToString(), f2.ToString(), (f1*f2).ToString());
            Console.WriteLine("{0} / {1} = {2}", f1.ToString(), f2.ToString(), (f1/f2).ToString());
            Console.WriteLine("-({0}) = {1}", f1.ToString(), (-f1).ToString());
            Console.WriteLine("-({0}) = {1}", f2.ToString(), (-f2).ToString());
            Console.WriteLine("1/({0}) = {1}", f1.ToString(), Fraction.Invert(f1).ToString());
            Console.WriteLine("1/({0}) = {1}", f2.ToString(), Fraction.Invert(f2).ToString());
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}