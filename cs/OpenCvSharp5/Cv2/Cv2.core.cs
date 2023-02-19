using OpenCvSharp5.Internal;

namespace OpenCvSharp5;

public static class Cv2
{
    /// <summary>
    /// Returns the number of ticks.
    /// The function returns the number of ticks after the certain event (for example, when the machine was
    /// turned on). It can be used to initialize RNG or to measure a function execution time by reading the
    /// tick count before and after the function call.
    /// </summary>
    /// <returns></returns>
    public static long GetTickCount()
    {
        return NativeMethods.core_getTickCount();
    }

    /// <summary>
    /// Returns full configuration time cmake output.
    ///
    /// Returned value is raw cmake output including version control system revision, compiler version,
    /// compiler flags, enabled modules and third party libraries, etc.Output format depends on target architecture.
    /// </summary>
    /// <returns></returns>
    public static string GetBuildInformation()
    {
        using var stdString = StdString.Create();
        NativeMethods.core_getBuildInformation(stdString);
        return stdString.ToString();
    }
}
