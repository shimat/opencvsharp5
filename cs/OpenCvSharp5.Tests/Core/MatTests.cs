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
        
        //Assert.Equal(mat.Rows, mat.UnsafeRows);
        //Assert.Equal(mat.Cols, mat.UnsafeCols);
    }
    
    [Fact]
    public void Data()
    {
        using var mat = new Mat(3, 4, MatType.CV_8UC1);
        Assert.NotEqual(IntPtr.Zero, mat.Data);
    }
}
