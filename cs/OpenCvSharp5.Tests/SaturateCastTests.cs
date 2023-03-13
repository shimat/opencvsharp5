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
    public void ToByteGeneric()
    {
        Assert.Equal((byte)100, SaturateCast.Cast<sbyte, byte>(100));
        Assert.Equal((byte)100, SaturateCast.Cast<short, byte>(100));
        Assert.Equal((byte)100, SaturateCast.Cast<ushort, byte>(100));
        Assert.Equal((byte)100, SaturateCast.Cast<int, byte>(100));
        Assert.Equal((byte)100, SaturateCast.Cast<uint, byte>(100));
        Assert.Equal((byte)100, SaturateCast.Cast<float, byte>(100.0f));
        Assert.Equal((byte)100, SaturateCast.Cast<double, byte>(100.0));
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
        
        Assert.Equal((byte)10, SaturateCast.Cast<float, byte>(10.4f));
        Assert.Equal((byte)10, SaturateCast.Cast<double, byte>(10.4));
        Assert.Equal((byte)10, SaturateCast.Cast<float, byte>(10.9f));
        Assert.Equal((byte)10, SaturateCast.Cast<double, byte>(10.9));
    }

    [Fact]
    public void ToByteGenericWithRounding()
    {
        Assert.Equal((byte)10, SaturateCast.CastWithRounding<float, byte>(10.1f));
        Assert.Equal((byte)10, SaturateCast.CastWithRounding<double, byte>(10.1));

        Assert.Equal((byte)11, SaturateCast.CastWithRounding<float, byte>(10.9f));
        Assert.Equal((byte)11, SaturateCast.CastWithRounding<double, byte>(10.9));
    }

    [Fact]
    public void ToSByte()
    {
        Assert.Equal((sbyte)100, SaturateCast.ToSByte((byte)100));
        Assert.Equal((sbyte)100, SaturateCast.ToSByte((short)100));
        Assert.Equal((sbyte)100, SaturateCast.ToSByte((ushort)100));
        Assert.Equal((sbyte)100, SaturateCast.ToSByte(100));
        Assert.Equal((sbyte)100, SaturateCast.ToSByte((uint)100));
        Assert.Equal((sbyte)100, SaturateCast.ToSByte(100.0f));
        Assert.Equal((sbyte)100, SaturateCast.ToSByte(100.0));
        Assert.Equal((sbyte)100, SaturateCast.ToSByte(100L));
        Assert.Equal((sbyte)100, SaturateCast.ToSByte(100UL));

        Assert.Equal((sbyte)-128, SaturateCast.ToSByte((short)-1000));
        Assert.Equal((sbyte)-128, SaturateCast.ToSByte(-1000));
        Assert.Equal((sbyte)-128, SaturateCast.ToSByte(-1000.0f));
        Assert.Equal((sbyte)-128, SaturateCast.ToSByte(-1000.0));
        Assert.Equal((sbyte)-128, SaturateCast.ToSByte(-1000L));
        
        Assert.Equal((sbyte)127, SaturateCast.ToSByte((byte)255));
        Assert.Equal((sbyte)127, SaturateCast.ToSByte((short)10000));
        Assert.Equal((sbyte)127, SaturateCast.ToSByte((ushort)10000));
        Assert.Equal((sbyte)127, SaturateCast.ToSByte(10000));
        Assert.Equal((sbyte)127, SaturateCast.ToSByte((uint)10000));
        Assert.Equal((sbyte)127, SaturateCast.ToSByte(10000.0f));
        Assert.Equal((sbyte)127, SaturateCast.ToSByte(10000.0));
        Assert.Equal((sbyte)127, SaturateCast.ToSByte(10000L));
        Assert.Equal((sbyte)127, SaturateCast.ToSByte(10000UL));
        
        Assert.Equal((sbyte)10, SaturateCast.ToSByte(10.4f));
        Assert.Equal((sbyte)10, SaturateCast.ToSByte(10.4));
        Assert.Equal((sbyte)11, SaturateCast.ToSByte(10.6f));
        Assert.Equal((sbyte)11, SaturateCast.ToSByte(10.6));
    }

    [Fact]
    public void ToSByteGeneric()
    {
        Assert.Equal((sbyte)100, SaturateCast.Cast<byte, sbyte>(100));
        Assert.Equal((sbyte)100, SaturateCast.Cast<short, sbyte>(100));
        Assert.Equal((sbyte)100, SaturateCast.Cast<ushort, sbyte>(100));
        Assert.Equal((sbyte)100, SaturateCast.Cast<int, sbyte>(100));
        Assert.Equal((sbyte)100, SaturateCast.Cast<uint, sbyte>(100));
        Assert.Equal((sbyte)100, SaturateCast.Cast<float, sbyte>(100.0f));
        Assert.Equal((sbyte)100, SaturateCast.Cast<double, sbyte>(100.0));
        Assert.Equal((sbyte)100, SaturateCast.Cast<long, sbyte>(100L));
        Assert.Equal((sbyte)100, SaturateCast.Cast<ulong, sbyte>(100UL));
        
        Assert.Equal((sbyte)-128, SaturateCast.Cast<short, sbyte>(-1000));
        Assert.Equal((sbyte)-128, SaturateCast.Cast<int, sbyte>(-1000));
        Assert.Equal((sbyte)-128, SaturateCast.Cast<float, sbyte>(-1000.0f));
        Assert.Equal((sbyte)-128, SaturateCast.Cast<double, sbyte>(-1000.0));
        Assert.Equal((sbyte)-128, SaturateCast.Cast<long, sbyte>(-1000L));
        
        Assert.Equal((sbyte)127, SaturateCast.Cast<short, sbyte>(10000));
        Assert.Equal((sbyte)127, SaturateCast.Cast<ushort, sbyte>(10000));
        Assert.Equal((sbyte)127, SaturateCast.Cast<int, sbyte>(10000));
        Assert.Equal((sbyte)127, SaturateCast.Cast<uint, sbyte>(10000));
        Assert.Equal((sbyte)127, SaturateCast.Cast<float, sbyte>(10000.0f));
        Assert.Equal((sbyte)127, SaturateCast.Cast<double, sbyte>(10000.0));
        Assert.Equal((sbyte)127, SaturateCast.Cast<long, sbyte>(10000L));
        Assert.Equal((sbyte)127, SaturateCast.Cast<ulong, sbyte>(10000UL));
        
        Assert.Equal((sbyte)10, SaturateCast.Cast<float, sbyte>(10.4f));
        Assert.Equal((sbyte)10, SaturateCast.Cast<double, sbyte>(10.4));
        Assert.Equal((sbyte)10, SaturateCast.Cast<float, sbyte>(10.9f));
        Assert.Equal((sbyte)10, SaturateCast.Cast<double, sbyte>(10.9));
    }

    [Fact]
    public void ToSByteGenericWithRounding()
    {
        Assert.Equal((sbyte)10, SaturateCast.CastWithRounding<float, sbyte>(10.1f));
        Assert.Equal((sbyte)10, SaturateCast.CastWithRounding<double, sbyte>(10.1));

        Assert.Equal((sbyte)11, SaturateCast.CastWithRounding<float, sbyte>(10.9f));
        Assert.Equal((sbyte)11, SaturateCast.CastWithRounding<double, sbyte>(10.9));
    }
}
