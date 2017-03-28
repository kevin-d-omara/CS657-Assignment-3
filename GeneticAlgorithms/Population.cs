using System;
using System.Collections.Generic;
using System.Linq;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms
{
    /// <summary>
    /// A set of generations.
    /// </summary>
    public class Population
    {
        /// <summary>
        /// The set of generations.
        /// </summary>
        public List<Generation> Generations { get; private set; }

        /// <summary>
        /// Youngest generation.
        /// </summary>
        public Generation LatestGeneration { get; private set; }

        /// <summary>
        /// Number of generations.
        /// </summary>
        public int GenerationNumber { get; private set; }

        /// <summary>
        /// Number of chromosomes in each generation.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// The city to find the shortest route in.
        /// </summary>
        public City TheCity { get; private set; }

        /// <summary>
        /// Create a new population with a randomized first generation.
        /// </summary>
        /// <param name="size">Number of chromosomes in each generation.</param>
        /// <param name="city">City to find the shortest route in.</param>
        public Population(int size, City city)
        {
            Size = size;
            TheCity = city;
            CreateFirstGeneration();
        }

        /// <summary>
        /// Create the next generation by using selection on the latest generation.
        /// </summary>
        public void CreateNextGeneration()
        {
            // Use Selection to choose Size/2 pairs.
            // Use GeneticOperators to create children.
        }

        /// <summary>
        /// Create a set of randomized chromosomes to make up the first generation.
        /// </summary>
        private void CreateFirstGeneration()
        {
            var chromosomes = new RouteChromosome[Size];
            for (int i = 0; i < Size; ++i)
            {
                var shuffledLocations = Point.GetShuffledClone(TheCity.Homes);
                var chromosome = new RouteChromosome(TheCity.Warehouse, shuffledLocations);
                chromosomes[i] = chromosome;
            }

            var firstGeneration = new Generation(chromosomes);
            Generations.Add(firstGeneration);
            LatestGeneration = firstGeneration;
        }
    }
}
