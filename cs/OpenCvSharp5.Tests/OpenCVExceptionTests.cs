namespace OpenCvSharp5.Tests;

// ReSharper disable once InconsistentNaming
public class OpenCVExceptionTests
{
    [Fact]
    public void Default()
    {
        var ex = new OpenCVException();
        Assert.Equal(default, ex.Status);
        Assert.Equal("", ex.FuncName);
        Assert.Equal("", ex.ErrMsg);
        Assert.Equal("", ex.FileName);
        Assert.Equal(0, ex.Line);
        Assert.Equal(0, ex.UserData);

        Assert.Equal("Exception of type 'OpenCvSharp5.OpenCVException' was thrown.", ex.Message);
        Assert.Null(ex.InnerException);
    }

    [Fact]
    // ReSharper disable once InconsistentNaming
    public void FromOpenCVError()
    {
        var ex = new OpenCVException(
            status: ErrorCode.StsNotImplemented,
            funcName: "fooFunc",
            errMsg: "Hello World",
            fileName: "/a/b/opencv/modules/core/src/xxx.cpp",
            line: 1,
            userData: 2);

        Assert.Equal(ErrorCode.StsNotImplemented, ex.Status);
        Assert.Equal("fooFunc", ex.FuncName);
        Assert.Equal("Hello World", ex.ErrMsg);
        Assert.Equal("opencv/modules/core/src/xxx.cpp", ex.FileName);
        Assert.Equal(1, ex.Line);
        Assert.Equal(2, ex.UserData);

        Assert.Equal("Hello World (FuncName=fooFunc, FileName=opencv/modules/core/src/xxx.cpp, Line=1, Status=StsNotImplemented)", ex.Message);
        Assert.Null(ex.InnerException);
    }
}
