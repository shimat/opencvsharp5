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

    [Fact]
    public void IsValid()
    {
        var criteria1 = new TermCriteria(10, 0.1);
        Assert.True(criteria1.IsValid);

        var criteria2 = new TermCriteria(0, 0.1);
        Assert.True(criteria2.IsValid);
        
        var criteria3 = new TermCriteria(0, double.NaN);
        Assert.False(criteria3.IsValid);
    }
}
