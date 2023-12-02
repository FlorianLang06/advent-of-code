using System.Collections.Frozen;
using Lib;

namespace Day2;

public class Part1 : IPart
{
    public void Execute()
    {
        using var reader = new StreamReader("./input1.txt");
        var games = new List<Game>();
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (line == null) break;
            var game = ReadGame(line);
            games.Add(game);
        }

        var maxCubes = new Dictionary<CubeColor, int>()
        {
            { CubeColor.red, 12 },
            { CubeColor.green, 13 },
            { CubeColor.blue, 14 }
        }.ToFrozenDictionary();

        var possibleGames = games.Where(g => g.HandfulCubesSets.All(h => h.Cubes.All(c => maxCubes[c.Key] >= c.Value)));
        var result = possibleGames.Select(g => g.Id).Sum();
        Console.WriteLine($"Result: {result}");
    }

    private Game ReadGame(string input)
    {
        int id;
        if (!Int32.TryParse(input.Substring(5, 3).Replace(":", ""), out id))
        {
            throw new InvalidCastException("Can't cast game id");
        }

        var handfulCubesResult = new List<HandfulCubes>();
        var rounds = input.Split(":")[1].Split(";");
        foreach (var round in rounds)
        {
            var cubeResult = new Dictionary<CubeColor, int>();
            var cubes = round.Split(",");
            foreach (var cube in cubes)
            {
                int amount;
                if (!Int32.TryParse(cube.Substring(1, 2), out amount))
                {
                    throw new InvalidCastException("Can't cast cube amount");
                }

                CubeColor color;
                if (!Enum.TryParse(cube.Substring(3), out color))
                {
                    throw new InvalidCastException("Can't cast cube color");
                }
                cubeResult.Add(color, amount);
            }
            handfulCubesResult.Add(new HandfulCubes()
            {
                Cubes = cubeResult.ToFrozenDictionary()
            });
        }
        
        return new Game()
        {
            Id = id,
            HandfulCubesSets = handfulCubesResult.ToFrozenSet()
        };
    }
}