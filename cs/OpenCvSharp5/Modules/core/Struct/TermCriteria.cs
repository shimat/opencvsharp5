#pragma warning disable CA1051

namespace OpenCvSharp5;

/// <summary>
/// The class defining termination criteria for iterative algorithms.
/// </summary>
public readonly record struct TermCriteria(CriteriaTypes Type, int MaxCount, double Epsilon)
{
    /// <summary>
    /// the type of termination criteria: COUNT, EPS or COUNT + EPS
    /// </summary>
    public readonly CriteriaTypes Type = Type;

    /// <summary>
    /// the maximum number of iterations/elements
    /// </summary>
    public readonly int MaxCount = MaxCount;

    /// <summary>
    /// the desired accuracy
    /// </summary>
    public readonly double Epsilon = Epsilon;

    /// <summary>
    /// Default constructor
    /// </summary>
    public TermCriteria()
        : this(CriteriaTypes.Count, default, default)
    {
    }

    /// <summary>
    /// full constructor with both type (count | epsilon)
    /// </summary>
    /// <param name="maxCount"></param>
    /// <param name="epsilon"></param>
    public TermCriteria(int maxCount, double epsilon)
        : this(Type: CriteriaTypes.Count | CriteriaTypes.Eps,
            MaxCount: maxCount,
            Epsilon: epsilon)
    { 
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public bool IsValid
    {
        get
        {
            var isCount = Type.HasFlag(CriteriaTypes.Count) && MaxCount > 0;
            var isEps = Type.HasFlag(CriteriaTypes.Eps) && !double.IsNaN(Epsilon);
            return isCount || isEps;
        }
    }
}
