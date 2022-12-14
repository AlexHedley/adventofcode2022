#load "nuget:ScriptUnit, 0.1.0"
#r "nuget:FluentAssertions, 4.19.4"

#load "../utils/utils.csx"
#load "solution.csx"

using static ScriptUnit;
using FluentAssertions;

return await AddTestsFrom<DayTests06>().Execute();

public class DayTests06 : IDisposable
{
    public Day06 day06;

    public DayTests06()
    {
        day06 = new Day06();
    }

    public void Dispose() {}

    [Arguments("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7)]
    [Arguments("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
    [Arguments("nppdvjthqldpwncqszvftbrmjlhg", 6)]
    [Arguments("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
    [Arguments("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
    public void ParseInput(string input, int position)
    {
        var take = 4;
        var expectedPosition = day06.ParseInput(input, take);
        expectedPosition.Should().Be(position);
    }

    [Arguments("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19)]
    [Arguments("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
    [Arguments("nppdvjthqldpwncqszvftbrmjlhg", 23)]
    [Arguments("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
    [Arguments("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26)]
    public void ParseInput2(string input, int position)
    {
        var take = 14;
        var expectedPosition = day06.ParseInput(input, take);
        expectedPosition.Should().Be(position);
    }

    // public void ParseInputs()
    // {
    //     "Ok".Should().NotBe("Ok");
    // }

    [Arguments("mjqj", false)]
    [Arguments("jqjp", false)]
    [Arguments("qjpq", false)]
    [Arguments("jpqm", true)]
    public void UniqueCharacters(string input, bool result)
    {
        var isUnique = day06.UniqueCharacters(input);
        isUnique.Should().Be(result);
    }
}