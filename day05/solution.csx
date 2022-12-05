#load "../utils/utils.csx"

using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Day05
{
    public string Sanitise(string item)
    {
        var sanitisedItem = item.Replace("[", "");
        sanitisedItem = sanitisedItem.Replace("]", "");
        return sanitisedItem;
    }

    public List<IDictionary<int, string>> GenerateStackDictsList(string[] lines)
    {
        List<IDictionary<int, string>> stackList = new List<IDictionary<int, string>>();
        foreach (var line in lines)
        {
            IDictionary<int, string> stackInfo = new Dictionary<int, string>();

            var items = line.Split(' ');
            var length = line.Length;
            if (line == " 1   2   3   4   5   6   7   8   9 ") break;
            if (length == 0) break;
            var columnCount = (length + 1) / 4; //"[A] " = 4

            var index = 0;
            var skip = 0;
            var take = 3;
            var total = 0;
            while (total < length + 1)
            {
                var item = new string(line.Skip(total).Take(take).ToArray());

                var check = item.All(c => c == ' ');
                if (check) { }
                else
                {
                    stackInfo[index] = Sanitise(item.ToString());
                }

                skip = 4;
                total += skip;
                index++;
            }

            stackList.Add(stackInfo);
        }
        return stackList;
    }

    public List<Stack<string>> PopulateStacks(List<IDictionary<int, string>> stackDictsList, int columnCount)
    {
        var count = stackDictsList.Count;
        var stacks = new List<Stack<string>>();
        for (var s = 0; s < columnCount; s++)
        {
            Stack<string> stack = new Stack<string>();
            stacks.Add(stack);
        }

        for (var i = count - 1; i >= 0; i--)
        {
            var dict = stackDictsList[i];
            for (var s = 0; s < stacks.Count; s++)
            {
                if (dict.ContainsKey(s))
                {
                    stacks[s].Push(dict[s]);
                }
            }
        }

        return stacks;
    }

    public string[] GetCommands(string[] lines)
    {
        var index = 0;
        for (var i = 0; i < lines.Length; i++)
        {
            if (lines[i].Length == 0)
            {
                index = i;
            }
        }
        var count = lines.Count();
        var take = count - index;
        var items = lines.Skip(index + 1).Take(take).ToArray();

        return items;
    }

    public void ParseCommand(List<Stack<string>> stacks, string line)
    {
        var pattern = @"(\w*)(\s)(\d*)(\s)(\w*)(\s)(\d*)(\s)(\w*)(\s)(\d*)";
        var match = Regex.Match(line, pattern);

        var action = match.Groups[1].Value;
        var count = Int32.Parse(match.Groups[3].Value);
        var from = Int32.Parse(match.Groups[7].Value);
        var to = Int32.Parse(match.Groups[11].Value);

        // Console.WriteLine($"{action} {count} from {from} to {to}");

        for (var i = 0; i < count; i++)
        {
            var move = "";
            move = stacks[from - 1].Pop();
            stacks[to - 1].Push(move);
        }
    }

    public void ParseCommands(List<Stack<string>> stacks, string[] lines)
    {
        foreach (var line in lines)
        {
            ParseCommand(stacks, line);
        }
    }

    public void ParseCommandPart2(List<Stack<string>> stacks, string line)
    {
        var pattern = @"(\w*)(\s)(\d*)(\s)(\w*)(\s)(\d*)(\s)(\w*)(\s)(\d*)";
        var match = Regex.Match(line, pattern);

        var action = match.Groups[1].Value;
        var count = Int32.Parse(match.Groups[3].Value);
        var from = Int32.Parse(match.Groups[7].Value);
        var to = Int32.Parse(match.Groups[11].Value);

        Stack<string> itemsToMove = new Stack<string>();
        for (var i = 0; i < count; i++)
        {
            var move = "";
            move = stacks[from - 1].Pop();
            itemsToMove.Push(move);
        }
        while (itemsToMove.Count > 0)
        {
            var move = "";
            move = itemsToMove.Pop();
            stacks[to - 1].Push(move);
        }
    }

    public void ParseCommandsPart2(List<Stack<string>> stacks, string[] lines)
    {
        foreach (var line in lines)
        {
            ParseCommandPart2(stacks, line);
        }
    }

    public string GetTop(List<Stack<string>> stacks)
    {
        string message = "";
        foreach (var stack in stacks)
        {
            message += stack.Pop();
        }
        return message;
    }

    public void PrintStack(Stack<string> stack, int number, int length)
    {
        var clone = new Stack<string>(stack.Reverse());

        var diff = length - clone.Count;
        for (var j = 0; j < diff; j++)
        {
            Console.Write("    ");
        }
        while (clone.Count > 0)
        {
            var value = clone.Pop();
            Console.Write($"[{value}] ");
        }
        Console.Write($" {number}");
        Console.WriteLine();
    }

    public void PrintStacks(List<Stack<string>> stacks)
    {
        var max = stacks.Select(s => s.Count).Max();
        for (var i = 0; i < stacks.Count; i++)
        {
            PrintStack(stacks[i], i + 1, max);
        }
    }
}

Console.WriteLine("-- Day 5 --");

var day05 = new Day05();

// string fileName = @"input-sample.txt";
string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Console.WriteLine("Part 1.");

var columnCount = (lines[0].Length + 1) / 4; //"[A] " = 4
var stackDictsList = day05.GenerateStackDictsList(lines);
var stacks = day05.PopulateStacks(stackDictsList, columnCount);

day05.PrintStacks(stacks);

Console.WriteLine();

var commands = day05.GetCommands(lines);
day05.ParseCommands(stacks, commands);

day05.PrintStacks(stacks);

Console.WriteLine();

var message = day05.GetTop(stacks);
Console.WriteLine($"Message: {message}");

// Part 2
Console.WriteLine("Part 2.");

stackDictsList = day05.GenerateStackDictsList(lines);
stacks = day05.PopulateStacks(stackDictsList, columnCount);
day05.PrintStacks(stacks);
Console.WriteLine();
day05.ParseCommandsPart2(stacks, commands);
day05.PrintStacks(stacks);
Console.WriteLine();
message = day05.GetTop(stacks);
Console.WriteLine($"Message: {message}");

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();