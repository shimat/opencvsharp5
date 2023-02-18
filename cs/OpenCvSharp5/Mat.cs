﻿using System;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace OpenCvSharp5;

public class Mat : IDisposable
{
    private readonly MatHandle handle;

    public Mat()
    {
        handle = NativeMethods.core_Mat_new1();
    }
    
    public Mat(int row, int col, int type)
    {
        handle = NativeMethods.core_Mat_new2(row, col, type);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            handle.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public int Rows =>
        handle.GetHandleInScope(ptr =>
        {
            unsafe
            {
                var s = (NativeMat*)ptr;
                return s->rows;
            }
        });

    public int Cols =>
        handle.GetHandleInScope(ptr =>
        {
            unsafe
            {
                var s = (NativeMat*)ptr;
                return s->cols;
            }
        });

    public unsafe IntPtr Data =>
        handle.GetHandleInScope(ptr =>
        {
            unsafe
            {
                var s = (NativeMat*)ptr;
                return (IntPtr)s->data;
            }
        });

    public int SafeRows => NativeMethods.core_Mat_rows(handle);
    public int SafeCols => NativeMethods.core_Mat_cols(handle);
    public IntPtr SafeData => NativeMethods.core_Mat_data(handle);
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct NativeMat
{
    public int flags;
    //! the matrix dimensionality, >= 2
    public int dims;
    //! the number of rows and columns or (-1, -1) when the matrix has more than 2 dimensions
    public int rows;
    public int cols;
    //! pointer to the data
    public byte* data;
}

internal class MatHandle : SafeHandle
{
    internal MatHandle()
        : base(invalidHandleValue: IntPtr.Zero, ownsHandle: true)
    {
    }
    
    protected override bool ReleaseHandle()
    {
        NativeMethods.core_Mat_delete(handle);
        return true;
    }

    public override bool IsInvalid => handle == IntPtr.Zero;
}


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
