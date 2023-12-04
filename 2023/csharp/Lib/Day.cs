using Lib;

namespace Lib;

public class Day(int dayNumber, IPart part1, IPart? part2 = null)
{
    public void Run()
    {
        Console.WriteLine($"###### Advent of Code 2023 Day {dayNumber} ######\n");
        Console.WriteLine("## Part 1 ##");
        var result1 = part1.Execute();
        Console.WriteLine($"Result: {result1}");

        if (part2 != null)
        {
            Console.WriteLine("\n## Part 2 ##");
            var result2 = part2.Execute();
            Console.WriteLine($"Result: {result2}");
        }
    }
}