using Simulator.Maps;

namespace Simulator;
public class SimulationHistory
{
    private Simulation _simulation { get; }
    public int SizeX { get; }
    public int SizeY { get; }
    public List<SimulationTurnLog> TurnLogs { get; } = [];
    // store starting positions at index 0

    public SimulationHistory(Simulation simulation)
    {
        _simulation = simulation ??
            throw new ArgumentNullException(nameof(simulation));
        SizeX = _simulation.Map.SizeX;
        SizeY = _simulation.Map.SizeY;
        Run();
    }

    private void Run()
    {
        // implement
        List<SimulationTurnLog> turnLogList = [];

        while (!_simulation.Finished)
        {
            Dictionary<Point, char> Symbols = new();

            for (int x = 0; x < SizeX; x++)
            {
                for (int y = 0; y < SizeY; y++)
                {
                    var currentPoint = new Point(x, y);
                    var mappablesAtPoint = _simulation.Map.At(currentPoint);
                    if (mappablesAtPoint.Count > 1)
                    {
                        Symbols.Add(currentPoint, 'X');
                    }
                    else if (mappablesAtPoint.Count == 1)
                    {
                        Symbols.Add(currentPoint, _simulation.CurrentMappable.Symbol);
                    }
                    else
                    {
                        Symbols.Add(currentPoint, ' ');
                    }
                }
            }

            string mappableName = _simulation.CurrentMappable switch
            {
                Creature C => C.Name,
                Animals A => A.Description,
                _ => "Unknown"
            };

            var turnLog = new SimulationTurnLog(mappableName, _simulation.CurrentMoveName?.ToString(), Symbols);
            TurnLogs.Add(turnLog);

            _simulation.Turn();
        }

        //testHistory();
    }
    /// <summary>
    /// State of map after single simulation turn.
    /// </summary>
    public class SimulationTurnLog
    {
        /// <summary>
        /// Text representastion of moving object in this turn.
        /// CurrentMappable.ToString()
        /// </summary>
        //public required string Mappable { get; init; }
        public string Mappable { get; init; }
        /// <summary>
        /// Text representation of move in this turn.
        /// CurrentMoveName.ToString();
        /// </summary>
        //public required string Move { get; init; }
        public string Move { get; init; }
        /// <summary>
        /// Dictionary of IMappable.Symbol on the map in this turn.
        /// </summary>
        //public required Dictionary<Point, char> Symbols { get; init; }
        public Dictionary<Point, char> Symbols { get; init; }


        public SimulationTurnLog(string mappable, string? move, Dictionary<Point, char> symbols)
        {
            Mappable = mappable ?? throw new ArgumentNullException(nameof(mappable));
            Move = move ?? throw new ArgumentNullException(nameof(move));
            Symbols = symbols ?? throw new ArgumentNullException(nameof(symbols));
        }
    }

    private void testHistory()
    {
        for (int i = 0; i < TurnLogs.Count; i++)
        {
            Console.WriteLine($"===== TURN {i + 1} ======");
            Console.WriteLine(TurnLogs[i].Mappable);
            Console.WriteLine(TurnLogs[i].Move);
            Console.WriteLine(TurnLogs[i].Symbols[new Point(0, 0)]);
            Console.WriteLine();
        }
    }
}