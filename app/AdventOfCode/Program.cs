using AdventOfCode.Days;

namespace AdventOfCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent Of Code - 2022");

            //string fileName = @"Data\input-sample.txt";
            string fileName = @"Data\input.txt";
            var lines = Utils.GetLines(fileName);

            //foreach(var line in lines)
            //{
            //    Console.WriteLine(line);
            //}

            Day07 day07 = new Day07();

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
            // directories.ForEach(d => Console.WriteLine(d.Name));

            // day07.GetInitialTotals(directories);


            var totals = new Dictionary<string, long>();
            try
            {
                totals = day07.GetTotals(directories);
                //totals.Select(i => $"{i.Key}: {i.Value}").ToList().ForEach(Console.WriteLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //throw;
            }
            
            var total = totals.Values.OrderByDescending(x => x).ToList().Where(y => y <= 100000).Sum();
            Console.WriteLine($"Total: {total}");

            // Part 2
            // Console.WriteLine("Part 2.");

            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }
    }
}