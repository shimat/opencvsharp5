// ReSharper disable ArrangeMethodOrOperatorBody

namespace OpenCvSharp5.Tests.Core;

public class Point3iTests
{
    [Fact]
    public void Constructor()
    {
        var p = new Point3i(1, 2, 3);
        Assert.Equal(1, p.X);
        Assert.Equal(2, p.Y);
    }

    [Fact]
    public void PlusMinus()
    {
        var p = new Point3i(1, -2, 3);

        Assert.Equal(p, +p);
        Assert.Equal(p, p.Plus());
        Assert.Equal(new Point3i(-1, 2, -3), -p);
        Assert.Equal(new Point3i(-1, 2, -3), p.Negate());
    }

    [Fact]
    public void Arithmetic()
    {
        var p = new Point3i(10, 20, 30);
        
        Assert.Equal(new Point3i(12, 23, 34), p + new Point3i(2, 3, 4));
        Assert.Equal(new Point3i(12, 23, 34), p.Add(new Point3i(2, 3, 4)));
        
        Assert.Equal(new Point3i(8, 17, 26), p - new Point3i(2, 3, 4));
        Assert.Equal(new Point3i(8, 17, 26), p.Subtract(new Point3i(2, 3, 4)));

        Assert.Equal(new Point3i(30, 60, 90), p * 3);
        Assert.Equal(new Point3i(30, 60, 90), p.Multiply(3));
    }

    [Fact]
    public void VecConversion()
    {
        var p = new Point3i(1, 2, 3);
        var v = new Vec3<int>(1, 2, 3);

        Assert.Equal(v, p.ToVec3i());
        Assert.Equal(v, (Vec3<int>)p);

        Assert.Equal(p, Point3i.FromVec3i(v));
        Assert.Equal(p, (Point3i)v);
    }

    [Fact]
    public void ValueTupleConversion()
    {
        var p = new Point3i(1, 2, 3);
        var tuple = (1, 2, 3);

        Assert.Equal(tuple, p);

        Assert.Equal(p, Point3i.FromValueTuple(tuple));
        Assert.Equal(p, (Point3i)tuple);
    }
}
