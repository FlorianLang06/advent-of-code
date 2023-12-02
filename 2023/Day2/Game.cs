using System.Collections.Frozen;

namespace Day2;

public class Game
{
    public int Id { get; init; }
    public required FrozenSet<HandfulCubes> HandfulCubesSets { get; init; }
}