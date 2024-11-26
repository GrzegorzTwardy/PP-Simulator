namespace Simulator.Maps;

public abstract class SmallMap : Map
{
    private List<Creature>?[,] _fields;

    public SmallMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
        if (sizeX > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeX), "Too wide");
        }
        if (sizeY > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeY), "Too tall");
        }

        _fields = new List<Creature>[sizeX, sizeY];
    }

    public override void Add(Creature creature, Point p)
    {
        if (_fields[p.X, p.Y] == null)
            _fields[p.X, p.Y] = new List<Creature>();
        _fields[p.X, p.Y]?.Add(creature);
    }

    public override void Remove(Creature creature, Point p)
    {
        _fields[p.X, p.Y]?.Remove(creature);
        if (_fields[p.X, p.Y]?.Count == 0)
            _fields[p.X, p.Y] = null;
    }

    public override List<Creature> At(Point p)
    {
        return _fields[p.X, p.Y];
    }

    public override void At(int x, int y)
    {
        var point = new Point(x, y);
        At(point);
    }

    public override void Move(Creature creature, Point startPoint, Point endPoint)
    {
        Remove(creature, startPoint);
        Add(creature, endPoint);
    }
}