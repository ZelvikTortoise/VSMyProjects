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

        public void Add(ComplexNums what)
        {
            this.Re += what.Re;
            this.Im += what.Im;
        }

        public ComplexNums(int re, int im)
        {
            this.Re = re;
            this.Im = im;
        }

        public ComplexNums() : this(0, 0)   // Volání parametrického konstruktoru s parametry (int, int)
        {
            // Kód (níže) se provede až po zavolaném konstruktoru (výše)!
            this.Re = 12;
        }

        public void PrintParts()
        {
            Console.WriteLine("a = " + this.Re + ", b = " + this.Im);
        }

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
            ComplexNums z = new ComplexNums(1, -3);
            Console.Write("z = ");
            z.PrintNum();
            z.PrintParts();

            Console.WriteLine();

            ComplexNums test = new ComplexNums();
            Console.Write("test = ");
            test.PrintNum();
            test.PrintParts();

            Console.ReadKey();
        }
    }
}
