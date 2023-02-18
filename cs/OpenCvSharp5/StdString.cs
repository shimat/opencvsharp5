using System.Runtime.InteropServices;

namespace OpenCvSharp5;

internal class StdString : SafeHandle
{
    internal StdString()
        : base(invalidHandleValue: IntPtr.Zero, ownsHandle: true)
    {
    }
    
    public static StdString Create()
    {
        return NativeMethods.std_string_new1();
    }
    
    public static StdString Create(string s)
    {
        return NativeMethods.std_string_new2(s);
    }

    protected override bool ReleaseHandle()
    {
        NativeMethods.std_string_delete(handle);
        return true;
    }

    public override bool IsInvalid => handle == IntPtr.Zero;

    public override string? ToString()
    {
        if (IsInvalid)
            throw new InvalidOperationException("Invalid handle");

        var p = NativeMethods.std_string_c_str(this);
        return Marshal.PtrToStringUTF8(p);
    }
}

