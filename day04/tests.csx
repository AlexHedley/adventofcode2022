#load "nuget:ScriptUnit, 0.1.0"
#r "nuget:FluentAssertions, 4.19.4"

#load "solution.csx"

using static ScriptUnit;
using FluentAssertions;

return await AddTestsFrom<Day04Tests>().Execute();

public class Day04Tests : IDisposable
{
    public Day04 day04;

    public Day04Tests()
    {
        day04 = new Day04();
    }

    public void Dispose() { }

    public void BoundsRange()
    {
        var x = new Bounds(1, 3);
        var range = day04.BoundsRange(x);

        range.Should().HaveCount(3, "");
        range.Should().Equal(new List<int> { 1, 2, 3 });
    }

    // [Arguments("2-4,6-8", ",", ("2-4", "6-8"))]
    // [Arguments("2-4", "-", ("2", "4"))]
    // public void Split(string line, string separator, (string one, string two) result)
    // {
    //     // var line = "2-4,6-8";
    //     // var separator = ",";
    //     // var result = ("2-4", "6-8");
    //     var split = day04.Split(line, separator);

    //     split.Should().Be(result);
    // }

    public void Split1()
    {
        var line = "2-4,6-8";
        var separator = ",";
        var result = ("2-4", "6-8");
        var split = day04.Split(line, separator);

        split.Should().Be(result);
    }

    public void Split2()
    {
        var line = "2-4";
        var separator = "-";
        var result = ("2", "4");
        var split = day04.Split(line, separator);

        split.Should().Be(result);
    }

    public void PrintRange()
    {
        var range = new List<int>() { 2, 3, 4 };
        var printOut = day04.PrintRange(range);
        var result = ".234.....  2-4";

        printOut.Should().Be(result);
    }

    // [Arguments(new List<int>() { 2, 3, 4 }, new List<int>() { 6, 7, 8 }, false)]
    // public void Overlap(List<int> one, List<int> two, bool result)
    // {
    //     var overlap = day04.Overlap(one, two);

    //     overlap.Should().Be(result);
    // }

    public void Overlap_False()
    {
        var one = new List<int>() { 2, 3, 4 };
        var two = new List<int>() { 6, 7, 8 };
        var result = false;
        var overlap = day04.Overlap(one, two);

        overlap.Should().Be(result);
    }

    public void Overlap_True()
    {
        var one = new List<int>() { 6 };
        var two = new List<int>() { 4, 5, 6 };
        var result = true;
        var overlap = day04.Overlap(one, two);

        overlap.Should().Be(result);
    }

    public void Overlap2_False()
    {
        var one = new List<int>() { 2, 3, 4 };
        var two = new List<int>() { 6, 7, 8 };
        var result = false;
        var overlap = day04.OverlapPart2(one, two);

        overlap.Should().Be(result);
    }

    public void Overlap2_True()
    {
        var one = new List<int>() { 5, 6, 7 };
        var two = new List<int>() { 7, 8, 9 };
        var result = true;
        var overlap = day04.OverlapPart2(one, two);

        overlap.Should().Be(result);
    }
}