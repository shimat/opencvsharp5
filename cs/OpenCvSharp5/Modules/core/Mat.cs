using System.Runtime.InteropServices;
using OpenCvSharp5.Internal;

namespace OpenCvSharp5;

/// <summary>
/// n-dimensional dense array class
/// </summary>
public class Mat : IDisposable, IInputArray, IOutputArray, IInputOutputArray
{
    private readonly MatHandle handle;
    
    /// <summary>
    /// These are various constructors that form a matrix. As noted in the AutomaticAllocation, often
    /// the default constructor is enough, and the proper matrix will be allocated by an OpenCV function.
    /// The constructed matrix can further be assigned to another matrix or matrix expression or can be
    /// allocated with Mat::create.In the former case, the old content is de-referenced.
    /// </summary>
    public Mat()
    {
        NativeMethods.HandleException(
            NativeMethods.core_Mat_new1(out handle));
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
            NativeMethods.core_Mat_new2(row, col, type, out handle));
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
            NativeMethods.core_Mat_new3(row, col, type, s, out handle));
    }

    /// <summary>
    /// Protected implementation of Dispose pattern.
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
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
    
    /// <summary>
    /// includes several bit-fields:
    /// - the magic signature
    /// - continuity flag
    /// - depth
    /// - number of channels
    /// </summary>
    public int Flags => NativeMethods.core_Mat_flags(handle);
    
    /// <summary>
    /// the matrix dimensionality, >= 2
    /// </summary>
    public int Dims => NativeMethods.core_Mat_dims(handle);

    /// <summary>
    /// the number of rows or -1 when the matrix has more than 2 dimensions
    /// </summary>
    public int Rows => NativeMethods.core_Mat_rows(handle);

    /// <summary>
    /// the number of columns or -1 when the matrix has more than 2 dimensions
    /// </summary>
    public int Cols => NativeMethods.core_Mat_cols(handle);

    /// <summary>
    /// pointer to the data
    /// </summary>
    public IntPtr Data => NativeMethods.core_Mat_data(handle);
    
    /// <summary>
    /// Returns a matrix size.
    /// </summary>
    public Size Size() => NativeMethods.core_Mat_size(handle);
    
    /// <summary>
    /// Returns a matrix size.
    /// </summary>
    public int Size(int dim)
    {
        NativeMethods.HandleException(
            NativeMethods.core_Mat_sizeAt(handle, dim, out var result));
        return result;
    }

    /// <summary>
    /// Returns number of bytes each matrix row occupies.
    /// </summary>
    public nint Step() => NativeMethods.core_Mat_step(handle);
    
    /// <summary>
    /// Returns number of bytes each matrix row occupies.
    /// </summary>
    public nint Step(int dim)
    {
        NativeMethods.HandleException( 
            NativeMethods.core_Mat_stepAt(handle, dim, out var result));
        return result;
    }

    /// <summary>
    /// unsafe pointer to the data
    /// </summary>
    public unsafe byte* DataPointer => (byte*)Data;

    InputArrayHandle IInputArray.ToInputArrayHandle()
    {
        throw new NotImplementedException();
    }

    OutputArrayHandle IOutputArray.ToOutputArrayHandle()
    {
        throw new NotImplementedException();
    }

    InputOutputArrayHandle IInputOutputArray.ToInputOutputArrayHandle()
    {
        NativeMethods.HandleException(
            NativeMethods.core_InputOutputArray_new_byMat(handle, out var resultHandle));
        GC.KeepAlive(this);
        return resultHandle;
    }
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
