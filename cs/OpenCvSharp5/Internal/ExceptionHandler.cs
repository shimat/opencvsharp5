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
    private static readonly ThreadLocal<bool> exceptionHappened = new(false);
    private static readonly ThreadLocal<ErrorCode> localStatus = new();
    private static readonly ThreadLocal<string?> localFuncName = new();
    private static readonly ThreadLocal<string?> localErrMsg = new();
    private static readonly ThreadLocal<string?> localFileName = new();
    private static readonly ThreadLocal<int> localLine = new();

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
            exceptionHappened.Value = true;
            localStatus.Value = status;
            localErrMsg.Value = Utf8StringMarshaller.ConvertToManaged(errMsg);
            localFileName.Value = Utf8StringMarshaller.ConvertToManaged(fileName);
            localLine.Value = line;
            localFuncName.Value = Utf8StringMarshaller.ConvertToManaged(funcName);
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
            line);

    /// <summary>
    /// Throws appropriate exception if one happened
    /// </summary>
    public static void ThrowPossibleException()
    {
        if (CheckForException())
        {
            throw new OpenCVException(
                localStatus.Value,
                localFuncName.Value ?? "",
                localErrMsg.Value ?? "",
                localFileName.Value ?? "",
                localLine.Value);
        }
    }

    /// <summary>
    /// Returns a boolean which indicates if an exception occured for the current thread
    /// Reading this value changes its state, so an exception is handled only once
    /// </summary>
    private static bool CheckForException()
    {
        var value = exceptionHappened.Value;
        // reset exception value
        exceptionHappened.Value = false;
        return value;
    }
}
