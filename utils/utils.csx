using System.Collections.Generic;

public static class Utils
{
    public static string[] GetLines(string fileName)
    {
        return System.IO.File.ReadAllLines(fileName);
    }

    // Generate Matrix
    public static T[,] GenerateMatrix<T>(string[] lines, int rows, int cols)
    {
        T[,] matrix = new T[rows, cols];
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        var a = 0;
        for (var i = 0; i < rowLength; i++)
        {
            for (var j = 0; j < colLength; j++)
            {
                foreach (char c in lines[a].ToCharArray())
                {
                    if (typeof(T) == typeof(int))
                    {
                        // Console.WriteLine(c);
                        matrix[i, j] = (T)(object)int.Parse(c.ToString());
                    }
                    else if (typeof(T) == typeof(string))
                    {
                        matrix[i, j] = (T)(object)c.ToString();
                    }

                    j++;
                }
            }
            a++;
        }

        return matrix;
    }

    public static T[,] GenerateMatrix<T>(char c, int rows, int cols)
    {
        T[,] matrix = new T[rows, cols];
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        for (var i = 0; i < rowLength; i++)
        {
            for (var j = 0; j < colLength; j++)
            {
                if (typeof(T) == typeof(int))
                {
                    // Console.WriteLine(c);
                    matrix[i, j] = (T)(object)0;
                }
                else if (typeof(T) == typeof(string))
                {
                    matrix[i, j] = (T)(object)c.ToString();
                }
            }
        }

        return matrix;
    }

    // Print Matrix
    public static void PrintMatrix<T>(T[,] matrix)
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
    public static void LoopMatrix<T>(T[,] matrix)
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

    public static T[] GetColumn<T>(T[,] matrix, int columnNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber])
                .ToArray();
    }

    public static T[] GetRow<T>(T[,] matrix, int rowNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[rowNumber, x])
                .ToArray();
    }
}