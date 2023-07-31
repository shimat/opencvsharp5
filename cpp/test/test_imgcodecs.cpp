#include <gtest/gtest.h>
#include <filesystem>
#include "../src/imgcodecs.hpp"

TEST(ImgCodecsTest, imwrite) {
    const cv::Mat img(10, 10, CV_8UC3, cv::Scalar(0, 0, 255));

    const auto path = "imgcodecs_imwrite.png";

    int ret;
    ASSERT_EQ(
        imgcodecs_imwrite(path, &img, nullptr, 0, &ret),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(ret, 1);
    ASSERT_TRUE(std::filesystem::exists(path));

    ASSERT_EQ(
        imgcodecs_haveImageWriter(path, &ret),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(ret, 1);
    ASSERT_EQ(
        imgcodecs_haveImageReader(path, &ret),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(ret, 1);
}

TEST(ImgCodecsTest, imwriteJapanese) {
#if !_WIN32
    const cv::Mat img(10, 10, CV_8UC3, cv::Scalar(255, 0, 0));
    
    const auto path = "imgcodecs_imwrite_にほんご.png";

    int ret;
    ASSERT_EQ(
        imgcodecs_imwrite(path, &img, nullptr, 0, &ret),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(ret, 1);
    ASSERT_TRUE(std::filesystem::exists(path));

    ASSERT_EQ(
        imgcodecs_haveImageWriter(path, &ret),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(ret, 1);
    ASSERT_EQ(
        imgcodecs_haveImageReader(path, &ret),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(ret, 1);
#endif
}
