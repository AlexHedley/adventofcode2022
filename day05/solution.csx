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

    public List<IDictionary<int, string>> GenerateStackList(string[] lines)
    {
        List<IDictionary<int, string>> stackList = new List<IDictionary<int, string>>();
        foreach (var line in lines)
        {
            Console.WriteLine($"LINE: {line}");
            var items = line.Split(' ');
            Console.WriteLine($"#: {items.Count()}");

            var length = line.Length;
            Console.WriteLine($"Length: {length}");

            if (line == " 1   2   3   4   5   6   7   8   9 ") break;
            if (length == 0) break;

            var columnCount = (length + 1) / 4; //"[A] " = 4
            Console.WriteLine(columnCount);
            IDictionary<int, string> stackInfo = new Dictionary<int, string>();

            var index = 0;
            var skip = 0;
            var take = 3;
            var total = 0;
            while (total < length + 1)
            {
                Console.WriteLine($"Skip: {skip} | Take: {take} | Total: {total}");
                var item = new string(line.Skip(total).Take(take).ToArray());
                Console.WriteLine(item);

                var check = item.All(c => c == ' ');
                // Console.WriteLine(check);
                // var value = check ? "" : Sanitise(item.ToString());
                if (check) { }
                else
                {
                    stackInfo[index] = Sanitise(item.ToString());
                }

                skip = 4;
                total += skip;
                index++;
            }
            Console.WriteLine("done");

            // foreach (var col in items)
            // {
            //     Console.WriteLine(col);
            // }

            foreach (var kvp in stackInfo)
                Console.WriteLine("Key: {0}, Value: {1}", kvp.Key, kvp.Value);

            stackList.Add(stackInfo);
        }
        return stackList;
    }

    public List<Stack<string>> PopulateStacks(List<IDictionary<int, string>> stackList)
    {
        Stack<string> stack1 = new Stack<string>();
        Stack<string> stack2 = new Stack<string>();
        Stack<string> stack3 = new Stack<string>();
        Stack<string> stack4 = new Stack<string>();
        Stack<string> stack5 = new Stack<string>();
        Stack<string> stack6 = new Stack<string>();
        Stack<string> stack7 = new Stack<string>();
        Stack<string> stack8 = new Stack<string>();
        Stack<string> stack9 = new Stack<string>();

        for (var i = stackList.Count - 1; i >= 0; i--)
        {
            // Console.Write($"i: {i}");
            var dict = stackList[i];
            // foreach (var kvp in dict)
            //     Console.WriteLine("Key: {0}, Value: {1}", kvp.Key, kvp.Value);

            if (dict.ContainsKey(0)) stack1.Push(dict[0]);
            if (dict.ContainsKey(1)) stack2.Push(dict[1]);
            if (dict.ContainsKey(2)) stack3.Push(dict[2]);
            if (dict.ContainsKey(3)) stack4.Push(dict[3]);
            if (dict.ContainsKey(4)) stack5.Push(dict[4]);
            if (dict.ContainsKey(5)) stack6.Push(dict[5]);
            if (dict.ContainsKey(6)) stack7.Push(dict[6]);
            if (dict.ContainsKey(7)) stack8.Push(dict[7]);
            if (dict.ContainsKey(8)) stack9.Push(dict[8]);
        }

        return new List<Stack<string>>() { stack1, stack2, stack3, stack4, stack5, stack6, stack7, stack8, stack9 };
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

    public string GetTop(List<Stack<string>> stacks)
    {
        string message = "";
        foreach (var stack in stacks)
        {
            message += stack.Pop();
        }
        // Console.WriteLine($"{message}");
        return message;
    }

    public void PrintStack(Stack<string> stack, int number, int length)
    {
        // Stack<string> clone = (Stack<string>)stack.Clone();
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
        // foreach (var stack in stacks)
        // {
        //     Console.WriteLine($"STACKCOUNT #: {stack.Count}");
        // }

        var max = stacks.Select(s => s.Count).Max();
        Console.WriteLine($"MAX: {max}");
        for (var i = 0; i < stacks.Count; i++)
        {
            PrintStack(stacks[i], i + 1, max);
        }
    }

    public string[,] GenerateMatrix(string[] lines, int rows, int cols)
    {
        var pattern = @"(\[[A-Z]\]) (\[[A-Z]\]) (\[[A-Z]\])|(^\s*) (\[[A-Z]\]) (\[[A-Z]\])|(\[[A-Z]\]) (\[[A-Z]\]) (\s*$)|(^\s*) (\[[A-Z]\]) (\s*$)";

        string[,] matrix = new string[rows, cols];

        var i = 0;
        foreach (var line in lines)
        {
            var match = Regex.Match(line, pattern);

            var j = 0;
            for (var g = 1; g < match.Groups.Count; g++)
            {
                if (match.Groups[g].Success)
                {
                    // Console.WriteLine(match.Groups[g].Value);
                    matrix[i, j] = match.Groups[g].Value;
                    j++;
                }
            }
            i++;
        }
        return matrix;
    }

    // https://stackoverflow.com/a/51241629
    public string[] GetColumn(string[,] matrix, int columnNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber])
                .ToArray();
    }

    public string[] GetRow(string[,] matrix, int rowNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[rowNumber, x])
                .ToArray();
    }

    public void PrintMatrix(string[,] matrix)
    {
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                Console.Write($"{matrix[i, j]} ");
            }
            Console.Write(Environment.NewLine);
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

var stackList = day05.GenerateStackList(lines);
Console.WriteLine($"STACKLIST #: {stackList.Count}");
var stacks = day05.PopulateStacks(stackList);
Console.WriteLine($"STACKS #: {stacks.Count}");

day05.PrintStacks(stacks);

Console.WriteLine();

var commands = day05.GetCommands(lines);
day05.ParseCommands(stacks, commands);

day05.PrintStacks(stacks);

Console.WriteLine();

var message = day05.GetTop(stacks);
Console.WriteLine($"Message: {message}");

// Part 2
// Console.WriteLine("Part 2.");

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();