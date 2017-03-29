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
        Point[] points;

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
    }
}
