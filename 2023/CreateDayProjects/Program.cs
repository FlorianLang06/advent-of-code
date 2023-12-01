using System.Diagnostics;

var path = "/home/florian/Distrobox/Dev/dev/csharp/AdventOfCode/2023/";

for (var day = 1; day <= 24; day++)
{
    ExecuteCommand($"dotnet new console --framework net8.0 --name Day{day}");
    ExecuteCommand($"dotnet sln add Day{day}");
    File.WriteAllText(Path.Combine(path, $"Day{day}", "Program.cs"), $"Console.WriteLine(\"###### Advent of Code 2023 Day {day} ######\\n\");");
    Console.WriteLine($"Create Day {day}");
}

Console.WriteLine("Finished!!!");

void ExecuteCommand(string command)
{
    var process = new Process();
    process.StartInfo.FileName = "/bin/bash";
    process.StartInfo.Arguments = $"-c \"{command}\"";
    process.StartInfo.WorkingDirectory = path;
    process.Start();

    process.WaitForExit();
}