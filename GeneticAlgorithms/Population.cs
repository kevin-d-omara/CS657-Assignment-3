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
        public int GenerationNumber { get; private set; }

        /// <summary>
        /// Percent chance to use crossover instead of clone. [0f, 1f)
        /// </summary>
        public float CrossoverProbability { get; set; } = 0.70f;

        /// <summary>
        /// Percent chance to mutate each child. [0f, 1f)
        /// </summary>
        public float MutationProbability { get; set; } = 0.10f;

        /// <summary>
        /// Number of chromosomes in each generation.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// The City to find the shortest route through.
        /// </summary>
        public City TheCity { get; private set; }

        /// <summary>
        /// Create a new population with a randomized first generation and default genetic operator
        /// probabilities.
        /// </summary>
        /// <param name="size">Number of chromosomes in each generation.</param>
        /// <param name="city">City to find the shortest route through.</param>
        public Population(int size, City city)
        {
            Size = size;
            TheCity = city;

            CreateFirstGeneration();
        }

        /// <summary>
        /// Create a new population with a randomized first generation.
        /// </summary>
        /// <param name="size">Number of chromosomes in each generation.</param>
        /// <param name="city">City to find the shortest route through.</param>
        /// <param name="crossoverProbability">Percent chance to crossover instead of clone.</param>
        /// <param name="mutationProbability">Percent chance to mutate each child.</param>
        public Population(int size, City city,
            float crossoverProbability, float mutationProbability) : this(size, city)
        {
            CrossoverProbability = crossoverProbability;
            MutationProbability = mutationProbability;
        }

        /// <summary>
        /// Create the first generation of chromosomes.
        /// </summary>
        private void CreateFirstGeneration()
        {
            var firstChromosomes = new RouteChromosome[Size];
            for (int i = 0; i < Size; ++i)
            {
                firstChromosomes[i] = new RouteChromosome(TheCity.Warehouse,
                                                          TheCity.GetHomesInRandomOrder());
            }

            Generations = new List<Generation>();
            Generations.Add(new Generation(firstChromosomes));
            GenerationNumber = 1;
            LatestGeneration = Generations[0];
        }

        /// <summary>
        /// Create the next generation of chromosomes from the latest generation.
        /// </summary>
        public void CreateNextGeneration()
        {
            var newChromosomes = new List<IChromosome>();

            while (newChromosomes.Count < Size)
            {
                // Select pair (via Tournament Selection)
                // TODO: use tournament selection
                IChromosome parent1 = LatestGeneration.Chromosomes[0];
                IChromosome parent2 = LatestGeneration.Chromosomes[1];

                // Crossover
                IChromosome child1, child2;
                if (RandomizationProvider.random.NextDouble() < CrossoverProbability)
                {
                    // TODO: use CrossoverNoDuplicateGenes
                    GeneticOperators.Crossover(parent1, parent2, out child1, out child2);
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

            // Add new chromosomes to the next generation.
            var nextGeneration = new Generation(newChromosomes);
            Generations.Add(nextGeneration);
            LatestGeneration = nextGeneration;
        }
    }
}
