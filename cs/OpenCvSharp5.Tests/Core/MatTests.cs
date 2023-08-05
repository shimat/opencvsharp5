namespace OpenCvSharp5.Tests.Core;

public class MatTests
{
    [Fact]
    public void New1()
    {
        using var mat = new Mat();
    }
    
    [Fact]
    public void New2()
    {
        using var mat = new Mat(2, 3, MatType.CV_16SC4);

        Assert.Equal(2, mat.Rows);
        Assert.Equal(3, mat.Cols);
        Assert.Equal(new Size(3, 2), mat.Size());
        Assert.Equal(MatType.CV_16SC4, mat.Type());
        Assert.True(mat.IsContinuous());
        Assert.False(mat.IsSubmatrix());
    }

    [Fact]
    public void New3()
    {
        using var mat = new Mat(2, 3, MatType.CV_16SC4, new Scalar(1, 2, 3, 4));

        Assert.Equal(2, mat.Rows);
        Assert.Equal(3, mat.Cols);
        Assert.Equal(new Size(3, 2), mat.Size());
        Assert.Equal(MatType.CV_16SC4, mat.Type());
        Assert.True(mat.IsContinuous());
        Assert.False(mat.IsSubmatrix());

        foreach (var item in mat.AsSpan<Vec4<short>>())
        {
            Assert.Equal(new Vec4<short>(1, 2, 3, 4), item);
        }
    }
    
    [Fact]
    public void New4()
    {
        using var mat = new Mat(new Size(3, 2), MatType.CV_16SC4);

        Assert.Equal(2, mat.Rows);
        Assert.Equal(3, mat.Cols);
        Assert.Equal(new Size(3, 2), mat.Size());
        Assert.Equal(MatType.CV_16SC4, mat.Type());
        Assert.True(mat.IsContinuous());
        Assert.False(mat.IsSubmatrix());
    }

    [Fact]
    public void New5()
    {
        using var mat = new Mat(new Size(3, 2), MatType.CV_16SC4, new Scalar(1, 2, 3, 4));

        Assert.Equal(2, mat.Rows);
        Assert.Equal(3, mat.Cols);
        Assert.Equal(new Size(3, 2), mat.Size());
        Assert.Equal(MatType.CV_16SC4, mat.Type());
        Assert.True(mat.IsContinuous());
        Assert.False(mat.IsSubmatrix());

        foreach (var item in mat.AsSpan<Vec4<short>>())
        {
            Assert.Equal(new Vec4<short>(1, 2, 3, 4), item);
        }
    }

    [Fact]
    public void New6()
    {
        using var mat = new Mat(new []{1, 2, 3}, MatType.CV_32SC4);

        Assert.Equal(3, mat.Dims);
        Assert.Equal(1, mat.Size(0));
        Assert.Equal(2, mat.Size(1));
        Assert.Equal(3, mat.Size(2));
        Assert.Equal(MatType.CV_32SC4, mat.Type());
        Assert.True(mat.IsContinuous());
        Assert.False(mat.IsSubmatrix());
    }
    
    [Fact]
    public void New7()
    {
        using var mat = new Mat(new []{1, 2, 3}, MatType.CV_32SC4, new Scalar(1, 2, 3, 4));

        Assert.Equal(3, mat.Dims);
        Assert.Equal(1, mat.Size(0));
        Assert.Equal(2, mat.Size(1));
        Assert.Equal(3, mat.Size(2));
        Assert.Equal(MatType.CV_32SC4, mat.Type());
        Assert.True(mat.IsContinuous());
        Assert.False(mat.IsSubmatrix());

        foreach (var item in mat.AsSpan<Vec4<int>>())
        {
            Assert.Equal(new Vec4<int>(1, 2, 3, 4), item);
        }
    }

