#load "../utils/utils.csx"

using System.ComponentModel;
using System.IO;

public class Day07
{
    public List<Directory> GetDirectories(string[] lines)
    {
        List<Directory> directories = new List<Directory>();
        directories.Add(new Directory { Name = "/" });

        foreach (var line in lines)
        {
            // dir a
            if (line.Substring(0, 3) == "dir")
            {
                var directoryName = line.Substring(4, line.Length - 4).Trim();
                directories.Add(new Directory { Name = directoryName });
            }
        }
        return directories;
    }

    public void PopulateDirectories(string[] lines, List<Directory> directories)
    {
        Directory currentDirectory = null;
        Directory subDirectory = null;
        var currentDirectoryName = "";
        foreach (var line in lines)
        {
            if (IsCommand(line))
            {
                var itemsOfCommand = line.Split(" ");
                CommandEnum command;
                bool isCommand = Enum.TryParse(itemsOfCommand[1], out command);

                switch (command)
                {
                    case CommandEnum.cd:
                        var itemsOfDirectory = line.Split(" ");
                        currentDirectoryName = itemsOfDirectory[2];
                        // Console.WriteLine($"CDN: {currentDirectoryName}");
                        var directory = directories.FirstOrDefault(d => d.Name == currentDirectoryName);
                        if (directory != null)
                            currentDirectory = directory;
                        // Console.WriteLine($"CD: {currentDirectory}");
                        continue;
                    case CommandEnum.ls:
                        continue;
                    default:
                        break;
                }
            }

            if (IsDirectory(line))
            {
                var itemsOfDirectory = line.Split(" ");
                var directory = itemsOfDirectory[1];
                // Console.WriteLine($"D: {directory}");
                subDirectory = directories.FirstOrDefault(d => d.Name == directory);
                // Console.WriteLine($"SD: {subDirectory}");
                if (subDirectory != null)
                {
                    // Console.WriteLine($"Adding {subDirectory.Name} to {currentDirectory.Name}");

                    if (currentDirectory.Directories == null)
                    {
                        currentDirectory.Directories = new List<Directory>() { subDirectory };
                    }
                    else
                    {
                        currentDirectory.Directories.Add(subDirectory);
                    }
                }
            }
            // Console.WriteLine(currentDirectory);
        }
    }

    public void PopulateFiles(string[] lines, List<Directory> directories)
    {
        Directory currentDirectory = null;
        var currentDirectoryName = "";
        foreach (var line in lines)
        {
            var itemsOfCommand = line.Split(" ");
            CommandEnum command;
            bool isCommand = Enum.TryParse(itemsOfCommand[1], out command);

            switch (command)
            {
                case CommandEnum.cd:
                    var itemsOfDirectory = line.Split(" ");
                    currentDirectoryName = itemsOfDirectory[2];
                    // Console.WriteLine($"CDN: {currentDirectoryName}");
                    var directory = directories.FirstOrDefault(d => d.Name == currentDirectoryName);
                    if (directory != null)
                        currentDirectory = directory;
                    // Console.WriteLine($"CD: {currentDirectory}");
                    continue;
                case CommandEnum.ls:
                    continue;
                default:
                    break;
            }

            if (IsDirectory(line)) continue;

            // Add File to Directory
            (long FileSize, string FileName) fileInfo = line.Split(" ") switch { var a => (long.Parse(a[0]), a[1]) };
            var file = new CustomFile() { Name = fileInfo.FileName, Size = fileInfo.FileSize };

            // Console.WriteLine($"Adding {fileInfo.FileName} to {currentDirectory.Name}");

            if (currentDirectory.Files == null)
            {
                currentDirectory.Files = new List<CustomFile>() { file };
            }
            else
            {
                currentDirectory.Files.Add(file);
            }
        }
    }

    public Dictionary<string, long> GetTotals(List<Directory> directories)
    {
        Dictionary<string, long> totals = new Dictionary<string, long>();

        // File.AppendAllLines(@"directories.txt", new[] { "Starting" });
        //directories.ForEach(d => d.Files.Sum(f => f.Size));
        foreach (var directory in directories)
        {
            // Console.WriteLine($"{directory.Name}: {directory.Files?.Sum(f => f.Size)}");
            var total = GetTotal(directory);
            // File.AppendAllLines(@"directories.txt", new[] { $"{directory.Name}: {total}" });
            totals.Add(directory.Name, total);
        }

        return totals;
    }

    public long GetTotal(Directory directory)
    {
        if (directory.Directories == null)
        {
            return directory.Files?.Sum(f => f.Size) ?? 0L;
        }
        var total = directory.Files?.Sum(f => f.Size) ?? 0L;
        foreach (var dir in directory.Directories)
            total += GetTotal(dir);
        return total;
    }

    public bool IsCommand(string line)
    {
        return line.Substring(0, 1) == "$";
    }

    public bool IsDirectory(string line)
    {
        return line.Substring(0, 3) == "dir";
    }

    enum CommandEnum
    {
        [Description("Invalid")]
        None,
        [Description("Change Directory")]
        cd,
        [Description("List")]
        ls
    }

    public class Directory
    {
        // public Directory() {}

        public string Name = "";

        public List<Directory> Directories; // = new List<Directory>();
        public List<CustomFile> Files;

        // public override string ToString()
        // {
        //     var dirs = "";
        //     if (Directories != null)
        //         dirs = $"Directories '{String.Join(",", Directories)}' ({Directories?.Count ?? 0})";

        //     var files = "";
        //     if (Files != null)
        //         files = $"Files '{String.Join(",", Files)}' ({Files?.Count ?? 0})";

        //     return $"{Name}: {dirs} {files}".Trim();
        // }
    }

    public class CustomFile
    {
        // public CustomFile() {}
        public string Name = "";
        public long Size = 0L;

        // public override string ToString() => $"{Name}: {Size}";
    }
}

Console.WriteLine("-- Day 7 --");

var day07 = new Day07();

// string fileName = @"input-sample.txt";
string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Console.WriteLine("Part 1.");

var directories = day07.GetDirectories(lines);
// directories.ForEach(d => { Console.WriteLine(d.Name); });
// Console.WriteLine();

day07.PopulateDirectories(lines, directories);
// Console.WriteLine();
directories.ForEach(Console.WriteLine);

day07.PopulateFiles(lines, directories);
// directories.ForEach(Console.WriteLine);
// Console.WriteLine();
directories.ForEach(d => Console.WriteLine(d.Name));

var totals = day07.GetTotals(directories);
// totals.Select(i => $"{i.Key}: {i.Value}").ToList().ForEach(Console.WriteLine);

// var total = totals.Values.OrderByDescending(x => x).ToList().Where(y => y <= 100000).Sum();
// Console.WriteLine($"Total: {total}");

// Part 2
// Console.WriteLine("Part 2.");

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();