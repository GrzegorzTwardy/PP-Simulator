namespace Simulator.Maps;

public interface IMappable
{
    public abstract string Info { get; }
    public Point Position { get; }
    void Go(Direction direction);
    void InitMapandPosition(Map map, Point position);
}
