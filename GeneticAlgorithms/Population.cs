using System;
using System.Collections.Generic;
using System.Linq;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms
{
    /// <summary>
    /// A set of generations.
    /// </summary>
    public class Population<T> where T : IChromosome
    {
        /// <summary>
        /// The set of generations.
        /// </summary>
        public Generation<T>[] Generations { get; private set; }

        /// <summary>
        /// Youngest generation.
        /// </summary>
        public Generation<T> LatestGeneration { get; private set; }

        /// <summary>
        /// Number of generations.
        /// </summary>
        public int GenerationNumber { get; private set; }

        /// <summary>
        /// Create a new population.
        /// </summary>
        /// <param name="size">The number of chromosomes in each generation.</param>
        /// <param name="chromosomeLength">The length of each chromosome.</param>
        public Population(int size, int chromosomeLength)
        {

        }
    }
}
