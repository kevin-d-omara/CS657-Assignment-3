using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms.UnitTests
{
    [TestFixture]
    public class GenerationTest
    {
        //Generation generation;
        List<RouteChromosome> chromosomeList = new List<RouteChromosome>();

        RouteChromosome chromoA, chromoB;
        Point home;
        Point[] pointsA, pointsB;


        [SetUp]
        public void SetUp()
        {
            home = new Point(0, 0);
            pointsA = new Point[]
            {
                new Point(1, 0),
                new Point(1, 1),
                new Point(0, 1)
            };
            pointsB = new Point[]
            {
                new Point(2, 0),
                new Point(2, 2),
                new Point(0, 2)
            };
            chromoA = new RouteChromosome(home, pointsA);
            chromoB = new RouteChromosome(home, pointsB);

            chromosomeList.Add(chromoA);
            chromosomeList.Add(chromoB);

            // TODO: finish setup
            // generation = new Generation<RouteChromosome>(chromosomeList);
        }

        [Test]
        public void Constructor_ValidParameters()
        {
            
        }
    }
}
