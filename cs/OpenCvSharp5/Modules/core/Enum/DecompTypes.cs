﻿namespace OpenCvSharp5;

/// <summary>
/// matrix decomposition types
/// </summary>
[Flags]
public enum DecompTypes
{
    /// <summary>
    /// Gaussian elimination with the optimal pivot element chosen.
    /// </summary>
    LU = 0,
    
    /// <summary>
    /// singular value decomposition (SVD) method; the system can be over-defined and/or the matrix
    /// src1 can be singular 
    /// </summary>
    SVD = 1,
    
    /// <summary>
    /// eigenvalue decomposition; the matrix src1 must be symmetrical
    /// </summary>
    EIG = 2,
    
    /// <summary>
    /// Cholesky \f$LL^T\f$ factorization; the matrix src1 must be symmetrical and positively defined
    /// </summary>
    CHOLESKY = 3,
    
    /// <summary>
    /// QR factorization; the system can be over-defined and/or the matrix src1 can be singular
    /// </summary>
    QR = 4,
    
    /// <summary>
    /// while all the previous flags are mutually exclusive, this flag can be used together with any of the previous
    /// </summary>
    NORMAL = 16
}
