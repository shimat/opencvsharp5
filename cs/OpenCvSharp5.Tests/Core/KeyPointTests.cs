namespace OpenCvSharp5.Tests.Core;

public class KeyPointTests
{
    [Fact]
    public void New1()
    {
        var k = new KeyPoint(
            Pt: new Point2f(1, 2), 
            Size: 3f,
            Response: 4f,
            Angle: 5f,
            Octave: 6, 
            ClassId: 7);
        Assert.Equal(new Point2f(1, 2), k.Pt);
        Assert.Equal(3, k.Size);
        Assert.Equal(4, k.Response);
        Assert.Equal(5, k.Angle);
        Assert.Equal(6, k.Octave);
        Assert.Equal(7, k.ClassId);
    }

    [Fact]
    public void New2()
    {
        var k = new KeyPoint(
            x: 1f,
            y: 2f,
            size: 3f,
            response: 4f,
            angle: 5f,
            octave: 6,
            classId: 7);
        Assert.Equal(new Point2f(1, 2), k.Pt);
        Assert.Equal(3, k.Size);
        Assert.Equal(4, k.Response);
        Assert.Equal(5, k.Angle);
        Assert.Equal(6, k.Octave);
        Assert.Equal(7, k.ClassId);
    }
}
