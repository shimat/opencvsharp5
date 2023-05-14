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