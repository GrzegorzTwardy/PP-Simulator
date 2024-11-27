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
            DrawRow(Box.Vertical, i, Box.Vertical);
            DrawBorder(Box.MidLeft, Box.Cross, Box.MidRight);
        }
        DrawRow(Box.Vertical, Map.SizeY - 1, Box.Vertical);
        DrawBorder(Box.BottomLeft, Box.BottomMid, Box.BottomRight);
    }

    private void DrawBorder(char leftCorner, char middle, char rightCorner)
    {
        Console.Write(leftCorner);
        for (int i = Map.SizeX - 1; i >= 0; i--)
        {
            Console.Write(Box.Horizontal);
            Console.Write(middle);
        }
        Console.Write(Box.Horizontal);
        Console.WriteLine(rightCorner);
    }

    private void DrawRow(char verticalBorder, int row, char verticalBorderEnd)
    {
        Console.Write(verticalBorder);
        for (int j = 0; j < Map.SizeX; j++)
        {
            Console.Write(DrawCreature(j, row));
            Console.Write(verticalBorder);
        }
        Console.Write(DrawCreature(Map.SizeX-1, row));
        Console.WriteLine(verticalBorderEnd);
    }

    private char DrawCreature(int x, int y)
    {
        var listOfCreatures = new List<IMappable>();
        if (Map.At(new Point(x, y)) != null)
        {
            listOfCreatures = Map.At(new Point(x, y));
            if (listOfCreatures.Count > 0)
            {
                if (listOfCreatures.Count > 1)
                    return 'X';
                else if (listOfCreatures[0].GetType().Name == "Orc")
                    return 'O';
                else return 'E';
            }
            else
                return ' ';
        }
        else
            return ' ';
    }
}
