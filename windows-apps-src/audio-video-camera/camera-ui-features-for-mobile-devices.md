---
ms.assetid: c43d4af3-9a1a-4eae-a137-1267c293c1b5
description: This article show you how to take advantage of special camera UI features that are only present on mobile devices.
title: Camera UI features for mobile devices
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Camera UI features for mobile devices

This article show you how to take advantage of special camera UI features that are only present on mobile devices. 

## Add the mobile extension to your project 

To use these features, you must add a reference to the Microsoft Mobile Extension SDK for Universal App Platform to your project.

**To add a reference to the mobile extension SDK for hardware camera button support**

1.  In **Solution Explorer**, right-click **References** and select **Add Reference**.

2.  Expand the **Windows Universal** node and select **Extensions**.

3.  Select the **Microsoft Mobile Extension SDK for Universal App Platform** check box.

## Hide the status bar

Mobile devices have a [**StatusBar**](/uwp/api/Windows.UI.ViewManagement.StatusBar) control that provides the user with status information about the device. This control takes up space on the screen that can interfere with the media capture UI. You can hide the status bar by calling [**HideAsync**](/uwp/api/windows.ui.viewmanagement.statusbar.hideasync), but you must make this call from within a conditional block where you use the [**ApiInformation.IsTypePresent**](/uwp/api/windows.foundation.metadata.apiinformation.istypepresent) method to determine if the API is available. This method will only return true on mobile devices that support the status bar. You should hide the status bar when your app launches or when you begin previewing from the camera.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetHideStatusBar":::

When your app is shutting down or when the user navigates away from the media capture page of your app, you can make the control visible again.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetShowStatusBar":::

## Use the hardware camera button

Some mobile devices have a dedicated hardware camera button that some users prefer over an on-screen control. To be notified when the hardware camera button is pressed, register a handler for the [**HardwareButtons.CameraPressed**](/uwp/api/windows.phone.ui.input.hardwarebuttons.camerapressed) event. Because this API is available on mobile devices only, you must again use the **IsTypePresent** to make sure the API is supported on the current device before attempting to access it.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetPhoneUsing":::

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetRegisterCameraButtonHandler":::

In the handler for the **CameraPressed** event, you can initiate a photo capture.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetCameraPressed":::

When your app is shutting down or the user moves away from the media capture page of your app, unregister the hardware button handler.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/BasicMediaCaptureWin10/cs/MainPage.xaml.cs" id="SnippetUnregisterCameraButtonHandler":::

## Related topics

* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md)
