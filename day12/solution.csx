#load "../utils/utils.csx"

public class Day12
{
    public void Part1(string[] lines)
    {
        var rows = lines.Length;
        var cols = lines[0].Length;
        var matrix = Utils.GenerateMatrix<string>(lines, rows, cols);
        Utils.PrintMatrix(matrix);
        Console.WriteLine();
        
        var sp = Utils.CoordinatesOf(matrix, "S");
        Console.WriteLine(sp);

        var ep = Utils.CoordinatesOf(matrix, "E");
        Console.WriteLine(ep);

        var spValue = "a"; //matrix[0,0];
        int index = char.ToUpper(char.Parse(spValue));
        Console.WriteLine($"{spValue}: {index}");
        // Starting Position
        // UpdatePosition(matrix, 0, 0, "S");
        // Utils.PrintMatrix(matrix);
        // var matrix = Utils.GenerateMatrix<string>('.', rows, cols);
    }

    public void UpdatePosition(string[,] matrix, int rowIndex, int colIndex, string symbol)
    {
        matrix[rowIndex, colIndex] = symbol;
    }

    // public void Part2() {}
}

Console.WriteLine("-- Day 12 --");

var day12 = new Day12();

string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Console.WriteLine("Part 1.");
day12.Part1(lines);

// Part 2
// Console.WriteLine("Part 2.");
// day12.Part2();

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();