---
title: Get a preview frame from the camera
description: Learn how to capture a single preview frame from the camera stream for image analysis or processing in a WinUI 3 desktop app.
ms.topic: how-to
ms.date: 07/08/2026
author: GrantMeStrength
ms.author: jken
---

# Get a preview frame from the camera

This article shows how to capture a single preview frame from the camera preview stream in a WinUI 3 desktop app. You can use preview frames for lightweight image analysis, such as barcode scanning or face detection, without recording a full photo.

## Prerequisites

This article assumes you have initialized a [MediaCapture](/uwp/api/Windows.Media.Capture.MediaCapture) instance and started a camera preview. See [Show the camera preview in a WinUI app](camera-quickstart-winui3.md) or [Camera preview access](simple-camera-preview-access.md) for details.

## Get a preview frame as a SoftwareBitmap

Call [GetPreviewFrameAsync](/uwp/api/windows.media.capture.mediacapture.getpreviewframeasync) to get a single frame from the preview stream. You can pass an empty [VideoFrame](/uwp/api/Windows.Media.VideoFrame) to receive the frame in the preview stream's current format, or pass a `VideoFrame` with a specific pixel format.

```csharp
using Windows.Media;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Graphics.Imaging;

private async Task<SoftwareBitmap> GetPreviewFrameAsSoftwareBitmapAsync()
{
    // Get the preview stream dimensions using VideoEncodingProperties
    var previewProperties =
        _mediaCapture.VideoDeviceController
            .GetMediaStreamProperties(MediaStreamType.VideoPreview)
            as VideoEncodingProperties;

    // Create a VideoFrame to receive the preview frame.
    // VideoFrame implements IDisposable — use a using statement.
    using var previewFrame = new VideoFrame(
        BitmapPixelFormat.Bgra8,
        (int)previewProperties.Width,
        (int)previewProperties.Height);

    await _mediaCapture.GetPreviewFrameAsync(previewFrame);

    // Return a copy of the SoftwareBitmap because the
    // VideoFrame is disposed when this method returns.
    return SoftwareBitmap.Copy(previewFrame.SoftwareBitmap);
}
```

You can also call `GetPreviewFrameAsync` without parameters to get a frame in the native format:

```csharp
var previewFrame = await _mediaCapture.GetPreviewFrameAsync();
SoftwareBitmap previewBitmap = previewFrame.SoftwareBitmap;
```

## Display the preview frame in your UI

To display the captured frame in an [Image](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.image) control, convert it to a [SoftwareBitmapSource](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.imaging.softwarebitmapsource):

```csharp
using Microsoft.UI.Xaml.Media.Imaging;

private async Task DisplayPreviewFrameAsync(
    SoftwareBitmap softwareBitmap)
{
    // SoftwareBitmapSource requires Bgra8 with premultiplied alpha
    if (softwareBitmap.BitmapPixelFormat != BitmapPixelFormat.Bgra8 ||
        softwareBitmap.BitmapAlphaMode != BitmapAlphaMode.Premultiplied)
    {
        softwareBitmap = SoftwareBitmap.Convert(
            softwareBitmap,
            BitmapPixelFormat.Bgra8,
            BitmapAlphaMode.Premultiplied);
    }

    var source = new SoftwareBitmapSource();
    await source.SetBitmapAsync(softwareBitmap);

    // Set the Image control's source on the UI thread
    DispatcherQueue.TryEnqueue(() =>
    {
        PreviewImage.Source = source;
    });
}
```

> [!IMPORTANT]
> In WinUI 3, `SoftwareBitmapSource` is in the `Microsoft.UI.Xaml.Media.Imaging` namespace, not `Windows.UI.Xaml.Media.Imaging`. Use `DispatcherQueue.TryEnqueue` to update the UI, not `CoreDispatcher.RunAsync`.

## Process the preview frame

After you obtain a `SoftwareBitmap`, you can process it with any image analysis library. For example, you can use Windows built-in APIs for face detection:

```csharp
using Windows.Media;

private async Task AnalyzePreviewFrameAsync()
{
    var previewFrame = await _mediaCapture.GetPreviewFrameAsync();
    SoftwareBitmap bitmap = previewFrame.SoftwareBitmap;

    if (bitmap != null)
    {
        // Use the bitmap for image analysis
        // For example, pass it to a face detector or
        // barcode scanner

        bitmap.Dispose();
    }

    previewFrame.Dispose();
}
```

> [!TIP]
> Dispose of the `VideoFrame` and `SoftwareBitmap` after processing to release the underlying memory buffers promptly.

## Related content

- [Camera preview access](simple-camera-preview-access.md)
- [Basic photo, video, and audio capture with MediaCapture](basic-photo-capture.md)
- [Process media frames with MediaFrameReader](process-media-frames-with-mediaframereader.md)
- [Camera](camera.md)
