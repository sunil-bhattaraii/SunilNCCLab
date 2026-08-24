using System;
namespace Lab16;

[AttributeUsage(AttributeTargets.Class)]
class AuthorAttribute : Attribute
{
    public string Name;
    public AuthorAttribute(string name) => Name = name;
}

[Author("Sunil")]
class Program
{
    [Obsolete("Use NewMethod() instead.")]
    static void OldMethod()
    {
        Console.WriteLine("Old Method");
    }

    static void Main()
    {
        OldMethod(); // Built-in attribute

        AuthorAttribute? attr =
            (AuthorAttribute?)Attribute.GetCustomAttribute(
                typeof(Program), typeof(AuthorAttribute));

        Console.WriteLine(attr?.Name);

        Shared.Print.MyDetails(16);
    }
}
