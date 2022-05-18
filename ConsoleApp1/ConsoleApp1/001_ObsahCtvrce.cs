using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hodina1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Deklarace proměnné
            double a;
            double S;

            //Název programu
            Console.Out.WriteLine("VÝPOČET OBSAHU ČTVERCE");
            Console.Out.WriteLine("----------------------\n");

            //Zadávání hodnoty
            Console.Out.Write("Zadejte a: ");
            a = double.Parse(Console.ReadLine());

            //Hodnota proměnné
            Console.Out.WriteLine("\na = " + a + " cm.\n");

            //Výpočet
            S = a * a;
            //Console.Out.WriteLine("Obsah čtverce o straně a = " + a + " cm, je " + S + " cm2.\n");
            Console.WriteLine("Obsah čtverce o straně a = {0:F4} cm je {1:F4} cm2.\n", a, S);


            //Konec programu
            Console.Out.WriteLine("KONEC PROGRAMU");
            Console.Out.WriteLine("-*-*-*-*-*-*-*-");

            Console.ReadKey();


        }
    }
}
