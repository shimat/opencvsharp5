using OpenCvSharp5.Internal;

namespace OpenCvSharp5;

static partial class Cv2
{
    /// <summary>
    /// Draws a simple, thick, or filled up-right rectangle.
    /// </summary>
    /// <param name="img">Image.</param>
    /// <param name="pt1"></param>
    /// <param name="pt2"></param>
    /// <param name="color"></param>
    /// <param name="thickness"></param>
    /// <param name="lineType"></param>
    /// <param name="shift"></param>
    /// <exception cref="NotImplementedException"></exception>
    public static void Rectangle(
        IInputOutputArray img, 
        Point pt1, 
        Point pt2,
        Scalar color, 
        int thickness = 1,
        LineTypes lineType = LineTypes.Link8, 
        int shift = 0)
    {
        using var imgHandle = img.ToInputOutputArrayHandle();

        NativeMethods.HandleException(
            NativeMethods.imgproc_rectangle1(
                imgHandle,
                pt1,
                pt2,
                color,
                thickness,
                lineType,
                shift));

        GC.KeepAlive(img);
    }

    /// <summary>
    /// use `rec` parameter as alternative specification of the drawn rectangle: `r.tl() and
    /// r.br()-Point(1,1)` are opposite corners
    /// </summary>
    /// <param name="img">Image.</param>
    /// <param name="rec"></param>
    /// <param name="color"></param>
    /// <param name="thickness"></param>
    /// <param name="lineType"></param>
    /// <param name="shift"></param>
    /// <exception cref="NotImplementedException"></exception>
    public static void Rectangle(
        IInputOutputArray img,
        Rect rec,
        in Scalar color,
        int thickness = 1,
        LineTypes lineType = LineTypes.Link8,
        int shift = 0)
    {
        using var imgHandle = img.ToInputOutputArrayHandle();

        NativeMethods.HandleException(
            NativeMethods.imgproc_rectangle2(
                imgHandle,
                rec,
                color,
                thickness,
                lineType,
                shift));

        GC.KeepAlive(img);
    }
}

