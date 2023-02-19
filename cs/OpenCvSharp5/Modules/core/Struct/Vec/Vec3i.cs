﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using OpenCvSharp5.Internal;

namespace OpenCvSharp5;

/// <summary>
/// 3-Tuple of int (System.Int32)
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage("Design", "CA1051: Do not declare visible instance fields")]
// ReSharper disable once InconsistentNaming
public struct Vec3i : IVec<Vec3i, int>, IEquatable<Vec3i>
{
    /// <summary>
    /// The value of the first component of this object.
    /// </summary>
    public int Item0;

    /// <summary>
    /// The value of the second component of this object.
    /// </summary>
    public int Item1;

    /// <summary>
    /// The value of the third component of this object.
    /// </summary>
    public int Item2;

    /// <summary>
    /// Deconstructing a Vector
    /// </summary>
    /// <param name="item0"></param>
    /// <param name="item1"></param>
    /// <param name="item2"></param>
    public readonly void Deconstruct(out int item0, out int item1, out int item2) 
        => (item0, item1, item2) = (Item0, Item1, Item2);

    /// <summary>
    /// Initializer
    /// </summary>
    /// <param name="item0"></param>
    /// <param name="item1"></param>
    /// <param name="item2"></param>
    public Vec3i(int item0, int item1, int item2)
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
    public readonly Vec3i Add(Vec3i other) => new(
        SaturateCast.ToInt32(Item0 + other.Item0),
        SaturateCast.ToInt32(Item1 + other.Item1),
        SaturateCast.ToInt32(Item2 + other.Item2));

    /// <summary>
    /// this - other
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public readonly Vec3i Subtract(Vec3i other) => new(
        SaturateCast.ToInt32(Item0 - other.Item0),
        SaturateCast.ToInt32(Item1 - other.Item1),
        SaturateCast.ToInt32(Item2 - other.Item2));

    /// <summary>
    /// this * alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public readonly Vec3i Multiply(double alpha) => new(
        SaturateCast.ToInt32(Item0 * alpha),
        SaturateCast.ToInt32(Item1 * alpha),
        SaturateCast.ToInt32(Item2 * alpha));

    /// <summary>
    /// this / alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public readonly Vec3i Divide(double alpha) => new(
        SaturateCast.ToInt32(Item0 / alpha),
        SaturateCast.ToInt32(Item1 / alpha),
        SaturateCast.ToInt32(Item2 / alpha));

#pragma warning disable 1591
    public static Vec3i operator +(Vec3i a, Vec3i b) => a.Add(b);
    public static Vec3i operator -(Vec3i a, Vec3i b) => a.Subtract(b);
    public static Vec3i operator *(Vec3i a, double alpha) => a.Multiply(alpha);
    public static Vec3i operator /(Vec3i a, double alpha) => a.Divide(alpha);
#pragma warning restore 1591

    /// <summary>
    /// Indexer
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public int this[int i]
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

#pragma warning disable 1591
    // ReSharper disable InconsistentNaming
    public Vec3f ToVec3f() => new(Item0, Item1, Item2);
    public Vec3d ToVec3d() => new(Item0, Item1, Item2);
    // ReSharper restore InconsistentNaming
#pragma warning restore 1591

    /// <inheritdoc />
    public readonly bool Equals(Vec3i other) =>
        Item0 == other.Item0 &&
        Item1 == other.Item1 &&
        Item2 == other.Item2;

    /// <inheritdoc />
    public readonly override bool Equals(object? obj)
    {
        if (obj is null) return false;
        return obj is Vec3i v && Equals(v);
    }

    /// <summary> 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator ==(Vec3i a, Vec3i b) => a.Equals(b);

    /// <summary> 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator !=(Vec3i a, Vec3i b) => !a.Equals(b);

    /// <inheritdoc />
    public readonly override int GetHashCode()
    {
#if DOTNET_FRAMEWORK || NETSTANDARD2_0
            unchecked
            {
                var hashCode = Item0;
                hashCode = (hashCode * 397) ^ Item1;
                hashCode = (hashCode * 397) ^ Item2;
                return hashCode;
            }
#else
        return HashCode.Combine(Item0, Item1, Item2);
#endif
    }

    /// <inheritdoc />
    public readonly override string ToString() => $"{nameof(Vec3i)} ({Item0}, {Item1}, {Item2})";
}
