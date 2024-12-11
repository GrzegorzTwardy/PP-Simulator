namespace Simulator.Maps;

public abstract class SmallMap : Map
{
    private Dictionary<Point, List<IMappable>> _fields;

    public Dictionary<Point, List<IMappable>> Fields { get => _fields; set => _fields = value; }

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
        Fields = new Dictionary<Point, List<IMappable>>();
    }

    public override void Add(IMappable mappable, Point p)
    {
        if (Fields[p] == null)
            Fields[p] = new List<IMappable>();
        Fields[p]?.Add(mappable);
    }

    public override void Remove(IMappable mappable, Point p)
    {
        Fields[p]?.Remove(mappable);
        if (Fields[p]?.Count == 0)
            Fields[p] = null;
    }

    public override List<IMappable> At(Point p)
    {
        return Fields[p];
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
}

//namespace Simulator.Maps;

//public abstract class SmallMap : Map
//{
//    private List<IMappable>?[,] _fields;

//    public List<IMappable>?[,] Fields { get => _fields; set => _fields = value; }

//    public SmallMap(int sizeX, int sizeY) : base(sizeX, sizeY)
//    {
//        if (sizeX > 20)
//        {
//            throw new ArgumentOutOfRangeException(nameof(sizeX), "Too wide");
//        }
//        if (sizeY > 20)
//        {
//            throw new ArgumentOutOfRangeException(nameof(sizeY), "Too tall");
//        }

//        Fields = new List<IMappable>[sizeX, sizeY];
//    }

//    public override void Add(IMappable mappable, Point p)
//    {
//        if (Fields[p.X, p.Y] == null)
//            Fields[p.X, p.Y] = new List<IMappable>();
//        Fields[p.X, p.Y]?.Add(mappable);
//    }

//    public override void Remove(IMappable mappable, Point p)
//    {
//        Fields[p.X, p.Y]?.Remove(mappable);
//        if (Fields[p.X, p.Y]?.Count == 0)
//            Fields[p.X, p.Y] = null;
//    }

//    public override List<IMappable> At(Point p)
//    {
//        return Fields[p.X, p.Y];
//    }

//    public override void At(int x, int y)
//    {
//        var point = new Point(x, y);
//        At(point);
//    }

//    public override void Move(IMappable mappable, Point startPoint, Point endPoint)
//    {
//        Remove(mappable, startPoint);
//        Add(mappable, endPoint);
//    }
//}