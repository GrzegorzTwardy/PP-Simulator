namespace Simulator;

public abstract class Creature
{
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

    public abstract void SayHi();

    public void Upgrade()
    {
        if (level < 10)
            level++;
        else
            Console.WriteLine($"Cannot upgrade {Name}, it's level is 10 (max).");
    }

    public void Go(Direction d)
    {
        string directionName = d.ToString().ToLower();
        Console.WriteLine($"{Name} goes {directionName}.");
    }

    public void Go(Direction[] d)
    {
        for (int i = 0; i < d.Length; i++)
        {
            Go(d[i]);
        }
    }

    public void Go(string d)
    {
        Go(DirectionParser.Parse(d));
    }
}