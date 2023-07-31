using System.Runtime.InteropServices;

namespace OpenCvSharp5.Tests.Core;

public class MatExprTests
{
    [Fact]
    public void NewAndDispose()
    {
        using var matExpr = new MatExpr();
    }

    [Fact]
    public void FromMatToMat()
    {
        using var srcMat = new Mat(2, 2, MatType.CV_32SC1, new [,]
        {
            { 1, 2 },
            { 3, 4 }
        });
        using var matExpr = new MatExpr(srcMat);
        using var dstMat = matExpr.ToMat();

        Assert.Equal(new Size(2, 2), dstMat.Size());
        Assert.Equal(MatType.CV_32SC1, dstMat.Type());
        Assert.Equal(1, Marshal.ReadInt32(dstMat.Data, 0));
        Assert.Equal(2, Marshal.ReadInt32(dstMat.Data, 4));
        Assert.Equal(3, Marshal.ReadInt32(dstMat.Data, 8));
        Assert.Equal(4, Marshal.ReadInt32(dstMat.Data, 12));
    } 

    [Fact]
    public void Size()
    {
        using var mat = new Mat(3, 4, MatType.CV_32SC4, Scalar.All(1));
        using var matExpr = new MatExpr(mat);
        
        Assert.Equal(new Size(4, 3), matExpr.Size());
    } 

    [Fact]
    public void Type()
    {
        using var mat = new Mat(3, 4, MatType.CV_64FC3, Scalar.All(1));
        using var matExpr = new MatExpr(mat);
        
        Assert.Equal(MatType.CV_64FC3, matExpr.Type());
    }

    [Fact]
    public void Row()
    {
        using var srcMat = new Mat(2, 2, MatType.CV_8UC1, new byte[,]
        {
            { 1, 2 },
            { 3, 4 }
        });
        using var matExpr = new MatExpr(srcMat);
        using var row = matExpr.Row(1);
        using var dstMat = row.ToMat();

        Assert.Equal(new Size(2, 1), dstMat.Size());
        Assert.Equal(MatType.CV_8UC1, dstMat.Type());
        Assert.Equal(3, Marshal.ReadByte(dstMat.Data, 0));
        Assert.Equal(4, Marshal.ReadByte(dstMat.Data, 1));
    } 

    [Fact]
    public void Col()
    {
        using var srcMat = new Mat(2, 2, MatType.CV_8UC1, new byte[,]
        {
            { 1, 2 },
            { 3, 4 }
        });
        using var matExpr = new MatExpr(srcMat);
        using var row = matExpr.Col(0);
        using var dstMat = row.ToMat();

        Assert.Equal(new Size(1, 2), dstMat.Size());
        Assert.Equal(MatType.CV_8UC1, dstMat.Type());
        Assert.Equal(1, Marshal.ReadByte(dstMat.Data, 0));
        Assert.Equal(3, Marshal.ReadByte(dstMat.Data, 2));
    } 
}
