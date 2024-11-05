﻿namespace Simulator;
public class Orc : Creature
{
    static int huntCounter = 0;
    private int rage;
    public int Rage
    {
        get { return rage; }
        init
        {
            if (value > 10)
                rage = 10;
            else if (value < 0)
                rage = 0;
            else
                rage = value;
        } 
    }

    public override int Power
    {
        get { return (7 * Level) + (3 * Rage); }
    }

    public Orc(string name, int level = 1, int rage = 1) : base(name, level)
    {
        Rage = rage;
    }

    public Orc() : base("Unknown Orc", 1) => Rage = rage;

    public void Hunt()
    {
        huntCounter += 1;

        Console.WriteLine($"{Name} is hunting.");
        if (rage < 10 && huntCounter % 2 == 0) rage++;
    }

    public override void SayHi() => Console.WriteLine(
        $"Hi, I'm {Name}, my level is {Level}, my rage is {Rage}."
    );
}