using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RoutePlanner.Business;
using RoutePlanner.APIInterfaces;
using RoutePlanner.Model;

namespace RoutePlannerTest
{
    [TestClass]
    public class ConsoleIFTest
    {
        private Mock<IRoutePlannerBL> routePlannerMock;
        private APIConsole APIconsole;

        [TestInitialize]
        public void InitializeTest()
        {
            routePlannerMock = new Mock<IRoutePlannerBL>();
            APIconsole = new APIConsole(routePlannerMock.Object);
        }

        [TestMethod]
        public void ResolveQuery_withRoute_routeIsConfigured()
        {
            // Arrange
            Academy academyFrom = new Academy();
            Academy academyTo = new Academy();
            int distance = 0;
            string[] parameters = { "--setup", "A-B", "5" };
            routePlannerMock.Setup(m => m.AddRoute(It.IsAny<Academy>(), It.IsAny<Academy>(), It.IsAny<int>()))
                .Callback<Academy, Academy, int>((a, b, d) => { academyFrom = a; academyTo = b; distance = d; });

            // Action
            APIconsole.ResolveQuery(parameters);

            // Assert
            routePlannerMock.Verify(m => m.AddRoute(It.IsAny<Academy>(), It.IsAny<Academy>(), It.IsAny<int>()), Times.Once());
            Assert.AreEqual("A", academyFrom.Name);
            Assert.AreEqual("B", academyTo.Name);
            Assert.AreEqual(5, distance);
        }

        [TestMethod]
        public void ResolveQuery_QueryDistance_GetDistanceIsCalled()
        {
            // Arrange
            IList<Academy> academies = new List<Academy>();
            string[] parameters = { "--setup", "A-B", "5", "--distance", "A-B" };
            routePlannerMock.Setup(m => m.GetDistance(It.IsAny<IList<Academy>>()))
                .Callback<IList<Academy>>((a) => academies = a);

            // Action
            APIconsole.ResolveQuery(parameters);

            // Assert
            Assert.IsTrue(academies.Count(a => a.Name == "A") == 1);
            Assert.IsTrue(academies.Count(a => a.Name == "B") == 1);
            Assert.IsTrue(academies.Count() == 2);
        }

        delegate void GetRoutesCallback(Academy from, Academy to, int numberOfJumps, ref int routesFound);     // needed for Callback
        delegate int GetRoutesReturns(Academy from, Academy to, int numberOfJumps, ref int routesFound);      // needed for Returns

        [TestMethod]
        public void ResolveQuery_QueryNumberTrips_GetDistanceIsCalled()
        {
            // Arrange
            Academy from = null;
            Academy to = null;
            int jumps = 0;
            string[] parameters = { "--setup", "A-B", "4", "B-C", "3", "A-C", "5", "--numberTrips", "A-C", "2" };
            routePlannerMock.Setup(m => m.GetRoutes(It.IsAny<Academy>(), It.IsAny<Academy>(), It.IsAny<int>(), ref It.Ref<int>.IsAny))
                .Callback(new GetRoutesCallback((Academy a, Academy b, int c, ref int d) => { from = a; to = b; jumps = c; d = 1; }))
                .Returns(new GetRoutesReturns((Academy a, Academy b, int c, ref int d) => { return 1; }));

            // Action
            var result = APIconsole.ResolveQuery(parameters);

            // Assert
            Assert.AreEqual("1", result);
            routePlannerMock.Verify(m => m.GetRoutes(It.IsAny<Academy>(), It.IsAny<Academy>(), It.IsAny<int>(), ref It.Ref<int>.IsAny), Times.Once());
            Assert.AreEqual("A", from.Name);
            Assert.AreEqual("C", to.Name);
            Assert.AreEqual(2, jumps);
        }
    }
}