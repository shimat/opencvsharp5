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
    public void SafeRowsCols()
    {
        using var mat = new Mat(3, 4, MatType.CV_8UC1);
        Assert.Equal(3, mat.SafeRows);
        Assert.Equal(4, mat.SafeCols);
    }

    [Fact]
    public void Data()
    {
        using var mat = new Mat(3, 4, MatType.CV_8UC1);
        Assert.Equal(mat.Data, mat.SafeData);
    }
}
