namespace OpenCvSharp5.Tests.Core;

public class DMatchTests
{
    [Fact]
    public void New1()
    {
        var d = new DMatch(1, 2, 3, 4);
        Assert.Equal(1, d.QueryIdx);
        Assert.Equal(2, d.TrainIdx);
        Assert.Equal(3, d.ImgIdx);
        Assert.Equal(4, d.Distance);
    }

    [Fact]
    public void New2()
    {
        var d = new DMatch(1, 2, 3);
        Assert.Equal(1, d.QueryIdx);
        Assert.Equal(2, d.TrainIdx);
        Assert.Equal(-1, d.ImgIdx);
        Assert.Equal(3, d.Distance);
    }

    [Fact]
    public void Empty()
    {
        var d = DMatch.Empty();
        Assert.Equal(-1, d.QueryIdx);
        Assert.Equal(-1, d.TrainIdx);
        Assert.Equal(-1, d.ImgIdx);
        Assert.Equal(float.MaxValue, d.Distance);
    }
    
    [Fact]
    public void ComparisonOperators()
    {
        var d1 = new DMatch(1, 2, 3, 4);
        var d2 = new DMatch(2, 4, 5, 4);
        var d3 = new DMatch(5, 6, 7, 8);

        Assert.False(d1 < d2);
        Assert.True(d1 <= d2);
        Assert.False(d1 > d2);
        Assert.True(d1 >= d2);

        Assert.True(d1 < d3);
        Assert.True(d1 <= d3);
        Assert.False(d1 > d3);
        Assert.False(d1 >= d3);

        Assert.False(d3 < d1);
        Assert.False(d3 <= d1);
        Assert.True(d3 > d1);
        Assert.True(d3 >= d1);
    }

    [Fact]
    public void CompareTo()
    {
        var d1 = new DMatch(1, 2, 3, 4);
        var d2 = new DMatch(2, 4, 5, 4);
        var d3 = new DMatch(5, 6, 7, 8);

        Assert.Equal(0, d1.CompareTo(d2));
        Assert.Equal(-1, d1.CompareTo(d3));
        Assert.Equal(+1, d3.CompareTo(d1));
    }

    [Fact]
    // ReSharper disable once InconsistentNaming
    public void ToVec4f()
    {
        var d = new DMatch(1, 2, 3, 4);
        Assert.Equal(new Vec4<float>(1, 2, 3, 4), d.ToVec4f());
        Assert.Equal(new Vec4<float>(1, 2, 3, 4), (Vec4<float>)d);
    }

    [Fact]
    // ReSharper disable once InconsistentNaming
    public void FromVec4f()
    {
        var v = new Vec4<float>(1, 2, 3, 4);
        Assert.Equal(new DMatch(1, 2, 3, 4), DMatch.FromVec4f(v));
        Assert.Equal(new DMatch(1, 2, 3, 4), (DMatch)v);
    }
}
