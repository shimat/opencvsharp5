﻿using System.Runtime.InteropServices;

namespace OpenCvSharp5;

/// <summary>
/// Template class specifying a continuous subsequence (slice) of a sequence.
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public readonly record struct Range(int Start, int End)
{
    /// <summary> 
    /// </summary>
    public readonly int Start = Start;

    /// <summary> 
    /// </summary>
    public readonly int End = End;
    
    /// <summary>
    /// 
    /// </summary>
    public static Range All => new (int.MinValue, int.MaxValue);
}
