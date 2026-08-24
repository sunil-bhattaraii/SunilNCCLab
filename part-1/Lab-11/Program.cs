/*
 * Generic Class with Generic field and method
 */

namespace Lab11;

class Box<T>
{
    // generic field
    public T Item { get; set; }

    public Box(T item)
    {
        Item = item;
    }

    // generic method
    public void Show<U>(U value)
    {
        Console.WriteLine($"Generic Method: {value}");
    }
}

class Program
{
    static void Main()
    {
        Box<int> intBox = new Box<int>(10);
        Console.WriteLine($"Int Box: {intBox.Item}");
        intBox.Show("hello");

        Box<string> strBox = new Box<string>("Hello");
        Console.WriteLine($"String Box: {strBox.Item}");
        strBox.Show(99);

        Shared.Print.MyDetails(11);
    }
}
