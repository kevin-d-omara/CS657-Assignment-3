using System;
using System.Collections.Generic;
using System.Linq;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms
{
    /// <summary>
    /// A set of generations of chromosomes for the one-agent delivery / travelling salesman
    /// problem.
    /// </summary>
    public class OneAgentPopulation : PopulationBase
    {
        /// <summary>
        /// The City to find the shortest route through.
        /// </summary>
        public City TheCity { get; private set; }

        /// <summary>
        /// Create a new population with a randomized first generation.
        /// </summary>
        /// <param name="size">Number of chromosomes in each generation.</param>
        /// <param name="city">City with one warehouse to find the shortest route through.</param>
        /// <param name="crossoverProbability">Percent chance to crossover instead of clone.</param>
        /// <param name="mutationProbability">Percent chance to mutate each child.</param>
        /// <param name="selection">Method to select chromosomes for crossover.</param>
        public OneAgentPopulation(int size, City city,
            float crossoverProbability, float mutationProbability, ISelection selection)
        {
            Size = size;
            TheCity = city;

            CrossoverProbability = crossoverProbability;
            MutationProbability = mutationProbability;
            Selector = selection;

            CreateFirstGeneration();
        }

        /// <summary>
        /// Create the first generation of chromosomes.
        /// </summary>
        protected override void CreateFirstGeneration()
        {
            var firstChromosomes = new RouteChromosome[Size];
            for (int i = 0; i < Size; ++i)
            {
                firstChromosomes[i] = new RouteChromosome(TheCity.Warehouses[0],
                                                          TheCity.GetHomesInRandomOrder());
            }

            Generations = new List<Generation>() { new Generation(firstChromosomes) };
            LatestGeneration = Generations[0];
        }
    }
}
