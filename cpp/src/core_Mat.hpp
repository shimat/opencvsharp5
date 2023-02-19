#pragma once

#include <opencv2/core.hpp>
#include <opencv2/core/types_c.h>

// ReSharper disable CppNonInlineFunctionDefinitionInHeaderFile
// ReSharper disable CppInconsistentNaming

CVAPI(cv::Mat*) core_Mat_new1()
{
    return new cv::Mat;
}

CVAPI(cv::Mat*) core_Mat_new2(const int row, const int col, const int type)
{
    return new cv::Mat(row, col, type);
}

CVAPI(cv::Mat*) core_Mat_new3(const int row, const int col, const int type, const cv::Scalar s)
{
    return new cv::Mat(row, col, type, s);
}

CVAPI(void) core_Mat_delete(const cv::Mat* obj)
{
    delete obj;
}

CVAPI(int) core_Mat_rows(const cv::Mat* obj)
{
    return obj->rows;
}

CVAPI(int) core_Mat_cols(const cv::Mat* obj)
{
    return obj->cols;
}

CVAPI(uchar*) core_Mat_data(const cv::Mat* obj)
{
    return obj->data;
}
