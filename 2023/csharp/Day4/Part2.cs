using System.Collections.Frozen;
using Lib;

namespace Day4;

public class Part2 : IPart
{
    private IList<Card> _cards = new List<Card>();
    public int Execute()
    {
        _cards = ReadCards().ToList();
        var endResult = 0;

        for (int i = 0; i < _cards.Count; i++)
        {
            endResult += GetWinNumberCountOfCard(i);
        }
        
        return endResult;
    }

    private int GetWinNumberCountOfCard(int cardIndex)
    {
        if (cardIndex >= _cards.Count)
        {
            return 0;
        }
        var card = _cards[cardIndex];

        var winCount = card.MyNumbers.Count(n => card.WinningNumbers.Contains(n));
        var result = 1;
        for (var i = 1; i <= winCount; i++)
        {
            result += GetWinNumberCountOfCard(cardIndex + i);
        }

        return result;
    }

    private IEnumerable<Card> ReadCards()
    {
        var result = new List<Card>();
        using var reader = new StreamReader("./input.txt");
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (line == null) continue;
            var numbers = line.Split(":")[1];
            var splitNumbers = numbers.Split("|");
            var winningNumbers = splitNumbers[0].Trim().Split(" ").Where(s => s != string.Empty).Select(int.Parse).ToList();
            var myNumbers = splitNumbers[1].Trim().Split(" ").Where(s => s != string.Empty).Select(int.Parse).ToList();
            result.Add(new Card()
            {
                WinningNumbers = winningNumbers,
                MyNumbers = myNumbers
            });
        }

        return result;
    }
}