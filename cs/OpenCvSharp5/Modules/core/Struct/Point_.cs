using System.Numerics;
using System.Runtime.InteropServices;

namespace OpenCvSharp5;

[StructLayout(LayoutKind.Sequential)]
internal struct Point_<T> : IEquatable<Point_<T>>
    where T : IBinaryInteger<T>, IConvertible
{
    public T X;
    public T Y;

    public Point_(T x, T y)
    {
        X = x;
        Y = y;
    }
    
    /// <summary>
    /// Returns the distance between the specified two points
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public readonly double DistanceTo(Point_<T> p)
    {
        T xDiff = p.X - X;
        T yDiff = p.Y - Y;
        T sqDist = xDiff * xDiff + yDiff * yDiff;
        return Math.Sqrt(sqDist.ToDouble(null));
    }

    /// <summary>
    /// Calculates the dot product of two 2D vectors.
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public readonly T DotProduct(Point_<T> p) => X * p.X + Y * p.Y;

    /// <summary>
    /// Calculates the cross product of two 2D vectors.
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public readonly T CrossProduct(Point_<T> p) => X * p.Y - X * p.Y;

    /// <inheritdoc />
    public bool Equals(Point_<T> other) 
        => EqualityComparer<T>.Default.Equals(X, other.X) && 
           EqualityComparer<T>.Default.Equals(Y, other.Y);

    /// <inheritdoc />
    public override bool Equals(object? obj) 
        => obj is Point_<T> other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() 
        => HashCode.Combine(X, Y);

    /// <inheritdoc />
    public readonly override string ToString() => $"(x:{X} y:{Y})";
}
