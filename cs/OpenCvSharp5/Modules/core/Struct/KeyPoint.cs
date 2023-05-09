using System.Runtime.InteropServices;

namespace OpenCvSharp5;

/// <summary>
/// Data structure for salient point detectors
/// </summary>
/// <param name="Pt">Coordinate of the point</param>
/// <param name="Size">Feature size</param>
/// <param name="Angle">Feature orientation in degrees (has negative value if the orientation is not defined/not computed)</param>
/// <param name="Response">Feature strength (can be used to select only the most prominent key points)</param>
/// <param name="Octave">Scale-space octave in which the feature has been found; may correlate with the size</param>
/// <param name="ClassId">Point class (can be used by feature classifiers or object detectors)</param>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public record struct KeyPoint(Point2f Pt, float Size, float Angle, float Response, int Octave, int ClassId)
{
    /// <summary>
    /// Coordinate of the point
    /// </summary>
    public Point2f Pt = Pt;

    /// <summary>
    /// Feature size
    /// </summary>
    public float Size = Size;

    /// <summary>
    /// Feature orientation in degrees (has negative value if the orientation is not defined/not computed)
    /// </summary>
    public float Angle = Angle;

    /// <summary>
    /// Feature strength (can be used to select only the most prominent key points)
    /// </summary>
    public float Response = Response;

    /// <summary>
    /// Scale-space octave in which the feature has been found; may correlate with the size
    /// </summary>
    public int Octave = Octave;

    /// <summary>
    /// Point class (can be used by feature classifiers or object detectors)
    /// </summary>
    public int ClassId = ClassId;

    /// <summary>
    /// Complete constructor
    /// </summary>
    /// <param name="x">X-coordinate of the point</param>
    /// <param name="y">Y-coordinate of the point</param>
    /// <param name="size">Feature size</param>
    /// <param name="angle">Feature orientation in degrees (has negative value if the orientation is not defined/not computed)</param>
    /// <param name="response">Feature strength (can be used to select only the most prominent key points)</param>
    /// <param name="octave">Scale-space octave in which the feature has been found; may correlate with the size</param>
    /// <param name="classId">Point class (can be used by feature classifiers or object detectors)</param>
    public KeyPoint(float x, float y, float size, float angle = -1, float response = 0, int octave = 0,
        int classId = -1)
        : this(new Point2f(x, y), size, angle, response, octave, classId)
    {
    }
}
