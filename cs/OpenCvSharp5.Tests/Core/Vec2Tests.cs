namespace OpenCvSharp5.Tests.Core;

using Vec2b = Vec2<byte>;

public class Vec2Tests
{
    [Fact]
    public void Byte()
    {
        var v = new Vec2b(1, 2);
        Assert.Equal(1, v.Item1);
        Assert.Equal(2, v.Item2);

        var (item1, item2) = v;
        Assert.Equal(1, item1);
        Assert.Equal(2, item2);

        Assert.Equal(new Vec2b(1, 2), v);
        Assert.NotEqual(new Vec2b(2, 1), v);

        Assert.Equal("Vec2 { Item1 = 1, Item2 = 2 }", v.ToString());

        Assert.Equal(1, v[0]);
        Assert.Equal(2, v[1]);
        Assert.Equal(2, v[^1]);
        Assert.Equal(1, v[^2]);
        Assert.Throws<ArgumentOutOfRangeException>(() => v[2]);
        Assert.Throws<ArgumentOutOfRangeException>(() => v[-1]);
        Assert.Throws<ArgumentOutOfRangeException>(() => v[^3]);

        v[0] = 2;
        v[1] = 3;
        Assert.Equal(new Vec2b(2, 3), v);
        v[^1] = 5;
        v[^2] = 4;
        Assert.Equal(new Vec2b(4, 5), v);
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            v[2] = 4;
        });
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            v[^3] = 4;
        });

        Assert.Equal(new Vec2b(111, 111), Vec2b.All(111));
    }

    [Fact]
    public void ByteOperators()
    {
        Assert.Equal(new Vec2b(4, 6), new Vec2b(1, 2) + new Vec2b(3, 4));
        Assert.Equal(new Vec2b(4, 6), checked(new Vec2b(1, 2) + new Vec2b(3, 4)));
        Assert.Equal(new Vec2b(9, 19), new Vec2b(10, 20) + new Vec2b(255, 255));
        Assert.Throws<OverflowException>(() => checked(new Vec2b(10, 20) + new Vec2b(255, 255)));
        Assert.Equal(new Vec2b(4, 6), new Vec2b(1, 2).Add(new Vec2b(3, 4)));
        Assert.Equal(new Vec2b(4, 6), new Vec2b(1, 2).AddChecked(new Vec2b(3, 4)));
        Assert.Equal(new Vec2b(9, 19), new Vec2b(10, 20).Add(new Vec2b(255, 255)));
        Assert.Throws<OverflowException>(() => new Vec2b(10, 20).AddChecked(new Vec2b(255, 255)));

        Assert.Equal(new Vec2b(5, 10), new Vec2b(10, 20) - new Vec2b(5, 10));
        Assert.Equal(new Vec2b(5, 10), checked(new Vec2b(10, 20) - new Vec2b(5, 10)));
        Assert.Equal(new Vec2b(246, 236), new Vec2b(10, 20) - new Vec2b(20, 40));
        Assert.Throws<OverflowException>(() => checked(new Vec2b(10, 20) - new Vec2b(20, 40)));
        Assert.Equal(new Vec2b(5, 10), new Vec2b(10, 20).Subtract(new Vec2b(5, 10)));
        Assert.Equal(new Vec2b(5, 10), new Vec2b(10, 20).SubtractChecked(new Vec2b(5, 10)));
        Assert.Equal(new Vec2b(246, 236), new Vec2b(10, 20).Subtract(new Vec2b(20, 40)));
        Assert.Throws<OverflowException>(() => new Vec2b(10, 20).SubtractChecked(new Vec2b(20, 40)));

        Assert.Equal(new Vec2b(15, 30), new Vec2b(10, 20) * 1.5);
        Assert.Equal(new Vec2b(10, 21), new Vec2b(10, 20) * 1.05);
        Assert.Equal(new Vec2b(0, 0), new Vec2b(10, 20) * -1.5);
        Assert.Equal(new Vec2b(15, 30), checked(new Vec2b(10, 20) * 1.5));
        Assert.Equal(new Vec2b(10, 21), checked(new Vec2b(10, 20) * 1.05));
        Assert.Throws<OverflowException>(() => checked(new Vec2b(10, 20) * -1.5));
        Assert.Equal(new Vec2b(15, 30), new Vec2b(10, 20).Multiply(1.5));
        Assert.Equal(new Vec2b(10, 21), new Vec2b(10, 20).Multiply(1.05));
        Assert.Equal(new Vec2b(0, 0), new Vec2b(10, 20).Multiply(-1.5));
        Assert.Equal(new Vec2b(15, 30), new Vec2b(10, 20).MultiplyChecked(1.5));
        Assert.Equal(new Vec2b(10, 21), new Vec2b(10, 20).MultiplyChecked(1.05));
        Assert.Throws<OverflowException>(() => new Vec2b(10, 20).MultiplyChecked(-1.5));

        Assert.Equal(new Vec2b(5, 10), new Vec2b(10, 20) / 2);
        Assert.Equal(new Vec2b(3, 6), new Vec2b(10, 20) / 3);
        Assert.Equal(new Vec2b(0, 0), new Vec2b(10, 20) / -2);
        Assert.Equal(new Vec2b(255, 255), new Vec2b(10, 20) / 0);
        Assert.Equal(new Vec2b(5, 10), checked(new Vec2b(10, 20) / 2));
        Assert.Equal(new Vec2b(3, 6), checked(new Vec2b(10, 20) / 3));
        Assert.Throws<OverflowException>(() => checked(new Vec2b(10, 20) / -2));
        Assert.Throws<OverflowException>(() => checked(new Vec2b(10, 20) / 0));
        Assert.Equal(new Vec2b(5, 10), new Vec2b(10, 20).Divide(2));
        Assert.Equal(new Vec2b(3, 6), new Vec2b(10, 20).Divide(3));
        Assert.Equal(new Vec2b(0, 0), new Vec2b(10, 20).Divide(-2));
        Assert.Equal(new Vec2b(255, 255), new Vec2b(10, 20).Divide(0));
        Assert.Equal(new Vec2b(5, 10), new Vec2b(10, 20).DivideChecked(2));
        Assert.Equal(new Vec2b(3, 6), new Vec2b(10, 20).DivideChecked(3));
        Assert.Throws<OverflowException>(() => new Vec2b(10, 20).DivideChecked(-2));
        Assert.Throws<OverflowException>(() => new Vec2b(10, 20).DivideChecked(0));
    }
}
