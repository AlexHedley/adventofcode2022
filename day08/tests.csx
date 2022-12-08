#load "nuget:ScriptUnit, 0.1.0"
#r "nuget:FluentAssertions, 4.19.4"

#load "../utils/utils.csx"
#load "solution.csx"

using static ScriptUnit;
using FluentAssertions;

return await AddTestsFrom<DayTests08>().Execute();

public class DayTests08 : IDisposable
{
    public Day08 day08;

    public DayTests08()
    {
        day08 = new Day08();
    }

    public void Dispose() { }

    // public void GetInnerMatrix()
    // {
    //     var lines = new string[] { "30373", "25512", "65332", "33549", "35390" };
    //     var matrix = Utils.GenerateMatrix(lines, 5, 5);
    // }

    public void CountOfMatrixEdge()
    {
        var lines = new string[] { "123", "456", "789" };
        var matrix = Utils.GenerateMatrix(lines, 3, 3);
        var count = day08.CountOfMatrixEdge(matrix);

        count.Should().Be(8);
    }

    public void CheckLeft()
    {
        int[] row = new int[] { 1, 2, 3 };
        var check = day08.CheckLeft(row, 1);
        check.Should().Be(true);
    }

    public void CheckRight()
    {
        int[] row = new int[] { 1, 2, 3 };
        var check = day08.CheckRight(row, 1);
        check.Should().Be(false);
    }

    public void CheckTop()
    {
        int[] col = new int[] { 1, 2, 3 };
        var check = day08.CheckTop(col, 1);
        check.Should().Be(true);
    }

    public void CheckBottom()
    {
        int[] col = new int[] { 1, 2, 3 };
        var check = day08.CheckBottom(col, 1);
        check.Should().Be(false);
    }

    public void CheckDirections()
    {
        var lines = new string[] { "123", "456", "789" };
        // 123
        // 456
        // 789
        var matrix = Utils.GenerateMatrix(lines, 3, 3);
        var free = day08.CheckDirections(matrix, 1, 1);
        free.Should().BeTrue();
    }
}