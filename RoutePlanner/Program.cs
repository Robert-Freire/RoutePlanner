using System;
using RoutePlanner.APIInterfaces;
using RoutePlanner.Business;

namespace RoutePlanner
{
    class Program
    {
        static void Main(string[] args)
        {
            var console = new APIConsole(new RoutePlannerBL());
            var respose = console.ResolveQuery(args);
            Console.WriteLine(respose);
        }
    }
}
