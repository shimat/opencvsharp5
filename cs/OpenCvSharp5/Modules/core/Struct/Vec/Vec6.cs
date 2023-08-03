using System.Numerics;
using System.Runtime.InteropServices;

namespace OpenCvSharp5;

#pragma warning disable CA1000

/// <summary>
/// 6-Tuple
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="Item0">The value of the first component of this object.</param>
/// <param name="Item1">The value of the second component of this object.</param>
/// <param name="Item2">The value of the third component of this object.</param>
/// <param name="Item3">The value of the fourth component of this object.</param>
/// <param name="Item4">The value of the third component of this object.</param>
/// <param name="Item5">The value of the fourth component of this object.</param>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
// ReSharper disable once InconsistentNaming
public record struct Vec6<T>(T Item0, T Item1, T Item2, T Item3, T Item4, T Item5)
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
    /// The value of the fourth component of this object.
    /// </summary>
    public T Item3 = Item3;
    
    /// <summary>
    /// The value of the fifth component of this object.
    /// </summary>
    public T Item4 = Item4;

    /// <summary>
    /// The value of the sixth component of this object.
    /// </summary>
    public T Item5 = Item5;
    
    /// <summary>
    /// returns a Vec with all elements set to v0
    /// </summary>
    /// <param name="v0"></param>
    /// <returns></returns>
    public static Vec6<T> All(T v0) => new(v0, v0, v0, v0, v0, v0);

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
    
    /// <summary>
    /// Indexer
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public T this[Index i]
    {
        readonly get =>
            i.GetOffset(6) switch
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
            switch (i.GetOffset(6))
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
                case 3:
                    Item3 = value;
                    break;
                case 4:
                    Item4 = value;
                    break;
                case 5:
                    Item5 = value;
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
    public readonly Vec6<T> Add(Vec6<T> other) => new(
        T.CreateSaturating(Item0 + other.Item0),
        T.CreateSaturating(Item1 + other.Item1),
        T.CreateSaturating(Item2 + other.Item2),
        T.CreateSaturating(Item3 + other.Item3),
        T.CreateSaturating(Item4 + other.Item4),
        T.CreateSaturating(Item5 + other.Item5));
    
    /// <summary>
    /// this + other
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public readonly Vec6<T> AddChecked(Vec6<T> other) => new(
        T.CreateSaturating(checked(Item0 + other.Item0)),
        T.CreateSaturating(checked(Item1 + other.Item1)),
        T.CreateSaturating(checked(Item2 + other.Item2)),
        T.CreateSaturating(checked(Item3 + other.Item3)),
        T.CreateSaturating(checked(Item4 + other.Item4)),
        T.CreateSaturating(checked(Item5 + other.Item5)));

    /// <summary>
    /// this - other
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public readonly Vec6<T> Subtract(Vec6<T> other) => new(
        T.CreateSaturating(Item0 - other.Item0),
        T.CreateSaturating(Item1 - other.Item1),
        T.CreateSaturating(Item2 - other.Item2),
        T.CreateSaturating(Item3 - other.Item3),
        T.CreateSaturating(Item4 - other.Item4),
        T.CreateSaturating(Item5 - other.Item5));

    /// <summary>
    /// this - other
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public readonly Vec6<T> SubtractChecked(Vec6<T> other) => new(
        T.CreateSaturating(checked(Item0 - other.Item0)),
        T.CreateSaturating(checked(Item1 - other.Item1)),
        T.CreateSaturating(checked(Item2 - other.Item2)),
        T.CreateSaturating(checked(Item3 - other.Item3)),
        T.CreateSaturating(checked(Item4 - other.Item4)),
        T.CreateSaturating(checked(Item5 - other.Item5)));

    /// <summary>
    /// this * alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public readonly Vec6<T> Multiply(double alpha) => new(
        T.CreateSaturating(double.CreateSaturating(Item0) * alpha),
        T.CreateSaturating(double.CreateSaturating(Item1) * alpha),
        T.CreateSaturating(double.CreateSaturating(Item2) * alpha),
        T.CreateSaturating(double.CreateSaturating(Item3) * alpha),
        T.CreateSaturating(double.CreateSaturating(Item4) * alpha),
        T.CreateSaturating(double.CreateSaturating(Item5) * alpha));

    /// <summary>
    /// this * alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public readonly Vec6<T> MultiplyChecked(double alpha) => new(
        T.CreateChecked(double.CreateSaturating(Item0) * alpha),
        T.CreateChecked(double.CreateSaturating(Item1) * alpha),
        T.CreateChecked(double.CreateSaturating(Item2) * alpha),
        T.CreateChecked(double.CreateSaturating(Item3) * alpha),
        T.CreateChecked(double.CreateSaturating(Item4) * alpha),
        T.CreateChecked(double.CreateSaturating(Item5) * alpha));

    /// <summary>
    /// this / alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public readonly Vec6<T> Divide(double alpha) => new(
        T.CreateSaturating(double.CreateSaturating(Item0) / alpha),
        T.CreateSaturating(double.CreateSaturating(Item1) / alpha),
        T.CreateSaturating(double.CreateSaturating(Item2) / alpha),
        T.CreateSaturating(double.CreateSaturating(Item3) / alpha),
        T.CreateSaturating(double.CreateSaturating(Item4) / alpha),
        T.CreateSaturating(double.CreateSaturating(Item5) / alpha));

    /// <summary>
    /// this / alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public readonly Vec6<T> DivideChecked(double alpha) => new(
        T.CreateChecked(double.CreateSaturating(Item0) / alpha),
        T.CreateChecked(double.CreateSaturating(Item1) / alpha),
        T.CreateChecked(double.CreateSaturating(Item2) / alpha),
        T.CreateChecked(double.CreateSaturating(Item3) / alpha),
        T.CreateChecked(double.CreateSaturating(Item4) / alpha),
        T.CreateChecked(double.CreateSaturating(Item5) / alpha));

#pragma warning disable 1591
    public static Vec6<T> operator +(Vec6<T> a, Vec6<T> b) => a.Add(b);
    public static Vec6<T> operator checked +(Vec6<T> a, Vec6<T> b) => a.AddChecked(b);
    public static Vec6<T> operator -(Vec6<T> a, Vec6<T> b) => a.Subtract(b);
    public static Vec6<T> operator checked -(Vec6<T> a, Vec6<T> b) => a.SubtractChecked(b);
    public static Vec6<T> operator *(Vec6<T> a, double alpha) => a.Multiply(alpha);
    public static Vec6<T> operator checked *(Vec6<T> a, double alpha) => a.MultiplyChecked(alpha);
    public static Vec6<T> operator /(Vec6<T> a, double alpha) => a.Divide(alpha);
    public static Vec6<T> operator checked /(Vec6<T> a, double alpha) => a.DivideChecked(alpha);
#pragma warning restore 1591
}
