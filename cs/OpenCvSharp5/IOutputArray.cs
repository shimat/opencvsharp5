﻿using System.Runtime.InteropServices;
using OpenCvSharp5.Internal;

namespace OpenCvSharp5;

/// <summary>
/// This is the proxy interface for passing writable input arrays into OpenCV functions.
/// </summary>
public interface IOutputArray : IInputArray
{
    /// <summary>
    /// Creates a handle of cv::_OutputArray*
    /// </summary>
    /// <returns></returns>
    OutputArrayHandle ToOutputArrayHandle();
}

/// <summary>
/// Represents cv::_OutputArray*
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
            NativeMethods.core_OutputArray_delete(handle));
        return true;
    }

    /// <inheritdoc />
    public override bool IsInvalid => handle == IntPtr.Zero;
}
