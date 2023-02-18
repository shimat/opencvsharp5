namespace OpenCvSharp5.Tests.Core;

public class MatTests
{
    private readonly ITestOutputHelper testOutputHelper;

    public MatTests(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void NewAndDispose()
    {
        using var mat = new Mat();
    }

    [Fact]
    public void RowsCols()
    {
        using var mat = new Mat(3, 4, 0);
        Assert.Equal(3, mat.Rows);
        Assert.Equal(4, mat.Cols);
    }

    [Fact]
    public void SafeRowsCols()
    {
        using var mat = new Mat(3, 4, 0);
        Assert.Equal(3, mat.SafeRows);
        Assert.Equal(4, mat.SafeCols);
    }
}
