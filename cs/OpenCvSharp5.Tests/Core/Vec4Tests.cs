namespace OpenCvSharp5.Tests.Core;

using Vec4b = Vec4<byte>;

public class Vec4Tests
{
    [Fact]
    public void Byte()
    {
        var v = new Vec4b(1, 2, 3, 4);
        Assert.Equal(1, v.Item0);
        Assert.Equal(2, v.Item1);
        Assert.Equal(3, v.Item2);
        Assert.Equal(4, v.Item3);

        var (item1, item2, item3, item4) = v;
        Assert.Equal(1, item1);
        Assert.Equal(2, item2);
        Assert.Equal(3, item3);
        Assert.Equal(4, item4);

        Assert.Equal(new Vec4b(1, 2, 3, 4), v);
        Assert.NotEqual(new Vec4b(3, 2, 1, 0), v);

        Assert.Equal("Vec4 { Item0 = 1, Item1 = 2, Item2 = 3, Item3 = 4 }", v.ToString());

        Assert.Equal(1, v[0]);
        Assert.Equal(2, v[1]);
        Assert.Equal(3, v[2]);
        Assert.Equal(4, v[3]);
        Assert.Equal(4, v[^1]);
        Assert.Equal(3, v[^2]);
        Assert.Equal(2, v[^3]);
        Assert.Equal(1, v[^4]);
        Assert.Throws<ArgumentOutOfRangeException>(() => v[4]);
        Assert.Throws<ArgumentOutOfRangeException>(() => v[-1]);
        Assert.Throws<ArgumentOutOfRangeException>(() => v[^5]);

        v[0] = 2;
        v[1] = 3;
        v[2] = 4;
        v[3] = 5;
        Assert.Equal(new Vec4b(2, 3, 4, 5), v);
        v[^1] = 7;
        v[^2] = 6;
        v[^3] = 5;
        v[^4] = 4;
        Assert.Equal(new Vec4b(4, 5, 6, 7), v);
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            v[4] = 4;
        });
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            v[^5] = 4;
        });

        Assert.Equal(new Vec4b(111, 111, 111, 111), Vec4b.All(111));
    }

    [Fact]
    public void ByteOperators()
    {
        Assert.Equal(new Vec4b(4, 6, 8, 10), new Vec4b(1, 2, 3, 4) + new Vec4b(3, 4, 5, 6));
        Assert.Equal(new Vec4b(4, 6, 8, 10), checked(new Vec4b(1, 2, 3, 4) + new Vec4b(3, 4, 5, 6)));
        Assert.Equal(new Vec4b(9, 19, 29, 39), new Vec4b(10, 20, 30, 40) + new Vec4b(255, 255, 255, 255));
        Assert.Throws<OverflowException>(() => checked(new Vec4b(10, 20, 30, 40) + new Vec4b(255, 255, 255, 255)));
        Assert.Equal(new Vec4b(4, 6, 8, 10), new Vec4b(1, 2, 3, 4).Add(new Vec4b(3, 4, 5, 6)));
        Assert.Equal(new Vec4b(4, 6, 8, 10), new Vec4b(1, 2, 3, 4).AddChecked(new Vec4b(3, 4, 5, 6)));
        Assert.Equal(new Vec4b(9, 19, 29, 39), new Vec4b(10, 20, 30, 40).Add(new Vec4b(255, 255, 255, 255)));
        Assert.Throws<OverflowException>(() => new Vec4b(10, 20, 30, 40).AddChecked(new Vec4b(255, 255, 255, 255)));

        Assert.Equal(new Vec4b(5, 10, 15, 20), new Vec4b(10, 20, 30, 40) - new Vec4b(5, 10, 15, 20));
        Assert.Equal(new Vec4b(5, 10, 15, 20), checked(new Vec4b(10, 20, 30, 40) - new Vec4b(5, 10, 15, 20)));
        Assert.Equal(new Vec4b(246, 236, 226, 216), new Vec4b(10, 20, 30, 40) - new Vec4b(20, 40, 60, 80));
        Assert.Throws<OverflowException>(() => checked(new Vec4b(10, 20, 30, 40) - new Vec4b(20, 40, 60, 80)));
        Assert.Equal(new Vec4b(5, 10, 15, 20), new Vec4b(10, 20, 30, 40).Subtract(new Vec4b(5, 10, 15, 20)));
        Assert.Equal(new Vec4b(5, 10, 15, 20), new Vec4b(10, 20, 30, 40).SubtractChecked(new Vec4b(5, 10, 15, 20)));
        Assert.Equal(new Vec4b(246, 236, 226, 216), new Vec4b(10, 20, 30, 40).Subtract(new Vec4b(20, 40, 60, 80)));
        Assert.Throws<OverflowException>(() => new Vec4b(10, 20, 30, 40).SubtractChecked(new Vec4b(20, 40, 60, 80)));

        Assert.Equal(new Vec4b(15, 30, 45, 60), new Vec4b(10, 20, 30, 40) * 1.5);
        Assert.Equal(new Vec4b(10, 21, 31, 42), new Vec4b(10, 20, 30, 40) * 1.05);
        Assert.Equal(new Vec4b(0, 0, 0, 0), new Vec4b(10, 20, 30, 40) * -1.5);
        Assert.Equal(new Vec4b(15, 30, 45, 60), checked(new Vec4b(10, 20, 30, 40) * 1.5));
        Assert.Equal(new Vec4b(10, 21, 31, 42), checked(new Vec4b(10, 20, 30, 40) * 1.05));
        Assert.Throws<OverflowException>(() => checked(new Vec4b(10, 20, 30, 40) * -1.5));
        Assert.Equal(new Vec4b(15, 30, 45, 60), new Vec4b(10, 20, 30, 40).Multiply(1.5));
        Assert.Equal(new Vec4b(10, 21, 31, 42), new Vec4b(10, 20, 30, 40).Multiply(1.05));
        Assert.Equal(new Vec4b(0, 0, 0, 0), new Vec4b(10, 20, 30, 40).Multiply(-1.5));
        Assert.Equal(new Vec4b(15, 30, 45, 60), new Vec4b(10, 20, 30, 40).MultiplyChecked(1.5));
        Assert.Equal(new Vec4b(10, 21, 31, 42), new Vec4b(10, 20, 30, 40).MultiplyChecked(1.05));
        Assert.Throws<OverflowException>(() => new Vec4b(10, 20, 30, 40).MultiplyChecked(-1.5));

        Assert.Equal(new Vec4b(5, 10, 15, 20), new Vec4b(10, 20, 30, 40) / 2);
        Assert.Equal(new Vec4b(3, 6, 10, 13), new Vec4b(10, 20, 30, 40) / 3);
        Assert.Equal(new Vec4b(0, 0, 0, 0), new Vec4b(10, 20, 30, 40) / -2);
        Assert.Equal(new Vec4b(255, 255, 255, 255), new Vec4b(10, 20, 30, 40) / 0);
        Assert.Equal(new Vec4b(5, 10, 15, 20), checked(new Vec4b(10, 20, 30, 40) / 2));
        Assert.Equal(new Vec4b(3, 6, 10, 13), checked(new Vec4b(10, 20, 30, 40) / 3));
        Assert.Throws<OverflowException>(() => checked(new Vec4b(10, 20, 30, 40) / -2));
        Assert.Throws<OverflowException>(() => checked(new Vec4b(10, 20, 30, 40) / 0));
        Assert.Equal(new Vec4b(5, 10, 15, 20), new Vec4b(10, 20, 30, 40).Divide(2));
        Assert.Equal(new Vec4b(3, 6, 10, 13), new Vec4b(10, 20, 30, 40).Divide(3));
        Assert.Equal(new Vec4b(0, 0, 0, 0), new Vec4b(10, 20, 30, 40).Divide(-2));
        Assert.Equal(new Vec4b(255, 255, 255, 255), new Vec4b(10, 20, 30, 40).Divide(0));
        Assert.Equal(new Vec4b(5, 10, 15, 20), new Vec4b(10, 20, 30, 40).DivideChecked(2));
        Assert.Equal(new Vec4b(3, 6, 10, 13), new Vec4b(10, 20, 30, 40).DivideChecked(3));
        Assert.Throws<OverflowException>(() => new Vec4b(10, 20, 30, 40).DivideChecked(-2));
        Assert.Throws<OverflowException>(() => new Vec4b(10, 20, 30, 40).DivideChecked(0));
    }
}
