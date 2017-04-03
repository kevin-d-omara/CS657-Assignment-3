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
            int numberOfHomes = 30;
            City cityA, cityB;
            CreateCity(numberOfHomes, out cityA, out cityB);
            var GeneticA = CreateGeneticAlgorithm(cityA);
            var GeneticB = CreateGeneticAlgorithm(cityB);

            // Evolve solution for a single run.
            GeneticA.EvolveSolution();
            GeneticB.EvolveSolution();

            // Print summary of each generation.
            for (int i = 0; i < GeneticA.population.GenerationNumber; ++i)
            {
                var generation = GeneticA.population.Generations[i];
                //DisplayFitnessOf(generation, i);
            }

            // Print solution to file.
            var outfileA = "ResultsA.txt";
            var outfileB = "ResultsB.txt";
            var fittestChromosomeA = GeneticA.population.LatestGeneration.GetMostFitChromosome();
            var fittestChromosomeB = GeneticB.population.LatestGeneration.GetMostFitChromosome();
            var routeA = ((RouteChromosome)fittestChromosomeA).Route;
            var routeB = ((RouteChromosome)fittestChromosomeB).Route;
            PrintRoute(routeA, outfileA);
            PrintRoute(routeB, outfileB);

            // Print best solution from run.
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("Most fit chromosomeA of run: " + GeneticA.CandidateSolution.Fitness);
            Console.WriteLine("Most fit chromosomeB of run: " + GeneticB.CandidateSolution.Fitness);

            //Console.ReadKey();
        }

        /// <summary>
        /// Return a new genetic algorithm from the hard coded values in this function.
        /// </summary>
        private static GeneticAlgorithm CreateGeneticAlgorithm(City city)
        {
            var sizeOfPopulation = 100;
            var numberOfGenerations = 200;

            var crossoverProbability = 0.70f;
            var mutationProbability = 0.50f;

            var tournamentSize = 3;
            var selector = new TournamentSelection(tournamentSize);

            var population = new OneAgentPopulation(sizeOfPopulation, city,
                crossoverProbability, mutationProbability, selector);

            return new GeneticAlgorithm(numberOfGenerations, population);
        }

        /// <summary>
        /// Create a randomized rectangular city with N homes.
        /// Fixed to a 30x30 grid with warehouses at (5,5) and (25,25).
        /// </summary>
        /// <param name="N">Number of randomly placed homes.</param>
        /// <param name="cityA">City with warehouse A and homes closer to A.</param>
        /// <param name="cityB">City with warehouse B and homes closer to B.</param>
        private static void CreateCity(int N, out City cityA, out City cityB)
        {
            var width = 30;
            var height = 30;

            var occupied = new HashSet<Point>();
            var pointA = new Point(5, 5);
            var pointB = new Point(25, 25);
            occupied.Add(pointA);
            occupied.Add(pointB);

            var warehouseA = new List<Point>() { pointA };
            var warehouseB = new List<Point>() { pointB };

            // Make N random homes.
            var homes = new List<Point>();
            for (int i = 0; i < N; ++i)
            {
                while (true)
                {
                    var point = GetRandomPointIn(width, height);
                    if (!occupied.Contains(point))
                    {
                        occupied.Add(point);
                        homes.Add(point);
                        break;
                    }
                }
            }

            List<Point> closerToA, closerToB;
            Point.GetPointsCloserTo(pointA, pointB, homes.ToArray(), out closerToA, out closerToB);

            cityA = new City(closerToA, warehouseA);
            cityB = new City(closerToB, warehouseB);
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
