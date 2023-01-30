
#include <opencv2/opencv.hpp>
#include <opencv2/core/types_c.h>

CVAPI(int64) core_getTickCount()
{
    return cv::getTickCount();
}
