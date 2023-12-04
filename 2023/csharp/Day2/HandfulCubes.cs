using System.Collections.Frozen;

namespace Day2;

public class HandfulCubes
{
    public required FrozenDictionary<CubeColor, int> Cubes { get; init; }
}