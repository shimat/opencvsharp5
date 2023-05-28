﻿using OpenCvSharp5.Internal;
using OpenCvSharp5.Internal.Vectors;

namespace OpenCvSharp5;

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
    public static bool ImReadMulti(string fileName, out Mat[] mats, ImreadModes flags = ImreadModes.AnyColor)
    {
        ArgumentException.ThrowIfNullOrEmpty(fileName);

        using var matsVec = new VectorOfMat();
        NativeMethods.HandleException(
            NativeMethods.imgcodecs_imreadmulti(fileName, matsVec.Handle, (int)flags, out var ret));
        mats = matsVec.ToArray();
        return ret != 0;
    }

    /// <summary>
    /// Saves an image to a specified file.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <param name="img">Image to be saved.</param>
    /// <param name="prms">Format-specific save parameters encoded as pairs</param>
    /// <returns></returns>
    public static bool ImWrite(string fileName, Mat img, int[]? prms = null)
    {
        if (string.IsNullOrEmpty(fileName))
            throw new ArgumentNullException(nameof(fileName));
        ArgumentNullException.ThrowIfNull(img);
        if (prms is null)
            prms = Array.Empty<int>();

        NativeMethods.HandleException(
            NativeMethods.imgcodecs_imwrite(fileName, img.Handle, prms, prms.Length, out var ret));
        GC.KeepAlive(img);
        return ret != 0;
    }

    /// <summary>
    /// Saves an image to a specified file.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <param name="img">Image to be saved.</param>
    /// <param name="prms">Format-specific save parameters encoded as pairs</param>
    /// <returns></returns>
    public static bool ImWrite(string fileName, Mat img, params ImageEncodingParam[] prms)
    {
        ArgumentNullException.ThrowIfNull(prms);
        if (prms.Length <= 0)
            return ImWrite(fileName, img);

        var p = new List<int>();
        foreach (var item in prms)
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
    /// <param name="prms">Format-specific save parameters encoded as pairs</param>
    /// <returns></returns>
    public static bool ImWrite(string fileName, IEnumerable<Mat> img, int[]? prms = null)
    {
        if (string.IsNullOrEmpty(fileName))
            throw new ArgumentNullException(nameof(fileName));
        ArgumentNullException.ThrowIfNull(img);
        prms ??= Array.Empty<int>();

        using var imgVec = new VectorOfMat(img);
        NativeMethods.HandleException(
            NativeMethods.imgcodecs_imwrite_multi(fileName, imgVec.Handle, prms, prms.Length, out var ret));
        GC.KeepAlive(img);
        return ret != 0;
    }

    /// <summary>
    /// Saves an image to a specified file.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <param name="img">Image to be saved.</param>
    /// <param name="prms">Format-specific save parameters encoded as pairs</param>
    /// <returns></returns>
    public static bool ImWrite(string fileName, IEnumerable<Mat> img, params ImageEncodingParam[] prms)
    {
        ArgumentNullException.ThrowIfNull(prms);
        if (prms.Length <= 0)
            return ImWrite(fileName, img);

        var p = new List<int>();
        foreach (var item in prms)
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
        ArgumentNullException.ThrowIfNull(buf);
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
        ArgumentNullException.ThrowIfNull(buf);

        using var imgHandle = buf.ToInputArrayHandle();

        NativeMethods.HandleException(
            NativeMethods.imgcodecs_imdecode_InputArray(imgHandle, (int)flags, out var ret));
        
        GC.KeepAlive(buf);
        return new Mat(ret);
    }

    /// <summary>
    /// Reads image from the specified buffer in memory.
    /// </summary>
    /// <param name="buf">The input array of vector of bytes.</param>
    /// <param name="flags">The same flags as in imread</param>
    /// <returns></returns>
    public static Mat ImDecode(byte[] buf, ImreadModes flags)
    {
        ArgumentNullException.ThrowIfNull(buf);
        var ret = ImDecode(new ReadOnlySpan<byte>(buf), flags);
        GC.KeepAlive(buf);
        return ret;
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
                    NativeMethods.imgcodecs_imdecode_vector(pBuf, span.Length, (int)flags, out var ret));
                return new Mat(ret);
            }
        }
    }

    /// <summary>
    /// Compresses the image and stores it in the memory buffer
    /// </summary>
    /// <param name="ext">The file extension that defines the output format</param>
    /// <param name="img">The image to be written</param>
    /// <param name="buf">Output buffer resized to fit the compressed image.</param>
    /// <param name="prms">Format-specific parameters.</param>
    public static bool ImEncode(string ext, IInputArray img, out byte[] buf, int[]? prms = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(ext);
        ArgumentNullException.ThrowIfNull(img);
        if (prms is null)
            prms = Array.Empty<int>();

        using var bufVec = new VectorOfByte();
        using var imgHandle = img.ToInputArrayHandle();
        NativeMethods.HandleException(
            NativeMethods.imgcodecs_imencode_vector(ext, imgHandle, bufVec.Handle, prms, prms.Length, out var ret));
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
    /// <param name="prms">Format-specific parameters.</param>
    public static void ImEncode(string ext, IInputArray img, out byte[] buf, params ImageEncodingParam[] prms)
    {
        ArgumentNullException.ThrowIfNull(prms);
        var p = new List<int>();
        foreach (var item in prms)
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
        ArgumentException.ThrowIfNullOrEmpty(fileName);

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
        ArgumentException.ThrowIfNullOrEmpty(fileName);

        NativeMethods.HandleException(
            NativeMethods.imgcodecs_haveImageWriter(fileName, out var ret));
        return ret != 0;
    }
}