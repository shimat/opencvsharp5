using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
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
    
    #region Fields

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
    public nint Data => NativeMethods.core_Mat_data(handle);

    /// <summary>
    /// unsafe pointer to the data
    /// </summary>
    public unsafe byte* DataPointer => (byte*)Data;
    
    /// <summary>
    /// Returns a matrix size.
    /// </summary>
    [Pure]
    public Size Size() => NativeMethods.core_Mat_size(handle);

    /// <summary>
    /// Returns a matrix size.
    /// </summary>
    [Pure]
    public int Size(int dim)
    {
        NativeMethods.HandleException(
            NativeMethods.core_Mat_sizeAt(handle, dim, out var result));
        return result;
    }

    /// <summary>
    /// Returns number of bytes each matrix row occupies.
    /// </summary>
    [Pure]
    public nint Step() => NativeMethods.core_Mat_step(handle);

    /// <summary>
    /// Returns number of bytes each matrix row occupies.
    /// </summary>
    [Pure]
    public nint Step(int dim)
    {
        NativeMethods.HandleException(
            NativeMethods.core_Mat_stepAt(handle, dim, out var result));
        return result;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Creates a matrix header for the specified matrix row.
    /// </summary>
    /// <param name="y">A 0-based row index.</param>
    /// <returns></returns>
    [Pure]
    public Mat Row(int y)
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);

        NativeMethods.HandleException(
            NativeMethods.core_Mat_row(handle, y, out var matPtr));

        GC.KeepAlive(this);
        return new Mat(matPtr);
    }

    /// <summary>
    /// Creates a matrix header for the specified matrix column.
    /// </summary>
    /// <param name="x">A 0-based column index.</param>
    /// <returns></returns>
    [Pure]
    public Mat Col(int x)
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);

        NativeMethods.HandleException(
            NativeMethods.core_Mat_col(handle, x, out var matPtr));

        GC.KeepAlive(this);
        return new Mat(matPtr);
    }

    /// <summary>
    /// Creates a matrix header for the specified row span.
    ///
    /// The method makes a new header for the specified row span of the matrix. Similarly to Mat::row and Mat::col , this is an O(1) operation.
    /// </summary>
    /// <param name="startRow">An inclusive 0-based start index of the row span.</param>
    /// <param name="endRow">An exclusive 0-based ending index of the row span.</param>
    /// <returns></returns>
    /// <exception cref="ObjectDisposedException"></exception>
    [Pure]
    public Mat RowRange(int startRow, int endRow)
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);

        NativeMethods.HandleException(
            NativeMethods.core_Mat_rowRange1(handle, startRow, endRow, out var matPtr));

        GC.KeepAlive(this);
        return new Mat(matPtr);
    }

    /// <summary>
    /// Creates a matrix header for the specified row span.
    ///
    /// The method makes a new header for the specified row span of the matrix. Similarly to Mat::row and Mat::col , this is an O(1) operation.
    /// </summary>
    /// <param name="r">Range structure containing both the start and the end indices.</param>
    /// <returns></returns>
    /// <exception cref="ObjectDisposedException"></exception>
    [Pure]
    public Mat RowRange(Range r)
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);

        NativeMethods.HandleException(
            NativeMethods.core_Mat_rowRange2(handle, r, out var matPtr));

        GC.KeepAlive(this);
        return new Mat(matPtr);
    }

    /// <summary>
    /// Creates a matrix header for the specified column span.
    ///
    /// The method makes a new header for the specified column span of the matrix. Similarly to Mat::row and Mat::col , this is an O(1) operation.
    /// </summary>
    /// <param name="startCol">An inclusive 0-based start index of the column span.</param>
    /// <param name="endCol">An exclusive 0-based ending index of the column span.</param>
    /// <returns></returns>
    /// <exception cref="ObjectDisposedException"></exception>
    [Pure]
    public Mat ColRange(int startCol, int endCol)
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);
        NativeMethods.HandleException(
            NativeMethods.core_Mat_colRange1(handle, startCol, endCol, out var matPtr));

        GC.KeepAlive(this);
        return new Mat(matPtr);
    }

    /// <summary> 
    /// Creates a matrix header for the specified column span.
    ///
    /// The method makes a new header for the specified column span of the matrix. Similarly to Mat::row and Mat::col , this is an O(1) operation.
    /// </summary>
    /// <param name="r">Range structure containing both the start and the end indices.</param>
    /// <returns></returns>
    /// <exception cref="ObjectDisposedException"></exception>
    [Pure]
    public Mat ColRange(Range r)
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);

        NativeMethods.HandleException(
            NativeMethods.core_Mat_colRange2(handle, r, out var matPtr));

        GC.KeepAlive(this);
        return new Mat(matPtr);
    }

    /// <summary>
    /// Extracts a diagonal from a matrix.
    ///
    /// The method makes a new header for the specified matrix diagonal. The new matrix is represented as a
    /// single-column matrix. Similarly to Mat::row and Mat::col, this is an O(1) operation.
    /// </summary>
    /// <param name="d">index of the diagonal, with the following values:
    /// - d=0 is the main diagonal.
    /// - d&lt;0 is a diagonal from the lower half. For example, d=-1 means the diagonal is set
    /// immediately below the main one.
    /// - `d&gt;0` is a diagonal from the upper half. For example, d=1 means the diagonal is set
    /// immediately above the main one.</param>
    /// <returns></returns>
    /// <exception cref="ObjectDisposedException"></exception>
    [Pure]
    public Mat Diag(int d = 0)
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);
        
        NativeMethods.HandleException(
            NativeMethods.core_Mat_diag(handle, d, out var matPtr));

        GC.KeepAlive(this);
        return new Mat(matPtr);
    }
    
    /// <summary>
    /// creates a diagonal matrix.
    ///
    /// The method creates a square diagonal matrix from specified main diagonal.
    /// </summary>
    /// <param name="d">One-dimensional matrix that represents the main diagonal.</param>
    /// <returns></returns>
    public static Mat Diag(Mat d) => d.Diag();

    /// <summary>
    /// Creates a full copy of the array and the underlying data.
    ///
    /// The method creates a full copy of the array. The original step[] is not taken into account. So, the
    /// array copy is a continuous array occupying total()*elemSize() bytes.
    /// </summary>
    /// <returns></returns>
    [Pure]
    public Mat Clone()
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);
        
        NativeMethods.HandleException(
            NativeMethods.core_Mat_clone(handle, out var matPtr));

        GC.KeepAlive(this);
        return new Mat(matPtr);
    }

    /// <summary>
    /// Copies the matrix to another one.
    /// </summary>
    /// <param name="m"></param>
    public void CopyTo(IOutputArray m)
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);
        ThrowIfNull(m);

        using var mHandle = m.ToOutputArrayHandle();
        NativeMethods.HandleException(
            NativeMethods.core_Mat_copyTo1(handle, mHandle));

        GC.KeepAlive(this);
        GC.KeepAlive(m);
    }

    /// <summary>
    /// Copies the matrix to another one.
    /// </summary>
    /// <param name="m">Destination matrix. If it does not have a proper size or type before the operation, it is reallocated.</param>
    /// <param name="mask">Operation mask of the same size as \*this. Its non-zero elements indicate which matrix
    /// elements need to be copied. The mask has to be of type CV_8U and can have 1 or multiple channels.</param>
    /// <exception cref="ObjectDisposedException"></exception>
    public void CopyTo(IOutputArray m, IInputArray mask)
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);
        ThrowIfNull(m);

        using var mHandle = m.ToOutputArrayHandle();
        using var maskHandle = m.ToInputArrayHandle();
        NativeMethods.HandleException(
            NativeMethods.core_Mat_copyTo2(handle, mHandle, maskHandle));

        GC.KeepAlive(this);
        GC.KeepAlive(m);
        GC.KeepAlive(mask);
    }

    /// <summary>
    /// Converts an array to another data type with optional scaling.
    ///
    /// 
    /// </summary>
    /// <param name="m">output matrix; if it does not have a proper size or type before the operation, it is reallocated.</param>
    /// <param name="rtype">desired output matrix type or, rather, the depth since the number of channels are the
    /// same as the input has; if rtype is negative, the output matrix will have the same type as the input.</param>
    /// <param name="alpha">optional scale factor.</param>
    /// <param name="beta">optional delta added to the scaled values.</param>
    /// <exception cref="ObjectDisposedException"></exception>
    public void ConvertTo(IOutputArray m, MatType rtype, double alpha = 1, double beta = 0)
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);
        ThrowIfNull(m);

        using var mHandle = m.ToOutputArrayHandle();
        NativeMethods.HandleException(
            NativeMethods.core_Mat_convertTo(handle, mHandle, rtype, alpha, beta));

        GC.KeepAlive(this);
        GC.KeepAlive(m);
    }
    
    /// <summary>
    /// Provides a functional form of convertTo.
    ///
    /// This is an internally used method called by the @ref MatrixExpressions engine.
    /// </summary>
    /// <param name="m">Destination array.</param>
    /// <param name="type">Desired destination array depth (or -1 if it should be the same as the source type).</param>
    /// <exception cref="ObjectDisposedException"></exception>
    public void AssignTo(Mat m, MatType? type = null)
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);
        ThrowIfNull(m);

        int typeValue = type.GetValueOrDefault(-1);
        NativeMethods.HandleException(
            NativeMethods.core_Mat_assignTo(handle, m.handle, typeValue));

        GC.KeepAlive(this);
        GC.KeepAlive(m);
    }
    
    /// <summary>
    /// Sets all or some of the array elements to the specified value.
    ///
    /// This is an advanced variant of the Mat::operator=(const Scalar&amp; s) operator.
    /// </summary>
    /// <param name="value">Assigned scalar converted to the actual array type.</param>
    /// <param name="mask">Operation mask of the same size as \*this. Its non-zero elements indicate which matrix 
    /// elements need to be copied. The mask has to be of type CV_8U and can have 1 or multiple channels</param>
    /// <returns></returns>
    /// <exception cref="ObjectDisposedException"></exception>
    public Mat SetTo(IInputArray value, IInputArray? mask = null)
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);
        ThrowIfNull(value);

        using var valueHandle = value.ToInputArrayHandle();
        using var maskHandle = mask?.ToInputArrayHandle();
        NativeMethods.HandleException(
            NativeMethods.core_Mat_setTo1(handle, valueHandle, maskHandle));

        GC.KeepAlive(this);
        GC.KeepAlive(value);
        GC.KeepAlive(mask);

        return this;
    }
    
    /// <summary>
    /// Sets all or some of the array elements to the specified value.
    ///
    /// This is an advanced variant of the Mat::operator=(const Scalar&amp; s) operator.
    /// </summary>
    /// <param name="value">Assigned scalar converted to the actual array type.</param>
    /// <param name="mask">Operation mask of the same size as \*this. Its non-zero elements indicate which matrix 
    /// elements need to be copied. The mask has to be of type CV_8U and can have 1 or multiple channels</param>
    /// <returns></returns>
    /// <exception cref="ObjectDisposedException"></exception>
    public Mat SetTo(Scalar value, IInputArray? mask = null)
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);
        ThrowIfNull(value);
        
        using var maskHandle = mask?.ToInputArrayHandle();
        NativeMethods.HandleException(
            NativeMethods.core_Mat_setTo2(handle, value, maskHandle));

        GC.KeepAlive(this);
        GC.KeepAlive(value);
        GC.KeepAlive(mask);

        return this;
    }

    /// <summary>
    /// Sets all the array elements to 0.
    /// </summary>
    /// <returns></returns>
    public Mat SetZero()
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);
        
        NativeMethods.HandleException(
            NativeMethods.core_Mat_setZero(handle));

        GC.KeepAlive(this);
        return this;
    }

    /// <summary>
    /// Changes the shape and/or the number of channels of a 2D matrix without copying the data.
    ///
    /// The method makes a new matrix header for \*this elements. The new matrix may have a different size
    /// and/or different number of channels. Any combination is possible if:
    /// -   No extra elements are included into the new matrix and no elements are excluded. Consequently,
    /// the product rows\*cols\*channels() must stay the same after the transformation.
    /// -   No data is copied. That is, this is an O(1) operation. Consequently, if you change the number of
    /// rows, or the operation changes the indices of elements row in some other way, the matrix must be
    /// continuous. See Mat::isContinuous .
    /// </summary>
    /// <param name="cn">New number of channels. If the parameter is 0, the number of channels remains the same.</param>
    /// <param name="rows">New number of rows. If the parameter is 0, the number of rows remains the same.</param>
    /// <returns></returns>
    [Pure]
    public Mat Reshape(int cn, int rows = 0)
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);
        
        NativeMethods.HandleException(
            NativeMethods.core_Mat_reshape1(handle, cn, rows, out var outMatHandle));

        GC.KeepAlive(this);
        return new Mat(outMatHandle);
    }

    /// <summary>
    /// Changes the shape and/or the number of channels of a 2D matrix without copying the data.
    ///
    /// The method makes a new matrix header for \*this elements. The new matrix may have a different size
    /// and/or different number of channels. Any combination is possible if:
    /// -   No extra elements are included into the new matrix and no elements are excluded. Consequently,
    /// the product rows\*cols\*channels() must stay the same after the transformation.
    /// -   No data is copied. That is, this is an O(1) operation. Consequently, if you change the number of
    /// rows, or the operation changes the indices of elements row in some other way, the matrix must be
    /// continuous. See Mat::isContinuous .
    /// </summary>
    /// <param name="cn">New number of channels. If the parameter is 0, the number of channels remains the same.</param>
    /// <param name="newShape"></param>
    /// <returns></returns>
    [Pure]
    public Mat Reshape(int cn, IReadOnlyCollection<int> newShape)
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);
        ThrowIfNull(newShape);
        
        NativeMethods.HandleException(
            NativeMethods.core_Mat_reshape2(
                handle, cn, newShape.Count, newShape.ToArray(), out var outMatHandle));

        GC.KeepAlive(this);
        return new Mat(outMatHandle);
    }

    /// <summary>
    /// Reports whether the matrix is continuous or not.
    /// </summary>
    /// <returns></returns>
    [Pure]
    public bool IsContinuous()
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);
        
        NativeMethods.HandleException(
            NativeMethods.core_Mat_isContinuous(handle, out var ret));
        GC.KeepAlive(this);

        return ret != 0;
    }
    
    /// <summary>
    /// returns true if the matrix is a submatrix of another matrix
    /// </summary>
    /// <returns></returns>
    [Pure]
    public bool IsSubmatrix()
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);
        
        NativeMethods.HandleException(
            NativeMethods.core_Mat_isSubmatrix(handle, out var ret));
        GC.KeepAlive(this);

        return ret != 0;
    }
    
    /// <summary>
    /// Returns the matrix element size in bytes.
    ///
    /// The method returns the matrix element size in bytes. For example, if the matrix type is CV_16SC3 ,
    /// the method returns 3\*sizeof(short) or 6.
    /// </summary>
    /// <returns></returns>
    [Pure]
    public nint ElemSize()
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);
        
        NativeMethods.HandleException(
            NativeMethods.core_Mat_elemSize(handle, out var ret));
        GC.KeepAlive(this);

        return ret;
    }

    /// <summary>
    /// Returns the size of each matrix element channel in bytes.
    ///
    /// The method returns the matrix element channel size in bytes, that is, it ignores the number of 
    /// channels. For example, if the matrix type is CV_16SC3 , the method returns sizeof(short) or 2.
    /// </summary>
    /// <returns></returns>
    [Pure]
    public nint ElemSize1()
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);
        
        NativeMethods.HandleException(
            NativeMethods.core_Mat_elemSize1(handle, out var ret));
        GC.KeepAlive(this);

        return ret;
    }

    /// <summary>
    /// returns element type, similar to CV_MAT_TYPE(cvmat->type)
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ObjectDisposedException"></exception>
    [Pure]
    public MatType Type()
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
    [Pure]
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
    [Pure]
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
    [Pure]
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
    [Pure]
    public nint Total()
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);

        NativeMethods.HandleException(
            NativeMethods.core_Mat_total(handle, out var ret));
        GC.KeepAlive(this);

        return ret;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="elemChannels">Number of channels or number of columns the matrix should have.
    /// For a 2-D matrix, when the matrix has only 1 column, then it should have 
    /// elemChannels channels; When the matrix has only 1 channel, 
    /// then it should have elemChannels columns. 
    /// For a 3-D matrix, it should have only one channel. Furthermore, 
    /// if the number of planes is not one, then the number of rows 
    /// within every plane has to be 1; if the number of rows within 
    /// every plane is not 1, then the number of planes has to be 1.</param>
    /// <param name="depth">The depth the matrix should have. Set it to -1 when any depth is fine.</param>
    /// <param name="requireContinuous">Set it to true to require the matrix to be continuous</param>
    /// <returns>-1 if the requirement is not satisfied. 
    /// Otherwise, it returns the number of elements in the matrix. Note 
    /// that an element may have multiple channels.</returns>
    /// <exception cref="ObjectDisposedException"></exception>
    public int CheckVector(int elemChannels, int depth = -1, bool requireContinuous = true)
    {
        if (disposeSignaled != 0)
            throw new ObjectDisposedException(GetType().Name);
        
        NativeMethods.HandleException(
            NativeMethods.core_Mat_checkVector(
                handle, elemChannels, depth, requireContinuous ? 1: 0, out var ret));
        GC.KeepAlive(this);

        return ret;
    }
    
