using System;
using System.Collections.Generic;
using System.Linq;
using KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms;

namespace KevinDOMara.SDSU.CS657.Assignment3.Application
{
    public class GeneticAlgorithm
    {
        /// <summary>
        /// Population to evolve.
        /// </summary>
        public PopulationBase population { get; private set; }

        /// <summary>
        /// Number of generations to evolve.
        /// </summary>
        public int numberOfGenerations { get; private set; }

        /// <summary>
        /// The most fit chromosome of the entire population.
        /// </summary>
        public IChromosome CandidateSolution { get; private set; }

        /// <summary>
        /// Create a new genetic algorithm.
        /// </summary>
        /// <param name="numberOfGenerations">Number of generations to evolve.</param>
        /// <param name="population">Population to evolve.</param>
        public GeneticAlgorithm(int numberOfGenerations, PopulationBase population)
        {
            this.population = population;
            this.numberOfGenerations = numberOfGenerations;
        }

        /// <summary>
        /// Create new generations until the final generation is reached.
        /// </summary>
        public void EvolveSolution()
        {
            for (int i = 0; i < numberOfGenerations; ++i)
            {
                population.CreateNextGeneration();

                var fittestChromosome = population.LatestGeneration.GetMostFitChromosome();
                if (CandidateSolution == null ||
                    fittestChromosome.Fitness > CandidateSolution.Fitness)
                {
                    CandidateSolution = fittestChromosome;
                }
            }
        }
    }
}
