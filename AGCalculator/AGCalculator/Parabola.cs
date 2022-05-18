using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AGCalculator
{
    sealed class Parabola : IObject2D
    {
        public enum Open { Up, Down, Right, Left}

        public MyPoint V { get; private set; }
        public Open Direction { get; private set; }
        public double Parameter { get; private set; }   // Parameter = p/2 for P: (x-m)^2 = 2p(y-n).
        private const string coordinateStringFormat = "f02";

        public Parabola()
        {

        }   // For info purposes.
        /// <summary>
        /// Creates a parabola with a directrix either parallel to X-axis or to Y-axis.
        /// </summary>
        /// <param name="V">Parabola's vertex</param>
        /// <param name="F">Parabola's focus</param>
        public Parabola(MyPoint V, MyPoint F)
        {
            if (V.X == F.X && V.Y == F.Y)
                throw new ArgumentException("Parabola's vertex and focus cannot be identical.");

            this.V = V;

            if (V.Y == F.Y)
                if (V.X < F.X)
                {
                    this.Direction = Open.Right;
                    this.Parameter = F.X - V.X;
                }
                else
                {
                    this.Direction = Open.Left;
                    this.Parameter = V.X - F.X;
                }

            else if (V.X == F.X)
                if (V.Y < F.Y)
                {
                    this.Direction = Open.Up;
                    this.Parameter = F.Y - V.Y;
                }
                else
                {
                    this.Direction = Open.Down;
                    this.Parameter = V.Y - F.Y;
                }
            else
                throw new ArgumentException("Only parabolas with directrix parallel to X-axis or to Y-axis are allowed. Other types aren't supported.");

            if (this.Parameter <= 0)
                throw new Exception("A logical error has occured in Parabola's parametric constructor. Parabola's parameter equals " + this.Parameter.ToString(coordinateStringFormat) + " which is less or equal to zero.");
        }


        public string GetParabolaEquation()
        {
            StringBuilder sb = new StringBuilder();
            Open direction = this.Direction;
            double m = this.V.X;
            double n = this.V.Y;
            double pp = 4 * this.Parameter;    // We know that this.Parameter = p / 2 = |VF|, pp = 2*p.
            double temp, temp2;

            if (direction == Open.Up || direction == Open.Down)
            {
                if (m == 0)
                    sb.Append("x^2 = ");
                else
                {
                    temp = m;
                    sb.Append("(x ");
                    if (temp < 0)
                    {
                        sb.Append("+ ");
                        temp = -temp;
                    }
                    else
                        sb.Append("- ");
                    // temp = |m| now.
                    sb.Append(temp.ToString(coordinateStringFormat));
                    sb.Append(")^2 = ");
                }

                temp2 = pp;
                if (direction == Open.Down)
                    temp2 = -temp2;
                // this.Parameter > 0 always.
                if (temp2 == -1)
                    sb.Append("-");
                else if (temp2 != 1)
                    sb.Append(temp2.ToString(coordinateStringFormat));
                if (n != 0)
                {
                    if (temp2 == -1)
                        sb.Append("(");
                    else if (temp2 != 1)
                        sb.Append("•(");
                    temp = n;
                    sb.Append("y ");
                    if (temp < 0)
                    {
                        sb.Append("+ ");
                        temp = -temp;
                    }
                    else
                        sb.Append("- ");
                    // temp = |n| now.
                    sb.Append(temp.ToString(coordinateStringFormat));
                    if (temp2 != 1)
                        sb.Append(")");
                }
                else
                    sb.Append("y"); // Without '•'.
            }
            else if (direction == Open.Right || direction == Open.Left)
            {
                if (n == 0)
                    sb.Append("y^2 = ");
                else
                {
                    temp = n;
                    sb.Append("(y ");
                    if (temp < 0)
                    {
                        sb.Append("+ ");
                        temp = -temp;
                    }
                    else
                        sb.Append("- ");
                    // temp = |n| now.
                    sb.Append(temp.ToString(coordinateStringFormat));
                    sb.Append(")^2 = ");
                }

                temp2 = pp;
                if (direction == Open.Left)
                    temp2 = -temp2;
                // this.Parameter > 0 always.
                if(temp2 == -1)
                    sb.Append("-");
                else if (temp2 != 1)
                    sb.Append(temp2.ToString(coordinateStringFormat));
                if (m != 0)
                {
                    if (temp2 == -1)
                        sb.Append("(");
                    else if (temp2 != 1)
                        sb.Append("•(");
                    temp = m;
                    sb.Append("x ");
                    if (temp < 0)
                    {
                        sb.Append("+ ");
                        temp = -temp;
                    }
                    else
                        sb.Append("- ");
                    // temp = |m| now.
                    sb.Append(temp.ToString(coordinateStringFormat));
                    if (temp2 != 1)
                        sb.Append(")");
                }
                else
                    sb.Append("x"); // Without '•'.
            }
            else
                throw new Exception("Unhanled parabola's opening direction in method " + nameof(GetParabolaEquation) + "(...).");

            return sb.ToString();
        }
        public string GetGeneralEquation()
        {
            StringBuilder sb = new StringBuilder();
            Open direction = this.Direction;
            double m = this.V.X;
            double n = this.V.Y;
            double pp = 4 * this.Parameter; // We know that this.Parameter = p/2 = |VF|, we want pp = 2*p.
            double temp;

            switch (this.Direction)
            {
                case Open.Up:
                    sb.Append("x^2 ");

                    temp = -2 * m;
                    if (temp != 0)
                    {
                        if (temp < 0)
                        {
                            sb.Append("- ");
                            temp = -temp;
                        }
                        else
                            sb.Append("+ ");
                        // temp = |-2m| now.
                        if (temp != 1)
                            sb.Append(temp.ToString(coordinateStringFormat));
                        sb.Append("x ");
                    }                        

                    temp = -pp;
                    if (temp != 0)
                    {
                        if (temp < 0)
                        {
                            sb.Append("- ");
                            temp = -temp;
                        }
                        else
                            sb.Append("+ ");
                        // temp = |-2p| now.
                        if (temp != 1)
                            sb.Append(temp.ToString(coordinateStringFormat));
                        sb.Append("y ");
                    }                   

                    temp = m * m + pp * n;
                    if (temp != 0)
                    {
                        if (temp < 0)
                        {
                            sb.Append("- ");
                            temp = -temp;
                        }
                        else
                            sb.Append("+ ");
                        // temp = |m*m + 2p*n| now.
                        sb.Append(temp.ToString(coordinateStringFormat));
                        sb.Append(" ");
                    }
                    break;


                case Open.Down:
                    sb.Append("x^2 ");
                    
                    temp = -2 * m;
                    if (temp != 0)
                    {
                        if (temp < 0)
                        {
                            sb.Append("- ");
                            temp = -temp;
                        }
                        else
                            sb.Append("+ ");
                        // temp = |-2m| now.
                        if (temp != 1)
                            sb.Append(temp.ToString(coordinateStringFormat));
                        sb.Append("x ");
                    }                    

                    temp = pp;
                    if (temp != 0)
                    {
                        if (temp < 0)
                        {
                            sb.Append("- ");
                            temp = -temp;
                        }
                        else
                            sb.Append("+ ");
                        // temp = |2p| now.
                        if (temp != 1)
                            sb.Append(temp.ToString(coordinateStringFormat));
                        sb.Append("y ");
                    }                        

                    temp = m * m - pp * n;
                    if (temp != 0)
                    {
                        if (temp < 0)
                        {
                            sb.Append("- ");
                            temp = -temp;
                        }
                        else
                            sb.Append("+ ");
                        // temp = |m*m - 2p*n| now.
                        sb.Append(temp.ToString(coordinateStringFormat));
                        sb.Append(" ");
                    }                        
                    break;


                case Open.Right:
                    sb.Append("y^2 ");
                    
                    temp = -pp;
                    if (temp != 0)
                    {
                        if (temp < 0)
                        {
                            sb.Append("- ");
                            temp = -temp;
                        }
                        else
                            sb.Append("+ ");
                        // temp = |-2p| now.
                        if (temp != 1)
                            sb.Append(temp.ToString(coordinateStringFormat));
                        sb.Append("x ");
                    }                    

                    temp = -2*n;
                    if (temp != 0)
                    {
                        if (temp < 0)
                        {
                            sb.Append("- ");
                            temp = -temp;
                        }
                        else
                            sb.Append("+ ");
                        // temp = |-2n| now.
                        if (temp != 1)
                            sb.Append(temp.ToString(coordinateStringFormat));
                        sb.Append("y ");
                    }                    

                    temp = n * n + pp * m;
                    if (temp != 0)
                    {
                        if (temp < 0)
                        {
                            sb.Append("- ");
                            temp = -temp;
                        }
                        else
                            sb.Append("+ ");
                        // temp = |n*n + 2p*m| now.
                        sb.Append(temp.ToString(coordinateStringFormat));
                        sb.Append(" ");
                    }                    
                    break;


                case Open.Left:
                    sb.Append("y^2 ");

                    temp = pp;
                    if (temp != 0)
                    {
                        if (temp < 0)
                        {
                            sb.Append("- ");
                            temp = -temp;
                        }
                        else
                            sb.Append("+ ");
                        // temp = |2p| now.
                        if (temp != 1)
                            sb.Append(temp.ToString(coordinateStringFormat));
                        sb.Append("x ");
                    }                   

                    temp = -2 * n;
                    if (temp != 0)
                    {
                        if (temp < 0)
                        {
                            sb.Append("- ");
                            temp = -temp;
                        }
                        else
                            sb.Append("+ ");
                        // temp = |-2n| now.
                        if (temp != 1)
                            sb.Append(temp.ToString(coordinateStringFormat));
                        sb.Append("y ");
                    }                    

                    temp = n * n - pp * m;
                    if (temp != 0)
                    {
                        if (temp < 0)
                        {
                            sb.Append("- ");
                            temp = -temp;
                        }
                        else
                            sb.Append("+ ");
                        // temp = |n*n - 2p*m| now.
                        sb.Append(temp.ToString(coordinateStringFormat));
                        sb.Append(" ");
                    }                    
                    break;

                default:
                    throw new Exception("Unhanled parabola's opening direction in method " + nameof(GetGeneralEquation) + "(...).");
            }

            sb.Append("= 0");

            return sb.ToString();
        }
        public string GetVertexAndFocusInfo()
        {
            StringBuilder sb = new StringBuilder();
            double m = this.V.X;
            double n = this.V.Y;
            double Fx, Fy;
            double VF = this.Parameter; // We know that this.Parameter = |VF|.

            switch (this.Direction)
            {
                case Open.Up:
                    Fx = m;
                    Fy = n + VF;
                    break;
                case Open.Down:
                    Fx = m;
                    Fy = n - VF;
                    break;
                case Open.Right:
                    Fx = m + VF;
                    Fy = n;
                    break;
                case Open.Left:
                    Fx = m - VF;
                    Fy = n;
                    break;
                default:
                    throw new Exception("Unhanled parabola's opening direction in method " + nameof(GetVertexAndFocusInfo) + "(...).");
            }

            sb.Append("V = [");
            sb.Append(m.ToString(coordinateStringFormat));
            sb.Append(", ");
            sb.Append(n.ToString(coordinateStringFormat));
            sb.Append("], F = [");
            sb.Append(Fx.ToString(coordinateStringFormat));
            sb.Append(", ");
            sb.Append(Fy.ToString(coordinateStringFormat));
            sb.Append("]");
            
            return sb.ToString();
        }
        public string GetDirectrixEquation()
        {
            StringBuilder sb = new StringBuilder();
            double m = this.V.X;
            double n = this.V.Y;
            double Vd = this.Parameter; // We know that this.Parameter = |Vd|.
            double d;

            sb.Append("d: ");
            switch (this.Direction)
            {
                case Open.Up:
                    sb.Append("y = ");
                    d = n - Vd;
                    sb.Append(d.ToString(coordinateStringFormat));
                    break;
                case Open.Down:
                    sb.Append("y = ");
                    d = n + Vd;
                    sb.Append(d.ToString(coordinateStringFormat));
                    break;
                case Open.Right:
                    sb.Append("x = ");
                    d = m - Vd;
                    sb.Append(d.ToString(coordinateStringFormat));
                    break;
                case Open.Left:
                    sb.Append("x = ");
                    d = m + Vd;
                    sb.Append(d.ToString(coordinateStringFormat));
                    break;
                default:
                    throw new Exception("Unhanled parabola's opening direction in method " + nameof(GetDirectrixEquation) + "(...).");
            }

            return sb.ToString();
        }
        public string GetOpeningDirection()
        {
            StringBuilder sb = new StringBuilder();
            Language lang = Program.language;

            string parabolaText = lang.TypeOf2DObjectParabola;
            StringBuilder sbTemp = new StringBuilder();
            string temp = (parabolaText[0].ToString()).ToUpper();
            sbTemp.Append(temp);
            for (int i = 1; i < parabolaText.Length; i++)
                sbTemp.Append(parabolaText[i]);
            sb.Append(sbTemp.ToString());

            sb.Append(" ");
            sb.Append(lang.ParabolaIsOpenToTheText);
            sb.Append(" ");
            switch (this.Direction)
            {                
                case Open.Up:
                    sb.Append(lang.ParabolaTopText);
                    break;
                case Open.Down:
                    sb.Append(lang.ParabolaBottomText);
                    break;
                case Open.Right:
                    sb.Append(lang.ParabolaRightText);
                    break;
                case Open.Left:
                    sb.Append(lang.ParabolaLeftText);
                    break;
                default:
                    throw new Exception("Unhanled parabola's opening direction in method " + nameof(GetOpeningDirection) + "(...).");
            }
            sb.Append(".");

            return sb.ToString();
        }

        public ObjectInfo GetInfo1(Language lang)
        {
            // V = [Vx, Vy], F = [Fx, Fy]
            StringBuilder sb = new StringBuilder();
            string[] lnames = new string[4] { "Vx", "Vy", "Fx", "Fy" };
            string spec;
            string infoName = lang.InfoParabolaVertexAndFocus;

            sb.Append("V = [");
            sb.Append(lnames[0]);
            sb.Append(", ");
            sb.Append(lnames[1]);
            sb.Append("], ");
            sb.Append("F = [");
            sb.Append(lnames[2]);
            sb.Append(", ");
            sb.Append(lnames[3]);
            sb.Append("]");
            spec = sb.ToString();

            return new ObjectInfo(infoName, lnames, spec);
        }    
        public List<ObjectInfo> GetAllInfoPossibilities(Language lang)
        {
            return new List<ObjectInfo> { GetInfo1(lang) };
        }

        public bool IsPointOfThisObject(MyPoint X)
        {
            double p = 2 * this.Parameter;  // We know that this.Parameter = p / 2 = |VF|.
            double par1, par2;
            double Vx = this.V.X;
            double Vy = this.V.Y;

            switch (this.Direction)
            {
                case Open.Up:
                    par1 = X.X - Vx;
                    par1 *= par1;
                    par2 = X.Y - Vy;
                    par2 = 2 * p * par2;
                    return par1 == par2;
                case Open.Down:
                    par1 = X.X - Vx;
                    par1 *= par1;
                    par2 = X.Y - Vy;
                    par2 = -2 * p * par2;
                    return par1 == par2;
                case Open.Right:
                    par1 = X.Y - Vy;
                    par1 *= par1;
                    par2 = X.X - Vx;
                    par2 = 2 * p * par2;
                    return par1 == par2;
                case Open.Left:
                    par1 = X.Y - Vy;
                    par1 *= par1;
                    par2 = X.X - Vx;
                    par2 = -2 * p * par2;
                    return par1 == par2;
                default:
                    throw new Exception("Unhanled parabola's opening direction in method " + nameof(IsPointOfThisObject) + "(...).");
            }
        }
    }
}
