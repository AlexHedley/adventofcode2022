#load "../utils/utils.csx"

public class Day04
{
    public List<int> BoundsRange(Bounds x)
    {
        return Enumerable.Range(x.Lower, x.Upper - x.Lower + 1).ToList();
    }

    public (string one, string two) Split(string line, string separator)
    {
        (string one, string two) split = line.Split(separator) switch { var a => (a[0], a[1]) };
        return split;
    }

    public string PrintRange(List<int> range)
    {
        string printOut = "";
        for (int i = 1; i < 10; i++)
        {
            if (range.Contains(i))
            {
                // Console.Write(i);
                printOut += i;
            }
            else
            {
                // Console.Write(".");
                printOut += ".";
            }
        }
        // Console.Write("  ");
        printOut += "  ";
        // Console.Write($"{range[0]}-{range[^1]}");
        printOut += $"{range[0]}-{range[^1]}";
        // Console.WriteLine();
        return printOut;
    }

    public bool Overlap(List<int> one, List<int> two)
    {
        bool existsCheck12 = one.All(x => two.Any(y => x == y));
        bool existsCheck21 = two.All(y => one.Any(x => y == x));
        return existsCheck12 || existsCheck21;
    }
}

public struct Bounds
{
    public Bounds(int lower, int upper)
    {
        Lower = lower;
        Upper = upper;
    }

    public int Lower { get; }
    public int Upper { get; }

    public override string ToString() => $"{Lower}..{Upper}";
}

Console.WriteLine("-- Day 4 --");

var day04 = new Day04();

// string fileName = @"input-sample.txt";
string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Console.WriteLine("Part 1.");

var total = 0;
lines.ToList().ForEach(x =>
{
    var split = day04.Split(x, ",");

    var first = day04.Split(split.one, "-");
    var firstBounds = new Bounds(Int32.Parse(first.one), Int32.Parse(first.two));
    var firstRange = day04.BoundsRange(firstBounds);
    // firstRange.ForEach(Console.WriteLine);
    var firstPrint = day04.PrintRange(firstRange);
    Console.Write(firstPrint);
    Console.WriteLine();

    var second = day04.Split(split.two, "-");
    var secondBounds = new Bounds(Int32.Parse(second.one), Int32.Parse(second.two));
    var secondRange = day04.BoundsRange(secondBounds);
    // secondRange.ForEach(Console.WriteLine);
    var secondPrint = day04.PrintRange(secondRange);
    Console.Write(secondPrint);

    var overlap = day04.Overlap(firstRange, secondRange);
    Console.Write($"  {overlap}");
    Console.WriteLine();

    total += Convert.ToInt32(overlap);
});

Console.WriteLine($"Total: {total}");

// Part 2
// Console.WriteLine("Part 2.");

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();