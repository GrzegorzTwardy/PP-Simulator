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

        for (int i = Map.SizeY - 1 - 1; i >= 0; i--)
        {
            DrawRow(Box.Vertical, i, Box.Vertical);
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

    private void DrawRow(char verticalBorder, int row, char verticalBorderEnd)
    {
        Console.Write(verticalBorder);
        for (int j = 0; j < Map.SizeX - 1; j++)
        {
            DrawCreature(j, row);
            Console.Write(verticalBorder);
        }
        Console.Write(' ');
        Console.WriteLine(verticalBorderEnd);
    }

    private void DrawCreature(int x, int y)
    {
        var listOfCreatures = new List<IMappable>();
        if (Map.At(new Point(x, y)) != null)
        {
            listOfCreatures = Map.At(new Point(x, y));
            if (listOfCreatures.Count > 0)
            {
                if (listOfCreatures.Count > 1)
                    Console.WriteLine('X');
                else if (listOfCreatures[0].GetType().Name == "Orc")
                    Console.WriteLine('O');
                else Console.WriteLine('E');
            }
            else
                Console.WriteLine(' ');
        }
        else
            Console.WriteLine(' ');
    }
}
