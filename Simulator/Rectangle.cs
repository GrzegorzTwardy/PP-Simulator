namespace Simulator;

public class Rectangle
{
    public int X1 { get; }
    public int Y1 { get; }
    public int X2 { get; }
    public int Y2 { get; }

    public Rectangle(int x1, int y1, int x2, int y2)
    {
        try
        {
            if (x1 == x2 || y1 == y2)
                throw new ArgumentException("Punkty nie mogą być współliniowe.");
        }
        catch (ArgumentException)
        {

        }
        finally
        {
            if ((x1 > x2) && (y1 > y2))
            {

            }
        }
    }
}
