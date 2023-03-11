using System.Numerics;
using System.Runtime.InteropServices;

namespace OpenCvSharp5;

public interface IVec2<T>
    : IEquatable<IVec2<T>>
    where T : unmanaged, INumber<T>
{
    /// <summary>
    /// The value of the first component of this object.
    /// </summary>
    public T Item0 { get; set; }

    /// <summary>
    /// The value of the second component of this object.
    /// </summary>
    public T Item1 { get; set; }

    /// <summary>
    /// Deconstructing a Vector
    /// </summary>
    /// <param name="item0"></param>
    /// <param name="item1"></param>
    public void Deconstruct(out T item0, out T item1) => (item0, item1) = (Item0, Item1);
    
    /// <summary>
    /// Indexer
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public T this[int i]
    {
        get =>
            i switch
            {
                0 => Item0,
                1 => Item1,
                _ => throw new ArgumentOutOfRangeException(nameof(i))
            };
        set
        {
            switch (i)
            {
                case 0: Item0 = value; break;
                case 1: Item1 = value; break;
                default: throw new ArgumentOutOfRangeException(nameof(i));
            }
        }
    }

    /// <inheritdoc />
    bool IEquatable<IVec2<T>>.Equals(IVec2<T>? other) =>
        other is not null &&
        Item0 == other.Item0 &&
        Item1 == other.Item1;
    
    public int GetHashCode() => HashCode.Combine(Item0, Item1);
    
    public string ToString() => $"{GetType().Name} ({Item0}, {Item1})";

    /// <summary>
    /// this + other
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public IVec2<T> Add(IVec2<T> other);

    /// <summary>
    /// this - other
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public IVec2<T> Subtract(IVec2<T> other);

    /// <summary>
    /// this * alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public IVec2<T> Multiply(double alpha);

    /// <summary>
    /// this * alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public IVec2<T> Divide(double alpha);

    public static IVec2<T> operator +(IVec2<T> a, IVec2<T> b) => a.Add(b);
    public static IVec2<T> operator -(IVec2<T> a, IVec2<T> b) => a.Subtract(b);
    public static IVec2<T> operator *(IVec2<T> a, double alpha) => a.Multiply(alpha);
    public static IVec2<T> operator /(IVec2<T> a, double alpha) => a.Divide(alpha);
}

/// <summary>
/// 2-Tuple
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
// ReSharper disable once InconsistentNaming
public struct Vec2<T>
    : IEquatable<Vec2<T>>
    where T : unmanaged, INumber<T>
{
    /// <summary>
    /// The value of the first component of this object.
    /// </summary>
    public T Item0;

    /// <summary>
    /// The value of the second component of this object.
    /// </summary>
    public T Item1;

    /// <summary>
    /// Deconstructing a Vector
    /// </summary>
    /// <param name="item0"></param>
    /// <param name="item1"></param>
    public readonly void Deconstruct(out T item0, out T item1) => (item0, item1) = (Item0, Item1);

    /// <summary>
    /// Initializer
    /// </summary>
    /// <param name="item0"></param>
    /// <param name="item1"></param>
    public Vec2(T item0, T item1)
    {
        Item0 = item0;
        Item1 = item1;
    }

    /// <summary>
    /// returns a Vec with all elements set to v0
    /// </summary>
    /// <param name="v0"></param>
    /// <returns></returns>
    public static Vec2<T> All(T v0) => new(v0, v0);
    
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
                0 => Item0,
                1 => Item1,
                _ => throw new ArgumentOutOfRangeException(nameof(i))
            };
        set
        {
            switch (i)
            {
                case 0: Item0 = value; break;
                case 1: Item1 = value; break;
                default: throw new ArgumentOutOfRangeException(nameof(i));
            }
        }
    }

    /// <inheritdoc />
    public readonly bool Equals(Vec2<T> other) =>
        Item0 == other.Item0 &&
        Item1 == other.Item1;

    /// <inheritdoc />
    public readonly override bool Equals(object? obj)
    {
        if (obj is null) return false;
        return obj is Vec2<T> v && Equals(v);
    }

    /// <summary> 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator ==(Vec2<T> a, Vec2<T> b) => a.Equals(b);

    /// <summary> 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator !=(Vec2<T> a, Vec2<T> b) => !a.Equals(b);

    /// <inheritdoc />
    public readonly override int GetHashCode() => HashCode.Combine(Item0, Item1);

    /// <inheritdoc />
    public readonly override string ToString() => $"{GetType().Name} ({Item0}, {Item1})";
}

public struct Vec2b_
{
    public byte Item0;
    public byte Item1;

    /// <summary>
    /// Indexer
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public byte this[int i]
    {
        get
        {
            switch (i)
            {
                case 0:
                    return Item0;
                case 1:
                    return Item1;
                default:
                    throw new ArgumentOutOfRangeException(nameof(i));
            }
        }
        set
        {
            switch (i)
            {
                case 0: Item0 = value; break;
                case 1: Item1 = value; break;
                default: throw new ArgumentOutOfRangeException(nameof(i));
            }
        }
    }
}
