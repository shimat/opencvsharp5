using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using OpenCvSharp5.Internal.Vectors;

namespace OpenCvSharp5.Internal;

// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo

internal static partial class NativeMethods
{
    // imread

    [LibraryImport(LibraryName, 
        EntryPoint = "imgcodecs_imread",
        StringMarshallingCustomType = typeof(AnsiStringMarshaller))]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imread_Windows(
        string fileName, int flags, out MatHandle returnValue);

    [LibraryImport(LibraryName, 
        EntryPoint = "imgcodecs_imread",
        StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imread_NotWindows(
        string fileName, int flags, out MatHandle returnValue);
    
    [Pure]
    public static ExceptionStatus imgcodecs_imread(string fileName, int flags, out MatHandle returnValue) =>
        IsWindows() 
            ? imgcodecs_imread_Windows(fileName, flags, out returnValue) 
            : imgcodecs_imread_NotWindows(fileName, flags, out returnValue);


    // imreadmulti

    [LibraryImport(LibraryName,
        EntryPoint = "imgcodecs_imreadmulti1",
        StringMarshallingCustomType = typeof(AnsiStringMarshaller))]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imreadmulti1_Windows(
        string fileName, 
        VectorOfMatHandle mats, 
        int flags, 
        out int returnValue);
    
    [LibraryImport(LibraryName,
        EntryPoint = "imgcodecs_imreadmulti1",
        StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imreadmulti1_NotWindows(
        string fileName, 
        VectorOfMatHandle mats, 
        int flags, 
        out int returnValue);

    [Pure]
    public static ExceptionStatus imgcodecs_imreadmulti1(
        string fileName, VectorOfMatHandle mats, int flags, out int returnValue)
    {
        if (IsWindows())
            return imgcodecs_imreadmulti1_Windows(fileName, mats, flags, out returnValue);
        return imgcodecs_imreadmulti1_NotWindows(fileName, mats, flags, out returnValue);
    }

    [LibraryImport(LibraryName, 
        EntryPoint = "imgcodecs_imreadmulti2",
        StringMarshallingCustomType = typeof(AnsiStringMarshaller))]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imreadmulti2_Windows(
        string fileName, 
        VectorOfMatHandle mats, 
        int start,
        int count,
        int flags,
        out int returnValue);
    
    [LibraryImport(LibraryName, 
        EntryPoint = "imgcodecs_imreadmulti2",
        StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imreadmulti2_NotWindows(
        string fileName, 
        VectorOfMatHandle mats, 
        int start,
        int count,
        int flags,
        out int returnValue);
    
    [Pure]
    public static ExceptionStatus imgcodecs_imreadmulti2(
        string fileName, VectorOfMatHandle mats, int start, int count,int flags, out int returnValue) =>
        IsWindows() 
            ? imgcodecs_imreadmulti2_Windows(fileName, mats, start, count, flags, out returnValue) 
            : imgcodecs_imreadmulti2_NotWindows(fileName, mats, start, count, flags, out returnValue);
    

    // imcount

    [LibraryImport(LibraryName,
        EntryPoint = "imgcodecs_imcount",
        StringMarshallingCustomType = typeof(AnsiStringMarshaller))]
    public static partial ExceptionStatus imgcodecs_imcount_Windows(
        string fileName,
        int flags,
        out nint returnValue);
    
    [LibraryImport(LibraryName,
        EntryPoint = "imgcodecs_imcount",
        StringMarshalling = StringMarshalling.Utf8)]
    public static partial ExceptionStatus imgcodecs_imcount_NotWindows(
        string fileName,
        int flags,
        out nint returnValue);

    [Pure]
    public static ExceptionStatus imgcodecs_imcount(
        string fileName, int flags, out nint returnValue) =>
        IsWindows() 
            ? imgcodecs_imcount_Windows(fileName, flags, out returnValue) 
            : imgcodecs_imcount_NotWindows(fileName, flags, out returnValue);


    // imwrite

    [LibraryImport(LibraryName, 
        EntryPoint = "imgcodecs_imwrite",
        StringMarshallingCustomType = typeof(AnsiStringMarshaller))]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imwrite_Windows(
        string fileName, MatHandle img, int[] @params, int paramsLength, out int returnValue);

    [LibraryImport(LibraryName, 
        EntryPoint = "imgcodecs_imwrite",
        StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imwrite_NotWindows(
        string fileName, MatHandle img, int[] @params, int paramsLength, out int returnValue);
    
    [Pure]
    public static ExceptionStatus imgcodecs_imwrite(
        string fileName, MatHandle img, int[] @params, int paramsLength, out int returnValue)
    {
        if (IsWasm()) {
            returnValue = 0;
            return ExceptionStatus.Occurred;
        }

        if (IsWindows())
            return imgcodecs_imwrite_Windows(fileName, img, @params, paramsLength, out returnValue);
        return imgcodecs_imwrite_NotWindows(fileName, img, @params, paramsLength, out returnValue);
    }


    // imwritemulti

    [LibraryImport(LibraryName, 
        EntryPoint = "imgcodecs_imwritemulti",
        StringMarshallingCustomType = typeof(AnsiStringMarshaller))]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imwritemulti_Windows(
        string fileName, VectorOfMatHandle img, int[] @params, int paramsLength, out int returnValue);

    [LibraryImport(LibraryName, 
        EntryPoint = "imgcodecs_imwritemulti",
        StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imwritemulti_NotWindows(
        string fileName, VectorOfMatHandle img, int[] @params, int paramsLength, out int returnValue);
    
    [Pure]
    public static ExceptionStatus imgcodecs_imwritemulti(
        string fileName, VectorOfMatHandle img, int[] @params, int paramsLength, out int returnValue) =>
        IsWindows() 
            ? imgcodecs_imwritemulti_Windows(fileName, img, @params, paramsLength, out returnValue) 
            : imgcodecs_imwritemulti_NotWindows(fileName, img, @params, paramsLength, out returnValue);


    // 

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imdecode_Mat(
        MatHandle buf, int flags, out MatHandle returnValue);
    
    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imdecode_InputArray(
        InputArrayHandle buf, int flags, out MatHandle returnValue);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static unsafe partial ExceptionStatus imgcodecs_imdecode_bytes(
        byte* buf, int bufLength, int flags, out MatHandle returnValue);
    
    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imdecodemulti_InputArray(
        InputArrayHandle buf, int flags, VectorOfMatHandle mats, out int returnValue);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static unsafe partial ExceptionStatus imgcodecs_imdecodemulti_bytes(
        byte* buf, int bufLength, int flags, VectorOfMatHandle mats, out int returnValue);

    // Do not consider that "ext" may not be ASCII characters
    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imencode_vector(
        [MarshalAs(UnmanagedType.LPStr)] string ext, InputArrayHandle img, VectorOfByteHandle buf, int[] @params, int paramsLength, out int returnValue);


    // haveImageReader

    [LibraryImport(LibraryName, 
        EntryPoint = "imgcodecs_haveImageReader",
        StringMarshallingCustomType = typeof(AnsiStringMarshaller))]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_haveImageReader_Windows(
        string fileName, out int returnValue);
    
    [LibraryImport(LibraryName, 
        EntryPoint = "imgcodecs_haveImageReader",
        StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_haveImageReader_NotWindows(
        string fileName, out int returnValue);
    
    [Pure]
    public static ExceptionStatus imgcodecs_haveImageReader(string fileName, out int returnValue)
    {
        if (IsWasm()) {
            returnValue = default(int);
            return ExceptionStatus.Occurred;
        }
            
        return IsWindows() 
            ? imgcodecs_haveImageReader_Windows(fileName, out returnValue) 
            : imgcodecs_haveImageReader_NotWindows(fileName, out returnValue);
    }

    // haveImageWriter

    [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_haveImageWriter(
        string fileName, out int returnValue);
}
