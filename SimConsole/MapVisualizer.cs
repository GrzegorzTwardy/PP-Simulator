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

        for (int i = Map.SizeY - 1; i >= 0; i--)
        {
            DrawRow(Box.Vertical, Box.Vertical, i);
            DrawBorder(Box.MidLeft, Box.Cross, Box.MidRight);
        }
        DrawRow(Box.Vertical, Box.Vertical, Map.SizeY - 1);
        DrawBorder(Box.BottomLeft, Box.BottomMid, Box.BottomRight);
    }

    private void DrawBorder(char leftCorner, char middle, char rightCorner)
    {
        Console.Write(leftCorner);
        for (int i = 0; i < Map.SizeX-1; i++)
        {
            Console.Write(Box.Horizontal);
            Console.Write(middle);
        }
        Console.Write(Box.Horizontal);
        Console.WriteLine(rightCorner);
    }

    private void DrawRow(char verticalBorder, char verticalBorderEnd, int row)
    {
        Console.Write(verticalBorder);
        for (int j = 0; j < Map.SizeX-1; j++)
        {
            Console.Write(DrawSymbol(j, row));
            Console.Write(verticalBorder);
        }
        Console.Write(DrawSymbol(Map.SizeX - 1, row));
        Console.WriteLine(verticalBorderEnd);
    }

    private char DrawSymbol(int x, int y)
    {
        var mappablesAtPoint = Map.At(new Point(x, y));
        if (mappablesAtPoint == null || mappablesAtPoint.Count == 0)
            return ' ';

        if (mappablesAtPoint.Count > 1)
            return 'X';
        else if (mappablesAtPoint[0].GetType().Name == "Orc")
            return 'O';
        else return 'E';
    }
}
