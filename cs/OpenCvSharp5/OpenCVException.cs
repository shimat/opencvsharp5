using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace OpenCvSharp5;

/// <summary>
/// The default exception to be thrown by OpenCV 
/// </summary>
[Serializable]
// ReSharper disable once InconsistentNaming
public class OpenCVException : Exception
{
    /// <summary>
    /// The numeric code for error status
    /// </summary>
    public ErrorCode Status { get; }

    /// <summary>
    /// The source file name where error is encountered
    /// </summary>
    public string FuncName { get; }

    /// <summary>
    /// A description of the error
    /// </summary>
    public string ErrMsg { get; }

    /// <summary>
    /// The source file name where error is encountered
    /// </summary>
    public string FileName { get; }

    /// <summary>
    /// The line number in the source where error is encountered
    /// </summary>
    public int Line { get; }

    /// <summary>
    /// The user data
    /// </summary>
    public IntPtr UserData { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="status">The numeric code for error status</param>
    /// <param name="funcName">The source file name where error is encountered</param>
    /// <param name="errMsg">A description of the error</param>
    /// <param name="fileName">The source file name where error is encountered</param>
    /// <param name="line">The line number in the source where error is encountered</param>
    /// <param name="userData"></param>
    public OpenCVException(ErrorCode status, string funcName, string errMsg, string fileName, int line, IntPtr userData = default)
        : base(BuildMessage(status, funcName, errMsg, fileName, line))
    {
        Status = status;
        FuncName = funcName;
        ErrMsg = errMsg;
        FileName = fileName;
        Line = line;
        UserData = userData;
    }

    /// <inheritdoc />
    protected OpenCVException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Status = (ErrorCode) info.GetInt32(nameof(Status));
        FuncName = info.GetString(nameof(FuncName)) ?? "";
        FileName = info.GetString(nameof(FileName)) ?? "";
        ErrMsg = info.GetString(nameof(ErrMsg)) ?? "";
        Line = info.GetInt32(nameof(Line));
        UserData = new IntPtr(info.GetInt64(nameof(UserData)));
    }

    /// <inheritdoc />
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Status), Status);
        info.AddValue(nameof(FuncName), FuncName);
        info.AddValue(nameof(FileName), FileName);
        info.AddValue(nameof(ErrMsg), ErrMsg);
        info.AddValue(nameof(Line), Line);
        info.AddValue(nameof(UserData), UserData.ToInt64());
    }

    /// <inheritdoc />
    public OpenCVException()
    {
        Status = 0;
        FuncName = "";
        ErrMsg = "";
        FileName = "";
        Line = 0;
        UserData = default;
    }

    /// <inheritdoc />
    public OpenCVException(string message) : base(message)
    {
        Status = 0;
        FuncName = "";
        ErrMsg = "";
        FileName = "";
        Line = 0;
        UserData = default;
    }

    /// <inheritdoc />
    public OpenCVException(string message, Exception innerException) : base(message, innerException)
    {
        Status = 0;
        FuncName = "";
        ErrMsg = "";
        FileName = "";
        Line = 0;
        UserData = default;
    }

    private static string BuildMessage(ErrorCode status, string funcName, string errMsg, string fileName, int line)
    {
        fileName = Regex.Replace(fileName, @".*(?=opencv)", "");
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            fileName = fileName.Replace("\\", "/");
        return $"{errMsg} (FuncName={funcName}, FileName={fileName}, Line={line}, Status={status})";
    }
}
