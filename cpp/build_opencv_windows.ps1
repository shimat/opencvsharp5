function BuildForWindows($platform) {

    echo "### platform = $platform"

    if ($platform -eq "x64") {
        $msbuildPlatform = "x64"
    }
    else {
        $msbuildPlatform = "Win32"
    }
    $buildPath = "opencv/build_windows_${platform}"

    cmake `
        -S opencv `
        -B $buildPath `
        -G "Visual Studio 17 2022" `
        -A $msbuildPlatform `
        -D CMAKE_BUILD_TYPE=Release `
        -D BUILD_SHARED_LIBS=OFF `
        -D ENABLE_CXX11=1 `
        -D CMAKE_INSTALL_PREFIX=$buildPath/install `
        -D INSTALL_C_EXAMPLES=OFF `
        -D INSTALL_PYTHON_EXAMPLES=OFF `
        -D BUILD_DOCS=OFF `
        -D BUILD_WITH_DEBUG_INFO=OFF `
        -D BUILD_DOCS=OFF `
        -D BUILD_EXAMPLES=OFF `
        -D BUILD_TESTS=OFF `
        -D BUILD_PERF_TESTS=OFF `
        -D BUILD_JAVA=OFF `
        -D BUILD_WITH_DEBUG_INFO=OFF `
        -D BUILD_opencv_apps=OFF `
        -D BUILD_opencv_java_bindings_generator=OFF `
        -D BUILD_opencv_js=OFF `
        -D BUILD_opencv_js_bindings_generator=OFF `
        -D BUILD_opencv_objc_bindings_generator=OFF `
        -D BUILD_opencv_python_bindings_generator=OFF `
        -D BUILD_opencv_python_tests=OFF `
        -D BUILD_opencv_ts=OFF `
        -D BUILD_opencv_world=OFF `
        -D BUILD_LIST=core,imgproc,imgcodecs `
        -D WITH_QT=OFF `
        -D WITH_FREETYPE=OFF `
        -D WITH_ADE=OFF `
        -D WITH_FFMPEG=OFF `
        -D OPENCV_ENABLE_NONFREE=ON `
    
    cmake --build $buildPath --config Release --target INSTALL -- -maxcpucount 
}

# Entry point
If ((Resolve-Path -Path $MyInvocation.InvocationName).ProviderPath -eq $MyInvocation.MyCommand.Path) {

    ##### Change here #####
    $platform = "x64"
    #$platform = "x86"

    BuildForWindows $platform
}
