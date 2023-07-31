using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenCvSharp5.Internal;

internal static partial class NativeMethods
{
    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgproc_rectangle1(
        InputOutputArrayHandle img,
        Point pt1,
        Point pt2,
        in Scalar color,
        int thickness,
        LineTypes lineType,
        int shift);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgproc_rectangle2(
        InputOutputArrayHandle img, 
        Rect rec, 
        in Scalar color, 
        int thickness,
        LineTypes lineType,
        int shift);
}
