using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

#pragma warning disable CA1707

namespace OpenCvSharp5;

internal static partial class NativeMethods
{
    private const string LibraryName = "OpenCvSharpExtern";

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial long core_getTickCount();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr core_Mat_new1(int row, int col, int type);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void core_Mat_delete(IntPtr obj);
}

[StructLayout(LayoutKind.Sequential)]
internal class Mat : IDisposable
{
    public void Dispose()
    {
    }
}

[CustomMarshaller(typeof(Mat), MarshalMode.ManagedToUnmanagedIn, typeof(MatMarshaller))]
internal static unsafe class MatMarshaller
{
    // Unmanaged representation of Mat
    internal struct MatUnmanaged
    {
    }

    public static nint ConvertToUnmanaged(Mat managed)
    {
        throw new NotImplementedException();
    }

    public static Mat ConvertToManaged(MatUnmanaged* unmanaged)
    {
        throw new NotImplementedException();
    }

    public static void Free(MatUnmanaged *unmanaged)
        => throw new NotImplementedException();
}