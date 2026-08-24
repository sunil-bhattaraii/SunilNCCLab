namespace Lab7;

class Program
{
    interface ILand
    {
        void Walk();
    }

    interface IWater
    {
        void Swim();
    }

    interface IAir
    {
        void Fly();
    }

    abstract class Creature
    {
        public Creature()
        {
            Console.WriteLine("I am a living Creature");
        }
    }

    class Duck: Creature, ILand, IWater, IAir
    {
        public void Walk()
        {
            Console.WriteLine("Quack, Quack, I walk");
        }

        public void Swim()
        {
            Console.WriteLine("Splash! Splash!");
        }

        public void Fly()
        {
            Console.WriteLine("I can Flyy...");
        }

    }


    static void Main()
    {
        Duck d = new Duck();
        d.Walk();
        d.Fly();
        d.Swim();


        Shared.Print.MyDetails(7);
    }
}
