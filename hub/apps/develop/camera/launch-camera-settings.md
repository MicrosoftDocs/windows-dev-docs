---
description: Learn how to launch the Windows Settings app directly to the camera settings page.
title: Launch the camera settings page
ms.date: 11/26/2024
ms.topic: article
keywords: windows 10, winui 3
dev_langs:
- csharp
ms.localizationpriority: medium
---


# Launch the camera settings page

Windows defines a set of URIs that allow apps to launch the Windows Settings app and display a particular settings page. This article explains how to launch the Windows Settings app directly to the camera settings page and, optionally, navigate directly to the settings for a particular camera on the device. For more information, see [Launch the Windows Settings app](/windows/uwp/launch-resume/launch-settings-app).

## The camera settings URL

Starting with Windows 11, Build 22000, the URI `ms-settings:camera` launches the Windows Settings app and navigates to the camera settings page. Note that in previous versions of Windows, this same URI would launch the default camera application. In addition to the general camera settings page, you can append the query string parameter `cameraId` set to the symbolic link name, in escaped URI format, to launch directly to the settings page for the associated camera.

In the following example, the [DeviceInformation](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) class is used to retrieve the symbolic link name for the first video capture device on the current machine, if one exists. Next, [LaunchUriAsync](/uwp/api/windows.system.launcher.launchuriasync) is called to launch the Windows Settings app. The `ms-settings:camera` Uri specifies that the camera settings page should be shown. The optional query string parameter `cameraId` is set to the symbolic link name for the camera, escaped with a call to [Url.EscapeDataString](/dotnet/api/system.uri.escapedatastring), to specify that the settings for the associated camera should be shown. 


:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/camera-winui/CS/CameraWinUI/MainWindow.xaml.cs" id="SnippetLaunchCameraSettings":::


## Related topics

* [Launch the Windows Settings app](/windows/uwp/launch-resume/launch-settings-app)