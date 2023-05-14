﻿using System.Runtime.InteropServices;
using OpenCvSharp5.Internal;

namespace OpenCvSharp5;

/// <summary>
/// This is the proxy interface for passing readable and writable input arrays into OpenCV functions.
/// </summary>
public interface IInputOutputArray : IOutputArray
{
    /// <summary>
    /// Creates a handle of cv::_InputOutputArray*
    /// </summary>
    /// <returns></returns>
    InputOutputArrayHandle ToInputOutputArrayHandle();
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
