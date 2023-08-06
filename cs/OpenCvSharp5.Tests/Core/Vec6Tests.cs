namespace OpenCvSharp5.Tests.Core;

using Vec6b = Vec6<byte>;

public class Vec6Tests
{
    [Fact]
    public void Byte()
    {
        var v = new Vec6b(1, 2, 3, 4, 5, 6);
        Assert.Equal(1, v.Item0);
        Assert.Equal(2, v.Item1);
        Assert.Equal(3, v.Item2);
        Assert.Equal(4, v.Item3);
        Assert.Equal(5, v.Item4);
        Assert.Equal(6, v.Item5);

        var (item1, item2, item3, item4, item5, item6) = v;
        Assert.Equal(1, item1);
        Assert.Equal(2, item2);
        Assert.Equal(3, item3);
        Assert.Equal(4, item4);
        Assert.Equal(5, item5);
        Assert.Equal(6, item6);

        Assert.Equal(new Vec6b(1, 2, 3, 4, 5, 6), v);
        Assert.NotEqual(new Vec6b(5, 4, 3, 2, 1, 0), v);

        Assert.Equal("Vec6 { Item0 = 1, Item1 = 2, Item2 = 3, Item3 = 4, Item4 = 5, Item5 = 6 }", v.ToString());

        Assert.Equal(1, v[0]);
        Assert.Equal(2, v[1]);
        Assert.Equal(3, v[2]);
        Assert.Equal(4, v[3]);
        Assert.Equal(5, v[4]);
        Assert.Equal(6, v[5]);
        Assert.Equal(6, v[^1]);
        Assert.Equal(5, v[^2]);
        Assert.Equal(4, v[^3]);
        Assert.Equal(3, v[^4]);
        Assert.Equal(2, v[^5]);
        Assert.Equal(1, v[^6]);
        Assert.Throws<ArgumentOutOfRangeException>(() => v[6]);
        Assert.Throws<ArgumentOutOfRangeException>(() => v[-1]);
        Assert.Throws<ArgumentOutOfRangeException>(() => v[^7]);

        v[0] = 2;
        v[1] = 3;
        v[2] = 4;
        v[3] = 5;
        v[4] = 6;
        v[5] = 7;
        Assert.Equal(new Vec6b(2, 3, 4, 5, 6, 7), v);
        v[^1] = 7;
        v[^2] = 6;
        v[^3] = 5;
        v[^4] = 4;
        v[^5] = 3;
        v[^6] = 2;
        Assert.Equal(new Vec6b(2, 3, 4, 5, 6, 7), v);
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            v[6] = 6;
        });
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            v[^7] = 7;
        });

        Assert.Equal(new Vec6b(111, 111, 111, 111, 111, 111), Vec6b.All(111));
    }

    [Fact]
    public void ByteOperators()
    {
        Assert.Equal(new Vec6b(4, 6, 8, 10, 12, 14), new Vec6b(1, 2, 3, 4, 5, 6) + new Vec6b(3, 4, 5, 6, 7, 8));
        Assert.Equal(new Vec6b(4, 6, 8, 10, 12, 14), checked(new Vec6b(1, 2, 3, 4, 5, 6) + new Vec6b(3, 4, 5, 6, 7, 8)));
        Assert.Equal(new Vec6b(9, 19, 29, 39, 49, 59), new Vec6b(10, 20, 30, 40, 50, 60) + new Vec6b(255, 255, 255, 255, 255, 255));
        Assert.Throws<OverflowException>(() => checked(new Vec6b(10, 20, 30, 40, 50, 60) + new Vec6b(255, 255, 255, 255, 255, 255)));
        Assert.Equal(new Vec6b(4, 6, 8, 10, 12, 14), new Vec6b(1, 2, 3, 4, 5, 6).Add(new Vec6b(3, 4, 5, 6, 7, 8)));
        Assert.Equal(new Vec6b(4, 6, 8, 10, 12, 14), new Vec6b(1, 2, 3, 4, 5, 6).AddChecked(new Vec6b(3, 4, 5, 6, 7, 8)));
        Assert.Equal(new Vec6b(9, 19, 29, 39, 49, 59), new Vec6b(10, 20, 30, 40, 50, 60).Add(new Vec6b(255, 255, 255, 255, 255, 255)));
        Assert.Throws<OverflowException>(() => new Vec6b(10, 20, 30, 40, 50, 60).AddChecked(new Vec6b(255, 255, 255, 255, 255, 255)));

        Assert.Equal(new Vec6b(5, 10, 15, 20, 25, 30), new Vec6b(10, 20, 30, 40, 50, 60) - new Vec6b(5, 10, 15, 20, 25, 30));
        Assert.Equal(new Vec6b(5, 10, 15, 20, 25, 30), checked(new Vec6b(10, 20, 30, 40, 50, 60) - new Vec6b(5, 10, 15, 20, 25, 30)));
        Assert.Equal(new Vec6b(246, 236, 226, 216, 206, 196), new Vec6b(10, 20, 30, 40, 50, 60) - new Vec6b(20, 40, 60, 80, 100, 120));
        Assert.Throws<OverflowException>(() => checked(new Vec6b(10, 20, 30, 40, 50, 60) - new Vec6b(20, 40, 60, 80, 100, 120)));
        Assert.Equal(new Vec6b(5, 10, 15, 20, 25, 30), new Vec6b(10, 20, 30, 40, 50, 60).Subtract(new Vec6b(5, 10, 15, 20, 25, 30)));
        Assert.Equal(new Vec6b(5, 10, 15, 20, 25, 30), new Vec6b(10, 20, 30, 40, 50, 60).SubtractChecked(new Vec6b(5, 10, 15, 20, 25, 30)));
        Assert.Equal(new Vec6b(246, 236, 226, 216, 206, 196), new Vec6b(10, 20, 30, 40, 50, 60).Subtract(new Vec6b(20, 40, 60, 80, 100, 120)));
        Assert.Throws<OverflowException>(() => new Vec6b(10, 20, 30, 40, 50, 60).SubtractChecked(new Vec6b(20, 40, 60, 80, 100, 120)));

        Assert.Equal(new Vec6b(15, 30, 45, 60, 75, 90), new Vec6b(10, 20, 30, 40, 50, 60) * 1.5);
        Assert.Equal(new Vec6b(10, 21, 31, 42, 52, 63), new Vec6b(10, 20, 30, 40, 50, 60) * 1.05);
        Assert.Equal(new Vec6b(0, 0, 0, 0, 0, 0), new Vec6b(10, 20, 30, 40, 50, 60) * -1.5);
        Assert.Equal(new Vec6b(15, 30, 45, 60, 75, 90), checked(new Vec6b(10, 20, 30, 40, 50, 60) * 1.5));
        Assert.Equal(new Vec6b(10, 21, 31, 42, 52, 63), checked(new Vec6b(10, 20, 30, 40, 50, 60) * 1.05));
        Assert.Throws<OverflowException>(() => checked(new Vec6b(10, 20, 30, 40, 50, 60) * -1.5));
        Assert.Equal(new Vec6b(15, 30, 45, 60, 75, 90), new Vec6b(10, 20, 30, 40, 50, 60).Multiply(1.5));
        Assert.Equal(new Vec6b(10, 21, 31, 42, 52, 63), new Vec6b(10, 20, 30, 40, 50, 60).Multiply(1.05));
        Assert.Equal(new Vec6b(0, 0, 0, 0, 0, 0), new Vec6b(10, 20, 30, 40, 50, 60).Multiply(-1.5));
        Assert.Equal(new Vec6b(15, 30, 45, 60, 75, 90), new Vec6b(10, 20, 30, 40, 50, 60).MultiplyChecked(1.5));
        Assert.Equal(new Vec6b(10, 21, 31, 42, 52, 63), new Vec6b(10, 20, 30, 40, 50, 60).MultiplyChecked(1.05));
        Assert.Throws<OverflowException>(() => new Vec6b(10, 20, 30, 40, 50, 60).MultiplyChecked(-1.5));

        Assert.Equal(new Vec6b(5, 10, 15, 20, 25, 30), new Vec6b(10, 20, 30, 40, 50, 60) / 2);
        Assert.Equal(new Vec6b(3, 6, 10, 13, 16, 20), new Vec6b(10, 20, 30, 40, 50, 60) / 3);
        Assert.Equal(new Vec6b(0, 0, 0, 0, 0, 0), new Vec6b(10, 20, 30, 40, 50, 60) / -2);
        Assert.Equal(new Vec6b(255, 255, 255, 255, 255, 255), new Vec6b(10, 20, 30, 40, 50, 60) / 0);
        Assert.Equal(new Vec6b(5, 10, 15, 20, 25, 30), checked(new Vec6b(10, 20, 30, 40, 50, 60) / 2));
        Assert.Equal(new Vec6b(3, 6, 10, 13, 16, 20), checked(new Vec6b(10, 20, 30, 40, 50, 60) / 3));
        Assert.Throws<OverflowException>(() => checked(new Vec6b(10, 20, 30, 40, 50, 60) / -2));
        Assert.Throws<OverflowException>(() => checked(new Vec6b(10, 20, 30, 40, 50, 60) / 0));
        Assert.Equal(new Vec6b(5, 10, 15, 20, 25, 30), new Vec6b(10, 20, 30, 40, 50, 60).Divide(2));
        Assert.Equal(new Vec6b(3, 6, 10, 13, 16, 20), new Vec6b(10, 20, 30, 40, 50, 60).Divide(3));
        Assert.Equal(new Vec6b(0, 0, 0, 0, 0, 0), new Vec6b(10, 20, 30, 40, 50, 60).Divide(-2));
        Assert.Equal(new Vec6b(255, 255, 255, 255, 255, 255), new Vec6b(10, 20, 30, 40, 50, 60).Divide(0));
        Assert.Equal(new Vec6b(5, 10, 15, 20, 25, 30), new Vec6b(10, 20, 30, 40, 50, 60).DivideChecked(2));
        Assert.Equal(new Vec6b(3, 6, 10, 13, 16, 20), new Vec6b(10, 20, 30, 40, 50, 60).DivideChecked(3));
        Assert.Throws<OverflowException>(() => new Vec6b(10, 20, 30, 40, 50, 60).DivideChecked(-2));
        Assert.Throws<OverflowException>(() => new Vec6b(10, 20, 30, 40, 50, 60).DivideChecked(0));
    }
}
