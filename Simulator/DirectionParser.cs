namespace Simulator;

public static class DirectionParser
{
    public static Direction[] Parse(string d)
    {
        int validSize = 0;

        foreach (char c in d.ToLower())
        {
            if (c == 'r' || c == 'l' || c == 'u' || c == 'd')
                validSize++;
        }
        
        Direction[] result = new Direction[validSize];

        int i = 0;

        foreach (char c in d.ToLower())
        {
            switch (c)
            {
                case 'r':
                    result[i] = Direction.Right;
                    i++;
                    break;
                case 'l':
                    result[i] = Direction.Left;
                    i++;
                    break;
                case 'u':
                    result[i] = Direction.Up;
                    i++;
                    break;
                case 'd':
                    result[i] = Direction.Down;
                    i++;
                    break;
            }
        }
        return result;
    }
}
