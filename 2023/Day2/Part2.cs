using System.Collections.Frozen;
using Lib;

namespace Day2;

public class Part2 : IPart
{
    public int Execute()
    {
        using var reader = new StreamReader("./input.txt");
        var games = new List<Game>();
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (line == null) break;
            var game = ReadGame(line);
            games.Add(game);
        }

        var result = games.Select(g =>
            g.HandfulCubesSets.Select(h =>
                h.Cubes.FirstOrDefault(c => c.Key == CubeColor.red).Value
            ).Max()
            *
            g.HandfulCubesSets.Select(h =>
                h.Cubes.FirstOrDefault(c => c.Key == CubeColor.green).Value
            ).Max()
            *
            g.HandfulCubesSets.Select(h =>
                h.Cubes.FirstOrDefault(c => c.Key == CubeColor.blue).Value
            ).Max()
        ).Sum();
        return result;
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