using System;

namespace Simulator;

public class Birds : Animals
{
    bool CanFly { get; set; } = true;

    public override string Info
    {
        get
        {
            if (CanFly)
                return $"{Description} (fly+) <{Size}>";
            else
                return $"{Description} (fly-) <{Size}>";
        }
    }
    public Birds() { }
}