using System.Runtime.InteropServices;

namespace OpenCvSharp5;

/// <summary>
/// This is the proxy interface for passing read-only input arrays into OpenCV functions.
/// </summary>
public interface IInputArray
{
    /// <summary>
    /// Creates a handle of cv::_InputArray*
    /// </summary>
    /// <returns></returns>
    InputArrayHandle ToInputArrayHandle();
}

/// <summary>
/// Represents cv::_InputArray*
/// </summary>
public class InputArrayHandle : SafeHandle
{
    /// <summary>
    /// Constructor
    /// </summary>
    public InputArrayHandle()
        : base(invalidHandleValue: IntPtr.Zero, ownsHandle: true)
    {
    }

    /// <inheritdoc />
    protected override bool ReleaseHandle() => throw new NotImplementedException();

    /// <inheritdoc />
    public override bool IsInvalid => handle == IntPtr.Zero;
}
