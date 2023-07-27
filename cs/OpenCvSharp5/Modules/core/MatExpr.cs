using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using OpenCvSharp5.Internal;

namespace OpenCvSharp5;

/// <summary>
/// Matrix expression representation
/// </summary>
public class MatExpr : IDisposable, IInputArray, ISafeHandleHolder
{
    private readonly MatExprHandle handle;
    private GCHandle dataHandle;
    private volatile int disposeSignaled;
    
    /// <summary>
    /// </summary>
    public MatExprHandle Handle => handle;
    SafeHandle ISafeHandleHolder.Handle => handle;
    /// <inheritdoc />
    public bool IsDisposed => handle.IsClosed || handle.IsInvalid;

    #region Init & Disposal

    /// <summary>
    /// </summary>
    public MatExpr(MatExprHandle handle)
    {
        this.handle = handle;
    }
    
    /// <summary> 
    /// </summary>
    public MatExpr()
    {
        NativeMethods.HandleException(
            NativeMethods.core_MatExpr_new1(out handle));
    }
    
    /// <summary> 
    /// </summary>
    public MatExpr(Mat m)
    {
        ThrowIfNull(m);
        if (m.IsDisposed)
            throw new ArgumentException("The Mat is disposed.", nameof(m));
        
        NativeMethods.HandleException(
            NativeMethods.core_MatExpr_new2(m.Handle, out handle));

        GC.KeepAlive(m);
    }

    private void ReleaseUnmanagedResources()
    {
        if (dataHandle.IsAllocated)
            dataHandle.Free();
    }
    
    /// <summary>
    /// Protected implementation of Dispose pattern.
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        // http://stackoverflow.com/questions/425132/a-reference-to-a-volatile-field-will-not-be-treated-as-volatile-implications
        if (Interlocked.Exchange(ref disposeSignaled, 1) != 0)
            return;

        ReleaseUnmanagedResources();

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
    ~MatExpr()
    {
        Dispose(false);
    }

    #endregion

    /// <summary> 
    /// </summary>
    /// <param name="m"></param>
    public static explicit operator MatExpr(Mat m) => new(m);

    /// <summary> 
    /// </summary>
    /// <param name="self"></param>
    public static explicit operator Mat(MatExpr self) => self.ToMat();
    
    /// <summary>
    /// </summary>
    [Pure]
    public Mat ToMat()
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);

        NativeMethods.HandleException(
            NativeMethods.core_MatExpr_toMat(handle, out var matPtr));

        GC.KeepAlive(this);
        return new Mat(matPtr);
    }

    /// <summary>
    /// </summary>
    [Pure]
    public Size Size() 
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);

        NativeMethods.HandleException(
            NativeMethods.core_MatExpr_size(handle, out var ret));
        GC.KeepAlive(this);

        return ret;
    }
    
    /// <summary>
    /// </summary>
    [Pure]
    public MatType Type()
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);

        NativeMethods.HandleException(
            NativeMethods.core_MatExpr_type(handle, out var ret));
        GC.KeepAlive(this);

        return ret;
    }

    /// <summary>
    /// </summary>
    [Pure]
    public MatExpr Row(int y)
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);

        NativeMethods.HandleException(
            NativeMethods.core_MatExpr_row(handle, y, out var matExprPtr));

        GC.KeepAlive(this);
        return new MatExpr(matExprPtr);
    }

    /// <summary>
    /// </summary>
    [Pure]
    public MatExpr Col(int x)
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);

        NativeMethods.HandleException(
            NativeMethods.core_MatExpr_col(handle, x, out var matExprPtr));

        GC.KeepAlive(this);
        return new MatExpr(matExprPtr);
    }
    
    /// <summary>
    /// </summary>
    [Pure]
    public MatExpr Diag(int d = 0)
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);
        
        NativeMethods.HandleException(
            NativeMethods.core_MatExpr_diag(handle, d, out var matExprPtr));

        GC.KeepAlive(this);
        return new MatExpr(matExprPtr);
    }
    
    /// <summary>
    /// </summary>
    [Pure]
    public MatExpr Crop(Range rowRange, Range colRange)
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);
        
        NativeMethods.HandleException(
            NativeMethods.core_MatExpr_cropByRange(
                handle, rowRange, colRange, out var matExprPtr));

        GC.KeepAlive(this);
        return new MatExpr(matExprPtr);
    }
    
    /// <summary>
    /// </summary>
    public MatExpr this[Range rowRange, Range colRange] => Crop(rowRange, colRange);
    
    /// <summary>
    /// </summary>
    [Pure]
    public MatExpr Crop(Rect roi)
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);
        
        NativeMethods.HandleException(
            NativeMethods.core_MatExpr_cropByRect(
                handle, roi, out var matExprPtr));

        GC.KeepAlive(this);
        return new MatExpr(matExprPtr);
    }

    /// <summary>
    /// </summary>
    public MatExpr this[Rect roi] => Crop(roi);

    /// <inheritdoc />
    [Pure]
    public InputArrayHandle ToInputArrayHandle()
    {
        NativeMethods.HandleException(
            NativeMethods.core_InputArray_new_byMatExpr(handle, out var resultHandle));
        GC.KeepAlive(this);
        return resultHandle;
    }
}

/// <summary>
/// </summary>
public class MatExprHandle : SafeHandle
{
    /// <summary>
    /// </summary>
    public MatExprHandle()
        : base(invalidHandleValue: IntPtr.Zero, ownsHandle: true)
    {
    }

    /// <inheritdoc />
    protected override bool ReleaseHandle()
    {
        NativeMethods.HandleException(
            NativeMethods.core_MatExpr_delete(handle));
        return true;
    }

    /// <inheritdoc />
    public override bool IsInvalid => handle == IntPtr.Zero;
}
