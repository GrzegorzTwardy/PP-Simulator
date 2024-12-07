﻿using Simulator.Maps;

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
    /// IMappable moving on the map.
    /// </summary>
    public List<IMappable> Mappables { get; }

    /// <summary>
    /// Starting positions of mappables.
    /// </summary>
    public List<Point> Positions { get; }

    /// <summary>
    /// Cyclic list of mappables moves. 
    /// Bad moves are ignored - use DirectionParser.
    /// First move is for first mappable, second for second and so on.
    /// When all mappables make moves, 
    /// next move is again for first mappable and so on.
    /// </summary>
    public string Moves { get; }

    /// <summary>
    /// Has all moves been done?
    /// </summary>
    public bool Finished = false;

    /// <summary>
    /// Creature which will be moving current turn.
    /// </summary>
    public IMappable CurrentMappable => Mappables[_turnIndex % Mappables.Count];

    /// <summary>
    /// Lowercase name of direction which will be used in current turn.
    /// </summary>
    //public string CurrentMoveName => _parsedMoves[_turnIndex % Mappables.Count].ToString().ToLower();
    public string CurrentMoveName => _parsedMoves[_turnIndex].ToString().ToLower();
    /// <summary>
    /// Simulation constructor.
    /// Throw errors:
    /// if mappables' list is empty,
    /// if number of mappables differs from 
    /// number of starting positions.
    /// </summary>
    public Simulation(Map map, List<IMappable> mappables,
        List<Point> positions, string moves)
    {
        if (mappables.Count == 0)
            throw new ArgumentException("IMappables' list is empty");
        if (mappables.Count != positions.Count)
            throw new ArgumentException("Number of mappables differs from number of starting positions");
        if (map == null)
            throw new ArgumentException("There is no map selected");

        Map = map;
        Mappables = mappables;
        Positions = positions;
        _parsedMoves = DirectionParser.Parse(moves);
        _turnIndex = 0;
        
        //set up starting positions
        for (int i = 0; i < mappables.Count; i++)
        {
            Mappables[i].InitMapandPosition(Map, Positions[i]);
        }
    }

    /// <summary>
    /// Makes one move of current mappable in current direction.
    /// Throw error if simulation is finished.
    /// </summary>
    public void Turn()
    {
        if (Finished)
            throw new InvalidOperationException("Simulation is already finished");

        CurrentMappable.Go(_parsedMoves[_turnIndex]);

        if (_turnIndex == _parsedMoves.Count-1)
        {
            Finished = true;
            return;
        }
        _turnIndex++;
    }
}
