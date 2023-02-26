using System.Runtime.InteropServices;

namespace OpenCvSharp5.Internal;

/// <summary>
/// This is the proxy interface for passing read-only input arrays into OpenCV functions.
/// </summary>
public interface IInputArray
{
}

/// <summary>
/// This is the proxy interface for passing writable input arrays into OpenCV functions.
/// </summary>
public interface IOutputArray : IInputArray
{
    /// <summary>
    /// Creates a handle of cv::_OutputArray*
    /// </summary>
    /// <returns></returns>
    OutputArrayHandle ToHandle();
}

/// <summary>
/// This is the proxy interface for passing readable and writable input arrays into OpenCV functions.
/// </summary>
public interface IInputOutputArray : IOutputArray
{
    /// <summary>
    /// Creates a handle of cv::_InputOutputArray*
    /// </summary>
    /// <returns></returns>
    InputOutputArrayHandle ToHandle();
}

/// <summary>
/// Represents cv::_InputOutputArray*
/// </summary>
public class OutputArrayHandle : SafeHandle
{
    /// <summary>
    /// Constructor
    /// </summary>
    public OutputArrayHandle()
        : base(invalidHandleValue: IntPtr.Zero, ownsHandle: true)
    {
    }

    /// <inheritdoc />
    protected override bool ReleaseHandle()
    {
        NativeMethods.HandleException(
            NativeMethods.core_InputOutputArray_delete(handle));
        return true;
    }

    /// <inheritdoc />
    public override bool IsInvalid => handle == IntPtr.Zero;
}


/// <summary>
/// Represents cv::_InputOutputArray*
/// </summary>
public class InputOutputArrayHandle : SafeHandle
{
    /// <summary>
    /// Constructor
    /// </summary>
    public InputOutputArrayHandle()
        : base(invalidHandleValue: IntPtr.Zero, ownsHandle: true)
    {
    }

    /// <inheritdoc />
    protected override bool ReleaseHandle()
    {
        NativeMethods.HandleException(
            NativeMethods.core_InputOutputArray_delete(handle));
        return true;
    }

    /// <inheritdoc />
    public override bool IsInvalid => handle == IntPtr.Zero;
}
