﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using OpenCvSharp5.Internal;

namespace OpenCvSharp5;

/// <summary>
/// 4-Tuple of int (System.Int32)
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage("Design", "CA1051: Do not declare visible instance fields")]
// ReSharper disable once InconsistentNaming
public struct Vec4i : IVec<Vec4i, int>, IEquatable<Vec4i>
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
    /// The value of the fourth component of this object.
    /// </summary>
    public int Item3;

    /// <summary>
    /// Deconstructing a Vector
    /// </summary>
    /// <param name="item0"></param>
    /// <param name="item1"></param>
    /// <param name="item2"></param>
    /// <param name="item3"></param>
    public readonly void Deconstruct(out int item0, out int item1, out int item2, out int item3)
        => (item0, item1, item2, item3) = (Item0, Item1, Item2, Item3);

    /// <summary>
    /// Initializer
    /// </summary>
    /// <param name="item0"></param>
    /// <param name="item1"></param>
    /// <param name="item2"></param>
    /// <param name="item3"></param>
    public Vec4i(int item0, int item1, int item2, int item3)
    {
        Item0 = item0;
        Item1 = item1;
        Item2 = item2;
        Item3 = item3;
    }

    #region Operators

    /// <summary>
    /// this + other
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public readonly Vec4i Add(Vec4i other) => new(
        SaturateCast.ToInt32(Item0 + other.Item0),
        SaturateCast.ToInt32(Item1 + other.Item1),
        SaturateCast.ToInt32(Item2 + other.Item2),
        SaturateCast.ToInt32(Item3 + other.Item3));

    /// <summary>
    /// this - other
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public readonly Vec4i Subtract(Vec4i other) => new(
        SaturateCast.ToInt32(Item0 - other.Item0),
        SaturateCast.ToInt32(Item1 - other.Item1),
        SaturateCast.ToInt32(Item2 - other.Item2),
        SaturateCast.ToInt32(Item3 - other.Item3));

    /// <summary>
    /// this * alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public readonly Vec4i Multiply(double alpha) => new(
        SaturateCast.ToInt32(Item0 * alpha),
        SaturateCast.ToInt32(Item1 * alpha),
        SaturateCast.ToInt32(Item2 * alpha),
        SaturateCast.ToInt32(Item3 * alpha));

    /// <summary>
    /// this / alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public readonly Vec4i Divide(double alpha) => new(
        SaturateCast.ToInt32(Item0 / alpha),
        SaturateCast.ToInt32(Item1 / alpha),
        SaturateCast.ToInt32(Item2 / alpha),
        SaturateCast.ToInt32(Item3 / alpha));

#pragma warning disable 1591
    public static Vec4i operator +(Vec4i a, Vec4i b) => a.Add(b);
    public static Vec4i operator -(Vec4i a, Vec4i b) => a.Subtract(b);
    public static Vec4i operator *(Vec4i a, double alpha) => a.Multiply(alpha);
    public static Vec4i operator /(Vec4i a, double alpha) => a.Divide(alpha);
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
                3 => Item3,
                _ => throw new ArgumentOutOfRangeException(nameof(i))
            };
        set
        {
            switch (i)
            {
                case 0: Item0 = value; break;
                case 1: Item1 = value; break;
                case 2: Item2 = value; break;
                case 3: Item3 = value; break;
                default: throw new ArgumentOutOfRangeException(nameof(i));
            }
        }
    }

    #endregion

#pragma warning disable 1591
    // ReSharper disable InconsistentNaming
    public Vec4f ToVec4f() => new(Item0, Item1, Item2, Item3);
    public Vec4d ToVec4d() => new(Item0, Item1, Item2, Item3);
    // ReSharper restore InconsistentNaming
#pragma warning restore 1591

    /// <inheritdoc />
    public readonly bool Equals(Vec4i other) =>
        Item0 == other.Item0 &&
        Item1 == other.Item1 &&
        Item2 == other.Item2 && 
        Item3 == other.Item3;

    /// <inheritdoc />
    public readonly override bool Equals(object? obj)
    {
        if (obj is null) return false;
        return obj is Vec4i v && Equals(v);
    }

    /// <summary> 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator ==(Vec4i a, Vec4i b) => a.Equals(b);

    /// <summary> 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator !=(Vec4i a, Vec4i b) => !a.Equals(b);

    /// <inheritdoc />
    public readonly override int GetHashCode() => HashCode.Combine(Item0, Item1, Item2, Item3);

    /// <inheritdoc />
    public readonly override string ToString() => $"{nameof(Vec4i)} ({Item0}, {Item1}, {Item2}, {Item3})";
}
