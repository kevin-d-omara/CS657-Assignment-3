using System;
using System.Collections.Generic;
using System.Linq;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms
{
    /// <summary>
    /// A possible solution (route) to the travelling salesman problem.
    /// </summary>
    public abstract class ChromosomeBase : IChromosome
    {
        /// <summary>
        /// Number of points in the route (discluding start/end).
        /// </summary>
        public int Length { get; protected set; }

        /// <summary>
        /// Gets the fitness according to some metric.
        /// </summary>
        public abstract float Fitness { get; protected set; }

        /// <summary>
        /// The genes of this chromosome.
        /// </summary>
        protected Gene[] _genes;

        /// <summary>
        /// Return a shallow copy of this chromosome.
        /// </summary>
        public abstract IChromosome Clone();

        /// <summary>
        /// Returns the gene at the index.
        /// </summary>
        public Gene GetGene(int index)
        {
            if (index < 0 || index >= Length)
            {
                throw new System.ArgumentOutOfRangeException("index", index, String.Format("The provided index is out of the range [0, {0}].", Length - 1));
            }

            return _genes[index];
        }

        /// <summary>
        /// Return the genes between the start and end indices.
        /// </summary>
        public Gene[] GetGenes(int startIndex, int endIndex)
        {
            if (startIndex < 0 || startIndex >= Length)
            {
                throw new System.ArgumentOutOfRangeException("startIndex", startIndex, String.Format("The starting index is out of the range [0, {0}].", Length - 1));
            }
            if (endIndex < 0 || endIndex >= Length)
            {
                throw new System.ArgumentOutOfRangeException("startIndex", endIndex, String.Format("The ending index is out of the range [0, {0}].", Length - 1));
            }
            if (startIndex > endIndex)
            {
                throw new System.ArgumentException(String.Format("The starting index ({0}) is past the ending index ({0}).", startIndex, endIndex));
            }

            return _genes.Skip(startIndex).Take(endIndex - startIndex + 1).ToArray();
        }

        /// <summary>
        /// Return the entire gene array.
        /// </summary>
        public Gene[] GetGenes()
        {
            return _genes;
        }

        /// <summary>
        /// Replace the gene at the index.
        /// </summary>
        public virtual void ReplaceGene(int index, Gene gene)
        {
            if (index < 0 || index >= Length)
            {
                throw new System.ArgumentOutOfRangeException("index", index, String.Format("The provided index is out of the range [0, {0}].", Length - 1));
            }

            _genes[index] = gene;
        }

        /// <summary>
        /// Replaces the genes starting at index. If there are more genes provided than there are
        /// genes to replace, the excess genes will be appended to the chromosome (i.e. the
        /// chromosome's length will be increased).
        /// </summary>
        public virtual void ReplaceGenes(int startIndex, Gene[] genes)
        {
            ReplaceElements<Gene>(startIndex, _genes, genes);
        }

        /// <summary>
        /// Replaces the elements starting at index. If there are more elements provided than there
        /// are elements to replace, the excess elements will be appended to the array (i.e. the
        /// array's length will be increased).
        /// </summary>
        /// <typeparam name="T">Type of the elements.</typeparam>
        /// <param name="startIndex">Index to start replacing from.</param>
        /// <param name="array">Array to replace elements in.</param>
        /// <param name="elements">Elements to replace with.</param>
        protected void ReplaceElements<T>(int startIndex, T[] array, T[] elements)
        {
            var length = array.Length;
            if (startIndex < 0 || startIndex >= length)
            {
                throw new System.ArgumentOutOfRangeException("index", startIndex, String.Format("The provided index was out of the range [0, {0}].", length - 1));
            }

            var numOverwritten = length - startIndex;
            var numNew = elements.Length;
            var numExtra = numNew - numOverwritten;

            if (numExtra > 0)
            {
                Array.Resize(ref array, length + numExtra);
            }

            for (int i = 0; i < numNew; ++i)
            {
                array[startIndex + 1] = elements[i];
            }
        }
    }
}