#pragma warning disable CA1720

    /// <summary>
    /// Returns a pointer to the specified matrix row.
    /// </summary>
    /// <param name="i0">A 0-based row index.</param>
    /// <returns></returns>
    public unsafe byte* Ptr(int i0 = 0) =>
            (byte*)NativeMethods.core_Mat_ptr1(handle, i0);

    /// <summary>
    /// Returns a pointer to the specified matrix row.
    /// </summary>
    /// <param name="i0">A 0-based row index.</param>
    /// <returns></returns>
    public unsafe T* Ptr<T>(int i0 = 0) where T : unmanaged =>
        (T*)NativeMethods.core_Mat_ptr1(handle, i0);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="row">Index along the dimension 0</param>
    /// <param name="col">Index along the dimension 1</param>
    /// <returns></returns>
    public unsafe byte* Ptr(int row, int col) =>
        (byte*)NativeMethods.core_Mat_ptr2(handle, row, col);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="row">Index along the dimension 0</param>
    /// <param name="col">Index along the dimension 1</param>
    /// <returns></returns>
    public unsafe T* Ptr<T>(int row, int col) where T : unmanaged =>
        (T*)NativeMethods.core_Mat_ptr2(handle, row, col);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="i0"></param>
    /// <param name="i1"></param>
    /// <param name="i2"></param>
    /// <returns></returns>
    public unsafe byte* Ptr(int i0, int i1, int i2) =>
        (byte*)NativeMethods.core_Mat_ptr3(handle, i0, i1, i2);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="i0"></param>
    /// <param name="i1"></param>
    /// <param name="i2"></param>
    /// <returns></returns>
    public unsafe T* Ptr<T>(int i0, int i1, int i2) where T : unmanaged =>
        (T*)NativeMethods.core_Mat_ptr3(handle, i0, i1, i2);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="idx"></param>
    /// <returns></returns>
    public unsafe byte* Ptr(params int[] idx) =>
        (byte*)NativeMethods.core_Mat_ptrNd(handle, idx);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="idx"></param>
    /// <returns></returns>
    public unsafe T* Ptr<T>(params int[] idx) where T : unmanaged =>
        (T*)NativeMethods.core_Mat_ptrNd(handle, idx);
    
