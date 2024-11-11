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

    static void Main(string[] args)
    {
        Lab5a();
    }
}
