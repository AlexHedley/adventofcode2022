#load "nuget:ScriptUnit, 0.1.0"
#r "nuget:FluentAssertions, 4.19.4"

using static ScriptUnit;   
using FluentAssertions;

return await AddTestsFrom<SampleTests>().Execute();

public class SampleTests : IDisposable
{
    public SampleTests()
    {
    	//Do init here..  
    }

    public void Dispose()
    {
        //Do "tear down" here--
    }

    public void Success()
    {
        "Ok".Should().Be("Ok");
    }

    public void Fail()
    {
        "Ok".Should().NotBe("Ok");
    }
}