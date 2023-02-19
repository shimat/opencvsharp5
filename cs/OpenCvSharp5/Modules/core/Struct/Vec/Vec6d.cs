﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace OpenCvSharp5;

/// <summary>
/// 6-Tuple of double (System.Double)
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage("Design", "CA1051: Do not declare visible instance fields")]
public struct Vec6d : IVec<Vec6d, double>, IEquatable<Vec6d>
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
    /// The value of the fourth component of this object.
    /// </summary>
    public double Item3;

    /// <summary>
    /// The value of the fifth component of this object.
    /// </summary>
    public double Item4;

    /// <summary>
    /// The value of the sixth component of this object.
    /// </summary>
    public double Item5;

    /// <summary>
    /// Deconstructing a Vector
    /// </summary>
    /// <param name="item0"></param>
    /// <param name="item1"></param>
    /// <param name="item2"></param>
    /// <param name="item3"></param>
    /// <param name="item4"></param>
    /// <param name="item5"></param>
    public readonly void Deconstruct(out double item0, out double item1, out double item2, out double item3, out double item4, out double item5) 
        => (item0, item1, item2, item3, item4, item5) = (Item0, Item1, Item2, Item3, Item4, Item5);

    /// <summary>
    /// Initializer
    /// </summary>
    /// <param name="item0"></param>
    /// <param name="item1"></param>
    /// <param name="item2"></param>
    /// <param name="item3"></param>
    /// <param name="item4"></param>
    /// <param name="item5"></param>
    public Vec6d(double item0, double item1, double item2, double item3, double item4, double item5)
    {
        Item0 = item0;
        Item1 = item1;
        Item2 = item2;
        Item3 = item3;
        Item4 = item4; 
        Item5 = item5;
    }

    #region Operators

    /// <summary>
    /// this + other
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public readonly Vec6d Add(Vec6d other) => new(
        Item0 + other.Item0,
        Item1 + other.Item1,
        Item2 + other.Item2,
        Item3 + other.Item3,
        Item4 + other.Item4,
        Item5 + other.Item5);

    /// <summary>
    /// this - other
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public readonly Vec6d Subtract(Vec6d other) => new(
        Item0 - other.Item0,
        Item1 - other.Item1,
        Item2 - other.Item2,
        Item3 - other.Item3,
        Item4 - other.Item4,
        Item5 - other.Item5);

    /// <summary>
    /// this * alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public readonly Vec6d Multiply(double alpha) => new(
        Item0 * alpha,
        Item1 * alpha,
        Item2 * alpha,
        Item3 * alpha,
        Item4 * alpha,
        Item5 * alpha);

    /// <summary>
    /// this / alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public readonly Vec6d Divide(double alpha) => new(
        Item0 / alpha,
        Item1 / alpha,
        Item2 / alpha,
        Item3 / alpha,
        Item4 / alpha,
        Item5 / alpha);

#pragma warning disable 1591
    public static Vec6d operator +(Vec6d a, Vec6d b) => a.Add(b);
    public static Vec6d operator -(Vec6d a, Vec6d b) => a.Subtract(b);
    public static Vec6d operator *(Vec6d a, double alpha) => a.Multiply(alpha);
    public static Vec6d operator /(Vec6d a, double alpha) => a.Divide(alpha);
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
                3 => Item3,
                4 => Item4,
                5 => Item5,
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
                case 4: Item4 = value; break;
                case 5: Item5 = value; break;
                default: throw new ArgumentOutOfRangeException(nameof(i));
            }
        }
    }

    #endregion

    /// <inheritdoc />
    public readonly bool Equals(Vec6d other) =>
        Item0.Equals(other.Item0) && 
        Item1.Equals(other.Item1) &&
        Item2.Equals(other.Item2) && 
        Item3.Equals(other.Item3) && 
        Item4.Equals(other.Item4) &&
        Item5.Equals(other.Item5);

    /// <inheritdoc />
    public readonly override bool Equals(object? obj)
    {
        if (obj is null) return false;
        return obj is Vec6d v && Equals(v);
    }

    /// <summary> 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator ==(Vec6d a, Vec6d b) => a.Equals(b);

    /// <summary> 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator !=(Vec6d a, Vec6d b) => !(a == b);

    /// <inheritdoc />
    public readonly override int GetHashCode()
    {
#if DOTNET_FRAMEWORK || NETSTANDARD2_0
            unchecked
            {
                var hashCode = Item0.GetHashCode();
                hashCode = (hashCode * 397) ^ Item1.GetHashCode();
                hashCode = (hashCode * 397) ^ Item2.GetHashCode();
                hashCode = (hashCode * 397) ^ Item3.GetHashCode();
                hashCode = (hashCode * 397) ^ Item4.GetHashCode();
                hashCode = (hashCode * 397) ^ Item5.GetHashCode();
                return hashCode;
            }
#else
        return HashCode.Combine(Item0, Item1, Item2, Item3, Item4, Item5);
#endif
    }

    /// <inheritdoc />
    public readonly override string ToString() => $"{nameof(Vec6d)} ({Item0}, {Item1}, {Item2}, {Item3}, {Item4}, {Item5})";
}
