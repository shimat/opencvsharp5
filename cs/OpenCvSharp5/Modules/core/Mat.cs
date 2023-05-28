using System.Runtime.InteropServices;
using OpenCvSharp5.Internal;

namespace OpenCvSharp5;

/// <summary>
/// n-dimensional dense array class
/// </summary>
public class Mat : IDisposable, IInputArray, IOutputArray, IInputOutputArray, ISafeHandleHolder
{
    private readonly MatHandle handle;
    private GCHandle dataHandle;
    private volatile int disposeSignaled;
    
    /// <summary>
    /// </summary>
    public MatHandle Handle => handle;
    SafeHandle ISafeHandleHolder.Handle => handle;
    /// <inheritdoc />
    public bool IsDisposed => handle.IsClosed || handle.IsInvalid;

    #region Init & Disposal

    /// <summary>
    /// </summary>
    public Mat(MatHandle handle)
    {
        this.handle = handle;
    }
    
    /// <summary>
    /// Loads an image from a file. (cv::imread)
    /// </summary>
    /// <param name="fileName">Name of file to be loaded.</param>
    /// <param name="flags">Specifies color type of the loaded image</param>
    public Mat(string fileName, ImreadModes flags = ImreadModes.Color)
    {
        ArgumentException.ThrowIfNullOrEmpty(fileName);

        NativeMethods.HandleException(
            NativeMethods.imgcodecs_imread(fileName, (int) flags, out handle));
    }

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
    /// constructs 2D matrix of the specified size and type
    /// </summary>
    /// <param name="size">2D array size: Size(cols, rows) . In the Size() constructor, 
    /// the number of rows and the number of columns go in the reverse order.</param>
    /// <param name="type">Array type. Use MatType.CV_8UC1, ..., CV_64FC4 to create 1-4 channel matrices, 
    /// or MatType.CV_8UC(n), ..., CV_64FC(n) to create multi-channel matrices.</param>
    public Mat(Size size, MatType type)
        : this(size.Height, size.Width, type)
    {
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
    /// constructs 2D matrix and fills it with the specified Scalar value.
    /// </summary>
    /// <param name="size">2D array size: Size(cols, rows) . In the Size() constructor, 
    /// the number of rows and the number of columns go in the reverse order.</param>
    /// <param name="type">Array type. Use MatType.CV_8UC1, ..., CV_64FC4 to create 1-4 channel matrices, 
    /// or CV_8UC(n), ..., CV_64FC(n) to create multi-channel (up to CV_CN_MAX channels) matrices.</param>
    /// <param name="s">An optional value to initialize each matrix element with. 
    /// To set all the matrix elements to the particular value after the construction, use SetTo(Scalar s) method .</param>
    public Mat(Size size, MatType type, Scalar s)
        : this(size.Height, size.Width, type, s)
    {
    }
    
    /// <summary>
    /// constructs n-dimensional matrix
    /// </summary>
    /// <param name="sizes">Array of integers specifying an n-dimensional array shape.</param>
    /// <param name="type">Array type. Use MatType.CV_8UC1, ..., CV_64FC4 to create 1-4 channel matrices, 
    /// or MatType. CV_8UC(n), ..., CV_64FC(n) to create multi-channel matrices.</param>
    public Mat(IEnumerable<int> sizes, MatType type)
    {
        ArgumentNullException.ThrowIfNull(sizes);

        var sizesArray = sizes as int[] ?? sizes.ToArray();
        NativeMethods.HandleException(
            NativeMethods.core_Mat_new4(sizesArray.Length, sizesArray, type, out handle));
    }

    /// <summary>
    /// constructs n-dimensional matrix
    /// </summary>
    /// <param name="sizes">Array of integers specifying an n-dimensional array shape.</param>
    /// <param name="type">Array type. Use MatType.CV_8UC1, ..., CV_64FC4 to create 1-4 channel matrices, 
    /// or MatType. CV_8UC(n), ..., CV_64FC(n) to create multi-channel matrices.</param>
    /// <param name="s">An optional value to initialize each matrix element with. 
    /// To set all the matrix elements to the particular value after the construction, use SetTo(Scalar s) method .</param>
    public Mat(IEnumerable<int> sizes, MatType type, Scalar s)
    {
        ArgumentNullException.ThrowIfNull(sizes);

        var sizesArray = sizes as int[] ?? sizes.ToArray();
        NativeMethods.HandleException(
            NativeMethods.core_Mat_new5(sizesArray.Length, sizesArray, type, s, out handle));
    }

    /// <summary>
    /// </summary>
    /// <param name="m">Array that (as a whole or partly) is assigned to the constructed matrix.
    /// No data is copied by these constructors. Instead, the header pointing to m data or its sub-array is constructed and
    /// associated with it. The reference counter, if any, is incremented. So, when you modify the matrix formed using such a constructor,
    /// you also modify the corresponding elements of m . If you want to have an independent copy of the sub-array, use Mat::clone() .</param>
    protected Mat(Mat m)
    {
        ArgumentNullException.ThrowIfNull(m);

        m.ThrowIfDisposed();

        NativeMethods.HandleException(
            NativeMethods.core_Mat_new6(m.handle, out handle));
    }

    /// <summary>
    /// constructor for matrix headers pointing to user-allocated data
    /// </summary>
    /// <param name="rows">Number of rows in a 2D array.</param>
    /// <param name="cols">Number of columns in a 2D array.</param>
    /// <param name="type">Array type. Use MatType.CV_8UC1, ..., CV_64FC4 to create 1-4 channel matrices, 
    /// or MatType. CV_8UC(n), ..., CV_64FC(n) to create multi-channel matrices.</param>
    /// <param name="data">Pointer to the user data. Matrix constructors that take data and step parameters do not allocate matrix data. 
    /// Instead, they just initialize the matrix header that points to the specified data, which means that no data is copied. 
    /// This operation is very efficient and can be used to process external data using OpenCV functions. 
    /// The external data is not automatically de-allocated, so you should take care of it.</param>
    /// <param name="step">Number of bytes each matrix row occupies. The value should include the padding bytes at the end of each row, if any.
    /// If the parameter is missing (set to AUTO_STEP ), no padding is assumed and the actual step is calculated as cols*elemSize() .</param>
    public Mat(int rows, int cols, MatType type, IntPtr data, nint step = 0)
    {
        NativeMethods.HandleException(
            NativeMethods.core_Mat_new7(rows, cols, type, data, new IntPtr(step), out handle));
    }

    /// <summary>
    /// constructor for matrix headers pointing to user-allocated data
    /// </summary>
    /// <param name="rows">Number of rows in a 2D array.</param>
    /// <param name="cols">Number of columns in a 2D array.</param>
    /// <param name="type">Array type. Use MatType.CV_8UC1, ..., CV_64FC4 to create 1-4 channel matrices, 
    /// or MatType. CV_8UC(n), ..., CV_64FC(n) to create multi-channel matrices.</param>
    /// <param name="data">Pointer to the user data. Matrix constructors that take data and step parameters do not allocate matrix data. 
    /// Instead, they just initialize the matrix header that points to the specified data, which means that no data is copied. 
    /// This operation is very efficient and can be used to process external data using OpenCV functions. 
    /// The external data is not automatically de-allocated, so you should take care of it.</param>
    /// <param name="step">Number of bytes each matrix row occupies. The value should include the padding bytes at the end of each row, if any.
    /// If the parameter is missing (set to AUTO_STEP ), no padding is assumed and the actual step is calculated as cols*elemSize() .</param>
    public Mat(int rows, int cols, MatType type, Array data, nint step = 0)
    {
        dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
        NativeMethods.HandleException(
            NativeMethods.core_Mat_new7(rows, cols, type,
                dataHandle.AddrOfPinnedObject(), new IntPtr(step), out handle));
    }

    /// <summary>
    /// constructor for matrix headers pointing to user-allocated data
    /// </summary>
    /// <param name="size">2D array size: Size(cols, rows) . In the Size() constructor, the number of rows and the
    /// number of columns go in the reverse order.</param>
    /// <param name="type">Array type. Use CV_8UC1, ..., CV_64FC4 to create 1-4 channel matrices, or
    /// CV_8UC(n), ..., CV_64FC(n) to create multi-channel (up to CV_CN_MAX channels) matrices.</param>
    /// <param name="data">Pointer to the user data. Matrix constructors that take data and step parameters do not
    /// allocate matrix data. Instead, they just initialize the matrix header that points to the specified
    /// data, which means that no data is copied. This operation is very efficient and can be used to
    /// process external data using OpenCV functions. The external data is not automatically deallocated, so
    /// you should take care of it.</param>
    /// <param name="step">Number of bytes each matrix row occupies. The value should include the padding bytes at
    /// the end of each row, if any. If the parameter is missing (set to AUTO_STEP ), no padding is assumed
    /// and the actual step is calculated as cols*elemSize(). See Mat::elemSize.</param>
    public Mat(Size size, int type, IntPtr data, nint step = 0)
        : this(size.Height, size.Width, type, data, step)
    {
    }

    /// <summary>
    /// constructor for matrix headers pointing to user-allocated data
    /// </summary>
    /// <param name="sizes">Array of integers specifying an n-dimensional array shape.</param>
    /// <param name="type">Array type. Use MatType.CV_8UC1, ..., CV_64FC4 to create 1-4 channel matrices, 
    /// or MatType. CV_8UC(n), ..., CV_64FC(n) to create multi-channel matrices.</param>
    /// <param name="data">Pointer to the user data. Matrix constructors that take data and step parameters do not allocate matrix data. 
    /// Instead, they just initialize the matrix header that points to the specified data, which means that no data is copied. 
    /// This operation is very efficient and can be used to process external data using OpenCV functions. 
    /// The external data is not automatically de-allocated, so you should take care of it.</param>
    /// <param name="steps">Array of ndims-1 steps in case of a multi-dimensional array (the last step is always set to the element size). 
    /// If not specified, the matrix is assumed to be continuous.</param>
    public Mat(IEnumerable<int> sizes, MatType type, IntPtr data, IEnumerable<nint>? steps = null)
    {
        ArgumentNullException.ThrowIfNull(sizes);
        ArgumentNullException.ThrowIfNull(data);

        var sizesArray = sizes as int[] ?? sizes.ToArray();
        var stepsArray = steps?.ToArray();
        NativeMethods.HandleException(
            NativeMethods.core_Mat_new8(sizesArray.Length, sizesArray, type, data, stepsArray, out handle));
    }

    /// <summary>
    /// constructor for matrix headers pointing to user-allocated data
    /// </summary>
    /// <param name="sizes">Array of integers specifying an n-dimensional array shape.</param>
    /// <param name="type">Array type. Use MatType.CV_8UC1, ..., CV_64FC4 to create 1-4 channel matrices, 
    /// or MatType. CV_8UC(n), ..., CV_64FC(n) to create multi-channel matrices.</param>
    /// <param name="data">Pointer to the user data. Matrix constructors that take data and step parameters do not allocate matrix data. 
    /// Instead, they just initialize the matrix header that points to the specified data, which means that no data is copied. 
    /// This operation is very efficient and can be used to process external data using OpenCV functions. 
    /// The external data is not automatically de-allocated, so you should take care of it.</param>
    /// <param name="steps">Array of ndims-1 steps in case of a multi-dimensional array (the last step is always set to the element size). 
    /// If not specified, the matrix is assumed to be continuous.</param>
    public Mat(IEnumerable<int> sizes, MatType type, Array data, IEnumerable<nint>? steps = null)
    {
        ArgumentNullException.ThrowIfNull(sizes);
        ArgumentNullException.ThrowIfNull(data);

        dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);

        var sizesArray = sizes as int[] ?? sizes.ToArray();

        var stepsArray = steps?.ToArray();
        NativeMethods.HandleException(
            NativeMethods.core_Mat_new8(sizesArray.Length, sizesArray,
                type, dataHandle.AddrOfPinnedObject(), stepsArray, out handle));
    }
    
