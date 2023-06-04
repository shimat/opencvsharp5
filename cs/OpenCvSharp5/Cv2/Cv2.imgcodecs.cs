using OpenCvSharp5.Internal;
using OpenCvSharp5.Internal.Vectors;

namespace OpenCvSharp5;

// ReSharper disable CommentTypo

static partial class Cv2
{
    /// <summary>
    /// Loads an image from a file.
    /// </summary>
    /// <param name="fileName">Name of file to be loaded.</param>
    /// <param name="flags">Specifies color type of the loaded image</param>
    /// <returns></returns>
    public static Mat ImRead(string fileName, ImreadModes flags = ImreadModes.Color)
    {
        if (string.IsNullOrEmpty(fileName))
            throw new ArgumentNullException(nameof(fileName));

        NativeMethods.HandleException(
            NativeMethods.imgcodecs_imread(fileName, (int)flags, out var ret));

        return new Mat(ret);
    }

    /// <summary>
    /// Loads a multi-page image from a file.
    /// </summary>
    /// <param name="fileName">Name of file to be loaded.</param>
    /// <param name="mats">A vector of Mat objects holding each page, if more than one.</param>
    /// <param name="flags">Flag that can take values of @ref cv::ImreadModes, default with IMREAD_ANYCOLOR.</param>
    /// <returns></returns>
    public static bool ImReadMulti(
        string fileName, 
        out DisposableArray<Mat> mats, 
        ImreadModes flags = ImreadModes.AnyColor)
    {
        ThrowIfNullOrEmpty(fileName);

        using var matsVec = new VectorOfMat();
        NativeMethods.HandleException(
            NativeMethods.imgcodecs_imreadmulti1(
                fileName, matsVec.Handle, (int)flags, out var ret));
        mats = new DisposableArray<Mat>(matsVec.ToArray());
        return ret != 0;
    }

    /// <summary>
    /// Loads a multi-page image from a file.
    /// </summary>
    /// <param name="fileName">Name of file to be loaded.</param>
    /// <param name="mats">A vector of Mat objects holding each page, if more than one.</param>
    /// <param name="start">Start index of the image to load</param>
    /// <param name="count">Count number of images to load</param>
    /// <param name="flags">Flag that can take values of @ref cv::ImreadModes, default with IMREAD_ANYCOLOR.</param>
    /// <returns></returns>
    public static bool ImReadMulti(
        string fileName, 
        out DisposableArray<Mat> mats, 
        int start, 
        int count,
        ImreadModes flags = ImreadModes.AnyColor)
    {
        ThrowIfNullOrEmpty(fileName);

        using var matsVec = new VectorOfMat();
        NativeMethods.HandleException(
            NativeMethods.imgcodecs_imreadmulti2(
                fileName, matsVec.Handle, start, count, (int)flags, out var ret));
        mats = new DisposableArray<Mat>(matsVec.ToArray());
        return ret != 0;
    }
    
    /// <summary>
    /// Returns the number of images inside the give file
    /// </summary>
    /// <param name="fileName">Name of file to be loaded.</param>
    /// <param name="flags">Flag that can take values of cv::ImreadModes, default with cv::IMREAD_ANYCOLOR.</param>
    /// <returns></returns>
    public static int ImCount(string fileName, ImreadModes flags = ImreadModes.AnyColor)
    {
        ThrowIfNullOrEmpty(fileName);
        
        NativeMethods.HandleException(
            NativeMethods.imgcodecs_imcount(
                fileName, (int)flags, out var ret));
        return (int)ret;
    }

    /// <summary>
    /// Saves an image to a specified file.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <param name="img">Image to be saved.</param>
    /// <param name="parameters">Format-specific save parameters encoded as pairs</param>
    /// <returns></returns>
    public static bool ImWrite(string fileName, Mat img, int[]? parameters = null)
    {
        if (string.IsNullOrEmpty(fileName))
            throw new ArgumentNullException(nameof(fileName));
        ThrowIfNull(img);
        parameters ??= Array.Empty<int>();

        NativeMethods.HandleException(
            NativeMethods.imgcodecs_imwrite(
                fileName, img.Handle, parameters, parameters.Length, out var ret));
        GC.KeepAlive(img);
        return ret != 0;
    }

