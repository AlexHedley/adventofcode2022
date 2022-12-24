#load "../utils/utils.csx"

public class Day24
{
    List<string> blizzardDirections = new List<string>{ "^","v","<",">" };

    public void Part1(string[] lines)
    {
        var rows = lines.Length;
        var cols = lines[0].Length;
        var matrix = Utils.GenerateMatrix<string>(lines, rows, cols);
        Utils.PrintMatrix(matrix);

        var startingPoint = FindStartingPoint(matrix);
        Console.WriteLine($"Starting Point: {startingPoint.rowIndex},{startingPoint.colIndex}");
        var finishingPoint = FindFinishingPoint(matrix);
        Console.WriteLine($"Finishing Point: {finishingPoint.rowIndex},{finishingPoint.colIndex}");

        MoveExpedition(matrix, startingPoint.rowIndex, startingPoint.colIndex);
        Utils.PrintMatrix(matrix);

        // # - Walls
        // . - Clear ground
        // ^ v < > - Blizzard Directions
        var blizzards = FindBlizzards(matrix);

        var counter = 5;
        for (int i = 0; i < counter; i++)
        {
            // blizzards.ForEach(b => Console.WriteLine($"{b.x},{b.y} ({b.direction})"));
            blizzards = MoveItems(blizzards, matrix);
            TryMoveExpedition(matrix);
            Utils.PrintMatrix(matrix);

            var expedition = FindExpedition(matrix);
            Console.WriteLine($"Expedition: {expedition.x},{expedition.y}");
            if (expedition == finishingPoint)
            {
                Console.WriteLine($"Done: {i+1}");
            }
        }
    }

    public List<(int x, int y, string direction)> MoveItems(List<(int x, int y, string direction)> items, string[,] matrix)
    {
        var piece = "";
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);
        // Console.WriteLine($"{rowLength}:{colLength}");
        List<(int x, int y, string direction)> newPositions = new List<(int x, int y, string direction)>();

