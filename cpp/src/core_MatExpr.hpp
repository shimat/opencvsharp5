#pragma once

#include "common.hpp"

// ReSharper disable CppNonInlineFunctionDefinitionInHeaderFile
// ReSharper disable CppInconsistentNaming

#pragma region Init & Disposal

CVAPI(ExceptionStatus) core_MatExpr_new1(cv::MatExpr** result)
{
    BEGIN_WRAP;
    *result = new cv::MatExpr;
    END_WRAP;
}

CVAPI(ExceptionStatus) core_MatExpr_new2(cv::Mat *m, cv::MatExpr** result)
{
    BEGIN_WRAP;
    *result = new cv::MatExpr(*m);
    END_WRAP;
}

CVAPI(ExceptionStatus) core_MatExpr_delete(const cv::MatExpr* obj)
{
    BEGIN_WRAP;
    delete obj;
    END_WRAP;
}

#pragma endregion

#pragma region Methods

CVAPI(ExceptionStatus) core_MatExpr_toMat(const cv::MatExpr *obj, cv::Mat **result)
{
    BEGIN_WRAP;
    *result = new cv::Mat(*obj);
    END_WRAP;
}

CVAPI(ExceptionStatus) core_MatExpr_size(const cv::MatExpr *obj, CvSize *result)
{
    BEGIN_WRAP;
    *result = cvSize(obj->size());
    END_WRAP;
}

CVAPI(ExceptionStatus) core_MatExpr_type(const cv::MatExpr* obj, int* result)
{
    BEGIN_WRAP;
    *result = obj->type();
    END_WRAP;
}

CVAPI(ExceptionStatus) core_MatExpr_row(const cv::MatExpr *obj, const int y, cv::MatExpr **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::MatExpr(obj->row(y));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_MatExpr_col(const cv::MatExpr *obj, const int x, cv::MatExpr **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::MatExpr(obj->col(x));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_MatExpr_diag(const cv::MatExpr *obj, const int d, cv::MatExpr **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::MatExpr(obj->diag(d));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_MatExpr_cropByRange(
    const cv::MatExpr *obj, const CvSlice rowRange, const CvSlice colRange, cv::MatExpr **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::MatExpr((*obj)(cpp(rowRange), cpp(colRange)));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_MatExpr_cropByRect(
    const cv::MatExpr *obj, const CvRect roi, cv::MatExpr **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::MatExpr((*obj)(cpp(roi)));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_MatExpr_t(const cv::MatExpr *obj, cv::MatExpr **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::MatExpr(obj->t());
    END_WRAP;
}

CVAPI(ExceptionStatus) core_MatExpr_inv(const cv::MatExpr *obj, const int method, cv::MatExpr **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::MatExpr(obj->inv(method));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_MatExpr_mul1(const cv::MatExpr *obj, const cv::MatExpr *e, const double scale, cv::MatExpr **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::MatExpr(obj->mul(*e, scale));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_MatExpr_mul2(const cv::MatExpr *obj, const cv::Mat *m, const double scale, cv::MatExpr **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::MatExpr(obj->mul(*m, scale));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_MatExpr_cross(const cv::MatExpr *obj, const cv::Mat *m, cv::MatExpr **returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::MatExpr(obj->cross(*m));
    END_WRAP;
}

CVAPI(ExceptionStatus) core_MatExpr_dot(const cv::MatExpr *obj, const cv::Mat *m, double *returnValue)
{
    BEGIN_WRAP;
    *returnValue = obj->dot(*m);
    END_WRAP;
}

#pragma endregion