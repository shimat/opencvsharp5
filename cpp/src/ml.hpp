
#include <opencv2/opencv.hpp>
#include <opencv2/core/types_c.h>

CVAPI(void*) ml_ParamGrid_create()
{
    return cv::ml::ParamGrid::create();
}
