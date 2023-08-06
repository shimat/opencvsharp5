// ReSharper disable ArrangeMethodOrOperatorBody

namespace OpenCvSharp5.Tests.Core;

public class Point3fTests
{
    [Fact]
    public void Constructor()
    {
        var p = new Point3f(1, 2, 3);
        Assert.Equal(1, p.X);
        Assert.Equal(2, p.Y);
    }

    [Fact]
    public void PlusMinus()
    {
        var p = new Point3f(1, -2, 3);

        Assert.Equal(p, +p);
        Assert.Equal(p, p.Plus());
        Assert.Equal(new Point3f(-1, 2, -3), -p);
        Assert.Equal(new Point3f(-1, 2, -3), p.Negate());
    }

    [Fact]
    public void Arithmetic()
    {
        var p = new Point3f(10, 20, 30);
        
        Assert.Equal(new Point3f(12, 23, 34), p + new Point3f(2, 3, 4));
        Assert.Equal(new Point3f(12, 23, 34), p.Add(new Point3f(2, 3, 4)));
        
        Assert.Equal(new Point3f(8, 17, 26), p - new Point3f(2, 3, 4));
        Assert.Equal(new Point3f(8, 17, 26), p.Subtract(new Point3f(2, 3, 4)));

        Assert.Equal(new Point3f(30, 60, 90), p * 3);
        Assert.Equal(new Point3f(30, 60, 90), p.Multiply(3));
    }

    [Fact]
    public void VecConversion()
    {
        var p = new Point3f(1, 2, 3);
        var v = new Vec3<float>(1, 2, 3);

        Assert.Equal(v, p.ToVec3f());
        Assert.Equal(v, (Vec3<float>)p);

        Assert.Equal(p, Point3f.FromVec3f(v));
        Assert.Equal(p, (Point3f)v);
    }

    [Fact]
    public void ValueTupleConversion()
    {
        var p = new Point3f(1, 2, 3);
        var tuple = (1f, 2f, 3f);

        Assert.Equal(tuple, p);

        Assert.Equal(p, Point3f.FromValueTuple(tuple));
        Assert.Equal(p, (Point3f)tuple);
    }
}
