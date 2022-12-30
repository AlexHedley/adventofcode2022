#load "../utils/utils.csx"

public class Day13
{
    public void Part1(string[] lines)
    {
        var items = ParseInput(lines);
        Console.WriteLine(items.Count);

        foreach(var item in items)
        {
            ParseItem(item);
        }
    }



    void ParseItem(List<string> item)
    {
        foreach(var line in item)
        {
            Console.WriteLine($"LINE: {line}");
            
            
        }
    }

    List<List<string>> ParseInput(string[] lines)
    {
        List<List<string>> items = new List<List<string>>();

        List<string> curSet = new List<string>();    
        foreach (String line in lines)
        {
            if (line.Trim().Length == 0)
            {
                if (curSet.Count > 0)
                {
                    items.Add(curSet);
                    curSet = new List<string>();
                }               
            }
            else
            {
                curSet.Add(line);
            }
        }

        if (curSet.Count > 0)
        {
            items.Add(curSet);
        }

        return items;
    }
}

Console.WriteLine("-- Day 13 --");

var day13 = new Day13();

string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Console.WriteLine("Part 1.");
day13.Part1(lines);

// Part 2
// Console.WriteLine("Part 2.");

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();