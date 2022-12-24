using System.Collections.Generic;
using System.Runtime.Serialization;

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
        int rowLength = matrix.GetLength(0); // = rows;
        int colLength = matrix.GetLength(1); // = cols;

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
        Console.WriteLine();
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

    public static void UpdatePosition(string[,] matrix, int rowIndex, int colIndex, string symbol)
    {
        matrix[rowIndex, colIndex] = symbol;
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

    // https://stackoverflow.com/a/3261006
    public static Tuple<int, int> CoordinatesOf<T>(T[,] matrix, T value)
    {
        int w = matrix.GetLength(0); // width
        int h = matrix.GetLength(1); // height

        for (int x = 0; x < w; ++x)
        {
            for (int y = 0; y < h; ++y)
            {
                if (matrix[x, y].Equals(value))
                    return Tuple.Create(x, y);
            }
        }

        return Tuple.Create(-1, -1);
    }

    // https://codereview.stackexchange.com/a/44549
    public static int toNumber(String name)
    {
        int number = 0;
        for (int i = 0; i < name.Length; i++)
        {
            number = number * 26 + (name[i] - ('A' - 1));
        }
        return number;
    }

    public static String toName(int number)
    {
        StringBuilder sb = new StringBuilder();
        while (number-- > 0)
        {
            sb.Append((char)('A' + (number % 26)));
            number /= 26;
        }
        return new string(sb.ToString().Reverse().ToArray());
    }
}

public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct
{
    if (string.IsNullOrWhiteSpace(value))
        return default;

    return Enum.TryParse(value, true, out TEnum result) ? result : default;
}

public static T ToEnumFromValue<T>(this string str)
{
    var enumType = typeof(T);
    foreach (var name in Enum.GetNames(enumType))
    {
        var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
        if (enumMemberAttribute.Value == str) return (T)Enum.Parse(enumType, name);
    }
    //throw exception or whatever handling you want or
    return default(T);
}