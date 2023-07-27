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

CVAPI(ExceptionStatus) core_Mat_new4(const int ndims, int* sizes, const int type, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(ndims, sizes, type);
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_new5(const int ndims, int* sizes, const int type, CvScalar s, cv::Mat **returnValue)
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

CVAPI(ExceptionStatus) core_Mat_new7(const int rows, const int cols, const int type, void* data, const size_t step, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(rows, cols, type, data, step);
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_new8(const int ndims, const int* sizes, const int type, void* data, const size_t* steps, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(ndims, sizes, type, data, steps);
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_new9(cv::Mat *mat, const CvSlice rowRange, const CvSlice colRange, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(*mat, cpp(rowRange), cpp(colRange));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_new10(cv::Mat *mat, const CvRect roi, cv::Mat **returnValue)
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

CVAPI(ExceptionStatus) core_Mat_row(const cv::Mat *obj, const int y, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(obj->row(y));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_col(const cv::Mat *obj, const int x, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(obj->col(x));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_rowRange1(const cv::Mat *obj, const int startRow, const int endRow, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(obj->rowRange(startRow, endRow));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_rowRange2(const cv::Mat *obj, const cv::Range r, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(obj->rowRange(r));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_colRange1(const cv::Mat *obj, const int startCol, const int endCol, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(obj->colRange(startCol, endCol));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_colRange2(const cv::Mat *obj, const cv::Range r, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(obj->colRange(r));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_diag(const cv::Mat *obj, const int d, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(obj->diag(d));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_clone(const cv::Mat *obj, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(obj->clone());
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_copyTo1(const cv::Mat *obj, const cv::_OutputArray *m)
{
    BEGIN_WRAP;
    obj->copyTo(*m);
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_copyTo2(const cv::Mat *obj, const cv::_OutputArray *m, const cv::_InputArray *mask)
{
    BEGIN_WRAP;
    obj->copyTo(*m, *mask);
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_convertTo(const cv::Mat *obj, const cv::_OutputArray *m, const int rtype, const double alpha, const double beta)
{
    BEGIN_WRAP;
    obj->convertTo(*m, rtype, alpha, beta);
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_assignTo(const cv::Mat *obj, cv::Mat *m, const int type)
{
    BEGIN_WRAP;
    obj->assignTo(*m, type);
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_setTo1(cv::Mat *obj, const cv::_InputArray *value, cv::_InputArray *mask)
{
    BEGIN_WRAP;
    obj->setTo(*value, entity(mask));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_setTo2(cv::Mat *obj, const CvScalar value, cv::_InputArray *mask)
{
    BEGIN_WRAP;
    obj->setTo(static_cast<cv::Scalar>(value), entity(mask));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_setZero(cv::Mat *obj)
{
    BEGIN_WRAP;
    obj->setZero();
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_reshape1(const cv::Mat *obj, const int cn, const int rows, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(obj->reshape(cn, rows));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_reshape2(const cv::Mat *obj, const int cn, const int newndims, const int* newsz, cv::Mat **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::Mat(obj->reshape(cn, newndims, newsz));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_isContinuous(const cv::Mat* obj, int* result)
{
    BEGIN_WRAP;
    *result = (obj->isContinuous() ? 1 : 0);
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_isSubmatrix(const cv::Mat* obj, int* result)
{
    BEGIN_WRAP;
    *result = (obj->isSubmatrix() ? 1 : 0);
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_elemSize(const cv::Mat* obj, size_t* result)
{
    BEGIN_WRAP;
    *result = obj->elemSize();
    END_WRAP;
}

CVAPI(ExceptionStatus) core_Mat_elemSize1(const cv::Mat* obj, size_t* result)
{
    BEGIN_WRAP;
    *result = obj->elemSize1();
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

CVAPI(ExceptionStatus) core_Mat_checkVector(
    const cv::Mat* obj, const int elemChannels, const int depth, const int requireContinuous, int *result)
{
    BEGIN_WRAP;
    *result = obj->checkVector(elemChannels, depth, requireContinuous != 0);
    END_WRAP;
}

CVAPI(uchar*) core_Mat_ptr1(cv::Mat* obj, const int i0)
{
    return obj->ptr(i0);
}

CVAPI(uchar*) core_Mat_ptr2(cv::Mat* obj, const int row, const int col)
{
    return obj->ptr(row, col);
}

CVAPI(uchar*) core_Mat_ptr3(cv::Mat* obj, const int i0, const int i1, const int i2)
{
    return obj->ptr(i0, i1, i2);
}

CVAPI(uchar*) core_Mat_ptrNd(cv::Mat* obj, const int* idx)
{
    return obj->ptr(idx);
}

#pragma endregion