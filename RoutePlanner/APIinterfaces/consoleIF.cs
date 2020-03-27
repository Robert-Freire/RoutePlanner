using System;
using System.Linq;
using RoutePlanner.Business;
using RoutePlanner.Model;

namespace RoutePlanner.APIInterfaces
{
    public class APIConsole
    {
        private IRoutePlannerBL routePlanner;
        public APIConsole(IRoutePlannerBL routePlanner)
        {
            this.routePlanner = routePlanner;
        }
        private const string PARAMETERS_DESCRIPTION =
            @"RoutePlanerr: error. CheckParameters
            Usage: RoutePlanner --setup <list_of_Routes> [command] [arguments]
            <list_ofRoutes> Ej. A-B 4 B-C 6
            Commands
            --distance <route>. Returns the distance of a route Ej. --distance A-B-C
            --numberTrips <route> <stops>. Returns the number of routes between two academies with n stops. Ej --numberTrips A-C 4
            --shortestRoute <route>. Returns the shortest route between two points. Ej. --shortestRoute A-C";
        private const string NO_SUCH_ROUTE = "NO SUCH ROUTE";
        public string ResolveQuery(string[] args)
        {

            if (args.Length < 3) return PARAMETERS_DESCRIPTION;

            try
            {
                var paramsQuery = SetupRoutePlanner(args);
                return RunQuery(paramsQuery);

            }
            catch (System.Exception)
            {
                return PARAMETERS_DESCRIPTION;
            }
        }

        private string RunQuery(string[] paramsQuery)
        {
            if (paramsQuery.Length < 1)
            {
                return PARAMETERS_DESCRIPTION;
            }
            switch (paramsQuery[0].ToLower())
            {
                case "--distance":
                    return QueryDistance(paramsQuery);
                case "--numbertrips":
                    return QueryNumberTrips(paramsQuery);
                case "--shortestroute":
                     return QueryShortestRoute(paramsQuery);
                default:
                    return PARAMETERS_DESCRIPTION;

            }
        }

        private string QueryShortestRoute(string[] paramsQuery)
        {
            var route = this.GetRoute(paramsQuery[1]);
            if (route.Length != 2) return PARAMETERS_DESCRIPTION;
            
            var distance = routePlanner.ShortestRoute(new Academy() { Name = route[0] }, new Academy() { Name = route[1] });

            if (distance == int.MaxValue)
                return NO_SUCH_ROUTE;
                
            return distance.ToString();
        }

        private string QueryNumberTrips(string[] paramsQuery)
        {
            var route = this.GetRoute(paramsQuery[1]);
            if (route.Length != 2) return PARAMETERS_DESCRIPTION;
            int jumps;
            if (!int.TryParse(paramsQuery[2], out jumps)) return PARAMETERS_DESCRIPTION;

            int numRoutes = 0;
            routePlanner.GetRoutes(new Academy() { Name = route[0] }, new Academy() { Name = route[1] }, jumps, ref numRoutes);

            return numRoutes.ToString();
        }

        private string QueryDistance(string[] paramsQuery)
        {
            var route = this.GetRoute(paramsQuery[1]);
            if (route.Length < 2) return PARAMETERS_DESCRIPTION;

            var distance = routePlanner.GetDistance(route.Select(r => new Academy { Name = r }).ToList());

            if (distance < 0) return NO_SUCH_ROUTE;
            return distance.ToString();
        }

        private string[] SetupRoutePlanner(string[] args)
        {
            if (args[1].ToUpper() != "--setup".ToUpper())
            {
                for (var i = 1; i + 1 < args.Length; i = i + 2)
                {
                    SetupRoute(args[i], args[i + 1]);
                    if (args[i + 2].StartsWith("--"))
                    {
                        var length = args.Length - i - 2;
                        string[] command = new string[length]; ;
                        Array.Copy(args, i + 2, command, 0, length);
                        return command;
                    }
                }
            }
            throw new Exception("Incorrect Parameters");
        }

        private void SetupRoute(string route, string distance)
        {
            var academies = GetRoute(route);
            if (academies.Length != 2)
            {
                throw new Exception("Incorrect Parameters");
            }
            int weight;
            if (!int.TryParse(distance, out weight))
            {
                throw new Exception("Incorrect Parameters");
            }

            routePlanner.AddRoute(new Academy { Name = academies[0] }, new Academy { Name = academies[1] }, weight);
        }

        private string[] GetRoute(string route)
        {
            return route.Split('-');
        }
    }
};