#load "../utils/utils.csx"

public class Day09
{
    public List<(int row, int col)> HeadPositions = new List<(int row, int col)>();
    public List<(int row, int col)> TailPositions = new List<(int row, int col)>();

    public void Part1(string[] lines)
    {
        // var initial = new string[] { "......", "......", "......", "......", "......", "......" };

        var commands = ParseInputs(lines);
        // day09.PrintCommands(commands);
        var leftMax = commands.Where(c => c.direction == "L").Max();
        var rightMax = commands.Where(c => c.direction == "R").Max();
        var upMax = commands.Where(c => c.direction == "U").Max();
        var downMax = commands.Where(c => c.direction == "D").Max();
        // Console.WriteLine($"U{upMax} D{downMax} L{leftMax} R{rightMax}");

        var colsMax = new List<int>() { leftMax.count, rightMax.count }.Max();
        var rowsMax = new List<int>() { upMax.count, downMax.count }.Max();
        // Console.WriteLine($"R#:{rowsMax} C#:{colsMax}");

        var rows = rowsMax + 1; // 6 ?
        var cols = colsMax + 1; // 6 ?
        // Console.WriteLine($"R#:{rows} C#:{cols}");

        var matrix = Utils.GenerateMatrix<string>('.', rows, cols);
        // Utils.PrintMatrix(matrix);
        // Console.WriteLine();

        SetStartingPoint(matrix, rowsMax, 0);
        // Utils.PrintMatrix(matrix);
        // Console.WriteLine();

        SetStartingPointHead(matrix, rowsMax, 0);
        // Utils.PrintMatrix(matrix);
        // Console.WriteLine();

        SetStartingPointTail(matrix, rowsMax, 0);
        // Utils.PrintMatrix(matrix);
        // Console.WriteLine();

        // PrintPositions();

        RunCommands(matrix, commands);
        // Utils.PrintMatrix(matrix);
        // PrintPositions();

        var totalMoves = commands.Sum(c => c.count);
        Console.WriteLine($"Moves: {totalMoves}");

        Utils.PrintMatrix(matrix);
        Console.WriteLine();

        // TODO: Remove the first position!

        HeadPositions.Remove((0, 5));
        var headCount = HeadPositions.DistinctBy(c => c).Count();
        Console.WriteLine($"Head Count: {headCount} ({HeadPositions.Count})");

        TailPositions.Remove((0, 5));
        var tailCount = TailPositions.DistinctBy(c => c).Count();
        Console.WriteLine($"Tail Count: {tailCount} ({TailPositions.Count})");

        Console.WriteLine();

        // var cleanMatrix = Utils.GenerateMatrix<string>('.', rows, cols);
        // ShowPath(cleanMatrix, "H");
        // Console.WriteLine();
        var cleanMatrix = Utils.GenerateMatrix<string>('.', rows, cols);
        ShowPath(cleanMatrix, "T");
    }

    public void UpdatePosition(string[,] matrix, int rowIndex, int colIndex, string symbol)
    {
        matrix[rowIndex, colIndex] = symbol;
    }

    public void UpdateHead(string[,] matrix, int rowIndex, int colIndex)
    {
        SwitchLastPosition(matrix, "H");
        UpdatePosition(matrix, rowIndex, colIndex, "H");
        HeadPositions.Add((rowIndex, colIndex));
    }

    public void UpdateTail(string[,] matrix, int rowIndex, int colIndex)
    {
        SwitchLastPosition(matrix, "T");
        UpdatePosition(matrix, rowIndex, colIndex, "T");
        TailPositions.Add((rowIndex, colIndex));
    }

    public void SwitchLastPosition(string[,] matrix, string position)
    {
        if (position == "H")
        {
            if (HeadPositions.Count > 0)
            {
                var lastPosition = HeadPositions[^1];
                UpdatePosition(matrix, lastPosition.row, lastPosition.col, ".");
            }
        }
        if (position == "T")
        {
            if (TailPositions.Count > 0)
            {
                var lastPosition = TailPositions[^1];
                UpdatePosition(matrix, lastPosition.row, lastPosition.col, ".");
            }
        }
    }

    public void SetStartingPoint(string[,] matrix, int rowIndex, int colIndex)
    {
        UpdatePosition(matrix, rowIndex, colIndex, "s");
    }

    public void SetStartingPointHead(string[,] matrix, int rowIndex, int colIndex)
    {
        UpdateHead(matrix, rowIndex, colIndex); // Don't want to count at end...
        // UpdatePosition(matrix, rowIndex, colIndex, "H");
    }

    public void SetStartingPointTail(string[,] matrix, int rowIndex, int colIndex)
    {
        UpdateTail(matrix, rowIndex, colIndex);
        // UpdatePosition(matrix, rowIndex, colIndex, "T");
    }

