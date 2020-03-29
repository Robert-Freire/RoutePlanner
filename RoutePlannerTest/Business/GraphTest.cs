using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoutePlanner.Model;

namespace RoutePlannerTest
{
    [TestClass]
    public class GraphTest
    {
        [TestMethod]
        public void AddNode_elementIsAdded_elementIsSetup()
        {
            // Arrange
            var graph = new Graph<Academy>();
            var academyA = new Academy { Name = "A" };

            // Action
            graph.AddNode(new Node<Academy>(academyA, academyA.Name));

            // Assert
            Assert.AreEqual(academyA.Name, graph.NodeDictionary[academyA.Name].Value.Name);
        }

        [TestMethod]
        public void AddConnection_routeIsAdded_connectionIsSetup()
        {
            // Arrange
            var graph = new Graph<Academy>();
            var academyA = new Academy { Name = "A" };
            var academyB = new Academy { Name = "B" };
            var distance = 25;

            // Action
            graph.AddConnection(new Node<Academy>(academyA, academyA.Name),
                                new Node<Academy>(academyB, academyB.Name),
                                distance);

            // Assert
            var result = graph.GetConnection(new Node<Academy>(academyA, academyA.Name),
                                            new Node<Academy>(academyB, academyB.Name));
            Assert.AreEqual(distance, result);
        }
    }
}
