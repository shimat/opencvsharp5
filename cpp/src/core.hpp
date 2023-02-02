#pragma once

#include <opencv2/core.hpp>
#include <opencv2/core/types_c.h>

CVAPI(int64) core_getTickCount()
{
    return cv::getTickCount();
}
