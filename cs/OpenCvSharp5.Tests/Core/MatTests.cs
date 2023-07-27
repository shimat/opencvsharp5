using System.Runtime.InteropServices;

namespace OpenCvSharp5.Tests.Core;

public class MatTests
{
    [Fact]
    public void NewAndDispose()
    {
        using var mat = new Mat();
    }

    [Fact]
    public void RowsCols()
    {
        using var mat = new Mat(3, 4, MatType.CV_8UC1);
        Assert.Equal(3, mat.Rows);
        Assert.Equal(4, mat.Cols);
    }
    
    [Fact]
    public void RowRange()
    {
        using var mat = new Mat(3, 4, MatType.CV_8UC1);
        using var matRange1 = mat.RowRange(0, 1);
        Assert.Equal(1, matRange1.Rows);
        Assert.Equal(4, matRange1.Cols);
        using var matRange2 = mat.RowRange(new Range(1, 3));
        Assert.Equal(2, matRange2.Rows);
        Assert.Equal(4, matRange2.Cols);
        using var matRange3 = mat.RowRange(Range.All);
        Assert.Equal(3, matRange3.Rows);
        Assert.Equal(4, matRange3.Cols);
    }
    
    [Fact]
    public void ColRange()
    {
        using var mat = new Mat(3, 4, MatType.CV_8UC1);
        using var matRange1 = mat.ColRange(0, 1);
        Assert.Equal(3, matRange1.Rows);
        Assert.Equal(1, matRange1.Cols);
        using var matRange2 = mat.ColRange(new Range(1, 3));
        Assert.Equal(3, matRange2.Rows);
        Assert.Equal(2, matRange2.Cols);
        using var matRange3 = mat.ColRange(Range.All);
        Assert.Equal(3, matRange3.Rows);
        Assert.Equal(4, matRange3.Cols);
    }
    
    [Fact]
    public void Diag()
    {
        var matData = new byte[]
        {
            1, 2, 3,
            4, 5, 6,
            7, 8, 9
        };
        using var mat = new Mat(3, 3, MatType.CV_8UC1, matData);

        using var diag = mat.Diag();
        Assert.Equal(3, diag.Rows);
        Assert.Equal(1, diag.Cols);
        Assert.Equal(MatType.CV_8UC1, diag.Type());
        Assert.Equal(4, diag.Step());

        Assert.Equal("""
            [  1;
               5;
               9]
            """.Replace("\r\n", "\n"), Cv2.Format(diag, FormatType.Default));
    }

    [Fact]
    public void Data()
    {
        using var mat = new Mat(3, 4, MatType.CV_8UC1);
        Assert.NotEqual(IntPtr.Zero, mat.Data);
    }

    [Fact]
    public void Size()
    {
        using var mat = new Mat(3, 4, MatType.CV_8UC1);
        Assert.Equal(new Size(4, 3), mat.Size());
        Assert.Equal(3, mat.Size(0));
        Assert.Equal(4, mat.Size(1));
    }

    [Fact]
    public void Step()
    {
        using var mat = new Mat(3, 7, MatType.CV_8UC1);
        Assert.Equal(7, mat.Step());
        Assert.Equal(7, mat.Step(0));
    }
    
    [Fact]
    public void IsContinuous()
    {
        using var mat = new Mat(3, 3, MatType.CV_8UC3, Scalar.All(1));
        Assert.True(mat.IsContinuous());

        // TODO
    }
    
    [Fact]
    public void IsSubmatrix()
    {
        using var mat = new Mat(3, 3, MatType.CV_8UC3, Scalar.All(1));
        Assert.False(mat.IsSubmatrix());

        // TODO
    }
    
    [Fact]
    public void ElemSize()
    {
        using var mat1 = new Mat(1, 1, MatType.CV_8UC1);
        Assert.Equal(1, mat1.ElemSize());
        using var mat2 = new Mat(1, 1, MatType.CV_8UC4);
        Assert.Equal(4, mat2.ElemSize());
    }
    
    [Fact]
    public void ElemSize1()
    {
        using var mat1 = new Mat(1, 1, MatType.CV_8UC1);
        Assert.Equal(1, mat1.ElemSize1());
        using var mat2 = new Mat(1, 1, MatType.CV_8UC4);
        Assert.Equal(1, mat2.ElemSize1());
    }

