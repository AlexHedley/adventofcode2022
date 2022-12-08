public static class Utils
{
    public static string[] GetLines(string fileName)
    {
        return System.IO.File.ReadAllLines(fileName);
    }

    // Generate Matrix
    public static int[,] GenerateMatrix(string[] lines, int rows, int cols)
    {
        int[,] matrix = new int[rows, cols];
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        var a = 0;
        for (var i = 0; i < rowLength; i++)
        {
            for (var j = 0; j < colLength; j++)
            {
                foreach (char c in lines[a].ToCharArray())
                {
                    // Console.WriteLine(c);
                    matrix[i, j] = int.Parse(c.ToString());
                    j++;
                }
            }
            a++;
        }

        return matrix;
    }

    // Print Matrix
    public static void PrintMatrix(int[,] matrix)
    {
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                Console.Write($"{matrix[i, j]}");
            }
            Console.Write(Environment.NewLine);
        }
    }

    // Loop Matrix
    public static void LoopMatrix(int[,] matrix)
    {
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                Console.Write(matrix[i, j]);
            }
        }
    }

    public static int[] GetColumn(int[,] matrix, int columnNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber])
                .ToArray();
    }

    public static int[] GetRow(int[,] matrix, int rowNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[rowNumber, x])
                .ToArray();
    }
}