namespace Simulator;

public class Birds : Animals
{
    public override char Symbol { get; } = 'b';
    public bool CanFly { get; set; } = true;

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
    public Birds(string description, bool canFly = true, uint size = 3) : base(description, size)
    {
        CanFly = canFly;
        if (CanFly) Symbol = 'B';
    }

    public override void Go(Direction direction)
    {
        if (Map == null)
            throw new ArgumentNullException("This mappable hasn't been assigned to any map.");

        if (CanFly)
        {
            Point nextPosition = Map.Next(Position, direction);

            Map.Move(this, Position, nextPosition);
            Position = nextPosition;
            nextPosition = Map.Next(Position, direction);

            Map.Move(this, Position, nextPosition);
            Position = nextPosition;
        }
        else
        {
            Point nextPosition = Map.NextDiagonal(Position, direction);
            Map.Move(this, Position, nextPosition);
            Position = nextPosition;
        }
    }
}