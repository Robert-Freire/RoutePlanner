# RoutePlanner

This is a command-line application that provide us with information about routes between academies.

## Getting Started

### Prerequisites

Download and install the [.NET Core SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) on your computer.

### Installing

To build the application go to the folder of the solution an execute.

```console
cd RoutePlanner
dotnet build
```

### Running the application

#### Console arguments
To execute the application you must supply the configuration parameters and the query to execute

--setup *list_of_Routes* [command] [arguments]\
*list_of_Routes* list of pair *route* distance. You can enter so many routes as you want, but the router are all in the format from-to e.g. A-B 4 B-C 6\
*route* a route is a set of academies separated by - e.g. A-B-C is the route that goes from A to C passing by B.

The application supports these commands\
--distance *route*. Returns the distance of a route e.g. --distance A-B-C\
--numberTrips *route* *stops*. Returns the number of routes between two academies with n stops. Ej --numberTrips A-C 4\
--shortestRoute *route*. Returns the shortest route between two points. e.g. --shortestRoute A-C\


### Execution

There are several ways to execute the application. You can 
* Go to the folder RoutePlanner inside the solution and execute dotnet run --setup *list_of_Routes* [command] [arguments]. For example

```console
cd RoutePlanner
dotnet run --setup A-B 5 B-C 4 C-D 8 D-C 8 D-E 6 A-D 5 C-E 2 E-B 3 A-E 7 --distance A-B-C
dotnet run --setup A-B 5 B-C 4 C-D 8 D-C 8 D-E 6 A-D 5 C-E 2 E-B 3 A-E 7 --distance A-E-B-C-D
dotnet run --setup A-B 5 B-C 4 C-D 8 D-C 8 D-E 6 A-D 5 C-E 2 E-B 3 A-E 7 --distance A-E-D
dotnet run --setup A-B 5 B-C 4 C-D 8 D-C 8 D-E 6 A-D 5 C-E 2 E-B 3 A-E 7 --numberTrips A-C 4
dotnet run --setup A-B 5 B-C 4 C-D 8 D-C 8 D-E 6 A-D 5 C-E 2 E-B 3 A-E 7 --shortestRoute A-C

```

* Go to the folder RoutePlanner\bin\Debug\netcoreapp3.1 inside the solution and execute

```console
cd RoutePlanner\bin\Debug\netcoreapp3.1
RoutePlanner.exe --setup A-B 5 B-C 4 C-D 8 D-C 8 D-E 6 A-D 5 C-E 2 E-B 3 A-E 7 --distance C-D-C
RoutePlanner.exe --setup A-B 5 B-C 4 C-D 8 D-C 8 D-E 6 A-D 5 C-E 2 E-B 3 A-E 7 --numberTrips C-C 2
RoutePlanner.exe --setup A-B 5 B-C 4 C-D 8 D-C 8 D-E 6 A-D 5 C-E 2 E-B 3 A-E 7 --shortestRoute A-C
```

## Running the tests
To run the test, go to the folder RoutePlannerTests inside the solution and execute

```console
cd RoutePlannerTest
dotnet build
dotnet test
```

## Dependencies
As no dependencies were allowed in the solution and only in tests, only these where added 

* Moq -- As a mocking framework
* Microsoft TestFramework -- As a testing framework

## Notes

### Solution notes

As I ran out of time the project is not fully tested and also not all the question of the test were answered and fully tested as a summary of the status

1. The distance of the route A-B-C.
    * Tested.     
    ```c#
    RoutePlannerBLTest.GetDistance_RouteABC_9IsReturned
    ```
    * Interfaced   
    ```console
    RoutePlanner.exe --setup A-B 5 B-C 4 C-D 8 D-C 8 D-E 6 A-D 5 C-E 2 E-B 3 A-E 7 --distance C-D-C
    ```
1. The distance of the route A-E-B-C-D.
    * Tested.     
    ```c#
    RoutePlannerBLTest.GetDistance_RouteAEBCD_22Returned
    ```
    * Interfaced   
    ```console
    RoutePlanner.exe --setup A-B 5 B-C 4 C-D 8 D-C 8 D-E 6 A-D 5 C-E 2 E-B 3 A-E 7 --distance A-E-B-C-D
    ```
1. The distance of the route A-E-D.
    * Tested.     
    ```c#
    GetDistance_RouteAED_NotFoundValueIsReturned
    ```
    * Interfaced   
    ```console
    RoutePlanner.exe --setup A-B 5 B-C 4 C-D 8 D-C 8 D-E 6 A-D 5 C-E 2 E-B 3 A-E 7 --distance A-E-D
    ```
1. The number of trips starting at C and ending at C with a maximum of 3 stops. In the
sample data below, there are two such trips: C-D-C (2 stops) and C-E-B-C (3 stops).
 * Tested.     
    ```c#
    getRoutes_FromCtoCinMax3Stops_2RoutesFound
    ```
    * Not Interfaced   
1. The number of trips starting at A and ending at C with exactly 4 stops.
    * Tested.     
    ```c#
    GetRoutes_FromAtoCin4Stops_3RoutesFound
    ```
    * Interfaced   
    ```console
    RoutePlanner.exe --setup A-B 5 B-C 4 C-D 8 D-C 8 D-E 6 A-D 5 C-E 2 E-B 3 A-E 7 --numberTrips A-C 4
    ```
1. The length of the shortest route (in terms of distance to travel) from A to C.
 * Tested.     
    ```c#
    ShortestDistance_FromAtoC_9IsReturned
    ```
    * Interfaced   
    ```console
    RoutePlanner.exe --setup A-B 5 B-C 4 C-D 8 D-C 8 D-E 6 A-D 5 C-E 2 E-B 3 A-E 7 --shortestRoute A-C
    ```
1. The length of the shortest route (in terms of distance to travel) from B to B.
    * Not solved.
1. The number of different routes from C to C with a distance of less than 30.
    * Not solved.

### Implementation details
    The application has been split in three-layer
    * data. 
        Includes the basic data of the application, Academies, and the nodes that we use to build the graph
    * Business. 
        Includes the logic of the application, that in our case is split between the graph, a data structure to store  graphs, as the graph has a logic I preferred to put this class in the Business although this can be arguable, and a RoutePlanner that uses the graph to solve the different problems
    * APIInterfaces.
        Includes the interfaces between our application and the world in our case we only implemented one APIConsole that allows us to interpret the instructions from the command line

### Implementation notes
    The application due to time/specification constraints has some weakness that I want to notice.
    * Is missing a defensive programming approach. 
    * There are no tests for edge cases.
    * There are no tests for wrong parameters.
    * There are home-made solutions in place of external libraries. As:
        * Graph data structure.
        * Dependency injection.
        * Parameter parser.
