﻿using System.Data.SqlTypes;
using System.Runtime.InteropServices;
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

    /* @brief 

The function cv::split splits a multi-channel array into separate single-channel arrays:
\f[\texttt{mv} [c](I) =  \texttt{src} (I)_c\f]
If you need to extract a single channel or do some other sophisticated channel permutation, use
mixChannels .

The following example demonstrates how to split a 3-channel matrix into 3 single channel matrices.
@snippet snippets/core_split.cpp example

@param src input multi-channel array.
@param mvbegin output array; the number of arrays must match src.channels(); the arrays themselves are
reallocated, if needed.
@sa merge, mixChannels, cvtColor
*/
    /// <summary>
    /// Divides a multi-channel array into several single-channel arrays.
    /// </summary>
    /// <param name="src">The source multi-channel array</param>
    /// <param name="mv">output array; the number of arrays must match src.channels();
    /// the arrays themselves are reallocated, if needed.</param>
    public static void Split(Mat src, out Mat[] mv)
    {
        ThrowIfNull(src);
        src.ThrowIfDisposed();
        
        using var vec = new VectorOfMat();

        NativeMethods.HandleException(
            NativeMethods.core_split(src.Handle, vec.Handle));
        mv = vec.ToArray();

        GC.KeepAlive(src);
    }
}
