cmake_minimum_required(VERSION 3.20)

add_library(
    WRAPPER_SOURCES STATIC
    "core.cpp" 
    "imgproc.cpp" 
    "std_string.cpp" "imgcodecs.cpp")
