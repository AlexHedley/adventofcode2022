#load "nuget:ScriptUnit, 0.1.0"
#r "nuget:FluentAssertions, 4.19.4"

#load "../utils/utils.csx"
#load "solution.csx"

using static ScriptUnit;
using FluentAssertions;

return await AddTestsFrom<Day02Tests>().Execute();

public class Day02Tests : IDisposable
{
    public Day02 day02;

    public Day02Tests()
    {
        day02 = new Day02();
    }

    public void Dispose() { }

    public void CalculateRound()
    {

    }

    [Arguments("A", "R")]
    [Arguments("X", "R")]
    [Arguments("B", "P")]
    [Arguments("Y", "P")]
    [Arguments("C", "S")]
    [Arguments("Z", "S")]
    public void GetAction(string letter, string actionLetter)
    {
        var action = day02.GetAction(letter);

        action.Letter.Should().Be(actionLetter);
    }

    // public void CalculatePoints()
    // {
    //     Action a = new Action() { Letter = "A", Score = 1 };
    //     Action b = new Action() { Letter = "B", Score = 2 };
    //     Action c = new Action() { Letter = "C", Score = 1 };
    //     a.Beats = c;
    //     b.Beats = a;
    //     c.Beats = b;

    //     var points = day02.CalculatePoints(a, b);

    //     points.Should().Be(5, "");
    // }

    public void CalculateRoundPart2()
    {

    }

    public void CalculatePointsPart2()
    {

    }
}