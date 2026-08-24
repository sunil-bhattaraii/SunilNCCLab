using Shared;

namespace Lab3;

class Program
{
    static void Main()
    {
        int [][] jagged = new int[3][];

        jagged[0] = new int[2] {1, 2};
        jagged[1] = new int[3] { 3, 4, 5};
        jagged[2] = new int[5] { 6, 7, 8, 9, 10};

        for(int i = 0; i < 3; i++)
        {
            Console.Write($"row {i + 1}: ");
            foreach(int num in jagged[i])
            {
                Console.Write($"{num}, ");
            }
            Console.WriteLine();
        }

        Shared.Print.MyDetails(3);
    }
}
