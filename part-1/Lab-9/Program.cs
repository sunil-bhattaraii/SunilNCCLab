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
delegate void Greeting(string name);

// event publisher
class Button
{
    // event
    public event EventHandler? Clicked;

    public void Press()
    {
        Clicked?.Invoke(null, EventArgs.Empty);
    }
}

class Program
{
    static void SayHello(string name) {
        Console.WriteLine($"Hello, {name}");
        }

    static void SayBye(string name){
        Console.WriteLine($"Bye, {name}");
        }

    static void Main()
    {
        // a) delegate
        Greeting greet = SayHello;
        greet("Sunil");

        // b) multicast delegate
        greet += SayBye;
        greet("Ram");

        // c) Func delegate
        Func<int, int, int> add = (a, b) => a + b;
        Console.WriteLine($"Func: {add(3, 4)}");

        // d) Action delegate
        Action<string> display = msg => Console.WriteLine($"Action: {msg}");
        display("Delegate Demo");

        // e) anonymous method
        Greeting farewell = delegate (string name) { Console.WriteLine($"Farewell, {name}"); };
        farewell("Hari");

        // f) event
        Button btn = new Button();
        btn.Clicked += (s, e) => Console.WriteLine("Button was clicked");
        btn.Press();

        Shared.Print.MyDetails(9);
    }
}
