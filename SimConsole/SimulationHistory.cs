using SimConsole;
using Simulator.Maps;
using Simulator;

public class SimulationHistory
{
    // Lista do przechowywania map i danych o ruchach w poszczególnych turach
    private List<(Dictionary<Point, List<IMappable>> mapState, string mappableType, string moveName)> _turnsData = new();

    public Simulation Sim { get; }

    public SimulationHistory(Simulation sim)
    {
        Sim = sim;
        MapVisualizer mapVisualizer = new(sim.Map);

        Console.WriteLine("Starting Positions: ");
        mapVisualizer.Draw();
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();

        // Rozpoczęcie symulacji
        while (!sim.Finished)
        {
            Console.WriteLine($"<{sim.CurrentMappable.GetType().Name}>{sim.CurrentMappable.Position}: {sim.CurrentMoveName} ");
            sim.Turn();
            SaveTurn();
            mapVisualizer.Draw();
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        // Przykłady wywołania DrawTurn z konkretnymi turami
        DrawTurn(5);
        DrawTurn(10);
        DrawTurn(15);
        DrawTurn(20);
    }
    private Dictionary<Point, List<IMappable>> CopyMapState(Dictionary<Point, List<IMappable>> originalState)
    {
        var copiedState = new Dictionary<Point, List<IMappable>>();

        foreach (var entry in originalState)
        {
            var copiedList = new List<IMappable>();

            foreach (var mappable in entry.Value)
            {
                if (mappable is Elf elf)
                {
                    copiedList.Add(new Elf());
                }
                else if(mappable is Orc orc)
                {
                    copiedList.Add(new Elf());
                }
                else if (mappable is Animals animals)
                {
                    if (animals is Birds birds)
                    {
                        copiedList.Add(new Birds("Unknown"));
                    }
                    else copiedList.Add(new Animals("Unknown"));
                }
                else
                {
                    throw new NotImplementedException($"Copy not implemented for type {mappable.GetType()}");
                }
            }

            copiedState[entry.Key] = copiedList;
        }

        return copiedState;
    }

    public void SaveTurn()
    {
        var mapState = new Dictionary<Point, List<IMappable>>();

        if (Sim.Map != null)
        {
            if (Sim.Map is BigMap bigMap)
            {
                mapState = CopyMapState(bigMap.Fields); // Głębokie kopiowanie mapy
            }
            else if (Sim.Map is SmallMap smallMap)
            {
                mapState = CopyMapState(smallMap.Fields); // Głębokie kopiowanie mapy
            }
        }

        // Zapisujemy aktualny mappable i jego ruch
        _turnsData.Add((
            mapState,
            Sim.CurrentMappable.GetType().Name, // Zapisujemy nazwę typu mappable
            Sim.CurrentMoveName                // Zapisujemy nazwę ruchu
        ));
    }


    // Pobiera dane z konkretnej tury
    public (Dictionary<Point, List<IMappable>> mapState, string mappableType, string moveName) GetTurnData(int turn)
    {
        if (turn > 0 && turn <= _turnsData.Count)
            return _turnsData[turn - 1];

        throw new IndexOutOfRangeException($"{turn} - this turn doesn't exist.");
    }

    // Rysuje mapę z danej tury, wyświetlając również dane o mappable
    public void DrawTurn(int turn)
    {
        if (turn < 1 || turn > _turnsData.Count)
        {
            Console.WriteLine("Invalid turn.");
            return;
        }

        // Pobieramy dane z zapisanej tury
        var (turnState, mappableType, moveName) = _turnsData[turn - 1];

        // Tworzymy tymczasową mapę
        var tempMap = CreateTempMap(Sim.Map);

        // Kopiowanie stanu mapy
        CopyStateToTempMap(turnState, tempMap);

        // Tworzymy MapVisualizer i rysujemy mapę
        MapVisualizer mapVisualizer = new(tempMap);

        // Wypisujemy informacje o turze
        Console.WriteLine($"\n============= TURN {turn} ============");
        Console.WriteLine($"Current Mappable: {mappableType}");
        Console.WriteLine($"Current Move: {moveName}");

        mapVisualizer.Draw();
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    // Metoda tworząca tymczasową mapę o tym samym typie co oryginalna mapa
    private Map CreateTempMap(Map originalMap)
    {
        var mapType = originalMap.GetType();
        var constructor = mapType.GetConstructor(new[] { typeof(int), typeof(int) });

        if (constructor != null)
        {
            return (Map)constructor.Invoke(new object[] { originalMap.SizeX, originalMap.SizeY });
        }

        throw new NotImplementedException($"Nie obsługuję mapy typu: {mapType}");
    }

    // Kopiuje stan mapy z jednej mapy do innej
    private void CopyStateToTempMap(Dictionary<Point, List<IMappable>> state, Map tempMap)
    {
        if (tempMap is BigMap bigMap)
        {
            foreach (var field in state)
            {
                bigMap.Fields[field.Key] = new List<IMappable>(field.Value);
            }
        }
        else if (tempMap is SmallMap smallMap)
        {
            foreach (var field in state)
            {
                smallMap.Fields[field.Key] = new List<IMappable>(field.Value);
            }
        }
        else
        {
            throw new NotImplementedException("Unsupported map type.");
        }
    }
}


//using Simulator.Maps;
//using Simulator;
//using SimConsole;
//using System;
//using System.Collections.Generic;
//using System.Reflection;

//namespace SimConsole
//{
//    public class SimulationHistory
//    {
//        // Lista map (stanów mapy w danej turze)
//        private List<Map> _turnsMap = new List<Map>();

//        public Simulation Sim { get; }

//        public SimulationHistory(Simulation sim)
//        {
//            Sim = sim;
//            MapVisualizer mapVisualizer = new(sim.Map);

//            Console.WriteLine("Starting Positions: ");
//            mapVisualizer.Draw();
//            Console.WriteLine("Press any key to continue...");
//            Console.ReadLine();
//            while (!sim.Finished)
//            {
//                Console.WriteLine($"<{sim.CurrentMappable.GetType().Name}>{sim.CurrentMappable.Position}: {sim.CurrentMoveName} ");
//                sim.Turn();
//                SaveTurn();
//                mapVisualizer.Draw();
//                Console.WriteLine("Press any key to continue...");
//                Console.ReadLine();
//            }

//            // Możliwość rysowania konkretnych tur
//            DrawTurn(5);
//            DrawTurn(10);
//            DrawTurn(15);
//            DrawTurn(20);
//        }

//        // Zapisuje stan mapy w danej turze
//        public void SaveTurn()
//        {
//            // Tworzymy nową mapę, która będzie kopią bieżącej mapy
//            Map mapStateCopy = CreateTempMap(Sim.Map);

//            // Kopiowanie stanu mapy: przechodzimy przez wszystkie pola mapy
//            if (Sim.Map is BigMap bigMap)
//            {
//                foreach (var field in bigMap.Fields)
//                {
//                    // Upewniamy się, że tworzymy kopię listy, a nie tylko referencję
//                    if (mapStateCopy is BigMap tempBigMap)
//                    {
//                        tempBigMap.Fields[field.Key] = new List<IMappable>(field.Value);
//                    }
//                }
//            }
//            else if (Sim.Map is SmallMap smallMap)
//            {
//                foreach (var field in smallMap.Fields)
//                {
//                    // Upewniamy się, że tworzymy kopię listy, a nie tylko referencję
//                    if (mapStateCopy is SmallMap tempSmallMap)
//                    {
//                        tempSmallMap.Fields[field.Key] = new List<IMappable>(field.Value);
//                    }
//                }
//            }

//            // Dodajemy skopiowaną mapę do listy tur
//            _turnsMap.Add(mapStateCopy);
//        }

//        // Pobiera stan mapy z danej tury
//        public Map GetTurn(int turn)
//        {
//            if (turn > 0 && turn <= _turnsMap.Count)
//                return _turnsMap[turn - 1];

//            throw new IndexOutOfRangeException($"{turn} - this turn doesn't exist.");
//        }

//        Rysuje stan mapy z danej tury
//        public void DrawTurn(int turn)
//        {
//            // Sprawdzamy, czy tura jest poprawna
//            if (turn < 1 || turn > _turnsMap.Count)
//            {
//                Console.WriteLine("Invalid turn.");
//                return;
//            }

//            // Pobieramy stan mapy z danej tury
//            Map turnState = _turnsMap[turn - 1];

//            // Tworzymy MapVisualizer z mapy z danej tury
//            MapVisualizer mapVisualizer = new(turnState);

//            // Wyświetlamy informacje o turze
//            Console.WriteLine($"\n============= TURN {turn} ============");

//            // Rysujemy mapę
//            mapVisualizer.Draw();

//            Console.WriteLine("Press any key to continue...");
//            Console.ReadLine();
//        }


//        // Tworzy nową mapę na podstawie typu oryginalnej mapy
//        private Map CreateTempMap(Map originalMap)
//        {
//            var mapType = originalMap.GetType();

//            // Zastosowanie refleksji, aby dynamicznie utworzyć mapę
//            var constructor = mapType.GetConstructor(new[] { typeof(int), typeof(int) });

//            if (constructor != null)
//            {
//                return (Map)constructor.Invoke(new object[] { originalMap.SizeX, originalMap.SizeY });
//            }

//            throw new NotImplementedException($"Nie obsługuję mapy typu: {mapType}");
//        }

//        // Kopiuje stan z oryginalnej mapy do mapy pomocniczej
//        private void CopyStateToTempMap(Map tempMap)
//        {
//            if (tempMap is BigMap bigMap)
//            {
//                // Kopiowanie stanu dla mapy BigMap
//                // Sprawdzamy, czy pole "Fields" jest dostępne w BigMap
//                foreach (var field in bigMap.Fields)
//                {
//                    bigMap.Fields[field.Key] = new List<IMappable>(field.Value);
//                }
//            }
//            else if (tempMap is SmallMap smallMap)
//            {
//                // Kopiowanie stanu dla mapy SmallMap
//                foreach (var field in smallMap.Fields)
//                {
//                    smallMap.Fields[field.Key] = new List<IMappable>(field.Value);
//                }
//            }
//            else
//            {
//                throw new NotImplementedException("Unsupported map type.");
//            }
//        }
//    }
//}












//using Simulator.Maps;
//using Simulator;
//using SimConsole;
//using System;
//using System.Reflection;
//namespace SimConsole;

//public class SimulationHistory
//{
//    // _turns[[mapa-fields]]
//    private List<Dictionary<Point, List<IMappable>>> _maps = [];

//    public Simulation Sim { get; }
//    public SimulationHistory(Simulation sim)
//    {
//        Sim = sim;
//        MapVisualizer mapVisualizer = new(sim.Map);

//        Console.WriteLine("Starting Positions: ");
//        mapVisualizer.Draw();
//        Console.WriteLine("Press any key to continue...");
//        Console.ReadLine();
//        while (!sim.Finished)
//        {
//            Console.WriteLine($"<{sim.CurrentMappable.GetType().Name}>{sim.CurrentMappable.Position}: {sim.CurrentMoveName} ");
//            sim.Turn();
//            SaveTurn();
//            mapVisualizer.Draw();
//            Console.WriteLine("Press any key to continue...");
//            Console.ReadLine();
//        }
//        DrawTurn(5);
//        DrawTurn(10);
//        DrawTurn(15);
//        DrawTurn(20);
//    }

//    public void SaveTurn()
//    {
//        var mapState = new Dictionary<Point, List<IMappable>>();
//        if (Sim.Map != null)
//        {
//            if (Sim.Map is BigMap bigMap)
//            {
//                foreach (var field in bigMap.Fields)
//                {
//                    mapState[field.Key] = new List<IMappable>(field.Value);
//                }
//            }
//            else if (Sim.Map is SmallMap smallMap)
//            {
//                foreach (var field in smallMap.Fields)
//                {
//                    mapState[field.Key] = new List<IMappable>(field.Value);
//                }

//            }
//            _maps.Add(mapState);
//        }
//    }

//    public Dictionary<Point, List<IMappable>> GetTurn(int turn)
//    {
//        if (turn > 0 && turn <= _maps.Count)
//            return _maps[turn-1];
//        throw new IndexOutOfRangeException($"{turn} - this turn doesn't exist.");
//    }


//    public void DrawTurn(int turn)
//    {
//        if (turn < 1 || turn > _maps.Count)
//        {
//            Console.WriteLine("Invalid turn.");
//            return;
//        }
//        var turnState = _maps[turn-1];
//        var tempMap = CreateTempMap(Sim.Map);
//        CopyStateToTempMap(turnState, tempMap);

//        MapVisualizer mapVisualizer = new(tempMap);

//        Console.WriteLine($"\n============= TURN {turn} ============");
//        mapVisualizer.Draw();
//        Console.WriteLine("Press any key to continue...");
//        Console.ReadLine();
//    }

//    private Map CreateTempMap(Map originalMap)
//    {
//        var mapType = originalMap.GetType();

//        var constructor = mapType.GetConstructor(new[] { typeof(int), typeof(int) });

//        if (constructor != null)
//        {
//            return (Map)constructor.Invoke(new object[] { originalMap.SizeX, originalMap.SizeY });
//        }

//        throw new NotImplementedException($"Nie obsługuję mapy typu: {mapType}");
//    }

//    private void CopyStateToTempMap(Dictionary<Point, List<IMappable>> state, Map tempMap)
//    {
//        if (tempMap is BigMap bigMap)
//        {
//            foreach (var field in state)
//            {
//                bigMap.Fields[field.Key] = new List<IMappable>(field.Value);
//            }
//        }
//        else if (tempMap is SmallMap smallMap)
//        {
//            foreach (var field in state)
//            {
//                smallMap.Fields[field.Key] = new List<IMappable>(field.Value);
//            }
//        }
//        else
//        {
//            throw new NotImplementedException("Unsupported map type.");
//        }
//    }
//}

//public class SimulationHistory
//{
//    // _turns[[nr_tury], [mapa-fields], [string]]
//    //private List<Dictionary<Point, List<IMappable>>> _turns = new([]);
//    private List<Map> _turns = new([]);
//    private Map mapToDisplay;
//    private List<Direction> _moves;
//    public Simulation Sim { get; }
//    public SimulationHistory(Simulation sim)
//    {
//        Sim = sim;
//    }

//    public void SaveTurn()
//    {
//        var currentTurnFields = new Dictionary<Point, List<IMappable>>();

//        if (Sim.Map is BigMap bigMap)
//        {
//            foreach (var entry in bigMap.Fields)
//                currentTurnFields[entry.Key] = new List<IMappable>(entry.Value);
//        }
//        else if (Sim.Map is SmallMap smallMap)
//        {
//            foreach (var entry in smallMap.Fields)
//                currentTurnFields[entry.Key] = new List<IMappable>(entry.Value);
//        }
//        _turns.Add(currentTurnFields);
//    }

//    public Dictionary<Point, List<IMappable>> GetTurn(int turn)
//    {
//        if (turn > 0 && turn <= _turns.Count)
//            return _turns[turn - 1];
//        throw new IndexOutOfRangeException($"{turn} - this turn doesn't exist.");
//    }
//}
