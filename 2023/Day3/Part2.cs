using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
using Lib;

namespace Day3;

public class Part2 : IPart
{
    private string[] _lines = Array.Empty<string>();
    public int Execute()
    {
        var input = File.ReadAllText("./input.txt");
        _lines = input.Split("\n");

        var numbers = new List<Numbers>();
        
        for (var i = 0; i < _lines.Length; i++)
        {
            var line = _lines[i];
            foreach (Match match in Regex.Matches(line, @"\b\d{1,3}\b"))
            {
                var result = IsPartNumber(match, line, i);
                if (result.index > -1)
                {
                    numbers.Add(new()
                    {
                        Value = int.Parse(match.Value),
                        GearIndex = result.index,
                        GearLineIndex = result.lineIndex
                    });
                }
            }
        }

        var grouped = numbers.GroupBy(n => (n.GearIndex, n.GearLineIndex));
        var endResult = 0;
        foreach (var group in grouped)
        {
            if (group.Count() == 2)
            {
                endResult += group.Select(n => n.Value).Aggregate((x, y) => x * y);
            }
        }
        
        return endResult;
    }

    private (int index, int lineIndex) IsPartNumber(Match match, string line, int lineIndex)
    {
        var indexBefore = match.Index - 1;
        if (indexBefore >= 0 && IsGear(line[indexBefore]))
        {
            return (match.Index - 1, lineIndex);
        }

        var indexAfter = match.Index + match.Length;
        if (indexAfter < line.Length && IsGear(line[indexAfter]))
        {
            return (match.Index + match.Length, lineIndex);
        }

        if (lineIndex > 0)
        {
            var result = GetLineGearIndex(match, lineIndex - 1);
            if (result.index > -1)
            {
                return result;
            }
        }
        
        if (lineIndex < _lines.Length - 1)
        {
            var result = GetLineGearIndex(match, lineIndex + 1);
            if (result.index > -1)
            {
                return result;
            }
        }

        return (-1, -1);
    }
    
    private bool IsGear(char symbol)
    {
        return symbol == '*';
    }

    private (int index, int lineIndex) GetLineGearIndex(Match match, int lineIndex)
    {
        var lineAbove = _lines[lineIndex];
        var startIndex = match.Index > 0 ? match.Index - 1 : match.Index;
        var length = match.Length;
        if (startIndex < match.Index) length++;
        if (match.Index + match.Length < lineAbove.Length) length++;

        var substring = lineAbove.Substring(startIndex, length);
        var chars = substring.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            if (IsGear(chars[i]))
            {
                return (startIndex + i, lineIndex);
            }
        }

        return (-1, -1);
    }
}

class Numbers
{
    public int Value { get; init; }
    public int GearIndex { get; init; }
    public int GearLineIndex { get; init; }
}