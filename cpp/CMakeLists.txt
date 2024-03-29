﻿cmake_minimum_required(VERSION 3.20)

project(OpenCvSharpExtern LANGUAGES CXX VERSION 0.1.0)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)
set(VCPKG_BUILD_TYPE release)
set(CMAKE_POSITION_INDEPENDENT_CODE ON)

add_compile_options("$<$<C_COMPILER_ID:MSVC>:/utf-8>")
add_compile_options("$<$<CXX_COMPILER_ID:MSVC>:/utf-8>")
add_compile_options("$<$<CXX_COMPILER_ID:MSVC>:/Zc:__cplusplus>")

add_compile_definitions(CVAPI_EXPORTS)

message("### CMAKE_SOURCE_DIR = ${CMAKE_SOURCE_DIR}")
message("### CMAKE_TOOLCHAIN_FILE = ${CMAKE_TOOLCHAIN_FILE}")
message("### CMAKE_GENERATOR = ${CMAKE_GENERATOR}")
message("### WASM_LIB = ${WASM_LIB}")

find_package(OpenCV REQUIRED)

include_directories(
    ${OpenCV_INCLUDE_DIRS}
)

set(CMAKE_MSVC_RUNTIME_LIBRARY "MultiThreaded$<$<CONFIG:Debug>:Debug>")

include(CheckCXXCompilerFlag)
check_cxx_compiler_flag("-fuse-ld=gold" COMPILER_SUPPORTS_GOLD)
if(COMPILER_SUPPORTS_GOLD)
  execute_process(COMMAND ${CMAKE_CXX_COMPILER} -fuse-ld=gold -Wl,--version OUTPUT_VARIABLE stdout ERROR_QUIET)
  if("${stdout}" MATCHES "GNU gold")
    set(CMAKE_EXE_LINKER_FLAGS "${CMAKE_EXE_LINKER_FLAGS} -fuse-ld=gold")
    set(CMAKE_SHARED_LINKER_FLAGS "${CMAKE_SHARED_LINKER_FLAGS} -fuse-ld=gold")
  endif()
endif()

add_subdirectory(src)

if(EMSCRIPTEN)
    add_library(
        ${PROJECT_NAME} SHARED 
        $<TARGET_OBJECTS:WRAPPER_SOURCES> ${WASM_LIB})
else()
    add_library(
        ${PROJECT_NAME} SHARED 
        $<TARGET_OBJECTS:WRAPPER_SOURCES>)
    target_link_libraries(
        ${PROJECT_NAME}
        ${OpenCV_LIBRARIES})
endif()

#include (GenerateExportHeader)
#GENERATE_EXPORT_HEADER(${PROJECT_NAME})

include(CTest)
include(FetchContent)
enable_testing()

FetchContent_Declare(
    googletest
    GIT_REPOSITORY https://github.com/google/googletest.git
    GIT_TAG        b796f7d44681514f58a683a3a71ff17c94edb0c1 # release-1.13.0
    #GIT_TAG        58d77fa8070e8cec2dc1ed015d66b454c8d78850 # release-1.12.1
)

# For Windows: Prevent overriding the parent project's compiler/linker settings
set(gtest_force_shared_crt ON CACHE BOOL "" FORCE)
FetchContent_MakeAvailable(googletest)

add_executable(
    ${PROJECT_NAME}.Tests
    test/test_core.cpp
    test/test_core_Mat.cpp
    test/test_core_InputArray.cpp
    test/test_imgproc.cpp
    test/test_imgcodecs.cpp
     "test/test_core_MatExpr.cpp")
target_link_libraries(
    ${PROJECT_NAME}.Tests 
    gtest_main 
    ${PROJECT_NAME})
add_test(
    NAME ${PROJECT_NAME}.Tests
    COMMAND ${PROJECT_NAME}.Tests)
