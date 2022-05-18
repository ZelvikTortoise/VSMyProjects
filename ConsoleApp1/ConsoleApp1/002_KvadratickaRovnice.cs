using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hodina1
{
    class Program
    {
        //Metoda
        static void Main(string[] args)
        {
            //Deklarace proměnných
            double a, b, c, d, x1, x2, x;
            

            //Zadání koeficientů kvadratické rovnice
            Console.Write("Zadejte proměnnou a: ");
            a = double.Parse(Console.ReadLine());
            Console.Write("Zadejte proměnnou b: ");
            b = double.Parse(Console.ReadLine());
            Console.Write("Zadejte proměnnou c: ");
            c = double.Parse(Console.ReadLine());

            //yolo
            d = (b * b) - 4 * a * c;
            x1 = (-b + Math.Sqrt(d)) / (2 * a);
            x2 = (-b - Math.Sqrt(d)) / (2 * a);
            x = -c / b;
            //If
            if (a != 0)
            {
                if (d > 0)
                {
                    Console.WriteLine("Kořen x1 = " + x1);
                    Console.WriteLine("Kořen x2 = " + x2);
                }
                else if (d == 0)
                {
                    Console.WriteLine("Kořen x1 = " + x1 + " a kořen x2 = " + x2);
                }
                else
                {
                    Console.WriteLine("Kvadratická rovnice nemá řešení.");
                }

            }
            else if (b==0&&c==0)
            {
                Console.WriteLine("Rovnice to je, ale není. Je to řešení. Taky. Občas. Takže R.");
            }
            else if (b == 0)
            {
                Console.WriteLine("Je to jeblé moc na mě. Takže lineární bez lineárního člena man. Takže " + c + " = 0." );
            }
            else if (c == 0)
            {
                Console.WriteLine("Rovnice bez absolutního člena. Yolo. Takže .");
            }
            else
            {
                Console.WriteLine("Rovnice je lineární. Kořen rovnice x = " + x);
            }
            

        }
    }
}
