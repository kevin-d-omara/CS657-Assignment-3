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
        /// Create two children by exchanging genes past a random index between parentA and parentB.
        /// This method is "blind" because children may contain internally-duplicated genes.
        /// </summary>
        /// <remarks>
        /// parentA = [0,1,2,...,n]  parentB = [0,1,2,...,n]
        ///         = [ A-l | A-r ]          = [ B-l | B-r ]
        /// childA  = [ A-l | B-r ]  childB  = [ B-l | A-r ]
        ///                 ^ breaking point (random index)
        /// </remarks>
        public static void BlindCrossover(IChromosome parentA, IChromosome parentB,
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

        /// <summary>
        /// Create two children by re-ordereing genes past a random index in parentA and parentB
        /// using entropy from each other. This method prevents children from having
        /// internally-duplicated genes.
        /// </summary>
        /// <remarks>
        /// parentA = [a b c | d e f]
        /// parentB = [a d e | c f b]
        ///                  ^ breaking point
        ///                  
        /// Sections to swap:          Blind crossover result:
        /// swapA = [d e f]     ->     childA = [a b c | c f b]
        /// swapB = [c f b]     ->     childB = [a d e | d e f]
        /// 
        /// Note: childA has duplicate genes b and c.
        ///       childB has duplicate genes d and e.
        /// 
        /// Instead of exchanging genes (and risking internal duplication), this crossover method
        /// re-orders the right halves according to entropy from each other.
        /// </remarks>
        public static void EntropyCrossover(IChromosome parentA, IChromosome parentB,
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
            childA.ReplaceGenes(breakingPoint, ReorderByEntropy(genesA, genesB));
            childB.ReplaceGenes(breakingPoint, ReorderByEntropy(genesB, genesA));
        }

        /// <summary>
        /// Create re-ordered gene array from A using entropy in B.
        /// </summary>
        /// <remarks>
        /// A = [a b c d e]
        ///        * *   *  ← genes exclusive to A
        /// B = [f d a g h]
        ///      *     * *  ← genes exclusive to B
        /// 
        /// Overwite B (*) genes with A (*) genes from left-to-right:
        /// 
        /// A = [a b c d e]
        ///       ↙   ↘  ↓
        /// B = [f d a g h]
        ///   = [b d a c e]
        /// </remarks>
        /// <param name="A">Gene array to be re-ordered.</param>
        /// <param name="B">Source of entropy.</param>
        /// <returns>Re-ordered gene[] containing genes from A.</returns>
        private static Gene[] ReorderByEntropy(Gene[] A, Gene[] B)
        {
            if (A.Length != B.Length)
            {
                throw new System.ArgumentException(String.Format("The number of genes in A and B is unequal: {0} != {1}", A.Length, B.Length));
            }

            var length = A.Length;
            var genesInA = new HashSet<Gene>(A);
            var genesInB = new HashSet<Gene>(B);

            var exclusiveToA = new Queue<Gene>();
            for (int i = 0; i < length; ++i)
            {
                // Collect genes in A that are not in B.
                if (!genesInB.Contains(A[i]))
                {
                    exclusiveToA.Enqueue(A[i]);
                }
            }

            var C = (Gene[])B.Clone();
            for (int i = 0; i < length; ++i)
            {
                // Overwrite genes in B that are not in A.
                if (!genesInA.Contains(B[i]))
                {
                    C[i] = exclusiveToA.Dequeue();
                }
            }

            return C;
        }

        /// <summary>
        /// Return a shallow copy with the order of all genes reversed starting at a random index.
        /// </summary>
        /// <remarks>
        /// chromosome = [a b f e d c]
        ///                   ^ random index
        ///            = [a b c d e f]
        ///                   | ← → | reversed the order of these genes
        /// </remarks>
        /// <param name="onRight">True if reversing genes on right side, false if left side.</param>
        public static IChromosome RightHandReverse(IChromosome chromosome, bool onRight)
        {
            var reversedChromosome = chromosome.Clone();

            var index = RandomizationProvider.random.Next(0, chromosome.Length);
            reversedChromosome.ReverseGenes(index, onRight);

            return reversedChromosome;
        }

        /// <summary>
        /// Return a shallow copy with the order of all genes reversed beyond a pivot where the two
        /// adjacent genes have the largest change between them.
        /// </summary>
        /// <remarks>
        /// chromosome = [a b f e d c]
        ///                  ^ largest change between any adjacent genes
        ///            = [a b c d e f]
        ///                   | ← → | reversed the order of these genes
        /// </remarks>
        /// <param name="onRight">True if reversing genes on right side, false if left side.</param>
        public static IChromosome GuidedReverse(IChromosome chromosome, bool onRight)
        {
            var reversedChromosome = chromosome.Clone();

            var index = chromosome.GetIndexOfLargestChange();
            if (onRight) { ++index; }
            reversedChromosome.ReverseGenes(index, onRight);

            return reversedChromosome;
        }
    }
}
