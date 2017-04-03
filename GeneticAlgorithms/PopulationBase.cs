using System;
using System.Collections.Generic;
using System.Linq;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms
{
    /// <summary>
    /// A set of generations.
    /// </summary>
    public abstract class PopulationBase
    {
        /// <summary>
        /// The set of generations.
        /// </summary>
        public List<Generation> Generations { get; protected set; }

        /// <summary>
        /// Youngest generation.
        /// </summary>
        public Generation LatestGeneration { get; protected set; }

        /// <summary>
        /// Number of generations.
        /// </summary>
        public int GenerationNumber { get { return Generations.Count; } }

        /// <summary>
        /// Percent chance to use crossover instead of clone. [0f, 1f)
        /// </summary>
        public float CrossoverProbability { get; set; } = 0.70f;

        /// <summary>
        /// Percent chance to mutate each child. [0f, 1f)
        /// </summary>
        public float MutationProbability { get; set; } = 0.10f;

        /// <summary>
        /// Number of chromosomes in each generation.
        /// </summary>
        public int Size { get; protected set; }

        /// <summary>
        /// Method to select chromosomes for crossover.
        /// </summary>
        protected ISelection Selector;

        /// <summary>
        /// Create the first generation of chromosomes.
        /// </summary>
        protected abstract void CreateFirstGeneration();

        /// <summary>
        /// Create the next generation of chromosomes from the latest generation.
        /// </summary>
        public abstract void CreateNextGeneration();
    }
}
