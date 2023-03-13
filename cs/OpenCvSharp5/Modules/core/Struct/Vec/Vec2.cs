﻿using System.Numerics;
using System.Runtime.InteropServices;

namespace OpenCvSharp5;

/// <summary>
/// 2-Tuple static method 
/// </summary>
public static class Vec2
{
    /// <summary>
    /// returns a Vec with all elements set to v0
    /// </summary>
    /// <param name="v0"></param>
    /// <returns></returns>
    public static Vec2<T> All<T>(T v0)
        where T : unmanaged, IBinaryNumber<T> => new(v0, v0);
}

/// <summary>
/// 2-Tuple
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="Item1">The value of the first component of this object.</param>
/// <param name="Item2">The value of the second component of this object.</param>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public record struct Vec2<T>(T Item1, T Item2)
    where T : unmanaged, IBinaryNumber<T>
{
    /// <summary>
    /// The value of the first component of this object.
    /// </summary>
    public T Item1 = Item1;

    /// <summary>
    /// The value of the second component of this object.
    /// </summary>
    public T Item2 = Item2;
    
    /// <summary>
    /// Indexer
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public T this[int i]
    {
        readonly get =>
            i switch
            {
                0 => Item1,
                1 => Item2,
                _ => throw new ArgumentOutOfRangeException(nameof(i))
            };
        set
        {
            switch (i)
            {
                case 0: Item1 = value; break;
                case 1: Item2 = value; break;
                default: throw new ArgumentOutOfRangeException(nameof(i));
            }
        }
    }
    
    /// <summary>
    /// this + other
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public readonly Vec2<T> Add(Vec2<T> other) => new(
        T.CreateSaturating(Item1 + other.Item1),
        T.CreateSaturating(Item2 + other.Item2));

    /// <summary>
    /// this - other
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public readonly Vec2<T> Subtract(Vec2<T> other) => new(
        T.CreateSaturating(Item1 - other.Item1),
        T.CreateSaturating(Item2 - other.Item2));

    /// <summary>
    /// this * alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public readonly Vec2<T> Multiply(double alpha) => new(
        T.CreateSaturating(double.CreateSaturating(Item1) * alpha),
        T.CreateSaturating(double.CreateSaturating(Item2) * alpha));

    /// <summary>
    /// this / alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public readonly Vec2<T> Divide(double alpha) => new(
        T.CreateSaturating(double.CreateSaturating(Item1) / alpha),
        T.CreateSaturating(double.CreateSaturating(Item2) / alpha));

#pragma warning disable 1591
    public static Vec2<T> operator +(Vec2<T> a, Vec2<T> b) => a.Add(b);
    public static Vec2<T> operator -(Vec2<T> a, Vec2<T> b) => a.Subtract(b);
    public static Vec2<T> operator *(Vec2<T> a, double alpha) => a.Multiply(alpha);
    public static Vec2<T> operator /(Vec2<T> a, double alpha) => a.Divide(alpha);
#pragma warning restore 1591
}
