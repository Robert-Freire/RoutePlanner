using System.Collections.Generic;
using RoutePlanner.Business.Graph;
using RoutePlanner.Model;

namespace RoutePlanner.Business
{
    public class RoutePlannerBL : IRoutePlannerBL
    {
        private IGraph<Academy> Graph;
        public RoutePlannerBL(IGraph<Academy> graph)
        {
            Graph = graph;
        }
        public void AddRoute(Academy from, Academy to, int distance)
        {
            Graph.AddConnection(new Node<Academy>(from, from.Name),
                                new Node<Academy>(to, to.Name),
                                distance);
        }

        public int GetDistance(IList<Academy> routes)
        {
            var distance = 0;
            for (var i = 0; i < routes.Count - 1; i++)
            {
                var step = GetDistance(routes[i], routes[i + 1]);
                if (step < 0)
                    return step;
                distance += step;
            }
            return distance;
        }
        public int GetRoutes(Academy from, Academy to, int numberOfJumps, ref int routesFound)
        {
            var origin = Graph.NodeDictionary[from.Name];
            if (numberOfJumps == 1)
            {
                foreach (var dest in origin.Neighbors)
                {
                    if (dest.Value.Name == to.Name)
                    {
                        return ++routesFound;
                    }
                }
            }
            else if (numberOfJumps > 1)
            {
                foreach (var dest in origin.Neighbors)
                {
                    GetRoutes(dest.Value, to, numberOfJumps - 1, ref routesFound);
                }
            }

            return routesFound;
        }
        public int ShortestRoute(Academy from, Academy to)
        {
            return Graph.ShortestRoute(new Node<Academy>(from, from.Name), new Node<Academy>(to, to.Name));

        }
        private int GetDistance(Academy from, Academy to)
        {
            return Graph.GetConnection(new Node<Academy>(from, from.Name),
                                        new Node<Academy>(to, to.Name));
        }
    }
}