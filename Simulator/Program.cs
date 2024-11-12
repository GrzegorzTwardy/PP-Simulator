﻿using Simulator.Maps;
using System.ComponentModel;

namespace Simulator;

internal class Program
{
    static void Lab4b()
    {
        object[] myObjects = {
        new Animals() { Description = "dogs"},
        new Birds { Description = "  eagles ", Size = 10 },
        new Elf("e", 15, -3),
        new Orc("morgash", 6, 4)
    };
        Console.WriteLine("\nMy objects:");
        foreach (var o in myObjects) Console.WriteLine(o);
        /*
            My objects:
            ANIMALS: Dogs <3>
            BIRDS: Eagles (fly+) <10>
            ELF: E## [10][0]
            ORC: Morgash [6][4]
        */
    }

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
                Console.WriteLine($"Punkt na skos do góry od {a.ToString()}: " + map.Next(w, d));
                Console.WriteLine($"Punkt na skos do góry od {w.ToString()}: " + map.NextDiagonal(a, d) + "\n");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Błąd: " + ex.Message);
            }
        }
    }

    static void Main(string[] args)
    {
        Lab5b();
    }
}
