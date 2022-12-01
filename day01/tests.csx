#load "nuget:ScriptUnit, 0.1.0"
#r "nuget:FluentAssertions, 4.19.4"

using static ScriptUnit;   
using FluentAssertions;

return await AddTestsFrom<Day01Tests>().Execute();

public class Day01Tests : IDisposable
{
    public Day01Tests()
    {
    	//Do init here..  
    }

    public void Dispose()
    {
        //Do "tear down" here--
    }
}