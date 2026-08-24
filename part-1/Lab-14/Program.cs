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
        Console.WriteLine($"Add: {add(3, 4)}");

        // b) LINQ with lambda
        int[] nums = { 1, 2, 3, 4, 5 };

        var evens = nums.Where(n => n % 2 == 0).ToList();
        Console.WriteLine($"\nEven: {string.Join(", ", evens)}");

        Shared.Print.MyDetails(14);
    }
}
