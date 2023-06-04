#include <gtest/gtest.h>
#include "../src/core_Mat.hpp"
#include <array>

TEST(CoreTestMat, Newdelete1) {
    auto deleter = [](const cv::Mat *obj) {
        core_Mat_delete(obj);
    };

    cv::Mat* mat = nullptr;
    ASSERT_EQ(core_Mat_new1(&mat), ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::Mat, decltype(deleter)> obj(mat);

    ASSERT_EQ(obj->empty(), true);
}

TEST(CoreTestMat, newdelete2) {
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

TEST(CoreTestMat, newdelete3) {
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

TEST(CoreTestMat, newdelete4) {
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

TEST(CoreTestMat, newdelete5) {
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

TEST(CoreTestMat, newdelete6) {
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

TEST(CoreTestMat, newdelete7) {
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

TEST(CoreTestMat, newdelete8) {
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

TEST(CoreTestMat, newdelete9) {
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

TEST(CoreTestMat, newdelete10) {
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

TEST(CoreTestMat, size) {
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

TEST(CoreTestMat, data) {
    const cv::Mat m(3, 4, CV_8UC1);
    
    ASSERT_NE(core_Mat_data(&m), nullptr);
}

TEST(CoreTests, step) {
    const cv::Mat m(3, 7, CV_8UC1);
    
    ASSERT_EQ(core_Mat_step(&m), 7);

    size_t step0{};
    ASSERT_EQ(
        core_Mat_stepAt(&m, 0, &step0), 
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(step0, 7);
}

#pragma region Methods

TEST(CoreTestMat, type) {
    int t;

    cv::Mat m1;
    ASSERT_EQ(
        core_Mat_type(&m1, &t),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(t, 0);

    m1.create(10, 10, CV_8UC3);
    ASSERT_EQ(
        core_Mat_type(&m1, &t),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(t, CV_8UC3);

    const cv::Mat m2(10, 10, CV_32SC4);
    ASSERT_EQ(
        core_Mat_type(&m2, &t),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(t, CV_32SC4);
}

TEST(CoreTestMat, depth) {
    int d;

    cv::Mat m1;
    ASSERT_EQ(
        core_Mat_depth(&m1, &d),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(d, 0);

    m1.create(10, 10, CV_8UC1);
    ASSERT_EQ(
        core_Mat_depth(&m1, &d),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(d, CV_8U);

    const cv::Mat m2(10, 10, CV_32FC3);
    ASSERT_EQ(
        core_Mat_depth(&m2, &d),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(d, CV_32F);
}

TEST(CoreTestMat, channels) {
    int ch;

    cv::Mat m1(10, 10, CV_8UC1);
    ASSERT_EQ(
        core_Mat_channels(&m1, &ch),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(ch, 1);

    const cv::Mat m2(10, 10, CV_8UC3);
    ASSERT_EQ(
        core_Mat_channels(&m2, &ch),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(ch, 3);
}

TEST(CoreTestMat, empty) {
    int empty;

    cv::Mat m1;
    ASSERT_EQ(
        core_Mat_empty(&m1, &empty),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(empty, 1);

    m1.create(10, 10, CV_8UC1);
    ASSERT_EQ(
        core_Mat_empty(&m1, &empty),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(empty, 0);

    const cv::Mat m2(10, 10, CV_8UC1, cv::Scalar::all(1));
    ASSERT_EQ(
        core_Mat_empty(&m2, &empty),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(empty, 0);
}

TEST(CoreTestMat, total) {
    size_t total;

    const cv::Mat m1 = cv::Mat::zeros(10, 10, CV_8UC1);
    ASSERT_EQ(
        core_Mat_total(&m1, &total),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(total, 100);

    const cv::Mat m2 = cv::Mat::ones(3, 4, CV_8UC1);
    ASSERT_EQ(
        core_Mat_total(&m2, &total),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(total, 12);
}

#pragma endregion