#pragma once

#include "common.hpp"

// ReSharper disable CppNonInlineFunctionDefinitionInHeaderFile
// ReSharper disable CppInconsistentNaming

CVAPI(ExceptionStatus) core_Mat_new1(cv::Mat** result)
{
    BEGIN_WRAP;
    *result = new cv::Mat;
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_new2(const int row, const int col, const int type, cv::Mat** result)
{
    BEGIN_WRAP;
    *result = new cv::Mat(row, col, type);
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_new3(const int row, const int col, const int type, const CvScalar s, cv::Mat** result)
{
    BEGIN_WRAP;
    *result = new cv::Mat(row, col, type, s);
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_delete(const cv::Mat* obj)
{
    BEGIN_WRAP;
    delete obj;
    END_WRAP;
}

CVAPI(int) core_Mat_flags(const cv::Mat* obj)
{
    return obj->flags;
}

CVAPI(int) core_Mat_dims(const cv::Mat* obj)
{
    return obj->dims;
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


CVAPI(CvSize) core_Mat_size(const cv::Mat *obj)
{
    return cvSize(obj->size());
}

CVAPI(ExceptionStatus) core_Mat_sizeAt(const cv::Mat *obj, const int i, int *result)
{
    BEGIN_WRAP;
    *result = obj->size[i];
    END_WRAP;
}

CVAPI(size_t) core_Mat_step(const cv::Mat* obj)
{
    return obj->step;
}

CVAPI(ExceptionStatus) core_Mat_stepAt(const cv::Mat *obj, const int i, size_t *result)
{
    BEGIN_WRAP;
    *result = obj->step[i];
    END_WRAP;
}