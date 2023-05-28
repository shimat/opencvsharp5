﻿using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OpenCvSharp5.Internal.Vectors;

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
        string fileName, VectorOfMatHandle mats, int flags, out int returnValue);

    // imwrite

    [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imwrite(
        string fileName, MatHandle img, int[] @params, int paramsLength, out int returnValue);

    // imwrite_multi

    [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imwrite_multi(
        string fileName, VectorOfMatHandle img, int[] @params, int paramsLength, out int returnValue);

    // 

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imdecode_Mat(
        MatHandle buf, int flags, out MatHandle returnValue);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static unsafe partial ExceptionStatus imgcodecs_imdecode_vector(
        byte* buf, int bufLength, int flags, out MatHandle returnValue);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imdecode_InputArray(
        InputArrayHandle buf, int flags, out MatHandle returnValue);

    // Do not consider that "ext" may not be ASCII characters
    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_imencode_vector(
        [MarshalAs(UnmanagedType.LPStr)] string ext, InputArrayHandle img, VectorOfByteHandle buf, int[] @params, int paramsLength, out int returnValue);

    // haveImageReader

    [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_haveImageReader(
        string fileName, out int returnValue);

    // haveImageWriter

    [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ExceptionStatus imgcodecs_haveImageWriter(
        string fileName, out int returnValue);
}
