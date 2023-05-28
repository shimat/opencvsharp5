#pragma once

#include "common.hpp"
#include <opencv2/imgcodecs.hpp>


CVAPI(ExceptionStatus) imgcodecs_imread(const char* filename, int flags, cv::Mat** returnValue)
{
    BEGIN_WRAP;
    const auto ret = cv::imread(filename, flags);
    *returnValue = new cv::Mat(ret);
    END_WRAP;
}

CVAPI(ExceptionStatus) imgcodecs_imreadmulti1(
    const char* filename, 
    std::vector<cv::Mat>* mats, 
    int flags, 
    int* returnValue)
{
    BEGIN_WRAP;
    *returnValue = cv::imreadmulti(filename, *mats, flags) ? 1 : 0;
    END_WRAP;
}

CVAPI(ExceptionStatus) imgcodecs_imreadmulti2(
    const char* filename,
    std::vector<cv::Mat>* mats,
    int start,
    int count,
    int flags,
    int* returnValue)
{
    BEGIN_WRAP;
    *returnValue = cv::imreadmulti(filename, *mats, start, count, flags) ? 1 : 0;
    END_WRAP;
}

CVAPI(ExceptionStatus) imgcodecs_imcount(
    const char* filename,
    int flags,
    size_t* returnValue)
{
    BEGIN_WRAP;
    *returnValue = cv::imcount(filename, flags);
    END_WRAP;
}

CVAPI(ExceptionStatus) imgcodecs_imwrite(
    const char* filename,
    const cv::Mat* img, 
    int* params, int paramsLength, 
    int* returnValue)
{
    BEGIN_WRAP;
    std::vector<int> paramsVec;
    paramsVec.assign(params, params + paramsLength);
    *returnValue = cv::imwrite(filename, *img, paramsVec) ? 1 : 0;
    END_WRAP;
}

CVAPI(ExceptionStatus) imgcodecs_imwrite_multi(
    const char* filename,
    const std::vector<cv::Mat>* img, 
    int* params, int paramsLength, 
    int* returnValue)
{
    BEGIN_WRAP;
    std::vector<int> paramsVec;
    paramsVec.assign(params, params + paramsLength);
    *returnValue = cv::imwrite(filename, *img, paramsVec) ? 1 : 0;
    END_WRAP;
}

CVAPI(ExceptionStatus) imgcodecs_imdecode_Mat(
	const cv::Mat* buf, 
    int flags, 
    cv::Mat** returnValue)
{
    BEGIN_WRAP;
    const auto ret = cv::imdecode(*buf, flags);
    *returnValue = new cv::Mat(ret);
    END_WRAP;
}
CVAPI(ExceptionStatus) imgcodecs_imdecode_vector(
    uchar* buf, 
    int bufLength, 
    int flags, cv::Mat** returnValue)
{
    BEGIN_WRAP;
    //const std::vector<uchar> bufVec(buf, buf + bufLength);
    const cv::Mat bufMat(1, bufLength, CV_8UC1, buf, cv::Mat::AUTO_STEP);
    const auto ret = cv::imdecode(bufMat, flags);
    *returnValue = new cv::Mat(ret);
    END_WRAP;
}
CVAPI(ExceptionStatus) imgcodecs_imdecode_InputArray(
	const cv::_InputArray* buf, 
    int flags, 
    cv::Mat** returnValue)
{
    BEGIN_WRAP;
    const auto ret = cv::imdecode(*buf, flags);
    *returnValue = new cv::Mat(ret);
    END_WRAP;
}

CVAPI(ExceptionStatus) imgcodecs_imencode_vector(
    const char* ext, const cv::_InputArray* img,
    std::vector<uchar>* buf, int* params, int paramsLength, 
    int* returnValue)
{
    BEGIN_WRAP;
    std::vector<int> paramsVec;
    if (params != nullptr)
        paramsVec = std::vector<int>(params, params + paramsLength);
    *returnValue = cv::imencode(ext, *img, *buf, paramsVec) ? 1 : 0;
    END_WRAP;
}



CVAPI(ExceptionStatus) imgcodecs_haveImageReader(const char* filename, int* returnValue)
{
    BEGIN_WRAP;
    *returnValue = cv::haveImageReader(filename) ? 1 : 0;
    END_WRAP;
}

CVAPI(ExceptionStatus) imgcodecs_haveImageWriter(const char* filename, int* returnValue)
{
    BEGIN_WRAP;
    *returnValue = cv::haveImageWriter(filename) ? 1 : 0;
    END_WRAP;
}