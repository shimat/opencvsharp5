using System.Runtime.InteropServices;

namespace OpenCvSharp5;

/// <summary>
/// 
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public record struct Size(int Width, int Height)
{
    /// <summary>
    /// </summary>
    public int Width = Width;

    /// <summary>
    /// </summary>
    public int Height = Height;
}

/// <summary>
/// 
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
// ReSharper disable once InconsistentNaming
public record struct Size2f(float Width, float Height)
{
    /// <summary>
    /// </summary>
    public float Width = Width;

    /// <summary>
    /// </summary>
    public float Height = Height;
}

/// <summary>
/// 
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public record struct Size2d(double Width, double Height)
{
    /// <summary>
    /// </summary>
    public double Width = Width;

    /// <summary>
    /// </summary>
    public double Height = Height;
}
