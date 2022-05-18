using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AGCalculator
{
    sealed class Ellipse : IObject2D
    {
        public MyPoint S { get; private set; }
        public double SemiMajorAxisLength { get; private set; }
        public double SemiMinorAxisLength { get; private set; }
        public double LinearEccentricity { get; private set; }
        public bool Horizontal { get; private set; }
        private const string coordinateStringFormat = "f02";

        public Ellipse()
        {

        }   // For info purposes.
        public Ellipse(MyPoint S, double a, double b)
        {
            if (a <= 0 || b <= 0)
                throw new ArgumentException("SemiAxes' lengths cannot be less or equal to zero.");

            this.S = S;
            if (a > b)
            {
                this.SemiMajorAxisLength = a;
                this.SemiMinorAxisLength = b;
                this.LinearEccentricity = Math.Sqrt(a * a - b * b);
                this.Horizontal = true;
            }
            else
            {
                this.SemiMajorAxisLength = b;
                this.SemiMinorAxisLength = a;
                this.LinearEccentricity = Math.Sqrt(b * b - a * a);
                this.Horizontal = false;
            }            
        }

        private void GetEllipseEquationOnePart(bool coordinateX, bool majorSemiAxis, StringBuilder sb)
        {
            double temp;

            if (coordinateX)
            {
                if (S.X == 0)
                    sb.Append("x^2 ");
                else
                {
                    temp = S.X;
                    sb.Append("(x ");
                    if (temp < 0)
                    {
                        sb.Append("+ ");
                        temp = -temp;
                    }
                    else
                        sb.Append("- ");
                    sb.Append(temp.ToString(coordinateStringFormat));
                    sb.Append(")^2 ");
                }
                if (SemiMajorAxisLength != 1)
                {
                    sb.Append("/ ");
                    if (majorSemiAxis)
                        temp = SemiMajorAxisLength * SemiMajorAxisLength;
                    else
                        temp = SemiMinorAxisLength * SemiMinorAxisLength;
                    sb.Append(temp.ToString(coordinateStringFormat));
                    sb.Append(" ");
                }
            }
            else
            {
                if (S.Y == 0)
                    sb.Append("y^2 ");
                else
                {
                    temp = S.Y;
                    sb.Append("(y ");
                    if (temp < 0)
                    {
                        sb.Append("+ ");
                        temp = -temp;
                    }
                    else
                        sb.Append("- ");
                    sb.Append(temp.ToString(coordinateStringFormat));
                    sb.Append(")^2 ");
                }
                if (SemiMinorAxisLength != 1)
                {
                    sb.Append("/ ");
                    if (majorSemiAxis)
                        temp = SemiMajorAxisLength * SemiMajorAxisLength;
                    else
                        temp = SemiMinorAxisLength * SemiMinorAxisLength;
                    sb.Append(temp.ToString(coordinateStringFormat));
                    sb.Append(" ");
                }
            }
        }
        public string GetEllipseEquation()
        {
            StringBuilder sb = new StringBuilder();

            GetEllipseEquationOnePart(true, this.Horizontal, sb);  // x
            sb.Append("+ ");    // Ellipse.
            GetEllipseEquationOnePart(false, !this.Horizontal, sb); // y  
            sb.Append("= 1");

            return sb.ToString();
        }
        public string GetGeneralEquation()
        {
            StringBuilder sb = new StringBuilder();
            double a, b, temp;
            bool horizontal = this.Horizontal;

            if (horizontal)
            {
                a = SemiMajorAxisLength;
                b = SemiMinorAxisLength;
            }
            else
            {
                a = SemiMinorAxisLength;
                b = SemiMajorAxisLength;
            }

            // b^2*x^2
            temp = b * b;
            if (temp != 1)
                sb.Append(temp.ToString(coordinateStringFormat));
            sb.Append("x^2 + ");

            // a^2*y^2
            temp = a * a;
            if (temp != 1)
                sb.Append(temp.ToString(coordinateStringFormat));
            sb.Append("y^2 ");

            if (S.X != 0)
            {
                temp = -2 * b * b * S.X;
                if (temp < 0)
                {
                    sb.Append("- ");
                    temp = -temp;
                }
                else
                    sb.Append("+ ");
                if (temp != 1)
                    sb.Append(temp.ToString(coordinateStringFormat));
                sb.Append("x ");
            }
            
            if (S.Y != 0)
            {
                temp = -2 * a * a * S.Y;
                if (temp < 0)
                {
                    sb.Append("- ");
                    temp = -temp;
                }
                else
                    sb.Append("+ ");
                if (temp != 1)
                    sb.Append(temp.ToString(coordinateStringFormat));
                sb.Append("y ");
            }

            temp = b * b * S.X * S.X - a * a * b * b + a * a * S.Y * S.Y;
            if (temp != 0)
            {
                if (temp < 0)
                {
                    sb.Append("- ");
                    temp = -temp;
                }
                else
                    sb.Append("+ ");
                sb.Append(temp.ToString(coordinateStringFormat));
                sb.Append(" ");
            }

            sb.Append("= 0");

            return sb.ToString();
        }
        public string GetMidpointAndFociInfo()
        {
            StringBuilder sb = new StringBuilder();
            double e = this.LinearEccentricity;
            bool horizontal = this.Horizontal;
            double temp;

            sb.Append("S = [");
            sb.Append(S.X.ToString(coordinateStringFormat));
            sb.Append("; ");
            sb.Append(S.Y.ToString(coordinateStringFormat));
            sb.Append("], ");

            if (SemiMajorAxisLength != SemiMinorAxisLength)
            {
                sb.Append("E = [");
                temp = S.X;
                if (horizontal)
                    temp -= e;
                sb.Append(temp.ToString(coordinateStringFormat));
                sb.Append("; ");
                temp = S.Y;
                if (!horizontal)
                    temp -= e;
                sb.Append(temp.ToString(coordinateStringFormat));
                sb.Append("], ");

                sb.Append("F = [");
                temp = S.X;
                if (horizontal)
                    temp += e;
                sb.Append(temp.ToString(coordinateStringFormat));
                sb.Append("; ");
                temp = S.Y;
                if (!horizontal)
                    temp += e;
                sb.Append(temp.ToString(coordinateStringFormat));
                sb.Append("]");
            }
            else
                sb.Append(Program.language.EllipseIsCircle);    // a == b
            

            return sb.ToString();
        }
        public string GetAllVerticesInfo()
        {
            StringBuilder sb = new StringBuilder();
            double a, b, temp;
            if (this.Horizontal)
            {
                a = SemiMajorAxisLength;
                b = SemiMinorAxisLength;
            }
            else
            {
                a = SemiMinorAxisLength;
                b = SemiMajorAxisLength;
            }

            sb.Append("A = [");
            temp = S.X - a;
            sb.Append(temp.ToString(coordinateStringFormat));
            sb.Append("; ");
            sb.Append(S.Y.ToString(coordinateStringFormat));
            sb.Append("], ");

            sb.Append("B = [");
            temp = S.X + a;
            sb.Append(temp.ToString(coordinateStringFormat));
            sb.Append("; ");
            sb.Append(S.Y.ToString(coordinateStringFormat));
            sb.Append("], ");

            sb.Append("C = [");
            sb.Append(S.X.ToString(coordinateStringFormat));
            sb.Append("; ");
            temp = S.Y - b;
            sb.Append(temp.ToString(coordinateStringFormat));
            sb.Append("], ");

            sb.Append("D = [");
            sb.Append(S.X.ToString(coordinateStringFormat));
            sb.Append("; ");
            temp = S.Y + b;
            sb.Append(temp.ToString(coordinateStringFormat));
            sb.Append("]");

            return sb.ToString();
        }
        public string GetEllipseParametersInfo()
        {
            StringBuilder sb = new StringBuilder();
            double a, b, e;
            bool isCircle = false;
            if (this.Horizontal)
            {
                a = SemiMajorAxisLength;
                b = SemiMinorAxisLength;
            }
            else
            {
                a = SemiMinorAxisLength;
                b = SemiMajorAxisLength;
            }
            e = this.LinearEccentricity;

            if (a == b)
                isCircle = true;

            sb.Append("a = ");
            sb.Append(a.ToString(coordinateStringFormat));
            sb.Append(", ");

            sb.Append("b = ");
            sb.Append(b.ToString(coordinateStringFormat));
            sb.Append(", ");

            if (isCircle)
                sb.Append("(");
            sb.Append("e = ");
            sb.Append(e.ToString(coordinateStringFormat));
            if (isCircle)
                sb.Append(")");

            return sb.ToString();
        }

        public ObjectInfo GetInfo1(Language lang)
        {
            // S = [m, n]; a, b ... semiaxes' lengths
            StringBuilder sb = new StringBuilder();
            string[] lnames = new string[4] { "m", "n", "a", "b" };
            string spec;

            sb.Append("S = [");
            sb.Append(lnames[0]);
            sb.Append(", ");
            sb.Append(lnames[1]);
            sb.Append("]; ");
            sb.Append(lnames[2]);
            sb.Append(", ");
            sb.Append(lnames[3]);
            sb.Append(" ... ");
            sb.Append(lang.EllipseAxesLengthsName);
            spec = sb.ToString();

            return new ObjectInfo(lang.InfoEllipseParameters, lnames, spec);
        }
        public List<ObjectInfo> GetAllInfoPossibilities(Language lang)
        {
            return new List<ObjectInfo> { GetInfo1(lang) };
        }

        public bool IsPointOfThisObject(MyPoint X)
        {
            double par1, par2;
            bool horizontal = this.Horizontal;
            par1 = X.X - S.X;
            par1 = par1 * par1;
            if (horizontal)
                par1 = par1 / (this.SemiMajorAxisLength * this.SemiMajorAxisLength);
            else
                par1 = par1 / (this.SemiMinorAxisLength * this.SemiMinorAxisLength);

            par2 = X.Y - S.Y;
            par2 = par2 * par2;
            if (!horizontal)
                par2 = par2 / (this.SemiMajorAxisLength * this.SemiMajorAxisLength);
            else
                par2 = par2 / (this.SemiMinorAxisLength * this.SemiMinorAxisLength);


            return par1 + par2 == 1;
        }
    }
}
