#pragma once

#include "common.hpp"

// ReSharper disable CppNonInlineFunctionDefinitionInHeaderFile
// ReSharper disable CppInconsistentNaming

CVAPI(ExceptionStatus) core_Mat_new1(cv::Mat **result)
{
    BEGIN_WRAP
    *result = new cv::Mat;
    END_WRAP
}

CVAPI(ExceptionStatus) core_Mat_new2(const int row, const int col, const int type, cv::Mat** result)
{
    BEGIN_WRAP
    * result = new cv::Mat(row, col, type);
    END_WRAP
}

CVAPI(ExceptionStatus) core_Mat_new3(const int row, const int col, const int type, const cv::Scalar s, cv::Mat** result)
{
    BEGIN_WRAP
    * result = new cv::Mat(row, col, type, s);
    END_WRAP
}

CVAPI(ExceptionStatus) core_Mat_delete(const cv::Mat* obj)
{
    BEGIN_WRAP
    delete obj;
    END_WRAP
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
