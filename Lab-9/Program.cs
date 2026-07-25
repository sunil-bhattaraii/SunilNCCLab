/*
 * a) Delegate
 * b) Multicast delegate
 * c) Func Delegate
 * d) Action Delegate
 * e) Anonymous Method
 * f) Event in C#
 */

namespace Lab9;

// delegate
delegate int Operate(int a, int b);

// event publisher
class Alarm
{
    // event
    public event Action? OnRing;

    public void Trigger()
    {
        OnRing?.Invoke();
    }
}

class Program
{
    // delegate method
    static int Add(int a, int b) => a + b;

    // multicast delegate method
    static void Log(string msg) => Console.WriteLine($"Log: {msg}");
    static void Alert(string msg) => Console.WriteLine($"Alert: {msg}");

    static void Main()
    {
        // a) delegate
        Operate op = new Operate(Add);
        Console.WriteLine($"Add: {op(3, 4)}");

        // b) multicast delegate
        Action<string> notifier = Log;
        notifier += Alert;
        notifier("Server is down");

        // c) Func delegate
        Func<int, int, int> multiply = (x, y) => x * y;
        Console.WriteLine($"Multiply: {multiply(5, 6)}");

        // d) Action delegate
        Action<string> greet = name => Console.WriteLine($"Hello, {name}!");
        greet("Sunil");

        // e) anonymous method
        Operate sub = delegate (int a, int b) { return a - b; };
        Console.WriteLine($"Subtract: {sub(10, 3)}");

        // f) event
        Alarm alarm = new Alarm();
        alarm.OnRing += () => Console.WriteLine("Wake up!");
        alarm.OnRing += () => Console.WriteLine("Get ready!");
        alarm.Trigger();

        Shared.Print.MyDetails(9);
    }
}
