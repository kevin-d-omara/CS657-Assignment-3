using System;
using System.Collections.Generic;
using System.Linq;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms
{
    /// <summary>
    /// Operations that can be used to create modified chromosomes.
    /// </summary>
    public class GeneticOperators
    {
        /// <summary>
        /// Return a shallow copy of the chromosome.
        /// </summary>
        public static RouteChromosome Clone(RouteChromosome chromosome)
        {
            var genes = (Point[])chromosome.Genes.Clone();
            var clone = new RouteChromosome(chromosome.Home, genes);

            return clone;
        }

        /// <summary>
        /// Return a shallow copy of the chromosome with the position of two genes swapped.
        /// </summary>
        public static RouteChromosome Mutate(RouteChromosome chromosome)
        {
            var genes = (Point[])chromosome.Genes.Clone();

            var index1 = RandomizationProvider.random.Next(chromosome.Length);
            var index2 = RandomizationProvider.random.Next(chromosome.Length);
            var gene1 = genes[index1];
            var gene2 = genes[index2];

            genes[index1] = gene2;
            genes[index2] = gene1;

            var mutant = new RouteChromosome(chromosome.Home, genes);

            return mutant;
        }
    }
}
