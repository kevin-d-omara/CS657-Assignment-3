using System;
using System.Collections.Generic;
using System.Linq;
using KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms;
using KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms.UnitTests;

namespace KevinDOMara.SDSU.CS657.Assignment3.Application
{
    public class OneAgentDriver
    {
        public OneAgentPopulation population;
        public int numberOfGenerations;

        public OneAgentDriver(int sizeOfPopulation, int numberOfGenerations, City city,
            float crossoverProbability, float mutationProbability, ISelection selector)
        {
            this.numberOfGenerations = numberOfGenerations;

            population = new OneAgentPopulation(sizeOfPopulation, city,
                crossoverProbability, mutationProbability, selector);
        }

        public void EvolveSolution()
        {
            for (int i = 0; i < numberOfGenerations; ++i)
            {
                // Display fitness (?)
                population.CreateNextGeneration();
            }

            // Print route (?)
        }

        /*
            var limit = 200;
            for (int i = 0; i < limit; ++i)
            {
                DisplayFitnessOf(population.LatestGeneration, population.GenerationNumber);
                population.CreateNextGeneration();
            }

            PrintRoute(((RouteChromosome)(population.LatestGeneration.GetMostFitChromosome())).Route, "Results.txt");
         */

        public void PrintRoute(string filename)
        {

        }
    }
}
