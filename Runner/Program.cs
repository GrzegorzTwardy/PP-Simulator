using Simulator.Maps;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Simulator;

internal class Program
{
    static void Lab5a()
    {
        int[] c = [-2, -2, 2, 2, 3, 1, 5, 6, 6, -4, 0, 7, 2, 10, 5, -4, 2, 2, -2, -3, 2, 3, 2, 10];
        Point p3 = new(0, 0);

        for (int i = 0; i < c.Length-3; i+=4)
        {
            Point p1 = new(c[i], c[i+1]);
            Point p2 = new(c[i+2], c[i+3]);

            Console.WriteLine($" - Punkty {p1}, {p2}");

            try
            {
                Rectangle r = new Rectangle(p1, p2);
                Console.WriteLine("Prostokąt: " + r.ToString() );

                if (r.Contains(p3))
                    Console.WriteLine($"Punkt {p3} zawiera się w prostokącie.");
                else
                    Console.WriteLine($"Punkt {p3} NIE zawiera się w prostokącie.");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("prostokąt nie isntieje");
            }
            Console.WriteLine("\n");
        }
    }

    static void Lab5b()
    {
        int[] sizes = [2, 5, 7];
        Point p = new(2, 6);
        Point a = new(1, 1);
        Point w = new(5, 0);
        Direction d = Direction.Right;
        foreach (int s in sizes)
        {
            try
            {
                SmallSquareMap map = new SmallSquareMap(s);
                Console.WriteLine("Rozmiar mapy: " + map.Size);
                Console.WriteLine($"Czy istnieje {p.ToString()}: " + map.Exist(p));
                Console.WriteLine($"Punkt na prawo od {a.ToString()}: " + map.Next(a, d));
                Console.WriteLine($"Punkt w prawo w dół od {a.ToString()}: " + map.NextDiagonal(a, d));
                Console.WriteLine($"Punkt w prawo w dół od {w.ToString()}: " + map.NextDiagonal(w, d) + "\n");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Błąd: " + ex.Message);
            }
        }
    }

    static void cw2Lab6()
    {
        var o1 = new Orc();
        var o2 = new Orc("Orc2");
        var point = new Point(2, 2);
        var map = new SmallSquareMap(20);
        map.Add(o1, point);
        map.Add(o2, point);
        map.At(point);
        map.Remove(o2, point);
        Console.WriteLine('\n');
        map.At(point.X, point.Y);
        Console.WriteLine('\n');
        map.Move(o1, point, new Point(4, 3));
        Console.WriteLine($"Old point: {point.X}:{point.Y}");
        map.At(point);
        Console.WriteLine("New point: 4:3");
        map.At(4, 3);
    }

    static void Lab7a()
    {
        var mappables = new List<IMappable>() {new Orc("c1"), new Elf("c2"), new Orc("c3")};
        var mapa = new SmallSquareMap(15);
        var positions = new List<Point>() { new Point(0, 0), new Point(6, 2), new Point(14, 3) };
        var simulation = new Simulation(mapa, mappables, positions, "rurll"); // 1;0, 6;3, 0;0, 0;0, 5;3
        while (!simulation.Finished)
        {
            simulation.Turn();
        }
    }

    static void Lab12()
    {
        //var options = new JsonSerializerOptions
        //{
        //    WriteIndented = true,
        //    ReferenceHandler = ReferenceHandler.Preserve
        //};

        //Orc o1 = new("Gorbag", 3, 5);
        //Orc o2 = new("Morgash", 2, 7);

        //List<Orc> orcs = new() { o1, o2, o1 };
        //Console.WriteLine(orcs[0] == orcs[2]); // True

        //string json = JsonSerializer.Serialize(orcs, options);
        //Console.WriteLine("\nJSON:");
        //Console.WriteLine(json);

        //List<Orc> deserialized =
        //    JsonSerializer.Deserialize<List<Orc>>(json, options)!;

        //Console.Write("\nReference preserved:");
        //Console.WriteLine(deserialized[0] == deserialized[2]);

        var options = new JsonSerializerOptions { WriteIndented = true };

        List<IMappable> mapables = [
            new Orc("Gorbag", 3, 5),
            new Elf("Elandor", 2, 7),
            new Animals { Description = "Rasbbits", Size = 10 },
            new Birds { Description = "Eagles", Size = 15 },
            new Birds { Description = "Emu", Size = 8, CanFly = false }
        ];

        string json = JsonSerializer.Serialize(mapables, options);
        Console.WriteLine("\nJSON:");
        Console.WriteLine(json);

        List<IMappable> deserialized =
            JsonSerializer.Deserialize<List<IMappable>>(json, options)!;
    }

    static void Main(string[] args)
    {
        Lab12();
    }
}
