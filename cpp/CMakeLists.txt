cmake_minimum_required(VERSION 3.20)

project(OpenCvSharpExtern LANGUAGES CXX VERSION 0.1.0)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)
set(VCPKG_BUILD_TYPE release)

add_compile_definitions(CVAPI_EXPORTS)

message("### CMAKE_SOURCE_DIR = ${CMAKE_SOURCE_DIR}")
message("### CMAKE_TOOLCHAIN_FILE = ${CMAKE_TOOLCHAIN_FILE}")

find_package(OpenCV REQUIRED)

include_directories(
    ${OpenCV_INCLUDE_DIRS}
)

set(CMAKE_MSVC_RUNTIME_LIBRARY "MultiThreaded$<$<CONFIG:Debug>:Debug>")
add_subdirectory(src)

#add_library(${PROJECT_NAME} SHARED ${SOURCE_FILES})

add_library(
    ${PROJECT_NAME} SHARED 
    $<TARGET_OBJECTS:WRAPPER_SOURCES>)
target_link_libraries(
    ${PROJECT_NAME}
    ${OpenCV_LIBRARIES} -static
)

include (GenerateExportHeader)
GENERATE_EXPORT_HEADER(${PROJECT_NAME})