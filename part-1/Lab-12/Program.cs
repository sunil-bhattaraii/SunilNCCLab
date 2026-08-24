/*
 * WAP to take input from keyboard and write them to a file
 */

namespace Lab12;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter lines (type 'exit' to stop):");

        string path = "output.txt";
        using (StreamWriter writer = new StreamWriter(path))
        {
            string? line;
            while ((line = Console.ReadLine()) != null && line != "exit")
                writer.WriteLine(line);
        }

        Console.WriteLine($"\nFile written to {path}");
        Console.WriteLine(File.ReadAllText(path));

        Shared.Print.MyDetails(12);
    }
}
