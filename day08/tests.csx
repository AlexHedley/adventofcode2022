#load "nuget:ScriptUnit, 0.1.0"
#r "nuget:FluentAssertions, 4.19.4"

#load "../utils/utils.csx"
#load "solution.csx"

using static ScriptUnit;
using FluentAssertions;

return await AddTestsFrom<DayTests08>().Execute();

public class DayTests08 : IDisposable
{
    public Day day;

    public DayTests08()
    {
        day = new Day();
    }

    public void Dispose() {}

    public void Success()
    {
        "Ok".Should().Be("Ok");
    }

    public void Fail()
    {
        "Ok".Should().NotBe("Ok");
    }
}