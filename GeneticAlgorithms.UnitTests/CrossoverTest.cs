using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms.UnitTests
{
    [TestFixture]
    public class CrossoverTest
    {
        RouteChromosome parentA, parentB;
        RouteChromosome childA , childB;
        Point           homeA  , homeB;
        Point[]         genesA , genesB;

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

            // Hand picked seed such that:
            // parentA = (a, b, c)  parentB = (e, f, g)
            // childA  = (a, f, g)  childB  = (e, b, c)
            //               ^   breaking point   ^
            RandomizationProvider.random = new Random(1);
            GeneticOperators.Crossover(parentA, parentB, out childA, out childB);
        }

        [Test]
        public void Crossover_ThrowsException()
        {
            Point[] emptyPoints = new Point[0];
            RouteChromosome shortRoute = new RouteChromosome(homeA, emptyPoints);

            // Unequal lengths.
            Assert.That(() =>
                GeneticOperators.Crossover(parentA, shortRoute, out childA, out childB),
                Throws.ArgumentException);

            // Too short.
            RouteChromosome onePointRoute = new RouteChromosome(homeA, new Point[1] { homeB });

            Assert.That(() =>
                GeneticOperators.Crossover(onePointRoute, onePointRoute, out childA, out childB),
                Throws.ArgumentException);
            Assert.That(() =>
                GeneticOperators.Crossover(shortRoute, shortRoute, out childA, out childB),
                Throws.ArgumentException);
        }

        [Test]
        public void Crossover_BreakingPoints_AreEqual()
        {
            var indexA = GetBreakingPoint(parentA, childA);
            var indexB = GetBreakingPoint(parentB, childB);

            Assert.AreEqual(indexA, indexB);
        }

        [Test]
        public void Crossover_BreakingPoint_WithinBounds()
        {
            var index = GetBreakingPoint(parentA, childA);

            Assert.IsTrue(index >= 0);
            Assert.IsTrue(index < parentA.Length);
        }

        [Test]
        public void Crossover_SwappedParts_Match()
        {
            var index = GetBreakingPoint(parentA, childA);

            // parentA = (a, b, c)  parentB = (e, f, g)
            // childA  = (a, f, g)  childB  = (e, b, c)
            //               ^   breaking point   ^
            var childA_R = childA.Genes.Skip(index);   // f, g
            var parentB_R = parentB.Genes.Skip(index); // f, g

            var childB_R = childB.Genes.Skip(index);   // b, c
            var parentA_R = parentA.Genes.Skip(index); // b, c

            Assert.IsTrue(childA_R.SequenceEqual(parentB_R)); // { f, g } == { f, g }
            Assert.IsTrue(childB_R.SequenceEqual(parentA_R)); // { b, c } == { b , c}

        }

        private int GetBreakingPoint(RouteChromosome parent, RouteChromosome child)
        {
            var length = parent.Genes.Length;
            for (int i = 0; i < length; ++i)
            {
                if (parent.Genes[i] != child.Genes[i])
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
