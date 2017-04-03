using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms.UnitTests
{
    [TestFixture]
    public class RouteChromosomeTest
    {
        RouteChromosome chromo, largeChromo;
        Point home;
        Point[] points, largePoints;

        [SetUp]
        public void SetUp()
        {
            home = new Point(0, 0);
            points = new Point[]
            {
                new Point(1, 0),
                new Point(1, 1),
                new Point(0, 1)
            };

            chromo = new RouteChromosome(home, points);

            largePoints = new Point[]
            {
                new Point(1, 0),
                new Point(8, 0),
                new Point(7, 0)
            };
            largeChromo = new RouteChromosome(home, largePoints);
        }

        [Test]
        public void Constructor_ValidParameters()
        {
            Assert.AreEqual(home,  chromo.Home);

            Assert.AreEqual(3, chromo.Length);
            Assert.AreEqual(-4f, chromo.Fitness);

            var point = (Point)(chromo.GetGene(1).value);
            Assert.AreEqual(1, point.x);
            Assert.AreEqual(1, point.y);

            Assert.AreEqual(home, chromo.Home);

            Assert.AreEqual(1, chromo.Route.Points[2].x);
            Assert.AreEqual(1, chromo.Route.Points[2].y);
        }

        [Test]
        public void GetIndexOfLargestChange()
        {
            Assert.IsTrue(0 == largeChromo.GetIndexOfLargestChange());
        }

        [Test]
        public void ReverseGenes_OnRight()
        {
            var reversedChromosome = largeChromo.Clone();
            reversedChromosome.ReverseGenes(1, true);

            Assert.AreEqual(new Point(1, 0), reversedChromosome.GetGene(0).value);
            Assert.AreEqual(new Point(7, 0), reversedChromosome.GetGene(1).value);
            Assert.AreEqual(new Point(8, 0), reversedChromosome.GetGene(2).value);
            Assert.AreEqual(largeChromo.Length, reversedChromosome.Length);
        }

        [Test]
        public void ReverseGenes_OnLeft()
        {
            var reversedChromosome = largeChromo.Clone();
            reversedChromosome.ReverseGenes(1, false);
            
            Assert.AreEqual(new Point(8, 0), reversedChromosome.GetGene(0).value);
            Assert.AreEqual(new Point(1, 0), reversedChromosome.GetGene(1).value);
            Assert.AreEqual(new Point(7, 0), reversedChromosome.GetGene(2).value);
            Assert.AreEqual(largeChromo.Length, reversedChromosome.Length);
        }
    }
}
