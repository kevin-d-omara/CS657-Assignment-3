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
        Point[]         pointsA , pointsB;

        [SetUp]
        public void SetUp()
        {
            homeA = new Point(0, 0);
            homeB = new Point(2, 0);
            pointsA = new Point[]
            {
                new Point(1, 0),
                new Point(1, 1),
                new Point(0, 1)
            };
            pointsB = new Point[]
            {
                new Point(4, 0),
                new Point(4, 2),
                new Point(2, 2)
            };

            parentA = new RouteChromosome(homeA, pointsA);
            parentB = new RouteChromosome(homeB, pointsB);

            // Hand picked seed such that:
            // parentA = (a, b, c)  parentB = (e, f, g)
            // childA  = (a, f, g)  childB  = (e, b, c)
            //               ^   breaking point   ^
            RandomizationProvider.random = new Random(1);
            CrossoverConcrete(parentA, parentB, out childA, out childB);
        }

        [Test]
        public void Crossover_ThrowsException()
        {
            Point[] emptyPoints = new Point[0];
            RouteChromosome shortRoute = new RouteChromosome(homeA, emptyPoints);

            // Unequal lengths.
            Assert.That(() =>
                CrossoverConcrete(parentA, shortRoute, out childA, out childB),
                Throws.ArgumentException);

            // Too short.
            RouteChromosome onePointRoute = new RouteChromosome(homeA, new Point[1] { homeB });

            Assert.That(() =>
                CrossoverConcrete(onePointRoute, onePointRoute, out childA, out childB),
                Throws.ArgumentException);
            Assert.That(() =>
                CrossoverConcrete(shortRoute, shortRoute, out childA, out childB),
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
            var childA_R = childA.GetGenes().Skip(index);   // f, g
            var parentB_R = parentB.GetGenes().Skip(index); // f, g

            var childB_R = childB.GetGenes().Skip(index);   // b, c
            var parentA_R = parentA.GetGenes().Skip(index); // b, c

            Assert.IsTrue(childA_R.SequenceEqual(parentB_R)); // { f, g } == { f, g }
            Assert.IsTrue(childB_R.SequenceEqual(parentA_R)); // { b, c } == { b , c}

        }

        private int GetBreakingPoint(RouteChromosome parent, RouteChromosome child)
        {
            var length = parent.Length;
            for (int i = 0; i < length; ++i)
            {
                if (parent.GetGene(i) != child.GetGene(i))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// A wrapper for GeneticAlgorithms.Crossover(). Boxes and unboxes children so that the
        /// function may be called.
        /// </summary>
        private void CrossoverConcrete<T>(T parentA, T parentB, out T childA, out T childB) where T : IChromosome
        {
            IChromosome tmpChildA, tmpChildB;
            GeneticOperators.Crossover(parentA, parentB, out tmpChildA, out tmpChildB);
            childA = (T)tmpChildA;
            childB = (T)tmpChildB;
        }
    }
}
