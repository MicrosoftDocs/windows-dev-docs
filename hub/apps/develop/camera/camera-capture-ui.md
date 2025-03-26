---
title: Capture photos and video in a desktop app with the Windows built-in camera UI
description: Learn how to capture photos and video using the camera UI built into Windows.  
ms.topic: article
ms.date: 07/23/2024
ms.author: drewbat
author: drewbatgit
ms.localizationpriority: medium
#customer intent: As a developer, I want to access the camera in a Windows app using WinUI 3.
---

# Capture photos and video in a desktop app with the Windows built-in camera UI

This article describes how to use the [CameraCaptureUI](/windows/windows-app-sdk/api/winrt/microsoft.windows.media.capture) class to capture photos or videos by using the camera UI built into Windows. This feature allows your app to get a user-captured photo or video with just a few lines of code.

If you want to provide your own camera UI, or if your scenario requires more robust, low-level control of the capture operation, then you should use the [MediaCapture](/uwp/api/windows.media.capture.mediacapture) class, and implement your own capture experience. For more information, see [Basic photo, video, and audio capture with MediaCapture](basic-photo-capture).

Note that the **CameraCaptureUI** class in the [Microsoft.Windows.Media.Capture](/windows/windows-app-sdk/api/winrt/microsoft.windows.media.capture) namespace is not supported for UWP apps. For information on using the UWP version of this feature, see [Capture photos and video in a UWP app with the Windows built-in camera UI](/windows/uwp/audio-video-camera/capture-photos-and-video-with-cameracaptureui)

Create a new instance of **CameraCaptureUI**, passing in the [AppWindow.Id](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.id) property of your app window. The [PhotoSettings](/windows/windows-app-sdk/api/winrt/microsoft.windows.media.capture.cameracaptureui.photosettings) property allows you to specify some constraints on the captured photo, including the file format and maximum resolution and whether UI allows the user to crop the photo after it's captured. The [VideoSettings](/windows/windows-app-sdk/api/winrt/microsoft.windows.media.capture.cameracaptureui.videosettings) property provides similar properties for video capture, such as the maximum resolution and duration and whether the UI allows the user to trip the video after it's captured.

Call [CaptureFileAsync](/windows/windows-app-sdk/api/winrt/microsoft.windows.media.capture.cameracaptureui.capturefileasync) to launch the camera capture UI asynchronously. Use one of the values from the [CameraCaptureUIMode](/windows/windows-app-sdk/api/winrt/microsoft.windows.media.capture.cameracaptureuimode) to specify whether the UI should allow photo capture, video capture, or both. When **CaptureFileAsync** completes, it will return a [StorageFile](/uwp/api/windows.storage.storagefile) file object containing the captured photo or video. If the returned object is null it means that either the user cancelled the capture operation or an error occurred.

### [C#](#tab/csharp)

```csharp
    var cameraCaptureUI = new CameraCaptureUI(this.AppWindow.Id);
    cameraCaptureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Png;
    cameraCaptureUI.PhotoSettings.AllowCropping = false;

    // Capture a photo asynchronously
    StorageFile photo = await cameraCaptureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

    if (photo != null)
    {
        // Photo capture was successful
    } else
    {
        // Photo capture failed or was cancelled
    }
    CaptureVideo();
}
```

### [C++/WinRT](#tab/cpp)

```cpp
// Get the WindowId for the window
Microsoft::UI::WindowId windowId = this->AppWindow().Id();

// Initialize CameraCaptureUI with a window handle
winrt::Microsoft::Windows::Media::Capture::CameraCaptureUI cameraUI(windowId);

// Configure Photo Settings
cameraUI.PhotoSettings().Format(CameraCaptureUIPhotoFormat::Png);
cameraUI.PhotoSettings().AllowCropping(false);

// Capture a photo asynchronously
auto photo = co_await cameraUI.CaptureFileAsync(CameraCaptureUIMode::Photo);


if (photo != nullptr)
{
    // Photo capture was successful
}
else
{
    // Photo capture failed or was cancelled
}
```

---


