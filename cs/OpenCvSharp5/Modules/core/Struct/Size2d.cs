﻿using System.Runtime.InteropServices;

#pragma warning disable CA1051

namespace OpenCvSharp5;

/// <summary>
/// 
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public struct Size2d : IEquatable<Size2d>
{
    /// <summary>
    /// 
    /// </summary>
    public double Width;

    /// <summary>
    /// 
    /// </summary>
    public double Height;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public Size2d(float width, float height)
    {
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public Size2d(double width, double height)
    {
        Width = width;
        Height = height;
    }

    #region Operators

    /// <summary>
    /// Compares two CvPoint objects. The result specifies whether the members of each object are equal.
    /// </summary>
    /// <param name="lhs">A Point to compare.</param>
    /// <param name="rhs">A Point to compare.</param>
    /// <returns>This operator returns true if the members of left and right are equal; otherwise, false.</returns>
    public static bool operator ==(Size2d lhs, Size2d rhs) => lhs.Equals(rhs);

    /// <summary>
    /// Compares two CvPoint objects. The result specifies whether the members of each object are unequal.
    /// </summary>
    /// <param name="lhs">A Point to compare.</param>
    /// <param name="rhs">A Point to compare.</param>
    /// <returns>This operator returns true if the members of left and right are unequal; otherwise, false.</returns>
    public static bool operator !=(Size2d lhs, Size2d rhs) => !lhs.Equals(rhs);

    #endregion

    #region Override
        
    /// <inheritdoc />
    public readonly bool Equals(Size2d other) => Width.Equals(other.Width) && Height.Equals(other.Height);

    /// <inheritdoc />
    public override readonly bool Equals(object? obj) => obj is Size2d other && Equals(other);

    /// <inheritdoc />
    public override readonly int GetHashCode()
    {
#if DOTNET_FRAMEWORK || NETSTANDARD2_0
            unchecked
            {
                return (Width.GetHashCode() * 397) ^ Height.GetHashCode();
            }
#else
        return HashCode.Combine(Width, Height);
#endif
    }

    /// <inheritdoc />
    public override readonly string ToString() => $"(width:{Width} height:{Height})";

    #endregion

}
