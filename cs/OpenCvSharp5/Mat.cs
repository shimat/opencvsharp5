using System;
using System.Runtime.InteropServices;

namespace OpenCvSharp5;

public class Mat : IDisposable
{
    private readonly MatHandle handle;

    public Mat()
    {
        handle = NativeMethods.core_Mat_new1();
    }
    
    public Mat(int row, int col, int type)
    {
        handle = NativeMethods.core_Mat_new2(row, col, type);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            handle.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public int Rows
    {
        get
        {
            if (handle.IsInvalid)
                throw new ObjectDisposedException($"{nameof(Mat)} is disposed.");

            var addRef = false;
            try
            {
                handle.DangerousAddRef(ref addRef);
                var ptr = handle.DangerousGetHandle();
                unsafe
                {
                    var s = (NativeMat*)ptr;
                    return s->rows;
                }
            }
            finally
            {
                if (addRef)
                    handle.DangerousRelease();
                GC.KeepAlive(this);
            }
        }
    }
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct NativeMat
{
    public int flags;
    //! the matrix dimensionality, >= 2
    public int dims;
    //! the number of rows and columns or (-1, -1) when the matrix has more than 2 dimensions
    public int rows;
    public int cols;
    //! pointer to the data
    public byte* data;
}

internal class MatHandle : SafeHandle
{
    internal MatHandle()
        : base(invalidHandleValue: IntPtr.Zero, ownsHandle: true)
    {
    }
    
    protected override bool ReleaseHandle()
    {
        NativeMethods.core_Mat_delete(handle);
        return true;
    }

    public override bool IsInvalid => handle == IntPtr.Zero;
}
