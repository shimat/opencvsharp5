#pragma once

#include "common.hpp"

// ReSharper disable CppNonInlineFunctionDefinitionInHeaderFile
// ReSharper disable CppInconsistentNaming

CVAPI(cv::ErrorCallback) core_redirectError(cv::ErrorCallback errCallback, void* userData, void** prevUserData)
{
    return cv::redirectError(errCallback, userData, prevUserData);
}

CVAPI(ExceptionStatus) core_getTickCount(int64 *result)
{
    BEGIN_WRAP;
    *result = cv::getTickCount();
    END_WRAP;
}

CVAPI(ExceptionStatus) core_getBuildInformation(std::string *buffer)
{
    BEGIN_WRAP;
    buffer->assign(cv::getBuildInformation());
    END_WRAP;
}


CVAPI(ExceptionStatus) core_getVersionString(std::string *buffer)
{
    BEGIN_WRAP;
    buffer->assign(cv::getVersionString());
    END_WRAP;
}




CVAPI(ExceptionStatus) core_compare(
    cv::_InputArray *src1, cv::_InputArray* src2, cv::_OutputArray* dst, int cmpop)
{
    BEGIN_WRAP;
    cv::compare(*src1, *src2, *dst, cmpop);
    END_WRAP;
}

CVAPI(ExceptionStatus) core_countNonZero(cv::_InputArray* src, int* result)
{
    BEGIN_WRAP;
    *result = cv::countNonZero(*src);
    END_WRAP;
}

CVAPI(ExceptionStatus) core_split(
    cv::Mat* src, cv::Mat** mvbegin)
{
    BEGIN_WRAP;
    cv::split(*src, *mvbegin);
    END_WRAP;
}