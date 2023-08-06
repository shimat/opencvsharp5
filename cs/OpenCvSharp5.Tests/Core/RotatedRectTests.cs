namespace OpenCvSharp5.Tests.Core;

public class RotatedRectTests
{
    [Fact]
    public void New()
    {
        var rr = new RotatedRect(new Point2f(1, 2), new Size2f(3, 4), 5);
        Assert.Equal(new Point2f(1, 2), rr.Center);
        Assert.Equal(new Size2f(3, 4), rr.Size);
        Assert.Equal(5, rr.Angle);
    }

    [Fact]
    public void Points()
    {
        var rr = new RotatedRect(new Point2f(1, 2), new Size2f(3, 4), 0);
        var points = rr.Points();
        Assert.Equal(new Point2f[]
        {
            new(-0.5f, 4),
            new(-0.5f, 0),
            new(2.5f, 0),
            new(2.5f, 4),
        }, points);
    }

    [Fact]
    public void BoundingRect()
    {
        var rr = new RotatedRect(new Point2f(1, 2), new Size2f(3, 4), 0);
        var br = rr.BoundingRect();
        Assert.Equal(new Rect(-1, 0, 5, 5), br);
    }
}
