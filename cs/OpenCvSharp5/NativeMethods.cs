using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace OpenCvSharp5
{
    public static partial class NativeMethods
    {
        private const string LibraryName = "OpenCvSharpExtern";

        [LibraryImport(LibraryName)]
        public static partial long core_getTickCount();
    }
}