using Xunit.Abstractions;

namespace OpenCvSharp5.Tests;

public class UnitTest1
{
    private readonly ITestOutputHelper testOutputHelper;

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test1()
    {
        testOutputHelper.WriteLine("cv::getTickCount() = {0}", NativeMethods.core_getTickCount());
    }
}