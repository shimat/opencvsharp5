#include <gtest/gtest.h>
#include <filesystem>
#include "../src/imgcodecs.hpp"

TEST(test_imgcodecs, imwrite) {
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

TEST(test_imgcodecs, imwrite_japanese) {
	const cv::Mat img(10, 10, CV_8UC3, cv::Scalar(255, 0, 0));

    const auto path = "haveImageReader_‚É‚Ù‚ñ‚²“ú–{Œê.png";//"imgcodecs_imwrite_‚É‚Ù‚ñ‚².png";

    std::vector<uchar> exp{ 104,97,118,101,73,109,97,103,101,82,101,97,100,101,114,95,227,129,171,227,129,187,227,130,147,227,129,148,230,151,165,230,156,172,232,170,158,46,112,110,103 };
    for (size_t i = 0; auto b : exp){
		ASSERT_EQ(b, path[i++]);
    }

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
