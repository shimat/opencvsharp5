#pragma once

#include "common.hpp"

#pragma region InputArray

CVAPI(ExceptionStatus) core_InputArray_new_byMat(cv::Mat* mat, cv::_InputArray** returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::_InputArray(*mat);
    END_WRAP;
}

CVAPI(ExceptionStatus) core_InputArray_new_byMatExpr(cv::MatExpr* matExpr, cv::_InputArray** returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::_InputArray(*matExpr);
    END_WRAP;
}

CVAPI(ExceptionStatus) core_InputArray_delete(const cv::_InputArray* obj)
{
    BEGIN_WRAP;
    delete obj;
    END_WRAP;
}

#pragma endregion

#pragma region OutputArray

CVAPI(ExceptionStatus) core_OutputArray_new_byMat(cv::Mat* mat, cv::_OutputArray** returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::_OutputArray(*mat);
    END_WRAP;
}

CVAPI(ExceptionStatus) core_OutputArray_delete(const cv::_OutputArray* obj)
{
    BEGIN_WRAP;
    delete obj;
    END_WRAP;
}

#pragma endregion

#pragma region InputOutputArray

CVAPI(ExceptionStatus) core_InputOutputArray_new_byMat(cv::Mat* mat, cv::_InputOutputArray** returnValue)
{
    BEGIN_WRAP;
    *returnValue = new cv::_InputOutputArray(*mat);
    END_WRAP;
}

CVAPI(ExceptionStatus) core_InputOutputArray_delete(const cv::_InputOutputArray* obj)
{
    BEGIN_WRAP;
    delete obj;
    END_WRAP;
}

#pragma endregion
