/*
 * WAP to demonstrate asynchronous programming in C#
 * using async and await keywords
 */

namespace Lab17;

class Program
{
    // async method
    static async Task FetchData(string source, int delay)
    {
        Console.WriteLine($"Fetching from {source}...");
        await Task.Delay(delay);
        Console.WriteLine($"Done from {source} after {delay}ms");
    }

    // async with return value
    static async Task<int> ComputeAsync()
    {
        await Task.Delay(1000);
        return 42;
    }

    static async Task Main()
    {
        // run concurrently
        Task t1 = FetchData("API-A", 2000);
        Task t2 = FetchData("API-B", 1000);
        await Task.WhenAll(t1, t2);

        Console.WriteLine();

        // async with return
        int result = await ComputeAsync();
        Console.WriteLine($"Result: {result}");

        Shared.Print.MyDetails(17);
    }
}
