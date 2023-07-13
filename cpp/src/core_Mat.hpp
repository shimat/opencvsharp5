#pragma once

#include "common.hpp"

// ReSharper disable CppNonInlineFunctionDefinitionInHeaderFile
// ReSharper disable CppInconsistentNaming

#pragma region Init & Disposal

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

CVAPI(ExceptionStatus) core_Mat_new4(int ndims, int* sizes, int type, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(ndims, sizes, type);
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_new5(int ndims, int* sizes, int type, CvScalar s, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(ndims, sizes, type, static_cast<cv::Scalar>(s));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_new6(cv::Mat *mat, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(*mat);
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_new7(int rows, int cols, int type, void* data, size_t step, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(rows, cols, type, data, step);
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_new8(int ndims, const int* sizes, int type, void* data, const size_t* steps, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(ndims, sizes, type, data, steps);
    END_WRAP;
}

inline cv::Range cpp(CvSlice s)
{
    return {s.start_index, s.end_index};
}

CVAPI(ExceptionStatus) core_Mat_new9(cv::Mat *mat, CvSlice rowRange, CvSlice colRange, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(*mat, cpp(rowRange), cpp(colRange));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_new10(cv::Mat *mat, CvRect roi, cv::Mat **returnValue)
{
    BEGIN_WRAP
    *returnValue = new cv::Mat(*mat, static_cast<cv::Rect>(roi));
    END_WRAP
}

CVAPI(ExceptionStatus) core_Mat_new11(cv::Mat *mat, cv::Range *ranges, cv::Mat **returnValue)
{
    BEGIN_WRAP
    *returnValue = new cv::Mat(*mat, ranges);
    END_WRAP
}

CVAPI(ExceptionStatus) core_Mat_delete(const cv::Mat* obj)
{
    BEGIN_WRAP;
    delete obj;
    END_WRAP;
}

#pragma endregion

#pragma region Fields

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

#pragma endregion

#pragma region Methods

CVAPI(ExceptionStatus) core_Mat_row(const cv::Mat *obj, int y, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(obj->row(y));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_col(const cv::Mat *obj, int x, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(obj->col(x));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_rowRange1(const cv::Mat *obj, int startrow, int endrow, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(obj->rowRange(startrow, endrow));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_rowRange2(const cv::Mat *obj, cv::Range r, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(obj->rowRange(r));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_colRange1(const cv::Mat *obj, int startcol, int endcol, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(obj->colRange(startcol, endcol));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_colRange2(const cv::Mat *obj, cv::Range r, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(obj->colRange(r));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_type(const cv::Mat* obj, int* result)
{
    BEGIN_WRAP;
    *result = obj->type();
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_depth(const cv::Mat* obj, int* result)
{
    BEGIN_WRAP;
    *result = obj->depth();
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_channels(const cv::Mat* obj, int* result)
{
    BEGIN_WRAP;
    *result = obj->channels();
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_empty(const cv::Mat* obj, int *result)
{
    BEGIN_WRAP;
    *result = obj->empty() ? 1 : 0;
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_total(const cv::Mat* obj, size_t* result)
{
    BEGIN_WRAP;
    *result = obj->total();
    END_WRAP;
}

#pragma endregion