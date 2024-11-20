//using Simulator.Maps;
namespace Simulator;

public abstract class Creature
{
    //public Map? Map { get; private set; }
    //public Point Position { get; private set; }
    private string? name;
    private int level = 1;

    public string Name
    {
        get { return name ?? "Unknown"; }
        init
        {
            name = Validator.Shortener(value, 3, 25, '#');
        }
    }

    public int Level
    {
        get { return level; }
        init
        {
            var tmp = value;

            if (tmp < 1)
                level = 1;
            else if (tmp > 10)
                level = 10;
            else
                level = tmp;
        }
    }

    public abstract int Power { get; }

    public abstract string Info { get; }

    public Creature(string name, int level = 1)
    {
        Name = name;
        Level = level;
    }

    public Creature() {}

    public override string ToString()
    {
        return GetType().Name + ":  " + Info;
    }

    public abstract string Greeting();

    public void Upgrade()
    {
        if (level < 10)
            level++;
        else
            Console.WriteLine($"Cannot upgrade {Name}, it's level is 10 (max).");
    }

    public string Go(Direction direction) => $"{direction.ToString().ToLower()}";

    public string[] Go(Direction[] d)
    {
        string[] tab = new string[d.Length];
        for (int i = 0; i < d.Length; i++)
        {
            tab[i] = Go(d[i]);
        }
        return tab;
    }

    public string[] Go(string d)
    {
        return Go(DirectionParser.Parse(d));
    }
}