#nullable enable
#load "../utils/utils.csx"

using System.Text.RegularExpressions;

public class Day11
{
    public void Part1(string[] lines)
    {
        var monkeys = ParseInput(lines);
        // monkeys.ForEach(Console.WriteLine);

        Dictionary<ulong, ulong> monkeyItemCount = new Dictionary<ulong, ulong>();
        monkeyItemCount = SetupMonkeyCounts(monkeys);
        // monkeyItemCount.Select(i => $"{i.Key}: {i.Value}").ToList().ForEach(Console.WriteLine);

        var numRounds = 21;
        for (var i = 1; i < numRounds; i++)
        {
            // Console.WriteLine($"Round {i}");
            PerformRound(monkeys, monkeyItemCount, 3UL);
        }

        // Console.WriteLine();
        // monkeyItemCount.Select(i => $"{i.Key}: {i.Value}").ToList().ForEach(Console.WriteLine);

        // Get Top 2
        var topValues = monkeyItemCount.Values
                             .OrderByDescending(x => x)
                             .Take(2)
                             .ToArray();
        var total = topValues[0] * topValues[1];
        Console.WriteLine($"Total: {total}");
    }

    public void Part2(string[] lines)
    {
        var monkeys = ParseInput(lines);

        Dictionary<ulong, ulong> monkeyItemCount = new Dictionary<ulong, ulong>();
        monkeyItemCount = SetupMonkeyCounts(monkeys);

        var numRounds = 10001;
        for (var i = 1; i < numRounds; i++)
        {
            PerformRound(monkeys, monkeyItemCount);
        }

        // Console.WriteLine();
        monkeyItemCount.Select(i => $"{i.Key}: {i.Value}").ToList().ForEach(Console.WriteLine);
        Console.WriteLine();

        // Get Top 2
        var topValues = monkeyItemCount.Values
                             .OrderByDescending(x => x)
                             .Take(2)
                             .ToArray();
        var total = topValues[0] * topValues[1];
        Console.WriteLine($"Total: {total}");
    }

    public Dictionary<ulong, ulong> SetupMonkeyCounts(List<Monkey> monkeys)
    {
        Dictionary<ulong, ulong> monkeyItemCount = new Dictionary<ulong, ulong>();
        foreach(var monkey in monkeys)
        {
            monkeyItemCount[monkey.Number] = 0UL; //monkey.Items.Count;
        }
        return monkeyItemCount;
    }
    
    public void PerformRound(List<Monkey> monkeys, Dictionary<ulong, ulong> monkeyItemCount, ulong divideBy = 1UL)
    {
        foreach (var monkey in monkeys)
        {
            foreach(var item in monkey.Items)
            {
                var worryLevel = Calculate(item, monkey.Operation);
                // Console.WriteLine($"{monkey.Number}: {item}: {worryLevel} {worryLevel/3}");
                var worryLevelDivideBy = worryLevel / divideBy;
                
                var to = PeformTest(monkey.Test, worryLevelDivideBy);
                // Console.WriteLine($"{to.Number}");
                // Console.WriteLine($"{monkey.Number}: {item} # {worryLevel} {worryLevelDivideBy} {to.Number}");

                var monkeyToThrowTo = monkeys.FirstOrDefault(m => m.Number == to.Number);
                monkeyToThrowTo?.Items.Add(worryLevelDivideBy);
                monkeyItemCount[monkey.Number] += 1L;
            }
            monkey.Items.Clear();
        }
        // monkeys.ForEach(m => 
        // {
        //     Console.WriteLine($"{m.Number}: {String.Join(',', m.Items)}");
        //     // monkeyItemCount[m.Number] += m.Items.Count;
        // });
    }

    public Monkey PeformTest(MonkeyTest test, ulong worryLevel)
    {
        var divisible = worryLevel % test.Divisible;
        return (divisible == 0) ? test.True! : test.False!;
    }

    public ulong Calculate(ulong old, Operation operation)
    {
        var worryLevel = 0UL;
        var lhs = operation.LHS == "old" ? old : ulong.Parse(operation.LHS);
        var rhs = operation.RHS == "old" ? old : ulong.Parse(operation.RHS);
        

        switch (operation.Action)
        {
            case "+":
                worryLevel = lhs + rhs;
                break;
            case "-":
                worryLevel = lhs - rhs;
                break;
            case "*":
                worryLevel = lhs * rhs;
                break;
            case "/":
                worryLevel = (rhs == 0) ? 0 : lhs / rhs;
                break;
            default:
                break;
        }
        return worryLevel;
    }

