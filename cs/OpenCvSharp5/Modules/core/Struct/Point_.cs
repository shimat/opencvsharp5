using System.Numerics;
using System.Runtime.InteropServices;

namespace OpenCvSharp5;

[StructLayout(LayoutKind.Sequential)]
internal record struct Point_<T>(T X, T Y)
    where T : IBinaryInteger<T>, IConvertible
{
    public T X = X;
    public T Y = Y;
    
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
}
