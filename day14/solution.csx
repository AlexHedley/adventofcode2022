#load "../utils/utils.csx"

public class Day14
{
    public void Part1(string[] lines)
    {
        var coordsList = ParseInput(lines);
        coordsList.ForEach(c => { Console.WriteLine(string.Join(" ", c)); });

        var boundaries = GetBoundaries(coordsList);
        Console.WriteLine($"{boundaries.minX}-{boundaries.maxX}, {boundaries.minY}-{boundaries.maxY}");
        var xDiff = boundaries.maxX-boundaries.minX + 1;
        var yDiff = boundaries.maxY-boundaries.minY + 1;
        Console.WriteLine($"Diffs - x: {xDiff}, y: {yDiff}");

        var xBounds = new Bounds(boundaries.minX, boundaries.maxX);
        var xRange = Utils.BoundsRange(xBounds);
        Utils.PrintRange(xRange);

        var yBounds = new Bounds(boundaries.minY, boundaries.maxY);
        var yRange = Utils.BoundsRange(yBounds);
        Utils.PrintRange(yRange);

        var matrix = Utils.GenerateMatrix<string>('.', yDiff, xDiff);
        Utils.PrintMatrix(matrix);
    }

    List<List<(int x, int y)>> ParseInput(string[] lines)
    {
        var coordsList = new List<List<(int x, int y)>>();
        foreach(var line in lines)
        {
            var coordinates = new List<(int x, int y)>();
            var items = line.Split("->", StringSplitOptions.TrimEntries);
            foreach(var item in items)
            {
                (int x, int y) coords = item.Split(",") switch { var c => (Int32.Parse(c[0]), Int32.Parse(c[1])) };
                coordinates.Add(coords);
            }
            coordsList.Add(coordinates);
        }
        return coordsList;
    }

    (int minX, int maxX, int minY, int maxY) GetBoundaries(List<List<(int x, int y)>> coords)
    {
        int minX, maxX, minY, maxY;
        minX = maxX = minY = maxY = -1;
        foreach(var list in coords)
        {
            foreach(var item in list)
            {
                var x = item.x;
                if (x > maxX) maxX = x;
                if (x > 0 && x < maxX) minX = x;

                var y = item.y;
                if (y > maxY) maxY = y;
                if (y > 0 && y < maxY) minY = y;
            }
        }
        return (minX, maxX, minY, maxY);
    }

    List<int> BoundsRange(Bounds x)
    {
        return Enumerable.Range(x.Lower, x.Upper - x.Lower + 1).ToList();
    }
}

Console.WriteLine("-- Day 14 --");

var day14 = new Day14();

string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Console.WriteLine("Part 1.");
day14.Part1(lines);

// Part 2
// Console.WriteLine("Part 2.");

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();