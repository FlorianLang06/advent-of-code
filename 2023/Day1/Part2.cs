﻿using Lib;

namespace Day1;

public class Part2 : IPart
{
    private readonly Dictionary<string, char> _numbersAsString = new()
    {
        {"one", '1'},
        {"two", '2'},
        {"three", '3'},
        {"four", '4'},
        {"five", '5'},
        {"six", '6'},
        {"seven", '7'},
        {"eight", '8'},
        {"nine", '9'},
        {"zero", '0'},
    };
    public void Execute()
    {
        using var reader = new StreamReader("./input2.txt");

        var endResult = 0;
        
        while (!reader.EndOfStream) {
            var line = reader.ReadLine();
            if (line == null) {
                break;
            }
            char firstDigit = '\0';
            char lastDigit = '\0';
            var chars = line.ToCharArray();
            for (var index=0; index<chars.Length; index++) {
                var c = chars[index];
                if (char.IsNumber(c)) {
                    if (firstDigit == '\0') {
                        firstDigit = c;
                    } else {
                        lastDigit = c;
                    }
                } else {
                    var lineLength = line.Length;
                    for (int length=3; length<6; length++) {
                        if ((index + length) > lineLength) {
                            break;
                        }
                        var substring = line.Substring(index, length);
                        if (_numbersAsString.ContainsKey(substring)) {
                            if (firstDigit == '\0') {
                                firstDigit = _numbersAsString[substring];
                            } else {
                                lastDigit = _numbersAsString[substring];
                            }
                        }
                    }
                }
            }
            string stringResult = string.Empty;
            stringResult += firstDigit;
            stringResult += lastDigit == '\0' ? firstDigit : lastDigit;
            var lineResult = Int32.Parse(stringResult);
            endResult += lineResult;
        }
        
        Console.WriteLine($"Result: {endResult}");
    }
}