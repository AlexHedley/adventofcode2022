#load "nuget:ScriptUnit, 0.1.0"
#r "nuget:FluentAssertions, 4.19.4"

#load "../utils/utils.csx"
#load "solution.csx"

using static ScriptUnit;
using FluentAssertions;

return await AddTestsFrom<DayTests09>().Execute();

public class DayTests09 : IDisposable
{
    public Day09 day;

    public DayTests09()
    {
        day09 = new Day09();
    }

    public void Dispose() { }

    // public void SetStartingPoint()
    // {
    //     var lines = new string[] { "...", "...", "..." };
    //     var matrix = Utils.GenerateMatrix<string>(lines, 3, 3);
    //     day09.SetStartingPoint(matrix, 2, 0);
    //     Utils.PrintMatrix(matrix);
    // }

    // public void Fail()
    // {
    //     "Ok".Should().NotBe("Ok");
    // }
}