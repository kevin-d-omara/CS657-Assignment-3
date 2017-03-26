using System;
using System.Collections.Generic;
using System.Linq;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms
{
    /// <summary>
    /// A sequential list of points. Note: start and end points may be different.
    /// </summary>
    public class Route
    {
        /// <summary>
        /// Sequential list of points (start and end may be different).
        /// </summary>
        public Point[] Points { get; private set; }

        /// <summary>
        /// Number of legs in the route.
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// Total distance of the route.
        /// </summary>
        public float TotalDistance { get; private set; }

        /// <summary>
        /// Create a route from the pre-defined set of points.
        /// </summary>
        /// <param name="points">Sequential list of points (start and end may be different).</param>
        public Route(Point[] points)
        {
            Points = points;
            Length = points.Length <= 1 ? 0 : points.Length - 1;
            TotalDistance = GetTotalDistance();
        }

        /// <summary>
        /// Create a route beginning and ending with the same point.
        /// </summary>
        public Route(Point home, Point[] points)
        {
            var numberOfLegs = points.Length + 1;
            var numberOfPoints = numberOfLegs + 1;

            Length = numberOfLegs;
            Points = new Point[numberOfPoints];

            Points[0] = home;
            Points[numberOfLegs] = home;

            for (int i = 1; i < numberOfLegs; ++i)
            {
                Points[i] = points[i - 1];
            }

            TotalDistance = GetTotalDistance();
        }

        /// <summary>
        /// Return the sum of distances between consecutive points along the route.
        /// </summary>
        private float GetTotalDistance()
        {
            var totalDistance = 0f;
            for (int i = 0; i < Length; ++i)
            {
                totalDistance += Point.Distance(Points[i], Points[i + 1]);
            }

            return totalDistance;
        }
    }
}
