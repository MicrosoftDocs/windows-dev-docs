---
title: Camera preview access with MediaCapture
description: Learn how to access the camera preview stream, handle exclusive control, and manage lifecycle events in a WinUI 3 desktop app.
ms.topic: how-to
ms.date: 07/08/2026
author: GrantMeStrength
ms.author: jken
---

# Camera preview access with MediaCapture

This article shows how to display the camera preview stream in a WinUI 3 desktop app, handle exclusive control scenarios, and manage app lifecycle events. For a quickstart that covers the basics of displaying a camera preview, see [Show the camera preview in a WinUI app](camera-quickstart-winui3.md).

## Display the camera preview

In WinUI 3, you use [MediaPlayerElement](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement) to display the camera preview. The UWP `CaptureElement` control is not available in WinUI 3.

Add a `MediaPlayerElement` to your XAML page:

```xaml
<MediaPlayerElement x:Name="PreviewElement"
                    AutoPlay="True"
                    HorizontalAlignment="Stretch" />
```

## Initialize MediaCapture

Create a [MediaCapture](/uwp/api/windows.media.capture.mediacapture) instance and initialize it with the desired settings. You can specify a particular camera device by setting [VideoDeviceId](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.videodeviceid).

```csharp
using Windows.Media.Capture;
using Windows.Devices.Enumeration;

private MediaCapture _mediaCapture;

private async Task InitializeCameraAsync()
{
    _mediaCapture = new MediaCapture();

    var settings = new MediaCaptureInitializationSettings
    {
        StreamingCaptureMode = StreamingCaptureMode.Video
    };

    // Optionally select a specific camera
    var devices = await DeviceInformation.FindAllAsync(
        DeviceClass.VideoCapture);

    if (devices.Count > 0)
    {
        settings.VideoDeviceId = devices[0].Id;
    }

    try
    {
        await _mediaCapture.InitializeAsync(settings);
    }
    catch (UnauthorizedAccessException)
    {
        // Camera access denied — prompt the user to enable camera
        // access in Windows Settings.
        return;
    }
}
```

## Start and stop the preview

After you initialize `MediaCapture`, connect the camera to the `MediaPlayerElement` for preview display. In WinUI 3, use a `MediaFrameSource` to feed frames to the player:

```csharp
using Windows.Media.Capture;
using Windows.Media.Capture.Frames;
using Microsoft.UI.Xaml.Controls;

private async Task StartPreviewAsync()
{
    // Get a frame source for the preview stream
    var frameSource = _mediaCapture.FrameSources.FirstOrDefault(
        source => source.Value.Info.MediaStreamType ==
            Windows.Media.Capture.MediaStreamType.VideoPreview
            || source.Value.Info.SourceKind ==
            MediaFrameSourceKind.Color);

    if (frameSource.Value != null)
    {
        var mediaSource = Windows.Media.Core.MediaSource
            .CreateFromMediaFrameSource(frameSource.Value);
        PreviewElement.Source = mediaSource;
    }
}

private async Task StopPreviewAsync()
{
    await _mediaCapture.StopPreviewAsync();
    PreviewElement.Source = null;
}
```

> [!NOTE]
> In WinUI 3, there is no `CaptureElement` control. Instead, connect `MediaCapture` to a `MediaPlayerElement` by creating a `MediaSource` from a `MediaFrameSource`. This gives you control over which frame source (front camera, rear camera, etc.) provides the preview. See [Show the camera preview in a WinUI app](camera-quickstart-winui3.md) for the recommended approach using the Windows Camera sample.

## Handle exclusive control

When another app takes exclusive control of the camera, your `MediaCapture` instance receives a [Failed](/uwp/api/windows.media.capture.mediacapture.failed) event. Register for this event, for example in your initialization method:

```csharp
_mediaCapture.Failed += MediaCapture_Failed;
```

Then clean up your preview when the event occurs:

```csharp
private void MediaCapture_Failed(MediaCapture sender,
    MediaCaptureFailedEventArgs errorEventArgs)
{
    DispatcherQueue.TryEnqueue(async () =>
    {
        await StopPreviewAsync();
        // Display an error message to the user
    });
}
```

> [!IMPORTANT]
> In WinUI 3 desktop apps, use `DispatcherQueue.TryEnqueue` to marshal calls back to the UI thread. The UWP `CoreDispatcher.RunAsync` method is not available.

## Handle window lifecycle events

In a WinUI 3 desktop app, the camera resource remains active even when the window is minimized or hidden. You should stop the preview when the window is not visible and restart it when the window becomes visible again. In your window constructor or initialization method, register for the `VisibilityChanged` event:

```csharp
this.VisibilityChanged += Window_VisibilityChanged;
```

Then implement the handler:

```csharp
private async void Window_VisibilityChanged(object sender,
    Microsoft.UI.Xaml.WindowVisibilityChangedEventArgs args)
{
    if (args.Visible)
    {
        await StartPreviewAsync();
    }
    else
    {
        await StopPreviewAsync();
    }
}
```

## Clean up resources

When your window closes, stop the preview and dispose of the `MediaCapture` instance. Register for the `Closed` event, for example in your window constructor:

```csharp
this.Closed += Window_Closed;
```

Then implement the handler:

```csharp
private async void Window_Closed(object sender,
    Microsoft.UI.Xaml.WindowEventArgs args)
{
    await StopPreviewAsync();
    _mediaCapture?.Dispose();
    _mediaCapture = null;
}
```

## Related content

- [Show the camera preview in a WinUI app](camera-quickstart-winui3.md)
- [Basic photo, video, and audio capture with MediaCapture](basic-photo-capture.md)
- [Camera](camera.md)
