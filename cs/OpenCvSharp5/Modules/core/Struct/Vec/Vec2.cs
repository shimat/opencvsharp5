using System.Numerics;
using System.Runtime.InteropServices;

namespace OpenCvSharp5;

/// <summary>
/// 2-Tuple interface
/// </summary>
/// <typeparam name="T"></typeparam>
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
                case 0:
                    Item0 = value;
                    break;
                case 1:
                    Item1 = value;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(i));
            }
        }
    }

    /// <inheritdoc />
    bool IEquatable<IVec2<T>>.Equals(IVec2<T>? other) =>
        other is not null &&
        Item0 == other.Item0 &&
        Item1 == other.Item1;

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

#pragma warning disable 1591
    public static IVec2<T> operator +(IVec2<T> a, IVec2<T> b) => a.Add(b);
    public static IVec2<T> operator -(IVec2<T> a, IVec2<T> b) => a.Subtract(b);
    public static IVec2<T> operator *(IVec2<T> a, double alpha) => a.Multiply(alpha);
    public static IVec2<T> operator /(IVec2<T> a, double alpha) => a.Divide(alpha);
#pragma warning restore 1591
}

/// <summary>
/// 2-Tuple factory methods
/// </summary>
public static class Vec2
{
    /// <summary>
    /// returns a Vec with all elements set to v0
    /// </summary>
    /// <param name="v0"></param>
    /// <returns></returns>
    public static Vec2<T> All<T>(T v0)
        where T : unmanaged, INumber<T>, IMinMaxValue<T> => new(v0, v0);
}

