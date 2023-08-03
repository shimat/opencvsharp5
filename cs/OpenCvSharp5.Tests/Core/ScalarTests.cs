namespace OpenCvSharp5.Tests.Core;

public class ScalarTests
{
    [Fact]
    public void Equal()
    {
        var s1 = new Scalar(1, 2, 3, 4);
        var s2 = new Scalar(1, 2, 3, 4);
        Assert.Equal(s1, s2);
    }
    
    [Fact]
    public void NewBy1Value()
    {
        var s = new Scalar(1);
        Assert.Equal(1, s.Val0);
        Assert.Equal(0, s.Val1);
        Assert.Equal(0, s.Val2);
        Assert.Equal(0, s.Val3);
        Assert.True(s.IsReal());
    }

    [Fact]
    public void NewBy2Values()
    {
        var s = new Scalar(1, 2);
        Assert.Equal(1, s.Val0);
        Assert.Equal(2, s.Val1);
        Assert.Equal(0, s.Val2);
        Assert.Equal(0, s.Val3);
        Assert.False(s.IsReal());
    }

    [Fact]
    public void NewBy3Values()
    {
        var s = new Scalar(1, 2, 3);
        Assert.Equal(1, s.Val0);
        Assert.Equal(2, s.Val1);
        Assert.Equal(3, s.Val2);
        Assert.Equal(0, s.Val3);
        Assert.False(s.IsReal());
    }

    [Fact]
    public void NewBy4Values()
    {
        var s = new Scalar(1, 2, 3, 4);
        Assert.Equal(1, s.Val0);
        Assert.Equal(2, s.Val1);
        Assert.Equal(3, s.Val2);
        Assert.Equal(4, s.Val3);
        Assert.False(s.IsReal());
    }

    [Fact]
    public void All()
    {
        var s = Scalar.All(1);
        Assert.Equal(1, s.Val0);
        Assert.Equal(1, s.Val1);
        Assert.Equal(1, s.Val2);
        Assert.Equal(1, s.Val3);
        Assert.False(s.IsReal());
    }
}
