using System;
using System.Collections.Generic;
using System.Linq;
using KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms;
using KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms.UnitTests;

namespace KevinDOMara.SDSU.CS657.Assignment3.Application
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            var GA = CreateGeneticAlgorithm();

            // Evolve solution for a single run.
            GA.EvolveSolution();

            // Print summary of each generation.
            for (int i = 0; i < GA.population.GenerationNumber; ++i)
            {
                var generation = GA.population.Generations[i];
                DisplayFitnessOf(generation, i);
            }

            // Print solution to file.
            var outfile = "Results.txt";
            var fittestChromosome = GA.population.LatestGeneration.GetMostFitChromosome();
            var route = ((RouteChromosome)fittestChromosome).Route;
            PrintRoute(route, outfile);

            // Print best solution from run.
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("Most fit chromosome of run: " + GA.CandidateSolution.Fitness);

            Console.ReadKey();
        }

        /// <summary>
        /// Return a new genetic algorithm from the hard coded values in this function.
        /// </summary>
        private static GeneticAlgorithm CreateGeneticAlgorithm()
        {
            var sizeOfPopulation = 300;
            var numberOfGenerations = 200;

            var crossoverProbability = 0.70f;
            var mutationProbability = 0.50f;

            var tournamentSize = 3;
            var selector = new TournamentSelection(tournamentSize);

            var width = 30;
            var height = 30;
            var numberOfHomes = 30;
            var city = CreateCity(width, height, numberOfHomes);

            var population = new OneAgentPopulation(sizeOfPopulation, city,
                crossoverProbability, mutationProbability, selector);

            return new GeneticAlgorithm(numberOfGenerations, population);
        }

        /// <summary>
        /// Create a randomized rectangular city with N homes.
        /// </summary>
        private static City CreateCity(int width, int height, int N)
        {
            var occupied = new HashSet<Point>();
            var point = GetRandomPointIn(width, height);

            occupied.Add(point);
            var warehouse = point;

            var homes = new List<Point>();
            for (int i = 0; i < N; ++i)
            {
                while (true)
                {
                    point = GetRandomPointIn(width, height);
                    if (!occupied.Contains(point))
                    {
                        occupied.Add(point);
                        homes.Add(point);
                        break;
                    }
                }
            }

            return new City(homes, new List<Point>() { warehouse });
        }

        /// <summary>
        /// Create a fixed city for testing.
        /// </summary>
        /// <returns></returns>
        private static City CreateCity()
        {
            var warehouse = new Point(0, 0);

            // Most fit chromosome: -27.31
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

            return new City(homes, new List<Point>() { warehouse });
        }

        /// <summary>
        /// Return a random point within the rectangle defined by (0,0) to (width, height).
        /// </summary>
        private static Point GetRandomPointIn(int width, int height)
        {
            var x = RandomizationProvider.random.Next(0, width + 1);
            var y = RandomizationProvider.random.Next(0, height + 1);
            return new Point(x, y);
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

        private static void PrintRoute(Route route, string filename)
        {
            string[] lines = new string[route.Points.Length];
            for (int i = 0; i < route.Points.Length; ++i)
            {
                lines[i] = route.Points[i].x.ToString() + ", " + route.Points[i].y.ToString();
            }
            System.IO.File.WriteAllLines(filename, lines);
        }
    }
}
