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
        RouteChromosome parent;
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
            parent = new RouteChromosome(home, genes);
        }

        [Test]
        public void Clone()
        {
            var cloneA = GeneticOperators.Clone(parent);

            // Modify original chromosome, but clone is untouched.
            parent.Genes[0] = new Point(7, 7);
            Assert.AreNotEqual(parent.Genes[0], cloneA.Genes[0]);

            // Note: Direct modification of Chromosome.Genes, .Route, etc. is not supported. Thus,
            // parentA.Fitness is NOT updated to reflect the change of parentA.Genes[0].
            //Assert.AreNotEqual(parentA.Fitness, cloneA.Fitness);
        }

        [Test]
        public void Mutate()
        {
            // Hand picked seed such that:
            // parentA = (a, b, c)
            // mutantA = (a, c, b)
            RandomizationProvider.random = new Random(7);
            RouteChromosome mutantA = GeneticOperators.Mutate(parent);

            // Check all points are still there.
            var genesInA = new HashSet<Point>(parent.Genes);
            foreach (Point gene in parent.Genes)
            {
                Assert.IsTrue(genesInA.Contains(gene));
            }

            // Check that only two points have swapped and correspond to parent.
            var numberOfDifferentGenes = 0;
            var length = parent.Genes.Length;
            for (int i = 0; i < length; ++i)
            {
                if (parent.Genes[i] != mutantA.Genes[i])
                {
                    ++numberOfDifferentGenes;
                }
            }
            Assert.AreEqual(2, numberOfDifferentGenes);

            // Note: it is possible for the "swapped" genes to have the same index. In this case,
            // Mutate() acts like Clone().
        }
    }
}
