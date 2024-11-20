namespace Simulator.Maps;

public class SmallSquareMap : Map
{
    public int Size { get; }

    public SmallSquareMap(int size) : base(size, size)
    {
        Size = size;
    }

    //public override bool Exist(Point p)
    //{
    //    return p.X >= 0 && p.X <= SizeX-1 && p.Y >= 0 && p.Y <= SizeY-1;
    //}

    public override Point Next(Point p, Direction d)
    {
        Point nextPoint = p.Next(d);
        return Exist(nextPoint) ? nextPoint : default;
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        Point nextDiagonalPoint = p.NextDiagonal(d);
        return Exist(nextDiagonalPoint) ? nextDiagonalPoint : default;
    }
}
