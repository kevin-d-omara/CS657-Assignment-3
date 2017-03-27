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
            Assert.AreEqual(5, A.x);
            Assert.AreEqual(5, A.y);

            Assert.AreEqual(10, B.x);
            Assert.AreEqual(15, B.y);

            Assert.AreEqual(-7, C.x);
            Assert.AreEqual( 2, C.y);
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

        [Test]
        public void Operator_EqualSign()
        {
            // Check condition 1.
            // Both are null.
            Point null1 = null;
            Point null2 = null;
            Assert.IsTrue(null1 == null2);

            Point A2 = A;
            Assert.IsTrue(A == A2);

            // Check condition 2.
            // Same instance.
            Assert.IsFalse(A == null1);
            Assert.IsFalse(null1 == A);

            // Check condition 3.
            Point D = new Point(A.x, -1);
            Point E = new Point(-1, A.y);
            Point F = new Point(A.x, A.y);
            Assert.IsFalse(A == B);
            Assert.IsFalse(A == D);
            Assert.IsFalse(A == E);
            Assert.IsTrue(A == F);

            Assert.IsFalse(B == A);
            Assert.IsFalse(D == A);
            Assert.IsFalse(E == A);
            Assert.IsTrue(F == A);
        }

        [Test]
        public void Operator_NotEqualSign()
        {
            // Check condition 1
            // Both are null.
            Point null1 = null;
            Point null2 = null;
            Assert.IsFalse(null1 != null2);

            // Same instance.
            Point A2 = A;
            Assert.IsFalse(A != A2);

            // Check condition 2.
            Assert.IsTrue(A != null1);
            Assert.IsTrue(null1 != A);

            // Check condition 3.
            Point D = new Point(A.x, -1);
            Point E = new Point(-1, A.y);
            Point F = new Point(A.x, A.y);
            Assert.IsTrue(A != B);
            Assert.IsTrue(A != D);
            Assert.IsTrue(A != E);
            Assert.IsFalse(A != F);

            Assert.IsTrue(B != A);
            Assert.IsTrue(D != A);
            Assert.IsTrue(E != A);
            Assert.IsFalse(F != A);
        }

        [Test]
        public void Operator_EqualsPoint()
        {
            // Check condition 1.
            // Both are null.
            Point A2 = A;
            Assert.IsTrue(A.Equals(A2));

            // Check condition 2.
            // Same instance.
            Point null1 = null;
            Assert.IsFalse(A.Equals(null1));

            // Check condition 3.
            Point D = new Point(A.x, -1);
            Point E = new Point(-1, A.y);
            Point F = new Point(A.x, A.y);
            Assert.IsFalse(A.Equals(B));
            Assert.IsFalse(A.Equals(D));
            Assert.IsFalse(A.Equals(E));
            Assert.IsTrue(A.Equals(F));

            Assert.IsFalse(B.Equals(A));
            Assert.IsFalse(D.Equals(A));
            Assert.IsFalse(E.Equals(A));
            Assert.IsTrue(F.Equals(A));
        }

        [Test]
        public void Operator_EqualsObject()
        {
            // Check condition 1.a
            Object nullObject = null;
            Assert.IsFalse(A.Equals(nullObject));

            // Check condition 1.b
            Point nullPoint = null;
            Assert.IsFalse(A.Equals((Object)nullPoint));

            NotPoint notPoint = new NotPoint();
            Assert.IsFalse(A.Equals(notPoint));

            // Check condition 2
            Point D = new Point(5, 5);
            Assert.IsTrue(A.Equals((Object)D));
            Assert.IsFalse(A.Equals((Object)B));
        }
        private class NotPoint { }

        [Test]
        public void Operator_GetHashCode()
        {
            Assert.IsTrue(A.GetHashCode() == A.GetHashCode());
            Assert.IsTrue(A.GetHashCode() != B.GetHashCode());

            Point A2 = new Point(5, 5);
            Assert.IsTrue(A.GetHashCode() == A2.GetHashCode());
        }
    }
}
