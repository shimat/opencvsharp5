#pragma once

#include "common.hpp"
#include <opencv2/imgproc.hpp>


CVAPI(ExceptionStatus) imgproc_rectangle1(
    cv::_InputOutputArray* img,
    cv::Point pt1,
    cv::Point pt2,
    cv::Scalar* color,
    int thickness,
    int lineType,
    int shift)
{
    BEGIN_WRAP;
    cv::rectangle(*img, pt1, pt2, *color, thickness, lineType, shift);
    END_WRAP;
}

CVAPI(ExceptionStatus) imgproc_rectangle2(
    cv::_InputOutputArray* img,
    const cv::Rect rec,
    const cv::Scalar* color,
    int thickness,
    int lineType, 
    int shift)
{
    BEGIN_WRAP;
    cv::rectangle(*img, rec, *color, thickness, lineType, shift);
    END_WRAP;
}