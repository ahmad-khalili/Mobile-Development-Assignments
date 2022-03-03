using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD_Assignment2
{
    public class Route
    {
        string name = String.Empty;
        List<Router> route = new List<Router>();
        static List<Route> routes = new List<Route>();
        Route(string n)
        {
            name = n;
        }

        public static void createRoute(string n)
        {
            routes.Add(new Route(n));
        }
        public static void addRouter(Route route, int time)
        {
            route.route.Add(new Router(time));
        }
        public static int calculateRoute(Route route)
        {
            int count = 0;
            foreach (var router in route.route)
            {
                count += router.time;
            }
            return count;
        }
        public async static Task calculateBestRouteAsync()
        {
            var bestRouteCalculator = Task.Run(() =>
            {
                int count = 0;
                int shortest = 0;
                string bestRoute = String.Empty;
                Parallel.For(0, routes.Count, new ParallelOptions { MaxDegreeOfParallelism = 4 }, i =>
                 {
                     count = calculateRoute(routes[i]);
                     if (i == 0)
                     {
                         shortest = count;
                     }
                     if (count < shortest)
                     {
                         shortest = count;
                         bestRoute = routes[i].name;
                     }
                     count = 0;
                     Console.WriteLine($"{routes[i].name} is running in thread [{Thread.CurrentThread.ManagedThreadId}]");
                 });
                Console.WriteLine($"\nThe shortest route is {bestRoute} with a total time of {shortest}\n");
            });
            await bestRouteCalculator;
            bestRouteCalculator.GetAwaiter().GetResult();
        }
        public static void calculateBestRoute()
        {
            int count = 0;
            int shortest = 0;
            string bestRoute = String.Empty;
            bool firstLoop = true;
            foreach (var route in routes)
            {
                count = calculateRoute(route);
                if (firstLoop)
                {
                    shortest = count;
                }
                if (count < shortest)
                {
                    shortest = count;
                    bestRoute = route.name;
                }
                count = 0;
                firstLoop = false;
                Console.WriteLine($"{route.name} is running in thread [{Thread.CurrentThread.ManagedThreadId}]");
            }
            Console.WriteLine($"\nThe shortest route is {bestRoute} with a total time of {shortest}\n");
        }
        public static void getRoutes()
        {
            foreach (var route in routes)
            {
                Console.WriteLine($"\n[{route.name}]");
            }
        }
        public static Route findRoute(string name)
        {
            foreach (var route in routes)
            {
                if (route.name == name)
                {
                    return route;
                }
            }
            return null;
        }
        public static string getName(Route route)
        {
            return route.name;
        }
    }
}
