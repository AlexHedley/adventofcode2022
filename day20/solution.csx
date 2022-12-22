#load "../utils/utils.csx"

using System.Collections;

public class Day20
{
    public void Part1(string[] lines)
    {
        Console.WriteLine($"IsUnique: {IsUnique(lines)}");
        // ShowDuplicates(lines);

        // var indexName = Utils.toName(1381); 
        // Console.WriteLine(indexName);
        
        var mappingDict = CreateMappingDict(lines);
        // PrintDictionary(mappingDict);
        List<string> keyList = new List<string>(mappingDict.Keys);

        // var list = ReOrder(lines);
        var list = ReOrder(keyList.ToArray(), mappingDict);
        PrintArray(list);
        PrintArray(list, mappingDict);

        // int itemIndex = list.IndexOf("0");
        // Console.WriteLine($"0: [{itemIndex}]: 0");
        var myKey = mappingDict.FirstOrDefault(x => x.Value == 0).Key;
        int itemIndex = list.IndexOf(myKey);
        Console.WriteLine($"0: [{myKey}] [{itemIndex}]: 0");

        var length = lines.Length;
        var oneThousand = WrapAround(itemIndex+1001, length);
        // Console.WriteLine($"1000: [{oneThousand}]: {list[oneThousand]}");
        var twoThousand = WrapAround(oneThousand+1001, length);
        // Console.WriteLine($"2000: [{twoThousand}]: {list[twoThousand]}");
        var threeThousand = WrapAround(twoThousand+1001, length);
        // Console.WriteLine($"3000: [{threeThousand}]: {list[threeThousand]}");

        var oneThousandLetter = list[oneThousand].ToString();
        var oneThousandValue = mappingDict[oneThousandLetter];
        Console.WriteLine($"1000: [{oneThousand}] [{oneThousandLetter}]: {oneThousandValue}");
        var twoThousandLetter = list[twoThousand].ToString();
        var twoThousandValue = mappingDict[twoThousandLetter];
        Console.WriteLine($"2000: [{twoThousand}] [{twoThousandLetter}]: {twoThousandValue}");
        var threeThousandLetter = list[threeThousand].ToString();
        var threeThousandValue = mappingDict[threeThousandLetter];
        Console.WriteLine($"3000: [{threeThousand}] [{threeThousandLetter}]: {threeThousandValue}");
        
        // var total = Convert.ToInt32(list[oneThousand]) + Convert.ToInt32(list[twoThousand]) + Convert.ToInt32(list[threeThousand]);
        var total = oneThousandValue + twoThousandValue + threeThousandValue;
        Console.WriteLine($"Total: {total}");
    }

    ArrayList ReOrder(string[] lines, Dictionary<string, int> mappingDict)
    {
        var initialOrder = new string[lines.Length];
        lines.CopyTo(initialOrder, 0);

        // Console.WriteLine(string.Join(",", initialOrder));

        var arrayList = new ArrayList();
        arrayList.AddRange(lines);
        // PrintArray(arrayList);

        for(int i = 0; i < initialOrder.Length; i++)
        {
            var item = initialOrder[i];
            
            int indexItem = arrayList.IndexOf(item);
            // Console.WriteLine($"[{indexItem}]: {item}");

            var originalValue = mappingDict[item];
            // Console.WriteLine(originalValue);

            // var newPosition = WrapAround(indexItem+Int32.Parse(item), initialOrder.Length);
            var newPosition = WrapAround(indexItem+originalValue, initialOrder.Length);
            // Console.WriteLine($"[{indexItem}]: {item} -> [{newPosition}]");

            arrayList.RemoveAt(indexItem);
            arrayList.Insert(newPosition, item);

            // PrintArray(arrayList);
        }

        return arrayList;
    }

    void PrintArray(ArrayList list)
    {
        foreach (object obj in list)  
        {  
            Console.Write(obj);
            Console.Write(" ");
        }
        Console.WriteLine();
    }

    void PrintArray(ArrayList list, Dictionary<string, int> mappingDict)
    {
        foreach (object obj in list)  
        {  
            Console.Write(mappingDict[obj.ToString()]);
            Console.Write(" ");
        }
        Console.WriteLine();
    }

    // AoC 2021 - Day 21
    // https://stackoverflow.com/a/14416133
    public int WrapAround(int x, int length)
    {
        var x_min = 1;
        var x_max = length;
        x = (((x - x_min) % (x_max - x_min)) + (x_max - x_min)) % (x_max - x_min) + x_min;
        return x;
    }

    // https://stackoverflow.com/a/668137
    void MoveWithinArray(Array array, int source, int dest)
    {
        Object temp = array.GetValue(source);
        Array.Copy(array, dest, array, dest + 1, source - dest);
        array.SetValue(temp, dest);
    }

    bool IsUnique(string[] array)
    {
        return array.ToList().Distinct().Count() == array.ToList().Count();
    }

    // https://stackoverflow.com/a/5080587
    void ShowDuplicates(string[] lines)
    {
        var duplicates = lines.ToList()
            .GroupBy(i => i)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key);
        foreach (var d in duplicates)
            Console.WriteLine(d);
    }

    Dictionary<string, int> CreateMappingDict(string[] lines)
    {
        var mappingDict = new Dictionary<string, int>();
        int n = 1;
        do
        {
            var indexName = Utils.toName(n); 
            mappingDict.Add(indexName, Int32.Parse(lines[n-1]));
            //Console.WriteLine($"[{indexName}]: {lines[n-1]}");

            n++;
        } while (n < lines.Length + 1);

        return mappingDict;
    }
    
    void PrintDictionary(Dictionary<string, int> dictionary)
    {
        dictionary.Select(i => $"{i.Key}: {i.Value}").ToList().ForEach(Console.WriteLine);
    }
}

Console.WriteLine("-- Day 20 --");

var day20 = new Day20();

// string fileName = @"input-sample.txt";
string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Console.WriteLine("Part 1.");
day20.Part1(lines);

// Part 2
// Console.WriteLine("Part 2.");

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();