#pragma warning restore CA1720
    
    /// <summary>
    /// Returns a value to the specified array element.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="i0">Index along the dimension 0</param>
    /// <returns>A value to the specified array element.</returns>
    public unsafe T Get<T>(int i0) where T : struct
    {
        var p = Ptr(i0);
        return Unsafe.Read<T>(p);
    }

    /// <summary>
    /// Returns a value to the specified array element.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="i0">Index along the dimension 0</param>
    /// <param name="i1">Index along the dimension 1</param>
    /// <returns>A value to the specified array element.</returns>
    public unsafe T Get<T>(int i0, int i1) where T : struct
    {
        var p = Ptr(i0, i1);
        return Unsafe.Read<T>(p);
    }

    /// <summary>
    /// Returns a value to the specified array element.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="i0">Index along the dimension 0</param>
    /// <param name="i1">Index along the dimension 1</param>
    /// <param name="i2">Index along the dimension 2</param>
    /// <returns>A value to the specified array element.</returns>
    public unsafe T Get<T>(int i0, int i1, int i2) where T : struct
    {
        var p = Ptr(i0, i1, i2);
        return Unsafe.Read<T>(p);
    }

    /// <summary>
    /// Returns a value to the specified array element.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="idx">Array of Mat::dims indices.</param>
    /// <returns>A value to the specified array element.</returns>
    public unsafe T Get<T>(params int[] idx) where T : struct
    {
        var p = Ptr(idx);
        return Unsafe.Read<T>(p);
    }

    /// <summary>
    /// Returns a value to the specified array element.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="i0">Index along the dimension 0</param>
    /// <returns>A value to the specified array element.</returns>
    public unsafe ref T At<T>(int i0) where T : unmanaged
    {
        var p = Ptr(i0);
        return ref Unsafe.AsRef<T>(p);
    }

    /// <summary>
    /// Returns a value to the specified array element.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="i0">Index along the dimension 0</param>
    /// <param name="i1">Index along the dimension 1</param>
    /// <returns>A value to the specified array element.</returns>
    public unsafe ref T At<T>(int i0, int i1) where T : unmanaged
    {
        var p = Ptr(i0, i1);
        return ref Unsafe.AsRef<T>(p);
    }

    /// <summary>
    /// Returns a value to the specified array element.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="i0">Index along the dimension 0</param>
    /// <param name="i1">Index along the dimension 1</param>
    /// <param name="i2">Index along the dimension 2</param>
    /// <returns>A value to the specified array element.</returns>
    public unsafe ref T At<T>(int i0, int i1, int i2) where T : unmanaged
    {
        var p = Ptr(i0, i1, i2);
        return ref Unsafe.AsRef<T>(p);
    }

    /// <summary>
    /// Returns a value to the specified array element.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="idx">Array of Mat::dims indices.</param>
    /// <returns>A value to the specified array element.</returns>
    public unsafe ref T At<T>(params int[] idx) where T : unmanaged
    {
        var p = Ptr(idx);
        return ref Unsafe.AsRef<T>(p);
    }
    
    /// <summary>
    /// Set a value to the specified array element.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="i0">Index along the dimension 0</param>
    /// <param name="value"></param>
    public unsafe void Set<T>(int i0, T value) where T : struct
    {
        var p = Ptr(i0);
        Unsafe.Write(p, value);
    }

    /// <summary>
    /// Set a value to the specified array element.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="i0">Index along the dimension 0</param>
    /// <param name="i1">Index along the dimension 1</param>
    /// <param name="value"></param>
    public unsafe void Set<T>(int i0, int i1, T value) where T : struct
    {
        var p = Ptr(i0, i1);
        Unsafe.Write(p, value);
    }

    /// <summary>
    /// Set a value to the specified array element.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="i0">Index along the dimension 0</param>
    /// <param name="i1">Index along the dimension 1</param>
    /// <param name="i2">Index along the dimension 2</param>
    /// <param name="value"></param>
    public unsafe void Set<T>(int i0, int i1, int i2, T value) where T : struct
    {
        var p = Ptr(i0, i1, i2);
        Unsafe.Write(p, value);
    }

    /// <summary>
    /// Set a value to the specified array element.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="idx">Array of Mat::dims indices.</param>
    /// <param name="value"></param>
    public unsafe void Set<T>(int[] idx, T value) where T : struct
    {
        var p = Ptr(idx);
        Unsafe.Write(p, value);
    }

    /// <summary>
    /// </summary>
    /// <param name="format"></param>
    /// <returns></returns>
    public string Dump(FormatType format = FormatType.Default) => Cv2.Format(this, format);

    #endregion

    #region InputOutputArray

    /// <inheritdoc />
    [Pure]
    public InputArrayHandle ToInputArrayHandle()
    {
        NativeMethods.HandleException(
            NativeMethods.core_InputArray_new_byMat(handle, out var resultHandle));
        GC.KeepAlive(this);
        return resultHandle;
    }

    /// <inheritdoc />
    [Pure]
    public OutputArrayHandle ToOutputArrayHandle()
    {
        NativeMethods.HandleException(
            NativeMethods.core_OutputArray_new_byMat(handle, out var resultHandle));
        GC.KeepAlive(this);
        return resultHandle;
    }

    /// <inheritdoc />
    [Pure]
    public InputOutputArrayHandle ToInputOutputArrayHandle()
    {
        NativeMethods.HandleException(
            NativeMethods.core_InputOutputArray_new_byMat(handle, out var resultHandle));
        GC.KeepAlive(this);
        return resultHandle;
    }

    #endregion
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
