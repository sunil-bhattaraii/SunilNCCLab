/*
 * WAP to demonstrate the concept of LINQ
 */

namespace Lab13;

class Program
{
    static void Main()
    {
        int[] nums = { 10, 25, 3, 48, 7, 16, 30 };

        // where - filter
        var even = from n in nums where n % 2 == 0 select n;
        Console.WriteLine("Even numbers:");
        foreach (var n in even) Console.WriteLine(n);

        // orderby
        var sorted = from n in nums orderby n descending select n;
        Console.WriteLine("\nDescending order:");
        foreach (var n in sorted) Console.WriteLine(n);

        // select - project
        var doubled = from n in nums select n * 2;
        Console.WriteLine("\nDoubled:");
        foreach (var n in doubled) Console.WriteLine(n);

        // first, count, sum
        Console.WriteLine($"\nFirst: {nums.First()}");
        Console.WriteLine($"Count > 10: {nums.Count(n => n > 10)}");
        Console.WriteLine($"Sum: {nums.Sum()}");

        Shared.Print.MyDetails(13);
    }
}
