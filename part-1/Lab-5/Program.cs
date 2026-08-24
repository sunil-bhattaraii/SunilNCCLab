namespace Lab5;

class Character
{
    protected string name;
    protected int health;

    public Character(string name)
    {
        this.name = name;
    }

    public void GetInfo()
    {
        Console.WriteLine($"Name: {name}\nHP: {health}");
    }
}

class Warrior: Character
{
    int mana;

    public Warrior(string name, int health, int mana): base(name)
    {
        base.health = health;
        this.mana = mana;
    }

    public void ShowInfo()
    {
        base.GetInfo();
        Console.WriteLine($"Mana: {mana}");
    }
}
class Program
{
    static void Main()
    {
        Warrior leo = new Warrior("leo", 1000, 1000);
        leo.ShowInfo();

        Shared.Print.MyDetails(5);
    }
}
