using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
using Lib;

namespace Day3;

public class Part1 : IPart
{
    private string[] _lines = Array.Empty<string>();
    public int Execute()
    {
        var input = File.ReadAllText("./input.txt");
        _lines = input.Split("\n");

        var endResult = 0;
        
        for (var i = 0; i < _lines.Length; i++)
        {
            var line = _lines[i];
            foreach (Match match in Regex.Matches(line, @"\b\d{1,3}\b"))
            {
                var result = IsPartNumber(match, line, i);
                if (result)
                {
                    endResult += Int32.Parse(match.Value);
                }
            }
        }
        
        return endResult;
    }

    private bool IsPartNumber(Match match, string line, int lineIndex)
    {
        var indexBefore = match.Index - 1;
        if (indexBefore >= 0 && IsSymbol(line[indexBefore]))
        {
            return true;
        }

        var indexAfter = match.Index + match.Length;
        if (indexAfter < line.Length && IsSymbol(line[indexAfter]))
        {
            return true;
        }

        if (lineIndex > 0 && CheckLine(match, lineIndex - 1))
        {
            return true;
        }
        
        if (lineIndex < _lines.Length - 1 && CheckLine(match, lineIndex + 1))
        {
            return true;
        }

        return false;
    }
    
    private bool IsSymbol(char symbol)
    {
        return !char.IsDigit(symbol) && symbol != '.';
    }

    private bool CheckLine(Match match, int lineIndex)
    {
        var lineAbove = _lines[lineIndex];
        var startIndex = match.Index > 0 ? match.Index - 1 : match.Index;
        var length = match.Length;
        if (startIndex < match.Index) length++;
        if (match.Index + match.Length < lineAbove.Length) length++;

        var substring = lineAbove.Substring(startIndex, length);
        return substring.ToCharArray().Any(IsSymbol);
    }
}