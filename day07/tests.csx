#load "nuget:ScriptUnit, 0.1.0"
#r "nuget:FluentAssertions, 4.19.4"

#load "../utils/utils.csx"
#load "solution.csx"

using static ScriptUnit;
using FluentAssertions;

return await AddTestsFrom<DayTests07>().Execute();

public class DayTests07 : IDisposable
{
    public Day07 day;

    public DayTests07()
    {
        day = new Day07();
    }

    public void Dispose() { }

}