namespace OpenCvSharp5.Tests.Core;

public class Rect2fTests
{
    // ReSharper disable once InconsistentNaming
    [Fact]
    public void FromLTRB()
    {
        var rect = Rect2f.FromLTRB(1, 2, 3, 4);
        Assert.Equal(new Rect2f(1, 2, 2, 2), rect);

        Assert.Throws<ArgumentException>(() =>
        {
            Rect2f.FromLTRB(10, 20, 5, 40);
        });
        Assert.Throws<ArgumentException>(() =>
        {
            Rect2f.FromLTRB(10, 20, 30, 5);
        });
    }

    [Fact]
    public void FromLocationSize()
    {
        var rect = new Rect2f(new Point2f(1, 2), new Size2f(3, 4));
        Assert.Equal(1, rect.X);
        Assert.Equal(2, rect.Y);
        Assert.Equal(3, rect.Width);
        Assert.Equal(4, rect.Height);
    }

    [Fact]
    public void Left()
    {
        var rect = new Rect2f(1, 2, 3, 4);
        Assert.Equal(1, rect.Left);
    }
    
    [Fact]
    public void Top()
    {
        var rect = new Rect2f(1, 2, 3, 4);
        Assert.Equal(2, rect.Top);
    }
    
    [Fact]
    public void Right()
    {
        var rect = new Rect2f(1, 2, 3, 4);
        Assert.Equal(4, rect.Right);
    }
    
    [Fact]
    public void Bottom()
    {
        var rect = new Rect2f(1, 2, 3, 4);
        Assert.Equal(6, rect.Bottom);
    }
    
    [Fact]
    public void TopLeft()
    {
        var rect = new Rect2f(1, 2, 3, 4);
        Assert.Equal(new Point(1, 2), rect.TopLeft);
    }
    
    [Fact]
    public void BottomRight()
    {
        var rect = new Rect2f(1, 2, 3, 4);
        Assert.Equal(new Point(4, 6), rect.BottomRight);
    }
    
    [Fact]
    public void Location()
    {
        var rect = new Rect2f(1, 2, 3, 4);
        Assert.Equal(new Point2f(1, 2), rect.Location);

        rect.Location = new Point2f(100, 200);
        Assert.Equal(new Rect2f(100, 200, 3, 4), rect);
    }
    
    [Fact]
    public void Size()
    {
        var rect = new Rect2f(1, 2, 3, 4);
        Assert.Equal(new Size2f(3, 4), rect.Size);

        rect.Size = new Size2f(300, 400);
        Assert.Equal(new Rect2f(1, 2, 300, 400), rect);
    }

    [Fact]
    public void Add()
    {
        var rect = new Rect2f(1, 2, 3, 4);
        Assert.Equal(new Rect2f(2, 4, 3, 4), rect.Add(new Point2f(1, 2)));
        Assert.Equal(new Rect2f(2, 4, 3, 4), rect + new Point2f(1, 2));
        Assert.Equal(new Rect2f(1, 2, 4, 6), rect.Add(new Size2f(1, 2)));
        Assert.Equal(new Rect2f(1, 2, 4, 6), rect + new Size2f(1, 2));
    }

    [Fact]
    public void Subtract()
    {
        var rect = new Rect2f(1, 2, 3, 4);
        Assert.Equal(new Rect2f(0, 0, 3, 4), rect.Subtract(new Point2f(1, 2)));
        Assert.Equal(new Rect2f(0, 0, 3, 4), rect - new Point2f(1, 2));
        Assert.Equal(new Rect2f(1, 2, 2, 2), rect.Subtract(new Size2f(1, 2)));
        Assert.Equal(new Rect2f(1, 2, 2, 2), rect - new Size2f(1, 2));
    }

    [Theory]
    [InlineData(200, 199, false)]
    [InlineData(200, 200, true)]
    [InlineData(200, 300, true)]
    [InlineData(200, 599, true)]
    [InlineData(200, 600, false)]
    public void Contains(int x, int y, bool result)
    {
        var rect = new Rect2f(100, 200, 300, 400);
        
        Assert.Equal(result, rect.Contains(new Point2f(x, y)));
        Assert.Equal(result, rect.Contains(x, y));
    }

    [Fact]
    public void ContainsRect()
    {
        var rect = new Rect2f(100, 200, 300, 400);
        
        Assert.False(rect.Contains(new Rect2f(0, 0, 10, 10)));
        Assert.False(rect.Contains(new Rect2f(0, 0, 200, 300)));
        Assert.True(rect.Contains(new Rect2f(100, 200, 100, 100)));
        Assert.True(rect.Contains(new Rect2f(100, 200, 300, 400)));
        Assert.False(rect.Contains(new Rect2f(100, 200, 301, 401)));
    }

    [Fact]
    public void Inflate()
    {
        var rect = new Rect2f(1, 2, 3, 4);
        rect.Inflate(1, 2);
        Assert.Equal(new Rect2f(0, 0, 5, 8), rect);

        rect.Inflate(new Size2f(10, 20));
        Assert.Equal(new Rect2f(-10, -20, 25, 48), rect);

        Assert.Equal(new Rect2f(0, 0, 5, 8), Rect2f.Inflate(rect, -10, -20));
    }

    [Theory]
    [MemberData(nameof(IntersectTestData))]
    public void Intersect(Rect2f rect1, Rect2f rect2, Rect2f expectedIntersect, bool expectedIntersectsWith)
    {
        Assert.Equal(
            expectedIntersect,
            rect1.Intersect(rect2));
        Assert.Equal(
            expectedIntersect,
            rect1 & rect2);
        Assert.Equal(
            expectedIntersect,
            Rect2f.Intersect(rect1, rect2));
        Assert.Equal(expectedIntersectsWith, rect1.IntersectsWith(rect2));
    }

    public static IEnumerable<object[]> IntersectTestData()
    {
        yield return new object[] { new Rect2f(), new Rect2f(), default(Rect2f), false };
        yield return new object[] { new Rect2f(1, 2, 3, 4), new Rect2f(1, 2, 3, 4), new Rect2f(1, 2, 3, 4), true };
        yield return new object[] { new Rect2f(0, 0, 100, 200), new Rect2f(50, 50, 100, 200), new Rect2f(50, 50, 50, 150), true };
        yield return new object[] { new Rect2f(0, 0, 100, 200), new Rect2f(1000, 1000, 100, 100), default(Rect2f), false };
    }

    [Theory]
    [MemberData(nameof(UnionTestData))]
    public void Union(Rect2f rect1, Rect2f rect2, Rect2f expectedUnion)
    {
        Assert.Equal(
            expectedUnion,
            rect1.Union(rect2));
        Assert.Equal(
            expectedUnion,
            rect1 | rect2);
        Assert.Equal(
            expectedUnion,
            Rect2f.Union(rect1, rect2));
    }

    public static IEnumerable<object[]> UnionTestData()
    {
        yield return new object[] { new Rect2f(), new Rect2f(), default(Rect2f) };
        yield return new object[] { new Rect2f(1, 2, 3, 4), new Rect2f(1, 2, 3, 4), new Rect2f(1, 2, 3, 4) };
        yield return new object[] { new Rect2f(0, 0, 100, 200), new Rect2f(50, 50, 100, 200), new Rect2f(0, 0, 150, 250) };
        yield return new object[] { new Rect2f(0, 0, 100, 200), new Rect2f(1000, 1000, 100, 200), new Rect2f(0, 0, 1100, 1200) };
    }
}