    /// <summary>
    /// creates a matrix header for a part of the bigger matrix
    /// </summary>
    /// <param name="m">Array that (as a whole or partly) is assigned to the constructed matrix. 
    /// No data is copied by these constructors. Instead, the header pointing to m data or its sub-array 
    /// is constructed and associated with it. The reference counter, if any, is incremented. 
    /// So, when you modify the matrix formed using such a constructor, you also modify the corresponding elements of m . 
    /// If you want to have an independent copy of the sub-array, use Mat::clone() .</param>
    /// <param name="rowRange">Range of the m rows to take. As usual, the range start is inclusive and the range end is exclusive. 
    /// Use Range.All to take all the rows.</param>
    /// <param name="colRange">Range of the m columns to take. Use Range.All to take all the columns.</param>
    public Mat(Mat m, Range rowRange, Range? colRange = null)
    {
        ArgumentNullException.ThrowIfNull(m);
        m.ThrowIfDisposed();

        colRange ??= Range.All;
        NativeMethods.HandleException(NativeMethods.core_Mat_new9(m.handle, rowRange, colRange.Value, out handle));
    }
    
    /// <summary>
    /// creates a matrix header for a part of the bigger matrix
    /// </summary>
    /// <param name="m">Array that (as a whole or partly) is assigned to the constructed matrix. 
    /// No data is copied by these constructors. Instead, the header pointing to m data or its sub-array 
    /// is constructed and associated with it. The reference counter, if any, is incremented. 
    /// So, when you modify the matrix formed using such a constructor, you also modify the corresponding elements of m . 
    /// If you want to have an independent copy of the sub-array, use Mat.Clone() .</param>
    /// <param name="roi">Region of interest.</param>
    public Mat(Mat m, Rect roi)
    {
        ArgumentNullException.ThrowIfNull(m);
        m.ThrowIfDisposed();

        NativeMethods.HandleException(
            NativeMethods.core_Mat_new10(m.handle, roi, out handle));
        GC.KeepAlive(m);
    }
    
