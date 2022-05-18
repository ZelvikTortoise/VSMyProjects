using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AGCalculator
{
    sealed class Hyperbola : IObject2D
    {
        public MyPoint S { get; private set; }
        public double LengthA { get; private set; }
        public double LengthB { get; private set; }
        public double LinearEccentricity { get; private set; }
        public bool OpenTopAndBottom { get; private set; }
        private const string coordinateStringFormat = "f02";

        public Hyperbola()
        {

        }   // For info purposes.
        public Hyperbola(MyPoint S, double a, double b, bool openTopAndBottom)
        {
            if (a <= 0 || b <= 0)
                throw new Exception("None of the parameters \"a, b\" cannot be less or equal to zero.");
            this.S = S;
            this.LengthA = a;
            this.LengthB = b;
            this.LinearEccentricity = Math.Sqrt(a * a + b * b);
            this.OpenTopAndBottom = openTopAndBottom;
        }
        public Hyperbola(double e, double a, MyPoint S, bool openTopAndBottom)
        {
            if (a <= 0 || e <= 0)
                throw new Exception("None of the parameters \"a, e\" cannot be less or equal to zero.");
            this.S = S;
            this.LinearEccentricity = e;
            this.LengthA = a;
            if (e <= a)
                throw new Exception("The parameter \"e cannot be less or equal to the parameter \"a.");
            this.LengthB = Math.Sqrt(e * e - a * a);
            this.OpenTopAndBottom = openTopAndBottom;
        }
        public Hyperbola(double e, MyPoint S, double b, bool openTopAndBottom)
        {
            if (e <= 0 || b <= 0)
                throw new Exception("None of the parameters \"e, b\" cannot be less or equal to zero.");
            this.S = S;
            this.LinearEccentricity = e;
            this.LengthB = b;
            if (e <= b)
                throw new Exception("The parameter \"e cannot be less or equal to the parameter \"b.");
            this.LengthA = Math.Sqrt(e * e - b * b);
            this.OpenTopAndBottom = openTopAndBottom;
        }
        // Note: Current version of this calculator doesn't support hyperbolas open to left and right (in design).

        private void GetHyperbolaEquationOneFraction(bool x, StringBuilder sb)
        {
            double s;
            string coordinate;
            double par2;
            double temp;
            
            if (x)
            {
                s = this.S.X;
                coordinate = "x";
                par2 = this.LengthA * this.LengthA;
            }
            else
            {
                s = this.S.Y;
                coordinate = "y";
                par2 = this.LengthB * this.LengthB;
            }

            if (s == 0)
            {
                sb.Append(coordinate);
                sb.Append("^2 ");
            }
            else
            {
                temp = -s;
                sb.Append("(");
                sb.Append(coordinate);
                sb.Append(" ");
                if (temp < 0)
                {
                    sb.Append("- ");
                    temp = -temp;
                }
                else
                    sb.Append("+ ");
                // temp = |s| now.
                sb.Append(temp.ToString(coordinateStringFormat));
                sb.Append(")^2 ");
            }
            if (par2 != 1)
            {
                sb.Append("/ ");
                sb.Append(par2.ToString(coordinateStringFormat));
                sb.Append(" ");
            }            

            // sb will be returned as a parameter (StringBuilder is a reference type).
        }
        public string GetHyperbolaEquation()
        {
            StringBuilder sb = new StringBuilder();

            if (this.OpenTopAndBottom)
            {
                GetHyperbolaEquationOneFraction(true, sb);
                sb.Append("- ");
                GetHyperbolaEquationOneFraction(false, sb);
            }
            else
            {
                GetHyperbolaEquationOneFraction(false, sb);
                sb.Append("- ");
                GetHyperbolaEquationOneFraction(true, sb);
            }

            sb.Append("= 1");

            return sb.ToString();
        }
        public string GetGeneralEquation()
        {
            StringBuilder sb = new StringBuilder();
            double a2 = this.LengthA * this.LengthA;
            double b2 = this.LengthB * this.LengthB;
            double m = this.S.X;
            double n = this.S.Y;
            double temp;

            if (this.OpenTopAndBottom)
            {
                if (b2 != 1)
                    sb.Append(b2.ToString(coordinateStringFormat));
                sb.Append("x^2 - ");
                if (a2 != 1)
                    sb.Append(a2.ToString(coordinateStringFormat));
                sb.Append("y^2 ");
                temp = -2 * b2 * m;
                if (temp != 0)
                {
                    if (temp < 0)
                    {
                        sb.Append("- ");
                        temp = -temp;
                    }
                    else
                        sb.Append("+ ");
                    // temp = |-2*b^2*m| now.
                    if (temp != 1)
                        sb.Append(temp.ToString(coordinateStringFormat));
                    sb.Append("x ");
                }
                temp = 2 * a2 * n;
                if (temp != 0)
                {
                    if (temp < 0)
                    {
                        sb.Append("- ");
                        temp = -temp;
                    }
                    else
                        sb.Append("+ ");
                    // temp = |2*a^2*n| now.
                    if (temp != 1)
                        sb.Append(temp.ToString(coordinateStringFormat));
                    sb.Append("y ");
                }
                temp = -a2 * n * n + b2 * m * m - a2 * b2;
                if (temp != 0)
                {
                    if (temp < 0)
                    {
                        sb.Append("- ");
                        temp = -temp;
                    }
                    else
                        sb.Append("+ ");
                    // temp = |-a^2*n^2 + b^2*m^2 - a^2*b^2| now.
                    sb.Append(temp.ToString(coordinateStringFormat));
                    sb.Append(" ");
                }
            }
            else
            {
                sb.Append("-");
                if (b2 != 1)
                    sb.Append(b2.ToString(coordinateStringFormat));
                sb.Append("x^2 + ");
                if (a2 != 1)
                    sb.Append(a2.ToString(coordinateStringFormat));
                sb.Append("y^2 ");
                temp = 2 * b2 * m;
                if (temp != 0)
                {
                    if (temp < 0)
                    {
                        sb.Append("- ");
                        temp = -temp;
                    }
                    else
                        sb.Append("+ ");
                    // temp = |2*b^2*m| now.
                    if (temp != 1)
                        sb.Append(temp.ToString(coordinateStringFormat));
                    sb.Append("x ");
                }
                temp = -2 * a2 * n;
                if (temp != 0)
                {
                    if (temp < 0)
                    {
                        sb.Append("- ");
                        temp = -temp;
                    }
                    else
                        sb.Append("+ ");
                    // temp = |-2*a^2*n| now.
                    if (temp != 1)
                        sb.Append(temp.ToString(coordinateStringFormat));
                    sb.Append("y ");
                }
                temp = a2 * n * n - b2 * m * m - a2 * b2;
                if (temp != 0)
                {
                    if (temp < 0)
                    {
                        sb.Append("- ");
                        temp = -temp;
                    }
                    else
                        sb.Append("+ ");
                    // temp = |a^2*n^2 - b^2*m^2 - a^2*b^2| now.
                    sb.Append(temp.ToString(coordinateStringFormat));
                    sb.Append(" ");
                }
            }
            sb.Append("= 0");

            return sb.ToString();
        }
        public string GetMidpointAndParametersInfo()
        {
            StringBuilder sb = new StringBuilder();
            double m = this.S.X;
            double n = this.S.Y;
            double a = this.LengthA;
            double b = this.LengthB;
            double e = this.LinearEccentricity;
            // Curently not informing about the type of hyperbola. (Which way it's open.)

            sb.Append("S = [");
            sb.Append(m.ToString(coordinateStringFormat));
            sb.Append("; ");
            sb.Append(n.ToString(coordinateStringFormat));
            sb.Append("]; ");

            sb.Append("a = ");
            sb.Append(a.ToString(coordinateStringFormat));
            sb.Append(", ");

            sb.Append("b = ");
            sb.Append(b.ToString(coordinateStringFormat));
            sb.Append(", ");

            sb.Append("e = ");
            sb.Append(e.ToString(coordinateStringFormat));

            return sb.ToString();
        }
        public string GetMainVertecesAndFociInfo()
        {
            StringBuilder sb = new StringBuilder();
            double m = this.S.X;
            double n = this.S.Y;
            double a = this.LengthA;
            double e = this.LinearEccentricity;
            bool openTopAndBot = this.OpenTopAndBottom;
            double temp;

            sb.Append("A = [");

            if (openTopAndBot)
                temp = m - a;
            else
                temp = m;
            sb.Append(temp.ToString(coordinateStringFormat));
            sb.Append("; ");
            if (openTopAndBot)
                temp = n;
            else
                temp = n - a;   // !!! Hyperbola equation: (y - n)^2 / a^2 - (x - m)^2 / b^2 = 0.
            sb.Append(temp.ToString(coordinateStringFormat));
            sb.Append("], ");

            sb.Append("B = [");
            if (openTopAndBot)
                temp = m + a;
            else
                temp = m;
            sb.Append(temp.ToString(coordinateStringFormat));
            sb.Append("; ");
            if (openTopAndBot)
                temp = n;
            else
                temp = n + a;   // !!! Hyperbola equation: (y - n)^2 / a^2 - (x - m)^2 / b^2 = 0.
            sb.Append(temp.ToString(coordinateStringFormat));
            sb.Append("], ");

            sb.Append("E = [");
            if (openTopAndBot)
                temp = m - e;
            else
                temp = m;
            sb.Append(temp.ToString(coordinateStringFormat));
            sb.Append("; ");
            if (openTopAndBot)
                temp = n;
            else
                temp = n - e;   // !!! Hyperbola equation: (y - n)^2 / a^2 - (x - m)^2 / b^2 = 0.
            sb.Append(temp.ToString(coordinateStringFormat));
            sb.Append("], ");

            sb.Append("F = [");
            if (openTopAndBot)
                temp = m + e;
            else
                temp = m;
            sb.Append(temp.ToString(coordinateStringFormat));
            sb.Append("; ");
            if (openTopAndBot)
                temp = n;
            else
                temp = n + e;   // !!! Hyperbola equation: (y - n)^2 / a^2 - (x - m)^2 / b^2 = 0.
            sb.Append(temp.ToString(coordinateStringFormat));
            sb.Append("]");

            return sb.ToString();
        }
        public string GetAsymptotesGeneralEquation()
        {
            StringBuilder sb = new StringBuilder();

            double nomin, denom;
            double m = this.S.X;
            double n = this.S.Y;
            double temp;
            if (this.OpenTopAndBottom)
            {
                // b / a
                nomin = this.LengthB;
                denom = this.LengthA;
            }
            else
            {
                // a / b
                nomin = this.LengthA;
                denom = this.LengthB;
            }

            if (nomin <= 0 || denom <= 0)
                throw new Exception("A logical error has occured in calculating the asymptotes' equations.");

            // First asymptote.
            sb.Append("a1: ");

            if (nomin != 1)
                sb.Append(nomin.ToString(coordinateStringFormat));
            sb.Append("x ");

            sb.Append("+ ");    // Invariant.
            if (denom != 1)
                sb.Append(denom.ToString(coordinateStringFormat));
            sb.Append("y ");

            temp = -n * denom - m * nomin;
            if (temp != 0)
            {
                if (temp < 0)
                {
                    sb.Append("- ");
                    temp = -temp;
                }
                else
                    sb.Append("+ ");
                // temp = |n*denom + m*nomin| now.
                sb.Append(temp.ToString(coordinateStringFormat));
                sb.Append(" ");
            }

            sb.Append("= 0; ");

            // Second asymptote:
            sb.Append("a2: ");

            if (nomin != 1)
                sb.Append(nomin.ToString(coordinateStringFormat));
            sb.Append("x ");

            sb.Append("- ");    // Invariant.
            if (denom != 1)
                sb.Append(denom.ToString(coordinateStringFormat));
            sb.Append("y ");

            temp = n * denom - m * nomin;
            if (temp != 0)
            {
                if (temp < 0)
                {
                    sb.Append("- ");
                    temp = -temp;
                }
                else
                    sb.Append("+ ");
                // temp = |n*denom - m*nomin| now.
                sb.Append(temp.ToString(coordinateStringFormat));
                sb.Append(" ");
            }

            sb.Append("= 0");

            return sb.ToString();
        }

        public ObjectInfo GetInfo1(Language lang)
        {
            // S = [m, n]; a, b ... semiaxes' lengths
            StringBuilder sb = new StringBuilder();
            string[] lnames = new string[4] { "m", "n", "a", "b" };
            string spec;
            string infoName = lang.InfoHyperbolaParametersAB;

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

            return new ObjectInfo(infoName, lnames, spec);
        }
        public ObjectInfo GetInfo2(Language lang)
        {
            // S = [m, n]; e ... linear eccentricity; a ... semiaxis's length
            StringBuilder sb = new StringBuilder();
            string[] lnames = new string[4] { "m", "n", "e", "a" };
            string spec;
            string infoName = lang.InfoHyperbolaParametersEA;

            sb.Append("S = [");
            sb.Append(lnames[0]);
            sb.Append(", ");
            sb.Append(lnames[1]);
            sb.Append("]; ");
            sb.Append(lnames[2]);
            sb.Append(" ... ");
            sb.Append(lang.HyperbolaLinearEccentricityName);
            sb.Append("; ");
            sb.Append(lnames[3]);
            sb.Append(" ... ");
            sb.Append(lang.HyperbolaAxisLengthName);
            spec = sb.ToString();

            return new ObjectInfo(infoName, lnames, spec);
        }
        public ObjectInfo GetInfo3(Language lang)
        {
            // S = [m, n]; e ... linear eccentricity; b ... semiaxis's length
            StringBuilder sb = new StringBuilder();
            string[] lnames = new string[4] { "m", "n", "e", "b" };
            string spec;
            string infoName = lang.InfoHyperbolaParametersEB;

            sb.Append("S = [");
            sb.Append(lnames[0]);
            sb.Append(", ");
            sb.Append(lnames[1]);
            sb.Append("]; ");
            sb.Append(lnames[2]);
            sb.Append(" ... ");
            sb.Append(lang.HyperbolaLinearEccentricityName);
            sb.Append("; ");
            sb.Append(lnames[3]);
            sb.Append(" ... ");
            sb.Append(lang.HyperbolaAxisLengthName);
            spec = sb.ToString();

            return new ObjectInfo(infoName, lnames, spec);
        }

        public List<ObjectInfo> GetAllInfoPossibilities(Language lang)
        {
            return new List<ObjectInfo> { GetInfo1(lang), GetInfo2(lang), GetInfo3(lang) };
        }

        public bool IsPointOfThisObject(MyPoint X)
        {
            double par1, par2;

            par1 = X.X - this.S.X;
            par1 *= par1;
            par1 = par1 / (this.LengthA * this.LengthA);

            par2 = X.Y - this.S.Y;
            par2 *= par2;
            par2 = par2 / (this.LengthB * this.LengthB);

            if (this.OpenTopAndBottom)
                return par1 - par2 == 1;
            else
                return par2 - par1 == 1;
        }
    }
}
