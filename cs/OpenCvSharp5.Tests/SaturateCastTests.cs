using OpenCvSharp5.Internal;
using static OpenCvSharp5.Internal.SaturateCastMethods;

namespace OpenCvSharp5.Tests;

public class SaturateCastTests
{
    [Fact]
    public void ToByte()
    {
        Assert.Equal((byte)100, SaturateCast<sbyte, byte>(100));
        Assert.Equal((byte)100, SaturateCast<byte, byte>(100));
        Assert.Equal((byte)100, SaturateCast<short, byte>(100));
        Assert.Equal((byte)100, SaturateCast<ushort, byte>(100));
        Assert.Equal((byte)100, SaturateCast<int, byte>(100));
        Assert.Equal((byte)100, SaturateCast<uint, byte>(100));
        Assert.Equal((byte)100, SaturateCast<float, byte>(100.1f));
        Assert.Equal((byte)100, SaturateCast<double, byte>(100.1));
        Assert.Equal((byte)100, SaturateCast<long, byte>(100L));
        Assert.Equal((byte)100, SaturateCast<ulong, byte>(100UL));

        Assert.Equal(byte.MinValue, SaturateCast<sbyte, byte>(-100));
        Assert.Equal(byte.MinValue, SaturateCast<short, byte>(-100));
        Assert.Equal(byte.MinValue, SaturateCast<int, byte>(-100));
        Assert.Equal(byte.MinValue, SaturateCast<float, byte>(-100.0f));
        Assert.Equal(byte.MinValue, SaturateCast<double, byte>(-100.0));
        Assert.Equal(byte.MinValue, SaturateCast<long, byte>(-100L));

        Assert.Equal(byte.MaxValue, SaturateCast<short, byte>(10000));
        Assert.Equal(byte.MaxValue, SaturateCast<ushort, byte>(10000));
        Assert.Equal(byte.MaxValue, SaturateCast<int, byte>(10000));
        Assert.Equal(byte.MaxValue, SaturateCast<uint, byte>(10000));
        Assert.Equal(byte.MaxValue, SaturateCast<double, byte>(10000.0));
        Assert.Equal(byte.MaxValue, SaturateCast<float, byte>(10000.0f));
        Assert.Equal(byte.MaxValue, SaturateCast<Half, byte>((Half)10000.0));
        Assert.Equal(byte.MaxValue, SaturateCast<long, byte>(10000L));
        Assert.Equal(byte.MaxValue, SaturateCast<ulong, byte>(10000UL));

        Assert.Equal(byte.MaxValue, SaturateCast<Int128, byte>(10000L));
        Assert.Equal(byte.MaxValue, SaturateCast<UInt128, byte>(10000UL));
    }

    [Fact]
    public void ToByte2()
    {
        Assert.Equal((byte)100, SaturateCastFromInteger<sbyte, byte>(100));
        Assert.Equal((byte)100, SaturateCastFromInteger<byte, byte>(100));
        Assert.Equal((byte)100, SaturateCastFromInteger<short, byte>(100));
        Assert.Equal((byte)100, SaturateCastFromInteger<ushort, byte>(100));
        Assert.Equal((byte)100, SaturateCastFromInteger<int, byte>(100));
        Assert.Equal((byte)100, SaturateCastFromInteger<uint, byte>(100));
        Assert.Equal((byte)100, SaturateCastFromInteger<long, byte>(100L));
        Assert.Equal((byte)100, SaturateCastFromInteger<ulong, byte>(100UL));

        Assert.Equal(byte.MinValue, SaturateCastFromInteger<sbyte, byte>(-100));
        Assert.Equal(byte.MinValue, SaturateCastFromInteger<short, byte>(-100));
        Assert.Equal(byte.MinValue, SaturateCastFromInteger<int, byte>(-100));
        Assert.Equal(byte.MinValue, SaturateCastFromInteger<long, byte>(-100L));

        Assert.Equal(byte.MaxValue, SaturateCastFromInteger<short, byte>(10000));
        Assert.Equal(byte.MaxValue, SaturateCastFromInteger<ushort, byte>(10000));
        Assert.Equal(byte.MaxValue, SaturateCastFromInteger<int, byte>(10000));
        Assert.Equal(byte.MaxValue, SaturateCastFromInteger<uint, byte>(10000));
        Assert.Equal(byte.MaxValue, SaturateCastFromInteger<long, byte>(10000L));
        Assert.Equal(byte.MaxValue, SaturateCastFromInteger<ulong, byte>(10000UL));

        Assert.Equal(byte.MaxValue, SaturateCastFromInteger<Int128, byte>(10000L));
        Assert.Equal(byte.MaxValue, SaturateCastFromInteger<UInt128, byte>(10000UL));

        Assert.Equal((byte)100, SaturateCastFromFloat<float, byte>(100.1f));
        Assert.Equal((byte)100, SaturateCastFromFloat<double, byte>(100.1));
        Assert.Equal(byte.MinValue, SaturateCastFromFloat<float, byte>(-100.0f));
        Assert.Equal(byte.MinValue, SaturateCastFromFloat<double, byte>(-100.0));
        Assert.Equal(byte.MaxValue, SaturateCastFromFloat<double, byte>(10000.0));
        Assert.Equal(byte.MaxValue, SaturateCastFromFloat<float, byte>(10000.0f));
        Assert.Equal(byte.MaxValue, SaturateCastFromFloat<Half, byte>((Half)10000.0));
    }

