using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenCvSharp5.Internal;

internal static partial class NativeMethods
{

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus core_MatExpr_new1(out MatExprHandle result);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus core_MatExpr_new2(MatHandle m, out MatExprHandle result);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus core_MatExpr_delete(IntPtr obj);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus core_MatExpr_toMat(MatExprHandle obj, out MatHandle result);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus core_MatExpr_size(MatExprHandle obj, out Size result);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus core_MatExpr_type(MatExprHandle obj, out int result);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus core_MatExpr_row(MatExprHandle obj, int y, out MatExprHandle returnValue);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus core_MatExpr_col(MatExprHandle obj, int x, out MatExprHandle returnValue);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus core_MatExpr_diag(MatExprHandle obj, int d, out MatExprHandle returnValue);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus core_MatExpr_cropByRange(
        MatExprHandle obj, Range rowRange, Range colRange, out MatExprHandle returnValue);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus core_MatExpr_cropByRect(
        MatExprHandle obj, Rect roi, out MatExprHandle returnValue);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus core_MatExpr_t(MatExprHandle obj, out MatExprHandle returnValue);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus core_MatExpr_inv(MatExprHandle obj, int method, out MatExprHandle returnValue);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus core_MatExpr_mul1(
        MatExprHandle obj, MatExprHandle e, double scale, out MatExprHandle returnValue);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus core_MatExpr_mul2(
        MatExprHandle obj, MatHandle m, double scale, out MatExprHandle returnValue);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus core_MatExpr_cross(
        MatExprHandle obj, MatHandle m, out MatExprHandle returnValue);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus core_MatExpr_dot(MatExprHandle obj, MatHandle m, out double returnValue);
}
