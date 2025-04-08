---
title: Host a camera barcode scanner preview in a UWP application
description: Host a camera barcode scanner preview in a UWP application on Windows 10 Version 1803 or later.
ms.date: 05/04/2023
ms.topic: article

ms.localizationpriority: medium
---

# Host a camera barcode scanner preview in a UWP application

**Requires Windows 10 Version 1803 or later.**

This topic describes how to host a camera barcode scanner preview in a UWP application.

## Step 1: Setup your camera preview

See [Display the camera preview](../audio-video-camera/simple-camera-preview-access.md) for instructions on how to quickly display the camera preview stream within a XAML page in a Universal Windows Platform (UWP) app. When complete, return to this topic for camera barcode scanner specific modifications.

## Step 2: Edit the capability declarations in your app manifest

Edit the capability declarations in the app manifest to prevent users from receiving the microphone consent prompt.

1. In Microsoft Visual Studio, in **Solution Explorer**, open the designer for the application manifest by double-clicking the **package.appxmanifest** item.
2. Select the **Capabilities** tab.
3. Uncheck the box for **Microphone**.

## Step 3: Add a `using` directive to support media capture

```Csharp
using Windows.Media.Capture;
```

## Step 4: Set up your media capture initialization settings

The following snippet shows how to initialize a [**MediaCaptureInitializationSettings**](/uwp/api/windows.media.capture.mediacaptureinitializationsettings) object with the following settings:

- [BarcodeScanner.VideoDeviceId](/uwp/api/windows.devices.pointofservice.barcodescanner.videodeviceid)
- [StreamingCaptureMode.Video](/uwp/api/windows.media.capture.streamingcapturemode)
- [PhotoCaptureSource.VideoPreview](/uwp/api/windows.media.capture.photocapturesource)

```Csharp
 private void InitCaptureSettings()
{
    _captureInitSettings = new MediaCaptureInitializationSettings();
    _captureInitSettings.VideoDeviceId = BarcodeScanner.VideoDeviceId;
    _captureInitSettings.StreamingCaptureMode = StreamingCaptureMode.Video;
    _captureInitSettings.PhotoCaptureSource = PhotoCaptureSource.VideoPreview;
}
```

## Step 5: Associate the MediaCapture object with a camera barcode scanner

Replace the existing [**InitializeAsync**](/uwp/api/windows.media.capture.mediacapture.initializeasync) method of the [**MediaCapture**](/uwp/api/windows.media.capture.mediacapture) object in `StartPreviewAsync()` (see [Step 1: Setup your camera preview](#step-1-setup-your-camera-preview)) with the following:

```Csharp
try
    {
        mediaCapture = new MediaCapture();
        await mediaCapture.InitializeAsync(InitCaptureSettings());

        displayRequest.RequestActive();
        DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
    }
```

> [!TIP]
> See [Display the camera preview](../audio-video-camera/simple-camera-preview-access.md#add-capability-declarations-to-the-app-manifest) for more advanced topics on hosting a camera preview in your UWP application.

## See also

- [JustScanIt - Windows Store app](https://aka.ms/justscanit)
- [BarcodeScanner sample](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/BarcodeScanner)
