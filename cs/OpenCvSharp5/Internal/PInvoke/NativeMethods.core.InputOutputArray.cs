using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenCvSharp5.Internal;

internal static partial class NativeMethods
{
    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus core_InputArray_new_byMat(MatHandle matHandle, out InputArrayHandle resultHandle);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus core_InputArray_delete(IntPtr obj);
    

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus core_OutputArray_new_byMat(MatHandle matHandle, out OutputArrayHandle resultHandle);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus core_OutputArray_delete(IntPtr obj);


    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus core_InputOutputArray_new_byMat(MatHandle matHandle, out InputOutputArrayHandle resultHandle);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus core_InputOutputArray_delete(IntPtr obj);
}
