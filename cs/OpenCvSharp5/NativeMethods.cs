using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace OpenCvSharp5
{
    public static partial class NativeMethods
    {
        private const string LibraryName = "OpenCvSharpExtern";

        [LibraryImport(LibraryName)]
        [UnmanagedCallConv(CallConvs = new [] { typeof(CallConvCdecl) })]
        public static partial long core_getTickCount();
    }
}