    /// <summary>
    /// Saves an image to a specified file.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <param name="img">Image to be saved.</param>
    /// <param name="parameters">Format-specific save parameters encoded as pairs</param>
    /// <returns></returns>
    public static bool ImWrite(string fileName, Mat img, params ImageEncodingParam[] parameters)
    {
        ThrowIfNull(parameters);
        if (parameters.Length <= 0)
            return ImWrite(fileName, img);

        var p = new List<int>();
        foreach (var item in parameters)
        {
            p.Add((int)item.EncodingId);
            p.Add(item.Value);
        }
        return ImWrite(fileName, img, p.ToArray());
    }

    /// <summary>
    /// Saves an image to a specified file.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <param name="img">Image to be saved.</param>
    /// <param name="parameters">Format-specific save parameters encoded as pairs</param>
    /// <returns></returns>
    public static bool ImWrite(string fileName, IEnumerable<Mat> img, int[]? parameters = null)
    {
        if (string.IsNullOrEmpty(fileName))
            throw new ArgumentNullException(nameof(fileName));
        ThrowIfNull(img);
        parameters ??= Array.Empty<int>();

        using var imgVec = new VectorOfMat(img);
        NativeMethods.HandleException(
            NativeMethods.imgcodecs_imwritemulti(
                fileName, imgVec.Handle, parameters, parameters.Length, out var ret));
        GC.KeepAlive(img);
        return ret != 0;
    }

    /// <summary>
    /// Saves an image to a specified file.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <param name="img">Image to be saved.</param>
    /// <param name="parameters">Format-specific save parameters encoded as pairs</param>
    /// <returns></returns>
    public static bool ImWrite(string fileName, IEnumerable<Mat> img, params ImageEncodingParam[] parameters)
    {
        ThrowIfNull(parameters);
        if (parameters.Length <= 0)
            return ImWrite(fileName, img);

        var p = new List<int>();
        foreach (var item in parameters)
        {
            p.Add((int)item.EncodingId);
            p.Add(item.Value);
        }
        return ImWrite(fileName, img, p.ToArray());
    }

    /// <summary>
    /// Reads image from the specified buffer in memory.
    /// </summary>
    /// <param name="buf">The input array of vector of bytes.</param>
    /// <param name="flags">The same flags as in imread</param>
    /// <returns></returns>
    public static Mat ImDecode(Mat buf, ImreadModes flags)
    {
        ThrowIfNull(buf);
        buf.ThrowIfDisposed();

        NativeMethods.HandleException(
            NativeMethods.imgcodecs_imdecode_Mat(buf.Handle, (int)flags, out var ret));
        
        GC.KeepAlive(buf);
        return new Mat(ret);
    }

    /// <summary>
    /// Reads image from the specified buffer in memory.
    /// </summary>
    /// <param name="buf">The input array of vector of bytes.</param>
    /// <param name="flags">The same flags as in imread</param>
    /// <returns></returns>
    public static Mat ImDecode(IInputArray buf, ImreadModes flags)
    {
        ThrowIfNull(buf);

        using var imgHandle = buf.ToInputArrayHandle();

        NativeMethods.HandleException(
            NativeMethods.imgcodecs_imdecode_InputArray(imgHandle, (int)flags, out var ret));
        
        GC.KeepAlive(buf);
        return new Mat(ret);
    }
    
    /// <summary>
    /// Reads image from the specified buffer in memory.
    /// </summary>
    /// <param name="span">The input slice of bytes.</param>
    /// <param name="flags">The same flags as in imread</param>
    /// <returns></returns>
    public static Mat ImDecode(ReadOnlySpan<byte> span, ImreadModes flags)
    {
        if (span.IsEmpty)
            throw new ArgumentException("Empty span", nameof(span));

        unsafe
        {
            fixed (byte* pBuf = span)
            {
                NativeMethods.HandleException(
                    NativeMethods.imgcodecs_imdecode_bytes(pBuf, span.Length, (int)flags, out var ret));
                return new Mat(ret);
            }
        }
    }

