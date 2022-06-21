---
description: This article explains how to launch the Windows Settings app directly to the camera settings page.
title: Launch the camera settings page
ms.date: 06/21/2022
ms.topic: article
keywords: windows 10, uwp
dev_langs:
- csharp
ms.localizationpriority: medium
---


# Launch the camera settings page

Windows defines URL schemes that allow apps to launch the Windows Settings app and display a particular settings page. This article explains how to launch the Windows Settings app directly to the camera settings page. For more information, see [Launch the Windows Settings app](/windows/uwp/launch-resume/launch-settings-app).

## The camera settings URL

Starting with Windows 11, Build 22000, the Uri `ms-settings:camera` launches the Windows Settings app and navigates to the camera settings page. Note that in previous versions of Windows, this same URL would launch the default camera application. In addition to the general camera settings page, you can append the query string parameter `cameraId` set to the symbolic link name, in escaped Uri format, to launch directly to the settings page for the associated camera.

In the following example, the [DeviceInformation](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) class to retrieve the symbolic link name for the first video capture device on the current machine, if one exists. Next, [LaunchUriAsync](/uwp/api/windows.system.launcher.launchuriasync) is called to launch the Windows Settings app. The `ms-settings:camera` Uri specifies that the camera settings page should be shown. The optional query string parameter `cameraId` is set to the symbolic link name for the camera, escaped with a call to [Url.EscapeDataString](/dotnet/api/system.uri.escapedatastring), to specify that the settings for the associated camera should be shown. 

```csharp
private async void cameraSettingsButton_click(object sender, RoutedEventArgs e)
{
    myButton.Content = "Clicked";

    var captureDevices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);
    if(captureDevices.Count() > 0)
    {
        var cameraSymbolicLink = captureDevices.First().Id;
        bool result = await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:camera?cameraId=" + Uri.EscapeDataString(cameraSymbolicLink)));
    }
}
```


## Related topics

* [Launch the Windows Settings app](/windows/uwp/launch-resume/launch-settings-app)