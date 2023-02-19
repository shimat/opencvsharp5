FROM ubuntu:22.04

ENV DEBIAN_FRONTEND=noninteractive
ENV OPENCV_VERSION=4.6.0

WORKDIR /
# Install opencv dependencies
RUN apt-get update && apt-get -y install --no-install-recommends \
      apt-transport-https \
      software-properties-common \
      curl \
      zip \
      unzip \
      tar \
      ca-certificates \
      g++ \
      make \
      cmake \
      git \
    && apt-get -y clean \
    && rm -rf /var/lib/apt/lists/*

# Setup opencv and opencv-contrib source
RUN curl -OL -q https://github.com/opencv/opencv/archive/${OPENCV_VERSION}.zip && \
    unzip -q ${OPENCV_VERSION}.zip && \
    rm ${OPENCV_VERSION}.zip && \
    mv opencv-${OPENCV_VERSION} opencv \
    && cd opencv && mkdir build && cd build && \
    cmake \
    -D CMAKE_BUILD_TYPE=RELEASE \
    -D BUILD_SHARED_LIBS=OFF \
    -D ENABLE_CXX11=ON \
    -D BUILD_EXAMPLES=OFF \
    -D BUILD_DOCS=OFF \
    -D BUILD_PERF_TESTS=OFF \
    -D BUILD_TESTS=OFF \
    -D BUILD_JAVA=OFF \
    -D BUILD_opencv_app=OFF \
    -D BUILD_opencv_java_bindings_generator=OFF \
    -D BUILD_opencv_js_bindings_generator=OFF \
    -D BUILD_opencv_python_bindings_generator=OFF \
    -D BUILD_opencv_python_tests=OFF \
    -D BUILD_opencv_ts=OFF \
    -D BUILD_opencv_js=OFF \
    -D BUILD_LIST=core,imgproc,ml \
    -D WITH_GSTREAMER=OFF \
    -D WITH_ADE=OFF \
    -D OPENCV_ENABLE_NONFREE=ON \
    .. \
    && make -j$(nproc) && make install && ldconfig \
    && rm -rf /opencv

RUN git clone https://github.com/microsoft/vcpkg \
    && /vcpkg/bootstrap-vcpkg.sh

COPY . .

RUN cmake \
    -S . \
    -B out-linux/build \
    -D CMAKE_TOOLCHAIN_FILE=/vcpkg/scripts/buildsystems/vcpkg.cmake