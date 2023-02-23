using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenCvSharp5.Internal;

internal static partial class NativeMethods
{
    private const string LibraryName = "OpenCvSharpExtern";

    static NativeMethods()
    {
        if (IsWasm())
        {
            return;
        }

        if (IsUnix())
        {
            unsafe
            {
                core_redirectError(&ExceptionHandler.ErrorHandlerCallback, null, null);
            }
        }

        else if (IsWindows())
        {
            unsafe
            {
                core_redirectError(&ExceptionHandler.ErrorHandlerThrowException, null, null);
            }
        }
        else
        {
            throw new OpenCvSharpException($"Unknown OS: {RuntimeInformation.OSDescription}");
        }
    }

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static unsafe partial nint core_redirectError(
        delegate* unmanaged[Cdecl]<ErrorCode, byte*, byte*, byte*, int, void*, int> errorHandler,
        void* userData, 
        void** prevUserData);

    internal static void HandleException(ExceptionStatus status)
    {
        // Check if there has been an exception
        if (status == ExceptionStatus.Occurred /*&& IsUnix()*/) // thrown can be 1 when unix 
        {
            ExceptionHandler.ThrowPossibleException();
        }
    }

    /// <summary>
    /// Returns whether the OS is *nix or not
    /// </summary>
    /// <returns></returns>
    private static bool IsUnix() =>
        RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
        RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
        RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD);

    /// <summary>
    /// Returns whether the OS is *nix or not
    /// </summary>
    /// <returns></returns>
    private static bool IsWindows() =>
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    /// <summary>
    /// Returns whether the architecture is Wasm or not
    /// </summary>
    /// <returns></returns>
    private static bool IsWasm() =>
        RuntimeInformation.OSArchitecture == Architecture.Wasm;
}

#pragma warning disable 1591
