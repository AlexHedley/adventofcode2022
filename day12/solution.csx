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

        var nearest = CheckNearest(matrix, 0, 0);
        var last = nearest.Last();
        nearest = CheckNearest(matrix, last.Item1, last.Item2);

        
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

    List<Tuple<int, int>> CheckNearest(string[,] matrix, int i, int j)
    {
        List<Tuple<int, int>> moves = new List<Tuple<int, int>>();

        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        var top = "";
        var left = "";
        var bottom = "";
        var right = "";

        var currentValue = matrix[i, j];

        bool canMoveTop = false;
        bool canMoveLeft = false;
        bool canMoveBottom = false;
        bool canMoveRight = false;

        if (i-1 > -1)
        {
            top = matrix[i-1, j];
            canMoveTop = CanMove(currentValue, top);
            if (canMoveTop) moves.Add(Tuple.Create(i-1, j));
        }
        if (j-1 > -1)
        {
            left = matrix[i, j-1];
            canMoveLeft = CanMove(currentValue, left);
            if (canMoveLeft) moves.Add(Tuple.Create(i, j-1));
        }
        if (i+1 < rowLength)
        {
            bottom = matrix[i+1, j]; 
            canMoveBottom = CanMove(currentValue, bottom);
            if (canMoveBottom) moves.Add(Tuple.Create(i+1, j));
        }
        if (j+1 < colLength)
        {
            right = matrix[i, j+1];
            canMoveRight = CanMove(currentValue, right);
            if (canMoveRight) moves.Add(Tuple.Create(i, j+1));
        }

        // Console.WriteLine($"T:{top} {canMoveTop} | L:{left} {canMoveLeft} | B:{bottom} {canMoveBottom} | R:{right} {canMoveRight}");
        moves.ForEach(Console.WriteLine);
        Console.WriteLine();
        return moves;
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