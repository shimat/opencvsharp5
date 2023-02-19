#include <gtest/gtest.h>
#include "../src/core_Mat.hpp"

TEST(test_core_Mat_newdelete_1, success) {
    auto deleter = [](const cv::Mat *obj) {
        core_Mat_delete(obj);
    };
    const std::unique_ptr<cv::Mat, decltype(deleter)> obj(
        core_Mat_new1());

    ASSERT_EQ(obj->empty(), true);
}

TEST(test_core_Mat_newdelete_2, success) {
    auto deleter = [](const cv::Mat *obj) {
        core_Mat_delete(obj);
    };
    const std::unique_ptr<cv::Mat, decltype(deleter)> obj(
        core_Mat_new2(3, 4, CV_8UC1));

    ASSERT_EQ(obj->rows, 3);
    ASSERT_EQ(obj->cols, 4);
    ASSERT_EQ(obj->type(), CV_8UC1);
}

TEST(test_core_Mat_newdelete_3, success) {
    auto deleter = [](const cv::Mat *obj) {
        core_Mat_delete(obj);
    };
    const std::unique_ptr<cv::Mat, decltype(deleter)> obj(
        core_Mat_new3(3, 4, CV_8UC4, cv::Scalar(1, 2, 3, 4)));
    
    ASSERT_EQ(obj->rows, 3);
    ASSERT_EQ(obj->cols, 4);
    ASSERT_EQ(obj->type(), CV_8UC4);
    ASSERT_EQ(obj->at<cv::Vec4b>(0, 0), cv::Vec4b(1, 2, 3, 4));
}

TEST(test_core_Mat_rowscols, success) {
    const cv::Mat m(3, 4, CV_8UC1);

    ASSERT_EQ(core_Mat_rows(&m), 3);
    ASSERT_EQ(core_Mat_cols(&m), 4);
}
