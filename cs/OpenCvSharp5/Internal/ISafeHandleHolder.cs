using System.Runtime.InteropServices;

namespace OpenCvSharp5.Internal;

/// <summary>
/// Indicates that SafeHandle is being held
/// </summary>
public interface ISafeHandleHolder
{
    /// <summary>
    /// SafeHandle field
    /// </summary>
    SafeHandle Handle { get; }

    /// <summary>
    /// Indicates whether the holding SafeHandle has been released.
    /// </summary>
    public bool IsDisposed { get; }
}

/// <summary> 
/// </summary>
public static class SafeHandleHolderExtensions
{
    /// <summary>
    /// If SafeHandle has already been released, this method throws ObjectDisposedException.
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="ObjectDisposedException"></exception>
    public static void ThrowIfDisposed(this ISafeHandleHolder obj)
    {
        if (obj.IsDisposed)
            throw new ObjectDisposedException(obj.GetType().Name);
    }
}
