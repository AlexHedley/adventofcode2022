#load "../utils/utils.csx"

// using System;
using System.Text.RegularExpressions;

public class Day15
{
    public void Part1(string[] lines)
    {
        var items = ParseInput(lines);

        var xBounds = GetBoundariesX(items);
        Console.WriteLine($"{xBounds.min} - {xBounds.max}");
        var yBounds = GetBoundariesY(items);
        Console.WriteLine($"{yBounds.min} - {yBounds.max}");

        int x = 0;
        int y = 0;
        var matrix = Utils.GenerateMatrix<string>('.', y, x);
        Utils.PrintMatrix(matrix);
    }

    (int min, int max) GetBoundariesX(List<(int x, int y, string type)> items)
    {
        int min = items.Min(t => t.x);
        int max = items.Max(t => t.x);

        return (min, max);
    }
    (int min, int max) GetBoundariesY(List<(int x, int y, string type)> items)
    {
        int min = items.Min(t => t.y);
        int max = items.Max(t => t.y);

        return (min, max);
    }

    List<(int x, int y, string type)> ParseInput(string[] lines)
    {
        var items = new List<(int x, int y, string type)>();
        foreach(var line in lines)
        {
            // Previous = Day 22 - 2021
            // Example: Sensor at x=2, y=18: closest beacon is at x=-2, y=15
            string pattern = @"(Sensor at )(x=(-?[0-9]*)), (y=(-?[0-9]*)): closest beacon is at (x=(-?[0-9]*)), (y=(-?[0-9]*))";
            var match = Regex.Match(line, pattern);

            if (match.Success) {
                int sX = Int32.Parse(match.Groups[3].Value);
                int sY = Int32.Parse(match.Groups[5].Value);
                items.Add((sX, sY, "S"));

                int bX = Int32.Parse(match.Groups[7].Value);
                int bY = Int32.Parse(match.Groups[9].Value);
                items.Add((bX, bY, "B"));

                // Console.WriteLine($"{sX} {sY} {bX} {bY}");
            }
        }
        return items;
    }
}

Console.WriteLine("-- Day 15 --");

var day15 = new Day15();

string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Console.WriteLine("Part 1.");
day15.Part1(lines);

// Part 2
// Console.WriteLine("Part 2.");

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();