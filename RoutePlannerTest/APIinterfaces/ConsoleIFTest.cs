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
    }
}