    /// <summary>
    /// creates a matrix header for a part of the bigger matrix
    /// </summary>
    /// <param name="m">Array that (as a whole or partly) is assigned to the constructed matrix. 
    /// No data is copied by these constructors. Instead, the header pointing to m data or its sub-array 
    /// is constructed and associated with it. The reference counter, if any, is incremented. 
    /// So, when you modify the matrix formed using such a constructor, you also modify the corresponding elements of m . 
    /// If you want to have an independent copy of the sub-array, use Mat.Clone() .</param>
    /// <param name="ranges">Array of selected ranges of m along each dimensionality.</param>
    public Mat(Mat m, params Range[] ranges)
    {
        ArgumentNullException.ThrowIfNull(m);
        ArgumentNullException.ThrowIfNull(ranges);
        if (ranges.Length == 0)
            throw new ArgumentException("empty ranges", nameof(ranges));
        m.ThrowIfDisposed();

        NativeMethods.HandleException(
            NativeMethods.core_Mat_new11(m.handle, ranges, out handle));
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
    ~Mat()
    {
        Dispose(false);
    }

    #endregion
    
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

    /// <summary>
    /// returns element type, similar to CV_MAT_TYPE(cvmat->type)
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ObjectDisposedException"></exception>
    public int Type()
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);