    /// <summary>
    /// Reads a multi-page image from a buffer in memory.
    /// </summary>
    /// <param name="buf">Input array or vector of bytes.</param>
    /// <param name="flags">The same flags as in cv::imread, see cv::ImreadModes.</param>
    /// <param name="mats">A vector of Mat objects holding each page, if more than one.</param>
    /// <returns></returns>
    public static bool ImDecodeMulti(IInputArray buf, ImreadModes flags, out DisposableArray<Mat> mats)
    {
        ThrowIfNull(buf);
        
        using var bufHandle = buf.ToInputArrayHandle();
        using var matsVec = new VectorOfMat();

        NativeMethods.HandleException(
            NativeMethods.imgcodecs_imdecodemulti_InputArray(
                bufHandle, (int)flags, matsVec.Handle, out var ret));
        mats = new DisposableArray<Mat>(matsVec.ToArray());

        return ret != 0;
    }
    
    /// <summary>
    /// Reads a multi-page image from a buffer in memory.
    /// </summary>
    /// <param name="span">Input array or vector of bytes.</param>
    /// <param name="flags">The same flags as in cv::imread, see cv::ImreadModes.</param>
    /// <param name="mats">A vector of Mat objects holding each page, if more than one.</param>
    /// <returns></returns>
    public static bool ImDecodeMulti(ReadOnlySpan<byte> span, ImreadModes flags, out DisposableArray<Mat> mats)
    {
        if (span.IsEmpty)
            throw new ArgumentException("Empty span", nameof(span));
        
        using var matsVec = new VectorOfMat();

        unsafe
        {
            fixed (byte* pBuf = span)
            {
                NativeMethods.HandleException(
                    NativeMethods.imgcodecs_imdecodemulti_bytes(
                        pBuf, span.Length, (int)flags, matsVec.Handle, out var ret));
                mats = new DisposableArray<Mat>(matsVec.ToArray());
                return ret != 0;
            }
        }
    }

    /// <summary>
    /// Compresses the image and stores it in the memory buffer
    /// </summary>
    /// <param name="ext">The file extension that defines the output format</param>
    /// <param name="img">The image to be written</param>
    /// <param name="buf">Output buffer resized to fit the compressed image.</param>
    /// <param name="parameters">Format-specific parameters.</param>
    public static bool ImEncode(string ext, IInputArray img, out byte[] buf, int[]? parameters = null)
    {
        ThrowIfNullOrEmpty(ext);
        ThrowIfNull(img);
        parameters ??= Array.Empty<int>();

        using var bufVec = new VectorOfByte();
        using var imgHandle = img.ToInputArrayHandle();
        NativeMethods.HandleException(
            NativeMethods.imgcodecs_imencode_vector(
                ext, imgHandle, bufVec.Handle, parameters, parameters.Length, out var ret));
        GC.KeepAlive(img);
        buf = bufVec.ToArray();
        return ret != 0;
    }

    /// <summary>
    /// Compresses the image and stores it in the memory buffer
    /// </summary>
    /// <param name="ext">The file extension that defines the output format</param>
    /// <param name="img">The image to be written</param>
    /// <param name="buf">Output buffer resized to fit the compressed image.</param>
    /// <param name="parameters">Format-specific parameters.</param>
    public static void ImEncode(string ext, IInputArray img, out byte[] buf, params ImageEncodingParam[] parameters)
    {
        ThrowIfNull(parameters);
        var p = new List<int>();
        foreach (var item in parameters)
        {
            p.Add((int)item.EncodingId);
            p.Add(item.Value);
        }
        ImEncode(ext, img, out buf, p.ToArray());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static bool HaveImageReader(string fileName)
    {
        ThrowIfNullOrEmpty(fileName);

        NativeMethods.HandleException(
            NativeMethods.imgcodecs_haveImageReader(fileName, out var ret));
        return ret != 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static bool HaveImageWriter(string fileName)
    {
        ThrowIfNullOrEmpty(fileName);

        NativeMethods.HandleException(
            NativeMethods.imgcodecs_haveImageWriter(fileName, out var ret));
        return ret != 0;
    }
}
