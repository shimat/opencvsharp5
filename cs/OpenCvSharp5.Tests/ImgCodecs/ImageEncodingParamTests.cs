namespace OpenCvSharp5.Tests.ImgCodecs;

public class ImageEncodingParamTests
{
    [Fact]
    public void New()
    {
        var p = new ImageEncodingParam(ImwriteFlags.JpegChromaQuality, 77);
        Assert.Equal(ImwriteFlags.JpegChromaQuality, p.EncodingId);
        Assert.Equal(77, p.Value);
    }
}
