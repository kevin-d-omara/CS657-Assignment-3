using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms.UnitTests
{
    [TestFixture]
    public class GeneticOperatorsTest
    {
        RouteChromosome parentA;
        RouteChromosome parentB;
        Point homeA;
        Point homeB;
        Point[] genesA;
        Point[] genesB;

        [SetUp]
        public void SetUp()
        {
            homeA = new Point(0, 0);
            homeB = new Point(2, 0);
            genesA = new Point[]
            {
                new Point(1, 0),
                new Point(1, 1),
                new Point(0, 1)
            };
            genesB = new Point[]
            {
                new Point(4, 0),
                new Point(4, 2),
                new Point(2, 2)
            };

            parentA = new RouteChromosome(homeA, genesA);
            parentB = new RouteChromosome(homeB, genesB);
        }

        [Test]
        public void Clone()
        {
            var cloneA = GeneticOperators.Clone(parentA);

            // Modify original chromosome, but clone is untouched.
            var genesA2 = (Point[])genesA.Clone();
            genesA2[0] = new Point(7, 7);
            parentA = new RouteChromosome(homeA, genesA2);

            Assert.AreNotEqual(parentA.Genes[0], cloneA.Genes[0]);
            Assert.AreNotEqual(parentA.Fitness, cloneA.Fitness);
        }

        [Test]
        public void Mutate()
        {
            // TODO
        }
    }
}
