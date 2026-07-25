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
        // a) non-generic collection (ArrayList)
        ArrayList list = new ArrayList();
        list.Add(10);
        list.Add("Hello");
        list.Add(3.14);

        Console.WriteLine("Non-Generic (ArrayList):");
        foreach (object item in list)
            Console.WriteLine(item);

        // b) generic collection (List<T>)
        List<string> names = new List<string>();
        names.Add("Ram");
        names.Add("Shyam");
        names.Add("Hari");

        Console.WriteLine("\nGeneric (List<string>):");
        foreach (string name in names)
            Console.WriteLine(name);

        Shared.Print.MyDetails(10);
    }
}