/// <summary>
/// 2-Tuple
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
// ReSharper disable once InconsistentNaming
public record struct Vec2<T>(T Item0, T Item1)
    where T : unmanaged, INumber<T>, IMinMaxValue<T>
{
    /// <summary>
    /// The value of the first component of this object.
    /// </summary>
    public T Item0 = Item0;

    /// <summary>
    /// The value of the second component of this object.
    /// </summary>
    public T Item1 = Item1;

    /// <summary>
    /// Deconstructing a Vector
    /// </summary>
    /// <param name="item0"></param>
    /// <param name="item1"></param>
    public readonly void Deconstruct(out T item0, out T item1) => (item0, item1) = (Item0, Item1);

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
                case 0:
                    Item0 = value;
                    break;
                case 1:
                    Item1 = value;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(i));
            }
        }
    }

    /// <summary>
    /// Indexer
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public T this[Index i]
    {
        readonly get =>
            i.GetOffset(2) switch
            {
                0 => Item0,
                1 => Item1,
                _ => throw new ArgumentOutOfRangeException(nameof(i))
            };
        set
        {
            switch (i.GetOffset(2))
            {
                case 0:
                    Item0 = value;
                    break;
                case 1:
                    Item1 = value;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(i));
            }
        }
    }

    /// <summary>
    /// this + other
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public readonly Vec2<T> Add(Vec2<T> other)
    {
        T result0, result1;
        
        if (typeof(T).IsAssignableTo(typeof(IBinaryInteger<>)))
        {
            if (T.MaxValue - Item0 >= other.Item0)
            {
                result0 = Item0 + other.Item0;
                result1 = Item1 + other.Item1;
            }
            else
            {
                var size = Marshal.SizeOf<T>();
                if (typeof(T).IsAssignableTo(typeof(ISignedNumber<>)))
                {
                    switch (size)
                    {
                        case < 4:
                        {
                            var item0 = int.CreateSaturating(Item0);
                            var item1 = int.CreateSaturating(Item1);
                            var otherItem0 = int.CreateSaturating(other.Item0);
                            var otherItem1 = int.CreateSaturating(other.Item1);
                            result0 = T.CreateSaturating(item0 + otherItem0);
                            result1 = T.CreateSaturating(item1 + otherItem1);
                            break;
                        }
                        case < 8:
                        {
                            var item0 = long.CreateSaturating(Item0);
                            var item1 = long.CreateSaturating(Item1);
                            var otherItem0 = long.CreateSaturating(other.Item0);
                            var otherItem1 = long.CreateSaturating(other.Item1);
                            result0 = T.CreateSaturating(item0 + otherItem0);
                            result1 = T.CreateSaturating(item1 + otherItem1);
                            break;
                        }
                        default:
                        {
                            var item0 = Int128.CreateSaturating(Item0);
                            var item1 = Int128.CreateSaturating(Item1);
                            var otherItem0 = Int128.CreateSaturating(other.Item0);
                            var otherItem1 = Int128.CreateSaturating(other.Item1);
                            result0 = T.CreateSaturating(item0 + otherItem0);
                            result1 = T.CreateSaturating(item1 + otherItem1);
                            break;
                        }
                    }
                }
                else
                {
                    switch (size)
                    {
                        case < 4:
                        {
                            var item0 = uint.CreateSaturating(Item0);
                            var item1 = uint.CreateSaturating(Item1);
                            var otherItem0 = uint.CreateSaturating(other.Item0);
                            var otherItem1 = uint.CreateSaturating(other.Item1);
                            result0 = T.CreateSaturating(item0 + otherItem0);
                            result1 = T.CreateSaturating(item1 + otherItem1);
                            break;
                        }
                        case < 8:
                        {
                            var item0 = ulong.CreateSaturating(Item0);
                            var item1 = ulong.CreateSaturating(Item1);
                            var otherItem0 = ulong.CreateSaturating(other.Item0);
                            var otherItem1 = ulong.CreateSaturating(other.Item1);
                            result0 = T.CreateSaturating(item0 + otherItem0);
                            result1 = T.CreateSaturating(item1 + otherItem1);
                            break;
                        }
                        default:
                        {
                            var item0 = UInt128.CreateSaturating(Item0);
                            var item1 = UInt128.CreateSaturating(Item1);
                            var otherItem0 = UInt128.CreateSaturating(other.Item0);
                            var otherItem1 = UInt128.CreateSaturating(other.Item1);
                            result0 = T.CreateSaturating(item0 + otherItem0);
                            result1 = T.CreateSaturating(item1 + otherItem1);
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            var item0 = double.CreateSaturating(Item0);
            var item1 = double.CreateSaturating(Item1);
            var otherItem0 = double.CreateSaturating(other.Item0);
            var otherItem1 = double.CreateSaturating(other.Item1);
            result0 = T.CreateSaturating(item0 + otherItem0);
            result1 = T.CreateSaturating(item1 + otherItem1);
        }

        return new Vec2<T>(result0, result1);

        /*
        try
        {
            item0 = checked(Item0 + other.Item0);
        }
        catch (OverflowException)
        {
            item0 = T.MaxValue;
        }
        try
        {
            item1 = checked(Item1 + other.Item1);
        }
        catch (OverflowException)
        {
            item1 = T.MaxValue;
        }

        return new Vec2<T>(
            T.CreateSaturating(item0),
            T.CreateSaturating(item1));*/
    }

    /// <summary>
    /// this + other
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public readonly Vec2<T> AddChecked(Vec2<T> other)
    {
        var item0 = checked(Item0 + other.Item0);
        var item1 = checked(Item1 + other.Item1);

        return new Vec2<T>(
            T.CreateSaturating(item0),
            T.CreateSaturating(item1));
    }

    /// <summary>
    /// this - other
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public readonly Vec2<T> Subtract(Vec2<T> other) => new(
        T.CreateSaturating(Item0 - other.Item0),
        T.CreateSaturating(Item1 - other.Item1));

    /// <summary>
    /// this * alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public readonly Vec2<T> Multiply(double alpha) => new(
        T.CreateSaturating(double.CreateSaturating(Item0) * alpha),
        T.CreateSaturating(double.CreateSaturating(Item1) * alpha));

    /// <summary>
    /// this / alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public readonly Vec2<T> Divide(double alpha) => new(
        T.CreateSaturating(double.CreateSaturating(Item0) / alpha),
        T.CreateSaturating(double.CreateSaturating(Item1) / alpha));

#pragma warning disable 1591
    public static Vec2<T> operator +(Vec2<T> a, Vec2<T> b) => a.Add(b);
    public static Vec2<T> operator checked +(Vec2<T> a, Vec2<T> b) => a.AddChecked(b);
    public static Vec2<T> operator -(Vec2<T> a, Vec2<T> b) => a.Subtract(b);
    public static Vec2<T> operator *(Vec2<T> a, double alpha) => a.Multiply(alpha);
    public static Vec2<T> operator /(Vec2<T> a, double alpha) => a.Divide(alpha);
#pragma warning restore 1591
}
