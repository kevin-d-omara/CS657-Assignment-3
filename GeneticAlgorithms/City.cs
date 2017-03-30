using System;
using System.Collections.Generic;
using System.Linq;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms
{
    /// <summary>
    /// A collection of homes and a warehouse.
    /// </summary>
    public class City
    {
        /// <summary>
        /// Location of each home.
        /// </summary>
        public Point[] Homes { get; private set; }

        /// <summary>
        /// Location of the warehouse.
        /// </summary>
        public Point Warehouse { get; private set; }

        /// <summary>
        /// Create a new city with the given homes and warehouse locations.
        /// </summary>
        public City(IList<Point> homes, Point warehouse)
        {
            Homes = homes.ToArray();
            Warehouse = warehouse;
        }

        /// <summary>
        /// Return the Homes in a randomized order.
        /// </summary>
        public Point[] GetHomesInRandomOrder()
        {
            return Point.GetShuffledClone(Homes);
        }
    }
}
