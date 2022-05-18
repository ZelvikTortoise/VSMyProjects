using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWorld
{
    class ComplexNums
    {
        private int Re;
        private int Im;

        public ComplexNums(int re, int im)
        {
            this.Re = re;
            this.Im = im;
        }

        public ComplexNums(double re, double im)
        {
            this.Re = (int)Math.Floor(re);
            this.Im = (int)Math.Floor(im);
        }

        public ComplexNums(int re, int im, int useless) : this(re, im)
        {
            
        }

        public ComplexNums(int re, int im, int useless1, int useless2) : this(re, im, useless1)
        {

        }

        public ComplexNums() : this(0, 0)   // Volání parametrického konstruktoru s parametry (int, int)
        {
            // Kód (níže) se provede až po zavolaném konstruktoru (výše)!
            this.Re = 12;            
        }

        // Je lepší override ToString().
        public void PrintNum()
        {
            char sign = '+';
            if (this.Im < 0)
                sign = '-';
            bool imNum = true;
            if (Math.Abs(this.Im) == 1)
                imNum = false;

            if (this.Im == 0)
                Console.WriteLine(this.Re);
            else
            {
                if (this.Re == 0)
                {
                    if (imNum)
                        Console.WriteLine(this.Im + "i");
                    else if (sign != '+')
                        Console.WriteLine("-i");
                    else
                        Console.WriteLine("i");
                }
                else if (imNum)
                    Console.WriteLine(this.Re + " " + sign + " " + Math.Abs(this.Im) + "i");
                else
                    Console.WriteLine(this.Re + " " + sign + " " + "i");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ComplexNums z = new ComplexNums(1, 1, 3, 5);
            Console.Write("z = ");
            z.PrintNum();

            Console.ReadKey();
        }
    }
}
