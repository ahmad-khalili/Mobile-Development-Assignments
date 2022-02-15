class DigitAnalyzer
{
    public static void GetLargestDigit(int number) {
        int largest = 0;
        while (number != 0) {
            int r = number % 10;
            if (largest < r) {
                largest = r;
            }
            number = number / 10;
        }
        Console.WriteLine("\nThe largest digit is: " + largest + "\n");
    }
}
class NumberAnalyzer
{
    public static void GetLargestNumber(string numbers)
    {
        string[] split  = numbers.Split(" ");
        List<int> values = split.Select(x =>  int.Parse(x)).ToList();
        Console.WriteLine("\nThe largest number is: " + values.Max() + "\n");
    }
}
class OrderAnalyzer 
{
    public static void ReverseNumber(int number) 
    {
        int reverseNumber = 0;
        while (number > 0) { 
            reverseNumber = reverseNumber * 10 + number % 10;
            number = number / 10;
        }
        Console.WriteLine("\nThe reversed number is: " + reverseNumber + "\n");
    }
}
class TextAnalyzer 
{
    public static void ExtractNumber(string text) 
    {
        string number = new string(text.SkipWhile(c => !char.IsDigit(c)).TakeWhile(c => char.IsDigit(c)).ToArray());
        if (number.Length > 0)
        {
            Console.WriteLine("\nThe extracted number is: " + number + "\n");
        }
        else
        {
            Console.WriteLine("\nNo number was found!\n");
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        int intUserInput;
        string stringUserInput;
        int choice;
        while (true)
        {
            Console.WriteLine("1- Find Largest Number\n"
                + "2- Reverse Number\n"
                + "3- Find Largest Digit\n"
                + "4- Search for a Number in a String\n"
                + "5- Exit\n"
                + "Choice: ");
            choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter a number or numbers seperated by spaces: ");
                    stringUserInput = Console.ReadLine();
                    NumberAnalyzer.GetLargestNumber(stringUserInput);
                    break;
                case 2:
                    Console.WriteLine("Enter a number: ");
                    intUserInput = Convert.ToInt32(Console.ReadLine());
                    OrderAnalyzer.ReverseNumber(intUserInput);
                    break;
                case 3:
                    Console.WriteLine("Enter a number: ");
                    intUserInput = Convert.ToInt32(Console.ReadLine());
                    DigitAnalyzer.GetLargestDigit(intUserInput);
                    break;
                case 4:
                    Console.WriteLine("Enter text: ");
                    stringUserInput = Console.ReadLine();
                    TextAnalyzer.ExtractNumber(stringUserInput);
                    break;
                case 5:
                    return;
                default:
                    Console.WriteLine("Invalid Input!\n");
                    break;
            }
        }
    }
}
