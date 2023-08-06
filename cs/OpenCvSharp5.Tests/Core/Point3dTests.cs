// ReSharper disable ArrangeMethodOrOperatorBody

namespace OpenCvSharp5.Tests.Core;

public class Point3dTests
{
    [Fact]
    public void Constructor()
    {
        var p = new Point3d(1, 2, 3);
        Assert.Equal(1, p.X);
        Assert.Equal(2, p.Y);
    }

    [Fact]
    public void PlusMinus()
    {
        var p = new Point3d(1, -2, 3);

        Assert.Equal(p, +p);
        Assert.Equal(p, p.Plus());
        Assert.Equal(new Point3d(-1, 2, -3), -p);
        Assert.Equal(new Point3d(-1, 2, -3), p.Negate());
    }

    [Fact]
    public void Arithmetic()
    {
        var p = new Point3d(10, 20, 30);
        
        Assert.Equal(new Point3d(12, 23, 34), p + new Point3d(2, 3, 4));
        Assert.Equal(new Point3d(12, 23, 34), p.Add(new Point3d(2, 3, 4)));
        
        Assert.Equal(new Point3d(8, 17, 26), p - new Point3d(2, 3, 4));
        Assert.Equal(new Point3d(8, 17, 26), p.Subtract(new Point3d(2, 3, 4)));

        Assert.Equal(new Point3d(30, 60, 90), p * 3);
        Assert.Equal(new Point3d(30, 60, 90), p.Multiply(3));
    }

    [Fact]
    public void VecConversion()
    {
        var p = new Point3d(1, 2, 3);
        var v = new Vec3<double>(1, 2, 3);

        Assert.Equal(v, p.ToVec3d());
        Assert.Equal(v, (Vec3<double>)p);

        Assert.Equal(p, Point3d.FromVec3d(v));
        Assert.Equal(p, (Point3d)v);
    }

    [Fact]
    public void ValueTupleConversion()
    {
        var p = new Point3d(1, 2, 3);
        var tuple = (1d, 2d, 3d);

        Assert.Equal(tuple, p);

        Assert.Equal(p, Point3d.FromValueTuple(tuple));
        Assert.Equal(p, (Point3d)tuple);
    }
}
