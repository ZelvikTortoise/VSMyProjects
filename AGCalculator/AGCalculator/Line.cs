using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AGCalculator
{
    abstract class Line : IObjectAG, IParametric
    {
        public MyPoint RepresentativePoint { get; protected set; }
        public Vector DirectionalVector { get; protected set; }
        public char ParameterName { get; set; } = 't';
        protected const string parameterRange = " ∈ ℝ";
        protected const string coordinateStringFormat = "f02";

        public abstract string GetParametricEquationForX();
        public abstract string GetParametricEquationForY();
        public abstract string GetParametricEquationForZ();
        public abstract string GetParameterNameAndRange();

        public abstract string GetGeneralEquation();

        public abstract ObjectInfo GetInfo1(Language lang);
        public abstract List<ObjectInfo> GetAllInfoPossibilities(Language lang);

        public abstract bool CheckParametricEquationEquivalency(MyPoint A, Vector[] vectors);

        public abstract bool IsPointOfThisObject(MyPoint X);
    }

    sealed class LineIn2D : Line, IObject2D
    {
        public Vector NormalVector { get; private set; }

        // For info purposes.
        public LineIn2D()
        {
            
        }

        // Two line's points.
        public LineIn2D(MyPoint A, MyPoint B)
        {
            Vector vect = new Vector(A, B);
            if (vect.X == 0 && vect.Y == 0)
                throw new ArgumentException("A line cannot be made out of two identical points.");
            this.RepresentativePoint = A;
            this.DirectionalVector = vect;
            this.NormalVector = Vector.GetPerpendicular2DVector(this.DirectionalVector);
        }

        // Directional vector + point.
        public LineIn2D(MyPoint X, Vector u)
        {
            if (u.X == 0 && u.Y == 0)
                throw new ArgumentException("You cannot make a line with a zero vector as its directional vector.");
            this.RepresentativePoint = X;
            this.DirectionalVector = u;
            this.NormalVector = Vector.GetPerpendicular2DVector(u);
        }

        // Normal vector + point.
        public LineIn2D(Vector n, MyPoint X)
        {
            if (n.X == 0 && n.Y == 0)
                throw new ArgumentException("You cannot make a line with a zero vector as its normal vector.");
            this.RepresentativePoint = X;
            this.DirectionalVector = Vector.GetPerpendicular2DVector(n);
            this.NormalVector = n;
        }

        // General form of equation.
        public LineIn2D(double a, double b, double c)
        {
            if (a == 0 && b == 0)
                throw new ArgumentException("This is not a line's general form of equation.");
            this.NormalVector = new Vector(a, b);
            this.DirectionalVector = Vector.GetPerpendicular2DVector(this.NormalVector);
            if (a != 0)
                this.RepresentativePoint = new MyPoint(-c / a, 0);
            else // b != 0
                this.RepresentativePoint = new MyPoint(0, -c / b);
        }

        // Slope form of the equation.
        public LineIn2D(double k, double q)
        {
            this.RepresentativePoint = new MyPoint(0, q);
            this.NormalVector = new Vector(-k, 1);
            this.DirectionalVector = Vector.GetPerpendicular2DVector(this.NormalVector);
        }

        /// <summary>
        /// Creates a line for x in a parametric equation of a line in 2D.
        /// </summary>
        /// <returns>"x = A.X +/- u.X"</returns>
        public override string GetParametricEquationForX()
        {
            if (this.DirectionalVector.X == 0)
                if (this.RepresentativePoint.X == 0)
                    return "x = 0"; // 0, 0
                else
                    return string.Concat("x = ", this.RepresentativePoint.X.ToString(coordinateStringFormat));  // A, 0
            else if (this.RepresentativePoint.X == 0)
            {
                if (DirectionalVector.X == 1)
                    return string.Concat("x = ", this.ParameterName);   // 0, t
                else if (DirectionalVector.X == -1)
                    return string.Concat("x = -", this.ParameterName);   // 0, -t
                else
                    return string.Concat("x = ", this.DirectionalVector.X.ToString(coordinateStringFormat), this.ParameterName);    // 0, ut
            }
            else
            {
                char sign;
                if (this.DirectionalVector.X >= 0)
                    sign = '+';
                else
                    sign = '-';
                if (this.DirectionalVector.X == 1 || this.DirectionalVector.X == -1)
                    return string.Concat("x = ", this.RepresentativePoint.X.ToString(coordinateStringFormat), " ", sign, " ", this.ParameterName); // A, +/-t
                else
                    return string.Concat("x = ", this.RepresentativePoint.X.ToString(coordinateStringFormat), " ", sign, " ", Math.Abs(this.DirectionalVector.X).ToString(coordinateStringFormat), this.ParameterName); // A, ut
            }
        }
        /// <summary>
        /// Creates a line for y in a parametric equation of a line in 2D.
        /// </summary>
        /// <returns>"y = A.Y +/- u.Y"</returns>
        public override string GetParametricEquationForY()
        {
            if (this.DirectionalVector.Y == 0)
                if (this.RepresentativePoint.Y == 0)
                    return "y = 0"; // 0, 0
                else
                    return string.Concat("y = ", this.RepresentativePoint.Y.ToString(coordinateStringFormat));  // A, 0
            else if (this.RepresentativePoint.Y == 0)
                if (DirectionalVector.Y == 1)
                    return string.Concat("y = ", this.ParameterName);   // 0, t
                else if (DirectionalVector.Y == -1)
                    return string.Concat("y = -", this.ParameterName);   // 0, -t
                else
                    return string.Concat("y = ", this.DirectionalVector.Y.ToString(coordinateStringFormat), this.ParameterName);    // 0, ut
            else
            {
                char sign;
                if (this.DirectionalVector.Y >= 0)
                    sign = '+';
                else
                    sign = '-';
                if (this.DirectionalVector.Y == 1 || this.DirectionalVector.Y == -1)
                    return string.Concat("y = ", this.RepresentativePoint.Y.ToString(coordinateStringFormat), " ", sign, " ", this.ParameterName); // A, +/-t
                else
                    return string.Concat("y = ", this.RepresentativePoint.Y.ToString(coordinateStringFormat), " ", sign, " ", Math.Abs(this.DirectionalVector.Y).ToString(coordinateStringFormat), this.ParameterName); // A, ut
            }            
        }
        /// <summary>
        /// Creates a line for z in a parametric equation of a line in 2D.
        /// </summary>
        /// <returns>"z = 0"</returns>
        public override string GetParametricEquationForZ()
        {
            return "z = 0";
        }
        public override string GetParameterNameAndRange()
        {
            return string.Concat(this.ParameterName, parameterRange);
        }

        /// <summary>
        /// Creates a general equation of a line in 2D.
        /// </summary>
        /// <returns>A general equation of a line: ax +/- |b|y +/- |c| = 0</returns>
        public override string GetGeneralEquation()
        {
            // We know that a != 0 OR b != 0.
            StringBuilder sb = new StringBuilder();
            double n1, n2, c;

            if (this.NormalVector.X != 0)
            {
                n1 = this.NormalVector.X;
                if (this.NormalVector.X < 0)
                {
                    sb.Append("-");
                    n1 = -n1;
                }
                // Note: We don't want to start with '+', eg. +3x - 2y + 8 = 0 isn't wanted. 
            
                // n1 = |this.NormalVector.X| now.
                if (n1 != 1)
                    sb.Append(n1.ToString(coordinateStringFormat));
                sb.Append("x ");
            }

            if (this.NormalVector.Y != 0)
            {
                n2 = this.NormalVector.Y;
                if (this.NormalVector.Y < 0)
                {
                    if (this.NormalVector.X != 0)
                        sb.Append("- ");
                    else
                        sb.Append("-");
                    n2 = -n2;
                }
                else if (this.NormalVector.X != 0)
                    sb.Append("+ ");
                // Note: We don't want to start with '+', eg. + 2y - 8 = 0 isn't wanted. 

                // n2 = |this.NormalVector.Y| now.
                if (n2 != 1)
                    sb.Append(n2.ToString(coordinateStringFormat));
                sb.Append("y ");
            }

            c = -(this.NormalVector.X * this.RepresentativePoint.X + this.NormalVector.Y * this.RepresentativePoint.Y);

            if (c != 0)
            {
                if (c < 0)
                {
                    sb.Append("- ");
                    c = -c;
                }
                else
                    sb.Append("+ ");
                // c = |c| now.
                sb.Append(c.ToString(coordinateStringFormat));
                sb.Append(" ");
            }

            sb.Append("= 0");                       
            
            return sb.ToString();
        }
        /// <summary>
        /// Creates a slope equation of a line in 2D.
        /// </summary>
        /// <returns>A slope equation of a line in 2D if b != 0. Otherwise returns null.</returns>
        public string GetLineSlopeEquation()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("y = ");

            if (this.NormalVector.Y == 0)
                return null;    // Slope equation of such line doesn't exist. (k == (+/-)Infinity)
            if (this.NormalVector.X == 0)
            {
                sb.Append(this.RepresentativePoint.Y.ToString(coordinateStringFormat)); // y = Y
                return sb.ToString();
            }
            double k = this.NormalVector.X / this.NormalVector.Y;   // We multiply by -1 after creating q.
            double q = (k * this.RepresentativePoint.X) + this.RepresentativePoint.Y;  // q = -c/b = (a*X + b*Y) / b = (a/b)*X + Y
            k = -k;
            if (k != 1 && k != -1)
                sb.Append(k.ToString(coordinateStringFormat));
            else if (k == -1)
                sb.Append("-");
            sb.Append("x ");

            if (q != 0)
            {
                if (q < 0)
                {
                    sb.Append("- ");
                    q = -q;
                }
                else
                    sb.Append("+ ");
                // q = |q| now.
                sb.Append(q.ToString(coordinateStringFormat));
            }

            return sb.ToString();   // y = kx +/- |q| (or: y = kx)
        }
        /// <summary>
        /// Creates a segmental equation of a line in 2D.
        /// </summary>
        /// <returns>Returns a segmental equation of a line in 2D if c != 0. Otherwise returns null since the equation doesn't exist.</returns>
        public string GetLineSegmentalEquation()
        {
            // We know that a != 0 OR b != 0. (ax + by + c = 0)
            double cMinus = this.RepresentativePoint.X * this.NormalVector.X + this.RepresentativePoint.Y * this.NormalVector.Y;

            // c == 0
            if (cMinus == 0)
                return null;    // Such line intersects [0; 0], therefore its segmental equation doesn't exist.

            StringBuilder sb = new StringBuilder();
            double px, py;
            if (this.NormalVector.X == 0)
            {
                sb.Append("y / ");
                py = cMinus / this.NormalVector.Y;
                sb.Append(py.ToString(coordinateStringFormat));
                sb.Append(" = 1");

                return sb.ToString();   // y / py = 1
            }
            else if (this.NormalVector.Y == 0)
            {
                sb.Append("x / ");
                px = cMinus / this.NormalVector.X;
                sb.Append(px.ToString(coordinateStringFormat));
                sb.Append(" = 1");

                return sb.ToString();   // x / px = 1
            }
            else
            {
                sb.Append("x / ");
                px = cMinus / this.NormalVector.X;
                sb.Append(px.ToString(coordinateStringFormat));
                sb.Append(" + y / ");
                py = cMinus / this.NormalVector.Y;
                sb.Append(py.ToString(coordinateStringFormat));
                sb.Append(" = 1");

                return sb.ToString();   // x / px + y / py = 1
            }
        }

        // Information for labels and textBoxes.
        public sealed override ObjectInfo GetInfo1(Language lang)
        {
            // A = [x1, y1], B = [x2, y2]
            StringBuilder sb = new StringBuilder();
            string[] lnames = new string[4] { "x1", "y1", "x2", "y2" };
            string spec;

            sb.Append("A = [");
            sb.Append(lnames[0]);
            sb.Append(", ");
            sb.Append(lnames[1]);
            sb.Append("], B = [");
            sb.Append(lnames[2]);
            sb.Append(", ");
            sb.Append(lnames[3]);
            sb.Append("]");
            spec = sb.ToString();

            return new ObjectInfo(lang.InfoTwoPoints, lnames, spec);
        }
        public ObjectInfo GetInfo2(Language lang)
        {
            // A = [x, y], u = (u1, u2)
            StringBuilder sb = new StringBuilder();
            string[] lnames = new string[4] { "x", "y", "u1", "u2" };
            string spec;

            sb.Append("A = [");
            sb.Append(lnames[0]);
            sb.Append(", ");
            sb.Append(lnames[1]);
            sb.Append("], u = (");
            sb.Append(lnames[2]);
            sb.Append(", ");
            sb.Append(lnames[3]);
            sb.Append(")");
            spec = sb.ToString();

            return new ObjectInfo(lang.InfoPointAndDirectionalVector, lnames, spec);
        }
        public ObjectInfo GetInfo3(Language lang)
        {
            // A = [x, y], n = (n1, n2)
            StringBuilder sb = new StringBuilder();
            string[] lnames = new string[4] { "x", "y", "n1", "n2" };
            string spec;

            sb.Append("A = [");
            sb.Append(lnames[0]);
            sb.Append(", ");
            sb.Append(lnames[1]);
            sb.Append("], n = (");
            sb.Append(lnames[2]);
            sb.Append(", ");
            sb.Append(lnames[3]);
            sb.Append(")");
            spec = sb.ToString();

            return new ObjectInfo(lang.InfoPointAndNormalVector, lnames, spec);
        }        
        public ObjectInfo GetInfo4(Language lang)
        {
            // ax + by + c = 0
            StringBuilder sb = new StringBuilder();
            string[] lnames = new string[3] { "a", "b", "c"};
            string spec;

            sb.Append(lnames[0]);
            sb.Append("x + ");
            sb.Append(lnames[1]);
            sb.Append("y + ");
            sb.Append(lnames[2]);
            sb.Append(" = 0");
            spec = sb.ToString();
            

            return new ObjectInfo(lang.InfoGeneralEquation, lnames, spec);
        }
        public ObjectInfo GetInfo5(Language lang)
        {
            // y = kx + q
            StringBuilder sb = new StringBuilder();
            string[] lnames = new string[2] { "k", "q" };
            string spec;

            sb.Append("y = ");
            sb.Append(lnames[0]);
            sb.Append("x + ");
            sb.Append(lnames[1]);
            spec = sb.ToString();

            return new ObjectInfo(lang.InfoSlopeEquation, lnames, spec);
        }
        public override List<ObjectInfo> GetAllInfoPossibilities(Language lang)
        {
            List<ObjectInfo> infoPossibilities = new List<ObjectInfo>()
            {
                GetInfo1(lang),
                GetInfo2(lang),
                GetInfo3(lang),
                GetInfo4(lang),
                GetInfo5(lang)
            };

            return infoPossibilities;
        }

        public override bool CheckParametricEquationEquivalency(MyPoint A, Vector[] vectors)
        {
            if (vectors.Length != 1)
                throw new ArgumentException("Class LineIn3D needs always excatly one vector.");
            if (vectors[0].X == 0 && vectors[0].Y == 0 && vectors[0].Z == 0)
                return false;

            Vector v = vectors[0];

            // Checking that the point A is a part of the line.
            if (!this.IsPointOfThisObject(A))
                return false;

            // Checking that the vector v is a multiple of this.DirectionalVector.
            // We know that this.DirectionalVector != (0, 0).
            if (this.DirectionalVector.X == 0)
            {
                if (v.X != 0 || v.Y == 0)
                    return false;
                else
                    return true;
            }
            else if (this.DirectionalVector.Y == 0)
            {
                if (v.X == 0 || v.Y != 0)
                    return false;
                else
                    return true;
            }
            else
            {
                if (v.X == 0 || v.Y == 0)
                    return false;

                double k1, k2;
                k1 = this.DirectionalVector.X / v.X;
                k2 = this.DirectionalVector.Y / v.Y;
                return k1 == k2;
            }
        }

        /// <summary>
        /// Checks if a point X lies on a line in 2D.
        /// </summary>
        /// <param name="X">The point we want to check.</param>
        /// <returns>True if X is a part of the line, false otherwise.</returns>
        public override bool IsPointOfThisObject(MyPoint X)
        {
            double par1, par2;

            if (this.DirectionalVector.X != 0 && this.DirectionalVector.Y != 0)
            {
                par1 = (X.X - this.RepresentativePoint.X) / this.DirectionalVector.X;
                par2 = (X.Y - this.RepresentativePoint.Y) / this.DirectionalVector.Y;
                return par1 == par2;
            }
            
            if (this.DirectionalVector.X == 0 && this.DirectionalVector.Y != 0)
            {
                return X.X == 0;
            }

            if (this.DirectionalVector.X != 0 && this.DirectionalVector.Y == 0)
            {
                return X.Y == 0;
            }

            if (this.DirectionalVector.X == 0 && this.DirectionalVector.Y == 0)
                throw new ArgumentException("Line cannot have zero vector as a directional vector.");
            throw new Exception("Logical error has occured in the implementation of this method.");
        }
    }

    sealed class LineIn3D : Line, IObject3D
    {
        // For info purposes.
        public LineIn3D()
        {

        }

        // Two points.
        public LineIn3D(MyPoint A, MyPoint B)
        {
            Vector vect = new Vector(A, B);
            if (vect.X == 0 && vect.Y == 0 && vect.Z == 0)
                throw new Exception("A line cannot be made out of two identical points.");
            this.RepresentativePoint = A;
            this.DirectionalVector = vect;            
        }

        // Directional vector + point.
        public LineIn3D(MyPoint X, Vector u)
        {
            if (u.X == 0 && u.Y == 0 && u.Z == 0)
                throw new Exception("You cannot make a line with a zero vector as its directional vector.");
            this.RepresentativePoint = X;
            this.DirectionalVector = u;
        }

        /// <summary>
        /// Creates a line for x in a parametric equation of a line in 3D.
        /// </summary>
        /// <returns>"x = A.X +/- u.X"</returns>
        public override string GetParametricEquationForX()
        {
            if (this.DirectionalVector.X == 0)
                if (this.RepresentativePoint.X == 0)
                    return "x = 0"; // 0, 0
                else
                    return string.Concat("x = ", this.RepresentativePoint.X.ToString(coordinateStringFormat));  // A, 0
            else if (this.RepresentativePoint.X == 0)
                return string.Concat("x = ", this.DirectionalVector.X.ToString(coordinateStringFormat), this.ParameterName);    // 0, ut
            else
            {
                char sign;
                if (this.DirectionalVector.X >= 0)
                    sign = '+';
                else
                    sign = '-';
                return string.Concat("x = ", this.RepresentativePoint.X.ToString(coordinateStringFormat), " ", sign, " ", Math.Abs(this.DirectionalVector.X).ToString(coordinateStringFormat), this.ParameterName); // A, ut
            }
        }
        /// <summary>
        /// Creates a line for y in a parametric equation of a line in 3D.
        /// </summary>
        /// <returns>"y = A.Y +/- u.Y"</returns>
        public override string GetParametricEquationForY()
        {
            if (this.DirectionalVector.Y == 0)
                if (this.RepresentativePoint.Y == 0)
                    return "y = 0"; // 0, 0
                else
                    return string.Concat("y = ", this.RepresentativePoint.Y.ToString(coordinateStringFormat));  // A, 0
            else if (this.RepresentativePoint.Y == 0)
                return string.Concat("y = ", this.DirectionalVector.Y.ToString(coordinateStringFormat), this.ParameterName);    // 0, ut
            else
            {
                char sign;
                if (this.DirectionalVector.Y >= 0)
                    sign = '+';
                else
                    sign = '-';
                return string.Concat("y = ", this.RepresentativePoint.Y.ToString(coordinateStringFormat), " ", sign, " ", Math.Abs(this.DirectionalVector.Y).ToString(coordinateStringFormat), this.ParameterName); // A, ut
            }
        }
        /// <summary>
        /// Creates a line for z in a parametric equation of a line in 3D.
        /// </summary>
        /// <returns>"z = A.Z +/- u.Z"</returns>
        public override string GetParametricEquationForZ()
        {
            if (this.DirectionalVector.Z == 0)
                if (this.RepresentativePoint.Z == 0)
                    return "z = 0"; // 0, 0
                else
                    return string.Concat("z = ", this.RepresentativePoint.Z.ToString(coordinateStringFormat));  // A, 0
            else if (this.RepresentativePoint.Z == 0)
                return string.Concat("z = ", this.DirectionalVector.Z.ToString(coordinateStringFormat), this.ParameterName);    // 0, ut
            else
            {
                char sign;
                if (this.DirectionalVector.Z >= 0)
                    sign = '+';
                else
                    sign = '-';
                return string.Concat("z = ", this.RepresentativePoint.Z.ToString(coordinateStringFormat), " ", sign, " ", Math.Abs(this.DirectionalVector.Z).ToString(coordinateStringFormat), this.ParameterName); // A, ut
            }
        }
        public override string GetParameterNameAndRange()
        {
            return string.Concat(this.ParameterName, parameterRange);
        }

        /// <summary>
        /// General equation of a line in a 3D space doesn't exist.
        /// </summary>
        /// <returns>Throws an Exception.</returns>
        public override string GetGeneralEquation()
        {
            throw new Exception("A line is not a hyperplane in 3D space.");
        }

        public sealed override ObjectInfo GetInfo1(Language lang)
        {
            return null;
        }   // (3D: TODO)
        public override List<ObjectInfo> GetAllInfoPossibilities(Language lang)
        {
            return null;
            /*List<ObjectInfo> infoPossibilities = new List<ObjectInfo>();
            LineIn3D abstractLine = new LineIn3D();
            infoPossibilities.Add(abstractLine.GetInfo1(lang));
            //infoPossibilities.Add(abstractLine.GetInfo2(lang));
            //infoPossibilities.Add(abstractLine.GetInfo3(lang));
            return infoPossibilities;*/
        }   // (3D: TODO)

        public override bool CheckParametricEquationEquivalency(MyPoint A, Vector[] vectors)
        {
            if (vectors.Length != 1)
                throw new ArgumentException("Class LineIn3D needs always excatly one vector.");
            if (vectors[0].X == 0 && vectors[0].Y == 0 && vectors[0].Z == 0)
                return false;

            throw new NotImplementedException(Program.language.ExceptionNotImplementedText);
        }

        /// <summary>
        /// Checks if a point X lies on a line in 3D.
        /// </summary>
        /// <param name="X">The point we want to check.</param>
        /// <returns>True if X is a part of the line, false otherwise.</returns>
        public override bool IsPointOfThisObject(MyPoint X)
        {
            double par1, par2, par3;

            if (this.DirectionalVector.X != 0)
                par1 = (X.X - this.RepresentativePoint.X) / this.DirectionalVector.X;
            else
                par1 = X.X - this.RepresentativePoint.X;

            if (this.DirectionalVector.Y != 0)
                par2 = (X.Y - this.RepresentativePoint.Y) / this.DirectionalVector.Y;
            else
                par2 = X.Y - this.RepresentativePoint.Y;

            if (this.DirectionalVector.Z != 0)
                par3 = (X.Z - this.RepresentativePoint.Z) / this.DirectionalVector.Z;
            else
                par3 = X.Z - this.RepresentativePoint.Z;

            return (par1 == par2) && (par2 == par3);
        }
    }
}
