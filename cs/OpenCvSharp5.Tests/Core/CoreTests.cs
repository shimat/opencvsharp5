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
        using var stdString = StdString.Create();
        //bool success = false;
        //stdString.DangerousAddRef(ref success);
        //Assert.True(success);
        var handle = stdString.Handle;
        NativeMethods.core_getBuildInformation(handle);
        //testOutputHelper.WriteLine(stdString.ToString());
    }

    [Fact]
    public void GetBuildInformation2()
    {
        var length = NativeMethods.core_getBuildInformation_pure();
        testOutputHelper.WriteLine("cv::getBuildInformation().size() = {0}", length);
    }

    [Fact]
    public void GetBuildInformation3()
    {
        var ptr = NativeMethods.std_string_new1_();
        NativeMethods.core_getBuildInformation(ptr);
        NativeMethods.std_string_delete(ptr);
    }
}
