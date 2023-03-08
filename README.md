# opencvsharp5
Experimental OpenCV5 wrapper for .NET

[![Windows](https://github.com/shimat/opencvsharp5/actions/workflows/windows.yml/badge.svg)](https://github.com/shimat/opencvsharp5/actions/workflows/windows.yml)
[![Ubuntu](https://github.com/shimat/opencvsharp5/actions/workflows/ubuntu.yml/badge.svg)](https://github.com/shimat/opencvsharp5/actions/workflows/ubuntu.yml)

# Concept
- I will try the new P/Invoke mechanism introduced in .NET 7.
- I makes only what is needed for me.
  - For me, OpenCvSharp is intended for use in server applications and does not require GUI, video input/output, etc.
  - I am not interested in running OpenCvSharp on Mac, Unity, MAUI, Raspberry Pi, UWP, etc.
- I don't necessarily care about compatibility with the old [shimat/opencvsharp](https://github.com/shimat/opencvsharp).

# Requirements
- .NET 7

# Target Platforms
- Windows x64
- Linux x64
