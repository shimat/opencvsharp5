using System.Runtime.InteropServices;

namespace OpenCvSharp5.Internal;

internal static class SafeHandleExtensions
{
    public static TResult GetHandleInScope<TResult>(this SafeHandle safeHandle, Func<IntPtr, TResult> func)
    {
        if (safeHandle.IsInvalid)
            throw new ObjectDisposedException($"{safeHandle.GetType().Name} is disposed.");

        var addRef = false;
        try
        {
            safeHandle.DangerousAddRef(ref addRef);
            var ptr = safeHandle.DangerousGetHandle();
            return func(ptr);
        }
        finally
        {
            if (addRef)
                safeHandle.DangerousRelease();
        }
    }

    public static void GetHandleInScope(this SafeHandle safeHandle, Action<IntPtr> func)
    {
        if (safeHandle.IsInvalid)
            throw new ObjectDisposedException($"{safeHandle.GetType().Name} is disposed.");

        var addRef = false;
        try
        {
            safeHandle.DangerousAddRef(ref addRef);
            var ptr = safeHandle.DangerousGetHandle();
            func(ptr);
        }
        finally
        {
            if (addRef)
                safeHandle.DangerousRelease();
        }
    }
}
