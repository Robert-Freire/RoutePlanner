using System.Collections.Generic;
using RoutePlanner.Model;

namespace RoutePlanner.Business
{
    public class RoutePlannerBL : IRoutePlannerBL
    {
        private Graph<Academy> graph = new Graph<Academy>();
        public void AddRoute(Academy from, Academy to, int distance)
        {
            graph.AddConnection(new Node<Academy>(from, from.Name),
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
            var origin = graph.NodeDictionary[from.Name];
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
            return graph.ShortestRoute(new Node<Academy>(from, from.Name), new Node<Academy>(to, to.Name));

        }
        private int GetDistance(Academy from, Academy to)
        {
            return graph.GetConnection(new Node<Academy>(from, from.Name),
                                        new Node<Academy>(to, to.Name));
        }
    }
}