#nullable enable
#load "../utils/utils.csx"

public class Day11
{
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
            foreach(var line in monkeySet)
            {
                Console.WriteLine(line);
                // TODO:
                // Get Number
                // Get Items
                // Get Operation
            }
            monkeys.Add(monkey);
        }

        // TODO: Loop Again and populate True/False

        return monkeys;
    }
}

public class Monkey
{
    public int Number = 0;
    public List<int> Items = new List<int>();
    public string Operation = "";
    public MonkeyTest Test = new MonkeyTest();

    public override string ToString() {
        return $"Monkey {Number} | Items: {String.Join(',', Items)} | Operation: {Operation} | {Test}";
    }
}

public class MonkeyTest
{
    public int Divisible = 0;
    public Monkey? True = null;
    public Monkey? False = null;

    public override string ToString() {
        return $"Test - Divisible:{Divisible} | True: {True?.Number} | False: {False?.Number}";
    }
}

Console.WriteLine("-- Day 11 --");

var day11 = new Day11();

string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Console.WriteLine("Part 1.");
var monkeys = day11.ParseInput(lines);
monkeys.ForEach(Console.WriteLine);

// Part 2
// Console.WriteLine("Part 2.");

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();