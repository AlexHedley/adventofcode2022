#load "../utils/utils.csx"

using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.IO;

public class Day21
{
    public void Part1(string[] lines)
    {
        var monkeys = ParseMonkeys(lines);
        // monkeys.ForEach(m => { Console.WriteLine(m.Name); });
        // PopulateMonkeys(lines, monkeys);
        // PrintMonkeys(monkeys);

        // Calculate(monkeys);

        var monkeyName = "root";
        CalculateFromName(monkeyName, monkeys);

        PrintMonkeys(monkeys, true);
        var monkey = monkeys.FirstOrDefault(m => m.Name == "root");
        Console.WriteLine($"Root: {monkey.Yell}");
    }

    void CalculateFromName(string monkeyName, List<Monkey> monkeys)
    {
        var monkey = monkeys.FirstOrDefault(m => m.Name == monkeyName);

        var allYelled = monkey.HasYelled; // && monkey.Monkeys.All(m => m.HasYelled == true);
        
        if (!allYelled)
        {
            if (monkey.Monkeys.Count > 0)
            {
                if (monkey.Monkeys[0].HasYelled && monkey.Monkeys[1].HasYelled) return;

                if (!monkey.Monkeys[0].HasYelled)
                {
                    CalculateFromName(monkey.Monkeys[0].Name, monkeys);
                }

                if (!monkey.Monkeys[1].HasYelled)
                {
                    CalculateFromName(monkey.Monkeys[1].Name, monkeys);
                }
            }
        }
    }

    void Calculate(List<Monkey> monkeys)
    {
        var allYelled = monkeys.All(m => m.HasYelled == true);

        if (!allYelled)
        {
            monkeys.ForEach(m => {
                if (m.Monkeys.Count > 0)
                {
                    if (m.Monkeys[0].HasYelled && m.Monkeys[1].HasYelled)
                    {
                        switch (m.Operation)
                        {
                            case OperationEnum.Plus:
                                m.Yell = m.Monkeys[0].Yell + m.Monkeys[1].Yell;
                                m.HasYelled = true;
                                break;                    
                            case OperationEnum.Minus:
                                m.Yell = m.Monkeys[0].Yell - m.Monkeys[1].Yell;
                                m.HasYelled = true;
                                break;
                            case OperationEnum.Multiply:
                                m.Yell = m.Monkeys[0].Yell * m.Monkeys[1].Yell;
                                m.HasYelled = true;
                                break;
                            case OperationEnum.Divide:
                                if (m.Monkeys[1].Yell != 0)
                                    m.Yell = m.Monkeys[0].Yell / m.Monkeys[1].Yell;
                                    m.HasYelled = true;
                                break;
                            default:
                                break;
                        }
                    }
                }
            });
            Calculate(monkeys);
        }
    }

    List<Monkey> ParseMonkeys(string[] lines)
    {
        List<Monkey> monkeys = new List<Monkey>();
        foreach(var line in lines)
        {
            // String.Split(":", line);
            (string name, string job) split = line.Split(": ") switch { var a => (a[0], a[1]) };
            // Console.WriteLine($"{split.name}: {split.job}");

            var monkey = new Monkey() { Name = split.name };
            monkeys.Add(monkey);
        }
        return monkeys;
    }

    void PopulateMonkeys(string[] lines, List<Monkey> monkeys)
    {
        foreach(var line in lines)
        {
            (string name, string job) split = line.Split(": ") switch { var a => (a[0], a[1]) };
            
            // Get monkey
            var monkey = monkeys.FirstOrDefault(m => m.Name == split.name);
            if (monkey == null) continue;

            // Check if yell?
            if (Int32.TryParse(split.job, out int num))
            {
                monkey.Yell = num;
                monkey.HasYelled = true;
            }
            else
            {
                // Parse Operation
                ParseJob(split.job, monkey, monkeys);
            }
        }
    }

    void ParseJob(string job, Monkey monkey, List<Monkey> monkeys)
    {
        var pattern = @"(\w+) (.) (\w+)";
        var match = Regex.Match(job, pattern);

        var monkey1Name = match.Groups[1].Value;
        var operation = match.Groups[2].Value;
        var monkey2Name = match.Groups[3].Value;
        
        // Console.WriteLine(operation);
        // Enum.TryParse(operation, out OperationEnum myOperation);
        // var myOperation = operation.ToEnum<OperationEnum>();
        var myOperation = operation.ToEnumFromValue<OperationEnum>();

        var monkey1 = monkeys.FirstOrDefault(m => m.Name == monkey1Name);
        var monkey2 = monkeys.FirstOrDefault(m => m.Name == monkey2Name);
        
        monkey.Monkeys.Add(monkey1);
        monkey.Monkeys.Add(monkey2);
        monkey.Operation = myOperation;
    }

    void PrintMonkeys(List<Monkey> monkeys, bool toFile = false)
    {
        if (toFile) File.WriteAllText(@"Monkeys.txt", String.Empty);
        if (toFile) File.AppendAllLines(@"Monkeys.txt", new[] { "Starting" });
        
        foreach(var monkey in monkeys)
        {
            Console.Write($"{monkey.Name}");
            Console.Write($" ");
            Console.Write($"{monkey.Yell}");
            Console.Write($" ");
            Console.Write($"{monkey.Operation}");
            Console.Write($" ");
            Console.Write($"{monkey.Monkeys.Count}");
            Console.WriteLine();
            
            var monkeysJob = "";
            if (monkey.Monkeys.Count > 0) monkeysJob = $"{monkey.Monkeys[0].Name} | {monkey.Monkeys[1].Name}";
            if (toFile) File.AppendAllLines(@"Monkeys.txt", new[] { $"{monkey.Name} {monkey.Yell} {monkey.Operation} [{monkey.Monkeys.Count}] {monkeysJob}" });
        }

        if (toFile) File.AppendAllLines(@"Monkeys.txt", new[] { "Finished." });
    }
}

public class Monkey
{
    // public Monkey(){}
    public string Name { get; set; }
    public int Yell { get; set; } = 0;
    public OperationEnum Operation { get; set; }
    public List<Monkey> Monkeys { get; set; } = new List<Monkey>();
    public bool HasYelled = false;
}

public enum OperationEnum
{
    [EnumMember(Value = "")]
    None,
    [EnumMember(Value = "+")]
    Plus,
    [EnumMember(Value = "-")]
    Minus,
    [EnumMember(Value = "*")]
    Multiply,
    [EnumMember(Value = "/")]
    Divide
}

Console.WriteLine("-- Day 21 --");

var day21 = new Day21();

string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Console.WriteLine("Part 1.");
day21.Part1(lines);

// Part 2
// Console.WriteLine("Part 2.");

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();