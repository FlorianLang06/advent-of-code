using System.Collections.Frozen;

namespace Day4;

public class Card
{
    public required List<int> WinningNumbers { get; init; }
    public required List<int> MyNumbers { get; init; }
}