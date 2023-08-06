namespace OpenCvSharp5.Tests.Core;

public class TermCriteriaTests
{
    [Fact]
    public void Default()
    {
        var criteria = new TermCriteria();
        Assert.Equal(CriteriaTypes.Count, criteria.Type);
        Assert.Equal(0, criteria.MaxCount);
        Assert.Equal(0, criteria.Epsilon);
    }

    [Fact]
    public void Full()
    {
        var criteria = new TermCriteria(CriteriaTypes.Count | CriteriaTypes.Eps, 10, 0.1);
        Assert.Equal(CriteriaTypes.Count | CriteriaTypes.Eps, criteria.Type);
        Assert.Equal(10, criteria.MaxCount);
        Assert.Equal(0.1, criteria.Epsilon);
    }

    [Fact]
    public void Both()
    {
        var criteria = new TermCriteria(10, 0.1);
        Assert.Equal(CriteriaTypes.Count | CriteriaTypes.Eps, criteria.Type);
        Assert.Equal(10, criteria.MaxCount);
        Assert.Equal(0.1, criteria.Epsilon);
    }

    [Theory]
    [InlineData(10, 0.1, true)]
    [InlineData(0, 0.1, true)]
    [InlineData(10, double.NaN, true)]
    [InlineData(0, double.NaN, false)]
    public void IsValid(int maxCount, double eps, bool isValid)
    {
        var criteria = new TermCriteria(maxCount, eps);
        Assert.Equal(isValid, criteria.IsValid);
    }
}
