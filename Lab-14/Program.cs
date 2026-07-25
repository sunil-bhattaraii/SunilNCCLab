/*
 * a) Lambda Expressions in C#
 * b) LINQ with Lambda Expression in C#
 */

namespace Lab14;

class Program
{
    static void Main()
    {
        // a) lambda expressions
        Func<int, int, int> add = (a, b) => a + b;
        Console.WriteLine($"Add: {add(3, 5)}");

        Func<string, bool> isLong = s => s.Length > 5;
        Console.WriteLine($"\"Hello\" is long? {isLong("Hello")}");
        Console.WriteLine($"\"Sunil\" is long? {isLong("Sunil")}");

        Action<string> print = msg => Console.WriteLine(msg);
        print("Lambda Action Demo");

        // b) LINQ with lambda
        int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var evens = nums.Where(n => n % 2 == 0).ToList();
        Console.WriteLine($"\nEven: {string.Join(", ", evens)}");

        var sorted = nums.OrderByDescending(n => n).ToList();
        Console.WriteLine($"Descending: {string.Join(", ", sorted)}");

        var squares = nums.Select(n => n * n).ToList();
        Console.WriteLine($"Squares: {string.Join(", ", squares)}");

        int sum = nums.Where(n => n > 5).Sum(n => n);
        Console.WriteLine($"Sum > 5: {sum}");

        Shared.Print.MyDetails(14);
    }
}
