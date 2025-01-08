using System.Text.Json.Serialization;

namespace Simulator.Maps;


[JsonPolymorphic(TypeDiscriminatorPropertyName = "Type")]
[JsonDerivedType(typeof(Elf), nameof(Elf))]
[JsonDerivedType(typeof(Orc), nameof(Orc))]
[JsonDerivedType(typeof(Animals), nameof(Animals))]
[JsonDerivedType(typeof(Birds), nameof(Birds))]
public interface IMappable
{
    char Symbol { get; }
    string Info { get; }
    Map? Map { get; }
    Point Position { get; }
    void Go(Direction direction);
    void InitMapandPosition(Map map, Point position);
    public string ToString();
}
