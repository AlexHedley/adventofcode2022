#load "../utils/utils.csx"

public class Day03
{
    public IEnumerable<string> Split(string str, int chunkSize)
    {
        return Enumerable.Range(0, str.Length / chunkSize)
            .Select(i => str.Substring(i * chunkSize, chunkSize));
    }

    public IEnumerable<string> Matching(List<string> splitLine)
    {
        return splitLine[0].ToCharArray().Select(c => c.ToString()).ToArray().Intersect<string>(splitLine[1].ToCharArray().Select(c => c.ToString()).ToArray());
    }
}

Dictionary<string, int> mappings = new Dictionary<string, int>() {
    { "a", 1 }, { "b", 2 }, { "c", 3 }, { "d", 4 }, { "e", 5 }, { "f", 6 }, { "g", 7 }, { "h", 8 }, { "i", 9 }, { "j", 10 }, { "k", 11 }, { "l", 12 }, { "m", 13 }, { "n", 14 }, { "o", 15 }, { "p", 16 }, { "q", 17 }, { "r", 18 }, { "s", 19 }, { "t", 20 }, { "u", 21 }, { "v", 22 }, { "w", 23 }, { "x", 24 }, { "y", 25 }, { "z", 26 },
    { "A", 27 }, { "B", 28 }, { "C", 29 }, { "D", 30 }, { "E", 31 }, { "F", 32 }, { "G", 33 }, { "H", 34 }, { "I", 35 }, { "J", 36 }, { "K", 37 }, { "L", 38 }, { "M", 39 }, { "N", 40 }, { "O", 41 }, { "P", 42 }, { "Q", 43 }, { "R", 44 }, { "S", 45 }, { "T", 46 }, { "U", 47 }, { "V", 48 }, { "W", 49 }, { "X", 50 }, { "Y", 51 }, { "Z", 52 }
};

Console.WriteLine("-- Day 3 --");

var day03 = new Day03();

// string fileName = @"input-sample.txt";
string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Console.WriteLine("Part 1.");

var total = 0;
foreach (var line in lines)
{
    var splitLine = day03.Split(line, line.Length / 2).ToList();
    var resultSet = day03.Matching(splitLine);
    foreach (var result in resultSet)
    {
        total += mappings[result];
    }
}

Console.WriteLine($"Total: {total}");

// Part 2
Console.WriteLine("Part 2.");

total = 0;
var chunkSize = 3;
foreach (var chunk in lines.Chunk(chunkSize)) //Returns a chunk with the correct size. 
{
    // Console.WriteLine(chunk);
    var listOfLists = new List<List<string>>();
    Parallel.ForEach(chunk, (item) =>
    {
        //Do something Parallel here. 
        var list = item.ToCharArray().Select(c => c.ToString()).ToList();
        listOfLists.Add(list);
    });
    var intersection = listOfLists
        .Skip(1)
        .Aggregate(
            new HashSet<string>(listOfLists.First()),
            (h, e) => { h.IntersectWith(e); return h; }
        );
    foreach (var i in intersection)
    {
        total += mappings[i];
    }
}

Console.WriteLine($"Total: {total}");

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();