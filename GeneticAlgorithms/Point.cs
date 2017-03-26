using System;
using System.Collections.Generic;
using System.Linq;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms
{
    /// <summary>
    /// An immutable pair of x-y coordinates.
    /// </summary>
    public class Point
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Returns the distance between A and B.
        /// </summary>
        public static float Distance(Point a, Point b)
        {
            var xSquared = (b.X - a.X) * (b.X - a.X);
            var ySquared = (b.Y - a.Y) * (b.Y - a.Y);

            return (float)Math.Sqrt(xSquared + ySquared);
        }
    }
}
