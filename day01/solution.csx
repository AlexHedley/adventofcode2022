#load "../utils/utils.csx"

public class Day01 {

    public Dictionary<int, long> GetElves(string[] lines) {
        var items = lines.Select(x => x).ToList();
        Dictionary<int, long> elves = new Dictionary<int, long>();
        var elfId = 1;
        for (int i = 0; i < items.Count; i++) {
            if (String.IsNullOrEmpty(items[i])) {
                elfId++;
                continue;
            }
            if (!elves.ContainsKey(elfId))
                elves.Add(elfId, Int32.Parse(items[i]));
            else
                elves[elfId] += Int32.Parse(items[i]);
        }
        return elves;
    }

    public long GetMaxValue(Dictionary<int, long> elves) {
        return elves.Values.Max();
    }

    public int GetMaxKey(Dictionary<int, long> elves) {
        return elves.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
    }

    public long GetTotal(Dictionary<int, long> elves) {
        return elves
                .OrderByDescending(x => x.Value)
                .Take(3)
                .Sum(x => x.Value);
    }
}

Console.WriteLine("-- Day 1 --");

// string fileName = @"input-sample.txt";
string fileName = @"input.txt";

var day01 = new Day01();
var lines = Utils.GetLines(fileName);

// Part 1
Console.WriteLine("Part 1.");

var elves = day01.GetElves(lines);

var maxValue = day01.GetMaxValue(elves);
var maxKey = day01.GetMaxKey(elves);
Console.WriteLine($"{maxKey}: {maxValue}");

// Part 2
Console.WriteLine("Part 2.");

var total = day01.GetTotal(elves);
Console.WriteLine($"Total: {total}");

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();