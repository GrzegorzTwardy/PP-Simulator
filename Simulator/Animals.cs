namespace Simulator;

public class Animals
{
    private string description = "Unknown";
    public required string Description
    {
        get
        {
            return description;
        }
        init
        {
            value = value.Trim();

            if (value.Length > 0)
            {
                if (value.Length > 15)
                {
                    value = value[..15].TrimEnd();
                }
                if (value.Length < 3)
                {
                    value = value.PadRight(3, '#');
                }
                description = char.ToUpper(value[0]) + value.Substring(1);
            }
        }
    }
    public uint Size { get; set; } = 3;
    public string Info => $"{Description} <{Size}>";
}
