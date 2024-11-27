using Simulator.Maps;

namespace Simulator;

public class Simulation
{
    private readonly List<Direction> _parsedMoves;
    private int _turnIndex = 0;
    /// <summary>
    /// Simulation's map.
    /// </summary>
    public Map Map { get; }

    /// <summary>
    /// Creatures moving on the map.
    /// </summary>
    public List<Creature> Creatures { get; }

    /// <summary>
    /// Starting positions of creatures.
    /// </summary>
    public List<Point> Positions { get; }

    /// <summary>
    /// Cyclic list of creatures moves. 
    /// Bad moves are ignored - use DirectionParser.
    /// First move is for first creature, second for second and so on.
    /// When all creatures make moves, 
    /// next move is again for first creature and so on.
    /// </summary>
    public string Moves { get; }

    /// <summary>
    /// Has all moves been done?
    /// </summary>
    public bool Finished = false;

    /// <summary>
    /// Creature which will be moving current turn.
    /// </summary>
    Creature CurrentCreature => Creatures[_turnIndex % Creatures.Count];

    /// <summary>
    /// Lowercase name of direction which will be used in current turn.
    /// </summary>
    public string CurrentMoveName => _parsedMoves[_turnIndex % Creatures.Count].ToString().ToLower();
    /// <summary>
    /// Simulation constructor.
    /// Throw errors:
    /// if creatures' list is empty,
    /// if number of creatures differs from 
    /// number of starting positions.
    /// </summary>
    public Simulation(Map map, List<Creature> creatures,
        List<Point> positions, string moves)
    {
        if (creatures.Count == 0)
            throw new ArgumentException("Creatures' list is empty");
        if (creatures.Count != positions.Count)
            throw new ArgumentException("Number of creatures differs from number of starting positions");
        if (map == null)
            throw new ArgumentException("There is no map selected");

        Map = map;
        Creatures = creatures;
        Positions = positions;
        _parsedMoves = DirectionParser.Parse(moves);
        _turnIndex = 0;
        
        //set up starting positions
        for (int i = 0; i < creatures.Count; i++)
        {
            Creatures[i].InitMapandPosition(Map, Positions[i]);
        }
    }

    /// <summary>
    /// Makes one move of current creature in current direction.
    /// Throw error if simulation is finished.
    /// </summary>
    public void Turn(bool enableDiagonal)
    {
        if (Finished)
            throw new InvalidOperationException("Simulation is already finished");
        if (_turnIndex == _parsedMoves.Count)
        {
            Finished = true;
            return;
        }
        else
        {
            //Creatures[_turnIndex % Creatures.Count].Go(_parsedMoves[_turnIndex], enableDiagonal);
            CurrentCreature.Go(_parsedMoves[_turnIndex], enableDiagonal);
            Console.WriteLine(CurrentCreature.Position);
            _turnIndex++;
        }
    }
}
