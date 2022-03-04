# Assignment 2
### Route Class
- The "Route" class consists of the route's name, a list for all the routers inside that route, and a list containing all the created routes
```ruby
public class Route
    {
        string name = String.Empty;
        List<Router> route = new List<Router>();
        static List<Route> routes = new List<Route>();
```
- To create a route, only the route's name is needed as an arguement for "createRoute", then this method creates a new route using the constructor (which also takes the name of route) and adds that newly created route to the list of routes.
- While adding a router to a route takes the route in which you want to add the router to, and the time required to reach the router.
```ruby
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
```
### Router Class
- The "Router" class only consists of an int describing the time required to reach the router, and a constructor to create a router.
```ruby
public class Router
    {
        public int time;
        public Router(int t)
        {
            time = t;
```
### Shortest Route Calculation
##### 1- Single Route Calculation
- The "calculateRoute" method takes a route as a parameter and cycles through the list inside of that class called "route" that contains all the routers in the route. Then using the count variable, I took the summation of all the routers' times found in that route and returned the value of the summation.
```ruby
public static int calculateRoute(Route route)
        {
            int count = 0;
            foreach (var router in route.route)
            {
                count += router.time;
            }
            return count;
```
##### 2- Asynced Shortest Route Calculation
- Using async, await, and the Task class, I created a variable called "bestRouteCalculator" and ran the statements inside the lambda expression with Task.Run() to take advantage of the await property.

- The calculator consists of  the Parallel.For that allowed me to define a new ParallelOption to limit the number of parallel threads to 4 (as requested). The Parallel.For loop cycles through all the routes found in the routes list with an iterator called i, and calls the method called "calculateRoute" while passing the route it has reached in the list.

- That value is placed in the count variable. Then, it checks if the for loop is in its first iteration to initially call the shortest route the first one (since the shortest can't be initliazed as 0), and then it checks if the count value is smaller than the shortest value (on all iterations) and assigns that value to the shortest while also taking the name of that route.

- Finally, the count value is reset and I've added an output showing each route's calculation thread id to help in testing if the method is actually working or not. Then ofcourse, I showed the route with the shortest path along with its total needed time to pass all its routers.
```ruby
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
```
##### 3- Normal Shortest Route Calculation
- Similarily to the async method, I used a foreach loop instead of a Parallel.For loop to cycle through all the routes found in the routes list. And passed each route to the "calculateRoute" method while assigning its returned value to count. Then i used a boolean to check for the first loop and intialize the value of the variable "shortest", then checked if the count is less than the shortest value to find the shortest's route's name and total time.
- Finally as in the async method, I reset the count, and made the bool to false (for it to only be true in the first iteration). Then I added the calculation thread id for each route for testing and the name/ total time of the shortest route.
```ruby
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
```
### Program Class
- The program class contains the switch statement that handles all the user input, for creating new routes, viewing all the created routes, adding routers to routes, and calculating the shortest routes between all the created ones.
```ruby
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
```
