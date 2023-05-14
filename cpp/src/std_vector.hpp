#pragma once

// ReSharper disable CppNonInlineFunctionDefinitionInHeaderFile

#include "common.hpp"


#pragma region std::string
CVAPI(std::vector<std::string>*) vector_string_new1()
{
    return new std::vector<std::string>;
}
CVAPI(std::vector<std::string>*) vector_string_new2(size_t size)
{
    return new std::vector<std::string>(size);
}
CVAPI(size_t) vector_string_getSize(std::vector<std::string>* vec)
{
    return vec->size();
}
CVAPI(void) vector_string_getElements(std::vector<std::string>* vector, const char** cStringPointers, int32_t* stringLengths)
{
    for (size_t i = 0; i < vector->size(); i++)
    {
        const auto& elem = vector->at(i);
        cStringPointers[i] = elem.c_str();
        stringLengths[i] = static_cast<int32_t>(elem.size());
    }
}
CVAPI(void) vector_string_delete(std::vector<std::string>* vector)
{
    delete vector;
}
#pragma endregion

#pragma region cv::Vec2f

CVAPI(std::vector<cv::Vec2f>*) vector_Vec2f_new1()
{
    return new std::vector<cv::Vec2f>;
}

CVAPI(size_t) vector_Vec2f_getSize(std::vector<cv::Vec2f>* vector)
{
    return vector->size();
}

CVAPI(cv::Vec2f*) vector_Vec2f_getPointer(std::vector<cv::Vec2f>* vector)
{
    return &(vector->at(0));
}

CVAPI(void) vector_Vec2f_delete(std::vector<cv::Vec2f>* vector)
{
    delete vector;
}

#pragma endregion

#pragma region cv::Vec3f
CVAPI(std::vector<cv::Vec3f>*) vector_Vec3f_new1()
{
    return new std::vector<cv::Vec3f>;
}

CVAPI(size_t) vector_Vec3f_getSize(std::vector<cv::Vec3f>* vector)
{
    return vector->size();
}

CVAPI(cv::Vec3f*) vector_Vec3f_getPointer(std::vector<cv::Vec3f>* vector)
{
    return &(vector->at(0));
}

CVAPI(void) vector_Vec3f_delete(std::vector<cv::Vec3f>* vector)
{
    delete vector;
}
#pragma endregion

#pragma region cv::Vec4f

CVAPI(std::vector<cv::Vec4f>*) vector_Vec4f_new1()
{
    return new std::vector<cv::Vec4f>;
}

CVAPI(std::vector<cv::Vec4f>*) vector_Vec4f_new3(cv::Vec4f* data, size_t dataLength)
{
    return new std::vector<cv::Vec4f>(data, data + dataLength);
}

CVAPI(size_t) vector_Vec4f_getSize(std::vector<cv::Vec4f>* vector)
{
    return vector->size();
}

CVAPI(cv::Vec4f*) vector_Vec4f_getPointer(std::vector<cv::Vec4f>* vector)
{
    return &(vector->at(0));
}

CVAPI(void) vector_Vec4f_delete(std::vector<cv::Vec4f>* vector)
{
    delete vector;
}

#pragma endregion

#pragma region cv::Vec4i

CVAPI(std::vector<cv::Vec4i>*) vector_Vec4i_new1()
{
    return new std::vector<cv::Vec4i>;
}

CVAPI(std::vector<cv::Vec4i>*) vector_Vec4i_new3(cv::Vec4i* data, size_t dataLength)
{
    return new std::vector<cv::Vec4i>(data, data + dataLength);
}

CVAPI(size_t) vector_Vec4i_getSize(std::vector<cv::Vec4i>* vector)
{
    return vector->size();
}

CVAPI(cv::Vec4i*) vector_Vec4i_getPointer(std::vector<cv::Vec4i>* vector)
{
    return &(vector->at(0));
}

CVAPI(void) vector_Vec4i_delete(std::vector<cv::Vec4i>* vector)
{
    delete vector;
}

#pragma endregion

#pragma region cv::Vec6f
CVAPI(std::vector<cv::Vec6f>*) vector_Vec6f_new1()
{
    return new std::vector<cv::Vec6f>;
}

CVAPI(size_t) vector_Vec6f_getSize(std::vector<cv::Vec6f>* vector)
{
    return vector->size();
}

CVAPI(cv::Vec6f*) vector_Vec6f_getPointer(std::vector<cv::Vec6f>* vector)
{
    return &(vector->at(0));
}

CVAPI(void) vector_Vec6f_delete(std::vector<cv::Vec6f>* vector)
{
    delete vector;
}
#pragma endregion

