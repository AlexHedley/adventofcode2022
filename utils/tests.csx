#load "nuget:ScriptUnit, 0.1.0"
#r "nuget:FluentAssertions, 4.19.4"

#load "utils.csx"

using static ScriptUnit;   
using FluentAssertions;

return await AddTestsFrom<UtilTests>().Execute();

public class UtilTests : IDisposable
{
    public UtilTests()
    {
    	//Do init here..
    }

    public void Dispose()
    {
        //Do "tear down" here--
    }

    public void GetLines()
    {
        string fileName = @"input-sample.txt";
        var lines = Utils.GetLines(fileName);
        string[] expectedLines = { "1000", "2000", "3000", "", "4000", "", "5000", "6000", "", "7000", "8000", "9000", "", "10000" };
        
        lines.Should().HaveCount(14, "beacuse there are 14 rows");
        lines.Should().Equal(expectedLines);
    }

}