    [Fact]
    public void Type()
    {
        using var mat = new Mat(1, 1, MatType.CV_8UC3);
        Assert.Equal(MatType.CV_8UC3, mat.Type());
    }

    [Fact]
    public unsafe void Ptr1()
    {
        using var mat = new Mat(3, 2, MatType.CV_8UC1);
        Assert.Equal(mat.Data, (nint)mat.Ptr(0));
        Assert.Equal(mat.Data + 2, (nint)mat.Ptr(1));
        Assert.Equal(mat.Data + 4, (nint)mat.Ptr(2));

        Assert.Equal((nint)((byte*)mat.Data + 4), (nint)mat.Ptr<byte>(2));
    }

    [Fact]
    public unsafe void Ptr2()
    {
        using var mat = new Mat(3, 3, MatType.CV_8UC1);
        Assert.Equal(mat.Data, (nint)mat.Ptr(0, 0));
        Assert.Equal(mat.Data + 4, (nint)mat.Ptr(1, 1));
        Assert.Equal(mat.Data + 7, (nint)mat.Ptr(2, 1));

        Assert.Equal((nint)((byte*)mat.Data + 7), (nint)mat.Ptr<byte>(2, 1));
    }

    [Fact]
    public void Get1()
    {
        using var mat = new Mat(3, 1, MatType.CV_8UC1, new byte[]{1, 2, 3});
        Assert.Equal((byte)1, mat.Get<byte>(0));
        Assert.Equal((byte)2, mat.Get<byte>(1));
        Assert.Equal((byte)3, mat.Get<byte>(2));
    }
    
    [Fact]
    public void Get2()
    {
        using var mat = new Mat(2, 2, MatType.CV_16UC1, new ushort[,]{
            {1, 2},
            {3, 4},
        });
        Assert.Equal((ushort)1, mat.Get<ushort>(0, 0));
        Assert.Equal((ushort)2, mat.Get<ushort>(0, 1));
        Assert.Equal((ushort)3, mat.Get<ushort>(1, 0));
        Assert.Equal((ushort)4, mat.Get<ushort>(1, 1));
    }

    [Fact]
    public void Set1()
    {
        using var mat = new Mat(3, 1, MatType.CV_32SC1);
        mat.Set(0, 0);
        mat.Set(1, 10);
        mat.Set(2, 20);

        Assert.Equal("""
            [0;
             10;
             20]
            """.Replace("\r\n", "\n"), mat.Dump());
    }

    [Fact]
    public void Set2()
    {
        using var mat = new Mat(2, 2, MatType.CV_16SC1);
        mat.Set<short>(0, 0, 0);
        mat.Set<short>(0, 1, 1);
        mat.Set<short>(1, 0, 10);
        mat.Set<short>(1, 1, 11);

        Assert.Equal("""
            [0, 1;
             10, 11]
            """.Replace("\r\n", "\n"), mat.Dump());
    }

    [Fact]
    public void At1()
    {
        using var mat = new Mat(3, 1, MatType.CV_8UC1, new byte[]{1, 2, 3});
        Assert.Equal((byte)1, mat.At<byte>(0));
        Assert.Equal((byte)2, mat.At<byte>(1));
        Assert.Equal((byte)3, mat.At<byte>(2));

        mat.At<byte>(0) = 10;
        mat.At<byte>(1) = 11;
        mat.At<byte>(2) = 12;

        Assert.Equal("""
            [ 10;
              11;
              12]
            """.Replace("\r\n", "\n"), mat.Dump());
    }

    [Fact]
    public void At2()
    {
        using var mat = new Mat(2, 2, MatType.CV_16UC1, new ushort[,]{
            {1, 2},
            {3, 4},
        });
        Assert.Equal((ushort)1, mat.At<ushort>(0, 0));
        Assert.Equal((ushort)2, mat.At<ushort>(0, 1));
        Assert.Equal((ushort)3, mat.At<ushort>(1, 0));
        Assert.Equal((ushort)4, mat.At<ushort>(1, 1));

        mat.At<ushort>(0, 0) = 100;
        mat.At<ushort>(0, 1) = 200;
        mat.At<ushort>(1, 0) = 300;
        mat.At<ushort>(1, 1) = 400;

        Assert.Equal("""
            [100, 200;
             300, 400]
            """.Replace("\r\n", "\n"), mat.Dump());
    }
}
