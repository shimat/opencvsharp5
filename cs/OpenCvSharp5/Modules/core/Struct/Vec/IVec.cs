﻿#pragma warning disable CA1040 // Avoid empty interfaces

namespace OpenCvSharp5;

/// <summary>
/// Vec empty interface
/// </summary>
public interface IVec
{ }

/// <summary>
/// Vec** interface
/// </summary>
/// <typeparam name="TSelf"></typeparam>
/// <typeparam name="TElem"></typeparam>
public interface IVec<TSelf, out TElem> : IVec
    where TSelf : IVec
    where TElem : unmanaged
{
    /// <summary>
    /// this + other
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public TSelf Add(TSelf other);

    /// <summary>
    /// this - other
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public TSelf Subtract(TSelf other);

    /// <summary>
    /// this * alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public TSelf Multiply(double alpha);

    /// <summary>
    /// this * alpha
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public TSelf Divide(double alpha);

    /// <summary>
    /// indexer
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    TElem this[int i] { get; }
}
