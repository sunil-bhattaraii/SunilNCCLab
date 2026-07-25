/*
 * Generic Class with Generic field and method
 */

namespace Lab11;

// generic class with generic field
class Box<T>
{
    // generic field
    public T Content { get; set; }

    public Box(T content)
    {
        Content = content;
    }

    // generic method
    public void Swap<U>(ref U a, ref U b)
    {
        U temp = a;
        a = b;
        b = temp;
    }
}

class Program
{
    static void Main()
    {
        Box<int> numBox = new Box<int>(42);
        Console.WriteLine($"Int Box: {numBox.Content}");

        Box<string> textBox = new Box<string>("Hello Generics");
        Console.WriteLine($"String Box: {textBox.Content}");

        // generic method
        int x = 1, y = 2;
        Console.WriteLine($"Before swap: x={x}, y={y}");
        numBox.Swap(ref x, ref y);
        Console.WriteLine($"After swap: x={x}, y={y}");

        Shared.Print.MyDetails(11);
    }
}
