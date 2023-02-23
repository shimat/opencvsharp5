namespace OpenCvSharp5.Tests;

public class ExceptionHandlingTests
{
    private const int TrialCount = 10;

    [Fact]
    public void MatException()
    {
        for (var i = 0; i < TrialCount; i++)
        {
            var ex = Assert.Throws<OpenCVException>(() =>
            {
                using var mat = new Mat(-1, -1, MatType.CV_8UC1);
            });

            Assert.Equal("s >= 0", ex.ErrMsg);
            Assert.Equal("cv::setSize", ex.FuncName);
            Assert.EndsWith("matrix.cpp", ex.FileName);
            Assert.Equal(246, ex.Line);
        }
    }
}
