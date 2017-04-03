using System;
using System.Collections.Generic;
using System.Linq;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms
{
    /// <summary>
    /// Interface for a generic chromosome. Any changes to the genes must be made through the
    /// appropriate getter and setter methods.
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

        /// <summary>
        /// Returns a shallow copy of the chromosome.
        /// </summary>
        IChromosome Clone();

        /// <summary>
        /// Returns the gene at the index.
        /// </summary>
        Gene GetGene(int index);

        /// <summary>
        ///  Returns the genes between the start and end indices (inclusive).
        /// </summary>
        Gene[] GetGenes(int startIndex, int endIndex);

        /// <summary>
        /// Returns the entire gene array.
        /// </summary>
        Gene[] GetGenes();

        /// <summary>
        /// Replaces the gene at the index.
        /// </summary>
        void ReplaceGene(int index, Gene gene);

        /// <summary>
        /// Replaces the genes starting at index. If there are more genes provided than there are
        /// genes to replace, the excess genes will be appended to the chromosome (i.e. the
        /// chromosome's length will be increased).
        /// </summary>
        void ReplaceGenes(int index, Gene[] genes);

        /// <summary>
        /// Reverse the order of all genes between index and start or end (inclusive).
        /// </summary>
        /// <param name="onRight">True if reversing genes on right side, false if left side.</param>
        void ReverseGenes(int index, bool onRight);

        /// <summary>
        /// Find the pair of adjacent genes with the largest change between them.
        /// </summary>
        int GetIndexOfLargestChange();
    }
}
