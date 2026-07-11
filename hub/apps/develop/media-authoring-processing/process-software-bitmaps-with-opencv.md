---
title: Process SoftwareBitmaps with OpenCV
description: Learn how to create a C++/WinRT component that bridges SoftwareBitmap and OpenCV Mat for image processing in WinUI 3 apps.
ms.topic: how-to
ms.date: 07/08/2026
author: GrantMeStrength
ms.author: jken
---

# Process SoftwareBitmaps with OpenCV

This article shows how to create a native C++/WinRT Windows Runtime component that converts [SoftwareBitmap](/uwp/api/Windows.Graphics.Imaging.SoftwareBitmap) objects to the OpenCV `Mat` type. This lets you use OpenCV's extensive image processing algorithms on frames captured with the Windows camera APIs and display the results in your WinUI 3 app.

## Overview

The `SoftwareBitmap` class is the common image format used by Windows media APIs, while OpenCV uses the `Mat` class. To bridge these formats, you create a C++/WinRT runtime component that:

1. Accepts a `SoftwareBitmap` input.
2. Converts it to an OpenCV `Mat`.
3. Applies the desired image processing.
4. Returns the result as a `SoftwareBitmap`.

Because OpenCV is a native C++ library, you use a C++/WinRT Windows Runtime Component project to create the bridge. Your C# WinUI 3 app references this component.

## Set up the C++/WinRT component project

1. In Visual Studio, add a new **Windows Runtime Component (C++/WinRT)** project to your solution. Name it something like `OpenCVBridge`.

2. Download the OpenCV NuGet package by running the following command in the Package Manager Console targeting the bridge project:

   ```console
   Install-Package OpenCV.Windows -ProjectName OpenCVBridge
   ```

   Alternatively, download the OpenCV release from [opencv.org](https://opencv.org/) and configure your project's include and library paths manually.

3. In the bridge project's `pch.h`, add the OpenCV headers:

   ```cpp
   #include <opencv2/core.hpp>
   #include <opencv2/imgproc.hpp>
   #include <robuffer.h>
   #include <windows.foundation.h>
   ```

## Create the OpenCVHelper runtime class

Define a runtime class that provides methods to convert between `SoftwareBitmap` and OpenCV `Mat`. Create the IDL file `OpenCVHelper.idl`:

```cpp
// OpenCVHelper.idl
namespace OpenCVBridge
{
    runtimeclass OpenCVHelper
    {
        OpenCVHelper();
        void ProcessBitmap(
            Windows.Graphics.Imaging.SoftwareBitmap input,
            Windows.Graphics.Imaging.SoftwareBitmap output);
    }
}
```

## Implement the conversion

In `OpenCVHelper.cpp`, implement the conversion from `SoftwareBitmap` to `Mat` and back:

```cpp
#include "pch.h"
#include "OpenCVHelper.h"
#include "OpenCVHelper.g.cpp"
#include <opencv2/imgproc.hpp>

using namespace winrt;
using namespace Windows::Graphics::Imaging;

namespace winrt::OpenCVBridge::implementation
{
    void OpenCVHelper::ProcessBitmap(
        SoftwareBitmap const& input,
        SoftwareBitmap const& output)
    {
        // Lock the input buffer for reading
        auto inputBuffer = input.LockBuffer(
            BitmapBufferAccessMode::Read);
        auto inputRef = inputBuffer.CreateReference();

        uint8_t* inputData = nullptr;
        uint32_t inputSize = 0;
        winrt::check_hresult(
            inputRef.as<::Windows::Foundation::
                IMemoryBufferByteAccess>()->GetBuffer(
                    &inputData, &inputSize));

        auto inputDesc =
            inputBuffer.GetPlaneDescription(0);

        // Create a Mat from the input data (use Stride for correct row size)
        cv::Mat inputMat(
            inputDesc.Height,
            inputDesc.Width,
            CV_8UC4,
            inputData,
            inputDesc.Stride);

        // Lock the output buffer for writing
        auto outputBuffer = output.LockBuffer(
            BitmapBufferAccessMode::Write);
        auto outputRef = outputBuffer.CreateReference();

        uint8_t* outputData = nullptr;
        uint32_t outputSize = 0;
        winrt::check_hresult(
            outputRef.as<::Windows::Foundation::
                IMemoryBufferByteAccess>()->GetBuffer(
                    &outputData, &outputSize));

        auto outputDesc =
            outputBuffer.GetPlaneDescription(0);

        cv::Mat outputMat(
            outputDesc.Height,
            outputDesc.Width,
            CV_8UC4,
            outputData,
            outputDesc.Stride);

        // Apply image processing - example: blur
        cv::GaussianBlur(inputMat, outputMat, cv::Size(15, 15), 5);
    }
}
```

> [!NOTE]
> The code above uses `IMemoryBufferByteAccess` to access the raw pixel data. The input and output `SoftwareBitmap` objects must use the `Bgra8` pixel format. If frames from `MediaFrameReader` use a different format, convert them first with `SoftwareBitmap.Convert`.

## Use the component from C\#

In your C# WinUI 3 app, add a project reference to the `OpenCVBridge` component. Then call `ProcessBitmap` from your frame processing code:

```csharp
using OpenCVBridge;
using Windows.Graphics.Imaging;

private readonly OpenCVHelper _openCVHelper = new();

private void ProcessFrameWithOpenCV(
    SoftwareBitmap inputBitmap)
{
    // Ensure the bitmap is in Bgra8 format
    if (inputBitmap.BitmapPixelFormat != BitmapPixelFormat.Bgra8)
    {
        inputBitmap = SoftwareBitmap.Convert(
            inputBitmap, BitmapPixelFormat.Bgra8);
    }

    // Create an output bitmap with the same dimensions
    var outputBitmap = new SoftwareBitmap(
        BitmapPixelFormat.Bgra8,
        inputBitmap.PixelWidth,
        inputBitmap.PixelHeight,
        BitmapAlphaMode.Premultiplied);

    // Process with OpenCV
    _openCVHelper.ProcessBitmap(inputBitmap, outputBitmap);

    // Display the result
    DispatcherQueue.TryEnqueue(async () =>
    {
        var source =
            new Microsoft.UI.Xaml.Media.Imaging
                .SoftwareBitmapSource();
        await source.SetBitmapAsync(outputBitmap);
        OutputImage.Source = source;
    });
}
```

## Add more processing operations

You can extend the `OpenCVHelper` class with additional methods for specific operations. Update the IDL and implementation:

```cpp
// Add to OpenCVHelper.idl
void ApplyCannyEdges(
    Windows.Graphics.Imaging.SoftwareBitmap input,
    Windows.Graphics.Imaging.SoftwareBitmap output,
    Double threshold1,
    Double threshold2);
```

```cpp
// Implementation
void OpenCVHelper::ApplyCannyEdges(
    SoftwareBitmap const& input,
    SoftwareBitmap const& output,
    double threshold1,
    double threshold2)
{
    // ... lock buffers as above ...

    cv::Mat grayMat;
    cv::cvtColor(inputMat, grayMat, cv::COLOR_BGRA2GRAY);

    cv::Mat edges;
    cv::Canny(grayMat, edges, threshold1, threshold2);

    cv::cvtColor(edges, outputMat, cv::COLOR_GRAY2BGRA);
}
```

## Related content

- [Use OpenCV with MediaFrameReader](../camera/use-opencv-with-mediaframereader.md)
- [Get a preview frame from the camera](../camera/get-a-preview-frame.md)
- [Camera](../camera/camera.md)
