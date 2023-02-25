using OpenCvSharp5.Internal;

namespace OpenCvSharp5;

static partial class Cv2
{
    public static void Rectangle(
        IInputOutputArray img, 
        Point pt1, 
        Point pt2,
        Scalar color, 
        int thickness = 1,
        LineTypes lineType = LineTypes.Link8, 
        int shift = 0)
    {
        throw new NotImplementedException();
    }

    public static void Rectangle(
        IInputOutputArray img,
        Rect rec,
        Scalar color,
        int thickness = 1,
        LineTypes lineType = LineTypes.Link8,
        int shift = 0)
    {
        throw new NotImplementedException();
    }
}

public interface IInputOutputArray{}

internal class InputOutputArrayImpl
{
    public static InputOutputArrayImpl FromMat(Mat mat)
    {
        throw new NotImplementedException();
    }
}
