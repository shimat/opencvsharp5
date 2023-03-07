using System.Runtime.InteropServices;

namespace OpenCvSharp5;

/// <summary>
/// Template class specifying a continuous subsequence (slice) of a sequence.
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Range : IEquatable<Range>
{
    /// <summary> 
    /// </summary>
    public readonly int Start;

    /// <summary> 
    /// </summary>
    public readonly int End;

    /// <summary>
    /// Constructor
    /// </summary>
    public Range(int start, int end)
    {
        Start = start;
        End = end;
    }

    /// <summary>
    /// 
    /// </summary>
    public static Range All => new (int.MinValue, int.MaxValue);
 
    /// <inheritdoc />
    public bool Equals(Range other) => Start == other.Start && End == other.End;

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Range other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(Start, End);

    /// <summary> 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(Range left, Range right) => left.Equals(right);

    /// <summary>
    ///  </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(Range left, Range right) => !(left == right);
}
