using System;
using System.Collections.Generic;

namespace MD_Assignment2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true) {
                Console.WriteLine("\nSelect Option: \n\n"+
                    "1- View Routes\n\n" +
                    "2- Create Route\n\n" +
                    "3- Add Router\n\n" +
                    "4- Calculate Best Route\n\n" +
                    "5- Exit\n");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Route.getRoutes();
                        break;
                    case "2":
                        Console.WriteLine("\nEnter Route's Name: ");
                        string routeNameInput = Console.ReadLine();
                        if (Route.findRoute(routeNameInput) == null)
                        {
                            Route.createRoute(routeNameInput);
                            Console.WriteLine("\nRoute Created!\n");
                        }
                        else
                        {
                            Console.WriteLine("\nRoute already exists\n");
                        }
                        break;
                    case "3":
                        Console.WriteLine("\nName of Route you want to add a Router to: ");
                        string routeChoiceInput = Console.ReadLine();
                        Route route = Route.findRoute(routeChoiceInput);
                        if (route != null)
                        {
                            Console.WriteLine("\nRouter's Time: ");
                            int routerTimeChoice = Convert.ToInt32(Console.ReadLine());
                            Route.addRouter(route, routerTimeChoice);
                            Console.WriteLine($"\nRouter of time {routerTimeChoice} has been added to {Route.getName(route)}\n");
                        }
                        else
                        {
                            Console.WriteLine("\nRoute Not Found\n");
                        }
                        break;
                    case "4":
                        Console.WriteLine("\nAsynced: ");
                        await Route.calculateBestRouteAsync();
                        Console.WriteLine("\nNon-Asynced: ");
                        Route.calculateBestRoute();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("\nInvalid Input");
                        break;
                }
            }
        }
    }
}