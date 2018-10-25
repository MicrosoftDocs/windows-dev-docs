---
author: QuinnRadich
title: Launch screen snipping
description: This topic describes the ms-screenclip and ms-screensketch URI schemes. Your app can use these URI schemes to launch the Snip & Sketch app or to open a new snip.
ms.author: quradic
ms.date: 8/1/2017
ms.topic: article


keywords: windows 10, uwp, uri, snip, sketch
ms.localizationpriority: medium
---

# Launch screen snipping

The **ms-screenclip:** and **ms-screensketch:** URI schemes allows you to initiate snipping or editing screenshots.

## Open a new snip from your app

The **ms-screenclip:** URI allows your app to automatically open up and start a new snip. The resulting snip is copied to the user's clipboard, but is not automatically passed back to the opening app.

**ms-screenclip:** takes the following parameters:

| Parameter | Type | Required | Description |
| --- | --- | --- | --- |
| source | string | no | A freeform string to indicate the source that launched the URI. |
| delayInSeconds | int | no | An integer value, from 1 to 30. Specifies the delay, in full seconds, between the URI call and when snipping begins. |

## Launching the Snip & Sketch App

The **ms-screensketch:** URI allows you to programatically launch the Snip & Sketch app, and open a specific image in that app for annotation.

**ms-screensketch:** takes the following parameters:

| Parameter | Type | Required | Description |
| --- | --- | --- | --- |
| sharedAccessToken | string | no | A token identifying the file to open in the Snip & Sketch app. Retrieved from [SharedStorageAccessManager.AddFile](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer.sharedstorageaccessmanager.addfile). If this parameter is omitted, the app will be launched without a file open. |
| source | string | no | A freeform string to indicate the source that launched the URI. |
| isTemporary | bool | no | If set to True, Screen Sketch will try to delete the file after opening it. |

The following example calls the [LaunchUriAsync](https://docs.microsoft.com/uwp/api/Windows.System.Launcher#Windows_System_Launcher_LaunchUriAsync_Windows_Foundation_Uri_) method to send an image to Snip & Sketch from the user's app.

```csharp

bool result = await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-screensketch:edit?source=MyApp&isTemporary=false&sharedAccessToken=2C37ADDA-B054-40B5-8B38-11CED1E1A2D"));

```