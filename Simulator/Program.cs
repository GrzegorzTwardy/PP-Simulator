namespace Simulator;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting simulator!\n");

        Creature c = new Creature("Shrek", 7);
        c.SayHi();
        Console.WriteLine(c.Info);

        Animals a = new() { Description = "Dogs" };
        Console.WriteLine(a.Info);
    }
}
