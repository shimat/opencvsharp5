﻿using System.Runtime.InteropServices;

#pragma warning disable CA1051

namespace OpenCvSharp5;

/// <summary>
/// 3-Tuple of double (System.Double)
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public struct Vec3d : IVec<Vec3d, double>, IEquatable<Vec3d>
{
    /// <summary>
    /// The value of the first component of this object.
    /// </summary>
    public double Item0;

    /// <summary>
    /// The value of the second component of this object.
    /// </summary>
    public double Item1;

    /// <summary>
    /// The value of the third component of this object.
    /// </summary>
    public double Item2;

    /// <summary>
    /// Deconstructing a Vector
    /// </summary>
    /// <param name="item0"></param>
    /// <param name="item1"></param>
    /// <param name="item2"></param>
    public readonly void Deconstruct(out double item0, out double item1, out double item2) 
        => (item0, item1, item2) = (Item0, Item1, Item2);

    /// <summary>
    /// Initializer
    /// </summary>
    /// <param name="item0"></param>
    /// <param name="item1"></param>
    /// <param name="item2"></param>
    public Vec3d(double item0, double item1, double item2)
    {
        Item0 = item0;
        Item1 = item1;
        Item2 = item2;
    }

    #region Operators

    /// <summary>
    /// this + other
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public readonly Vec3d Add(Vec3d other) => new(
        Item0 + other.Item0,
        Item1 + other.Item1,
        Item2 + other.Item2);

    /// <summary>
    /// this - other
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public readonly Vec3d Subtract(Vec3d other) => new(
        Item0 - other.Item0,
        Item1 - other.Item1,
        Item2 - other.Item2);

    /// <summary>
    /// this * alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public readonly Vec3d Multiply(double alpha) => new(
        Item0 * alpha,
        Item1 * alpha,
        Item2 * alpha);

    /// <summary>
    /// this / alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public readonly Vec3d Divide(double alpha) => new(
        Item0 / alpha,
        Item1 / alpha,
        Item2 / alpha);

#pragma warning disable 1591
    public static Vec3d operator +(Vec3d a, Vec3d b) => a.Add(b);
    public static Vec3d operator -(Vec3d a, Vec3d b) => a.Subtract(b);
    public static Vec3d operator *(Vec3d a, double alpha) => a.Multiply(alpha);
    public static Vec3d operator /(Vec3d a, double alpha) => a.Divide(alpha);
#pragma warning restore 1591

    /// <summary>
    /// Indexer
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public double this[int i]
    {
        readonly get =>
            i switch
            {
                0 => Item0,
                1 => Item1,
                2 => Item2,
                _ => throw new ArgumentOutOfRangeException(nameof(i))
            };
        set
        {
            switch (i)
            {
                case 0: Item0 = value; break;
                case 1: Item1 = value; break;
                case 2: Item2 = value; break;
                default: throw new ArgumentOutOfRangeException(nameof(i));
            }
        }
    }

    #endregion

    /// <inheritdoc />
    public readonly bool Equals(Vec3d other) => Item0.Equals(other.Item0) && Item1.Equals(other.Item1) && Item2.Equals(other.Item2);

    /// <inheritdoc />
    public readonly override bool Equals(object? obj)
    {
        if (obj is null) return false;
        return obj is Vec3d v && Equals(v);
    }

    /// <summary> 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator ==(Vec3d a, Vec3d b) => a.Equals(b);

    /// <summary> 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator !=(Vec3d a, Vec3d b) => !(a == b);

    /// <inheritdoc />
    public readonly override int GetHashCode()
    {
#if DOTNET_FRAMEWORK || NETSTANDARD2_0
            unchecked
            {
                var hashCode = Item0.GetHashCode();
                hashCode = (hashCode * 397) ^ Item1.GetHashCode();
                hashCode = (hashCode * 397) ^ Item2.GetHashCode();
                return hashCode;
            }
#else
        return HashCode.Combine(Item0, Item1, Item2);
#endif
    }

    /// <inheritdoc />
    public readonly override string ToString() => $"{nameof(Vec3d)} ({Item0}, {Item1}, {Item2})";
}
