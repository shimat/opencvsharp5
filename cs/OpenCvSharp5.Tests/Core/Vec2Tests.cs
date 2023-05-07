namespace OpenCvSharp5.Tests.Core;

public class Vec2Tests
{
    [Fact]
    public void Byte()
    {
        var v = new Vec2<byte>(1, 2);
        Assert.Equal(1, v.Item0);
        Assert.Equal(2, v.Item1);

        var (item0, item1) = v;
        Assert.Equal(1, item0);
        Assert.Equal(2, item1);
        
        Assert.Equal(1, v[0]);
        Assert.Equal(2, v[1]);
        Assert.Equal(2, v[^1]);
        Assert.Equal(1, v[^2]);
        Assert.Throws<ArgumentOutOfRangeException>(() => v[2]);
        Assert.Throws<ArgumentOutOfRangeException>(() => v[-1]);

        Assert.Equal(new Vec2<byte>(1, 2), v);
        Assert.NotEqual(new Vec2<byte>(2, 1), v);

        Assert.Equal("Vec2 { Item0 = 1, Item1 = 2 }", v.ToString());

        Assert.Equal(new Vec2<byte>(111, 111), Vec2.All<byte>(111));

        Assert.Equal(new Vec2<byte>(4, 6), new Vec2<byte>(1, 2) + new Vec2<byte>(3, 4));
        Assert.Equal(new Vec2<byte>(4, 6), checked(new Vec2<byte>(1, 2) + new Vec2<byte>(3, 4)));
        Assert.Equal(new Vec2<byte>(9, 19), new Vec2<byte>(10, 20) + new Vec2<byte>(255, 255));
        Assert.Throws<OverflowException>(() => checked(new Vec2<byte>(10, 20) + new Vec2<byte>(255, 255)));
        Assert.Equal(new Vec2<byte>(4, 6), new Vec2<byte>(1, 2).Add(new Vec2<byte>(3, 4)));
        Assert.Equal(new Vec2<byte>(4, 6), new Vec2<byte>(1, 2).AddChecked(new Vec2<byte>(3, 4)));
        Assert.Equal(new Vec2<byte>(9, 19), new Vec2<byte>(10, 20).Add(new Vec2<byte>(255, 255)));
        Assert.Throws<OverflowException>(() => new Vec2<byte>(10, 20).AddChecked(new Vec2<byte>(255, 255)));

        Assert.Equal(new Vec2<byte>(5, 10), new Vec2<byte>(10, 20) - new Vec2<byte>(5, 10));
        Assert.Equal(new Vec2<byte>(5, 10), checked(new Vec2<byte>(10, 20) - new Vec2<byte>(5, 10)));
        Assert.Equal(new Vec2<byte>(246, 236), new Vec2<byte>(10, 20) - new Vec2<byte>(20, 40));
        Assert.Throws<OverflowException>(() => checked(new Vec2<byte>(10, 20) - new Vec2<byte>(20, 40)));
        Assert.Equal(new Vec2<byte>(5, 10), new Vec2<byte>(10, 20).Subtract(new Vec2<byte>(5, 10)));
        Assert.Equal(new Vec2<byte>(5, 10), new Vec2<byte>(10, 20).SubtractChecked(new Vec2<byte>(5, 10)));
        Assert.Equal(new Vec2<byte>(246, 236), new Vec2<byte>(10, 20).Subtract(new Vec2<byte>(20, 40)));
        Assert.Throws<OverflowException>(() => new Vec2<byte>(10, 20).SubtractChecked(new Vec2<byte>(20, 40)));
    }
}
