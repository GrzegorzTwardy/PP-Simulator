namespace Simulator.Maps;

public class SmallSquareMap : Map
{
    public int Size { get; }

    public SmallSquareMap(int size)
    {
        if (size < 5 || size > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(size),
                "Rozmiar mapy musi być w zakresie od 5 do 20 punktów.");
        }
        Size = size;
    }

    public override bool Exist(Point p)
    {
        Rectangle r = new Rectangle(0, 0, Size - 1, Size - 1);
        return r.Contains(p);
    }

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
