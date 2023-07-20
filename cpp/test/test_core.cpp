#include <gtest/gtest.h>
#include "../src/core.hpp"

TEST(CoreTest, getTickCount) {
    int64 value = 0;
    ASSERT_EQ(
        core_getTickCount(&value), 
        ExceptionStatus::NotOccurred);

    ASSERT_NE(value, 0);
}

TEST(CoreTest, getBuildInformation) {
    std::string buf;
    ASSERT_EQ(
        core_getBuildInformation(&buf),
        ExceptionStatus::NotOccurred);

    ASSERT_FALSE(buf.empty());
}

TEST(CoreTest, getVersionString) {
    std::string buf;
    ASSERT_EQ(
        core_getVersionString(&buf),
        ExceptionStatus::NotOccurred);

    ASSERT_FALSE(buf.empty());
}

TEST(CoreTest, format) {
    const cv::Mat mat = cv::Mat::eye(2, 3, CV_8UC1);
    const cv::_InputArray src(mat);
    std::string buf;
    ASSERT_EQ(
        core_format(&src, cv::Formatter::FormatType::FMT_DEFAULT, &buf),
        ExceptionStatus::NotOccurred);

    ASSERT_FALSE(buf.empty());
}

TEST(CoreTest, compare) {
    cv::Mat src1(12, 12, CV_32SC1, cv::Scalar::all(0));
    cv::Mat src2(12, 12, CV_32SC1, cv::Scalar::all(1));
    cv::Mat dst;
    cv::_InputArray src1_(src1);
    cv::_InputArray src2_(src2);
    cv::_OutputArray dst_(dst);

    ASSERT_EQ(
        core_compare(&src1_, &src2_, &dst_, cv::CMP_EQ),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(dst.type(), CV_8UC1);
    ASSERT_EQ(dst.size(), src1.size());
    ASSERT_EQ(dst.at<uchar>(0), 0);

    ASSERT_EQ(
        core_compare(&src1_, &src2_, &dst_, cv::CMP_NE),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(dst.at<uchar>(0), 255);
}

TEST(CoreTest, countNonZero)
{
#ifdef _WIN32 // hangs on Ubuntu???
    int pixels;

    cv::Mat m = cv::Mat::zeros(10, 10, CV_8U);
    const cv::_InputArray ia1(m);
    ASSERT_EQ(
        core_countNonZero(&ia1, &pixels),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(pixels, 0);

    m.at<uchar>(1, 1) = 1;
    m.at<uchar>(2, 3) = 2;
    m.at<uchar>(3, 3) = 3;
    const cv::_InputArray ia2(m);
    ASSERT_EQ(
        core_countNonZero(&ia2, &pixels),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(pixels, 3);
#endif
}

TEST(CoreTest, split1)
{
    const cv::Mat src(1, 1, CV_32SC3, cv::Scalar(1, 2, 3));
    std::vector<cv::Mat> dst;
    ASSERT_EQ(
        core_split(&src, &dst),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(dst[0].at<int>(0), 1);
    ASSERT_EQ(dst[1].at<int>(0), 2);
    ASSERT_EQ(dst[2].at<int>(0), 3);
}
