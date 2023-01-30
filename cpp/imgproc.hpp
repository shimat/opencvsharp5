
#include <opencv2/opencv.hpp>
#include <opencv2/imgproc/imgproc_c.h>

CVAPI(CvRect) imgproc_cvMaxRect(const CvRect *rect1, const CvRect *rect2)
{
    return cvMaxRect(rect1, rect2);
}
