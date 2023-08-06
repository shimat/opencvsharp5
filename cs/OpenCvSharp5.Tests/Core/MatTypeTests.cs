namespace OpenCvSharp5.Tests.Core;

public class MatTypeTests
{
    [Fact]
    public void ToInt32() 
        => Assert.Equal(MatType.CV_16UC4.ToInt32(), MatType.CV_16UC4.Value);

    [Fact]
    public void FromInt32() 
        => Assert.Equal(MatType.CV_16UC4, MatType.FromInt32(MatType.CV_16UC4.Value));

    [Fact]
    public void Depth()
    {
        Assert.Equal(MatType.CV_8U, MatType.CV_8UC2.Depth);
        Assert.Equal(MatType.CV_8S, MatType.CV_8SC2.Depth);
        Assert.Equal(MatType.CV_16U, MatType.CV_16UC4.Depth);
        Assert.Equal(MatType.CV_16S, MatType.CV_16SC3.Depth);
        Assert.Equal(MatType.CV_32S, MatType.CV_32SC2.Depth);
        Assert.Equal(MatType.CV_32F, MatType.CV_32FC(6).Depth);
        Assert.Equal(MatType.CV_64F, MatType.CV_64FC1.Depth);
    }

    [Fact]
    public void Channels()
    {
        Assert.Equal(2, MatType.CV_8UC2.Channels);
        Assert.Equal(2, MatType.CV_8SC2.Channels);
        Assert.Equal(4, MatType.CV_16UC4.Channels);
        Assert.Equal(3, MatType.CV_16SC3.Channels);
        Assert.Equal(2, MatType.CV_32SC2.Channels);
        Assert.Equal(6, MatType.CV_32FC(6).Channels);
        Assert.Equal(1, MatType.CV_64FC1.Channels);
    }

    [Fact]
    public void IsInteger()
    {
        Assert.True(MatType.CV_8UC2.IsInteger);
        Assert.True(MatType.CV_8SC2.IsInteger);
        Assert.True(MatType.CV_16UC4.IsInteger);
        Assert.True(MatType.CV_16SC3.IsInteger);
        Assert.True(MatType.CV_32SC2.IsInteger);
        Assert.False(MatType.CV_32FC(6).IsInteger);
        Assert.False(MatType.CV_64FC1.IsInteger);
    }

    [Fact]
    public void EqualToInteger()
    {
        Assert.True(MatType.CV_8UC2 == MatType.CV_8UC2.Value);
        Assert.True(MatType.CV_8UC2 != MatType.CV_8UC3.Value);
        Assert.True(MatType.CV_8UC2 != MatType.CV_8SC2.Value);
        
        Assert.True(MatType.CV_8UC2.Equals(MatType.CV_8UC2.Value));
        Assert.False(MatType.CV_8UC2.Equals(MatType.CV_8UC3.Value));
        Assert.False(MatType.CV_8UC2.Equals(MatType.CV_8SC2.Value));
    }

    [Fact]
    public void ToStringTest()
    {
        Assert.Equal("CV_8UC2", MatType.CV_8UC2.ToString());
        Assert.Equal("CV_8SC2", MatType.CV_8SC2.ToString());
        Assert.Equal("CV_16UC4", MatType.CV_16UC4.ToString());
        Assert.Equal("CV_16SC3", MatType.CV_16SC3.ToString());
        Assert.Equal("CV_32SC2", MatType.CV_32SC2.ToString());
        Assert.Equal("CV_32FC(6)", MatType.CV_32FC(6).ToString());
        Assert.Equal("CV_64FC1", MatType.CV_64FC1.ToString());
    }

    [Fact]
    public void MakeType()
    {
        Assert.Equal(MatType.CV_8UC1, MatType.MakeType(MatType.CV_8U, 1));
        Assert.Equal(MatType.CV_8UC2, MatType.MakeType(MatType.CV_8U, 2));
        Assert.Equal(MatType.CV_16UC4, MatType.MakeType(MatType.CV_16U, 4));
        Assert.Equal(MatType.CV_32SC(6), MatType.MakeType(MatType.CV_32S, 6));
        Assert.Equal(MatType.CV_64FC(128), MatType.MakeType(MatType.CV_64F, 128));
        
        Assert.Throws<ArgumentException>(() => MatType.MakeType(-1, 1));
        Assert.Throws<ArgumentException>(() => MatType.MakeType(8, 1));

        Assert.Throws<ArgumentException>(() => MatType.MakeType(MatType.CV_32F, 0));
        Assert.Throws<ArgumentException>(() => MatType.MakeType(MatType.CV_32F, 512));
    }
}
