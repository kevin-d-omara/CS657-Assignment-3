using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms.UnitTests
{
    [TestFixture]
    public class RouteChromosomeTest
    {
        RouteChromosome chromo;
        Point home;
        Point[] genes;

        [SetUp]
        public void SetUp()
        {
            home = new Point(0, 0);
            genes = new Point[]
            {
                new Point(1, 0),
                new Point(1, 1),
                new Point(0, 1)
            };

            chromo = new RouteChromosome(home, genes);
        }

        [Test]
        public void Constructor_ValidParameters()
        {
            Assert.AreEqual(genes, chromo.Genes);
            Assert.AreEqual(home,  chromo.Home);

            Assert.AreEqual(3, chromo.Length);
            Assert.AreEqual(4f, chromo.Fitness);

            Assert.AreEqual(1, chromo.Genes[1].X);
            Assert.AreEqual(1, chromo.Genes[1].Y);

            Assert.AreEqual(home, chromo.Home);

            Assert.AreEqual(1, chromo.Route.Points[2].X);
            Assert.AreEqual(1, chromo.Route.Points[2].Y);
        }
    }
}
