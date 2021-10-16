using NUnit.Framework;
using System.Drawing;
using System;
using Igtampe.Neco.Common.LandView;

namespace Igtampe.Neco.Test.Common {
    public class LandviewTests {

        private Country TestCountry;

        private Road TestRoad;

        private Plot TestPlot;
        private readonly int TestPlotArea = 40500;

        private District Rectangle;
        private readonly int RectangleArea = 240000;

        private District Parallelogram;
        private readonly int ParallelogramArea = 249375;

        private Plot A1;
        private readonly int A1Area = 5625;

        private Plot A2;
        private readonly int A2Area = 70;

        [SetUp]
        public void Setup() {
            TestCountry = new() { Width = 1500, Height = 1500, Districts = new(), Name = "Test Country" };
            TestRoad = new() { Points = "0,0;0,50;50,50" };

            A1 = new() {
                ID = Guid.NewGuid(),
                Name = "A1",
                Points = "50,50;100,5;200,140",
            };

            A2 = new() {
                ID = Guid.NewGuid(),
                Name = "A2",
                Points = @"50,-180;
                          0,-1;
                          -90,-90;
                          -0,0"
            };

            Rectangle = new() {
                ID = Guid.NewGuid(),
                Name ="Rectangle",
                Points = @"-400,-200;
                            200,-200;
                            200, 200;
                           -400, 200",
                Plots= new()
            };

            A1.District = Rectangle;
            A2.District = Rectangle;

            Rectangle.Plots.Add(A1);
            Rectangle.Plots.Add(A2);

            TestPlot = new() {
                ID = Guid.NewGuid(),
                Name ="Test Plot",
                Points = @"300,100;
                           400,-500;
                           435,-10;
                           400,100"
            };

            Parallelogram = new() {
                ID = Guid.NewGuid(),
                Name = "Parallelogram",
                Points = @"215,100;
                          500,500;
                          500,-500;
                          215,-650",
                Plots = new()
            };

            TestPlot.District = Parallelogram;
            Parallelogram.Plots.Add(TestPlot);

            Rectangle.Country = TestCountry;
            TestCountry.Districts.Add(Rectangle);
            Parallelogram.Country = TestCountry;
            TestCountry.Districts.Add(Parallelogram);


        }

        [Test]
        public void TestCoutnryPoitns() {
            Point[] Points = TestCountry.GraphicalPoints;
            Assert.AreEqual(4, Points.Length, "Incorrect amount of points generated");
        }

        [Test]
        public void TestRoadPoitns() {
            string Point = TestRoad.Points.Replace("\n", "").Replace(" ", "").Replace("\r", "");
            var GraphicalPoints = TestRoad.GraphicalPoints;
            TestRoad.GraphicalPoints = GraphicalPoints;
            Assert.AreEqual(Point, TestRoad.Points, "Point conversion failed somewhere");
        }

        [Test]
        public void TestDistrictPoitns() {
            string Point = Parallelogram.Points.Replace("\n", "").Replace(" ", "").Replace("\r", "");
            var GraphicalPoints = Parallelogram.GraphicalPoints;
            Parallelogram.GraphicalPoints = GraphicalPoints;
            Assert.AreEqual(Point, Parallelogram.Points, "Point conversion failed somewhere");
            Parallelogram.Points = Point;
        }

        [Test]
        public void TestPlotPoitns() {
            string Point = A1.Points.Replace("\n", "").Replace(" ", "").Replace("\r", "");
            var GraphicalPoints = A1.GraphicalPoints;
            A1.GraphicalPoints = GraphicalPoints;
            Assert.AreEqual(Point, A1.Points, "Point conversion failed somewhere");
            A1.Points = Point;
        }

        [Test]
        public void TestDistrictContains() {
            var Calculated1 = LandViewUtils.CalculatePlotDistrict(TestCountry, A1);
            var Calculated2 = LandViewUtils.CalculatePlotDistrict(TestCountry, TestPlot);
            Assert.AreEqual(Rectangle, Calculated1, "District was calculated to be in the wrong district");
            Assert.AreEqual(Parallelogram, Calculated2, "District was calculated to be in the wrong district");
        }

        [Test]
        public void TestPlotColides() {
            Plot NoColide = new() {
                District = Parallelogram,
                Points = @"299,100;
                          300,50;
                          250,50;
                          225,90"
            };

            Plot Within = new() {
                District = Parallelogram,
                Points = @"399,10;
                           350,50;
                           350,50;
                           325,90"
            };

            Plot OnePoint = new() {
                District = Parallelogram,
                Points = @"299,100;
                           350,50;
                           250,50;
                           225,90"
            };

            Plot LineIntersect = new() {
                District = Parallelogram,
                Points = @"399,150;
                           300,50;
                           250,50;
                           225,90"
            };


            var NoColideResult = LandViewUtils.GetIntersectingPlot(NoColide);
            var WithinResult = LandViewUtils.GetIntersectingPlot(Within);
            var OnePointResult = LandViewUtils.GetIntersectingPlot(OnePoint);
            var LineIntersectResult = LandViewUtils.GetIntersectingPlot(LineIntersect);

            Assert.AreEqual(null, NoColideResult, "Plot with no collisions was found to collide with something");
            Assert.AreEqual(TestPlot, WithinResult, "Plot Entirely within TestPlot was found to not collide with it");
            Assert.AreEqual(TestPlot, OnePointResult, "Plot one point within TestPlot was found to not collide with it");
            Assert.AreEqual(TestPlot, LineIntersectResult, "Plot with a line crossing into TestPlot was found to not collide with it");
        }


        [Test]
        public void TestPlotAreaCalc() {
            Assert.AreEqual(A1Area, A1.Area(), "Area miscalculated for A1");
            Assert.AreEqual(A2Area, A2.Area(), "Area miscalculated for A2");
            Assert.AreEqual(TestPlotArea, TestPlot.Area(), "Area miscalculated for Testplot");
        }

        [Test]
        public void TestDistrictAreaCalc() {
            Assert.AreEqual(RectangleArea, Rectangle.Area(), "Area miscalculated for Rectangle");
            Assert.AreEqual(ParallelogramArea, Parallelogram.Area(), "Area miscalculated for Parallelogram");
        }

    }
}
