using OpenCvSharp5.Internal;

namespace OpenCvSharp5.Tests;

public class SaturateCastLegacyTests
{
    [Fact]
    public void ToByteLegacyTest()
    {
        Assert.Equal((byte)100, SaturateCast.ToByte((sbyte)100));
        Assert.Equal((byte)100, SaturateCast.ToByte((short)100));
        Assert.Equal((byte)100, SaturateCast.ToByte((ushort)100));
        Assert.Equal((byte)100, SaturateCast.ToByte(100));
        Assert.Equal((byte)100, SaturateCast.ToByte((uint)100));
        Assert.Equal((byte)100, SaturateCast.ToByte(100.0f));
        Assert.Equal((byte)100, SaturateCast.ToByte(100.0));
        Assert.Equal((byte)100, SaturateCast.ToByte(100L));
        Assert.Equal((byte)100, SaturateCast.ToByte(100UL));

        Assert.Equal((byte)0, SaturateCast.ToByte((sbyte)-100));
        Assert.Equal((byte)0, SaturateCast.ToByte((short)-100));
        Assert.Equal((byte)0, SaturateCast.ToByte(-100));
        Assert.Equal((byte)0, SaturateCast.ToByte(-100.0f));
        Assert.Equal((byte)0, SaturateCast.ToByte(-100.0));
        Assert.Equal((byte)0, SaturateCast.ToByte(-100L));

        Assert.Equal((byte)255, SaturateCast.ToByte((short)10000));
        Assert.Equal((byte)255, SaturateCast.ToByte((ushort)10000));
        Assert.Equal((byte)255, SaturateCast.ToByte(10000));
        Assert.Equal((byte)255, SaturateCast.ToByte((uint)10000));
        Assert.Equal((byte)255, SaturateCast.ToByte(10000.0f));
        Assert.Equal((byte)255, SaturateCast.ToByte(10000.0));
        Assert.Equal((byte)255, SaturateCast.ToByte(10000L));
        Assert.Equal((byte)255, SaturateCast.ToByte(10000UL));

        Assert.Equal((byte)10, SaturateCast.ToByte(10.4f));
        Assert.Equal((byte)10, SaturateCast.ToByte(10.4));

        Assert.Equal((byte)11, SaturateCast.ToByte(10.6f));
        Assert.Equal((byte)11, SaturateCast.ToByte(10.6));
    }
}
