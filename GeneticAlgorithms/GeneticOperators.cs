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

        /// <summary>
        /// Exchange genes past a random index between parentA and parentB. Does not change the
        /// original chromosomes.
        /// </summary>
        /// <remarks>
        /// parentA = [0,1,2,...,n]  parentB = [0,1,2,...,n]
        ///         = [ A-l | A-r ]          = [ B-l | B-r ]
        /// childA  = [ A-l | B-r ]  childB  = [ B-l | A-r ]
        ///                 ^ breaking point (random index)
        /// </remarks>
        public static void Crossover(RouteChromosome parentA, RouteChromosome parentB,
            out RouteChromosome childA, out RouteChromosome childB)
        {
            if (parentA.Length != parentB.Length)
            {
                throw new System.ArgumentException("The number of genes in both parents is unequal.");
            }

            var length = parentA.Length;
            if (length < 2)
            {
                throw new System.ArgumentException("The chromosome is too short. There must be at least two genes to use crossover.");
            }

            var genesA = (Point[])parentA.Genes.Clone();
            var genesB = (Point[])parentB.Genes.Clone();
            var breakingPoint = RandomizationProvider.random.Next(1, length);

            for (int i = breakingPoint; i < length; ++i)
            {
                genesA[i] = parentB.Genes[i];
                genesB[i] = parentA.Genes[i];
            }

            childA = new RouteChromosome(parentA.Home, genesA);
            childB = new RouteChromosome(parentB.Home, genesB);
        }
    }
}
