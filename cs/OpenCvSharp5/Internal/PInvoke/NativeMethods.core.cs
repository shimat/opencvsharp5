using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenCvSharp5.Internal;

internal static partial class NativeMethods
{
    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus core_getTickCount(out long value);
    
    [DllImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static extern ExceptionStatus core_getBuildInformation(StdString obj);

    [DllImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static extern ExceptionStatus core_getVersionString(StdString obj);
}
