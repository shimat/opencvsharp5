using System.Numerics;
using System.Runtime.InteropServices;

namespace OpenCvSharp5;

#pragma warning disable CA1000

/// <summary>
/// 3-Tuple
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="Item0">The value of the first component of this object.</param>
/// <param name="Item1">The value of the second component of this object.</param>
/// <param name="Item2">The value of the third component of this object.</param>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
// ReSharper disable once InconsistentNaming
public record struct Vec3<T>(T Item0, T Item1, T Item2)
    where T : unmanaged, IBinaryNumber<T>
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
    /// The value of the third component of this object.
    /// </summary>
    public T Item2 = Item2;
    
    /// <summary>
    /// returns a Vec with all elements set to v0
    /// </summary>
    /// <param name="v0"></param>
    /// <returns></returns>
    public static Vec3<T> All(T v0) => new(v0, v0, v0);

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

    /// <summary>
    /// Indexer
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public T this[Index i]
    {
        readonly get =>
            i.GetOffset(3) switch
            {
                0 => Item0,
                1 => Item1,
                2 => Item2,
                _ => throw new ArgumentOutOfRangeException(nameof(i))
            };
        set
        {
            switch (i.GetOffset(3))
            {
                case 0:
                    Item0 = value;
                    break;
                case 1:
                    Item1 = value;
                    break;
                case 2:
                    Item2 = value;
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
    public readonly Vec3<T> Add(Vec3<T> other) => new(
        T.CreateSaturating(Item0 + other.Item0),
        T.CreateSaturating(Item1 + other.Item1),
        T.CreateSaturating(Item2 + other.Item2));

    /// <summary>
    /// this + other
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public readonly Vec3<T> AddChecked(Vec3<T> other) => new(
        T.CreateSaturating(checked(Item0 + other.Item0)),
        T.CreateSaturating(checked(Item1 + other.Item1)),
        T.CreateSaturating(checked(Item2 + other.Item2)));

    /// <summary>
    /// this - other
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public readonly Vec3<T> Subtract(Vec3<T> other) => new(
        T.CreateSaturating(Item0 - other.Item0),
        T.CreateSaturating(Item1 - other.Item1),
        T.CreateSaturating(Item2 - other.Item2));

    /// <summary>
    /// this - other
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public readonly Vec3<T> SubtractChecked(Vec3<T> other) => new(
        T.CreateSaturating(checked(Item0 - other.Item0)),
        T.CreateSaturating(checked(Item1 - other.Item1)),
        T.CreateSaturating(checked(Item2 - other.Item2)));

    /// <summary>
    /// this * alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public readonly Vec3<T> Multiply(double alpha) => new(
        T.CreateSaturating(double.CreateSaturating(Item0) * alpha),
        T.CreateSaturating(double.CreateSaturating(Item1) * alpha),
        T.CreateSaturating(double.CreateSaturating(Item2) * alpha));

    /// <summary>
    /// this * alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public readonly Vec3<T> MultiplyChecked(double alpha) => new(
        T.CreateChecked(double.CreateSaturating(Item0) * alpha),
        T.CreateChecked(double.CreateSaturating(Item1) * alpha),
        T.CreateChecked(double.CreateSaturating(Item2) * alpha));

    /// <summary>
    /// this / alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public readonly Vec3<T> Divide(double alpha) => new(
        T.CreateSaturating(double.CreateSaturating(Item0) / alpha),
        T.CreateSaturating(double.CreateSaturating(Item1) / alpha),
        T.CreateSaturating(double.CreateSaturating(Item2) / alpha));

    /// <summary>
    /// this / alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public readonly Vec3<T> DivideChecked(double alpha) => new(
        T.CreateChecked(double.CreateSaturating(Item0) / alpha),
        T.CreateChecked(double.CreateSaturating(Item1) / alpha),
        T.CreateChecked(double.CreateSaturating(Item2) / alpha));

#pragma warning disable 1591
    public static Vec3<T> operator +(Vec3<T> a, Vec3<T> b) => a.Add(b);
    public static Vec3<T> operator checked +(Vec3<T> a, Vec3<T> b) => a.AddChecked(b);
    public static Vec3<T> operator -(Vec3<T> a, Vec3<T> b) => a.Subtract(b);
    public static Vec3<T> operator checked -(Vec3<T> a, Vec3<T> b) => a.SubtractChecked(b);
    public static Vec3<T> operator *(Vec3<T> a, double alpha) => a.Multiply(alpha);
    public static Vec3<T> operator checked *(Vec3<T> a, double alpha) => a.MultiplyChecked(alpha);
    public static Vec3<T> operator /(Vec3<T> a, double alpha) => a.Divide(alpha);
    public static Vec3<T> operator checked  /(Vec3<T> a, double alpha) => a.DivideChecked(alpha);
#pragma warning restore 1591
}
