#load "../utils/utils.csx"

public class Day08
{
    public void GetInnerMatrix(int[,] matrix)
    {
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        for (int i = 1; i < rowLength - 1; i++)
        {
            for (int j = 1; j < colLength - 1; j++)
            {
                Console.Write($"{matrix[i, j]}");
            }
            Console.WriteLine();
        }
    }

    public void ProcessMatrix(int[,] matrix)
    {
        var c = 0;
        var counter = 0;

        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        for (int i = 1; i < rowLength - 1; i++)
        {
            for (int j = 1; j < colLength - 1; j++)
            {
                // Console.Write(matrix[i, j]);
                var isVisible = CheckDirections(matrix, i, j);
                // Console.WriteLine($"{matrix[i, j]}: {isVisible}");
                Console.Write(isVisible.ToString().Substring(0, 1));
                // Console.WriteLine();
                if (isVisible) counter++;
                c++;
                // Console.WriteLine();
            }
            Console.WriteLine();
        }
        Console.WriteLine($"Inner Trees: {counter} [{c}]");

        var count = CountOfMatrixEdge(matrix);
        // Console.WriteLine($"Count: {count}");
        Console.WriteLine($"Total: {counter + count}");
    }

    public int CountOfMatrixEdge(int[,] matrix)
    {
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        var total = (rowLength * 2) + (colLength * 2) - 4;
        return total;
    }

    bool CheckDirections(int[,] matrix, int rowIndex, int colIndex)
    {
        var row = Utils.GetRow(matrix, rowIndex);
        // Console.WriteLine($"{matrix[rowIndex, colIndex]} :{string.Join(", ", row)}");

        var visibleLeft = CheckLeft(row, colIndex);
        // Console.WriteLine($"{matrix[rowIndex, colIndex]}:{visibleLeft} (L)");

        var visibleRight = CheckRight(row, colIndex);
        // Console.WriteLine($"{matrix[rowIndex, colIndex]}:{visibleRight} (R)");

        var col = Utils.GetColumn(matrix, colIndex);
        // Console.WriteLine($"{string.Join(", ", col)}");

        var visibleTop = CheckTop(col, rowIndex);
        // Console.WriteLine($"{visibleTop} (T)");

        var visibleBottom = CheckBottom(col, rowIndex);
        // Console.WriteLine($"{visibleBottom} (B)");

        // Console.WriteLine($"{matrix[rowIndex, colIndex]}: T{visibleTop} R{visibleRight} B{visibleBottom} L{visibleLeft}");
        // Console.WriteLine();

        var isVisible = new bool[] { visibleLeft, visibleRight, visibleTop, visibleBottom };
        return isVisible.Any(x => x == true);
    }

    bool CheckLeft(int[] row, int index)
    {
        var item = row[index];
        // Console.WriteLine($"L Item: {item} ({index}) [{string.Join(", ", row)}]");
        var items = row[0..index];
        // Console.WriteLine($"Items: {string.Join(", ", items)}");

        // return row.All(i => i <= item);
        foreach (var i in items)
        {
            if (i >= item) return false;
        }
        return true;
    }

    bool CheckRight(int[] row, int index)
    {
        var item = row[index];
        // Console.WriteLine($"R Item: {item} ({index}) [{string.Join(", ", row)}]");
        var items = row[(index + 1)..row.Length];
        // Console.WriteLine($"Items: {string.Join(", ", items)}");

        // return row.All(i => i <= item);
        foreach (var i in items)
        {
            if (i >= item) return false;
        }
        return true;
    }

    bool CheckTop(int[] col, int index)
    {
        var item = col[index];
        // Console.WriteLine($"T Item: {item} ({index}) [{string.Join(", ", col)}]");
        var items = col[0..index];
        // Console.WriteLine($"Items: {string.Join(", ", items)}");

        foreach (var i in items)
        {
            if (i >= item) return false;
        }
        return true;
    }

    bool CheckBottom(int[] col, int index)
    {
        var item = col[index];
        // Console.WriteLine($"B Item: {item} ({index}) [{string.Join(", ", col)}]");
        var items = col[(index + 1)..col.Length];
        // Console.WriteLine($"Items: {string.Join(", ", items)}");

        foreach (var i in items)
        {
            if (i >= item) return false;
        }
        return true;
    }
}

Console.WriteLine("-- Day 8 --");

var day08 = new Day08();

// string fileName = @"input-sample.txt";
string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Console.WriteLine("Part 1.");

var rows = lines.Length;
var cols = lines[0].Length;
var matrix = Utils.GenerateMatrix(lines, rows, cols);
Utils.PrintMatrix(matrix);
Console.WriteLine();

// Utils.LoopMatrix(matrix);
// Console.WriteLine();

day08.GetInnerMatrix(matrix);
Console.WriteLine();

day08.ProcessMatrix(matrix);

// var count = day08.CountOfMatrixEdge(matrix);
// Console.WriteLine($"Count: {count}");

// Part 2
// Console.WriteLine("Part 2.");

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();