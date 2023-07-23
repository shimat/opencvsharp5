#include <gtest/gtest.h>
#include "../src/core_MatExpr.hpp"

struct MatExprDeleter
{
    void operator()(const cv::MatExpr *obj) const
    {
        core_MatExpr_delete(obj);
    }
};

TEST(CoreTestMatExpr, newDelete1)
{
    cv::MatExpr* matExpr = nullptr;
    ASSERT_EQ(core_MatExpr_new1(&matExpr), ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::MatExpr, MatExprDeleter> obj(matExpr);
}

TEST(CoreTestMatExpr, newDelete2)
{
    cv::MatExpr* matExpr = nullptr;
    cv::Mat mat = cv::Mat::eye(2, 2, CV_8UC1);

    ASSERT_EQ(core_MatExpr_new2(&mat, &matExpr), ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::MatExpr, MatExprDeleter> obj(matExpr);
}


TEST(CoreTestMatExpr, size) {
    const auto matExpr = static_cast<cv::MatExpr>(cv::Mat(3, 4, CV_8UC1));
    CvSize size;

    ASSERT_EQ(core_MatExpr_size(&matExpr, &size), ExceptionStatus::NotOccurred);
    ASSERT_EQ(size.width, 4);
    ASSERT_EQ(size.height, 3);
}

TEST(CoreTestMatExpr, diag)
{
    const cv::MatExpr src = static_cast<cv::MatExpr>(cv::Mat_<uchar>(3, 3) <<
        1, 2, 3,
        4, 5, 6,
        7, 8, 9);
    cv::MatExpr *dst = nullptr;

    ASSERT_EQ(
        core_MatExpr_diag(&src, 0, &dst),
        ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::MatExpr, MatExprDeleter> obj(dst);
    ASSERT_EQ(dst->type(), CV_8UC1);
    ASSERT_EQ(dst->size(), cv::Size(1, 3));

    cv::Mat dstMat = *dst;    
    ASSERT_EQ(dstMat.at<uchar>(0), 1);
    ASSERT_EQ(dstMat.at<uchar>(1), 5);
    ASSERT_EQ(dstMat.at<uchar>(2), 9);
}

TEST(CoreTestMatExpr, type)
{
    const auto matExpr = static_cast<cv::MatExpr>(cv::Mat(3, 4, CV_16SC4));
    int type;

    ASSERT_EQ(core_MatExpr_type(&matExpr, &type), ExceptionStatus::NotOccurred);
    ASSERT_EQ(type, CV_16SC4);
}
