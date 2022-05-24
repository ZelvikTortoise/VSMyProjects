using System;
using System.Collections.Generic;

namespace ComplexNumbers
{
    struct ComplexNumbers
    {
        public double RealPart { get; set; }
        public double ImaginaryPart { get; set; }

        /// <summary>
        /// Creates a complex number in format of name = a + bi, where i is an imaginary unit.
        /// </summary>
        /// <param name="a">Real part</param>
        /// <param name="b">Imaginary part</param>
        /// <param name="name">Name of the number</param>
        public ComplexNumbers(double a, double b)
        {
            this.RealPart = a;
            this.ImaginaryPart = b;
        }

        public bool IsZero(bool excatly)
        {
            return excatly ? this.RealPart == 0 && this.ImaginaryPart == 0 : Math.Abs(RealPart) < 1E-15 && Math.Abs(ImaginaryPart) < 1E-15;
        }        

        public ComplexNumbers GetZero()
        {
            return new ComplexNumbers(0, 0);
        }

        public ComplexNumbers GetOne()
        {
            return new ComplexNumbers(1, 0);
        }

        public ComplexNumbers GetI()
        {
            return new ComplexNumbers(0, 1);
        }

        public double GetMagnitude()
        {
            return Math.Sqrt(this.RealPart * this.RealPart + this.ImaginaryPart * this.ImaginaryPart);
        }

        public bool IsComplexUnit()
        {
            return Math.Abs(this.GetMagnitude() - 1) < 1E-15;
        }

        public static bool IsAdditiveInverse(ComplexNumbers z1, ComplexNumbers z2)
        {
            return z1 == -z2;
        }

        public ComplexNumbers GetComplexConjugate()
        {
            return new ComplexNumbers(this.RealPart, -this.ImaginaryPart);
        }

        public ComplexNumbers GetReciprocal()
        {
            return new ComplexNumbers(1, 0) / this;
        }

        public static ComplexNumbers operator +(ComplexNumbers z1, ComplexNumbers z2)
        {
            return new ComplexNumbers(z1.RealPart + z2.RealPart, z1.ImaginaryPart + z2.ImaginaryPart);
        }
        public static ComplexNumbers operator -(ComplexNumbers z1, ComplexNumbers z2)
        {
            return new ComplexNumbers(z1.RealPart - z2.RealPart, z1.ImaginaryPart - z2.ImaginaryPart);
        }
        public static ComplexNumbers operator *(ComplexNumbers z1, ComplexNumbers z2)
        {
            return new ComplexNumbers(z1.RealPart * z2.RealPart - z1.ImaginaryPart * z2.ImaginaryPart, z1.RealPart * z2.ImaginaryPart +  z1.ImaginaryPart + z2.RealPart);
        }
        public static ComplexNumbers operator /(ComplexNumbers z1, ComplexNumbers z2)
        {
            return z1 * (z2.GetComplexConjugate() / Math.Pow(z2.GetMagnitude(), 2));
        }

        public static ComplexNumbers operator -(ComplexNumbers z)
        {
            return new ComplexNumbers(-z.RealPart, -z.ImaginaryPart);
        }
        public static ComplexNumbers operator *(ComplexNumbers z, double c)
        {
            return new ComplexNumbers(c * z.RealPart, c * z.ImaginaryPart);
        }
        public static ComplexNumbers operator /(ComplexNumbers z, double c)
        {
            return new ComplexNumbers(z.RealPart / c, z.ImaginaryPart / c);
        }

        public bool Equals(ComplexNumbers z)
        {
            return this.RealPart == z.RealPart && this.ImaginaryPart == z.ImaginaryPart;
        }
        public override bool Equals(Object obj)
        {
            return obj is ComplexNumbers z && this == z;
        }
        public override int GetHashCode()
        {
            return this.RealPart.GetHashCode() ^ this.ImaginaryPart.GetHashCode();
        }
        public static bool operator ==(ComplexNumbers z1, ComplexNumbers z2)
        {
            return z1.RealPart == z2.RealPart && z1.ImaginaryPart == z2.ImaginaryPart;
        }
        public static bool operator !=(ComplexNumbers x, ComplexNumbers y)
        {
            return !(x == y);
        }


