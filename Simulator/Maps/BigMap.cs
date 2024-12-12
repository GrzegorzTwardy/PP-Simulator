namespace Simulator.Maps;

public abstract class BigMap : Map
{
    private Dictionary<Point, List<IMappable>> _fields;

    public Dictionary<Point, List<IMappable>> Fields { get => _fields; set => _fields = value; }

    public BigMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
        if (sizeX > 1000)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeX), "Too wide");
        }
        if (sizeY > 1000)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeY), "Too tall");
        }

        Fields = new Dictionary<Point, List<IMappable>>();
    }

    public override void Add(IMappable mappable, Point p)
    {
        Fields.TryGetValue(p, out var list);
        if (list != null)
            list.Add(mappable);
        else
            Fields[p] = new List<IMappable> { mappable };
    }

    public override void Remove(IMappable mappable, Point p)
    {
        if (Fields.TryGetValue(p, out var list))
            list.Remove(mappable);
    }

    public override List<IMappable> At(Point p)
    {
        if (Fields.TryGetValue(p, out var list))
        {
            return list;
        }
        else
        {
            return new List<IMappable>();
        }
    }

    public override void At(int x, int y)
    {
        var point = new Point(x, y);
        At(point);
    }

    public override void Move(IMappable mappable, Point startPoint, Point endPoint)
    {
        Remove(mappable, startPoint);
        Add(mappable, endPoint);
    }

    public override Map Clone()
    {
        var newMap = (BigMap)Activator.CreateInstance(this.GetType(), SizeX, SizeY)!;
        foreach (var field in Fields)
        {
            newMap.Fields[field.Key] = new List<IMappable>(field.Value);
        }
        return newMap;
    }
}