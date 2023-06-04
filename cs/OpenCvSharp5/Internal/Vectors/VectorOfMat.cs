using System.Runtime.InteropServices;

namespace OpenCvSharp5.Internal.Vectors;

/// <summary> 
/// </summary>
public class VectorOfMat : IDisposable, IStdVector<Mat>, ISafeHandleHolder
{
    private readonly VectorOfMatHandle handle;
    private volatile int disposeSignaled;

    /// <inheritdoc />
    public VectorOfMatHandle Handle => handle;
    SafeHandle ISafeHandleHolder.Handle => handle;
    /// <inheritdoc />
    public bool IsDisposed => handle.IsClosed || handle.IsInvalid;

    /// <summary>
    /// Constructor
    /// </summary>
    public VectorOfMat()
    {
        handle = NativeMethods.vector_Mat_new1();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="size"></param>
    public VectorOfMat(int size)
    {
        if (size < 0)
            throw new ArgumentOutOfRangeException(nameof(size));
        handle = NativeMethods.vector_Mat_new2((uint)size);
    }
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mats"></param>
    public VectorOfMat(IEnumerable<Mat> mats)
    {
        if (mats is null)
            throw new ArgumentNullException(nameof(mats));

        var matsArray = mats.ToArray();
        var matPointers = matsArray.Select(x => x.Handle.DangerousGetHandle()).ToArray();

        handle = NativeMethods.vector_Mat_new3(
            matPointers,
            (uint)matPointers.Length);

        foreach (var m in matsArray)
        {
            GC.KeepAlive(m);
        }
        GC.KeepAlive(mats);
    }

    /// <summary>
    /// Protected implementation of Dispose pattern.
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        // http://stackoverflow.com/questions/425132/a-reference-to-a-volatile-field-will-not-be-treated-as-volatile-implications
        if (Interlocked.Exchange(ref disposeSignaled, 1) != 0)
            return;

        if (disposing)
        {
            handle.Dispose();
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc />
    ~VectorOfMat()
    {
        Dispose(false);
    }

    /// <summary>
    /// vector.size()
    /// </summary>
    public int Size
    {
        get
        {
            var res = NativeMethods.vector_Mat_getSize(handle);
            GC.KeepAlive(this);
            return (int)res;
        }
    }
    
    /// <summary>
    /// Converts std::vector to managed array
    /// </summary>
    /// <returns></returns>
    public Mat[] ToArray() => ToArray<Mat>();

    /// <summary>
    /// Converts std::vector to managed array
    /// </summary>
    /// <returns></returns>
    public T[] ToArray<T>()
        where T : Mat, new()
    {
        var size = Size;
        if (size == 0)
            return Array.Empty<T>();

        var dst = new T[size];
        CopyToArray(dst);
        return dst;
    }

    /// <summary>
    /// Converts std::vector to managed array
    /// </summary>
    /// <returns></returns>
    public void CopyToArray<T>(IReadOnlyList<T> dst)
        where T : Mat, new()
    {
        var size = Size;
        if (size != dst.Count)
            throw new ArgumentException("this.Size != dst.Count");
        if (size == 0)
            return;

        var dstPtr = new nint[size];
        for (var i = 0; i < size; i++)
        {
            dstPtr[i] = dst[i].Handle.DangerousGetHandle();
        }

        NativeMethods.vector_Mat_assignToArray(handle, dstPtr);
        GC.KeepAlive(this);
    }

    /*
    /// <summary>
    /// Converts std::vector to managed array
    /// </summary>
    /// <returns></returns>
    public T[] ToArray_<T>()
        where T : Mat, new()
    {
        var size = Size;
        if (size == 0)
            return Array.Empty<T>();

        var dst = new T[size];
        for (var i = 0; i < size; i++)
        {
            var m = new T();
            dst[i] = m;
            NativeMethods.vector_Mat_copyOneElement(handle, i, m.Handle);
        }
        GC.KeepAlive(this);

        return dst;
    }*/
}


/// <summary>
/// </summary>
public class VectorOfMatHandle : SafeHandle
{
    /// <summary>
    /// </summary>
    public VectorOfMatHandle()
        : base(invalidHandleValue: IntPtr.Zero, ownsHandle: true)
    {
    }

    /// <inheritdoc />
    protected override bool ReleaseHandle()
    {
        NativeMethods.vector_Mat_delete(handle);
        return true;
    }

    /// <inheritdoc />
    public override bool IsInvalid => handle == IntPtr.Zero;
}
