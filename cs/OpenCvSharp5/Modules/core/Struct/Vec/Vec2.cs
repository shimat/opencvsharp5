using System.Numerics;
using System.Runtime.InteropServices;

namespace OpenCvSharp5;

#pragma warning disable CA1000

/// <summary>
/// 2-Tuple
/// </summary>
/// <param name="Item0">The value of the first component of this object.</param>
/// <param name="Item1">The value of the second component of this object.</param>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
// ReSharper disable once InconsistentNaming
public record struct Vec2<T>(T Item0, T Item1)
    where T : unmanaged, INumber<T>
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
        var item0 = Item0 + other.Item0;
        var item1 = Item1 + other.Item1;

        return new Vec2<T>(
            T.CreateSaturating(item0),
            T.CreateSaturating(item1));
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
    public readonly Vec2<T> Subtract(Vec2<T> other)
    {
        var item0 = Item0 - other.Item0;
        var item1 = Item1 - other.Item1;

        return new Vec2<T>(
            T.CreateSaturating(item0),
            T.CreateSaturating(item1));
    }

    /// <summary>
    /// this - other
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public readonly Vec2<T> SubtractChecked(Vec2<T> other)
    {
        var item0 = checked(Item0 - other.Item0);
        var item1 = checked(Item1 - other.Item1);

        return new Vec2<T>(
            T.CreateSaturating(item0),
            T.CreateSaturating(item1));
    }

    /// <summary>
    /// this * alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public readonly Vec2<T> Multiply(double alpha) => new(
        T.CreateSaturating(double.CreateSaturating(Item0) * alpha),
        T.CreateSaturating(double.CreateSaturating(Item1) * alpha));

    /// <summary>
    /// this * alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public readonly Vec2<T> MultiplyChecked(double alpha) => new(
        T.CreateChecked(double.CreateSaturating(Item0) * alpha),
        T.CreateChecked(double.CreateSaturating(Item1) * alpha));

    /// <summary>
    /// this / alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public readonly Vec2<T> Divide(double alpha) => new(
        T.CreateSaturating(double.CreateSaturating(Item0) / alpha),
        T.CreateSaturating(double.CreateSaturating(Item1) / alpha));

    /// <summary>
    /// this / alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public readonly Vec2<T> DivideChecked(double alpha) => new(
        T.CreateChecked(double.CreateSaturating(Item0) / alpha),
        T.CreateChecked(double.CreateSaturating(Item1) / alpha));

#pragma warning disable 1591
    public static Vec2<T> operator +(Vec2<T> a, Vec2<T> b) => a.Add(b);
    public static Vec2<T> operator checked +(Vec2<T> a, Vec2<T> b) => a.AddChecked(b);
    public static Vec2<T> operator -(Vec2<T> a, Vec2<T> b) => a.Subtract(b);
    public static Vec2<T> operator checked -(Vec2<T> a, Vec2<T> b) => a.SubtractChecked(b);
    public static Vec2<T> operator *(Vec2<T> a, double alpha) => a.Multiply(alpha);
    public static Vec2<T> operator checked *(Vec2<T> a, double alpha) => a.MultiplyChecked(alpha);
    public static Vec2<T> operator /(Vec2<T> a, double alpha) => a.Divide(alpha);
    public static Vec2<T> operator checked /(Vec2<T> a, double alpha) => a.DivideChecked(alpha);
#pragma warning restore 1591
}
