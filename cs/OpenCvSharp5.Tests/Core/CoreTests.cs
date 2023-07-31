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
    public void Format()
    {
        var matData = new byte[]
        {
                    1, 2, 3,
                    4, 5, 6,
        };
        using var mat = new Mat(2, 3, MatType.CV_8UC1, matData);

        var s = Cv2.Format(mat, FormatType.Default);
        Assert.Equal("""
            [  1,   2,   3;
               4,   5,   6]
            """.Replace("\r\n", "\n"), s);
    }

    [Fact]
    public void Split1()
    {
        using var src = new Mat(1, 1, MatType.CV_8UC3, new Scalar(1, 2, 3));
        
        using var dst = Cv2.Split(src);

        Assert.Equal(3, dst.Count);
        
        Assert.All(dst, m =>
        {
            Assert.Equal(MatType.CV_8UC1, m.Type());
            Assert.Equal(src.Size(), m.Size());
        });

        Assert.Equal(1, Marshal.ReadByte(dst[0].Data));
        Assert.Equal(2, Marshal.ReadByte(dst[1].Data));
        Assert.Equal(3, Marshal.ReadByte(dst[2].Data));
    }

    [Fact]
    public void Split2()
    {
        using var src = new Mat(1, 1, MatType.CV_8UC3, new Scalar(1, 2, 3));

        // 1st 
        using var dst = new DisposableArray<Mat>(new []
        {
            new Mat(), new Mat(), new Mat(), 
        });
        Cv2.Split(src, dst);

        Assert.Equal(3, dst.Count);        
        Assert.All(dst, m =>
        {
            Assert.Equal(MatType.CV_8UC1, m.Type());
            Assert.Equal(src.Size(), m.Size());
        });

        // 2nd 
        var pointers = dst.Select(m => m.Data).ToArray();
        Cv2.Split(src, dst);
        for (var i = 0; i < dst.Count; i++)
        {
            // mat data is not reallocated
            Assert.Equal(pointers[i], dst[i].Data);
        }

        Assert.Equal(1, Marshal.ReadByte(dst[0].Data));
        Assert.Equal(2, Marshal.ReadByte(dst[1].Data));
        Assert.Equal(3, Marshal.ReadByte(dst[2].Data));
    }
}
