#load "../utils/utils.csx"

using System.ComponentModel;

public class Day10
{
    public void Part1(string[] lines)
    {
        var actions = ParseActions(lines);
        // actions.ForEach(a => { Console.WriteLine($"{a.Command} {a.Count}"); });
        
        var totalCycles = TotalCycles(actions);
        Console.WriteLine($"Total Cycles: {totalCycles}");

        var cycles = RunActions(actions);
        // cycles.Select(i => $"{i.Key}: {i.Value}").ToList().ForEach(Console.WriteLine);
        
        var total = GetTotal(cycles);
        Console.WriteLine($"Total: {total}");
    }

    public int GetTotal(Dictionary<int, int> cycles)
    {
        // index * value...

        var total = 0;
        // 20th, 60th, 100th, 140th, 180th, and 220th
        var twenty = cycles[19];
        var sixty = cycles[59];
        var hundred = cycles[99];
        var hundredandforty = cycles[139];
        var hundredandeighty = cycles[179];
        var twotenty = cycles[219];

        Console.WriteLine($"(20*{twenty}) + (60*{sixty}) + (100*{hundred}) + (140*{hundredandforty}) + (180*{hundredandeighty}) + (220*{twotenty})");
        Console.WriteLine($" ({20*twenty})  + ({60*sixty})  +  ({100*hundred})  +  ({140*hundredandforty})  +  ({180*hundredandeighty})  +  ({220*twotenty})");

        total = (20*twenty) + (60*sixty) + (100*hundred) + (140*hundredandforty) + (180*hundredandeighty) + (220*twotenty);
        return total;
    }

    public int TotalCycles(List<(CommandEnum Command, int Count)> actions)
    {
        var total = 0;
        foreach (var action in actions)
        {
            switch (action.Command)
            {
                case CommandEnum.Noop:
                    total += 1;
                    break;
                case CommandEnum.AddX:
                    total += 2;
                    break;
                default:
                    break;
            }
        }
        return total;
    }

    public Dictionary<int, int> RunActions(List<(CommandEnum Command, int Count)> actions)
    {
        var x = 1;
        var cycle = 1;
        Dictionary<int, int> cycles = new Dictionary<int, int>();

        foreach(var action in actions)
        {
            Console.WriteLine($"{action.Command} ({(int)action.Command}) {action.Count}");
            for (var c = 0; c <= (int)action.Command; c++)
            {
                Console.WriteLine($"Cycle: {cycle} - {c} == {(int)action.Command} [{x}]");
                if (c == (int)action.Command) x += action.Count;
                cycles.Add(cycle, x);
                cycle++;
            }
        }
        return cycles;
    }

    public List<(CommandEnum Command, int Count)> ParseActions(string[] lines)
    {
        var actions = new List<(CommandEnum Command, int Count)>();
        var action = (CommandEnum.None, 0);

        foreach(var line in lines)
        {
            var actionSplit = line.Split(" ");
            if (actionSplit.Length == 2)
            {
                action = (actionSplit[0].ToEnum<CommandEnum>(), Int32.Parse(actionSplit[1]));
            }
            else if (actionSplit.Length == 1)
            {
                action = (actionSplit[0].ToEnum<CommandEnum>(), 0);
            }
            
            actions.Add(action);
        }

        return actions;
    }
}

public enum CommandEnum
{
    [Description("none")]
    None = -1,
    [Description("noop")]
    Noop = 0,
    [Description("addx")]
    AddX = 1
}

Console.WriteLine("-- Day 10 --");

var day10 = new Day10();

// string fileName = @"input-simple-sample.txt";
// string fileName = @"input-sample.txt"; // 13140 != 13360
string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Console.WriteLine("Part 1.");
day10.Part1(lines);

// Part 2
// Console.WriteLine("Part 2.");

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();
