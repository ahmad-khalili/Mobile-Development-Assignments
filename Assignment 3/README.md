# Assignment 3
## Model Classes
### User
- The "User" class contains the required attributes along with the overriden "Equals" method and "toString" method
```ruby
 public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"ID: {Id} -- " +
                $"Name: {Name} -- " +
                $"Age: {Age}";
        }

        public override bool Equals(object? obj)
        {
            var item = obj as User;
            if (item == null)
            {
                return false;
            }
            return this.Id.Equals(item.Id);
```
### Staff User
- The "Staff User" class inherits the "User" class + its role attribute and uses the overriden "toString" method from the "User" class and adds the role to it
```ruby
public enum Roles { Role1, Role2, Role3 };
        public Roles Role { get; set; }

        public override string ToString()
        {
            return base.ToString() + $" -- Role: {Role}";
```
### Admin User
- The admin follows the same concept as the "Staff User" class, but instead adds a list of staff users
```ruby
public List<StaffUser> StaffUsers { get; set; }

        public override string ToString()
        {
            return base.ToString() +  $" -- Staff Users: {printStaffUsers()}";
        }

        private string printStaffUsers()
        {
            string staffUsers = string.Empty;
            int count = 0;
            foreach (var staffUser in StaffUsers)
            {
                if (count > 0)
                {
                    staffUsers += " -- ";
                }
                staffUsers += $"{staffUser.Name}";
                count++;
            }
            return staffUsers;
```
## ModelView Class
- Firstly, this class contains a list for each type of users (To be able to edit users according to their models/attributes), and 2 delegate classes, one for the users, and one for the exceptions (I'm not sure if you meant for us to print exceptions using delegates or to throw them in the ModelView class and catch them in the View class)
```ruby
        List<User> users = new List<User>();
        List<StaffUser> staffUsers = new List<StaffUser>();
        List<AdminUser> adminUsers = new List<AdminUser>();

        public delegate void UserDelegate(User user);
        public delegate void ExceptionDelegate(Exception exception);

        public UserDelegate OnAdd;
        public UserDelegate OnRemove;
        public UserDelegate OnUpdate;
        public ExceptionDelegate OnError;
```
### Adding Users
- The "AddUser" method has a return type of void (Because it only adds the passed user onto the users list).
- I defined a general exception, and changed its value based on the condition using the exceptions I defined. Then I passed that exception to the "ExceptionDelegate" and returned instead of throwing the error (so it could output in the View class)
- Finally, it checks if the "OnAdd" delegate has been initialized and passes the created user onto it. The other "AddUser" classes for Staff Users and Admin Users follow the concept but while checking the extra attribute for their models, and adds to a their respective lists
```ruby
public void AddUser(User user)
        {
            Exception exception = null;

            if (String.IsNullOrEmpty(user.Id.ToString()) || String.IsNullOrEmpty(user.Name) || String.IsNullOrEmpty(user.Age.ToString()))
            {
                exception = new InvalidDataException("Invalid Data!");
            }

            if (users.Contains(user))
            {
                exception = new DuplicateUserException("User Already Exists!");
            }

            if(OnError != null && exception != null)
            {
                OnError(exception);
                return;
            }

            if (OnAdd != null)
            {
                OnAdd(user);
            }


            users.Add(user);
```
### Updating Users
- The "EditUser" method takes 3 parameters, one for the user requested to be edited, and the values you want the user to be edited to (name, age)
- Then, as the user adding methods, I defined a general exception and changed its value based on the conditions, such as user not being found. then I edited that user from the users list using the index of that passed user and finally calling the "OnUpdate" delegate method
```ruby
public void EditUser(User user, string name, int age)
        {
            Exception exception = null;

            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(age.ToString()))
            {
                exception = new InvalidDataException("Invalid Data!");
            }

            if (!(users.Contains(user))){
                exception = new UserNotFoundException("User Was Not Found!");
            }

            if (OnError != null && exception != null)
            {
                OnError(exception);
                return;
            }

            users[users.IndexOf(user)].Name = name;
            users[users.IndexOf(user)].Age = age;

            if (OnUpdate != null)
            {
                OnUpdate(user);
```
### Removing Users
- The only difference with this method, is that it only has one exception, which is the user not being found exception, and it removes the user (if found) from the list based on the passed user
```ruby
public void DeleteUser(User user)
        {
            Exception exception = null;

            if (!(users.Contains(user)))
            {
                throw new UserNotFoundException("User Was Not Found!");
            }

            if (OnError != null && exception != null)
            {
                OnError(exception);
                return;
            }

            if (OnRemove != null)
            {
                OnRemove(user);
            }

            users.Remove(user);
```
## View Class
- The View class uses the ModelView class to aquire delegate methods (for the purpose of printing new changes when called methods from the ModelView class change any values from the models)
- It also defines its own methods to print the item that has been affected in the View class's console using the delegate methods
```ruby
ClassModelView viewModel = new ClassModelView();

viewModel.OnAdd += UpdateAddView;
viewModel.OnRemove += UpdateRemoveView;
viewModel.OnUpdate += UpdateView;
viewModel.OnError += UpdateErrorView;

void UpdateErrorView(Exception exception)
{
    Console.WriteLine($"Error: {exception.Message}");
}

void UpdateView(User user)
{
    Console.WriteLine($"User is Updated: {user}");
}

void UpdateRemoveView(User user)
{
    Console.WriteLine($"User is Removed: {user}");
}

void UpdateAddView(User user)
{
    Console.WriteLine($"User is Added: {user}");
}
```
## User Defined Exceptions
- I created the required exceptions in seperate classes. Then made them inherit the "Exception" class and use its message method to define the messages for those exceptions (The other 2 required exceptions follow suit)
```ruby
public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message) { }
```
