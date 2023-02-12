using Xunit.Abstractions;

namespace OpenCvSharp5.Tests.Core;

public class CoreTests
{
    private readonly ITestOutputHelper testOutputHelper;

    public CoreTests(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void GetTickCount()
    {
        testOutputHelper.WriteLine("cv::getTickCount() = {0}", NativeMethods.core_getTickCount());
    }

    [Fact]
    public void GetBuildInformation()
    {
        using var stdString = new StdString();
        NativeMethods.core_getBuildInformation(stdString);
        testOutputHelper.WriteLine(stdString.ToString());
    }
}
