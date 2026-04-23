---
title: Capture photos and video in a desktop app with the Windows built-in camera UI
description: Learn how to capture photos and video using the camera UI built into Windows.  
ms.topic: how-to
ms.date: 07/23/2024
ms.author: drewbat
author: drewbatgit
ms.localizationpriority: medium
#customer intent: As a developer, I want to access the camera in a Windows app using WinUI 3.
---

# Capture photos and video in a desktop app with the Windows built-in camera UI

This article describes how to use the [CameraCaptureUI](/windows/windows-app-sdk/api/winrt/microsoft.windows.media.capture) class to capture photos or videos by using the camera UI built into Windows. This feature allows your app to get a user-captured photo or video with just a few lines of code.

If you want to provide your own camera UI, or if your scenario requires more robust, low-level control of the capture operation, then you should use the [MediaCapture](/uwp/api/windows.media.capture.mediacapture) class, and implement your own capture experience. For more information, see [Basic photo, video, and audio capture with MediaCapture](basic-photo-capture.md).

Note that the **CameraCaptureUI** class in the [Microsoft.Windows.Media.Capture](/windows/windows-app-sdk/api/winrt/microsoft.windows.media.capture) namespace is not supported for UWP apps. For information on using the UWP version of this feature, see [Capture photos and video in a UWP app with the Windows built-in camera UI](/windows/uwp/audio-video-camera/capture-photos-and-video-with-cameracaptureui)

## Use the CameraCaptureUI class to capture photos

Create a new instance of **CameraCaptureUI**, passing in the [AppWindow.Id](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.id) property of your app window. The [PhotoSettings](/windows/windows-app-sdk/api/winrt/microsoft.windows.media.capture.cameracaptureui.photosettings) property allows you to specify some constraints on the captured photo, including the file format and maximum resolution and whether UI allows the user to crop the photo after it's captured. The [VideoSettings](/windows/windows-app-sdk/api/winrt/microsoft.windows.media.capture.cameracaptureui.videosettings) property provides similar properties for video capture, such as the maximum resolution and duration and whether the UI allows the user to trim the video after it's captured.

Call [CaptureFileAsync](/windows/windows-app-sdk/api/winrt/microsoft.windows.media.capture.cameracaptureui.capturefileasync) to launch the camera capture UI asynchronously. Use one of the values from the [CameraCaptureUIMode](/windows/windows-app-sdk/api/winrt/microsoft.windows.media.capture.cameracaptureuimode) to specify whether the UI should allow photo capture, video capture, or both. When **CaptureFileAsync** completes, it will return a [StorageFile](/uwp/api/windows.storage.storagefile) file object containing the captured photo or video. If the returned object is null it means that either the user cancelled the capture operation or an error occurred.

The following example demonstrates launching the **CameraCaptureUI** for photo capture, specifying the image format as PNG and disabling cropping. In this example the captured photo is set as the source for an [Image](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.image) control.

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

    // Show the captured photo in a XAML Image control  
    using (IRandomAccessStream fileStream = await photo.OpenAsync(Windows.Storage.FileAccessMode.Read))
    {
        // Set the image source to the selected bitmap 
        BitmapImage bitmapImage = new BitmapImage();
        await bitmapImage.SetSourceAsync(fileStream);
        iCapturedImage.Source = bitmapImage;
    }
} else
{
    // Photo capture failed or was cancelled
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

    // Show the captured photo in a XAML Image control  
    IRandomAccessStream fileStream = co_await photo.OpenAsync(Windows::Storage::FileAccessMode::Read);
    BitmapImage bitmapImage = BitmapImage();
    co_await bitmapImage.SetSourceAsync(fileStream);
    iCapturedImage().Source(bitmapImage);

}
else
{
    // Photo capture failed or was cancelled
}
```

---

## Use the CameraCaptureUI class to capture videos

The following example demonstrates launching the **CameraCaptureUI** for video capture, specifying the maximum video as standard definition and disabling trimming. In this example the captured photo is set as the source for an [MediaPlayerElement](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement) control.

### [C#](#tab/csharp)

```csharp
var cameraCaptureUI = new CameraCaptureUI(this.AppWindow.Id);
cameraCaptureUI.VideoSettings.MaxResolution = CameraCaptureUIMaxVideoResolution.StandardDefinition;
cameraCaptureUI.VideoSettings.AllowTrimming = true;
StorageFile videoFile = await cameraCaptureUI.CaptureFileAsync(CameraCaptureUIMode.Video);

if (videoFile != null)
{
    // Video capture was successful

    // Show the captured video in a MediaPlayerElement control
    mediaPlayerElement.Source = MediaSource.CreateFromStorageFile(videoFile);
    mediaPlayerElement.MediaPlayer.Play();
}
else
{
    // Video capture failed or was cancelled
}
```

### [C++/WinRT](#tab/cpp)

```cpp
Microsoft::UI::WindowId windowId = this->AppWindow().Id();

winrt::Microsoft::Windows::Media::Capture::CameraCaptureUI cameraUI(windowId);

cameraUI.VideoSettings().MaxResolution(CameraCaptureUIMaxVideoResolution::StandardDefinition);
cameraUI.VideoSettings().AllowTrimming(true);
StorageFile videoFile = co_await cameraUI.CaptureFileAsync(CameraCaptureUIMode::Video);

if (videoFile != nullptr)
{
    // Video capture was successful

    // Show the captured video in a MediaPlayerElement control  
    auto source = winrt::Windows::Media::Core::MediaSource::CreateFromStorageFile(videoFile);
    mediaPlayerElement().Source(MediaSource::CreateFromStorageFile(videoFile));

}
else
{
    // Photo capture failed or was cancelled
}
```

---

## Move and rename captured media files

The **CameraCaptureUI** creates randomized names for captured media files, so you may want to rename and move captured files to keep them organized. The following example moves and renames a captured file.


### [C#](#tab/csharp)

```csharp
StorageFile photo = await cameraCaptureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

if (photo != null)
{
    // Move and rename captured photo
    StorageFolder destinationFolder =
    await ApplicationData.Current.LocalFolder.CreateFolderAsync("ProfilePhotoFolder",
        CreationCollisionOption.OpenIfExists);

    await photo.CopyAsync(destinationFolder, "ProfilePhoto.jpg", NameCollisionOption.ReplaceExisting);
    await photo.DeleteAsync();
}
```

### [C++/WinRT](#tab/cpp)

```cpp
auto photo = co_await cameraUI.CaptureFileAsync(CameraCaptureUIMode::Photo);


if (photo != nullptr)
{
    // Move and rename captured photo
    StorageFolder destinationFolder = co_await ApplicationData::Current().LocalFolder()
        .CreateFolderAsync(L"ProfilePhotoFolder", CreationCollisionOption::OpenIfExists);

    co_await photo.CopyAsync(destinationFolder, L"ProfilePhoto.jpg", NameCollisionOption::ReplaceExisting);
    co_await photo.DeleteAsync();
}
```

---
