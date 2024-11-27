using Simulator;
using Simulator.Maps;
namespace SimConsole;

public class MapVisualizer
{
    public Map Map { get; }
    public MapVisualizer(Map map)
    {
        Map = map ?? throw new NullReferenceException(nameof(map));
    }

    public void Draw()
    {
        DrawBorder(Box.TopLeft, Box.TopMid, Box.TopRight);

        for (int i = 0; i < Map.SizeY - 1; i++)
        {
            DrawRow(Box.Vertical, ' ', Box.Vertical);
            DrawBorder(Box.MidLeft, Box.Cross, Box.MidRight);
        }
        DrawRow(Box.Vertical, ' ', Box.Vertical);
        DrawBorder(Box.BottomLeft, Box.BottomMid, Box.BottomRight);
    }

    private void DrawBorder(char leftCorner, char middle, char rightCorner)
    {
        Console.Write(leftCorner);
        for (int i = 0; i < Map.SizeX - 1; i++)
        {
            Console.Write(Box.Horizontal);
            Console.Write(middle);
        }
        Console.Write(Box.Horizontal);
        Console.WriteLine(rightCorner);
    }

    private void DrawRow(char verticalBorder, char space, char verticalBorderEnd)
    {
        Console.Write(verticalBorder);
        for (int j = 0; j < Map.SizeX - 1; j++)
        {
            Console.Write(space);
            Console.Write(verticalBorder);
        }
        Console.Write(' ');
        Console.WriteLine(verticalBorderEnd);
    }
}
