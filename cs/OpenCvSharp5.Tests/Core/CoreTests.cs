// ReSharper disable ArrangeMethodOrOperatorBody

using System.Runtime.InteropServices;

namespace OpenCvSharp5.Tests.Core;

public class CoreTests
{
    private readonly ITestOutputHelper testOutputHelper;

    public CoreTests(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void GetTickCount() => testOutputHelper.WriteLine("cv::getTickCount() = {0}", Cv2.GetTickCount());

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
        Assert.Matches(@"^5\.", result);
        testOutputHelper.WriteLine($"OpenCV Version = {result}");
    }

    [Fact]
    public void Split()
    {
        using var src = new Mat(1, 1, MatType.CV_8UC3, new Scalar(1, 2, 3));

        Cv2.Split(src, out var dst);
        Assert.Equal(3, dst.Length);

        using var dst0 = dst[0];
        using var dst1 = dst[1];
        using var dst2 = dst[2];
        Assert.All(dst, m =>
        {
            Assert.Equal(MatType.CV_8UC1, m.Type());
            Assert.Equal(src.Size(), m.Size());
        });

        Assert.Equal(1, Marshal.ReadByte(dst0.Data));
        Assert.Equal(2, Marshal.ReadByte(dst1.Data));
        Assert.Equal(3, Marshal.ReadByte(dst2.Data));
    }
}
