#load "../utils/utils.csx"

public class Day06
{
    public int ParseInput(string line)
    {
        var take = 4;
        var i = 0;
        while (i < line.Length)
        {
            var item = line.Skip(i).Take(take).ToArray();
            // Console.WriteLine(item);
            var isUnique = UniqueCharacters(new string(item));
            // Console.WriteLine(isUnique);
            if (isUnique)
            {
                // Console.WriteLine(i + take);
                return i + take;
            }
            i++;
        }
        return 0;
    }

    public void ParseInputs(string[] lines)
    {
        foreach (var line in lines)
        {
            var position = ParseInput(line);
            Console.WriteLine(position);
        }
    }

    public bool UniqueCharacters(String str)
    {
        for (int i = 0; i < str.Length; i++)
            for (int j = i + 1; j < str.Length; j++)
                if (str[i] == str[j])
                    return false;
        return true;
    }
}

Console.WriteLine("-- Day 6 --");

var day06 = new Day06();

string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Console.WriteLine("Part 1.");

// var line = "mjqjpqmgbljsphdztnvjfqwrcgsmlb";
// day06.ParseInput(line);
day06.ParseInputs(lines);

// Part 2
// Console.WriteLine("Part 2.");

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();
