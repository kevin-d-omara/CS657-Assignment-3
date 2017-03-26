using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms.UnitTests
{
    [TestFixture]
    public class PointTest
    {
        Point A;
        Point B;
        Point C;

        [SetUp]
        public void SetUp()
        {
            A = new Point(5, 5);
            B = new Point(10, 15);
            C = new Point(-7, 2);
        }

        [Test]
        public void Constructor_ValidParameters()
        {
            Assert.AreEqual(5, A.X);
            Assert.AreEqual(5, A.Y);

            Assert.AreEqual(10, B.X);
            Assert.AreEqual(15, B.Y);

            Assert.AreEqual(-7, C.X);
            Assert.AreEqual( 2, C.Y);
        }

        [Test]
        public void Distance_BothIn_FirstQuadrant()
        {
            var distanceAB = Point.Distance(A, B);
            var distanceBA = Point.Distance(B, A);

            Assert.IsTrue(distanceAB == distanceBA);
            Assert.That(11.18f, Is.EqualTo(distanceAB).Within(.05f));
            Assert.That(11.18f, Is.EqualTo(distanceBA).Within(.05f));
        }

        [Test]
        public void Distance_BothIn_SeparateQuadrant()
        {
            var distanceAC = Point.Distance(A, C);
            var distanceCA = Point.Distance(C, A);

            Assert.IsTrue(distanceAC == distanceCA);
            Assert.That(12.36f, Is.EqualTo(distanceAC).Within(.05f));
            Assert.That(12.36f, Is.EqualTo(distanceCA).Within(.05f));
        }
    }
}
