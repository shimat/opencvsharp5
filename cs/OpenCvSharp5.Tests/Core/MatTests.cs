namespace OpenCvSharp5.Tests.Core;

public class MatTests
{
    [Fact]
    public void NewAndDispose()
    {
        using var mat = new Mat();
    }

    [Fact]
    public void RowsCols()
    {
        using var mat = new Mat(3, 4, MatType.CV_8UC1);
        Assert.Equal(3, mat.Rows);
        Assert.Equal(4, mat.Cols);
    }
    
    [Fact]
    public void RowRange()
    {
        using var mat = new Mat(3, 4, MatType.CV_8UC1);
        using var matRange1 = mat.RowRange(0, 1);
        Assert.Equal(1, matRange1.Rows);
        Assert.Equal(4, matRange1.Cols);
        using var matRange2 = mat.RowRange(new Range(1, 3));
        Assert.Equal(2, matRange2.Rows);
        Assert.Equal(4, matRange2.Cols);
        using var matRange3 = mat.RowRange(Range.All);
        Assert.Equal(3, matRange3.Rows);
        Assert.Equal(4, matRange3.Cols);
    }

    [Fact]
    public void Data()
    {
        using var mat = new Mat(3, 4, MatType.CV_8UC1);
        Assert.NotEqual(IntPtr.Zero, mat.Data);
    }

    [Fact]
    public void Size()
    {
        using var mat = new Mat(3, 4, MatType.CV_8UC1);
        Assert.Equal(new Size(4, 3), mat.Size());
        Assert.Equal(3, mat.Size(0));
        Assert.Equal(4, mat.Size(1));
    }

    [Fact]
    public void Step()
    {
        using var mat = new Mat(3, 7, MatType.CV_8UC1);
        Assert.Equal(7, mat.Step());
        Assert.Equal(7, mat.Step(0));
    }
}
