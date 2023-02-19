using System.Runtime.InteropServices;
using OpenCvSharp5.Internal;

namespace OpenCvSharp5;

public class Mat : IDisposable
{
    private readonly MatHandle handle;

    public Mat()
    {
        handle = NativeMethods.core_Mat_new1();
    }
    
    public Mat(int row, int col, MatType type)
    {
        handle = NativeMethods.core_Mat_new2(row, col, type);
    }
    
    public Mat(int row, int col, MatType type, Scalar s)
    {
        handle = NativeMethods.core_Mat_new3(row, col, type, s);
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

    public int Rows =>
        handle.GetHandleInScope(ptr =>
        {
            unsafe
            {
                var s = (NativeMat*)ptr;
                return s->rows;
            }
        });

    public int Cols =>
        handle.GetHandleInScope(ptr =>
        {
            unsafe
            {
                var s = (NativeMat*)ptr;
                return s->cols;
            }
        });

    public unsafe IntPtr Data =>
        handle.GetHandleInScope(ptr =>
        {
            unsafe
            {
                var s = (NativeMat*)ptr;
                return (IntPtr)s->data;
            }
        });

    public int SafeRows => NativeMethods.core_Mat_rows(handle);
    public int SafeCols => NativeMethods.core_Mat_cols(handle);
    public IntPtr SafeData => NativeMethods.core_Mat_data(handle);
}

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct NativeMat
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
