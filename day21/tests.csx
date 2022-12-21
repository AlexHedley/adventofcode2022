#load "nuget:ScriptUnit, 0.1.0"
#r "nuget:FluentAssertions, 4.19.4"

#load "../utils/utils.csx"
#load "solution.csx"

using static ScriptUnit;
using FluentAssertions;

return await AddTestsFrom<DayTests21>().Execute();

public class DayTests21 : IDisposable
{
    public Day21 day21;

    public DayTests21()
    {
        day21 = new Day21();
    }

    public void Dispose() { }

    // public void Success()
    // {
    //     "Ok".Should().Be("Ok");
    // }

    // public void Fail()
    // {
    //     "Ok".Should().NotBe("Ok");
    // }
}