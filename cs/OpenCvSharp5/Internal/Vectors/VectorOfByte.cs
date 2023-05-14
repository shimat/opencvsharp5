using System.Runtime.InteropServices;

namespace OpenCvSharp5.Internal.Vectors;

/// <summary> 
/// </summary>
public class VectorOfByte : IDisposable, IStdVector<byte>, ISafeHandleHolder
{
    private readonly VectorOfByteHandle handle;
    private volatile int disposeSignaled;

    /// <inheritdoc />
    public VectorOfByteHandle Handle => handle;
    SafeHandle ISafeHandleHolder.Handle => handle;
    /// <inheritdoc />
    public bool IsDisposed => handle.IsClosed || handle.IsInvalid;

    /// <summary>
    /// Constructor
    /// </summary>
    public VectorOfByte()
    {
        handle = NativeMethods.vector_uchar_new1();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="size"></param>
    public VectorOfByte(nuint size)
    {
        if (size < 0)
            throw new ArgumentOutOfRangeException(nameof(size));
        handle = NativeMethods.vector_uchar_new2(size);
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="data"></param>
    public VectorOfByte(IEnumerable<byte> data)
    {
        if (data is null)
            throw new ArgumentNullException(nameof(data));
        var array = data.ToArray();
        handle = NativeMethods.vector_uchar_new3(array, (nuint)array.Length);
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
    ~VectorOfByte()
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
            var res = NativeMethods.vector_uchar_getSize(handle);
            GC.KeepAlive(this);
            return (int)res;
        }
    }

    /// <summary>
    /// &amp;vector[0]
    /// </summary>
    public IntPtr ElemPtr
    {
        get
        {
            var res = NativeMethods.vector_uchar_getPointer(handle);
            GC.KeepAlive(this);
            return res;
        }
    }

    /// <summary>
    /// Converts std::vector to managed array
    /// </summary>
    /// <returns></returns>
    public byte[] ToArray()
    {
        var size = Size;
        if (size == 0)        
            return Array.Empty<byte>();
        
        var dst = new byte[size];
        Marshal.Copy(ElemPtr, dst, 0, dst.Length);
        GC.KeepAlive(this); // ElemPtr is IntPtr to memory held by this object, so
        // make sure we are not disposed until finished with copy.
        return dst;
    }
}

/// <summary>
/// </summary>
public class VectorOfByteHandle : SafeHandle
{
    /// <summary>
    /// </summary>
    public VectorOfByteHandle()
        : base(invalidHandleValue: IntPtr.Zero, ownsHandle: true)
    {
    }

    /// <inheritdoc />
    protected override bool ReleaseHandle()
    {
        NativeMethods.vector_uchar_delete(handle);
        return true;
    }

    /// <inheritdoc />
    public override bool IsInvalid => handle == IntPtr.Zero;
}
