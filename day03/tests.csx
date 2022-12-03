#load "nuget:ScriptUnit, 0.1.0"
#r "nuget:FluentAssertions, 4.19.4"

#load "../utils/utils.csx"
#load "solution.csx"

using static ScriptUnit;
using FluentAssertions;

return await AddTestsFrom<Day03Tests>().Execute();

public class Day03Tests : IDisposable
{
    public Day03 day03;

    public Day03Tests()
    {
        day03 = new Day03();
    }

    public void Dispose() { }

    public void Split()
    {
        var line = "1234";
        var splitLine = day03.Split(line, line.Length / 2).ToList();

        splitLine.Should().HaveCount(2, "");
        splitLine.Should().ContainItemsAssignableTo<string>();
        splitLine.Should().Equal(new List<string> { "12", "34" }, "");
    }

    public void Matching()
    {
        var splitLine = new List<String>() { "12", "23" };
        var resultSet = day03.Matching(splitLine);

        resultSet.Should().HaveCount(1, "");
        resultSet.Should().ContainItemsAssignableTo<string>();
        resultSet.Should().Equal(new List<string>() { "2" }, "");
    }
}