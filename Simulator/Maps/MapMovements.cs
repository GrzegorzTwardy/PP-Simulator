namespace Simulator.Maps;

internal static class MapMovements
{
    public static Point WallNext(Map m, Point p, Direction d)
    {
        Point nextPoint = p.Next(d);
        return m.Exist(nextPoint) ? nextPoint : default;
    }

    public static Point WallNextDiagonal(Map m, Point p, Direction d)
    {
        Point nextDiagonalPoint = p.NextDiagonal(d);
        return m.Exist(nextDiagonalPoint) ? nextDiagonalPoint : default;
    }

    // TorusNext, BounceNext...
}