    public void PrintPositions()
    {
        Console.WriteLine("Head Positions");
        HeadPositions.ForEach(h => Console.WriteLine($"{h.row}, {h.col}"));
        Console.WriteLine("Tail Positions");
        TailPositions.ForEach(t => Console.WriteLine($"{t.row}, {t.col}"));
    }

    public List<(string direction, int count)> ParseInputs(string[] lines)
    {
        List<(string direction, int count)> commands = new List<(string direction, int count)>();
        foreach (var line in lines)
        {
            commands.Add(ParseInput(line));
        }
        return commands;
    }

    public (string direction, int count) ParseInput(string line)
    {
        return line.Split(" ") switch { var a => (a[0], Int32.Parse(a[1])) };
    }

    public void PrintCommands(List<(string direction, int count)> commands)
    {
        commands.ForEach(c => Console.WriteLine($"{c.direction}, {c.count}"));
    }

    public void RunCommands(string[,] matrix, List<(string direction, int count)> commands)
    {
        foreach (var command in commands)
        {
            RunCommand(matrix, command);
        }
    }

    public void RunCommand(string[,] matrix, (string direction, int count) command)
    {
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);
        // Console.WriteLine($"RL:{rowLength} | CL:{colLength}");

        // Now for the actual work...
        // Get the last position of HEAD.
        var headPosition = HeadPositions[^1];
        // Console.WriteLine($"H: {headPosition.row}, {headPosition.col}");

        var row = headPosition.row;
        var col = headPosition.col;

        for (var move = 0; move < command.count; move++)
        {
            // Console.WriteLine($"{command.direction}: {move}");
            // Move H
            switch (command.direction)
            {
                case "U":
                    if (row == 0) break;
                    row -= 1;
                    break;
                case "D":
                    if (row == rowLength - 1) break;
                    row += 1;
                    break;
                case "L":
                    if (col == 0) break;
                    col -= 1;
                    break;
                case "R":
                    if (col == colLength - 1) break;
                    col += 1;
                    break;
                default:
                    break;
            }

            if (headPosition.row != row || headPosition.col != col)
            {
                UpdateHead(matrix, row, col);
                UpdateTail(matrix, command.direction);
                // Utils.PrintMatrix(matrix);
                // PrintPositions();
            }
        }
    }

    public void UpdateTail(string[,] matrix, string direction)
    {
        var headPosition = HeadPositions[^1];
        // Console.WriteLine($"H: {headPosition.row}, {headPosition.col}");
        var tailPosition = TailPositions[^1];
        // Console.WriteLine($"T: {tailPosition.row}, {tailPosition.col}");

        var row = tailPosition.row;
        var col = tailPosition.col;

        var rowDiff = Math.Abs(headPosition.row - tailPosition.row);
        var colDiff = Math.Abs(headPosition.col - tailPosition.col);
        // Console.WriteLine($"Diff - R:{rowDiff} | C:{colDiff}");

        if (headPosition.row == tailPosition.row || headPosition.col == tailPosition.col)
        {
            // U D L R
            // Console.WriteLine($"UDLR ({direction})");
            switch (direction)
            {
                case "U":
                    if (rowDiff > 1)
                        row -= 1;
                    break;
                case "D":
                    if (rowDiff > 1)
                        row += 1;
                    break;
                case "L":
                    if (colDiff > 1)
                        col -= 1;
                    break;
                case "R":
                    if (colDiff > 1)
                        col += 1;
                    break;
                default:
                    break;
            }
        }
        else if (rowDiff > 1 || colDiff > 1)
        {
            // Handle Diagonals.
            // Console.WriteLine($"Diagonal ({direction})");
            switch (direction)
            {
                case "U":
                    row -= 1;
                    col = headPosition.col;
                    break;
                case "D":
                    row += 1;
                    col = headPosition.col;
                    break;
                case "L":
                    row = headPosition.row;
                    col -= 1;
                    break;
                case "R":
                    row = headPosition.row;
                    col += 1;
                    break;
                default:
                    break;
            }
        }

        if (tailPosition.row != row || tailPosition.col != col)
        {
            // Console.WriteLine($"T: New Position - {row},{col}");
            UpdateTail(matrix, row, col);
        }
    }

    public void ShowPath(string[,] matrix, string pos)
    {
        if (pos == "H")
        {
            foreach (var position in HeadPositions)
            {
                UpdatePosition(matrix, position.row, position.col, "#");
            }
        }
        else if (pos == "T")
        {
            foreach (var position in TailPositions)
            {
                UpdatePosition(matrix, position.row, position.col, "#");
            }
        }
        SetStartingPoint(matrix, matrix.GetLength(0) - 1, 0);
        Utils.PrintMatrix(matrix);
    }

}

Console.WriteLine("-- Day 9 --");

var day09 = new Day09();

// string fileName = @"input-sample.txt";
string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Console.WriteLine("Part 1.");
day09.Part1(lines);

// Part 2
// Console.WriteLine("Part 2.");

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();