        NativeMethods.HandleException(
            NativeMethods.core_Mat_type(handle, out var ret));
        GC.KeepAlive(this);

        return ret;
    }

    /// <summary>
    /// returns element type, similar to CV_MAT_DEPTH(cvmat->type)
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ObjectDisposedException"></exception>
    public int Depth()
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);

        NativeMethods.HandleException(
            NativeMethods.core_Mat_depth(handle, out var ret));
        GC.KeepAlive(this);

        return ret;
    }

    /// <summary>
    /// returns element type, similar to CV_MAT_CN(cvmat->type)
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ObjectDisposedException"></exception>
    public int Channels()
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);

        NativeMethods.HandleException(
            NativeMethods.core_Mat_channels(handle, out var ret));
        GC.KeepAlive(this);

        return ret;
    }

    /// <summary>
    /// returns true if matrix data is NULL
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ObjectDisposedException"></exception>
    public bool Empty()
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);

        NativeMethods.HandleException(
            NativeMethods.core_Mat_empty(handle, out var ret));
        GC.KeepAlive(this);

        return ret != 0;
    }
 
    /// <summary>
    /// returns the total number of matrix elements
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ObjectDisposedException"></exception>
    public nint Total()
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);

        NativeMethods.HandleException(
            NativeMethods.core_Mat_total(handle, out var ret));
        GC.KeepAlive(this);

        return ret;
    }



    InputArrayHandle IInputArray.ToInputArrayHandle()
    {
        NativeMethods.HandleException(
            NativeMethods.core_InputArray_new_byMat(handle, out var resultHandle));
        GC.KeepAlive(this);
        return resultHandle;
    }

    OutputArrayHandle IOutputArray.ToOutputArrayHandle() => throw new NotImplementedException();

    InputOutputArrayHandle IInputOutputArray.ToInputOutputArrayHandle()
    {
        NativeMethods.HandleException(
            NativeMethods.core_InputOutputArray_new_byMat(handle, out var resultHandle));
        GC.KeepAlive(this);
        return resultHandle;
    }
}

/// <summary>
/// </summary>
public class MatHandle : SafeHandle
{
    /// <summary>
    /// </summary>
    public MatHandle()
        : base(invalidHandleValue: IntPtr.Zero, ownsHandle: true)
    {
    }

    /// <inheritdoc />
    protected override bool ReleaseHandle()
    {
        NativeMethods.HandleException(
            NativeMethods.core_Mat_delete(handle));
        return true;
    }

    /// <inheritdoc />
    public override bool IsInvalid => handle == IntPtr.Zero;
}
