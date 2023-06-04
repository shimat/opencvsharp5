using System.Collections;

namespace OpenCvSharp5;

/// <summary>
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class DisposableArray<T> : IReadOnlyList<T>, IDisposable
    where T : IDisposable
{
    private readonly T[] data;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="data"></param>
    public DisposableArray(T[] data)
    {
        this.data = data;
    }
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="data"></param>
    public DisposableArray(IEnumerable<T> data)
    {
        this.data = data.ToArray();
    }

    /// <inheritdoc />
    public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)data).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => data.GetEnumerator();

    /// <inheritdoc />
    public int Count => data.Length;

    /// <inheritdoc />
    public T this[int index] => data[index];

    /// <inheritdoc />
    public void Dispose()
    {
        foreach (var d in data)
        {
            d.Dispose();
        }
    }

    /// <summary> 
    /// </summary>
    /// <param name="obj"></param>
    public static implicit operator T[](DisposableArray<T> obj) => obj.data;
}

/// <summary>
/// </summary>
public static class DisposableArrayExtensions
{
    /// <summary>
    /// </summary>
    public static DisposableArray<T> ToDisposableArray<T>(this IEnumerable<T> enumerable) where T : IDisposable =>
        new(enumerable);
}
