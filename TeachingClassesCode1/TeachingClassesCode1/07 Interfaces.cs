using System;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
// using c = System.Console;    // alias

namespace TeachingClassesCode1
{
    interface IGeometricShape
    {
        double GetArea();
        double GetPerimeter();
    }

    interface IPolygon : IGeometricShape
    {
        int NumberOfVertices { get; }
    }

    interface ICircle : IGeometricShape
    {
        double Radius { get; }
        Point Center { get; }
    }

    interface IGreetable
    {
        void GreetMe();
        int NumOfTimesGreeted { get; }
    }

    class Circle : ICircle, IGreetable
    {
        public double Radius { get; private set; }
        public Point Center { get; private set; }
        public double GetArea()
        {
            return Math.PI * Radius * Radius;
        }
        public double GetPerimeter()
        {
            return 2 * Math.PI * Radius;
        }

        public void GreetMe()
        {
            NumOfTimesGreeted++;
        }
        public int NumOfTimesGreeted { get; private set; } = 0;

        public void SayHello()
        {
            Console.WriteLine("Hello!");
        }
        public int PublicAppeal { get; set; }
        // etc.
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Třída může implementovat i víc rozhraní, ale nemůže pak dědit!
            // Rozhraní nemůžou mít datové položky (atributy)!
        }
    }

}