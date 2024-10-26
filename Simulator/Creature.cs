namespace Simulator;

public class Creature
{
    private string name;
    private int level;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int Level
    {
        get { return level; }
        set { level = value > 0 ? value : 1; }
    }
    public string Info => $"{Name} [{Level}]";

    public Creature(string name, int level = 1)
    {
        Name = name;
        Level = level;
    }

    public Creature() {}

    public void SayHi()
    {
        Console.WriteLine($"Hi, I'm {Name}, my level is {Level}.");
    }
}