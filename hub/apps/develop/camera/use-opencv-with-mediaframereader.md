---
title: Use OpenCV with MediaFrameReader
description: Learn how to use the OpenCV library with a MediaFrameReader to process real-time camera frames in a WinUI 3 desktop app.
ms.topic: how-to
ms.date: 07/10/2026
author: GrantMeStrength
ms.author: jken
---

# Use OpenCV with MediaFrameReader

This article shows how to use the OpenCV (Open Source Computer Vision) library with a [MediaFrameReader](/uwp/api/Windows.Media.Capture.Frames.MediaFrameReader) to process camera frames in real time. This technique is useful for image analysis tasks such as edge detection, object tracking, and background subtraction in a WinUI 3 desktop app.

## Prerequisites

Before you begin, make sure you have:

- A WinUI 3 desktop project (packaged or unpackaged).
- A compatible webcam connected to your device.
- The **OpenCvSharp4** and **OpenCvSharp4.runtime.win** NuGet packages installed in your project.
- Camera access declared for your app, as described in [Declare camera access](#declare-camera-access).

To install OpenCV for .NET:

```console
dotnet add package OpenCvSharp4
dotnet add package OpenCvSharp4.runtime.win
```

### Declare camera access

Before your app can use `MediaFrameReader` to read frames from a webcam, you must declare that your app uses the camera. How you do this depends on whether your app is packaged.

- **Packaged apps**: Add the `webcam` device capability to `Package.appxmanifest`:

    ```xml
    <Capabilities>
        <DeviceCapability Name="webcam" />
    </Capabilities>
    ```

- **Unpackaged apps**: Unpackaged apps don't have a package manifest, so there's no capability to declare. Camera access for unpackaged apps is instead controlled by the **Let desktop apps access your camera** setting under **Settings** > **Privacy & security** > **Camera** on the user's device.

In both cases, the user can still deny camera access at the OS level. Check for access before you initialize the camera and handle the denied case gracefully, as described in [Handle the Windows camera privacy setting](/windows/apps/develop/camera/camera-privacy-setting).

## Set up the MediaFrameReader

First, create a `MediaCapture` instance and find a color video frame source. Then create a `MediaFrameReader` to receive frames:

```csharp
using System.Linq;
using Windows.Media.Capture;
using Windows.Media.Capture.Frames;
using Windows.Graphics.Imaging;

private MediaCapture _mediaCapture;
private MediaFrameReader _frameReader;

private async Task InitializeFrameReaderAsync()
{
    // Find a color video source group
    var sourceGroups =
        await MediaFrameSourceGroup.FindAllAsync();

    var selectedGroup = sourceGroups.FirstOrDefault(group =>
        group.SourceInfos.Any(info =>
            info.MediaStreamType == MediaStreamType.VideoPreview &&
            info.SourceKind == MediaFrameSourceKind.Color));

    if (selectedGroup == null)
    {
        // No suitable camera found
        return;
    }

    _mediaCapture = new MediaCapture();
    var settings = new MediaCaptureInitializationSettings
    {
        SourceGroup = selectedGroup,
        MemoryPreference = MediaCaptureMemoryPreference.Cpu,
        StreamingCaptureMode = StreamingCaptureMode.Video,
    };

    await _mediaCapture.InitializeAsync(settings);

    // Find the color video source
    var colorSource = _mediaCapture.FrameSources
        .Values.FirstOrDefault(source =>
            source.Info.SourceKind == MediaFrameSourceKind.Color);

    if (colorSource == null)
    {
        return;
    }

    _frameReader =
        await _mediaCapture.CreateFrameReaderAsync(colorSource);
    _frameReader.FrameArrived += FrameReader_FrameArrived;

    await _frameReader.StartAsync();
}
```

> [!IMPORTANT]
> Set `MemoryPreference` to `Cpu` so frames arrive as `SoftwareBitmap` objects that you can access directly from managed code.

## Process frames with OpenCV

In the `FrameArrived` event handler, get the `SoftwareBitmap` from the frame and convert it to an OpenCV `Mat` for processing. You need the [IMemoryBufferByteAccess](/windows/win32/api/windows.foundation/nn-windows-foundation-imemorybufferbyteaccess) COM interface to access the raw pixel data. Add this definition to your project:

```csharp
using System.Runtime.InteropServices;

[ComImport]
[System.Runtime.InteropServices.Guid("5B0D3235-4DBA-4D44-865E-8F1D0E4FD04D")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
unsafe interface IMemoryBufferByteAccess
{
    void GetBuffer(out byte* buffer, out uint capacity);
}
```

Then process the frames:

```csharp
using OpenCvSharp;

private void FrameReader_FrameArrived(
    MediaFrameReader sender,
    MediaFrameArrivedEventArgs args)
{
    using var frameRef =
        sender.TryAcquireLatestFrame();
    if (frameRef == null) return;

    var bitmap = frameRef.VideoMediaFrame?.SoftwareBitmap;
    if (bitmap == null) return;

    // Convert to Bgra8 format if needed
    SoftwareBitmap convertedBitmap = null;
    if (bitmap.BitmapPixelFormat != BitmapPixelFormat.Bgra8)
    {
        convertedBitmap = SoftwareBitmap.Convert(
            bitmap, BitmapPixelFormat.Bgra8);
        bitmap = convertedBitmap;
    }

    // Access the pixel buffer
    using var buffer = bitmap.LockBuffer(
        BitmapBufferAccessMode.Read);
    using var reference =
        buffer.CreateReference();

    // Get the buffer as a byte array via
    // IMemoryBufferByteAccess
    unsafe
    {
        ((IMemoryBufferByteAccess)reference).GetBuffer(
            out byte* dataInBytes,
            out uint capacity);

        // Use stride from the buffer plane description
        // to avoid corrupted images from padding
        var desc = buffer.GetPlaneDescription(0);
        using var mat = Mat.FromPixelData(
            desc.Height, desc.Width,
            MatType.CV_8UC4,
            (IntPtr)dataInBytes,
            desc.Stride);

        // Process the frame with OpenCV
        ProcessFrame(mat);
    }

    // Dispose the converted bitmap if we created one
    convertedBitmap?.Dispose();
}
```

> [!NOTE]
> The unsafe code block uses [IMemoryBufferByteAccess](/windows/win32/api/windows.foundation/nn-windows-foundation-imemorybufferbyteaccess) to get a direct pointer to the bitmap data. You need to enable unsafe code in your project settings by adding `<AllowUnsafeBlocks>true</AllowUnsafeBlocks>` to your `.csproj` file.

## Apply OpenCV operations

You can apply any OpenCV image processing operation to the `Mat`. Here's an example that applies Canny edge detection:

```csharp
private SoftwareBitmap _processedBitmap;

private void ProcessFrame(Mat inputMat)
{
    // Convert to grayscale for edge detection
    using var grayMat = new Mat();
    Cv2.CvtColor(inputMat, grayMat, ColorConversionCodes.BGRA2GRAY);

    // Apply Canny edge detection
    using var edges = new Mat();
    Cv2.Canny(grayMat, edges, 50, 200);

    // Convert back to BGRA for display
    using var outputMat = new Mat();
    Cv2.CvtColor(edges, outputMat, ColorConversionCodes.GRAY2BGRA);

    // Create a SoftwareBitmap from the processed Mat
    var processedBitmap = new SoftwareBitmap(
        BitmapPixelFormat.Bgra8,
        outputMat.Width,
        outputMat.Height,
        BitmapAlphaMode.Premultiplied);

    using var destBuffer = processedBitmap.LockBuffer(
        BitmapBufferAccessMode.Write);
    using var destRef = destBuffer.CreateReference();

    unsafe
    {
        ((IMemoryBufferByteAccess)destRef).GetBuffer(
            out byte* destBytes, out uint destCapacity);

        // Copy the processed data to the SoftwareBitmap row by row.
        // outputMat.Step() and desc.Stride can differ (for example,
        // because of OpenCV's own row alignment), so a single bulk
        // copy across the whole buffer can corrupt the image. Copying
        // one row at a time, using each buffer's own stride, avoids
        // that mismatch.
        var desc = destBuffer.GetPlaneDescription(0);
        int rowBytes = outputMat.Width * outputMat.ElemSize();
        byte* sourceRow = (byte*)outputMat.Data.ToPointer();
        byte* destRow = destBytes;

        for (int row = 0; row < outputMat.Height; row++)
        {
            System.Buffer.MemoryCopy(
                sourceRow,
                destRow,
                destCapacity - (uint)(row * desc.Stride),
                (uint)rowBytes);

            sourceRow += outputMat.Step();
            destRow += desc.Stride;
        }
    }

    // Update the UI with the processed frame
    DispatcherQueue.TryEnqueue(async () =>
    {
        var source =
            new Microsoft.UI.Xaml.Media.Imaging
                .SoftwareBitmapSource();
        await source.SetBitmapAsync(processedBitmap);
        ProcessedImage.Source = source;

        _processedBitmap?.Dispose();
        _processedBitmap = processedBitmap;
    });
}
```

Add an `Image` control to your XAML to display the processed frames:

```xml
<Image x:Name="ProcessedImage" Stretch="Uniform" />
```

## Clean up resources

Stop the frame reader and release resources when you're done:

```csharp
private async Task CleanupAsync()
{
    if (_frameReader != null)
    {
        _frameReader.FrameArrived -= FrameReader_FrameArrived;
        await _frameReader.StopAsync();
        _frameReader.Dispose();
    }

    _mediaCapture?.Dispose();
    _processedBitmap?.Dispose();
}
```

## Related content

- [Process SoftwareBitmaps with OpenCV](../media-authoring-processing/process-software-bitmaps-with-opencv.md)
- [Get a preview frame from the camera](get-a-preview-frame.md)
- [Camera](camera.md)