    [Fact]
    public void AsRowSpan()
    {
        var matData = new int[]
        {
            1, 2, 3,
            4, 5, 6,
            7, 8, 9
        };
        using var mat = new Mat(3, 3, MatType.CV_32SC1, matData);

        var row0 = mat.AsRowSpan<int>(0);
        var row1 = mat.AsRowSpan<int>(1);
        var row2 = mat.AsRowSpan<int>(2);
        Assert.Equal(3, row0.Length);
        Assert.Equal(3, row1.Length);
        Assert.Equal(3, row2.Length);
        Assert.True(row0.SequenceEqual(new[] { 1, 2, 3 }));
        Assert.True(row1.SequenceEqual(new[] { 4, 5, 6 }));
        Assert.True(row2.SequenceEqual(new[] { 7, 8, 9 }));
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
            """.Replace("\r\n", "\n"), Cv2.Format(diag));
    }

    [Fact]
    public void CopyTo()
    {
        var matData = new short[,]
        {
            { 1, 2, 3 },
            { 4, 5, 6 }
        };
        using var src = new Mat(2, 3, MatType.CV_16SC1, matData);
        using var dst = new Mat();

        src.CopyTo(dst);

        Assert.Equal(2, dst.Rows);
        Assert.Equal(3, dst.Cols);
        Assert.Equal(MatType.CV_16SC1, dst.Type());

        Assert.Equal("""
            [1, 2, 3;
             4, 5, 6]
            """.Replace("\r\n", "\n"), Cv2.Format(dst));
    }

    [Fact]
    public void ConvertTo()
    {
        var matData = new short[,]
        {
            { 1, 2, 3 },
            { 4, 5, 6 }
        };
        using var src = new Mat(2, 3, MatType.CV_16SC1, matData);
        using var dst = new Mat();

        src.ConvertTo(dst, MatType.CV_32SC1, 2, 1);

        Assert.Equal(2, dst.Rows);
        Assert.Equal(3, dst.Cols);
        Assert.Equal(MatType.CV_32SC1, dst.Type());

        Assert.Equal("""
            [3, 5, 7;
             9, 11, 13]
            """.Replace("\r\n", "\n"), Cv2.Format(dst));
    }

    [Fact]
    public void AssignTo()
    {
        var matData = new [,]
        {
            { 1, 2 },
            { 3, 4 }
        };
        using var src = new Mat(2, 2, MatType.CV_32SC1, matData);
        using var dst = new Mat();

        src.AssignTo(dst, MatType.CV_8UC1);

        Assert.Equal(2, dst.Rows);
        Assert.Equal(2, dst.Cols);
        Assert.Equal(MatType.CV_8UC1, dst.Type());

        Assert.Equal("""
            [  1,   2;
               3,   4]
            """.Replace("\r\n", "\n"), Cv2.Format(dst));
    }

    [Fact]
    public void SetTo()
    {
        var matData = new byte[,]
        {
            { 1, 2 },
            { 3, 4 }
        };
        using var mat = new Mat(2, 2, MatType.CV_8UC1, matData);

        var ret = mat.SetTo(new Scalar(7));

        Assert.Same(mat, ret);

        Assert.Equal(2, mat.Rows);
        Assert.Equal(2, mat.Cols);
        Assert.Equal(MatType.CV_8UC1, mat.Type());

        Assert.Equal("""
            [  7,   7;
               7,   7]
            """.Replace("\r\n", "\n"), Cv2.Format(mat));
    }

    [Fact]
    public void SetZero()
    {
        var matData = new byte[,]
        {
            { 1, 2 },
            { 3, 4 }
        };
        using var mat = new Mat(2, 2, MatType.CV_8UC1, matData);

        var ret = mat.SetZero();

        Assert.Same(mat, ret);

        Assert.Equal(2, mat.Rows);
        Assert.Equal(2, mat.Cols);
        Assert.Equal(MatType.CV_8UC1, mat.Type());

        Assert.Equal(0, Cv2.CountNonZero(mat));
    }

    [Fact]
    public void ReShape()
    {
        var matData = new Vec2<byte>[,]
        {
            { new(1, 2), new(3, 4) },
            { new(5, 6), new(7, 8) }
        };
        using var src = new Mat(2, 2, MatType.CV_8UC2, matData);

        using var dst1 = src.Reshape(1, 2);
        Assert.Equal(MatType.CV_8UC1, dst1.Type());
        Assert.Equal(new Size(4, 2), dst1.Size());

        using var dst2 = src.Reshape(2, 4);
        Assert.Equal(MatType.CV_8UC2, dst2.Type());
        Assert.Equal(new Size(1, 4), dst2.Size());
    }

    [Fact]
    public void T()
    {
        var matData = new byte[,]
        {
            { 1, 2 },
            { 3, 4 }
        };
        using var src = new Mat(2, 2, MatType.CV_8UC1, matData);
        using var dstExpr = src.T();
        using var dst = dstExpr.ToMat();

        Assert.Equal(new Size(2, 2), dstExpr.Size());
        Assert.Equal(MatType.CV_8UC1, dstExpr.Type());

        Assert.Equal("""
            [  1,   3;
               2,   4]
            """.Replace("\r\n", "\n"), Cv2.Format(dst));
    }

    [Fact]
    public void Inv()
    {
        var matData = new double[,]
        {
            { 1, 2 },
            { 3, 4 }
        };
        using var src = new Mat(2, 2, MatType.CV_64FC1, matData);
        using var dstExpr = src.Inv();
        using var dst = dstExpr.ToMat();

        Assert.Equal(new Size(2, 2), dstExpr.Size());
        Assert.Equal(MatType.CV_64FC1, dstExpr.Type());

        Assert.Equal("""
            [-2, 1;
             1.5, -0.5]
            """.Replace("\r\n", "\n"), Cv2.Format(dst));
    }

    [Fact]
    public void Zeros()
    {
        using var zerosExpr1 = Mat.Zeros(2, 3, MatType.CV_8UC1);
        Assert.Equal(new Size(3, 2), zerosExpr1.Size());
        Assert.Equal(MatType.CV_8UC1, zerosExpr1.Type());
        Assert.Equal(0, Cv2.CountNonZero(zerosExpr1));

        using var zerosExpr2 = Mat.Zeros(new Size(3, 2), MatType.CV_8UC1);
        Assert.Equal(new Size(3, 2), zerosExpr2.Size());
        Assert.Equal(MatType.CV_8UC1, zerosExpr2.Type());
        Assert.Equal(0, Cv2.CountNonZero(zerosExpr2));
    }

    [Fact]
    public void Ones()
    {
        using var onesExpr1 = Mat.Ones(1, 2, MatType.CV_16UC1);
        using var onesMat1 = onesExpr1.ToMat();
        Assert.Equal(new Size(2, 1), onesExpr1.Size());
        Assert.Equal(MatType.CV_16UC1, onesExpr1.Type());
        Assert.Equal(2, Cv2.CountNonZero(onesExpr1));
        Assert.Equal((ushort)1, onesMat1.Get<ushort>(0, 0));
        Assert.Equal((ushort)1, onesMat1.Get<ushort>(0, 1));

        using var onesExpr2 = Mat.Ones(new Size(2, 1), MatType.CV_16UC1);
        using var onesMat2 = onesExpr2.ToMat();
        Assert.Equal(new Size(2, 1), onesExpr2.Size());
        Assert.Equal(MatType.CV_16UC1, onesExpr2.Type());
        Assert.Equal(2, Cv2.CountNonZero(onesExpr2));
        Assert.Equal((ushort)1, onesMat2.Get<ushort>(0, 0));
        Assert.Equal((ushort)1, onesMat2.Get<ushort>(0, 1));
    }

    [Fact]
    public void Eye()
    {
        using var eyeExpr1 = Mat.Eye(2, 2, MatType.CV_32SC1);
        using var eyeMat1 = eyeExpr1.ToMat();
        Assert.Equal(new Size(2, 2), eyeExpr1.Size());
        Assert.Equal(MatType.CV_32SC1, eyeExpr1.Type());
        Assert.Equal(2, Cv2.CountNonZero(eyeMat1));
        Assert.Equal(1, eyeMat1.Get<int>(0, 0));
        Assert.Equal(0, eyeMat1.Get<int>(0, 1));
        Assert.Equal(0, eyeMat1.Get<int>(1, 0));
        Assert.Equal(1, eyeMat1.Get<int>(1, 1));

        using var eyeExpr2 = Mat.Eye(new Size(2, 2), MatType.CV_32SC1);
        using var eyeMat2 = eyeExpr2.ToMat();
        Assert.Equal(new Size(2, 2), eyeExpr2.Size());
        Assert.Equal(MatType.CV_32SC1, eyeExpr2.Type());
        Assert.Equal(2, Cv2.CountNonZero(eyeMat2));
        Assert.Equal(1, eyeMat2.Get<int>(0, 0));
        Assert.Equal(0, eyeMat2.Get<int>(0, 1));
        Assert.Equal(0, eyeMat2.Get<int>(1, 0));
        Assert.Equal(1, eyeMat2.Get<int>(1, 1));
    }
    
    [Fact]
    public void Create()
    {
        using var mat = new Mat();

        mat.Create(2, 3, MatType.CV_32FC1);
        Assert.Equal(2, mat.Rows);
        Assert.Equal(3, mat.Cols);
        Assert.Equal(MatType.CV_32FC1, mat.Type());

        mat.Create(new Size(5, 3), MatType.CV_8SC2);
        Assert.Equal(3, mat.Rows);
        Assert.Equal(5, mat.Cols);
        Assert.Equal(MatType.CV_8SC2, mat.Type());

        mat.Create(new[] { 1, 2, 3 }, MatType.CV_64FC4);
        Assert.Equal(3, mat.Dims);
        Assert.Equal(1, mat.Size(0));
        Assert.Equal(2, mat.Size(1));
        Assert.Equal(3, mat.Size(2));
        Assert.Equal(MatType.CV_64FC4, mat.Type());
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
        Assert.Equal(mat.Data, (nint)mat.Ptr());
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