        public override string ToString()
        {
            // Special case:
            if (IsZero(false))
            {
                return "0";
            }

            string realPart, imaginaryPart;

            // Real part:
            if (this.RealPart == 0)
            {
                realPart = "";
            }                
            else
            {
                if (this.ImaginaryPart == 0)
                {
                    realPart = Math.Round(this.RealPart, 2).ToString();
                }
                else
                {
                    realPart = Math.Round(this.RealPart, 2).ToString() + " ";
                }
            }                     

            // Imaginary part:
            if (this.ImaginaryPart == 0)
            {
                imaginaryPart = "";
            }
            else
            {
                if (this.RealPart == 0)
                {
                    if (this.ImaginaryPart >= 0)
                    {
                        imaginaryPart = this.ImaginaryPart == 1 ? "i" : Math.Round(this.ImaginaryPart, 2).ToString() + "i";
                    }
                    else
                    {
                        imaginaryPart = this.ImaginaryPart == -1 ? "-i" : Math.Round(this.ImaginaryPart, 2).ToString() + "i";
                    }                    
                }
                else
                {
                    if (this.ImaginaryPart >= 0)
                    {
                        imaginaryPart = "+ " + (this.ImaginaryPart == 1 ? "i" : Math.Round(this.ImaginaryPart, 2).ToString() + "i");
                    }
                    else
                    {
                        imaginaryPart = "- " + (this.ImaginaryPart == -1 ? "i" : Math.Round(-this.ImaginaryPart, 2).ToString() + "i");
                    }
                }
            }

            return realPart + imaginaryPart;

            #region alternative
            /*
            int mult = 1;
            if (this.ImaginaryPart < 0)
            {
                mult = -1;
            }
            return (this.RealPart == 0 ? "" : Math.Round(this.RealPart, 2) + " ") + (this.ImaginaryPart == 0 ? "" : (mult == 1 ? (this.RealPart == 0 ? "" : "+ ") : "- ") + (mult * Math.Round(this.ImaginaryPart, 2)).ToString() + "i");
            */
            #endregion
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<ComplexNumbers> complexNumbers = new List<ComplexNumbers> {
                new ComplexNumbers(1, -2),
                new ComplexNumbers(1, 3),
                new ComplexNumbers(1, 0),
                new ComplexNumbers(0, 4),
                new ComplexNumbers(0, 0),

                new ComplexNumbers(0, -1),
                new ComplexNumbers(0, 1),
                new ComplexNumbers(-1, 0),
                new ComplexNumbers(-5, 6),
                new ComplexNumbers(-8, -7),
                new ComplexNumbers(-3, -1),

                new ComplexNumbers(5, 1),
                new ComplexNumbers(-1, -1),
                new ComplexNumbers(-2, 1)
            };
            
            Console.WriteLine("List of complex numbers:");
            for (int i = 0; i < complexNumbers.Count; i++)
            {
                Console.WriteLine("z{0} = " + complexNumbers[i], i);
            }
            Console.WriteLine();

            Console.WriteLine("Operations with complex numbers:");
            Console.WriteLine("1 / ({0}) = " + complexNumbers[3].GetReciprocal(), complexNumbers[3]);
            Console.WriteLine("conjugate of {0} = " + complexNumbers[1].GetComplexConjugate(), complexNumbers[1]);
            Console.WriteLine("conjugate of {0} = " + complexNumbers[3].GetComplexConjugate(), complexNumbers[3]);
            Console.WriteLine("|{0}| = {1}", complexNumbers[1], complexNumbers[1].GetMagnitude());
            Console.WriteLine("({0}) / ({1}) + ({2}) * ({3}) - (conjugate of {4}) = " + (complexNumbers[0] / complexNumbers[1] + complexNumbers[8] * complexNumbers[10] - complexNumbers[12].GetComplexConjugate()), complexNumbers[0], complexNumbers[1], complexNumbers[8], complexNumbers[10], complexNumbers[12]);
            Console.WriteLine();

            Console.Write("Press any key to exit... ");
            Console.ReadKey(); 
        }
    }
}
