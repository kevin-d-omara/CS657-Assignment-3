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
        /// Create a generation from the given set of chromosomes.
        /// </summary>
        public Generation(IList<IChromosome> chromosomes)
        {
            Number = chromosomes.Count;
            Chromosomes = chromosomes.ToArray();
        }

        /// <summary>
        /// Return the highest fitness value of this set of chromosomes.
        /// </summary>
        public IChromosome GetMostFitChromosome()
        {
            IChromosome mostFitChromosome = null;
            var highestFitness = Single.MinValue;

            foreach (IChromosome chromosome in Chromosomes)
            {
                var fitness = chromosome.Fitness;
                if (fitness > highestFitness)
                {
                    highestFitness = fitness;
                    mostFitChromosome = chromosome;
                }
            }

            return mostFitChromosome;
        }

        /// <summary>
        /// Return the lowest fitness value of this set of chromosomes.
        /// </summary>
        public IChromosome GetLeastFitChromosome()
        {
            IChromosome leastFitChromosome = null;
            var lowestFitness = Single.MaxValue;

            foreach (IChromosome chromosome in Chromosomes)
            {
                var fitness = chromosome.Fitness;
                if (fitness < lowestFitness)
                {
                    lowestFitness = fitness;
                    leastFitChromosome = chromosome;
                }
            }

            return leastFitChromosome;
        }

        /// <summary>
        /// Return the average fitness value of this set of chromosomes.
        /// </summary>
        public float GetAverageFitness()
        {
            var averageFitness = 0f;

            foreach (IChromosome chromosome in Chromosomes)
            {
                averageFitness += chromosome.Fitness;
            }

            return averageFitness / Chromosomes.Length;
        }
    }
}
