using System;
using RoutePlanner.APIInterfaces;
using RoutePlanner.Business;
using RoutePlanner.Business.Graph;
using RoutePlanner.Model;

namespace RoutePlanner
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = new Graph<Academy>();
            var routePlanner = new RoutePlannerBL(graph);
            var console = new APIConsole(routePlanner);
            var respose = console.ResolveQuery(args);
            Console.WriteLine(respose);
        }
    }
}
