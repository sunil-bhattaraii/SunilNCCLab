/*
 * a) Structure (struct)
 * b) Enumeration (enum)
 * c) Partial class
 */

namespace Lab8;

// struct
struct Book
{
    public string Title;
    public double Price;

    public Book(string title, double price)
    {
        Title = title;
        Price = price;
    }
}

// enum
enum Season
{
    Spring, Summer, Autumn, Winter
}

// partial class
partial class Player
{
    public string Name;

    public Player(string name)
    {
        Name = name;
    }
}

partial class Player
{
    public void Show()
    {
        Console.WriteLine($"Player: {Name}");
    }
}

class Program
{
    static void Main()
    {
        // struct
        Book b = new Book("Harry Potter", 500);
        Console.WriteLine($"Book: {b.Title}, Price: {b.Price}");

        // enum
        Season s = Season.Summer;
        Console.WriteLine($"Season: {s}");

        // partial class
        Player p = new Player("Neymar");
        p.Show();

        Shared.Print.MyDetails(8);
    }
}
