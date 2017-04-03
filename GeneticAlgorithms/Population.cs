using System;
using System.Collections.Generic;
using System.Linq;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms
{
    /// <summary>
    /// A set of generations.
    /// </summary>
    public class Population
    {
        /// <summary>
        /// The set of generations.
        /// </summary>
        public List<Generation> Generations { get; private set; }

        /// <summary>
        /// Youngest generation.
        /// </summary>
        public Generation LatestGeneration { get; private set; }

        /// <summary>
        /// Number of generations.
        /// </summary>
        public int GenerationNumber { get { return Generations.Count; } }

        /// <summary>
        /// Percent chance to use crossover instead of clone. [0f, 1f)
        /// </summary>
        public float CrossoverProbability { get; set; } = 0.70f;

        /// <summary>
        /// Percent chance to mutate each child. [0f, 1f)
        /// </summary>
        public float MutationProbability { get; set; } = 0.10f;

        /// <summary>
        /// Percent chance to use guided right hand reverse on each child.
        /// </summary>
        public float GuidedReverseProbability { get; set; } = 0.10f;

        /// <summary>
        /// Number of chromosomes in each generation.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// The City to find the shortest route through.
        /// </summary>
        public City TheCity { get; private set; }

        /// <summary>
        /// Method to select chromosomes for crossover.
        /// </summary>
        private ISelection Selector;

        /// <summary>
        /// Create a new population with a randomized first generation.
        /// </summary>
        /// <param name="size">Number of chromosomes in each generation.</param>
        /// <param name="city">City to find the shortest route through.</param>
        /// <param name="crossoverProbability">Percent chance to crossover instead of clone.</param>
        /// <param name="mutationProbability">Percent chance to mutate each child.</param>
        /// <param name="selection">Method to select chromosomes for crossover.</param>
        public Population(int size, City city,
            float crossoverProbability, float mutationProbability,
            ISelection selection)
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
        private void CreateFirstGeneration()
        {
            var firstChromosomes = new RouteChromosome[Size];
            for (int i = 0; i < Size; ++i)
            {
                firstChromosomes[i] = new RouteChromosome(TheCity.Warehouses[0],
                                                          TheCity.GetHomesInRandomOrder());
            }

            Generations = new List<Generation>();
            Generations.Add(new Generation(firstChromosomes));
            LatestGeneration = Generations[0];
        }

        /// <summary>
        /// Create the next generation of chromosomes from the latest generation.
        /// </summary>
        public void CreateNextGeneration()
        {
            var newChromosomes = new List<IChromosome>();
            var parentChromosomes = LatestGeneration.Chromosomes;

            while (newChromosomes.Count < Size)
            {
                // Select pair
                var parent1 = Selector.Select(parentChromosomes);
                var parent2 = Selector.Select(parentChromosomes);

                // Crossover
                IChromosome child1, child2;
                if (RandomizationProvider.random.NextDouble() < CrossoverProbability)
                {
                    GeneticOperators.EntropyCrossover(parent1, parent2, out child1, out child2);
                }
                else
                {
                    child1 = parent1.Clone();
                    child2 = parent2.Clone();
                }

                // Add to newChromosomes
                newChromosomes.Add(child1);
                newChromosomes.Add(child2);
            }

            // Mutation
            for (int i = 0; i < Size; ++i)
            {
                if (RandomizationProvider.random.NextDouble() < MutationProbability)
                {
                    newChromosomes[i] = GeneticOperators.Mutate(newChromosomes[i]);
                }
            }

            // Reverse most fit chromosome
            var mostFit = LatestGeneration.GetMostFitChromosome();
            newChromosomes[0] = mostFit.Clone();
            newChromosomes[0] = GeneticOperators.GuidedReverse(newChromosomes[0], true);
            newChromosomes[1] = mostFit.Clone();
            newChromosomes[1] = GeneticOperators.GuidedReverse(newChromosomes[1], false);

            // Add new chromosomes to the next generation.
            var nextGeneration = new Generation(newChromosomes);
            Generations.Add(nextGeneration);
            LatestGeneration = nextGeneration;
        }
    }
}
