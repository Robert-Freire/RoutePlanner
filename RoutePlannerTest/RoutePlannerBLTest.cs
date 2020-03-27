using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoutePlanner.Business;
using RoutePlanner.Model;

namespace RoutePlannerTest
{
    [TestClass]
    public class RoutePlannerBLTest
    {
        private Academy academyA = new Academy { Name = "A" };
        private Academy academyB = new Academy { Name = "B" };
        private Academy academyC = new Academy { Name = "C" };
        private Academy academyD = new Academy { Name = "D" };
        private Academy academyE = new Academy { Name = "E" };

        [TestMethod]
        public void GetDistance_RouteABC_9IsReturned()
        {
            // Arrange
            var routePlanner = new RoutePlannerBL();
            routePlanner.AddRoute(academyA, academyB, 5);
            routePlanner.AddRoute(academyB, academyC, 4);
            var routeQuery = new List<Academy>() { academyA, academyB, academyC };

            // Action
            var distance = routePlanner.GetDistance(routeQuery);

            // Assert
            Assert.AreEqual(9, distance);
        }

        [TestMethod]
        public void GetDistance_RouteAEBCD_22Returned()
        {
            // Arrange
            var routePlanner = DefaultPlanerSetup();
            var routeQuery = new List<Academy>() { academyA, academyE, academyB, academyC, academyD };

            // Action
            var distance = routePlanner.GetDistance(routeQuery);

            // Assert
            Assert.AreEqual(22, distance);
        }

        [TestMethod]
        public void GetDistance_RouteAED_NotFoundValueIsReturned()
        {
            // Arrange
            var routePlanner = DefaultPlanerSetup();
            var routeQuery = new List<Academy>() { academyA, academyE, academyD };

            // Action
            var distance = routePlanner.GetDistance(routeQuery);

            // Assert
            Assert.AreEqual(-1, distance);
        }


        [TestMethod]
        public void getRoutes_FromCtoCinMax3Stops_2RoutesFound()
        {
            // Arrange
            var routePlanner = DefaultPlanerSetup();

            // Action
            var numRoutes = 0;
            routePlanner.GetRoutes(academyC, academyC, 3, ref numRoutes);
            routePlanner.GetRoutes(academyC, academyC, 2, ref numRoutes);

            // Assert
            Assert.AreEqual(2, numRoutes);
        }

        [TestMethod]
        public void GetRoutes_FromAtoCin4Stops_3RoutesFound()
        {
            // Arrange
            var routePlanner = DefaultPlanerSetup();

            // Action
            var numRoutes = 0;
            routePlanner.GetRoutes(academyA, academyC, 4, ref numRoutes);

            // Assert
            Assert.AreEqual(3, numRoutes);
        }

        [TestMethod]
        public void ShortestDistance_FromAtoC_9IsReturned()
        {
            // Arrange
            var routePlanner = DefaultPlanerSetup();

            // Action
            var distance = routePlanner.ShortestRoute(academyA, academyC);

            // Assert
            Assert.AreEqual(9, distance);
        }
        private RoutePlannerBL DefaultPlanerSetup()
        {
            var routePlanner = new RoutePlannerBL();

            routePlanner.AddRoute(academyA, academyB, 5);
            routePlanner.AddRoute(academyB, academyC, 4);
            routePlanner.AddRoute(academyC, academyD, 8);
            routePlanner.AddRoute(academyD, academyC, 8);
            routePlanner.AddRoute(academyD, academyE, 6);
            routePlanner.AddRoute(academyA, academyD, 5);
            routePlanner.AddRoute(academyC, academyE, 2);
            routePlanner.AddRoute(academyE, academyB, 3);
            routePlanner.AddRoute(academyA, academyE, 7);

            return routePlanner;
        }
    }
}
