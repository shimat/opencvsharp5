namespace OpenCvSharp5.Tests.Core;

using Vec3b = Vec3<byte>;

public class Vec3Tests
{
    [Fact]
    public void Byte()
    {
        var v = new Vec3b(1, 2, 3);
        Assert.Equal(1, v.Item1);
        Assert.Equal(2, v.Item2);
        Assert.Equal(3, v.Item3);

        var (item1, item2, item3) = v;
        Assert.Equal(1, item1);
        Assert.Equal(2, item2);
        Assert.Equal(3, item3);

        Assert.Equal(new Vec3b(1, 2, 3), v);
        Assert.NotEqual(new Vec3b(2, 1, 0), v);

        Assert.Equal("Vec3 { Item1 = 1, Item2 = 2, Item3 = 3 }", v.ToString());

        Assert.Equal(1, v[0]);
        Assert.Equal(2, v[1]);
        Assert.Equal(3, v[2]);
        Assert.Equal(3, v[^1]);
        Assert.Equal(2, v[^2]);
        Assert.Equal(1, v[^3]);
        Assert.Throws<ArgumentOutOfRangeException>(() => v[3]);
        Assert.Throws<ArgumentOutOfRangeException>(() => v[-1]);
        Assert.Throws<ArgumentOutOfRangeException>(() => v[^4]);

        v[0] = 2;
        v[1] = 3;
        v[2] = 4;
        Assert.Equal(new Vec3b(2, 3, 4), v);
        v[^1] = 7;
        v[^2] = 6;
        v[^3] = 5;
        Assert.Equal(new Vec3b(5, 6, 7), v);
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            v[3] = 4;
        });
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            v[^4] = 4;
        });

        Assert.Equal(new Vec3b(111, 111, 111), Vec3b.All(111));
    }

    [Fact]
    public void ByteOperators()
    {
        Assert.Equal(new Vec3b(4, 6, 8), new Vec3b(1, 2, 3) + new Vec3b(3, 4, 5));
        Assert.Equal(new Vec3b(4, 6, 8), checked(new Vec3b(1, 2, 3) + new Vec3b(3, 4, 5)));
        Assert.Equal(new Vec3b(9, 19, 29), new Vec3b(10, 20, 30) + new Vec3b(255, 255, 255));
        Assert.Throws<OverflowException>(() => checked(new Vec3b(10, 20, 30) + new Vec3b(255, 255, 255)));
        Assert.Equal(new Vec3b(4, 6, 8), new Vec3b(1, 2, 3).Add(new Vec3b(3, 4, 5)));
        Assert.Equal(new Vec3b(4, 6, 8), new Vec3b(1, 2, 3).AddChecked(new Vec3b(3, 4, 5)));
        Assert.Equal(new Vec3b(9, 19, 29), new Vec3b(10, 20, 30).Add(new Vec3b(255, 255, 255)));
        Assert.Throws<OverflowException>(() => new Vec3b(10, 20, 30).AddChecked(new Vec3b(255, 255, 255)));

        Assert.Equal(new Vec3b(5, 10, 15), new Vec3b(10, 20, 30) - new Vec3b(5, 10, 15));
        Assert.Equal(new Vec3b(5, 10, 15), checked(new Vec3b(10, 20, 30) - new Vec3b(5, 10, 15)));
        Assert.Equal(new Vec3b(246, 236, 226), new Vec3b(10, 20, 30) - new Vec3b(20, 40, 60));
        Assert.Throws<OverflowException>(() => checked(new Vec3b(10, 20, 30) - new Vec3b(20, 40, 60)));
        Assert.Equal(new Vec3b(5, 10, 15), new Vec3b(10, 20, 30).Subtract(new Vec3b(5, 10, 15)));
        Assert.Equal(new Vec3b(5, 10, 15), new Vec3b(10, 20, 30).SubtractChecked(new Vec3b(5, 10, 15)));
        Assert.Equal(new Vec3b(246, 236, 226), new Vec3b(10, 20, 30).Subtract(new Vec3b(20, 40, 60)));
        Assert.Throws<OverflowException>(() => new Vec3b(10, 20, 30).SubtractChecked(new Vec3b(20, 40, 60)));

        Assert.Equal(new Vec3b(15, 30, 45), new Vec3b(10, 20, 30) * 1.5);
        Assert.Equal(new Vec3b(10, 21, 31), new Vec3b(10, 20, 30) * 1.05);
        Assert.Equal(new Vec3b(0, 0, 0), new Vec3b(10, 20, 30) * -1.5);
        Assert.Equal(new Vec3b(15, 30, 45), checked(new Vec3b(10, 20, 30) * 1.5));
        Assert.Equal(new Vec3b(10, 21, 31), checked(new Vec3b(10, 20, 30) * 1.05));
        Assert.Throws<OverflowException>(() => checked(new Vec3b(10, 20, 30) * -1.5));
        Assert.Equal(new Vec3b(15, 30, 45), new Vec3b(10, 20, 30).Multiply(1.5));
        Assert.Equal(new Vec3b(10, 21, 31), new Vec3b(10, 20, 30).Multiply(1.05));
        Assert.Equal(new Vec3b(0, 0, 0), new Vec3b(10, 20, 30).Multiply(-1.5));
        Assert.Equal(new Vec3b(15, 30, 45), new Vec3b(10, 20, 30).MultiplyChecked(1.5));
        Assert.Equal(new Vec3b(10, 21, 31), new Vec3b(10, 20, 30).MultiplyChecked(1.05));
        Assert.Throws<OverflowException>(() => new Vec3b(10, 20, 30).MultiplyChecked(-1.5));

        Assert.Equal(new Vec3b(5, 10, 15), new Vec3b(10, 20, 30) / 2);
        Assert.Equal(new Vec3b(3, 6, 10), new Vec3b(10, 20, 30) / 3);
        Assert.Equal(new Vec3b(0, 0, 0), new Vec3b(10, 20, 30) / -2);
        Assert.Equal(new Vec3b(255, 255, 255), new Vec3b(10, 20, 30) / 0);
        Assert.Equal(new Vec3b(5, 10, 15), checked(new Vec3b(10, 20, 30) / 2));
        Assert.Equal(new Vec3b(3, 6, 10), checked(new Vec3b(10, 20, 30) / 3));
        Assert.Throws<OverflowException>(() => checked(new Vec3b(10, 20, 30) / -2));
        Assert.Throws<OverflowException>(() => checked(new Vec3b(10, 20, 30) / 0));
        Assert.Equal(new Vec3b(5, 10, 15), new Vec3b(10, 20, 30).Divide(2));
        Assert.Equal(new Vec3b(3, 6, 10), new Vec3b(10, 20, 30).Divide(3));
        Assert.Equal(new Vec3b(0, 0, 0), new Vec3b(10, 20, 30).Divide(-2));
        Assert.Equal(new Vec3b(255, 255, 255), new Vec3b(10, 20, 30).Divide(0));
        Assert.Equal(new Vec3b(5, 10, 15), new Vec3b(10, 20, 30).DivideChecked(2));
        Assert.Equal(new Vec3b(3, 6, 10), new Vec3b(10, 20, 30).DivideChecked(3));
        Assert.Throws<OverflowException>(() => new Vec3b(10, 20, 30).DivideChecked(-2));
        Assert.Throws<OverflowException>(() => new Vec3b(10, 20, 30).DivideChecked(0));
    }
}
