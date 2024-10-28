namespace Simulator;

public class Creature
{
    private string? name;
    private int level = 1;

    public string Name
    {
        get { return name ?? "Unknown"; }
        init
        {
            value = value.Trim();

            if (value.Length > 0)
            {
                if (value.Length > 25)
                {
                    value = value[..25].TrimEnd();
                }
                if (value.Length < 3)
                {
                    value = value.PadRight(3, '#');
                }
                name = char.ToUpper(value[0]) + value.Substring(1);
            }
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