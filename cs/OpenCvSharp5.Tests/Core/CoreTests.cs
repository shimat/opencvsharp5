// ReSharper disable ArrangeMethodOrOperatorBody

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
        testOutputHelper.WriteLine("cv::getTickCount() = {0}", Cv2.GetTickCount());
    }

    [Fact]
    public void GetBuildInformation()
    {
        var result = Cv2.GetBuildInformation();
        Assert.NotEmpty(result);
        testOutputHelper.WriteLine(result);
    }

    [Fact]
    public void GetVersionString()
    {
        var result = Cv2.GetVersionString();
        Assert.NotEmpty(result);
        Assert.Matches(@"^\d", result);
        testOutputHelper.WriteLine($"OpenCV Version = {result}");
    }
}