        foreach(var item in items)
        {
            var i = item.x;
            var j = item.y;
            // Console.WriteLine($"{i},{j} ({item.direction})");
            // Update previous
            Utils.UpdatePosition(matrix, item.x, item.y, ".");

            switch (item.direction)
            {
                case "^":
                    if (i-1 > -1) // North
                    {
                        piece = matrix[i-1, j];
                        if (IsWall(piece))
                        {
                            piece = matrix[rowLength-1, j];
                            if (IsWall(piece))
                            {
                                piece = matrix[rowLength-2, j];
                                Utils.UpdatePosition(matrix, rowLength-2, j, item.direction);
                                newPositions.Add((rowLength-2, j, item.direction));
                            }
                            else
                            {
                                Utils.UpdatePosition(matrix, rowLength-1, j, item.direction);
                                newPositions.Add((rowLength-1, j, item.direction));
                            }
                        }
                        else
                        {
                            Utils.UpdatePosition(matrix, i-1, j, item.direction);
                            newPositions.Add((i-1, j, item.direction));
                        }
                    }
                    break;
                case "v":
                    if (i+1 < rowLength) // South
                    {
                        piece = matrix[i+1, j];
                        if (IsWall(piece))
                        {
                            piece = matrix[0, j];
                            if (IsWall(piece))
                            {
                                piece = matrix[1, j];
                                Utils.UpdatePosition(matrix, 1, j, item.direction);
                                newPositions.Add((1, j, item.direction));
                            }
                            else
                            {
                                Utils.UpdatePosition(matrix, 0, j, item.direction);
                                newPositions.Add((0, j, item.direction));
                            }
                        }
                        else
                        {
                            Utils.UpdatePosition(matrix, i+1, j, item.direction);
                            newPositions.Add((i+1, j, item.direction));
                        }
                    }
                    break;
                case "<":
                    if (j-1 > -1) // West
                    {
                        piece = matrix[i, j-1];
                        if (IsWall(piece))
                        {
                            piece = matrix[i, colLength-1];
                            if (IsWall(piece))
                            {
                                piece = matrix[i, colLength-2];
                                Utils.UpdatePosition(matrix, i, colLength-2, item.direction);
                                newPositions.Add((i, colLength-2, item.direction));
                            }
                            else
                            {
                                Utils.UpdatePosition(matrix, i, colLength-1, item.direction);
                                newPositions.Add((i, colLength-1, item.direction));
                            }
                        }
                        else
                        {
                            Utils.UpdatePosition(matrix, i, j-1, item.direction);
                            newPositions.Add((i, j-1, item.direction));
                        }
                    }
                    break;
                case ">":
                    if (j+1 < colLength) // East
                    {
                        piece = matrix[i, j+1];
                        if (IsWall(piece))
                        {
                            piece = matrix[i, 0];
                            if (IsWall(piece))
                            {
                                piece = matrix[i, 1];
                                Utils.UpdatePosition(matrix, i, 1, item.direction);
                                newPositions.Add((i, 1, item.direction));
                            }
                            else
                            {
                                Utils.UpdatePosition(matrix, i, 0, item.direction);
                                newPositions.Add((i, 0, item.direction));
                            }
                        }
                        else
                        {
                            Utils.UpdatePosition(matrix, i, j+1, item.direction);
                            newPositions.Add((i, j+1, item.direction));
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        return newPositions;
    }

    public bool IsWall(string item)
    {
        return item == "#";
    }

    public void TryMoveExpedition(string[,] matrix)
    {
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        var expedition = FindExpedition(matrix);
        var i = expedition.x;
        var j = expedition.y;
        Console.WriteLine($"TME: {i},{j}");
        var newPosition = (i, j);

        var n = "";
        var e = "";
        var s = "";
        var w = "";
        if (i-1 > -1) // North
        {
            n = matrix[i-1, j];
            if (n == ".") newPosition = (i-1, j);
        }
        if (j+1 < colLength) // East
        {
            e = matrix[i, j+1];
            if (e == ".") newPosition = (i, j+1);
        }
        if (i+1 < rowLength) // South
        {
            s = matrix[i+1, j];
            if (s == ".") newPosition = (i+1, j);
        }
        if (j-1 > -1) // West
        {
            w = matrix[i, j-1];
            if (w == ".") newPosition = (i, j-1);
        }
        Console.WriteLine($"N:{n} | E:{e} | S:{s} | W:{w}");
        Console.WriteLine($"({i}, {j}) == {newPosition}");

        if ((i, j) == newPosition)
        {
            Console.WriteLine("No Change");
        }
        else
        {
            Utils.UpdatePosition(matrix, i, j, "X");
            MoveExpedition(matrix, newPosition.i, newPosition.j);
        }
    }

    public void MoveExpedition(string[,] matrix, int rowIndex, int colIndex)
    {
        Utils.UpdatePosition(matrix, rowIndex, colIndex, "E");
    }

    public (int rowIndex, int colIndex) FindStartingPoint(string[,] matrix)
    {
        return FindEmptyPoint(matrix, 0);
    }

    public (int rowIndex, int colIndex) FindFinishingPoint(string[,] matrix)
    {
        int rowLength = matrix.GetLength(0);
        // int colLength = matrix.GetLength(1);
        // Console.WriteLine($"{rowLength}:{colLength}");
        return FindEmptyPoint(matrix, rowLength-1);
    }

    public (int rowIndex, int colIndex) FindEmptyPoint(string[,] matrix, int row)
    {
        var y = 0;
        var rowItems = Utils.GetRow(matrix, row);
        for (var i = 0; i < rowItems.Length; i++)
        {
            if (rowItems[i] == ".") y = i;
        }

        return (row, y);
    }
    
    public List<(int x, int y, string direction)> FindBlizzards(string[,] matrix)
    {
        var blizzards = new List<(int x, int y, string direction)>();

        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                var item = matrix[i, j];
                if (blizzardDirections.Contains(item))
                {
                    // Console.WriteLine("found");
                    blizzards.Add((i, j, item));
                }
            }
        }

        return blizzards;
    }

    public (int x, int y) FindExpedition(string[,] matrix)
    {
        var position = (0, 0);
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                var item = matrix[i, j];
                if (item == "E")
                {
                    position = (i, j);
                }
            }
        }

        return position;
    }
}

Console.WriteLine("-- Day 24 --");

var day24 = new Day24();

// string fileName = @"input-sample-small.txt";
string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Console.WriteLine("Part 1.");
day24.Part1(lines);

// Part 2
// Console.WriteLine("Part 2.");

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();