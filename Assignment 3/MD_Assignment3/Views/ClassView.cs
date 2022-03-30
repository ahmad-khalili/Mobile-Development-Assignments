using MD_Assignment3.Models;
using MD_Assignment3.ModelViews;

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