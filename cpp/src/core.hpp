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
    BEGIN_WRAP
    *result = cv::getTickCount();
    END_WRAP
}

CVAPI(ExceptionStatus) core_getBuildInformation(std::string *buffer)
{
    BEGIN_WRAP
    buffer->assign(cv::getBuildInformation());
    END_WRAP
}
