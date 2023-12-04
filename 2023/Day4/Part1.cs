using Lib;

namespace Day4;

public class Part1 : IPart
{
    public int Execute()
    {
        var cards = ReadCards();
        var endResult = 0;

        foreach (var card in cards)
        {
            var result = 0;
            foreach (var number in card.MyNumbers)
            {
                if (card.WinningNumbers.Contains(number))
                {
                    if (result == 0)
                    {
                        result = 1;
                    }
                    else
                    {
                        result *= 2;
                    }
                }
            }

            endResult += result;
        }
        return endResult;
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