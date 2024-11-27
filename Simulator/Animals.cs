using Simulator.Maps;
using System.Text.RegularExpressions;

namespace Simulator;

//public class Animals : IMappable
public class Animals
{
    private string description = "Unknown";
    public required string Description
    {
        get
        {
            return description;
        }
        init
        {
            description = Validator.Shortener(value, 3, 15, '#');
        }
    }
    public uint Size { get; set; } = 3;
    public virtual string Info => $"{Description} <{Size}>";

    public void Go(Direction direction)
    {
        throw new NotImplementedException();
    }

    public void InitMapandPosition(Map map, Point position)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return GetType().Name + ": " + Info;
    }
}
