/*
 * a) built-in attributes in C#
 * b) create and use custom attribute in C#
 */

namespace Lab16;

// b) custom attribute
[AttributeUsage(AttributeTargets.Class)]
class DescriptionAttribute : Attribute
{
    public string Text;
    public DescriptionAttribute(string text) => Text = text;
}

[Description("This is a demo class")]
class Gadget
{
    public string Name = "Phone";
}

// a) built-in attributes
[Obsolete("Use NewTool instead")]
class OldTool
{
    public static void Run() => Console.WriteLine("Running old tool");
}

class NewTool
{
    public static void Run() => Console.WriteLine("Running new tool");
}

class Program
{
    static void Main()
    {
        // a) built-in: [Obsolete]
#pragma warning disable CS0618
        OldTool.Run();
#pragma warning restore CS0618
        NewTool.Run();

        // b) custom attribute
        DescriptionAttribute? attr = (DescriptionAttribute?)
            Attribute.GetCustomAttribute(typeof(Gadget), typeof(DescriptionAttribute));

        if (attr != null)
            Console.WriteLine($"\nDescription: {attr.Text}");

        Gadget g = new Gadget();
        Console.WriteLine($"Gadget: {g.Name}");

        Shared.Print.MyDetails(16);
    }
}
