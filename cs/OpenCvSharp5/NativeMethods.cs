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
    public static partial void core_getBuildInformation_(StdString obj);

    [DllImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static extern void core_getBuildInformation(IntPtr obj);

    [DllImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static extern nuint core_getBuildInformation_pure();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial IntPtr core_Mat_new1(int row, int col, int type);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void core_Mat_delete(IntPtr obj);
    
    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    internal static partial StdString std_string_new1();

    [DllImport(LibraryName, EntryPoint = "std_string_new1")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    internal static extern IntPtr std_string_new1_();

    [LibraryImport(LibraryName, StringMarshallingCustomType = typeof(Utf8StringMarshaller))]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    internal static partial StdString std_string_new2(string str);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    internal static partial void std_string_delete(IntPtr obj);

    // The following code fails because
    // System.Runtime.InteropServices.Marshalling.Utf8StringMarshaller.Free(__retVal_native);
    // is called at the end in the code output by CodeGenerator (the return value of std::string.c_str must not be freed) 
    /*
    [LibraryImport(LibraryName, StringMarshallingCustomType = typeof(Utf8StringMarshaller))]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    internal static partial string std_string_c_str(StdString obj);
    //*/
    ///*
    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    internal static partial IntPtr std_string_c_str(StdString obj);
    //*/
}
