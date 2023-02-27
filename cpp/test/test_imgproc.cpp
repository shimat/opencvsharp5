#include <gtest/gtest.h>
#include "../src/imgproc.hpp"

TEST(test_imgproc, rectangle1) {
    cv::Mat img(3, 3, CV_8UC3, cv::Scalar::all(0));
    cv::Scalar color(255, 128, 64);

    cv::_InputOutputArray ioa = img;
    ASSERT_EQ(
        imgproc_rectangle1(&ioa, cv::Point(1, 1), cv::Point(2, 2), &color, -1, cv::LINE_8, 0), 
        ExceptionStatus::NotOccurred);

    ASSERT_EQ(img.at<cv::Vec3b>(0, 0), cv::Vec3b(0, 0, 0));
    ASSERT_EQ(img.at<cv::Vec3b>(1, 1), cv::Vec3b(255, 128, 64));
    ASSERT_EQ(img.at<cv::Vec3b>(2, 2), cv::Vec3b(255, 128, 64));
}
