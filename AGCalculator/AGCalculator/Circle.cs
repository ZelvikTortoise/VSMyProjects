using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AGCalculator
{
    sealed class Circle : IObject2D
    {
        public MyPoint S { get; private set; }
        public double Radius { get; private set; }

        private const string coordinateStringFormat = "f02";

        public Circle()
        {
            // For Info purposes.
        }   // For Info purposes.
        public Circle(MyPoint S, double radius)
        {
            if (radius <= 0)
                throw new ArgumentException("Circle cannot have a negative radius or a radius equal to zero.");
            this.S = S;
            this.Radius = radius;
        }
        public Circle(double a, double b, double c)
        {
            this.S = new MyPoint(-a / 2, -b / 2);
            double check = ((a * a + b * b) / 4) - c;
            if (check <= 0)
                throw new ArgumentException("Such circle cannot exist.");
            this.Radius = Math.Sqrt(check);
        }

        public string GetCircleEquation()
        {
            // (x - S.X)^2 + (y - S.Y)^2 = Radius^2
            StringBuilder sb = new StringBuilder();
            double x, y;

            if (S.X == 0)
                sb.Append("x^2 + ");
            else
            {
                x = S.X;
                sb.Append("(x ");
                if (x < 0)                   
                {
                    sb.Append("+ ");
                    x = -x;
                }
                else
                    sb.Append("- ");
                // x = |S.X| now.
                sb.Append(x.ToString(coordinateStringFormat));
                sb.Append(")^2 + ");
            }

            if (S.Y == 0)
                sb.Append("y^2 = ");
            else
            {
                y = S.Y;
                sb.Append("(y ");
                if (y < 0)                    
                {
                    sb.Append("+ ");
                    y = -y;
                }
                else
                    sb.Append("- ");
                // y = |S.Y| now.
                sb.Append(y.ToString(coordinateStringFormat));
                sb.Append(")^2 = ");
            }

            sb.Append((Radius * Radius).ToString(coordinateStringFormat));
            return sb.ToString();
        }
        public string GetGeneralEquation()
        {
            // x^2 + y^2 + ax + by + c = 0
            double a = -2 * S.X;
            double b = -2 * S.Y;
            double c = S.X * S.X + S.Y * S.Y - Radius * Radius;

            StringBuilder sb = new StringBuilder();
            sb.Append("x^2 + y^2 ");

            if (a != 0)
            {
                if (a < 0)
                {
                    sb.Append("- ");
                    a = -a;
                }
                else
                    sb.Append("+ ");
                // Now a = |a|.
                if (a != 1)
                    sb.Append(a.ToString(coordinateStringFormat));
                sb.Append("x ");                    
            }

            if (b != 0)
            {
                if (b < 0)
                {
                    sb.Append("- ");
                    b = -b;
                }
                else
                    sb.Append("+ ");
                // Now b = |b|.
                if (b != 1)
                    sb.Append(b.ToString(coordinateStringFormat));
                sb.Append("y ");
            }

            if (c != 0)
            {
                if (c < 0)
                {
                    sb.Append("- ");
                    c = -c;
                }
                else
                    sb.Append("+ ");
                // Now c = |c|.
                sb.Append(c.ToString(coordinateStringFormat));
                sb.Append(" ");
            }

            sb.Append("= 0");

            return sb.ToString();
        }
        public string GetMidpointInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("S = [");
            sb.Append(this.S.X.ToString(coordinateStringFormat));
            sb.Append(", ");
            sb.Append(this.S.Y.ToString(coordinateStringFormat));
            sb.Append("]");

            return sb.ToString();
        }
        public string GetRadiusInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("r = ");
            sb.Append(this.Radius.ToString(coordinateStringFormat));

            return sb.ToString();
        }

        public ObjectInfo GetInfo1(Language lang)
        {
            // S = [x, y], r ... radius/poloměr
            StringBuilder sb = new StringBuilder();
            string[] lnames = new string[3] { "x", "y", "r" };
            string spec;

            sb.Append("S = [");
            sb.Append(lnames[0]);
            sb.Append(", ");
            sb.Append(lnames[1]);
            sb.Append("], ");
            sb.Append(lnames[2]);
            sb.Append(" ... ");
            sb.Append(lang.RadiusName);
            spec = sb.ToString();

            return new ObjectInfo(lang.InfoMidpointAndRadius, lnames, spec);
        }
        public ObjectInfo GetInfo2(Language lang)
        {
            // x^2 + y^2 + ax + by + c = 0
            StringBuilder sb = new StringBuilder();
            string[] lnames = new string[3] { "a", "b", "c" };
            string spec;

            sb.Append("x^2 + y^2 + ");
            sb.Append(lnames[0]);
            sb.Append("x + ");
            sb.Append(lnames[1]);
            sb.Append("y + ");
            sb.Append(lnames[2]);
            sb.Append(" = 0");
            spec = sb.ToString();

            return new ObjectInfo(lang.InfoGeneralEquation, lnames, spec);
        }
        public List<ObjectInfo> GetAllInfoPossibilities(Language lang)
        {
            return new List<ObjectInfo> { GetInfo1(lang), GetInfo2(lang) };
        }

        public bool IsPointOfThisObject(MyPoint X)
        {
            double par1, par2;
            par1 = X.X - S.X;
            par1 = par1 * par1;
            par2 = X.Y - S.Y;
            par2 = par2 * par2;

            return par1 + par2 == Radius*Radius;
        }
    }
}
