using System;
using System.Collections.Generic;
using System.Linq;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms
{
    /// <summary>
    /// Interface for a generic immutable chromosome.
    /// </summary>
    public interface IChromosome
    {
        /// <summary>
        /// Gets the length (number of genes).
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Gets the fitness according to some metric.
        /// </summary>
        float Fitness { get; }
    }
}
