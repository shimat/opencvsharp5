using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenCvSharp5.Internal;

internal static partial class NativeMethods
{
    [DllImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static extern ExceptionStatus imgproc_rectangle1(
        InputOutputArrayHandle img,
        Point pt1,
        Point pt2,
        in Scalar color,
        int thickness,
        LineTypes lineType,
        int shift);

    [DllImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static extern ExceptionStatus imgproc_rectangle2(
        InputOutputArrayHandle img, 
        Rect rec, 
        in Scalar color, 
        int thickness,
        LineTypes lineType,
        int shift);
}
