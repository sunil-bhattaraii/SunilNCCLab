namespace Lab6;

class Staff()
{
    public virtual void Introduce()
    {
        Console.WriteLine("I am a staff");
    }

    public void Salary()
    {
        Console.WriteLine("My Base Salary is 40,000.");
    }
}

class Teacher: Staff
{
    public override void Introduce()
    {
        Console.WriteLine("I am a Teacher");
    }

    public new void Salary()
    {
        Console.WriteLine("My Gross Salary is 60,000.");
    }
}

class Program
{
    static void Main()
    {
        Staff s;
        Teacher t = new Teacher();
        s = t;
        t.Introduce();
        s.Introduce();

        t.Salary();
        s.Salary();

        Shared.Print.MyDetails(6);
    }
}
