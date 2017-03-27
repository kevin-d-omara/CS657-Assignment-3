using System;
using System.Collections.Generic;
using System.Linq;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms
{
    /// <summary>
    /// A set of chromosomes.
    /// </summary>
    public class Generation<T> where T : IChromosome
    {
        /// <summary>
        /// The list of chromosomes.
        /// </summary>
        public T[] Chromosomes { get; private set; }

        /// <summary>
        /// Number of chromosomes in this generation.
        /// </summary>
        public int Number { get; private set; }

        /// <summary>
        /// Create a generation from the given set of chromosomes.
        /// </summary>
        public Generation(List<T> chromosomes)
        {
            Number = chromosomes.Count;
            Chromosomes = chromosomes.ToArray();
        }
    }
}
