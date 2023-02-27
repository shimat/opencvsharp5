#pragma once

#include "common.hpp"

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
