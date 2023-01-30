# CMakeList.txt : 最上位の CMake プロジェクト ファイル。グローバル構成を行います
# また、サブプロジェクトをここに含めます。
#
cmake_minimum_required (VERSION 3.8)

project ("OpenCvSharp5")

# サブプロジェクトを含めます。
add_subdirectory ("OpenCvSharpExtern")
