using System.Collections.Frozen;

namespace Day2;

public class Game
{
    public int Id { get; init; }
    public FrozenSet<HandfulCubes> HandfulCubesSets { get; init; }
}