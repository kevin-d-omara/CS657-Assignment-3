using System;
using System.Collections.Generic;
using System.Linq;
using KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms;
using KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms.UnitTests;

namespace KevinDOMara.SDSU.CS657.Assignment3.Application
{
    public class Driver
    {
        public static void Main(string[] args)
        {
            var population = MakePopulation();

            var limit = 100;
            for (int i = 0; i < limit; ++i)
            {
                DisplayFitnessOf(population.LatestGeneration, population.GenerationNumber);
                population.CreateNextGeneration();
            }

            Console.ReadKey();
        }

        private static Population MakePopulation()
        {
            var size = 6;
            var city = CreateCity();

            var crossoverProbability = 0.70f;
            var mutationProbability = 0.10f;

            var tournamentSize = 3;
            var selection = new TournamentSelection(tournamentSize);

            return new Population(size, city, crossoverProbability, mutationProbability, selection);
        }

        private static City CreateCity()
        {
            var warehouse = new Point(0, 0);

            // Most fit: -27.31
            var homes = new List<Point>()
            {
                new Point(1, 0),
                new Point(2, 0),
                new Point(3, 0),
                new Point(4, 0),
                new Point(5, 0),
                new Point(6, 0),
                new Point(7, 0),
                new Point(8, 0),
                new Point(8, 8),
            };

            return new City(homes, warehouse);
        }

        private static void DisplayFitnessOf(Generation generation, int generationNumber)
        {
            Console.WriteLine("Generation " + generationNumber);
            Console.WriteLine("Highest Fitness: " + generation.GetMostFitChromosome().Fitness);
            Console.WriteLine("Average: " + generation.GetAverageFitness());
            Console.WriteLine("Lowest : " + generation.GetLeastFitChromosome().Fitness);
            //Console.WriteLine("Candidate Solution: " + generation.GetCandidateChromosome().GetValue());
            Console.WriteLine("");
        }
    }
}
