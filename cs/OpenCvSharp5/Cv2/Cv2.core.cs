using OpenCvSharp5.Internal;
using OpenCvSharp5.Internal.Vectors;

namespace OpenCvSharp5;

/// <summary>
/// OpenCV functions (cv::fooBar in C++ / cv2.fooBar in Python)
/// </summary>
public static partial class Cv2
{
    /// <summary>
    /// Returns the number of ticks.
    /// The function returns the number of ticks after the certain event (for example, when the machine was
    /// turned on). It can be used to initialize RNG or to measure a function execution time by reading the
    /// tick count before and after the function call.
    /// </summary>
    /// <returns></returns>
    public static long GetTickCount()
    {
        NativeMethods.HandleException(
            NativeMethods.core_getTickCount(out var result));
        return result;
    }

    /// <summary>
    /// Returns full configuration time cmake output.
    ///
    /// Returned value is raw cmake output including version control system revision, compiler version,
    /// compiler flags, enabled modules and third party libraries, etc.Output format depends on target architecture.
    /// </summary>
    /// <returns></returns>
    public static string GetBuildInformation()
    {
        using var stdString = StdString.Create();
        NativeMethods.HandleException(
            NativeMethods.core_getBuildInformation(stdString));
        return stdString.ToString();
    }

    /// <summary>
    /// Returns library version string.
    /// For example "3.4.1-dev".
    /// </summary>
    /// <returns></returns>
    public static string GetVersionString()
    {
        using var stdString = StdString.Create();
        NativeMethods.HandleException(
            NativeMethods.core_getVersionString(stdString));
        return stdString.ToString();
    }

    /// <summary>
    /// </summary>
    /// <param name="mtx"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    public static string Format(IInputArray mtx, FormatType format = FormatType.Default)
    {
        ThrowIfNull(mtx);

        using var buf = StdString.Create();
        using var mtxHandle = mtx.ToInputArrayHandle();
        NativeMethods.HandleException(
            NativeMethods.core_format(mtxHandle, (int)format, buf));

        GC.KeepAlive(mtx);
        return buf.ToString();
    }

    /// <summary>
    /// Performs the per-element comparison of two arrays or an array and scalar value.
    /// </summary>
    /// <param name="src1">first input array or a scalar; when it is an array, it must have a single channel.</param>
    /// <param name="src2">second input array or a scalar; when it is an array, it must have a single channel.</param>
    /// <param name="dst">output array of type ref CV_8U that has the same size and the same number of channels as the input arrays.</param>
    /// <param name="cmpop">a flag, that specifies correspondence between the arrays (cv::CmpTypes)</param>
    public static void Compare(IInputArray src1, IInputArray src2, IOutputArray dst, CmpTypes cmpop)
    {
        ThrowIfNull(src1);
        ThrowIfNull(src2);
        ThrowIfNull(dst);

        using var src1Handle = src1.ToInputArrayHandle();
        using var src2Handle = src2.ToInputArrayHandle();
        using var dstHandle = dst.ToOutputArrayHandle();

        NativeMethods.HandleException(
            NativeMethods.core_compare(src1Handle, src2Handle, dstHandle, (int)cmpop));
    }

    /// <summary>
    /// Counts non-zero array elements.
    /// </summary>
    /// <param name="src">single-channel array.</param>
    /// <returns></returns>
    public static int CountNonZero(IInputArray src)
    {
        ThrowIfNull(src);

        using var srcHandle = src.ToInputArrayHandle();

        NativeMethods.HandleException(
            NativeMethods.core_countNonZero(srcHandle, out var result));
        return result;
    }
    
    /// <summary>
    /// Divides a multi-channel array into several single-channel arrays.
    /// </summary>
    /// <param name="src">The source multi-channel array</param>
    /// <returns></returns>
    public static DisposableArray<Mat> Split(Mat src)
    {
        ThrowIfNull(src);
        src.ThrowIfDisposed();
        
        using var vec = new VectorOfMat();

        NativeMethods.HandleException(
            NativeMethods.core_split(src.Handle, vec.Handle));

        GC.KeepAlive(src);
        return new DisposableArray<Mat>(vec.ToArray());
    }

    /// <summary>
    /// Divides a multi-channel array into several single-channel arrays.
    /// </summary>
    /// <param name="src">The source multi-channel array</param>
    /// <param name="dst">output array; the number of arrays must match src.channels();
    /// the arrays themselves are reallocated, if needed.</param>
    public static void Split(Mat src, IReadOnlyList<Mat> dst)
    {
        ThrowIfNull(src);
        src.ThrowIfDisposed();
        
        using var dstVec = new VectorOfMat(dst);

        NativeMethods.HandleException(
            NativeMethods.core_split(src.Handle, dstVec.Handle));

        dstVec.CopyToArray(dst);

        GC.KeepAlive(src);
        GC.KeepAlive(dst);
    }
}
