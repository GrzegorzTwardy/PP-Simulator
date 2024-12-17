namespace Simulator.Maps;

public class BigBounceMap : BigMap
{
    public int SizeX { get; }
    public int SizeY { get; }

    public BigBounceMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
        SizeX = sizeX;
        SizeY = sizeY;
    }

    public override Point Next(Point p, Direction d)
    {
        Point nextPoint = p.Next(d);

        if (Exist(nextPoint))
            return nextPoint;

        return d switch
        {
            Direction.Right => new Point(SizeX - 2, p.Y),
            Direction.Left => new Point(1, p.Y),
            Direction.Up => new Point(p.X, SizeY - 2),
            Direction.Down => new Point(p.X, 1),
            _ => p
        };
        throw new InvalidDataException("Invalid Direction Value.");
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        Point nextPoint = p.NextDiagonal(d);
        if (Exist(nextPoint))
            return nextPoint;

        switch (d)
        {
            case Direction.Up:
                if (p.Y != SizeY-1)
                    return new Point(p.X - 1, p.Y+1);
                if (p.X != SizeX-1)
                    return new Point(p.X + 1, p.Y - 1);
                return new Point(p.X - 1, p.Y - 1);
            case Direction.Left:
                if (p.Y != SizeY - 1)
                    return new Point(p.X + 1, p.Y + 1);
                if (p.X != 0)
                    return new Point(p.X - 1, p.Y - 1);
                return new Point(1, p.Y - 1); ;
            case Direction.Right:
                if (p.X != SizeX - 1)
                    return new Point(p.X + 1, p.Y + 1);
                if (p.Y != 0)
                    return new Point(p.X - 1, p.Y - 1);
                return new Point(p.X - 1, 1); ;
            case Direction.Down:
                if (p.X != 0)
                    return new Point(p.X - 1, p.Y + 1);
                if (p.Y != 0)
                    return new Point(p.X + 1, p.Y - 1);
                return new Point(1, 1); ;
        }
        throw new InvalidDataException("Invalid Direction Value.");
    }
}
