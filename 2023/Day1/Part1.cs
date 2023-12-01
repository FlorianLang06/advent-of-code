using Lib;

namespace Day1;

public class Part1 : IPart
{
    public void Execute()
    {
        using var reader = new StreamReader("./input1.txt");

        var endResult = 0;
        
        while (!reader.EndOfStream) {
            var line = reader.ReadLine();
            if (line == null) {
                break;
            }
            char? firstDigit = null;
            char? lastDigit = null;
            foreach (var c in line.ToCharArray()) {
                if (char.IsNumber(c)) {
                    if (!firstDigit.HasValue) {
                        firstDigit = c;
                        lastDigit = c;
                    } else {
                        lastDigit = c;
                    }
                }
            }
            string stringResult = string.Empty;
            stringResult += firstDigit;
            stringResult += lastDigit;
            var lineResult = Int32.Parse(stringResult);
            endResult += lineResult;
        }
        
        Console.WriteLine($"Result: {endResult}");
    }
}