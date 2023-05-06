using OpenCvSharp5.Internal;

namespace OpenCvSharp5.Tests;

public class SaturateCastTests
{
    [Fact]
    public void ToByte()
    {
        Assert.Equal((byte)100, SaturateCast.ToByte((sbyte)100));
        Assert.Equal((byte)100, SaturateCast.ToByte((short)100));
        Assert.Equal((byte)100, SaturateCast.ToByte((ushort)100));
        Assert.Equal((byte)100, SaturateCast.ToByte(100));
        Assert.Equal((byte)100, SaturateCast.ToByte((uint)100));
        Assert.Equal((byte)100, SaturateCast.ToByte((Half)100.0));
        Assert.Equal((byte)100, SaturateCast.ToByte(100.0f));
        Assert.Equal((byte)100, SaturateCast.ToByte(100.0));
        Assert.Equal((byte)100, SaturateCast.ToByte(100L));
        Assert.Equal((byte)100, SaturateCast.ToByte(100UL));

        Assert.Equal((byte)0, SaturateCast.ToByte((sbyte)-100));
        Assert.Equal((byte)0, SaturateCast.ToByte((short)-100));
        Assert.Equal((byte)0, SaturateCast.ToByte(-100));
        Assert.Equal((byte)0, SaturateCast.ToByte(Half.MinValue));
        Assert.Equal((byte)0, SaturateCast.ToByte(-100.0f));
        Assert.Equal((byte)0, SaturateCast.ToByte(-100.0));
        Assert.Equal((byte)0, SaturateCast.ToByte(-100L));

        Assert.Equal((byte)255, SaturateCast.ToByte((short)10000));
        Assert.Equal((byte)255, SaturateCast.ToByte((ushort)10000));
        Assert.Equal((byte)255, SaturateCast.ToByte(10000));
        Assert.Equal((byte)255, SaturateCast.ToByte((uint)10000));
        Assert.Equal((byte)255, SaturateCast.ToByte(Half.MaxValue));
        Assert.Equal((byte)255, SaturateCast.ToByte(10000.0f));
        Assert.Equal((byte)255, SaturateCast.ToByte(10000.0));
        Assert.Equal((byte)255, SaturateCast.ToByte(10000L));
        Assert.Equal((byte)255, SaturateCast.ToByte(10000UL));

        Assert.Equal((byte)10, SaturateCast.ToByte(10.4f));
        Assert.Equal((byte)10, SaturateCast.ToByte(10.4));

        Assert.Equal((byte)11, SaturateCast.ToByte(10.6f));
        Assert.Equal((byte)11, SaturateCast.ToByte(10.6));
    }

    [Fact]
    public void ToInt16()
    {
        Assert.Equal((short)100, SaturateCast.ToInt16(100));
        Assert.Equal((short)100, SaturateCast.ToInt16(100));
        Assert.Equal((short)100, SaturateCast.ToInt16(100));
        Assert.Equal((short)100, SaturateCast.ToInt16(100));
        Assert.Equal((short)100, SaturateCast.ToInt16(100));
        Assert.Equal((short)100, SaturateCast.ToInt16(100));
        Assert.Equal((short)100, SaturateCast.ToInt16((Half)100.1));
        Assert.Equal((short)100, SaturateCast.ToInt16(100.1f));
        Assert.Equal((short)100, SaturateCast.ToInt16(100.1));
        Assert.Equal((short)100, SaturateCast.ToInt16(100L));
        Assert.Equal((short)100, SaturateCast.ToInt16(100UL));

        Assert.Equal((short)-100, SaturateCast.ToInt16(-100));
        Assert.Equal((short)-1000, SaturateCast.ToInt16(-1000));
        Assert.Equal(short.MinValue, SaturateCast.ToInt16(-100000));
        Assert.Equal(short.MinValue, SaturateCast.ToInt16(-100000.0f));
        Assert.Equal(short.MinValue, SaturateCast.ToInt16(Half.MinValue));
        Assert.Equal(short.MinValue, SaturateCast.ToInt16(-100000.0));
        Assert.Equal(short.MinValue, SaturateCast.ToInt16(-100000L));

        ///*
        Assert.Equal(1000, SaturateCast.ToInt16(1000));
        Assert.Equal(short.MaxValue, SaturateCast.ToInt16(60000));
        Assert.Equal(short.MaxValue, SaturateCast.ToInt16(100000));
        Assert.Equal(short.MaxValue, SaturateCast.ToInt16(100000));
        Assert.Equal(short.MaxValue, SaturateCast.ToInt16(100000.0));
        Assert.Equal(short.MaxValue, SaturateCast.ToInt16(100000.0f));
        Assert.Equal(short.MaxValue, SaturateCast.ToInt16(Half.MaxValue));
        Assert.Equal(short.MaxValue, SaturateCast.ToInt16(100000L));
        Assert.Equal(short.MaxValue, SaturateCast.ToInt16(100000UL));
        //*/
    }
}
