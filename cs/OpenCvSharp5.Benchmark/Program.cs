using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using OpenCvSharp5;

var summary = BenchmarkRunner.Run<MatFieldsMeasurement>();

public class MatFieldsMeasurement
{
    private int LoopCount = 10000;
    
    [Benchmark]
    public int RowCol()
    {
        using var mat = new Mat(3, 4, 0); var sum = 0;
        for (var i = 0; i < LoopCount; i++)
        {
            sum += mat.Rows;
            sum += mat.Cols;
        }
        return sum;
    }
}
