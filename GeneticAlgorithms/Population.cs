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
        /// The number of genes in each chromosome.
        /// </summary>
        public int ChromosomeLength { get; private set; }

        /// <summary>
        /// Create a new population with a randomized first generation.
        /// </summary>
        /// <param name="size">The number of chromosomes in each generation.</param>
        /// <param name="chromosomeLength">The length of each chromosome.</param>
        public Population(int size, int chromosomeLength)
        {

        }

        /// <summary>
        /// Create the next generation by using selection on the latest generation.
        /// </summary>
        public void CreateNextGeneration()
        {
            // Use Selection to choose Size/2 pairs.
            // Use GeneticOperators to create children.
        }
    }
}
