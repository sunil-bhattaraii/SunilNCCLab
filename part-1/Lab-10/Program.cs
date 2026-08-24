/*
 * a) Non generic collection
 * b) Generic Collection
 */

using System.Collections;

namespace Lab10;

class Program
{
    static void Main()
    {
        // a) non-generic collection
        ArrayList list = new ArrayList();
        list.Add(1);
        list.Add("two");
        list.Add(3.0);

        Console.WriteLine("Non-Generic Collection:");
        foreach (object item in list)
            Console.Write($"{item}, ");

        // b) generic collection
        List<int> nums = new List<int>();
        nums.Add(10);
        nums.Add(20);
        nums.Add(30);

        Console.WriteLine("\nGeneric Collection:");
        foreach (int n in nums)
            Console.Write($"{n}, ");
        Console.WriteLine();

        Shared.Print.MyDetails(10);
    }
}
