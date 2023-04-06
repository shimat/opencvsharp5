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

        Assert.Equal(new Vec2<byte>(1, 2), v);
        Assert.NotEqual(new Vec2<byte>(2, 1), v);
    }
}
