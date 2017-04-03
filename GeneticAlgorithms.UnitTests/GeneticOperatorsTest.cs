using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms.UnitTests
{
    [TestFixture]
    public class GeneticOperatorsTest
    {
        RouteChromosome parent, largeChromo;
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
            parent = new RouteChromosome(home, points);

            largePoints = new Point[]
{
                new Point(1, 0),
                new Point(8, 0),
                new Point(7, 0)
};
            largeChromo = new RouteChromosome(home, largePoints);
        }

        [Test]
        public void Mutate()
        {
            // Hand picked seed such that:
            // parentA = (a, b, c)
            // mutantA = (a, c, b)
            RandomizationProvider.random = new Random(7);
            RouteChromosome mutantA = (RouteChromosome)GeneticOperators.Mutate(parent);

            // Check all genes are still there.
            var genesInA = new HashSet<Gene>(parent.GetGenes());
            foreach (Gene gene in parent.GetGenes())
            {
                Assert.IsTrue(genesInA.Contains(gene));
            }

            // Check that only two points have swapped and correspond to parent.
            var numberOfDifferentGenes = 0;
            var length = parent.Length;
            for (int i = 0; i < length; ++i)
            {
                if (parent.GetGene(i) != mutantA.GetGene(i))
                {
                    ++numberOfDifferentGenes;
                }
            }
            Assert.AreEqual(2, numberOfDifferentGenes);

            // Note: it is possible for the "swapped" genes to have the same index. In this case,
            // Mutate() acts like Clone().
        }

        [Test]
        public void GuidedReverse_OnRight()
        {
            var reversedChromosome = GeneticOperators.GuidedReverse(largeChromo, true);

            Assert.AreEqual(new Point(1, 0), reversedChromosome.GetGene(0).value);
            Assert.AreEqual(new Point(7, 0), reversedChromosome.GetGene(1).value);
            Assert.AreEqual(new Point(8, 0), reversedChromosome.GetGene(2).value);
            Assert.AreEqual(largeChromo.Length, reversedChromosome.Length);
        }

        [Test]
        public void GuidedReverse_OnLeft()
        {
            var reversedChromosome = GeneticOperators.GuidedReverse(largeChromo, false);

            Assert.AreEqual(new Point(1, 0), reversedChromosome.GetGene(0).value);
            Assert.AreEqual(new Point(8, 0), reversedChromosome.GetGene(1).value);
            Assert.AreEqual(new Point(7, 0), reversedChromosome.GetGene(2).value);
            Assert.AreEqual(largeChromo.Length, reversedChromosome.Length);
        }
    }
}
