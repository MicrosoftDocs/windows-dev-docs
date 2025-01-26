---
title: Show the camera preview in a WinUI 3 app
description: Learn how to show the camera preview in a WinUI 3 app. 
ms.topic: article
ms.date: 06/14/2024
ms.author: drewbat
author: drewbatgit
ms.localizationpriority: medium
#customer intent: As a developer, I want to access the camera in a Windows app using WinUI 3.
---

# Show the camera preview in a WinUI 3 app

In this quickstart, you will learn how to create a basic WinUI 3 camera app that displays the camera preview. In a WinUI 3 app, you use the [MediaPlayerElement](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement) control in the [Microsot.UI.Xaml.Controls](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls) namespace to render the camera preview and the WinRT class [MediaCapture](/uwp/api/windows.media.capture.mediacapture) to access the device's camera preview stream. **MediaCapture** provides APIs for performing a wide range of camera-related tasks such as such as capturing photos and videos and configuring the camera's device driver. See the other articles in this section for details about other **MediaCapture** features.

The code in this walkthrough is adapted from the [MediaCapture WinUI 3 sample on github](https://github.com/microsoft/Windows-Camera/tree/master/Samples/MediaCaptureWinUI3). 

> [!TIP]
> For the UWP version of this article, see [Display the camera preview](/windows/uwp/audio-video-camera/simple-camera-preview-access) in the UWP documentation.

## Prerequisites

- Your device must have developer mode enabled. For more information see [Enable your device for development](/windows/apps/get-started/enable-your-device-for-development).
- Visual Studio 2022 or later with the **Windows application development** workload. 

## Create a new WinUI 3 app

In Visual Studio, create a new project. In the **Create a new project** dialog, set the language filter to "C#" and the platform filter to "Windows", then select the "Blank App, Packaged (WinUI 3 in desktop)" project template.


## Create the UI

The simple UI for this example includes a **MediaPlayerElement** control for displaying the camera preview, a [ComboBox](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.combobox) that allows you to select from the device's cameras, and buttons for initializing the **MediaCapture** class, starting and stopping the camera preview, and reseting the sample. We also include a [TextBlock](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textblock) for displaying status messages.

In your project's MainWindow.xml file, replace the default **StackPanel** control with the following XAML.

:::code language="xaml" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml" id="SnippetCameraWinUIXaml":::


## Update the MainWindow class definition

The rest of the code in this article will be added to the **MainWindow** class definition in your project's MainWindow.xaml.cs file. First, add a few class variables that will persist throughout the lifetime of the window. These variables include:

- A [DeviceInformationCollection](/uwp/api/windows.devices.enumeration.deviceinformationcollection) that will store a [DeviceInformation](/uwp/api/windows.devices.enumeration.deviceinformation) object for each available camera. The **DeviceInformation** object conveys information such as the unique identifier and the friendly name for the camera.
- A **MediaCapture** object that handles interactions with the selected camera's driver and allows you to retrieve the camera's video stream.
- A [MediaFrameSource](/uwp/api/windows.media.capture.frames.mediaframesource) object that represents a source of media frames, such as a video stream.
- A boolean to track when the camera preview is running. Some camera settings can't be changed while the preview is running, so it's a good practice to track the state of the camera preview.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCameraWinUIClassVars":::


## Populate the list of available cameras

Next we'll create a helper method to detect the cameras that are present on the current device and populate the **ComboBox** in the UI with the camera names, allowing the user to select a camera to preview.The [DeviceInformation.FindAllAsync](/uwp/api/windows.devices.enumeration.deviceinformation.findallasync) allows you to query for many different kinds of devices. We use [MediaDevice.GetVideoCaptureSelector](/uwp/api/windows.media.devices.mediadevice.getvideocaptureselector) to retrieve the identifier that specifies that we only want to retrieve video capture devices.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCameraWinUIPopulateCameraList":::

Add a call to this helper method to the **MainWindow** class constructor so that the **ComboBox** gets populated when the window loads.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCameraWinUIConstructor":::


## Initialize the MediaCapture object 

Initialize the **MediaCapture** object by calling [InitializeAsync](/uwp/api/windows.media.capture.mediacapture.initializeasync), passing in a [MediaCaptureInitializationSettings](/uwp/api/windows.media.capture.mediacaptureinitializationsettings) object containing the requested initialization parameters. There are a lot of optional initialization parameters that enable different scenarios. See the API reference page for the complete list. In this simple example we specify a few basic settings, including:

- The [VideoDeviceId](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.videodeviceid) property specifies the unique identifier of the camera that the **MediaCapture** will attach to. We get the device ID from the **DeviceInformationCollection**, using the selected index of the **ComboBox**.
- The [SharingMode](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.sharingmode) property specifies whether the app is requesting shared, read-only access to the camera, which allows you to view and capture from the video stream, or exclusive control of the camera, which allows you to change the camera configuration. Multiple apps can read from a camera simultaneously, but only one app at a time can have exclusive control.
- The [StreamingCaptureMode](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.streamingcapturemode) property specifies whether we want to capture video, audio, or audio and video.
- The [MediaCaptureMemoryPreference](/uwp/api/windows.media.capture.mediacaptureinitializationsettings.memorypreference) allows us to request to specifically use CPU memory for video frames. The value **Auto** lets the system use GPU memory if it's available.

Before initializing the **MediaCapture** object we call [AppCapability.CheckAccess](/uwp/api/windows.security.authorization.appcapabilityaccess.appcapability.checkaccess) method to determine if the user has denied our app access to the camera in Windows Settings.

> [!NOTE]
> Windows allows users to grant or deny access to the device's camera in the Windows Settings app, under **Privacy & Security -> Camera**. When initializing the capture device, apps should check whether they have access to the camera and handle the case where access is denied by the user. For more information, see [Handle the Windows camera privacy setting](/windows/uwp/audio-video-camera/camera-privacy-setting).

The **InitializeAsync** call is made from inside a **try** block so that we can recover if initialization fails. Apps should handle initialization failure gracefully. In this simple example, we'll just display an error message on failure.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCameraWinUIInitMediaCapture":::

## Initialize the camera preview

When the user clicks the start preview button, we will attempt to create a **MediaFrameSource** for a video stream from the camera device with which the **MediaCapture** object was initialized. The available frame sources are exposed by the [MediaCapture.FrameSources](/uwp/api/windows.media.capture.mediacapture.framesources) property.

 To find a frame source that is color video data, as opposed to a depth camera for example, we look for a frame source that has a [SourceKind](/uwp/api/windows.media.capture.frames.mediaframesourceinfo.sourcekind) of [Color](/uwp/api/windows.media.capture.frames.mediaframesourcekind). Some camera drivers provide a dedicated preview stream that is separate from the record stream. To get the preview video stream, we try to select a frame source that has a [MediaStreamType](/uwp/api/windows.media.capture.frames.mediaframesourceinfo.mediastreamtype) of [VideoPreview](/uwp/api/windows.media.capture.mediastreamtype). If no preview streams are found, we can get the record video stream by selecting a **MediaStreamType** of **VideoRecord**. If neither of these frame sources are available, then this capture device can't be used for video preview.

Once we have selected a frame source, we create a new [MediaPlayer](/uwp/api/windows.media.playback.mediaplayer) object that will be rendered by the **MediaPlayerElement** in our UI. We set the [Source](/uwp/api/windows.media.playback.mediaplayer.source) property of the **MediaPlayer** to a new [MediaSource](/uwp/api/windows.media.core.mediasource) object we create from our selected **MediaFrameSource**.

Call [Play](/uwp/api/windows.media.playback.mediaplayer.play) on the **MediaPlayer** object to begin rendering the video stream.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCameraWinUIStartPreview":::

Implement a handler for the [MediaFailed](/uwp/api/windows.media.playback.mediaplayer.mediafailed) event so that you can handle errors rendering the preview.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCameraWinUIMediaFailed":::

## Stop the camera preview

To stop the camera preview, call [Pause](/uwp/api/windows.media.playback.mediaplayer.pause) on the **MediaPlayer** object.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCameraWinUIStopPreview":::


## Reset the app

To make it easier to test out the sample app, add a method to reset the state of the app. Camera apps should always dispose of the camera and associated resources when the camera is no longer needed.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetCameraWinUIReset":::
