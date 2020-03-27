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
    //        args = "--setup A-B 2 B-C 4 D-C 8 D-E 6 A-D 5 C-E 2 E-B 3 A-E 7 --distance A-B-C".Split(' ');
            var respose = console.ResolveQuery(args);
            Console.WriteLine(respose);
        }
    }
}
