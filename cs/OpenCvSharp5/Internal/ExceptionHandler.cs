using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace OpenCvSharp5.Internal;

/// <summary>
/// This static class defines one instance which than can be used by multiple threads to gather exception information from OpenCV
/// Implemented as a singleton
/// </summary>
public static class ExceptionHandler
{
    // ThreadLocal variables to save the exception for the current thread
    private static readonly ThreadLocal<bool> ExceptionHappened = new(false);
    private static readonly ThreadLocal<ErrorCode> LocalStatus = new();
    private static readonly ThreadLocal<string?> LocalFuncName = new();
    private static readonly ThreadLocal<string?> LocalErrMsg = new();
    private static readonly ThreadLocal<string?> LocalFileName = new();
    private static readonly ThreadLocal<int> LocalLine = new();
    private static readonly ThreadLocal<IntPtr> LocalUserData = new();

    /// <summary>
    /// Callback function invoked by OpenCV when exception occurs 
    /// Stores the information locally for every thread
    /// </summary>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static unsafe int ErrorHandlerCallback(
        ErrorCode status, 
        byte *funcName, 
        byte *errMsg, 
        byte *fileName, 
        int line,
        void *userData)
    {
        try
        {
            return 0;
        }
        finally
        {
            ExceptionHappened.Value = true;
            LocalStatus.Value = status;
            LocalFuncName.Value = Utf8StringMarshaller.ConvertToManaged(funcName);
            LocalErrMsg.Value = Utf8StringMarshaller.ConvertToManaged(errMsg);
            LocalFileName.Value = Utf8StringMarshaller.ConvertToManaged(fileName);
            LocalLine.Value = line;
            LocalUserData.Value = new IntPtr(userData);
        }
    }

    /// <summary>
    /// Custom error handler to be thrown by OpenCV
    /// </summary>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static unsafe int ErrorHandlerThrowException(
        ErrorCode status, 
        byte *funcName, 
        byte *errMsg, 
        byte *fileName, 
        int line, 
        void *userData) =>
        throw new OpenCVException(
            status,
            Utf8StringMarshaller.ConvertToManaged(funcName) ?? "",
            Utf8StringMarshaller.ConvertToManaged(errMsg) ?? "",
            Utf8StringMarshaller.ConvertToManaged(fileName) ?? "", 
            line,
            new IntPtr(userData));

    /// <summary>
    /// Throws appropriate exception if one happened
    /// </summary>
    public static void ThrowPossibleException()
    {
        if (CheckForException())
        {
            throw new OpenCVException(
                LocalStatus.Value,
                LocalFuncName.Value ?? "",
                LocalErrMsg.Value ?? "",
                LocalFileName.Value ?? "",
                LocalLine.Value);
        }
    }

    /// <summary>
    /// Returns a boolean which indicates if an exception occured for the current thread
    /// Reading this value changes its state, so an exception is handled only once
    /// </summary>
    private static bool CheckForException()
    {
        var value = ExceptionHappened.Value;
        // reset exception value
        ExceptionHappened.Value = false;
        return value;
    }
}
