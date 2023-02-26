using System;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using OpenCvSharp5.Internal;

namespace OpenCvSharp5;

/// <summary>
/// n-dimensional dense array class
/// </summary>
public class Mat : IDisposable, IOutputArray, IInputOutputArray
{
    internal MatHandle Handle { get; }

    /// <summary>
    /// These are various constructors that form a matrix. As noted in the AutomaticAllocation, often
    /// the default constructor is enough, and the proper matrix will be allocated by an OpenCV function.
    /// The constructed matrix can further be assigned to another matrix or matrix expression or can be
    /// allocated with Mat::create.In the former case, the old content is de-referenced.
    /// </summary>
    public Mat()
    {
        NativeMethods.HandleException(
            NativeMethods.core_Mat_new1(out var handle));
        Handle = handle;
    }

    /// <summary> 
    /// </summary>
    /// <param name="row">Number of rows in a 2D array.</param>
    /// <param name="col">Number of columns in a 2D array.</param>
    /// <param name="type">Array type. Use CV_8UC1, ..., CV_64FC4 to create 1-4 channel matrices, or
    /// CV_8UC(n), ..., CV_64FC(n) to create multi-channel(up to CV_CN_MAX channels) matrices.</param>
    public Mat(int row, int col, MatType type)
    {
        NativeMethods.HandleException(
            NativeMethods.core_Mat_new2(row, col, type, out var handle));
        Handle = handle;
    }

    /// <summary> 
    /// </summary>
    /// <param name="row">Number of rows in a 2D array.</param>
    /// <param name="col">Number of columns in a 2D array.</param>
    /// <param name="type">Array type. Use CV_8UC1, ..., CV_64FC4 to create 1-4 channel matrices, or
    /// CV_8UC(n), ..., CV_64FC(n) to create multi-channel(up to CV_CN_MAX channels) matrices.</param>
    /// <param name="s">An optional value to initialize each matrix element with. To set all the matrix elements to
    /// the particular value after the construction, use the assignment operator
    /// Mat::operator=(const Scalar&amp; value) .</param>
    public Mat(int row, int col, MatType type, Scalar s)
    {
        NativeMethods.HandleException(
            NativeMethods.core_Mat_new3(row, col, type, s, out var handle));
        Handle = handle;
    }

    /// <summary>
    /// Protected implementation of Dispose pattern.
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Handle.Dispose();
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// the number of rows or -1 when the matrix has more than 2 dimensions
    /// </summary>
    public int Rows
    {
        get
        {
            unsafe
            {
                return ((NativeMat*)Handle.DangerousGetHandle())->rows;
            }
        }
    }

    /// <summary>
    /// the number of columns or -1 when the matrix has more than 2 dimensions
    /// </summary>
    public int Cols
    {
        get
        {
            unsafe
            {
                return ((NativeMat*)Handle.DangerousGetHandle())->cols;
            }
        }
    }

    /// <summary>
    /// unsafe pointer to the data
    /// </summary>
    public unsafe byte* DataPointer => ((NativeMat*)Handle.DangerousGetHandle())->data;

    /// <summary>
    /// pointer to the data
    /// </summary>
    public IntPtr Data
    {
        get
        {
            unsafe
            {
                return new IntPtr(DataPointer);
            }
        }
    }

    internal int SafeRows => NativeMethods.core_Mat_rows(Handle);
    internal int SafeCols => NativeMethods.core_Mat_cols(Handle);
    internal IntPtr SafeData => NativeMethods.core_Mat_data(Handle);

    OutputArrayHandle IOutputArray.ToHandle()
    {
        throw new NotImplementedException();
    }

    InputOutputArrayHandle IInputOutputArray.ToHandle()
    {
        NativeMethods.HandleException(
            NativeMethods.core_InputOutputArray_new_byMat(Handle, out var resultHandle));
        GC.KeepAlive(this);
        return resultHandle;
    }
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
        NativeMethods.HandleException(
            NativeMethods.core_Mat_delete(handle));
        return true;
    }

    public override bool IsInvalid => handle == IntPtr.Zero;
}
