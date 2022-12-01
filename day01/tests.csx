#load "nuget:ScriptUnit, 0.1.0"
#r "nuget:FluentAssertions, 4.19.4"

#load "solution.csx"

using static ScriptUnit;   
using FluentAssertions;

return await AddTestsFrom<Day01Tests>().Execute();

public class Day01Tests : IDisposable
{
    public Day01 day01;

    public Day01Tests()
    {
    	//Do init here..
        day01 = new Day01();
    }

    public void Dispose()
    {
        //Do "tear down" here--
    }

    public void GetLines()
    {
        string fileName = @"input-sample.txt";
        var lines = day01.GetLines(fileName);
        string[] expectedLines = { "1000", "2000", "3000", "", "4000", "", "5000", "6000", "", "7000", "8000", "9000", "", "10000" };
        
        lines.Should().HaveCount(14, "beacuse there are 14 rows");
        lines.Should().Equal(expectedLines);
    }

    public void GetElves() {
        string[] lines = { "1000", "2000", "", "5000" };
        var elves = day01.GetElves(lines);
        
        elves.Should().HaveCount(2, "");
    }

    public void GetMaxValue() {
        Dictionary<int, long> elves = new Dictionary<int, long>() { { 1, 2000 }, { 2, 3000 } };
        var max = day01.GetMaxValue(elves);

        max.Should().Be(3000);
    }

    public void GetMaxKey() {
        Dictionary<int, long> elves = new Dictionary<int, long>() { { 1, 4000 }, { 2, 2000 } };
        var max = day01.GetMaxKey(elves);

        max.Should().Be(1);
    }
    
    public void GetTotal() {
        Dictionary<int, long> elves = new Dictionary<int, long>() { { 1, 4000 }, { 2, 2000 }, { 3, 1000 }, { 4, 5000 } };
        var total = day01.GetTotal(elves);

        total.Should().Be(11000);
    }
}