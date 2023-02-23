#pragma once

#include "common.hpp"
#include <string>

// ReSharper disable CppNonInlineFunctionDefinitionInHeaderFile
// ReSharper disable CppInconsistentNaming

CVAPI(std::string*) std_string_new1()
{
    return new std::string;
}

CVAPI(std::string*) std_string_new2(const char* str)
{
    return new std::string(str);
}

CVAPI(void) std_string_new3(std::string** obj)
{
    *obj = new std::string;
}

CVAPI(void) std_string_delete(const std::string* obj)
{
    delete obj;
}

CVAPI(const char*) std_string_c_str(std::string* obj)
{
    return obj->c_str();
}