#pragma region cv::Point2i
CVAPI(std::vector<cv::Point>*) vector_Point2i_new1()
{
    return new std::vector<cv::Point>;
}
CVAPI(std::vector<cv::Point>*) vector_Point2i_new2(size_t size)
{
    return new std::vector<cv::Point>(size);
}
CVAPI(std::vector<cv::Point>*) vector_Point2i_new3(cv::Point* data, size_t dataLength)
{
    return new std::vector<cv::Point>(data, data + dataLength);
}
CVAPI(size_t) vector_Point2i_getSize(std::vector<cv::Point>* vector)
{
    return vector->size();
}
CVAPI(cv::Point*) vector_Point2i_getPointer(std::vector<cv::Point>* vector)
{
    return &(vector->at(0));
}
CVAPI(void) vector_Point2i_delete(std::vector<cv::Point>* vector)
{
    delete vector;
}
#pragma endregion

#pragma region cv::Point2f
CVAPI(std::vector<cv::Point2f>*) vector_Point2f_new1()
{
    return new std::vector<cv::Point2f>;
}
CVAPI(std::vector<cv::Point2f>*) vector_Point2f_new2(size_t size)
{
    return new std::vector<cv::Point2f>(size);
}
CVAPI(std::vector<cv::Point2f>*) vector_Point2f_new3(cv::Point2f* data, size_t dataLength)
{
    return new std::vector<cv::Point2f>(data, data + dataLength);
}
CVAPI(size_t) vector_Point2f_getSize(std::vector<cv::Point2f>* vector)
{
    return vector->size();
}
CVAPI(cv::Point2f*) vector_Point2f_getPointer(std::vector<cv::Point2f>* vector)
{
    return &(vector->at(0));
}
CVAPI(void) vector_Point2f_delete(std::vector<cv::Point2f>* vector)
{
    delete vector;
}
#pragma endregion

#pragma region cv::Point2d

CVAPI(std::vector<cv::Point2d>*) vector_Point2d_new1()
{
    return new std::vector<cv::Point2d>;
}

CVAPI(size_t) vector_Point2d_getSize(std::vector<cv::Point2d>* vector)
{
    return vector->size();
}

CVAPI(cv::Point2d*) vector_Point2d_getPointer(std::vector<cv::Point2d>* vector)
{
    return &(vector->at(0));
}

CVAPI(void) vector_Point2d_delete(std::vector<cv::Point2d>* vector)
{
    delete vector;
}

#pragma endregion

#pragma region cv::Point3f
CVAPI(std::vector<cv::Point3f>*) vector_Point3f_new1()
{
    return new std::vector<cv::Point3f>;
}
CVAPI(std::vector<cv::Point3f>*) vector_Point3f_new2(size_t size)
{
    return new std::vector<cv::Point3f>(size);
}
CVAPI(std::vector<cv::Point3f>*) vector_Point3f_new3(cv::Point3f* data, size_t dataLength)
{
    return new std::vector<cv::Point3f>(data, data + dataLength);
}
CVAPI(size_t) vector_Point3f_getSize(std::vector<cv::Point3f>* vector)
{
    return vector->size();
}
CVAPI(cv::Point3f*) vector_Point3f_getPointer(std::vector<cv::Point3f>* vector)
{
    return &(vector->at(0));
}
CVAPI(void) vector_Point3f_delete(std::vector<cv::Point3f>* vector)
{
    delete vector;
}
#pragma endregion

#pragma region cv::Rect
CVAPI(std::vector<cv::Rect>*) vector_Rect_new1()
{
    return new std::vector<cv::Rect>;
}
CVAPI(std::vector<cv::Rect>*) vector_Rect_new2(size_t size)
{
    return new std::vector<cv::Rect>(size);
}
CVAPI(std::vector<cv::Rect>*) vector_Rect_new3(cv::Rect* data, size_t dataLength)
{
    return new std::vector<cv::Rect>(data, data + dataLength);
}
CVAPI(size_t) vector_Rect_getSize(std::vector<cv::Rect>* vector)
{
    return vector->size();
}
CVAPI(cv::Rect*) vector_Rect_getPointer(std::vector<cv::Rect>* vector)
{
    return &(vector->at(0));
}
CVAPI(void) vector_Rect_delete(std::vector<cv::Rect>* vector)
{
    //vector->~vector();
    delete vector;
}

#pragma endregion

#pragma region cv::Rect2d
CVAPI(std::vector<cv::Rect2d>*) vector_Rect2d_new1()
{
    return new std::vector<cv::Rect2d>;
}
CVAPI(std::vector<cv::Rect2d>*) vector_Rect2d_new2(size_t size)
{
    return new std::vector<cv::Rect2d>(size);
}
CVAPI(std::vector<cv::Rect2d>*) vector_Rect2d_new3(cv::Rect2d* data, size_t dataLength)
{
    return new std::vector<cv::Rect2d>(data, data + dataLength);
}
CVAPI(size_t) vector_Rect2d_getSize(std::vector<cv::Rect2d>* vector)
{
    return vector->size();
}
CVAPI(cv::Rect2d*) vector_Rect2d_getPointer(std::vector<cv::Rect2d>* vector)
{
    return &(vector->at(0));
}
CVAPI(void) vector_Rect2d_delete(std::vector<cv::Rect2d>* vector)
{
    delete vector;
}

