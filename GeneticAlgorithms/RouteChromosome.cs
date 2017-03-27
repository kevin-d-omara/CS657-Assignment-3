using System;
using System.Collections.Generic;
using System.Linq;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms
{
    /// <summary>
    /// A possible solution (route) to the travelling salesman problem.
    /// </summary>
    public class RouteChromosome : IChromosome
    {
        /// <summary>
        /// Number of points in the route (discluding start/end).
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// Negative of the total route distance (i.e. 0 == shortest route).
        /// </summary>
        public float Fitness { get; private set; }

        /// <summary>
        /// Route (sequential path of points) discluding start and end.
        /// </summary>
        public Point[] Genes
        {
            get
            {
                return _genes;
            }
            private set
            {
                _genes = value;
                Length = _genes.Length;
                Route = new Route(Home, _genes);
                Fitness = -Route.TotalDistance;
            }
        }
        private Point[] _genes;

        /// <summary>
        /// The start and end point of the route.
        /// </summary>
        public Point Home { get; private set; }

        /// <summary>
        /// Sequential path of points including start and end (Home).
        /// </summary>
        public Route Route { get; private set; }

        /// <summary>
        /// A chromosome with the route defined by home and path.
        /// </summary>
        /// <param name="home">Start and end point of the route.</param>
        /// <param name="path">Sequential path of points in the route.</param>
        public RouteChromosome(Point home, Point[] path)
        {
            Home = home;
            Genes = path;
        }
    }
}
