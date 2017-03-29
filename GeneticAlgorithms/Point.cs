using System;
using System.Collections.Generic;
using System.Linq;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms
{
    /// <summary>
    /// An immutable pair of x-y coordinates.
    /// </summary>
    public class Point : IEquatable<Point>
    {
        public readonly int x;
        public readonly int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Return the distance between A and B.
        /// </summary>
        public static float Distance(Point a, Point b)
        {
            var xSquared = (b.x - a.x) * (b.x - a.x);
            var ySquared = (b.y - a.y) * (b.y - a.y);

            return (float)Math.Sqrt(xSquared + ySquared);
        }

        // Overriden operators (make comparison by value, not reference) ---------------------------

        /// <summary>
        /// Return true if the x and y components of a and b are equal.
        /// </summary>
        public static bool operator ==(Point a, Point b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b)) { return true; }

            // If one is null, bot not both, return false.
            if ((object)a == null || (object)b == null) { return false; }

            // Return true if x and y match.
            return a.x == b.x && a.y == b.y;
        }

        /// <summary>
        /// Return true if the x or y components of a and b are unequal.
        /// </summary>
        public static bool operator !=(Point a, Point b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b)) { return false; }

            // If one is null, bot not both, return false.
            if ((object)a == null || (object)b == null) { return true; }

            // Return true if x and y don't match.
            return !(a.x == b.x && a.y == b.y);
        }

        /// <summary>
        /// Return true if the x and y components of a and b are equal.
        /// </summary>
        public bool Equals(Point other)
        {
            return this == other;
        }

        /// <summary>
        /// Return true if the other object is a Point AND the x and y components of this and obj 
        /// are equal.
        /// </summary>
        public override bool Equals(object obj)
        {
            var otherPoint = obj as Point;

            if (obj == null || otherPoint == null)
            {
                return false;
            }

            return this == otherPoint;
        }

        /// <summary>
        /// Calculated by value, so separate instances with the same x & y will have the same hash.
        /// </summary>
        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() << 2;
        }
    }
}
