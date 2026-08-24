/*
 * WAP to demonstrate the concept of LINQ
 */

namespace Lab13;

class Program
{
    static void Main()
    {
        int[] nums = { 5, 3, 8, 1, 9, 2 };

        // where
        var even = from n in nums where n % 2 == 0 select n;
        Console.WriteLine("Even:");
        foreach (var n in even) Console.Write($"{n}, ");

        // orderby
        var sorted = from n in nums orderby n select n;
        Console.WriteLine("\nSorted:");
        foreach (var n in sorted) Console.Write($"{n}, ");

        // select
        var doubled = from n in nums select n * 2;
        Console.WriteLine("\nDoubled:");
        foreach (var n in doubled) Console.Write($"{n}, ");

        Shared.Print.MyDetails(13);
    }
}
