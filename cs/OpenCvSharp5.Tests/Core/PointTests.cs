
// ReSharper disable ArrangeMethodOrOperatorBody

namespace OpenCvSharp5.Tests.Core;

public class PointTests
{
    [Fact]
    public void Constructor()
    {
        var p = new Point(1, 2);
        Assert.Equal(1, p.X);
        Assert.Equal(2, p.Y);
    }

    [Fact]
    public void PlusMinus()
    {
        var p = new Point(1, -2);

        Assert.Equal(p, +p);
        Assert.Equal(p, p.Plus());
        Assert.Equal(new Point(-1, 2), -p);
        Assert.Equal(new Point(-1, 2), p.Negate());
    }

    [Fact]
    public void Arithmetic()
    {
        var p = new Point(10, 20);
        
        Assert.Equal(new Point(12, 23), p + new Point(2, 3));
        Assert.Equal(new Point(12, 23), p.Add(new Point(2, 3)));
        
        Assert.Equal(new Point(8, 17), p - new Point(2, 3));
        Assert.Equal(new Point(8, 17), p.Subtract(new Point(2, 3)));

        Assert.Equal(new Point(30, 60), p * 3);
        Assert.Equal(new Point(30, 60), p.Multiply(3));
    }
}
