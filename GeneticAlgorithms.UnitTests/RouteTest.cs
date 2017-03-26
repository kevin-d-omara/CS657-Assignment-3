using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms.UnitTests
{
    [TestFixture]
    public class RouteTest
    {
        Route nonCircularRoute;
        Route circularRoute;

        Point[] points;
        Point home;

        Point[] singlePoint;
        Point[] emptyPoints;

        [SetUp]
        public void SetUp()
        {
            points = new Point[]
            {
                new Point(1, 0),
                new Point(2, 0),
                new Point(2, 1),
                new Point(1, 1)
            };
            home = new Point(0, 0);

            singlePoint = new Point[] { new Point(0, 1) };
            emptyPoints = new Point[0];
        }

        [Test]
        public void Constructor_NonCircular_ValidParameters()
        {
            // Standard route.
            nonCircularRoute = new Route(points);

            Assert.AreEqual(points, nonCircularRoute.Points);
            Assert.AreEqual(points[0], nonCircularRoute.Points[0]);
            Assert.AreEqual(points[1], nonCircularRoute.Points[1]);
            Assert.AreEqual(points[2], nonCircularRoute.Points[2]);
            Assert.AreEqual(points[3], nonCircularRoute.Points[3]);

            Assert.AreEqual(3, nonCircularRoute.Length);
            Assert.AreEqual(3f, nonCircularRoute.TotalDistance);

            // Single point route.
            var singlePointedRoute = new Route(singlePoint);
            Assert.AreEqual(0, singlePointedRoute.Length);
            Assert.AreEqual(0f, singlePointedRoute.TotalDistance);

            // Zero point route
            var zeroPointRoute = new Route(emptyPoints);
            Assert.AreEqual(0, zeroPointRoute.Length);
            Assert.AreEqual(0f, zeroPointRoute.TotalDistance);
        }

        [Test]
        public void Constructor_Circular_ValidParameters()
        {
            // Standard route.
            circularRoute = new Route(home, points);

            Assert.AreEqual(home, circularRoute.Points[0]);
            Assert.AreEqual(points[0], circularRoute.Points[1]);
            Assert.AreEqual(points[1], circularRoute.Points[2]);
            Assert.AreEqual(points[2], circularRoute.Points[3]);
            Assert.AreEqual(points[3], circularRoute.Points[4]);
            Assert.AreEqual(home, circularRoute.Points[5]);

            Assert.AreEqual(5, circularRoute.Length);
            Assert.That(5.41f, Is.EqualTo(circularRoute.TotalDistance).Within(0.01f));

            // Single point route.
            var singlePointedRoute = new Route(home, singlePoint);
            Assert.AreEqual(2, singlePointedRoute.Length);
            Assert.AreEqual(2f, singlePointedRoute.TotalDistance);

            // Zero point route
            var zeroPointRoute = new Route(home, emptyPoints);
            Assert.AreEqual(1, zeroPointRoute.Length);
            Assert.AreEqual(0f, zeroPointRoute.TotalDistance);
        }
    }
}
