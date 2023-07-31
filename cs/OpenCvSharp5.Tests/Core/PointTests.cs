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

    [Fact]
    public void VecConversion()
    {
        var p = new Point(1, 2);
        var v = new Vec2<int>(1, 2);

        Assert.Equal(v, p.ToVec2i());
        Assert.Equal(v, (Vec2<int>)p);

        Assert.Equal(p, Point.FromVec2i(v));
        Assert.Equal(p, (Point)v);
    }

    [Fact]
    public void ValueTupleConversion()
    {
        var p = new Point(1, 2);
        var tuple = (1, 2);

        Assert.Equal(tuple, p);

        Assert.Equal(p, Point.FromValueTuple(tuple));
        Assert.Equal(p, (Point)tuple);
    }

    [Fact]
    public void Distance()
    {
        var p1 = new Point(1, 2);
        var p2 = new Point(1, 4);

        Assert.Equal(2, Point.Distance(p1, p2));
        Assert.Equal(2, p1.DistanceTo(p2));
    }

    [Fact]
    public void DotProduct()
    {
        var p1 = new Point(1, 2);
        var p2 = new Point(1, 4);

        Assert.Equal(9, Point.DotProduct(p1, p2));
        Assert.Equal(9, p1.DotProduct(p2));
    }

    [Fact]
    public void CrossProduct()
    {
        var p1 = new Point(1, 2);
        var p2 = new Point(1, 4);

        Assert.Equal(2, Point.CrossProduct(p1, p2));
        Assert.Equal(2, p1.CrossProduct(p2));
    }
}
