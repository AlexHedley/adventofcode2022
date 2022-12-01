Console.WriteLine("Day 1");

// string fileName = @"input-sample.txt";
string fileName = @"input.txt";
string[] lines = System.IO.File.ReadAllLines(fileName);

// Part 1
Console.WriteLine("Part 1.");

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

var maxValue = elves.Values.Max();
var keyOfMaxValue = elves.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

Console.WriteLine($"{keyOfMaxValue}: {maxValue}");

// Part 2
// Console.WriteLine("Part 2.");

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();