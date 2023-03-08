#include <gtest/gtest.h>
#include "../src/core_Mat.hpp"
#include <array>

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
    ASSERT_EQ(obj->at<cv::Vec4b>(2, 3), cv::Vec4b(1, 2, 3, 4));
}

TEST(test_core_Mat, _newdelete_4) {
    auto deleter = [](const cv::Mat *obj) {
        core_Mat_delete(obj);
    };
    
    cv::Mat* mat = nullptr;
    int sizes[3] = {2, 3, 4};
	ASSERT_EQ(core_Mat_new4(3, sizes, CV_8UC1, &mat), ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::Mat, decltype(deleter)> obj(mat);
    
    ASSERT_EQ(obj->rows, -1);
    ASSERT_EQ(obj->cols, -1);
    ASSERT_EQ(obj->dims, 3);
    ASSERT_EQ(obj->size[0], 2);
    ASSERT_EQ(obj->size[1], 3);
    ASSERT_EQ(obj->size[2], 4);
    ASSERT_EQ(obj->type(), CV_8UC1);
}

TEST(test_core_Mat, _newdelete_5) {
    auto deleter = [](const cv::Mat *obj) {
        core_Mat_delete(obj);
    };
    
    cv::Mat* mat = nullptr;
    int sizes[3] = {2, 3, 4};
	ASSERT_EQ(core_Mat_new5(3, sizes, CV_32SC4, cvScalar(1111, 2222, 3333, 4444), &mat), ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::Mat, decltype(deleter)> obj(mat);
    
    ASSERT_EQ(obj->rows, -1);
    ASSERT_EQ(obj->cols, -1);
    ASSERT_EQ(obj->dims, 3);
    ASSERT_EQ(obj->size[0], 2);
    ASSERT_EQ(obj->size[1], 3);
    ASSERT_EQ(obj->size[2], 4);
    ASSERT_EQ(obj->type(), CV_32SC4);
    ASSERT_EQ(obj->at<cv::Vec4i>(0, 0, 0), cv::Vec4i(1111, 2222, 3333, 4444));
    ASSERT_EQ(obj->at<cv::Vec4i>(1, 2, 3), cv::Vec4i(1111, 2222, 3333, 4444));
}

TEST(test_core_Mat, _newdelete_6) {
    auto deleter = [](const cv::Mat *obj) {
        core_Mat_delete(obj);
    };

    cv::Mat org(3, 4, CV_32FC1, cv::Scalar(3.14));
    cv::Mat* mat = nullptr;
	ASSERT_EQ(core_Mat_new6(&org, &mat), ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::Mat, decltype(deleter)> obj(mat);
    
    ASSERT_EQ(obj->rows, 3);
    ASSERT_EQ(obj->cols, 4);
    ASSERT_EQ(obj->type(), CV_32FC1);
    ASSERT_EQ(obj->data, org.data);
}

TEST(test_core_Mat, _newdelete_7) {
    auto deleter = [](const cv::Mat *obj) {
        core_Mat_delete(obj);
    };
    
    cv::Mat* mat = nullptr;
    std::array<uchar, 200> data{};
    data.fill(255);

	ASSERT_EQ(core_Mat_new7(2, 3, CV_8UC1, data.data(), 100, &mat), ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::Mat, decltype(deleter)> obj(mat);
    
    ASSERT_EQ(obj->rows, 2);
    ASSERT_EQ(obj->cols, 3);
    ASSERT_EQ(obj->type(), CV_8UC1);
    ASSERT_EQ(obj->step, 100);
    ASSERT_EQ(obj->at<uchar>(0, 0), 255);
    ASSERT_EQ(obj->at<uchar>(1, 2), 255);
}

TEST(test_core_Mat, _newdelete_8) {
    auto deleter = [](const cv::Mat *obj) {
        core_Mat_delete(obj);
    };
    
    cv::Mat* mat = nullptr;
    std::array<uchar, 1000> data{};
    data.fill(128);
    const int sizes[3] = {2, 3, 4};
    const size_t steps[2] = {10, 20};

	ASSERT_EQ(core_Mat_new8(3, sizes, CV_8UC1, data.data(), steps, &mat), ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::Mat, decltype(deleter)> obj(mat);
    
    ASSERT_EQ(obj->rows, -1);
    ASSERT_EQ(obj->cols, -1);
    ASSERT_EQ(obj->dims, 3);
    ASSERT_EQ(obj->size[0], 2);
    ASSERT_EQ(obj->size[1], 3);
    ASSERT_EQ(obj->size[2], 4);
    ASSERT_EQ(obj->type(), CV_8UC1);
    ASSERT_EQ(obj->step[0], 10);
    ASSERT_EQ(obj->step[1], 20);
    ASSERT_EQ(obj->at<uchar>(0, 0, 0), 128);
    ASSERT_EQ(obj->at<uchar>(1, 2, 3), 128);
}

TEST(test_core_Mat, _newdelete_9) {
    auto deleter = [](const cv::Mat *obj) {
        core_Mat_delete(obj);
    };

    cv::Mat org(10, 10, CV_32FC1);
    org.forEach<float>([](float &pixel, const int *pos)
    {
	    pixel = static_cast<float>(pos[0] + pos[1]);
    });

    cv::Mat* mat = nullptr;
	ASSERT_EQ(core_Mat_new9(&org, cvSlice(1, 3), cvSlice(2, 5), &mat), ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::Mat, decltype(deleter)> obj(mat);
    
    ASSERT_EQ(obj->rows, 2);
    ASSERT_EQ(obj->cols, 3);
    ASSERT_EQ(obj->type(), CV_32FC1);
    ASSERT_NE(obj->data, org.data);
    ASSERT_FALSE(obj->isContinuous());
    ASSERT_EQ(obj->at<float>(0, 0), 3.0f);
}

TEST(test_core_Mat, _newdelete_10) {
    auto deleter = [](const cv::Mat *obj) {
        core_Mat_delete(obj);
    };

    cv::Mat org(10, 10, CV_32SC1);
    org.forEach<int>([](int &pixel, const int *pos)
    {
	    pixel = pos[0] + pos[1];
    });

    cv::Mat* mat = nullptr;
	ASSERT_EQ(core_Mat_new10(&org, cvRect(1, 2, 3, 4), &mat), ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::Mat, decltype(deleter)> obj(mat);
    
    ASSERT_EQ(obj->rows, 4);
    ASSERT_EQ(obj->cols, 3);
    ASSERT_EQ(obj->type(), CV_32SC1);
    ASSERT_NE(obj->data, org.data);
    ASSERT_FALSE(obj->isContinuous());
    ASSERT_EQ(obj->at<int>(0, 0), 3.0f);
}

TEST(test_core_Mat, size) {
    const cv::Mat m(3, 4, CV_8UC1);

    ASSERT_EQ(core_Mat_rows(&m), 3);
    ASSERT_EQ(core_Mat_cols(&m), 4);

    ASSERT_EQ(static_cast<cv::Size>(core_Mat_size(&m)), cv::Size(4, 3));

    int dim0, dim1;
    ASSERT_EQ(
        core_Mat_sizeAt(&m, 0, &dim0),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(
        core_Mat_sizeAt(&m, 1, &dim1),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(dim0, 3);
    ASSERT_EQ(dim1, 4);
}

TEST(test_core_Mat, data) {
    const cv::Mat m(3, 4, CV_8UC1);
    
    ASSERT_NE(core_Mat_data(&m), nullptr);
}

TEST(test_core_Mat, step) {
    const cv::Mat m(3, 7, CV_8UC1);
    
    ASSERT_EQ(core_Mat_step(&m), 7);

    size_t step0{};
    ASSERT_EQ(
        core_Mat_stepAt(&m, 0, &step0), 
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(step0, 7);
}
