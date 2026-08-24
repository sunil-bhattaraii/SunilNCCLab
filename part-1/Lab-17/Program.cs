/*
 * WAP to demonstrate asynchronous programming in C#
 * using async and await keywords
 */

namespace Lab17;

class Program
{
    static async Task Download(string name, int delay)
    {
        Console.WriteLine($"Downloading {name}...");
        await Task.Delay(delay);
        Console.WriteLine($"Downloaded {name}");
    }

    static async Task<int> Calculate()
    {
        await Task.Delay(1000);
        return 42;
    }

    static async Task Main()
    {
        // concurrent tasks
        Task t1 = Download("file1", 2000);
        Task t2 = Download("file2", 1000);
        await Task.WhenAll(t1, t2);

        // async with return value
        int result = await Calculate();
        Console.WriteLine($"\nResult: {result}");

        Shared.Print.MyDetails(17);
    }
}
