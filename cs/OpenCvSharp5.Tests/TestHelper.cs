namespace OpenCvSharp5.Tests;

public static class TestHelper
{
    public static void ImageEquals(Mat img1, Mat img2)
    {
        if (img1 is null && img2 is null)
            return;

        Assert.NotNull(img1);
        Assert.NotNull(img2);

        Assert.Equal(img1.Type(), img2.Type());

        using var comparison = new Mat();
        Cv2.Compare(img1, img2, comparison, CmpTypes.NE);

        if (img1.Channels() == 1)
        {
            Assert.Equal(0, Cv2.CountNonZero(comparison));
        }
        else
        {
            var channels = Cv2.Split(comparison);
            try
            {
                foreach (var channel in channels)
                {
                    Assert.Equal(0, Cv2.CountNonZero(channel));
                }
            }
            finally
            {
                foreach (var channel in channels)
                {
                    channel.Dispose();
                }
            }
        }
    }
}
