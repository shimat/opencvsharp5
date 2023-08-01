namespace OpenCvSharp5.Tests.Core;

public class RectTests
{
    // ReSharper disable once InconsistentNaming
    [Fact]
    public void FromLTRB()
    {
        var rect = Rect.FromLTRB(1, 2, 3, 4);
        Assert.Equal(1, rect.X);
        Assert.Equal(2, rect.Y);
        Assert.Equal(2, rect.Width);
        Assert.Equal(2, rect.Height);
    }
    
    [Fact]
    public void Left()
    {
        var rect = new Rect(1, 2, 3, 4);
        Assert.Equal(1, rect.Left);
    }
    
    [Fact]
    public void Top()
    {
        var rect = new Rect(1, 2, 3, 4);
        Assert.Equal(2, rect.Top);
    }
    
    [Fact]
    public void Right()
    {
        var rect = new Rect(1, 2, 3, 4);
        Assert.Equal(4, rect.Right);
    }
    
    [Fact]
    public void Bottom()
    {
        var rect = new Rect(1, 2, 3, 4);
        Assert.Equal(6, rect.Bottom);
    }
    
    [Fact]
    public void TopLeft()
    {
        var rect = new Rect(1, 2, 3, 4);
        Assert.Equal(new Point(1, 2), rect.TopLeft);
    }
    
    [Fact]
    public void BottomRight()
    {
        var rect = new Rect(1, 2, 3, 4);
        Assert.Equal(new Point(4, 6), rect.BottomRight);
    }
    
    [Fact]
    public void Location()
    {
        var rect = new Rect(1, 2, 3, 4);
        Assert.Equal(new Point(1, 2), rect.Location);

        rect.Location = new Point(100, 200);
        Assert.Equal(new Rect(100, 200, 3, 4), rect);
    }
    
    [Fact]
    public void Size()
    {
        var rect = new Rect(1, 2, 3, 4);
        Assert.Equal(new Size(3, 4), rect.Size);

        rect.Size = new Size(300, 400);
        Assert.Equal(new Rect(1, 2, 300, 400), rect);
    }

    [Fact]
    public void Add()
    {
        var rect = new Rect(1, 2, 3, 4);
        Assert.Equal(new Rect(2, 4, 3, 4), rect.Add(new Point(1, 2)));
        Assert.Equal(new Rect(2, 4, 3, 4), rect + new Point(1, 2));
        Assert.Equal(new Rect(1, 2, 4, 6), rect.Add(new Size(1, 2)));
        Assert.Equal(new Rect(1, 2, 4, 6), rect + new Size(1, 2));
    }

    [Fact]
    public void Subtract()
    {
        var rect = new Rect(1, 2, 3, 4);
        Assert.Equal(new Rect(0, 0, 3, 4), rect.Subtract(new Point(1, 2)));
        Assert.Equal(new Rect(0, 0, 3, 4), rect - new Point(1, 2));
        Assert.Equal(new Rect(1, 2, 2, 2), rect.Subtract(new Size(1, 2)));
        Assert.Equal(new Rect(1, 2, 2, 2), rect - new Size(1, 2));
    }

    [Theory]
    [InlineData(200, 199, false)]
    [InlineData(200, 200, true)]
    [InlineData(200, 300, true)]
    [InlineData(200, 599, true)]
    [InlineData(200, 600, false)]
    public void Contains(int x, int y, bool result)
    {
        var rect = new Rect(100, 200, 300, 400);
        
        Assert.Equal(result, rect.Contains(new Point(x, y)));
        Assert.Equal(result, rect.Contains(x, y));
    }

    [Fact]
    public void ContainsRect()
    {
        var rect = new Rect(100, 200, 300, 400);
        
        Assert.False(rect.Contains(new Rect(0, 0, 10, 10)));
        Assert.False(rect.Contains(new Rect(0, 0, 200, 300)));
        Assert.True(rect.Contains(new Rect(100, 200, 100, 100)));
        Assert.True(rect.Contains(new Rect(100, 200, 300, 400)));
        Assert.False(rect.Contains(new Rect(100, 200, 301, 401)));
    }

    [Fact]
    public void Inflate()
    {
        var rect = new Rect(1, 2, 3, 4);
        rect.Inflate(1, 2);
        Assert.Equal(new Rect(0, 0, 5, 8), rect);

        rect.Inflate(new Size(10, 20));
        Assert.Equal(new Rect(-10, -20, 25, 48), rect);

        Assert.Equal(new Rect(0, 0, 5, 8), Rect.Inflate(rect, -10, -20));
    }

    [Theory]
    [MemberData(nameof(IntersectTestData))]
    public void Intersect(Rect rect1, Rect rect2, Rect expectedIntersect, bool expectedIntersectsWith)
    {
        Assert.Equal(
            expectedIntersect,
            rect1.Intersect(rect2));
        Assert.Equal(
            expectedIntersect,
            rect1 & rect2);
        Assert.Equal(
            expectedIntersect,
            Rect.Intersect(rect1, rect2));
        Assert.Equal(expectedIntersectsWith, rect1.IntersectsWith(rect2));
    }

    public static IEnumerable<object[]> IntersectTestData()
    {
        yield return new object[] { new Rect(), new Rect(), default(Rect), false };
        yield return new object[] { new Rect(1, 2, 3, 4), new Rect(1, 2, 3, 4), new Rect(1, 2, 3, 4), true };
        yield return new object[] { new Rect(0, 0, 100, 200), new Rect(50, 50, 100, 200), new Rect(50, 50, 50, 150), true };
        yield return new object[] { new Rect(0, 0, 100, 200), new Rect(1000, 1000, 100, 100), default(Rect), false };
    }

    [Theory]
    [MemberData(nameof(UnionTestData))]
    public void Union(Rect rect1, Rect rect2, Rect expectedUnion)
    {
        Assert.Equal(
            expectedUnion,
            rect1.Union(rect2));
        Assert.Equal(
            expectedUnion,
            rect1 | rect2);
        Assert.Equal(
            expectedUnion,
            Rect.Union(rect1, rect2));
    }

    public static IEnumerable<object[]> UnionTestData()
    {
        yield return new object[] { new Rect(), new Rect(), default(Rect) };
        yield return new object[] { new Rect(1, 2, 3, 4), new Rect(1, 2, 3, 4), new Rect(1, 2, 3, 4) };
        yield return new object[] { new Rect(0, 0, 100, 200), new Rect(50, 50, 100, 200), new Rect(0, 0, 150, 250) };
        yield return new object[] { new Rect(0, 0, 100, 200), new Rect(1000, 1000, 100, 200), new Rect(0, 0, 1100, 1200) };
    }
}
