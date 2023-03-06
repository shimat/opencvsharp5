#include <gtest/gtest.h>
#include "../src/core_Mat.hpp"

TEST(test_core_Mat, newdelete_1) {
    auto deleter = [](const cv::Mat *obj) {
        core_Mat_delete(obj);
    };

    cv::Mat* mat = nullptr;
    ASSERT_EQ(core_Mat_new1(&mat), ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::Mat, decltype(deleter)> obj(mat);

    ASSERT_EQ(obj->empty(), true);
}

TEST(test_core_Mat, _newdelete_2) {
    auto deleter = [](const cv::Mat *obj) {
        core_Mat_delete(obj);
    };

    cv::Mat* mat = nullptr;
    ASSERT_EQ(core_Mat_new2(3, 4, CV_8UC1, &mat), ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::Mat, decltype(deleter)> obj(mat);

    ASSERT_EQ(obj->rows, 3);
    ASSERT_EQ(obj->cols, 4);
    ASSERT_EQ(obj->type(), CV_8UC1);
}

TEST(test_core_Mat, _newdelete_3) {
    auto deleter = [](const cv::Mat *obj) {
        core_Mat_delete(obj);
    };

    cv::Mat* mat = nullptr;
    ASSERT_EQ(core_Mat_new3(3, 4, CV_8UC4, cvScalar(1, 2, 3, 4), &mat), ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::Mat, decltype(deleter)> obj(mat);
    
    ASSERT_EQ(obj->rows, 3);
    ASSERT_EQ(obj->cols, 4);
    ASSERT_EQ(obj->type(), CV_8UC4);
    ASSERT_EQ(obj->at<cv::Vec4b>(0, 0), cv::Vec4b(1, 2, 3, 4));
}

TEST(test_core_Mat, size) {
    const cv::Mat m(3, 4, CV_8UC1);

    ASSERT_EQ(core_Mat_rows(&m), 3);
    ASSERT_EQ(core_Mat_cols(&m), 4);
    ASSERT_EQ(static_cast<cv::Size>(core_Mat_size(&m)), cv::Size(4, 3));
}

TEST(test_core_Mat, data) {
    const cv::Mat m(3, 4, CV_8UC1);
    
    ASSERT_NE(core_Mat_data(&m), nullptr);
}

TEST(test_core_Mat, step) {
    const cv::Mat m(3, 7, CV_8UC1);
    
    ASSERT_EQ(core_Mat_step(&m), 7);
}
