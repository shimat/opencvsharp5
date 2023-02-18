#include <gtest/gtest.h>
#include "../src/core.hpp"
#include "../src/core_Mat.hpp"

TEST(test_core_getTickCount, success) {
    core_getTickCount();
}

TEST(test_core_Mat_newdelete_1, success) {
    auto deleter = [](const cv::Mat *obj) {
        core_Mat_delete(obj);
    };
    std::unique_ptr<cv::Mat, decltype(deleter)> obj(
        core_Mat_new1());
}

TEST(test_core_Mat_newdelete_2, success) {
    auto deleter = [](const cv::Mat *obj) {
        core_Mat_delete(obj);
    };
    std::unique_ptr<cv::Mat, decltype(deleter)> obj(
        core_Mat_new2(3, 4, CV_8UC1));
}

TEST(test_core_Mat_rowscols, success) {
    auto deleter = [](const cv::Mat* obj) {
        core_Mat_delete(obj);
    };
    std::unique_ptr<cv::Mat, decltype(deleter)> obj(
        core_Mat_new2(3, 4, CV_8UC1));

    ASSERT_EQ(core_Mat_rows(obj.get()), 3);
    ASSERT_EQ(core_Mat_cols(obj.get()), 4);
}
