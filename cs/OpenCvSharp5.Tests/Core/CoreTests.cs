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
        testOutputHelper.WriteLine(Cv2.GetBuildInformation());
    }
}
