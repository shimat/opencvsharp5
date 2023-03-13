using System.Numerics;
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

    [Fact]
    public void ToByteNew()
    {
        Assert.Equal((byte)100, SaturateCast.Cast<sbyte, byte>(100));
        Assert.Equal((byte)100, SaturateCast.Cast<short, byte>(100));
        Assert.Equal((byte)100, SaturateCast.Cast<ushort, byte>(100));
        Assert.Equal((byte)100, SaturateCast.Cast<int, byte>(100));
        Assert.Equal((byte)100, SaturateCast.Cast<uint, byte>(100));
        Assert.Equal((byte)100, SaturateCast.Cast<float, byte>(100.1f));
        Assert.Equal((byte)100, SaturateCast.Cast<double, byte>(100.1));
        Assert.Equal((byte)100, SaturateCast.Cast<long, byte>(100L));
        Assert.Equal((byte)100, SaturateCast.Cast<ulong, byte>(100UL));
        
        Assert.Equal((byte)0, SaturateCast.Cast<sbyte, byte>(-100));
        Assert.Equal((byte)0, SaturateCast.Cast<short, byte>(-100));
        Assert.Equal((byte)0, SaturateCast.Cast<int, byte>(-100));
        Assert.Equal((byte)0, SaturateCast.Cast<float, byte>(-100.0f));
        Assert.Equal((byte)0, SaturateCast.Cast<double, byte>(-100.0));
        Assert.Equal((byte)0, SaturateCast.Cast<long, byte>(-100L));
        
        Assert.Equal((byte)255, SaturateCast.Cast<short, byte>(10000));
        Assert.Equal((byte)255, SaturateCast.Cast<ushort, byte>(10000));
        Assert.Equal((byte)255, SaturateCast.Cast<int, byte>(10000));
        Assert.Equal((byte)255, SaturateCast.Cast<uint, byte>(10000));
        Assert.Equal((byte)255, SaturateCast.Cast<float, byte>(10000.0f));
        Assert.Equal((byte)255, SaturateCast.Cast<double, byte>(10000.0));
        Assert.Equal((byte)255, SaturateCast.Cast<long, byte>(10000L));
        Assert.Equal((byte)255, SaturateCast.Cast<ulong, byte>(10000UL));
        /*
        Assert.Equal((byte)11, SaturateCast.Cast<float, byte>(10.9f));
        Assert.Equal((byte)11, SaturateCast.Cast<double, byte>(10.9));*/
    }
}
