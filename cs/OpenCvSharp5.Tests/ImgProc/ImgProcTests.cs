namespace OpenCvSharp5.Tests.ImgProc;

public class ImgProcTests
{
    [Fact]
    public void Rectangle1()
    {
        using var mat = new Mat(10, 10, MatType.CV_8UC1, Scalar.All(0));

        Cv2.Rectangle(
            mat,
            new Point(1, 1),
            new Point(8, 8),
            Scalar.All(255),
            -1);
    }

    [Fact]
    public void Rectangle2()
    {
        using var mat = new Mat(10, 10, MatType.CV_8UC1, Scalar.All(0));

        Cv2.Rectangle(
            mat, 
            new Rect(1, 1, 8, 8),
            Scalar.All(255),
            -1);
    }
}
