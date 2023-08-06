namespace OpenCvSharp5.Tests;

public class DisposableArrayTests
{
    [Fact]
    public void Dispose()
    {
        var array = new[] { new MemoryStream(new byte[] {1}) };

        DisposableArray<MemoryStream> disposableArray;
        using (disposableArray = array.ToDisposableArray())
        {
        }

        Assert.Throws<ObjectDisposedException>(() => disposableArray[0].ReadByte());
    }
}
