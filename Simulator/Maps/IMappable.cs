namespace Simulator.Maps;

public interface IMappable
{
    string Info { get; }
    Point Position { get; }
    void Go(Direction direction);
    void InitMapandPosition(Map map, Point position);
}
