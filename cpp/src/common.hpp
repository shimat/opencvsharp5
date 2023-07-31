#pragma once

#if defined WIN32 || defined _WIN32
#pragma warning(push)
#include <codeanalysis/warnings.h>
#pragma warning(disable: ALL_CODE_ANALYSIS_WARNINGS)
#endif

#include <opencv2/core.hpp>
#include <opencv2/core/types_c.h>

#if defined WIN32 || defined _WIN32
#pragma warning(pop)
#endif


enum class ExceptionStatus : int { NotOccurred = 0, Occurred = 1 };

#if defined WIN32 || defined _WIN32
#define BEGIN_WRAP
#define END_WRAP return ExceptionStatus::NotOccurred;
#else
#define BEGIN_WRAP try{
#define END_WRAP return ExceptionStatus::NotOccurred;}catch(std::exception){return ExceptionStatus::Occurred;}
#endif


static cv::_InputArray entity(cv::_InputArray *obj)
{
    return (obj != nullptr) ? *obj : static_cast<cv::_InputArray>(cv::noArray());
}
static cv::_OutputArray entity(cv::_OutputArray *obj)
{
    return (obj != nullptr) ? *obj : static_cast<cv::_OutputArray>(cv::noArray());
}
static cv::_InputOutputArray entity(cv::_InputOutputArray *obj)
{
    return (obj != nullptr) ? *obj : cv::noArray();
}
static cv::Mat entity(cv::Mat *obj)
{
    return (obj != nullptr) ? *obj : cv::Mat();
}
static cv::UMat entity(cv::UMat* obj)
{
    return (obj != nullptr) ? *obj : cv::UMat();
}
static cv::SparseMat entity(cv::SparseMat *obj)
{
    return (obj != nullptr) ? *obj : cv::SparseMat();
}

inline cv::Range cpp(CvSlice s)
{
    return {s.start_index, s.end_index};
}
