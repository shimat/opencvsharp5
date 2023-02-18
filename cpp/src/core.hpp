﻿#pragma once

#include <opencv2/core.hpp>
#include <opencv2/core/types_c.h>

CVAPI(int64) core_getTickCount()
{
    return cv::getTickCount();
}

CVAPI(void) core_getBuildInformation(std::string *buffer)
{
    buffer->assign(cv::getBuildInformation());
}


CVAPI(std::string*) std_string_new1()
{
    return new std::string;
}

CVAPI(std::string*) std_string_new2(const char *str)
{
    return new std::string(str);
}

CVAPI(void) std_string_new3(std::string **obj)
{
    *obj = new std::string;
}

CVAPI(void) std_string_delete(const std::string *obj)
{
    delete obj;
}

CVAPI(const char*) std_string_c_str(std::string *obj)
{
    return obj->c_str();
}

CVAPI(cv::Mat*) core_Mat_new1()
{
    return new cv::Mat;
}

CVAPI(cv::Mat*) core_Mat_new2(const int row, const int col, const int type)
{
    return new cv::Mat(row, col, type);
}

CVAPI(void) core_Mat_delete(const cv::Mat *obj)
{
    delete obj;
}
