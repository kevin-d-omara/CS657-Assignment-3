using System;
using System.Collections.Generic;
using System.Linq;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms
{
    /// <summary>
    /// A set of generations of chromosomes for the two-agent delivery / travelling salesman
    /// problem.
    /// </summary>
    public class TwoAgentPopulation : PopulationBase
    {
        /// <summary>
        /// The City to find the shortest route through.
        /// </summary>
        public City TheCity { get; private set; }

        /// <summary>
        /// The set of generations for agent two.
        /// </summary>
        public List<Generation> GenerationsB { get; protected set; }

        /// <summary>
        /// Youngest generation for agent two.
        /// </summary>
        public Generation LatestGenerationB { get; protected set; }

        /// <summary>
        /// Create a new population with a randomized first generation.
        /// </summary>
        /// <param name="size">Number of chromosomes in each generation.</param>
        /// <param name="city">City with two warehouses to find the shortest route through.</param>
        /// <param name="crossoverProbability">Percent chance to crossover instead of clone.</param>
        /// <param name="mutationProbability">Percent chance to mutate each child.</param>
        /// <param name="selection">Method to select chromosomes for crossover.</param>
        public TwoAgentPopulation(int size, City city,
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
            // Determine which warehouse each point is closer to.
            var closerToA = new List<Point>();
            var closerToB = new List<Point>();
            foreach (Point point in TheCity.Homes)
            {
                var distanceToA = Point.Distance(TheCity.Warehouses[0], point);
                var distanceToB = Point.Distance(TheCity.Warehouses[1], point);

                if (distanceToA >= distanceToB)
                {
                    closerToA.Add(point);
                }
                else
                {
                    closerToB.Add(point);
                }
            }

            // Create random starting chromosomes for agent A and agent B. Chromosomes for A and B
            // may be different length, but all A chromosomes are the same length and same for B.
            var initialChromosomesA = new RouteChromosome[closerToA.Count];
            var initialChromosomesB = new RouteChromosome[closerToB.Count];
            for (int i = 0; i < Size; ++i)
            {
                initialChromosomesA[i] = new RouteChromosome(TheCity.Warehouses[0],
                    Point.GetShuffledClone(closerToA));
                initialChromosomesB[i] = new RouteChromosome(TheCity.Warehouses[1],
                    Point.GetShuffledClone(closerToB));
            }

            Generations = new List<Generation>();
            GenerationsB = new List<Generation>();
            Generations.Add(new Generation(initialChromosomesA));
            GenerationsB.Add(new Generation(initialChromosomesB));
            LatestGeneration = Generations[0];
            LatestGenerationB = GenerationsB[0];
        }

        /// <summary>
        /// Create the next generation of chromosomes from the latest generation.
        /// </summary>
        public override void CreateNextGeneration()
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
