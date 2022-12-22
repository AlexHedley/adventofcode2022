#load "../utils/utils.csx"

using System.Text.RegularExpressions;

public class Day22
{
    public void Part1(string[] lines)
    {
        var info = ParseInput(lines);
        var map = info.Map;
        var path = info.Path;

        // map.ToList().ForEach(Console.WriteLine);
        // Console.WriteLine(path);

        var rows = map.Length;
        var cols = map.OrderByDescending(s => s.Length).FirstOrDefault().Length; // var cols = map[0].Length;

        // Console.WriteLine($"{rows}, {cols}");
        var matrix = Utils.GenerateMatrix<string>(info.Map, rows, cols);
        // Utils.PrintMatrix(matrix);

        UpdatePosition(matrix, 0, 0, "X");
        // Utils.PrintMatrix(matrix);

        ParsePath(path, matrix);
        Utils.PrintMatrix(matrix);
    }

    void ParsePath(string path, string[,] matrix)
    {
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        // Example: 10R5L5R10L4R5L5

        int row = 0;
        int col = 0;
        string facing = "E";
        string letter = "R";

        var pattern = @"\d+|\D+";
        // var match = Regex.Match(path, pattern);
        Regex regex = new Regex(pattern);

        foreach (Match match in regex.Matches(path))
        {
            // Console.WriteLine($"{facing} {letter} {match.Value.ToString()}");
            
            if (Int32.TryParse(match.Value, out int num))
            {
                // Move num
                // TODO: Check for wall and wrap around.
                switch (facing)
                {
                    case "N":
                        // row = row - num;
                        row = WrapAround(row-num, rowLength);
                        break;
                    case "E":
                        // col = col + num;
                        col = WrapAround(col+num, rowLength);
                        break;
                    case "S":
                        // row = row + num;
                        row = WrapAround(row+num, rowLength);
                        break;
                    case "W":
                        // col = col - num;
                        col = WrapAround(col-num, rowLength);
                        break;
                    default:
                        break;
                }
                UpdatePosition(matrix, row, col, "X");
                // Utils.PrintMatrix(matrix);
            }
            else
            {
                letter = match.Value;
                switch (letter)
                {
                    case "R":
                        facing = Rotate(facing, "R");
                        break;
                    case "L":
                        facing = Rotate(facing, "L");
                        break;
                    default:
                        break;
                }
            }
        }
    }

    string Rotate(string facing, string turn)
    {
        // TODO: Decision Matrix instead

        // R - Clockwise
        // L - CounterClockwise
        //   N
        // W ✚ E
        //   S
        switch (facing)
        {
            case "N":
                switch (turn)
                {
                    case "R":
                        facing = "E";
                        break;
                    case "L":
                        facing = "W";
                        break;
                    default:
                        break;
                }
                break;
            case "E":
                switch (turn)
                {
                    case "R":
                        facing = "S";
                        break;
                    case "L":
                        facing = "N";
                        break;
                    default:
                        break;
                }
                break;
            case "S":
                switch (turn)
                {
                    case "R":
                        facing = "W";
                        break;
                    case "L":
                        facing = "E";
                        break;
                    default:
                        break;
                }
                break;
            case "W":
                switch (turn)
                {
                    case "R":
                        facing = "N";
                        break;
                    case "L":
                        facing = "S";
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        return facing;
    }
    (string[] Map, string Path) ParseInput(string[] lines)
    {
        string[] map = new string[lines.Length];
        bool isPath = false;
        string path = "";
        for (var i = 0; i < lines.Length; i++)
        {
            if (isPath) 
            {
                path = lines[i];
                continue;
            }
            if (lines[i].Trim().Length == 0)
            {
                isPath = true;
                continue;
            }
            else
            {
                map[i] = lines[i];
            }
        }
        map = map.Where(x => !string.IsNullOrEmpty(x)).ToArray();

        return (map, path);
    }

    public void UpdatePosition(string[,] matrix, int rowIndex, int colIndex, string symbol)
    {
        matrix[rowIndex, colIndex] = symbol;
    }

    public int WrapAround(int x, int length)
    {
        var x_min = 0;
        var x_max = length - 1;
        x = (((x - x_min) % (x_max - x_min)) + (x_max - x_min)) % (x_max - x_min) + x_min;
        return x;
    }
}

Console.WriteLine("-- Day 22 --");

var day22 = new Day22();

string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Console.WriteLine("Part 1.");
day22.Part1(lines);

// Part 2
// Console.WriteLine("Part 2.");

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();