    [Fact]
    public void ToByteWithRounding()
    {
        Assert.Equal((byte)11, SaturateCast<float, byte>(10.9f));
        Assert.Equal((byte)10, SaturateCast<float, byte>(10.4f));
        Assert.Equal((byte)11, SaturateCast<double, byte>(10.9));
        Assert.Equal((byte)10, SaturateCast<double, byte>(10.4));
        Assert.Equal((byte)11, SaturateCast<Half, byte>((Half)10.9));
        Assert.Equal((byte)10, SaturateCast<Half, byte>((Half)10.4));

        Assert.Equal((byte)11, SaturateCastFromFloat<float, byte>(10.9f));
        Assert.Equal((byte)10, SaturateCastFromFloat<float, byte>(10.4f));
        Assert.Equal((byte)11, SaturateCastFromFloat<double, byte>(10.9));
        Assert.Equal((byte)10, SaturateCastFromFloat<double, byte>(10.4));
        Assert.Equal((byte)11, SaturateCastFromFloat<Half, byte>((Half)10.9));
        Assert.Equal((byte)10, SaturateCastFromFloat<Half, byte>((Half)10.4));
    }

    [Fact]
    public void ToShort()
    {
        Assert.Equal((short)100, SaturateCast<sbyte, short>(100));
        Assert.Equal((short)100, SaturateCast<byte, short>(100));
        Assert.Equal((short)100, SaturateCast<short, short>(100));
        Assert.Equal((short)100, SaturateCast<ushort, short>(100));
        Assert.Equal((short)100, SaturateCast<int, short>(100));
        Assert.Equal((short)100, SaturateCast<uint, short>(100));
        Assert.Equal((short)100, SaturateCast<float, short>(100.1f));
        Assert.Equal((short)100, SaturateCast<double, short>(100.1));
        Assert.Equal((short)100, SaturateCast<long, short>(100L));
        Assert.Equal((short)100, SaturateCast<ulong, short>(100UL));

        Assert.Equal((short)-100, SaturateCast<sbyte, short>(-100));
        Assert.Equal((short)-1000, SaturateCast<short, short>(-1000));
        Assert.Equal(short.MinValue, SaturateCast<int, short>(-100000));
        Assert.Equal(short.MinValue, SaturateCast<float, short>(-100000.0f));
        Assert.Equal(short.MinValue, SaturateCast<double, short>(-100000.0));
        Assert.Equal(short.MinValue, SaturateCast<long, short>(-100000L));

        ///*
        Assert.Equal(1000, SaturateCast<short, short>(1000));
        Assert.Equal(short.MaxValue, SaturateCast<ushort, short>(60000));
        Assert.Equal(short.MaxValue, SaturateCast<int, short>(100000));
        Assert.Equal(short.MaxValue, SaturateCast<uint, short>(100000));
        Assert.Equal(short.MaxValue, SaturateCast<double, short>(100000.0));
        Assert.Equal(short.MaxValue, SaturateCast<float, short>(100000.0f));
        Assert.Equal(short.MaxValue, SaturateCast<Half, short>(Half.MaxValue));
        Assert.Equal(short.MaxValue, SaturateCast<long, short>(100000L));
        Assert.Equal(short.MaxValue, SaturateCast<ulong, short>(100000UL));

        Assert.Equal(short.MaxValue, SaturateCast<Int128, short>(100000L));
        Assert.Equal(short.MaxValue, SaturateCast<UInt128, short>(100000UL));
        //*/
    }

    [Fact]
    public void ToFloat()
    {
        Assert.Equal(100f, SaturateCast<int, float>(100), 1e-6);
        Assert.Equal(100f, SaturateCast<byte, float>(100), 1e-6);

        Assert.Equal(1.1f, SaturateCast<float, float>(1.1f), 1e-6);
        Assert.Equal(1.9f, SaturateCast<float, float>(1.9f), 1e-6);

        Assert.Equal(1.1, SaturateCast<float, double>(1.1f), 1e-6);
        Assert.Equal(1.9, SaturateCast<float, double>(1.9f), 1e-6);

        Assert.Equal((Half)1.1f, SaturateCast<float, Half>(1.1f));
        Assert.Equal((Half)1.9f, SaturateCast<float, Half>(1.9f));
    }
    
}
