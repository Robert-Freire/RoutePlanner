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

        [TestMethod]
        public void ShortestRoute_fromAtoC_4IsReturned()
        {
            // Arrange
            var graph = new Graph<string>();
            var pointA = "A";
            var nodeA = new Node<string>(pointA, pointA);
            var pointB = "B";
            var nodeB = new Node<string>(pointB, pointB);
            var pointC = "C";
            var nodeC = new Node<string>(pointC, pointC);

            graph.AddConnection(nodeA, nodeB, 2);
            graph.AddConnection(nodeB, nodeC, 2);
            graph.AddConnection(nodeA, nodeC, 8);

            // Action
            var distance = graph.ShortestRoute(nodeA, nodeC);

            // Assert
            Assert.AreEqual(4, distance);
        }
    }
}
