# See here for image contents: https://github.com/microsoft/vscode-dev-containers/tree/v0.195.0/containers/cpp/.devcontainer/base.Dockerfile
FROM mcr.microsoft.com/devcontainers/cpp:ubuntu-22.04

# [Optional] Uncomment this section to install additional packages.
RUN apt-get update && DEBIAN_FRONTEND=noninteractive \
     && apt-get -y install --no-install-recommends \
        libtbb-dev \
        libatlas-base-dev \
        git

RUN git clone --depth 1 https://github.com/opencv/opencv -b 4.7.0
RUN cd opencv \
    && cmake \
        -S . \
        -B build \
        -D CMAKE_BUILD_TYPE=Release \
        -D BUILD_SHARED_LIBS=OFF \
        -D ENABLE_CXX11=ON \
        -D BUILD_EXAMPLES=OFF \
        -D BUILD_DOCS=OFF \
        -D BUILD_PERF_TESTS=OFF \
        -D BUILD_TESTS=OFF \
        -D BUILD_JAVA=OFF \
        -D BUILD_opencv_apps=OFF \
        -D BUILD_LIST=core,imgproc,imgcodecs \
        -D WITH_GSTREAMER=OFF \
        -D WITH_ADE=OFF \
        -D BUILD_WEBP=ON \
        -D WITH_WEBP=ON \
        -D WITH_PROTOBUF=OFF \
        -D WITH_QUIRC=OFF \
        -D OPENCV_ENABLE_NONFREE=ON \
    && cd build \
    && make -j2 \
    && make install \
    && ldconfig