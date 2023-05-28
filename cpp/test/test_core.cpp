#include <gtest/gtest.h>
#include "../src/core.hpp"

TEST(test_core, getTickCount) {
    int64 value = 0;
    ASSERT_EQ(
        core_getTickCount(&value), 
        ExceptionStatus::NotOccurred);

    ASSERT_NE(value, 0);
}

TEST(test_core, getBuildInformation) {
    std::string buf;
    ASSERT_EQ(
        core_getBuildInformation(&buf),
        ExceptionStatus::NotOccurred);

    ASSERT_FALSE(buf.empty());
}

TEST(test_core, getVersionString) {
    std::string buf;
    ASSERT_EQ(
        core_getVersionString(&buf),
        ExceptionStatus::NotOccurred);

    ASSERT_FALSE(buf.empty());
}



TEST(test_core, compare) {
    cv::Mat src1(1, 1, CV_8U, cv::Scalar::all(0));
    cv::Mat src2(1, 1, CV_8U, cv::Scalar::all(1));
    cv::Mat dst;
    cv::_InputArray src1_(src1);
    cv::_InputArray src2_(src2);
    cv::_OutputArray dst_(dst);

    ASSERT_EQ(
        core_compare(&src1_, &src2_, &dst_, cv::CMP_EQ),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(dst.size(), cv::Size(1, 1));
    ASSERT_EQ(dst.at<uchar>(0), 0);

    ASSERT_EQ(
        core_compare(&src1_, &src2_, &dst_, cv::CMP_NE),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(dst.at<uchar>(0), 255);
}

TEST(test_core, countNonZero) {
    int pixels;

    cv::Mat m = cv::Mat::zeros(10, 10, CV_8U);
    cv::_InputArray ia1(m);
    ASSERT_EQ(
        core_countNonZero(&ia1, &pixels),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(pixels, 0);

    m.at<uchar>(1, 1) = 1;
    m.at<uchar>(2, 3) = 2;
    m.at<uchar>(3, 3) = 3;
    cv::_InputArray ia2(m);
    ASSERT_EQ(
        core_countNonZero(&ia2, &pixels),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(pixels, 3);
}
