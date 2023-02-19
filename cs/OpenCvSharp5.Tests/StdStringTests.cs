using OpenCvSharp5.Internal;

namespace OpenCvSharp5.Tests;

public class StdStringTests
{
    [Fact]
    public void NewAndDispose()
    {
        using var obj = StdString.Create();
    }

    [Fact]
    public void ToStringAscii()
    {
        using var obj = StdString.Create("Foo");
        Assert.Equal("Foo", obj.ToString());
    }

    [Fact]
    public void ToStringNonAscii()
    {
        using var obj = StdString.Create("こんにちは你好🎉");
        Assert.Equal("こんにちは你好🎉", obj.ToString());
    }
}
