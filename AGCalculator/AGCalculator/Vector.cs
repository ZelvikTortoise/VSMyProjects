using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGCalculator
{
    struct Vector
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }

        /// <summary>
        /// Creates a vector in 2D perpendicular to the parameter.             .
        /// Important: Call this method for 2D vectors only!
        /// </summary>
        /// <param name="vector">Vector in 2D.</param>
        /// <returns>Returns a vector in 2D perpendicular to the parameter. Note: If the parameter's 'z' coordinate isn't 0, the method throws an exception.</returns>
        public static Vector GetPerpendicular2DVector(Vector vector)
        {
            if (vector.Z != 0)
                throw new Exception("Call this method on vectors in 2D only! This vector's 'z' coordinate isn't equal to 0, therefore the vector is in 3D.");

            if (vector.X == 0 & vector.Y == 0)
                throw new Exception("Calling this method on the zero vector is forbidden since every vector is perpendicular to it.");

            return new Vector(-vector.Y, vector.X); 
        }

        /// <summary>
        /// Creates a vector opposite to the parameter.
        /// </summary>
        /// <param name="vector">The vector that we want the opposite of.</param>
        /// <returns>Returns a vector opposite to the parameter.</returns>
        public static Vector GetOppositeVector(Vector vector)
        {
            return new Vector(-vector.X, -vector.Y, -vector.Z);
        }

        /// <summary>
        /// Creates a vector AB = B - A.
        /// </summary>
        /// <param name="A">Starting point.</param>
        /// <param name="B">Ending point.</param>
        public Vector(MyPoint A, MyPoint B)
        {
            this.X = B.X - A.X;
            this.Y = B.Y - A.Y;
            this.Z = B.Z - A.Z;
        }

        /// <summary>
        /// Creates a vector in 2D. (z == 0)
        /// </summary>
        /// <param name="x">Vector's 'x' coordinate</param>
        /// <param name="y">Vector's 'y' coordinate</param>
        public Vector(double x, double y)
        {
            this.X = x;
            this.Y = y;
            this.Z = 0;
        }

        /// <summary>
        /// Creates a vector in 3D.
        /// </summary>
        /// <param name="x">Vector's 'x' coordinate</param>
        /// <param name="y">Vector's 'y' coordinate</param>
        /// <param name="z">Vector's 'z' coordinate</param>
        public Vector(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }
}
