---
author: drewbatgit
ms.assetid: c43d4af3-9a1a-4eae-a137-1267c293c1b5
description: This article show you how to take advantage of special camera UI features that are only present on mobile devices.
title: Camera UI features for mobile devices
ms.author: drewbat
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

#Camera UI features for mobile devices

This article show you how to take advantage of special camera UI features that are only present on mobile devices. 

## Add the mobile extension to your project 

To use these features, you must add a reference to the Microsoft Mobile Extension SDK for Universal App Platform to your project.

**To add a reference to the mobile extension SDK for hardware camera button support**

1.  In **Solution Explorer**, right-click **References** and select **Add Reference**.

2.  Expand the **Windows Universal** node and select **Extensions**.

3.  Select the **Microsoft Mobile Extension SDK for Universal App Platform** check box.

## Hide the status bar

Mobile devices have a [**StatusBar**](https://msdn.microsoft.com/library/windows/apps/dn633864) control that provides the user with status information about the device. This control takes up space on the screen that can interfere with the media capture UI. You can hide the status bar by calling [**HideAsync**](https://msdn.microsoft.com/library/windows/apps/dn610339), but you must make this call from within a conditional block where you use the [**ApiInformation.IsTypePresent**](https://msdn.microsoft.com/library/windows/apps/dn949016) method to determine if the API is available. This method will only return true on mobile devices that support the status bar. You should hide the status bar when your app launches or when you begin previewing from the camera.

[!code-cs[HideStatusBar](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetHideStatusBar)]

When your app is shutting down or when the user navigates away from the media capture page of your app, you can make the control visible again.

[!code-cs[ShowStatusBar](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetShowStatusBar)]

## Use the hardware camera button

Some mobile devices have a dedicated hardware camera button that some users prefer over an on-screen control. To be notified when the hardware camera button is pressed, register a handler for the [**HardwareButtons.CameraPressed**](https://msdn.microsoft.com/library/windows/apps/dn653805) event. Because this API is available on mobile devices only, you must again use the **IsTypePresent** to make sure the API is supported on the current device before attempting to access it.

[!code-cs[PhoneUsing](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetPhoneUsing)]

[!code-cs[RegisterCameraButtonHandler](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetRegisterCameraButtonHandler)]

In the handler for the **CameraPressed** event, you can initiate a photo capture.

[!code-cs[CameraPressed](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetCameraPressed)]

When your app is shutting down or the user moves away from the media capture page of your app, unregister the hardware button handler.

[!code-cs[UnregisterCameraButtonHandler](./code/BasicMediaCaptureWin10/cs/MainPage.xaml.cs#SnippetUnregisterCameraButtonHandler)]

## Related topics

* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md)





