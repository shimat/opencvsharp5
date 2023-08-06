
// ReSharper disable ArrangeMethodOrOperatorBody

namespace OpenCvSharp5.Tests.Core;

public class Point2dTests
{
    [Fact]
    public void Constructor()
    {
        var p = new Point2d(1, 2);
        Assert.Equal(1, p.X);
        Assert.Equal(2, p.Y);
    }

    [Fact]
    public void PlusMinus()
    {
        var p = new Point2d(1, -2);

        Assert.Equal(p, +p);
        Assert.Equal(p, p.Plus());
        Assert.Equal(new Point2d(-1, 2), -p);
        Assert.Equal(new Point2d(-1, 2), p.Negate());
    }

    [Fact]
    public void Arithmetic()
    {
        var p = new Point2d(10, 20);
        
        Assert.Equal(new Point2d(12, 23), p + new Point2d(2, 3));
        Assert.Equal(new Point2d(12, 23), p.Add(new Point2d(2, 3)));
        
        Assert.Equal(new Point2d(8, 17), p - new Point2d(2, 3));
        Assert.Equal(new Point2d(8, 17), p.Subtract(new Point2d(2, 3)));

        Assert.Equal(new Point2d(30, 60), p * 3);
        Assert.Equal(new Point2d(30, 60), p.Multiply(3));
    }

    [Fact]
    public void VecConversion()
    {
        var p = new Point2d(1, 2);
        var v = new Vec2<double>(1, 2);

        Assert.Equal(v, p.ToVec2d());
        Assert.Equal(v, (Vec2<double>)p);

        Assert.Equal(p, Point2d.FromVec2d(v));
        Assert.Equal(p, (Point2d)v);
    }

    [Fact]
    public void PointConversion()
    {
        var pd = new Point2d(1, 2);
        var pi = new Point(1, 2);

        Assert.Equal(pi, pd.ToPoint());
        Assert.Equal(pi, (Point)pd);

        Assert.Equal(pd, Point2d.FromPoint(pi));
        Assert.Equal(pd, (Point2d)pi);
    }

    [Fact]
    public void ValueTupleConversion()
    {
        var p = new Point2d(1, 2);
        var tuple = (1d, 2d);

        Assert.Equal(tuple, p);

        Assert.Equal(p, Point2d.FromValueTuple(tuple));
        Assert.Equal(p, (Point2d)tuple);
    }

    [Fact]
    public void Distance()
    {
        var p1 = new Point2d(1, 2);
        var p2 = new Point2d(1, 4);

        Assert.Equal(2, Point2d.Distance(p1, p2));
        Assert.Equal(2, p1.DistanceTo(p2));
    }

    [Fact]
    public void DotProduct()
    {
        var p1 = new Point2d(1, 2);
        var p2 = new Point2d(1, 4);

        Assert.Equal(9, Point2d.DotProduct(p1, p2));
        Assert.Equal(9, p1.DotProduct(p2));
    }

    [Fact]
    public void CrossProduct()
    {
        var p1 = new Point2d(1, 2);
        var p2 = new Point2d(1, 4);

        Assert.Equal(2, Point2d.CrossProduct(p1, p2));
        Assert.Equal(2, p1.CrossProduct(p2));
    }
}
