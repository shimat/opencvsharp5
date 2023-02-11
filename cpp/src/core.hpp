#pragma once

#include <opencv2/core.hpp>
#include <opencv2/core/types_c.h>

CVAPI(int64) core_getTickCount()
{
    return cv::getTickCount();
}

CVAPI(cv::Mat*) core_Mat_new1(const int row, const int col, const int type)
{
    return new cv::Mat(row, col, type);
}

CVAPI(void) core_Mat_delete(const cv::Mat *obj)
{
    delete obj;
}
