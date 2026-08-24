/*
 * a) exception handling using try, catch and finally blocks
 * b) throw keyword in exception handling
 * c) custom exception handling
 */

namespace Lab15;

class InvalidAgeException : Exception
{
    public InvalidAgeException(string msg) : base(msg) { }
}

class Program
{
    static void CheckAge(int age)
    {
        if (age < 0)
            throw new InvalidAgeException("Age cannot be negative");

        Console.WriteLine($"Age: {age}");
    }
    static int i = 0;


    static void Main()
    {
        try{
            Console.WriteLine("Enter a valid age: ");
            int age = int.Parse(Console.ReadLine()!);

            CheckAge(age);

            Console.WriteLine("The age is valid");
        }

        catch (FormatException)
        {
            Console.WriteLine("Please enter a valid number.");
        }

        catch(InvalidAgeException e){
            Console.WriteLine("ERROR: " + e.Message);
        }

        finally{
            Console.WriteLine("Program Terminated");
        }

        Shared.Print.MyDetails(15);

        while(i++ < 2) {
            Console.WriteLine("~/B.Sc. CSIT/6th_sem/lab reports/SunilNCCLab/Lab-15> dotnet run");
            Main();}
    }
}
