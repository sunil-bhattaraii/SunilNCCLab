/*
 * a) exception handling using try, catch and finally blocks
 * b) throw keyword in exception handling
 * c) custom exception handling
 */

namespace Lab15;

// custom exception
class InvalidAgeException : Exception
{
    public InvalidAgeException(string msg) : base(msg) { }
}

class Program
{
    // b) throw keyword
    static void CheckAge(int age)
    {
        if (age < 0)
            throw new ArgumentOutOfRangeException("Age cannot be negative");
        Console.WriteLine($"Valid age: {age}");
    }

    static void Main()
    {
        // a) try, catch, finally
        try
        {
            int[] arr = { 1, 2, 3 };
            Console.WriteLine(arr[5]);
        }
        catch (IndexOutOfRangeException e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
        finally
        {
            Console.WriteLine("Finally block executed\n");
        }

        // b) throw
        try
        {
            CheckAge(-5);
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine($"Error: {e.Message}\n");
        }

        // c) custom exception
        try
        {
            int age = -3;
            if (age < 0)
                throw new InvalidAgeException("Age must be non-negative");
        }
        catch (InvalidAgeException e)
        {
            Console.WriteLine($"Custom Error: {e.Message}");
        }

        Shared.Print.MyDetails(15);
    }
}
