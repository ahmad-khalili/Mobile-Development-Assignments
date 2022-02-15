# Assignment 1
### 1- First Operation (Find Largest Number)
- After taking the user input as a string from the main (to be able to get an infinite amount of numbers seperated by spaces), I passed the value to the "GetLargestNumber" function. After that, I created a string array and placed the value of the original user input and seperated the numbers using the split function.
- Then I used a list of type int to select each value or element from the string array and convert it into an integer. Then, I used the "Max()" function for int lists to find the maximum value.
```ruby
class NumberAnalyzer
{
    public static void GetLargestNumber(string numbers)
    {
        string[] split  = numbers.Split(" ");
        List<int> values = split.Select(x =>  int.Parse(x)).ToList();
        Console.WriteLine("\nThe largest number is: " + values.Max() + "\n");
```
### 2- Second Operation (Reverse a Given Number)
- I started by only taking the last digit using the remainder operator which is (number % 10). Then I appended that last digit from the original inputted number to the new int (reverseNumber) but multiplying it by 10 to take the actual digit and took advantage of the int to round it up to the closest int value by using (reverseNumber = reverseNumber * 10 + number % 10).
- After appending the last digit to the new (reverseNumber). I removed that digit from the original (number) by dividing it by 10 (number = number / 10), then I kept repeating using the while loop until the number reached 0.
```ruby
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
```
### 3- Third Operation (Find the Largest Digit of a Number)
- Using a similar method to the second operation, I took the rightmost digit from the number then gave that value to the largest value as a rounded up int, and then removed that digit by dividing it by 10, then kept comparing the rightmost digit to the largest value (and replacing it if the condition was met) until the number reached 0.
```ruby
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
```
### 4- Fourth Operation (Search for First Int Number Inside a Given Text)
- Since the requirement was to only find the first number, I used the "SkipWhile" method to skip every non-digit character before reaching a digit character. Then used the "TakeWhile" method to take the the digit characters until another non-digit character is reached. That way, it will only take the first number that shows and stop without taking another number.
- I also didn't account for decimals (because the requirement said "first int number").
```ruby
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
```