    public List<Monkey> ParseInput(string[] lines)
    {
        // https://stackoverflow.com/a/65203039/2895831
        List<List<string>> monkeySets = new List<List<string>>();

        List<string> curSet = new List<string>();    
        foreach (String line in lines)
        {
            if (line.Trim().Length == 0)
            {
                if (curSet.Count > 0)
                {
                    monkeySets.Add(curSet);
                    curSet = new List<string>();
                }               
            }
            else
            {
                curSet.Add(line);
            }
        }

        if (curSet.Count > 0)
        {
            monkeySets.Add(curSet);
        }

        var monkeys = new List<Monkey>(); 
        foreach(var monkeySet in monkeySets)
        {
            var monkey = new Monkey();
            var monkeyTest = new MonkeyTest();
            var operation = new Operation();
            // foreach(var line in monkeySet)
            for (var i = 0; i < monkeySet.Count; i++)
            {
                var pattern = @"(\d+)";
                
                // Get Number
                if (i == 0)
                {   
                    var match = Regex.Match(monkeySet[i], pattern);
                    var number = ulong.Parse(match.Groups[1].Value);
                    monkey.Number = number;
                }
                
                // Get Items
                if (i == 1)
                {
                    var items = monkeySet[i].Split(":")[1].Split(", ");
                    monkey.Items = items.Select(ulong.Parse).ToList();
                }
                
                // Get Operation
                if (i == 2)
                {
                    var info = monkeySet[i].Split(":");
                    var op = info[1].Trim();
                    // monkey.Operation = operation;
                    pattern = @"(\w*) = (\w*) (\+|\-|\*|\/) (\w*)";
                    var match = Regex.Match(op, pattern);
                    operation.LHS = match.Groups[2].Value;
                    operation.Action = match.Groups[3].Value;
                    operation.RHS = match.Groups[4].Value;
                }

                // Get Test
                if (i == 3)
                {
                    // var test = monkeySet[i].Split(":");
                    var match = Regex.Match(monkeySet[i], pattern);
                    var number = ulong.Parse(match.Groups[1].Value);
                    monkeyTest.Divisible = number;
                }
                if (i == 4)
                {
                    var match = Regex.Match(monkeySet[i], pattern);
                    var number = ulong.Parse(match.Groups[1].Value);
                    monkeyTest.MonkeyTrue = number;
                }
                if (i == 5)
                {
                    var match = Regex.Match(monkeySet[i], pattern);
                    var number = ulong.Parse(match.Groups[1].Value);
                    monkeyTest.MonkeyFalse = number;
                }
            }
            monkey.Test = monkeyTest;
            monkey.Operation = operation;
            monkeys.Add(monkey);
        }

        // Loop Again and populate True/False
        foreach (var monkey in monkeys)
        {
            var trueMonkey = monkeys.FirstOrDefault(m => m.Number == monkey.Test.MonkeyTrue);
            monkey.Test.True = trueMonkey;

            var falseMonkey = monkeys.FirstOrDefault(m => m.Number == monkey.Test.MonkeyFalse);
            monkey.Test.False = falseMonkey;
        }

        return monkeys;
    }
}

public class Monkey
{
    public ulong Number { get; set; } = 0;
    public List<ulong> Items { get; set; } = new List<ulong>();
    // public string Operation { get; set; } = "";
    public Operation Operation { get; set; } = new Operation();
    
    public MonkeyTest Test { get; set; } = new MonkeyTest();

    public override string ToString() {
        return $"Monkey {Number} | Items: {String.Join(',', Items)} | Operation: {Operation} | {Test}";
    }
}

public class MonkeyTest
{
    public ulong Divisible { get; set; } = 0;
    public Monkey? True { get; set; } = null;
    public ulong MonkeyTrue { get; set; } = 0;
    public Monkey? False { get; set; } = null;
    public ulong MonkeyFalse { get; set; } = 0;

    public override string ToString() {
        return $"Test - Divisible:{Divisible} | True: {True?.Number} ({MonkeyTrue}) | False: {False?.Number} ({MonkeyFalse})";
    }
}

public class Operation
{
    public string LHS { get; set; } = "";
    public string RHS { get; set; } = "";
    public string Action { get; set; } = "";

    public override string ToString() {
        return $"{LHS} {Action} {RHS}";
    }
}

Console.WriteLine("-- Day 11 --");

var day11 = new Day11();

string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Console.WriteLine("Part 1.");
day11.Part1(lines);

// Part 2
Console.WriteLine("Part 2.");
day11.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();