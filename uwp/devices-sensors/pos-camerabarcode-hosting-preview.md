---
title: Hosting Preview for Camera Barcode Scanner
description: Learn how to host a camera barcode scanner preview in your application
ms.date: 05/02/2018
ms.topic: article
keywords: windows 10, uwp, point of service, pos
ms.localizationpriority: medium
---
# Hosting a camera barcode scanner preview in your application
## Step 1: Setup your camera preview
The first step in adding a preview to your application for camera barcode scanner can be accomplished by following the instructions in the [Display the camera preview](../audio-video-camera/simple-camera-preview-access.md) topic.  Once you have completed this step, return to this topic for camera barcode scanner specific modifications.

## Step 2: Update capability declarations
To prevent your users from receiving the consent prompt for microphone you can exclude this from the capabilities listed in your app manifest.

1. In Microsoft Visual Studio, in **Solution Explorer**, open the designer for the application manifest by double-clicking the **package.appxmanifest** item.
2. Select the **Capabilities** tab.
3. Uncheck the box for **Microphone**

 ## Step 3: Add additional using directive for media capture

```Csharp
using Windows.Media.Capture;
```

## Step 4: Set up your MediaCapture initialization settings
The following example initializes the [**MediaCaptureInitializationSettings**](/uwp/api/windows.media.capture.mediacaptureinitializationsettings). 

```Csharp
 private void InitCaptureSettings()
{
    _captureInitSettings = new MediaCaptureInitializationSettings();
    _captureInitSettings.VideoDeviceId = BarcodeScanner.VideoDeviceId;
    _captureInitSettings.StreamingCaptureMode = StreamingCaptureMode.Video;
    _captureInitSettings.PhotoCaptureSource = PhotoCaptureSource.VideoPreview;
}
```
## Step 5: Associate your MediaCapture object with the camera barcode scanner
Replace the existing mediaCapture.InitializeAsync() in *StartPreviewAsync()* with the following:

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
> See [Display the camera preview](../audio-video-camera/simple-camera-preview-access.md#add-capability-declarations-to-the-app-manifest) for more advanced topics on hosting a camera preview in your application.

## See also

### Samples

- [Barcode scanner sample](https://github.com/microsoft/Windows-universal-samples/tree/master/Samples/BarcodeScanner)