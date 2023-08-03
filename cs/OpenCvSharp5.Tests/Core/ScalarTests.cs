using System.Security.Cryptography;

namespace OpenCvSharp5.Tests.Core;

public class ScalarTests
{
    [Fact]
    public void Equal()
    {
        var s1 = new Scalar(1, 2, 3, 4);
        var s2 = new Scalar(1, 2, 3, 4);
        Assert.Equal(s1, s2);
    }
    
    [Fact]
    public void NewBy1Value()
    {
        var s = new Scalar(1);
        Assert.Equal(1, s.Val0);
        Assert.Equal(0, s.Val1);
        Assert.Equal(0, s.Val2);
        Assert.Equal(0, s.Val3);
        Assert.True(s.IsReal());
    }

    [Fact]
    public void NewBy2Values()
    {
        var s = new Scalar(1, 2);
        Assert.Equal(1, s.Val0);
        Assert.Equal(2, s.Val1);
        Assert.Equal(0, s.Val2);
        Assert.Equal(0, s.Val3);
        Assert.False(s.IsReal());
    }

    [Fact]
    public void NewBy3Values()
    {
        var s = new Scalar(1, 2, 3);
        Assert.Equal(1, s.Val0);
        Assert.Equal(2, s.Val1);
        Assert.Equal(3, s.Val2);
        Assert.Equal(0, s.Val3);
        Assert.False(s.IsReal());
    }

    [Fact]
    public void NewBy4Values()
    {
        var s = new Scalar(1, 2, 3, 4);
        Assert.Equal(1, s.Val0);
        Assert.Equal(2, s.Val1);
        Assert.Equal(3, s.Val2);
        Assert.Equal(4, s.Val3);
        Assert.False(s.IsReal());
    }

    [Fact]
    public void All()
    {
        var s = Scalar.All(1);
        Assert.Equal(1, s.Val0);
        Assert.Equal(1, s.Val1);
        Assert.Equal(1, s.Val2);
        Assert.Equal(1, s.Val3);
        Assert.False(s.IsReal());
    }

    [Fact]
    public void GetIndexer()
    {
        var s = new Scalar(1, 2, 3, 4);
        Assert.Equal(1, s[0]);
        Assert.Equal(2, s[1]);
        Assert.Equal(3, s[2]);
        Assert.Equal(4, s[3]);
        Assert.Equal(4, s[^1]);
        Assert.Equal(3, s[^2]);
        Assert.Equal(2, s[^3]);
        Assert.Equal(1, s[^4]);
        Assert.Throws<ArgumentOutOfRangeException>(() => s[4]);
        Assert.Throws<ArgumentOutOfRangeException>(() => s[-1]);
        Assert.Throws<ArgumentOutOfRangeException>(() => s[^5]);
    }

    [Fact]
    public void SetIndexer()
    {
        // ReSharper disable once UseObjectOrCollectionInitializer
        var s = new Scalar();
        s[0] = 2;
        s[1] = 3;
        s[2] = 4;
        s[3] = 5;
        Assert.Equal(new Scalar(2, 3, 4, 5), s);

        s[^1] = 7;
        s[^2] = 6;
        s[^3] = 5;
        s[^4] = 4;
        Assert.Equal(new Scalar(4, 5, 6, 7), s);

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            s[4] = 4;
        });
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            s[^5] = 4;
        });
    }

    [Fact]
    public void RandomColor1()
    {
        var s = Scalar.RandomColor();
        Assert.InRange(s.Val0, 0, 255);
        Assert.InRange(s.Val1, 0, 255);
        Assert.InRange(s.Val2, 0, 255);
        Assert.Equal(0, s.Val3);
    }

    [Fact]
    public void RandomColor2()
    {
        var rng = new MockRandomNumberGenerator();
        var s = Scalar.RandomColor(rng);
        Assert.Equal(1, s.Val0);
        Assert.Equal(2, s.Val1);
        Assert.Equal(3, s.Val2);
        Assert.Equal(0, s.Val3);
    }
    
    private class MockRandomNumberGenerator : RandomNumberGenerator
    {
        public override void GetBytes(byte[] data)
        {
            data[0] = 1;
            data[1] = 2;
            data[2] = 3;
        }
    }
}
