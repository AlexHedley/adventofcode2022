#load "../utils/utils.csx"

public class Day23
{
    public void Part1(string[] lines)
    {
        var rows = lines.Length;
        var cols = lines[0].Length;
        var matrix = Utils.GenerateMatrix<string>(lines, rows, cols);
        
        Console.WriteLine("Initial");
        Utils.PrintMatrix(matrix);
        Console.WriteLine();

        // Elves # | empty ground .
        // N (north), S (south), W (west), and E (east)
        // NE, NW, SE, SW. - diagonal directions
        var moves = GetMoves(matrix);
        // Console.WriteLine($"Moves #:{moves.Count}");

        Console.WriteLine("Round 1");
        UpdateMoves(moves, matrix);

        Utils.PrintMatrix(matrix);
        Console.WriteLine();
    }

    public void UpdateMoves(List<((int x, int y), (int x, int y))> moves, string[,] matrix)
    {
        foreach(var move in moves)
        {
            var one = move.Item1;
            var two = move.Item2;
            // Console.WriteLine($"From: {one.x},{one.y} - To: {two.x},{two.y}");

            var itemsToCheck = moves.ToList();
            itemsToCheck.Remove(move);
            // Console.WriteLine(itemsToCheck.Count);
            var duplicates = false;
            foreach(var item in itemsToCheck)
            {
                // Console.WriteLine($"From: {item.Item1.x},{item.Item1.y} - To: {item.Item2.x},{item.Item2.y}");
                if (move.Item2.x == item.Item2.x && move.Item2.y == item.Item2.y) duplicates = true;
            }
            
            // var duplicates = itemsToCheck.Where(m => m.Item2.x == two.x && m.Item2.y == two.y);
            // if (duplicates) Console.WriteLine("1111");
            // Console.WriteLine(duplicates != null);

            if (!duplicates)
            {
                UpdatePosition(matrix, one.x, one.y, ".");
                UpdatePosition(matrix, two.x, two.y, "#");
            }
        }
        
    }

    public List<((int x, int y), (int x, int y))> GetMoves(string[,] matrix)
    {
        var moves = new List<((int x, int y), (int x, int y))>();
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                var currentValue = matrix[i, j];
                // Console.WriteLine($"{currentValue} | {i}:{j}");

                if (currentValue != "#") continue;
                // Console.WriteLine($"CURRENT ELF - {currentValue} | {i}:{j}");

                var adjecents = new List<string>();
                //   N  
                // W ✚ E
                //   S  
                var n = "";
                var e = "";
                var s = "";
                var w = "";

                // 0 0
                if (i-1 > -1) // North
                {
                    n = matrix[i-1, j];
                    adjecents.Add(n);
                }
                if (j+1 < colLength) // East
                {
                    e = matrix[i, j+1];
                    adjecents.Add(e);
                }
                if (i+1 < rowLength) // South
                {
                    s = matrix[i+1, j];
                    adjecents.Add(s); 
                }
                if (j-1 > -1) // West
                {
                    w = matrix[i, j-1];
                    adjecents.Add(w);
                }
                // Console.WriteLine($"N:{n} | E:{e} | S:{s} | W:{w}");

                // NW NE
                //   ✚
                // SW SE
                var ne = "";
                var se = "";
                var sw = "";
                var nw = "";

                if (i-1 > -1 && j+1 < colLength)
                {
                    ne = matrix[i-1, j+1];
                    adjecents.Add(ne);
                }
                if (i+1 < rowLength && j+1 < colLength)
                {
                    se = matrix[i+1, j+1];
                    adjecents.Add(se);
                }
                if (i+1 < rowLength && j-1 > -1)
                {
                    sw = matrix[i+1, j-1];
                    adjecents.Add(sw); 
                }
                if (i-1 > -1 && j-1 > -1)
                {
                    nw = matrix[i-1, j-1];
                    adjecents.Add(nw);
                }
                
                // Console.WriteLine($"NE:{ne} | SE:{se} | SW:{sw} | NW:{nw}");

                // adjecents.ForEach(Console.WriteLine);
                // Console.WriteLine($"{string.Join(", ", adjecents)}");

                // First Half
                // Considers the eight positions adjacent to themself
                // If no other Elves are in one of those eight positions, the Elf does not do anything during this round.
                var noElves = adjecents.All(x => x == ".");
                if (noElves)
                {
                    // Console.WriteLine("Do Nothing");
                }
                else
                {
                    // N, NE, or NW -> N
                    var goNorth = new List<string>() { n, ne, nw};
                    if (!goNorth.Any(x => x == "#"))
                    {
                        // Console.WriteLine("Go North");
                        if (i-1 > -1)
                        {
                            // UpdatePosition(matrix, i, j, ".");
                            // UpdatePosition(matrix, i-1, j, "#");
                            moves.Add(((i, j), (i-1, j)));
                        }
                        
                        continue;
                    }

                    // S, SE, or SW -> S
                    var goSouth = new List<string>() { s, se, sw};
                    if (!goSouth.Any(x => x == "#"))
                    {
                        // Console.WriteLine("Go South");
                        if (i+1 < rowLength)
                        {
                            // UpdatePosition(matrix, i, j, ".");
                            // UpdatePosition(matrix, i+1, j, "#");
                            moves.Add(((i, j), (i+1, j)));
                        }
                        continue;
                    }
                    
                    // W, NW, or SW -> W
                    var goWest = new List<string>() { w, nw, sw};
                    if (!goWest.Any(x => x == "#"))
                    {
                        // Console.WriteLine("Go West");
                        if (j-1 > -1)
                        {
                            // UpdatePosition(matrix, i, j, ".");
                            // UpdatePosition(matrix, i, j-1, "#");
                            moves.Add(((i, j), (i, j-1)));
                        }
                        continue;
                    }

                    // E, NE, or SE -> E
                    var goEast = new List<string>() { e, ne, se};
                    if (!goEast.Any(x => x == "#"))
                    {
                        // Console.WriteLine("Go East");
                        if (j+1 < colLength)
                        {
                            // UpdatePosition(matrix, i, j, ".");
                            // UpdatePosition(matrix, i, j+1, "#");
                            moves.Add(((i, j), (i, j+1)));
                        }
                        continue;
                    }
                }
            }
        }
        return moves;
    }

    public void UpdatePosition(string[,] matrix, int rowIndex, int colIndex, string symbol)
    {
        matrix[rowIndex, colIndex] = symbol;
    }
}

Console.WriteLine("-- Day 23 --");

var day23 = new Day23();

string fileName = @"input-sample-small.txt";
// string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Console.WriteLine("Part 1.");
day23.Part1(lines);

// Part 2
// Console.WriteLine("Part 2.");

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();