
// ReSharper disable ArrangeMethodOrOperatorBody

namespace OpenCvSharp5.Tests.Core;

public class SizeTests
{
    [Fact]
    public void Size()
    {
        var s = new Size(1, 2);
        Assert.Equal(1, s.Width);
        Assert.Equal(2, s.Height);

        var (w, h) = s;
        Assert.Equal(1, w);
        Assert.Equal(2, h);

        Assert.Equal("Size { Width = 1, Height = 2 }", s.ToString());

        Assert.True(new Size(1, 2) == new Size(1, 2));
        Assert.False(new Size(1, 2) == new Size(2, 3));
    }

    [Fact]
    public void Size2f()
    {
        var s = new Size2f(1.23f, 4.56f);
        Assert.Equal(1.23f, s.Width);
        Assert.Equal(4.56f, s.Height);

        var (w, h) = s;
        Assert.Equal(1.23f, w);
        Assert.Equal(4.56f, h);

        Assert.Equal("Size2f { Width = 1.23, Height = 4.56 }", s.ToString());

        Assert.True(new Size2f(1, 2) == new Size2f(1, 2));
        Assert.False(new Size2f(1, 2) == new Size2f(2, 3));
    }

    [Fact]
    public void Size2d()
    {
        var s = new Size2d(1.23, 4.56);
        Assert.Equal(1.23, s.Width);
        Assert.Equal(4.56, s.Height);

        var (w, h) = s;
        Assert.Equal(1.23, w);
        Assert.Equal(4.56, h);

        Assert.Equal("Size2d { Width = 1.23, Height = 4.56 }", s.ToString());

        Assert.True(new Size2d(1, 2) == new Size2d(1, 2));
        Assert.False(new Size2d(1, 2) == new Size2d(2, 3));
    }
}
