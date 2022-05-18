using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGCalculator
{
    struct MyPoint
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }

        /// <summary>
        /// Creates a point in 2D. (z == 0)
        /// </summary>
        /// <param name="x">Point's 'x' coordinate</param>
        /// <param name="y">Point's 'y' coordinate</param>
        public MyPoint(double x, double y)
        {
            this.X = x;
            this.Y = y;
            this.Z = 0;
        }

        /// <summary>
        /// Creates a point in 3D.
        /// </summary>
        /// <param name="x">Point's 'x' coordinate</param>
        /// <param name="y">Point's 'y' coordinate</param>
        /// <param name="z">Point's 'z' coordinate</param>
        public MyPoint(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }
}
