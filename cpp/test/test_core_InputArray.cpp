#include <gtest/gtest.h>
#include "../src/core_InputArray.hpp"

TEST(test_core_InputOutputArray, newdelete_byMat)
{
    auto deleter = [](const cv::_InputOutputArray* obj) {
        core_InputOutputArray_delete(obj);
    };

    cv::Mat mat(3, 4, CV_8UC1);
    cv::_InputOutputArray* ioa = nullptr;
    ASSERT_EQ(core_InputOutputArray_new_byMat(&mat, &ioa), ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::_InputOutputArray, decltype(deleter)> obj(ioa);

    ASSERT_EQ(obj->empty(), false);
    ASSERT_EQ(obj->isMat(), true);
}
