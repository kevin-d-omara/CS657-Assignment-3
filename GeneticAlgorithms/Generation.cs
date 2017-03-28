using System;
using System.Collections.Generic;
using System.Linq;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms
{
    /// <summary>
    /// A set of chromosomes.
    /// </summary>
    public class Generation
    {
        /// <summary>
        /// The list of chromosomes.
        /// </summary>
        public IChromosome[] Chromosomes { get; private set; }

        /// <summary>
        /// Number of chromosomes in this generation.
        /// </summary>
        public int Number { get; private set; }

        /// <summary>
        /// The most fit chromosome (i.e. the candidate solution).
        /// </summary>
        public IChromosome FittestChromosome { get; private set; }

        /// <summary>
        /// Create a generation from the given set of chromosomes.
        /// </summary>
        public Generation(List<IChromosome> chromosomes)
        {
            Number = chromosomes.Count;
            Chromosomes = chromosomes.ToArray();
        }
    }
}
