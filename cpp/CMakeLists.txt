cmake_minimum_required(VERSION 3.20)

project(OpenCvSharpExtern LANGUAGES CXX VERSION 0.1.0)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)
set(VCPKG_BUILD_TYPE release)

message("### CMAKE_SOURCE_DIR = ${CMAKE_SOURCE_DIR}")
message("### CMAKE_TOOLCHAIN_FILE = ${CMAKE_TOOLCHAIN_FILE}")

set(SOURCE_FILES
    "main.cpp"
    "core.hpp")

set(SOURCE_FILES_IMGPROC
    "imgproc.cpp"
    "imgproc.hpp")

set(SOURCE_FILES_ML
    "main.cpp"
    "ml.hpp")

add_compile_definitions(CVAPI_EXPORTS)

#set(CMAKE_TOOLCHAIN_FILE 
#    ${CMAKE_SOURCE_DIR}/vcpkg/scripts/buildsystems/vcpkg.cmake)

#set(OpenCV_STATIC ON)
#if(MSVC)
#    set(CMAKE_CXX_FLAGS_RELEASE "${CMAKE_CXX_FLAGS_RELEASE} /MT")
#	set(CMAKE_CXX_FLAGS_DEBUG "${CMAKE_CXX_FLAGS_DEBUG} /MTd")
		
#	set(CMAKE_CXX_STANDARD_LIBRARIES "${CMAKE_CXX_STANDARD_LIBRARIES} kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib")
#endif()
find_package(OpenCV REQUIRED)

include_directories(
    ${OpenCV_INCLUDE_DIRS}
)

set(CMAKE_MSVC_RUNTIME_LIBRARY "MultiThreaded$<$<CONFIG:Debug>:Debug>")
add_library(${PROJECT_NAME} SHARED ${SOURCE_FILES})
add_library("${PROJECT_NAME}_imgproc" SHARED ${SOURCE_FILES_IMGPROC})
add_library("${PROJECT_NAME}_ml" SHARED ${SOURCE_FILES_ML})


target_link_libraries(
    ${PROJECT_NAME}
    ${OpenCV_LIBRARIES} -static
)
target_link_libraries(
    "${PROJECT_NAME}_imgproc"
    ${OpenCV_LIBRARIES} -static
)
target_link_libraries(
    "${PROJECT_NAME}_ml"
    ${OpenCV_LIBRARIES} -static
)


include (GenerateExportHeader)
GENERATE_EXPORT_HEADER(${PROJECT_NAME})