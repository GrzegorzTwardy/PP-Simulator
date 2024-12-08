﻿namespace Simulator.Maps;

public abstract class SmallMap : Map
{
    private List<IMappable>?[,] _fields;

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

        _fields = new List<IMappable>[sizeX, sizeY];
    }

    public override void Add(IMappable mappable, Point p)
    {
        if (_fields[p.X, p.Y] == null)
            _fields[p.X, p.Y] = new List<IMappable>();
        _fields[p.X, p.Y]?.Add(mappable);
    }

    public override void Remove(IMappable mappable, Point p)
    {
        _fields[p.X, p.Y]?.Remove(mappable);
        if (_fields[p.X, p.Y]?.Count == 0)
            _fields[p.X, p.Y] = null;
    }

    public override List<IMappable> At(Point p)
    {
        return _fields[p.X, p.Y];
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