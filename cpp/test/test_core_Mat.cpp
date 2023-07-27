#include <gtest/gtest.h>
#include "../src/core_Mat.hpp"
#include <array>

#pragma region Constructors

struct MatDeleter
{
    void operator()(const cv::Mat *obj) const
    {
        core_Mat_delete(obj);
    }
};
struct MatExprDeleter
{
    void operator()(const cv::MatExpr *obj) const
    {
        delete obj;
    }
};

TEST(CoreTestMat, newDelete1)
{
    cv::Mat* mat = nullptr;
    ASSERT_EQ(core_Mat_new1(&mat), ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::Mat, MatDeleter> obj(mat);

    ASSERT_EQ(obj->empty(), true);
}

TEST(CoreTestMat, newDelete2)
{
    cv::Mat* mat = nullptr;
    ASSERT_EQ(core_Mat_new2(3, 4, CV_8UC1, &mat), ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::Mat, MatDeleter> obj(mat);

    ASSERT_EQ(obj->rows, 3);
    ASSERT_EQ(obj->cols, 4);
    ASSERT_EQ(obj->type(), CV_8UC1);
}

TEST(CoreTestMat, newDelete3)
{
    cv::Mat* mat = nullptr;
    ASSERT_EQ(core_Mat_new3(3, 4, CV_8UC4, cvScalar(1, 2, 3, 4), &mat), ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::Mat, MatDeleter> obj(mat);
    
    ASSERT_EQ(obj->rows, 3);
    ASSERT_EQ(obj->cols, 4);
    ASSERT_EQ(obj->type(), CV_8UC4);
    ASSERT_EQ(obj->at<cv::Vec4b>(0, 0), cv::Vec4b(1, 2, 3, 4));
    ASSERT_EQ(obj->at<cv::Vec4b>(2, 3), cv::Vec4b(1, 2, 3, 4));
}

TEST(CoreTestMat, newDelete4)
{
    cv::Mat* mat = nullptr;
    int sizes[3] = {2, 3, 4};
    ASSERT_EQ(core_Mat_new4(3, sizes, CV_8UC1, &mat), ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::Mat, MatDeleter> obj(mat);
    
    ASSERT_EQ(obj->rows, -1);
    ASSERT_EQ(obj->cols, -1);
    ASSERT_EQ(obj->dims, 3);
    ASSERT_EQ(obj->size[0], 2);
    ASSERT_EQ(obj->size[1], 3);
    ASSERT_EQ(obj->size[2], 4);
    ASSERT_EQ(obj->type(), CV_8UC1);
}

TEST(CoreTestMat, newDelete5)
{
    cv::Mat* mat = nullptr;
    int sizes[3] = {2, 3, 4};
    ASSERT_EQ(core_Mat_new5(3, sizes, CV_32SC4, cvScalar(1111, 2222, 3333, 4444), &mat), ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::Mat, MatDeleter> obj(mat);
    
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

TEST(CoreTestMat, newDelete6)
{
    cv::Mat org(3, 4, CV_32FC1, cv::Scalar(3.14));
    cv::Mat* mat = nullptr;
    ASSERT_EQ(core_Mat_new6(&org, &mat), ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::Mat, MatDeleter> obj(mat);
    
    ASSERT_EQ(obj->rows, 3);
    ASSERT_EQ(obj->cols, 4);
    ASSERT_EQ(obj->type(), CV_32FC1);
    ASSERT_EQ(obj->data, org.data);
}

TEST(CoreTestMat, newDelete7)
{
    cv::Mat* mat = nullptr;
    std::array<uchar, 200> data{};
    data.fill(255);

    ASSERT_EQ(core_Mat_new7(2, 3, CV_8UC1, data.data(), 100, &mat), ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::Mat, MatDeleter> obj(mat);
    
    ASSERT_EQ(obj->rows, 2);
    ASSERT_EQ(obj->cols, 3);
    ASSERT_EQ(obj->type(), CV_8UC1);
    ASSERT_EQ(obj->step, 100);
    ASSERT_EQ(obj->at<uchar>(0, 0), 255);
    ASSERT_EQ(obj->at<uchar>(1, 2), 255);
}

TEST(CoreTestMat, newDelete8)
{
    cv::Mat* mat = nullptr;
    std::array<uchar, 1000> data{};
    data.fill(128);
    constexpr int sizes[3] = {2, 3, 4};
    constexpr size_t steps[2] = {10, 20};

    ASSERT_EQ(core_Mat_new8(3, sizes, CV_8UC1, data.data(), steps, &mat), ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::Mat, MatDeleter> obj(mat);
    
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

TEST(CoreTestMat, newDelete9)
{
    cv::Mat org(10, 10, CV_32FC1);
    org.forEach<float>([](float &pixel, const int *pos)
    {
        pixel = static_cast<float>(pos[0] + pos[1]);
    });

    cv::Mat* mat = nullptr;
    ASSERT_EQ(core_Mat_new9(&org, cvSlice(1, 3), cvSlice(2, 5), &mat), ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::Mat, MatDeleter> obj(mat);
    
    ASSERT_EQ(obj->rows, 2);
    ASSERT_EQ(obj->cols, 3);
    ASSERT_EQ(obj->type(), CV_32FC1);
    ASSERT_NE(obj->data, org.data);
    ASSERT_FALSE(obj->isContinuous());
    ASSERT_EQ(obj->at<float>(0, 0), 3.0f);
}

TEST(CoreTestMat, newDelete10)
{
    cv::Mat org(10, 10, CV_32SC1);
    org.forEach<int>([](int &pixel, const int *pos)
    {
        pixel = pos[0] + pos[1];
    });

    cv::Mat* mat = nullptr;
    ASSERT_EQ(core_Mat_new10(&org, cvRect(1, 2, 3, 4), &mat), ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::Mat, MatDeleter> obj(mat);
    
    ASSERT_EQ(obj->rows, 4);
    ASSERT_EQ(obj->cols, 3);
    ASSERT_EQ(obj->type(), CV_32SC1);
    ASSERT_NE(obj->data, org.data);
    ASSERT_FALSE(obj->isContinuous());
    ASSERT_EQ(obj->at<int>(0, 0), 3.0f);
}

#pragma endregion

#pragma region Fields

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

#pragma endregion

#pragma region Methods

TEST(CoreTestMat, diag)
{
    const cv::Mat src = (cv::Mat_<uchar>(3,3) <<
                    1,2,3,
                    4,5,6,
                    7,8,9);
    cv::Mat *dst = nullptr;

    ASSERT_EQ(
        core_Mat_diag(&src, 0, &dst),
        ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::Mat, MatDeleter> obj(dst);
    
    ASSERT_EQ(dst->type(), CV_8UC1);
    ASSERT_EQ(dst->size(), cv::Size(1, 3));
    ASSERT_EQ(dst->at<uchar>(0), 1);
    ASSERT_EQ(dst->at<uchar>(1), 5);
    ASSERT_EQ(dst->at<uchar>(2), 9);
}

TEST(CoreTestMat, clone)
{
#ifdef _WIN32 // hangs on Ubuntu???
    const cv::Mat src = (cv::Mat_<uchar>(2,2) <<
                    1,2,
                    3,4);
    cv::Mat *dst = nullptr;

    ASSERT_EQ(
        core_Mat_clone(&src, &dst),
        ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::Mat, MatDeleter> obj(dst);
    
    ASSERT_EQ(dst->type(), CV_8UC1);
    ASSERT_EQ(dst->size(), cv::Size(2, 2));
    ASSERT_EQ(dst->at<uchar>(0, 0), 1);
    ASSERT_EQ(dst->at<uchar>(0, 1), 2);
    ASSERT_EQ(dst->at<uchar>(1, 0), 3);
    ASSERT_EQ(dst->at<uchar>(1, 1), 4);
#endif
}

TEST(CoreTestMat, copyTo)
{
#ifdef _WIN32 // hangs on Ubuntu???
    const cv::Mat src = (cv::Mat_<uchar>(2,2) <<
                    1,2,
                    3,4);
    cv::Mat dst;
    const cv::_OutputArray dst_(dst);

    ASSERT_EQ(
        core_Mat_copyTo1(&src, &dst_),
        ExceptionStatus::NotOccurred);
    
    ASSERT_EQ(dst.type(), CV_8UC1);
    ASSERT_EQ(dst.size(), cv::Size(2, 2));
    ASSERT_EQ(dst.at<uchar>(0, 0), 1);
    ASSERT_EQ(dst.at<uchar>(0, 1), 2);
    ASSERT_EQ(dst.at<uchar>(1, 0), 3);
    ASSERT_EQ(dst.at<uchar>(1, 1), 4);
#endif
}

TEST(CoreTestMat, convertTo)
{
    const cv::Mat src = (cv::Mat_<uchar>(2,2) <<
                    32, 64,
                    96, 128);
    cv::Mat dst;
    const cv::_OutputArray dst_(dst);

    ASSERT_EQ(
        core_Mat_convertTo(&src, &dst_, CV_32FC1, 1./255, 1),
        ExceptionStatus::NotOccurred);
    
    ASSERT_EQ(dst.type(), CV_32FC1);
    ASSERT_EQ(dst.size(), cv::Size(2, 2));
    ASSERT_FLOAT_EQ(dst.at<float>(0, 0), 32.0f/255 + 1);
    ASSERT_FLOAT_EQ(dst.at<float>(0, 1), 64.0f/255 + 1);
    ASSERT_FLOAT_EQ(dst.at<float>(1, 0), 96.0f/255 + 1);
    ASSERT_FLOAT_EQ(dst.at<float>(1, 1), 128.0f/255 + 1);
}

TEST(CoreTestMat, assignTo)
{
    const cv::Mat src = (cv::Mat_<uchar>(2,2) <<
                    1, 2,
                    3, 4);
    cv::Mat dst;

    ASSERT_EQ(
        core_Mat_assignTo(&src, &dst, CV_32SC1),
        ExceptionStatus::NotOccurred);
    
    ASSERT_EQ(dst.type(), CV_32SC1);
    ASSERT_EQ(dst.size(), cv::Size(2, 2));
    ASSERT_EQ(dst.at<int>(0, 0), 1);
    ASSERT_EQ(dst.at<int>(0, 1), 2);
    ASSERT_EQ(dst.at<int>(1, 0), 3);
    ASSERT_EQ(dst.at<int>(1, 1), 4);
}

TEST(CoreTestMat, setTo)
{
    cv::Mat mat(2, 1, CV_8UC3);

    CvScalar value;
    value.val[0] = 1;
    value.val[1] = 2;
    value.val[2] = 3;
    value.val[3] = 4;

    ASSERT_EQ(
        core_Mat_setTo2(&mat, value, nullptr),
        ExceptionStatus::NotOccurred);
    
    ASSERT_EQ(mat.type(), CV_8UC3);
    ASSERT_EQ(mat.size(), cv::Size(1, 2));
    ASSERT_EQ(mat.at<cv::Vec3b>(0, 0), cv::Vec3b(1, 2, 3));
    ASSERT_EQ(mat.at<cv::Vec3b>(1, 0), cv::Vec3b(1, 2, 3));
}

TEST(CoreTestMat, setZero)
{
    cv::Mat mat = (cv::Mat_<uchar>(2,2) <<
                    1, 2,
                    3, 4);

    ASSERT_EQ(
        core_Mat_setZero(&mat),
        ExceptionStatus::NotOccurred);
    
    ASSERT_EQ(mat.type(), CV_8UC1);
    ASSERT_EQ(mat.size(), cv::Size(2, 2));
    ASSERT_EQ(cv::countNonZero(mat), 0);
}

TEST(CoreTestMat, reshape)
{
    const cv::Mat src = (cv::Mat_<cv::Vec2b>(2,2) <<
                    cv::Vec2b(1, 2), cv::Vec2b(3, 4),
                    cv::Vec2b(5, 6), cv::Vec2b(7, 8));
    cv::Mat *dst1 = nullptr;
    cv::Mat *dst2 = nullptr;

    ASSERT_EQ(
        core_Mat_reshape1(&src, 1, 2, &dst1),
        ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::Mat, MatDeleter> obj1(dst1);    
    ASSERT_EQ(dst1->type(), CV_8UC1);
    ASSERT_EQ(dst1->size(), cv::Size(4, 2));

    ASSERT_EQ(
        core_Mat_reshape1(&src, 2, 4, &dst2),
        ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::Mat, MatDeleter> obj2(dst2);    
    ASSERT_EQ(dst2->type(), CV_8UC2);
    ASSERT_EQ(dst2->size(), cv::Size(1, 4));
}

TEST(CoreTestMat, t)
{
    const cv::Mat src = (cv::Mat_<uchar>(2,2) <<
                    1, 2,
                    3, 4);
    cv::MatExpr *dst = nullptr;

    ASSERT_EQ(
        core_Mat_t(&src, &dst),
        ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::MatExpr, MatExprDeleter> obj(dst); 

    
    ASSERT_EQ(dst->type(), CV_8UC1);
    ASSERT_EQ(dst->size(), cv::Size(2, 2));

    const cv::Mat dstMat = *dst;
    ASSERT_EQ(dstMat.at<uchar>(0, 0), 1);
    ASSERT_EQ(dstMat.at<uchar>(0, 1), 3);
    ASSERT_EQ(dstMat.at<uchar>(1, 0), 2);
    ASSERT_EQ(dstMat.at<uchar>(1, 1), 4);
}

TEST(CoreTestMat, inv)
{
    const cv::Mat src = (cv::Mat_<double>(2,2) <<
                    1, 2,
                    3, 4);
    cv::MatExpr *dst = nullptr;

    ASSERT_EQ(
        core_Mat_inv(&src, cv::DECOMP_LU, &dst),
        ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::MatExpr, MatExprDeleter> obj(dst); 

    ASSERT_EQ(dst->type(), CV_64FC1);
    ASSERT_EQ(dst->size(), cv::Size(2, 2));

    const cv::Mat dstMat = *dst;
    ASSERT_EQ(dstMat.at<double>(0, 0), -2);
    ASSERT_EQ(dstMat.at<double>(0, 1), 1);
    ASSERT_EQ(dstMat.at<double>(1, 0), 1.5);
    ASSERT_EQ(dstMat.at<double>(1, 1), -0.5);
}

TEST(CoreTestMat, mul)
{
    const cv::Mat src1 = (cv::Mat_<double>(2,2) <<
                    1, 2,
                    3, 4);
    const cv::Mat src2 = (cv::Mat_<double>(2,2) <<
                    -1, -2,
                    3, 4);
    const cv::_InputArray src2_(src2);
    cv::MatExpr *dst = nullptr;

    ASSERT_EQ(
        core_Mat_mul(&src1, &src2_, 1, &dst),
        ExceptionStatus::NotOccurred);
    const std::unique_ptr<cv::MatExpr, MatExprDeleter> obj(dst); 

    ASSERT_EQ(dst->type(), CV_64FC1);
    ASSERT_EQ(dst->size(), cv::Size(2, 2));

    const cv::Mat dstMat = *dst;
    ASSERT_EQ(dstMat.at<double>(0, 0), -1);
    ASSERT_EQ(dstMat.at<double>(0, 1), -4);
    ASSERT_EQ(dstMat.at<double>(1, 0), 9);
    ASSERT_EQ(dstMat.at<double>(1, 1), 16);
}

TEST(CoreTestMat, isContinuous)
{
    int is_cont;

    const cv::Mat m1(3, 3, CV_16SC1, cv::Scalar(1));
    ASSERT_EQ(
        core_Mat_isContinuous(&m1, &is_cont),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(is_cont, 1);

    const cv::Mat m2 = m1(cv::Range(1, 3), cv::Range(0, 2));
    ASSERT_EQ(
        core_Mat_isContinuous(&m2, &is_cont),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(is_cont, 0);
}

TEST(CoreTestMat, isSubmatrix)
{
    int is_submat;

    const cv::Mat m1(3, 3, CV_16SC1, cv::Scalar(1));
    ASSERT_EQ(
        core_Mat_isSubmatrix(&m1, &is_submat),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(is_submat, 0);

    const cv::Mat m2 = m1(cv::Range(1, 3), cv::Range(0, 2));
    ASSERT_EQ(
        core_Mat_isSubmatrix(&m2, &is_submat),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(is_submat, 1);
}

TEST(CoreTestMat, elemSize)
{
    size_t elem_size;

    cv::Mat m(1, 1, CV_16SC1);
    ASSERT_EQ(
        core_Mat_elemSize(&m, &elem_size),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(elem_size, 2);

    m.create(1, 1, CV_16SC4);
    ASSERT_EQ(
        core_Mat_elemSize(&m, &elem_size),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(elem_size, 8);
}

TEST(CoreTestMat, elemSize1)
{
    size_t elem_size;

    cv::Mat m(1, 1, CV_16SC1);
    ASSERT_EQ(
        core_Mat_elemSize1(&m, &elem_size),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(elem_size, 2);

    m.create(1, 1, CV_16SC4);
    ASSERT_EQ(
        core_Mat_elemSize1(&m, &elem_size),
        ExceptionStatus::NotOccurred);
    ASSERT_EQ(elem_size, 2);
}

TEST(CoreTestMat, type)
{
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

TEST(CoreTestMat, depth)
{
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

TEST(CoreTestMat, channels)
{
    int ch;

    const cv::Mat m1(10, 10, CV_8UC1);
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

TEST(CoreTestMat, empty)
{
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

TEST(CoreTestMat, total)
{
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