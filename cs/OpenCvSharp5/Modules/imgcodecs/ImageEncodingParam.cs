namespace OpenCvSharp5;

/// <summary>
/// The format-specific save parameters for cv::imwrite and cv::imencode
/// </summary>
/// <param name="EncodingId">format type ID</param>
/// <param name="Value">value of parameter</param>
public record ImageEncodingParam(ImwriteFlags EncodingId, int Value);
