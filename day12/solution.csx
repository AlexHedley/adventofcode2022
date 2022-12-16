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
        UpdatePosition(matrix, sp.Item1, sp.Item2, "a");

        var ep = Utils.CoordinatesOf(matrix, "E");
        Console.WriteLine(ep);
        UpdatePosition(matrix, ep.Item1, ep.Item2, "z");

        Utils.PrintMatrix(matrix);
        Console.WriteLine();

        // var spValue = "a"; //matrix[0,0];
        // int index = char.ToUpper(char.Parse(spValue));
        // Console.WriteLine($"{spValue}: {index}");

        CheckNearest(matrix, 0, 0);
        
        // Starting Position
        // UpdatePosition(matrix, 0, 0, "S");
        // Utils.PrintMatrix(matrix);
        // var matrix = Utils.GenerateMatrix<string>('.', rows, cols);
    }

    bool CanMove(string current, string toCheck)
    {
        if (string.IsNullOrEmpty(toCheck)) return false;
        
        int currentIndex = char.ToUpper(char.Parse(current));
        int toCheckIndex = char.ToUpper(char.Parse(toCheck));

        var diff = Math.Abs(currentIndex - toCheckIndex);
        return diff > 1 ? false : true;
    }

    void CheckNearest(string[,] matrix, int i, int j)
    {
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        var top = "";
        var left = "";
        var bottom = "";
        var right = "";

        var currentValue = matrix[i, j];

        if (i-1 > -1)
        {
            top = matrix[i-1, j];
        }
        if (j-1 > -1)
        {
            left = matrix[i, j-1];
        }
        if (i+1 < rowLength)
        {
            bottom = matrix[i+1, j]; 
        }
        if (j+1 < colLength)
        {
            right = matrix[i, j+1];
        }

        bool canMoveTop = CanMove(currentValue, top);
        bool canMoveLeft = CanMove(currentValue, left);
        bool canMoveBottom = CanMove(currentValue, bottom);
        bool canMoveRight = CanMove(currentValue, right);

        Console.WriteLine($"T:{top} {canMoveTop} | L:{left} {canMoveLeft} | B:{bottom} {canMoveBottom} | R:{right} {canMoveRight}");
    }

    void UpdatePosition(string[,] matrix, int rowIndex, int colIndex, string symbol)
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