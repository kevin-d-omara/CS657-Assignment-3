using System;
using System.Collections.Generic;
using System.Linq;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms
{
    /// <summary>
    /// A possible solution (route) to the travelling salesman problem.
    /// </summary>
    public class RouteChromosome : ChromosomeBase
    {
        /// <summary>
        /// Negative of the total route distance (i.e. 0 == shortest route).
        /// </summary>
        public override float Fitness { get; protected set; }

        /// <summary>
        /// The start and end point of the route.
        /// </summary>
        public Point Home { get; private set; }

        /// <summary>
        /// Sequential path of points including start and end (Home).
        /// </summary>
        public Route Route { get; private set; }

        /// <summary>
        /// Sequential points along the route discluding start and end.
        /// </summary>
        private Point[] _points;

        /// <summary>
        /// Create a chromosome with the route defined by home and path.
        /// </summary>
        /// <param name="home">Start and end point of the route.</param>
        /// <param name="path">Sequential path of points in the route, discluding start/end.</param>
        public RouteChromosome(Point home, Point[] path)
        {
            Home = home;
            _points = path;
            UpdateRoute();

            // Convert Points -> Genes
            Length = path.Length;
            _genes = new Gene[Length];
            for (int i = 0; i < Length; ++i)
            {
                _genes[i] = new Gene(path[i]);
            }
        }

        /// <summary>
        /// Return a shallow copy of this chromosome.
        /// </summary>
        public override IChromosome Clone()
        {
            var points = (Point[])_points.Clone();
            return new RouteChromosome(Home, points);
        }

        public override void ReplaceGene(int index, Gene gene)
        {
            base.ReplaceGene(index, gene);

            _points[index] = (Point)(gene.value);
            UpdateRoute();
        }

        /// <summary>
        /// Replaces the genes starting at index. If there are more genes provided than there are
        /// genes to replace, the excess genes will be appended to the chromosome (i.e. the
        /// chromosome's length will be increased).
        /// </summary>
        public override void ReplaceGenes(int startIndex, Gene[] genes)
        {
            base.ReplaceGenes(startIndex, genes);

            // Unwrap Genes -> Points
            var points = new Point[genes.Length];
            for (int i = 0; i < genes.Length; ++i)
            {
                points[i] = (Point)(genes[i].value);
            }

            ReplaceElements<Point>(startIndex, _points, points);
            UpdateRoute();
        }

        /// <summary>
        /// Update the Route and Fitness based on the current _points and Home.
        /// </summary>
        public void UpdateRoute()
        {
            Route = new Route(Home, _points);
            Fitness = -Route.TotalDistance;
        }
    }
}
