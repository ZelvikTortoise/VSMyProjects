using System;

namespace ComplexNumbers
{
    struct ComplexNumbers
    {
        public double RealPart { get; set; }
        public double ImaginaryPart { get; set; }
        public string Name;

        /// <summary>
        /// Creates a complex number in format of name = a + bi, where i is an imaginary unit.
        /// </summary>
        /// <param name="a">Real part</param>
        /// <param name="b">Imaginary part</param>
        /// <param name="name">Name of the number</param>
        public ComplexNumbers(double a, double b, string name)
        {
            this.RealPart = a;
            this.ImaginaryPart = b;
            this.Name = name;
        }
        /// <summary>
        /// Allows to create an identical complex number to z but with different name.
        /// </summary>
        /// <param name="z">Identical complex number</param>
        /// <param name="newName">The name of the new number</param>
        public ComplexNumbers(ComplexNumbers z, string newName)
        {
            this.RealPart = z.RealPart;
            this.ImaginaryPart = z.ImaginaryPart;
            this.Name = newName;
        }

        public bool IsZero(bool excatly)
        {
            return excatly ? this.RealPart == 0 && this.ImaginaryPart == 0 : Math.Abs(RealPart) < 1E-15 && Math.Abs(ImaginaryPart) < 1E-15;
        }        

        public ComplexNumbers GetZero()
        {
            return new ComplexNumbers(0, 0, null);
        }

        public ComplexNumbers GetOne()
        {
            return new ComplexNumbers(1, 0, null);
        }

        public ComplexNumbers GetI()
        {
            return new ComplexNumbers(0, 1, null);
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
            return new ComplexNumbers(this.RealPart, -this.ImaginaryPart, this.Name + "^_");
        }

        public ComplexNumbers GetReciprocal()
        {
            return new ComplexNumbers(new ComplexNumbers(1, 0, null) / this, "1/" + this.Name);
        }
        
        public void RenameTo(string newName)
        {
            this.Name = newName;
        }


        public static ComplexNumbers operator +(ComplexNumbers z1, ComplexNumbers z2)
        {
            return new ComplexNumbers(z1.RealPart + z2.RealPart, z1.ImaginaryPart + z2.ImaginaryPart, null);
        }
        public static ComplexNumbers operator -(ComplexNumbers z1, ComplexNumbers z2)
        {
            return new ComplexNumbers(z1.RealPart - z2.RealPart, z1.ImaginaryPart - z2.ImaginaryPart, null);
        }
        public static ComplexNumbers operator *(ComplexNumbers z1, ComplexNumbers z2)
        {
            return new ComplexNumbers(z1.RealPart * z2.RealPart - z1.ImaginaryPart * z2.ImaginaryPart, z1.RealPart * z2.ImaginaryPart +  z1.ImaginaryPart + z2.RealPart, null);
        }
        public static ComplexNumbers operator /(ComplexNumbers z1, ComplexNumbers z2)
        {
            return z1 * (z2.GetComplexConjugate() / Math.Pow(z2.GetMagnitude(), 2));
        }

        public static ComplexNumbers operator -(ComplexNumbers z)
        {
            return new ComplexNumbers(-z.RealPart, -z.ImaginaryPart, "-" + z.Name);
        }
        public static ComplexNumbers operator *(ComplexNumbers z, double c)
        {
            return new ComplexNumbers(c * z.RealPart, c * z.ImaginaryPart, null);
        }
        public static ComplexNumbers operator /(ComplexNumbers z, double c)
        {
            return new ComplexNumbers(z.RealPart / c, z.ImaginaryPart / c, null);
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
            if (IsZero(false))
            {
                return this.Name + " = 0";
            }

            int mult = 1;
            if (this.ImaginaryPart < 0)
            {
                mult = -1;
            }

            return this.Name + " = " + (this.RealPart == 0 ? "" : Math.Round(this.RealPart, 2) + " ") + (this.ImaginaryPart == 0 ? "" : (mult == 1 ? (this.RealPart == 0 ? "" : "+ ") : "- ") + (mult * Math.Round(this.ImaginaryPart, 2)).ToString() + "i");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ComplexNumbers z0 = new ComplexNumbers(1, -2, "z0");
            ComplexNumbers z1 = new ComplexNumbers(1, 3, "z1");
            ComplexNumbers z2 = new ComplexNumbers(1, 0, "z2");
            ComplexNumbers z3 = new ComplexNumbers(0, 4, "z3");
            ComplexNumbers z4 = new ComplexNumbers(0, 0, "z4");

            Console.WriteLine(z0.ToString());
            Console.WriteLine(z1.ToString());
            Console.WriteLine(z2.ToString());
            Console.WriteLine(z3.ToString());
            Console.WriteLine(z4);
            Console.WriteLine(z3.GetReciprocal());
            Console.WriteLine(z1.GetComplexConjugate());
            Console.WriteLine(z3.GetComplexConjugate());
            Console.WriteLine("|{0}| = {1}", z1, z1.GetMagnitude()); // It's better, if there is no name in the end...
            Console.WriteLine(z0 / z1 + z2 * z3 - z4.GetComplexConjugate());

            Console.WriteLine();
            Console.Write("Press any key to exit... ");
            Console.ReadKey();
        }
    }
}