#pragma endregion

#pragma region cv::RotatedRect
CVAPI(std::vector<cv::RotatedRect>*) vector_RotatedRect_new1()
{
    return new std::vector<cv::RotatedRect>;
}
CVAPI(std::vector<cv::RotatedRect>*) vector_RotatedRect_new2(size_t size)
{
    return new std::vector<cv::RotatedRect>(size);
}
CVAPI(std::vector<cv::RotatedRect>*) vector_RotatedRect_new3(cv::RotatedRect* data, size_t dataLength)
{
    return new std::vector<cv::RotatedRect>(data, data + dataLength);
}
CVAPI(size_t) vector_RotatedRect_getSize(std::vector<cv::RotatedRect>* vector)
{
    return vector->size();
}
CVAPI(cv::RotatedRect*) vector_RotatedRect_getPointer(std::vector<cv::RotatedRect>* vector)
{
    return &(vector->at(0));
}
CVAPI(void) vector_RotatedRect_delete(std::vector<cv::RotatedRect>* vector)
{
    delete vector;
}

#pragma endregion

#pragma region cv::KeyPoint
CVAPI(std::vector<cv::KeyPoint>*) vector_KeyPoint_new1()
{
    return new std::vector<cv::KeyPoint>;
}
CVAPI(std::vector<cv::KeyPoint>*) vector_KeyPoint_new2(size_t size)
{
    return new std::vector<cv::KeyPoint>(size);
}
CVAPI(std::vector<cv::KeyPoint>*) vector_KeyPoint_new3(cv::KeyPoint* data, size_t dataLength)
{
    return new std::vector<cv::KeyPoint>(data, data + dataLength);
}
CVAPI(size_t) vector_KeyPoint_getSize(std::vector<cv::KeyPoint>* vector)
{
    return vector->size();
}
CVAPI(cv::KeyPoint*) vector_KeyPoint_getPointer(std::vector<cv::KeyPoint>* vector)
{
    return &(vector->at(0));
}
CVAPI(void) vector_KeyPoint_delete(std::vector<cv::KeyPoint>* vector)
{
    //vector->~vector();
    delete vector;
}
#pragma endregion

#pragma region cv::DMatch
CVAPI(std::vector<cv::DMatch>*) vector_DMatch_new1()
{
    return new std::vector<cv::DMatch>;
}
CVAPI(std::vector<cv::DMatch>*) vector_DMatch_new2(size_t size)
{
    return new std::vector<cv::DMatch>(size);
}
CVAPI(std::vector<cv::DMatch>*) vector_DMatch_new3(cv::DMatch* data, size_t dataLength)
{
    return new std::vector<cv::DMatch>(data, data + dataLength);
}
CVAPI(size_t) vector_DMatch_getSize(std::vector<cv::DMatch>* vector)
{
    return vector->size();
}
CVAPI(cv::DMatch*) vector_DMatch_getPointer(std::vector<cv::DMatch>* vector)
{
    return &(vector->at(0));
}
CVAPI(void) vector_DMatch_delete(std::vector<cv::DMatch>* vector)
{
    delete vector;
}
#pragma endregion

#pragma region cv::Mat
CVAPI(std::vector<cv::Mat>*) vector_Mat_new1()
{
    return new std::vector<cv::Mat>;
}
CVAPI(std::vector<cv::Mat>*) vector_Mat_new2(uint32_t size)
{
    return new std::vector<cv::Mat>(size);
}
CVAPI(std::vector<cv::Mat>*) vector_Mat_new3(cv::Mat** data, uint32_t dataLength)
{
    auto* vec = new std::vector<cv::Mat>(dataLength);
    for (size_t i = 0; i < dataLength; i++)
    {
        (*vec)[i] = *(data[i]);
    }
    return vec;
}

CVAPI(size_t) vector_Mat_getSize(std::vector<cv::Mat>* vector)
{
    return vector->size();
}

CVAPI(cv::Mat*) vector_Mat_getPointer(std::vector<cv::Mat>* vector)
{
    return &(vector->at(0));
}

CVAPI(void) vector_Mat_assignToArray(std::vector<cv::Mat>* vector, cv::Mat** arr)
{
    for (size_t i = 0; i < vector->size(); i++)
    {
        (vector->at(i)).assignTo(*(arr[i]));
    }
}

CVAPI(void) vector_Mat_delete(std::vector<cv::Mat>* vector)
{
    //vector->~vector();
    delete vector;
}
#pragma endregion
