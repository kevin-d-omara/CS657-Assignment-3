using System;
using System.Collections.Generic;
using System.Linq;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms
{
    /// <summary>
    /// A method for selecting chromosomes.
    /// </summary>
    public interface ISelection
    {
        /// <summary>
        /// Selects a chromosome by some heuristic.
        /// </summary>
        IChromosome Select(IList<IChromosome> chromosomes);
    }
}
