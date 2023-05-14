using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenCvSharp5.Internal;

internal static partial class NativeMethods
{
    // imread

    [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imread(
        string fileName, int flags, out MatHandle returnValue);

    // imreadmulti

    [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imreadmulti(
        string fileName, IntPtr mats, int flags, out int returnValue);

    // imwrite

    [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imwrite(
        string fileName, IntPtr img, int[] @params, int paramsLength, out int returnValue);

    // imwrite_multi

    [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imwrite_multi(
        string fileName, IntPtr img, int[] @params, int paramsLength, out int returnValue);

    // 

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imdecode_Mat(
        IntPtr buf, int flags, out IntPtr returnValue);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static unsafe partial ExceptionStatus imgcodecs_imdecode_vector(
        byte* buf, int bufLength, int flags, out IntPtr returnValue);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imdecode_InputArray(
        IntPtr buf, int flags, out IntPtr returnValue);

    // Do not consider that "ext" may not be ASCII characters
    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imencode_vector(
        [MarshalAs(UnmanagedType.LPStr)] string ext, IntPtr img, IntPtr buf, int[] @params, int paramsLength, out int returnValue);

    // haveImageReader

    [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_haveImageReader(
        string fileName, out int returnValue);

    // haveImageWriter

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_haveImageWriter(
        [MarshalAs(UnmanagedType.LPStr)] string fileName, out int returnValue);
}
