namespace OpenCvSharp5;

#pragma warning disable CA1051

/// <summary>
/// Struct for matching: query descriptor index, train descriptor index, train image index and distance between descriptors.
/// </summary>
/// <param name="QueryIdx"> query descriptor index</param>
/// <param name="TrainIdx">train descriptor index</param>
/// <param name="ImgIdx">train image index</param>
/// <param name="Distance"></param>
public readonly record struct DMatch(int QueryIdx, int TrainIdx, int ImgIdx, float Distance) : IComparable<DMatch>
{
    /// <summary>
    /// query descriptor index
    /// </summary>
    public readonly int QueryIdx = QueryIdx; 

    /// <summary>
    /// train descriptor index
    /// </summary>
    public readonly int TrainIdx = TrainIdx; 
    
    /// <summary>
    /// train image index
    /// </summary>
    public readonly int ImgIdx = ImgIdx; 

    /// <summary>
    /// 
    /// </summary>
    public readonly float Distance = Distance;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static DMatch Empty() => new (-1, -1, -1, float.MaxValue);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="queryIdx"></param>
    /// <param name="trainIdx"></param>
    /// <param name="distance"></param>
    public DMatch(int queryIdx, int trainIdx, float distance) :
        this(queryIdx, trainIdx, -1, distance)
    {
    }
    
    /// <summary>
    /// Compares by distance (less is better)
    /// </summary>
    /// <param name="d1"></param>
    /// <param name="d2"></param>
    /// <returns></returns>
    public static bool operator <(DMatch d1, DMatch d2) => d1.Distance < d2.Distance;
    
    /// <summary>
    /// Compares by distance (less is better)
    /// </summary>
    /// <param name="d1"></param>
    /// <param name="d2"></param>
    /// <returns></returns>
    public static bool operator <=(DMatch d1, DMatch d2) => d1.Distance <= d2.Distance;

    /// <summary>
    /// Compares by distance (less is better)
    /// </summary>
    /// <param name="d1"></param>
    /// <param name="d2"></param>
    /// <returns></returns>
    public static bool operator >(DMatch d1, DMatch d2) => d1.Distance > d2.Distance;
    
    /// <summary>
    /// Compares by distance (less is better)
    /// </summary>
    /// <param name="d1"></param>
    /// <param name="d2"></param>
    /// <returns></returns>
    public static bool operator >=(DMatch d1, DMatch d2) => d1.Distance >= d2.Distance;

    /// <summary>
    /// Compares by distance (less is better)
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CompareTo(DMatch other) => Distance.CompareTo(other.Distance);

#pragma warning disable 1591

    public static explicit operator Vec4f(DMatch self) => self.ToVec4f();

    // ReSharper disable once InconsistentNaming
    public Vec4f ToVec4f() => new(QueryIdx, TrainIdx, ImgIdx, Distance);

    public static explicit operator DMatch(Vec4f v) => FromVec4f(v);

    // ReSharper disable once InconsistentNaming
    public static DMatch FromVec4f(Vec4f v) => new ((int)v.Item0, (int)v.Item1, (int)v.Item2, v.Item3);

#pragma warning restore 1591
}
