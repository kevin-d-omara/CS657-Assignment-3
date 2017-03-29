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
        /// Return a shallow copy of the chromosome with the position of two genes swapped.
        /// </summary>
        public static IChromosome Mutate(IChromosome chromosome)
        {
            var mutant = chromosome.Clone();

            var index1 = RandomizationProvider.random.Next(mutant.Length);
            var index2 = RandomizationProvider.random.Next(mutant.Length);
            var gene1 = mutant.GetGene(index1);
            var gene2 = mutant.GetGene(index2);

            mutant.ReplaceGene(index1, gene2);
            mutant.ReplaceGene(index2, gene1);

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
        public static void Crossover(IChromosome parentA, IChromosome parentB,
            out IChromosome childA, out IChromosome childB)
        {
            // TODO: replace out parameters w/ an IList<IChromosome> return value

            if (parentA.Length != parentB.Length)
            {
                throw new System.ArgumentException(String.Format("The number of genes in both parents is unequal: {0} != {1}", parentA.Length, parentB.Length));
            }

            var length = parentA.Length;
            if (length < 2)
            {
                throw new System.ArgumentException("The chromosomes are too short. There must be at least two genes to use crossover.");
            }

            var breakingPoint = RandomizationProvider.random.Next(1, length);
            var genesA = parentA.GetGenes(breakingPoint, length - 1);
            var genesB = parentB.GetGenes(breakingPoint, length - 1);

            childA = parentA.Clone();
            childB = parentB.Clone();
            childA.ReplaceGenes(breakingPoint, genesB);
            childB.ReplaceGenes(breakingPoint, genesA);
        }
    }
}
