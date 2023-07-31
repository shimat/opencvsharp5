using System.Runtime.InteropServices;

namespace OpenCvSharp5;

/// <summary>
/// 
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public record struct Point(int X, int Y)
{
    /// <summary>
    /// 
    /// </summary>
    public int X = X;

    /// <summary>
    /// 
    /// </summary>
    public int Y = Y;
        
    #region Cast
        
#pragma warning disable 1591

    // ReSharper disable once InconsistentNaming
    public readonly Vec2<int> ToVec2i() => new(X, Y);

    public static implicit operator Vec2<int>(Point point) => point.ToVec2i();

    // ReSharper disable once InconsistentNaming
    public static Point FromVec2i(Vec2<int> vec) => new(vec.Item1, vec.Item2);

    public static implicit operator Point(Vec2<int> vec) => FromVec2i(vec);

#pragma warning restore 1591

    #endregion

    #region Operators
   
    /// <summary>
    /// Unary plus operator
    /// </summary>
    /// <returns></returns>
    public readonly Point Plus() => this;

    /// <summary>
    /// Unary plus operator
    /// </summary>
    /// <param name="pt"></param>
    /// <returns></returns>
    public static Point operator +(Point pt) => pt;

    /// <summary>
    /// Unary minus operator
    /// </summary>
    /// <returns></returns>
    public readonly Point Negate() => new(-X, -Y);

    /// <summary>
    /// Unary minus operator
    /// </summary>
    /// <param name="pt"></param>
    /// <returns></returns>
    public static Point operator -(Point pt) => pt.Negate();

    /// <summary>
    /// Shifts point by a certain offset
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public readonly Point Add(Point p) => new(X + p.X, Y + p.Y);

    /// <summary>
    /// Shifts point by a certain offset
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static Point operator +(Point p1, Point p2) => p1.Add(p2);

    /// <summary>
    /// Shifts point by a certain offset
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public readonly Point Subtract(Point p) => new(X - p.X, Y - p.Y);

    /// <summary>
    /// Shifts point by a certain offset
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static Point operator -(Point p1, Point p2) => p1.Subtract(p2);

    /// <summary>
    /// Shifts point by a certain offset
    /// </summary>
    /// <param name="scale"></param>
    /// <returns></returns>
    public readonly Point Multiply(double scale) => new((int)(X * scale), (int)(Y * scale));

    /// <summary>
    /// Shifts point by a certain offset
    /// </summary>
    /// <param name="pt"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static Point operator *(Point pt, double scale) => pt.Multiply(scale);

    #endregion

    #region Methods

    /// <summary>
    /// Returns the distance between the specified two points
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static double Distance(Point p1, Point p2) => Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));

    /// <summary>
    /// Returns the distance between the specified two points
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public readonly double DistanceTo(Point p) => Distance(this, p);

    /// <summary>
    /// Calculates the dot product of two 2D vectors.
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static double DotProduct(Point p1, Point p2) => p1.X*p2.X + p1.Y*p2.Y;

    /// <summary>
    /// Calculates the dot product of two 2D vectors.
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public readonly double DotProduct(Point p) => DotProduct(this, p);

    /// <summary>
    /// Calculates the cross product of two 2D vectors.
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static double CrossProduct(Point p1, Point p2) => p1.X*p2.Y - p2.X*p1.Y;

    /// <summary>
    /// Calculates the cross product of two 2D vectors.
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public readonly double CrossProduct(Point p) => CrossProduct(this, p);

    #endregion
}
