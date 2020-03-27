using System.Collections.Generic;
using RoutePlanner.Model;

namespace RoutePlanner.Business
{
    public interface IRoutePlannerBL
    {
        public void AddRoute(Academy from, Academy to, int distance);
        public int GetDistance(IList<Academy> routes);
        public int GetRoutes(Academy from, Academy to, int numberOfJumps, ref int routesFound);
        public int ShortestRoute(Academy from, Academy to);
    }
}