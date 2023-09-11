---
title: Launch screen snipping
description: This topic describes the ms-screenclip and ms-screensketch URI schemes. Your app can use these URI schemes to launch the Snip & Sketch app or to open a new snip.
ms.date: 08/09/2017
ms.topic: article
keywords: windows 10, uwp, uri, snip, sketch
ms.localizationpriority: medium
ms.custom: RS5
---
# Launch screen snipping

The **ms-screenclip:** and **ms-screensketch:** URI schemes allows you to initiate snipping or editing screenshots.

## Open a new snip from your app

The **ms-screenclip:** URI allows your app to automatically open up and start a new snip. The resulting snip is copied to the user's clipboard, but is not automatically passed back to the opening app.

**ms-screenclip:** takes the following parameters:

| Parameter | Type | Required | Description |
| --- | --- | --- | --- |
| source | string | no | A freeform string to indicate the source that launched the URI. |
| type | string | no | A string value to indicate which special type of capture is requested. This parameter can be omitted when starting a new snip. Values supported include: snapshot, recording* |
| clippingMode | string | no | A string value to indicate the clipping type for the snip. Values supported include: Rectangle, Freeform, Window |
| delayInSeconds | int | no | An integer value, from 1 to 30. Specifies the delay, in full seconds, between the URI call and when snipping begins. |
| callbackformat | string | no | This parameter is unavailable. |

\* `type=recording` is available only on Windows 11 PCs with Snipping Tool version 11.2307 or newer, and only when the default handler for ms-screenclip is set to "Snipping Tool" instead of "Screen Clipping".

## Launching the Snipping Tool or Snip & Sketch App

The **ms-screensketch:** URI allows you to programatically launch the Snipping Tool app (on Windows 11) or Snip & Sketch app (on Windows 10), and open a specific image in that app for annotation.

**ms-screensketch:** takes the following parameters:

| Parameter | Type | Required | Description |
| --- | --- | --- | --- |
| sharedAccessToken | string | no | A token identifying the file to open. Retrieved from [SharedStorageAccessManager.AddFile](/uwp/api/windows.applicationmodel.datatransfer.sharedstorageaccessmanager.addfile). If this parameter is omitted, the app will be launched without a file open. |
| secondarySharedAccessToken | string | no | A string identifying a JSON file with metadata about the snip. The metadata may include a **clipPoints** field with an array of x,y coordinates, and/or a [userActivity](/uwp/api/windows.applicationmodel.useractivities.useractivity). |
| source | string | no | A freeform string to indicate the source that launched the URI. |
| isTemporary | bool | no | If set to True, Screen Sketch will try to delete the file after opening it. |

The following example calls the [LaunchUriAsync](/uwp/api/Windows.System.Launcher#Windows_System_Launcher_LaunchUriAsync_Windows_Foundation_Uri_) method to send an image to Snipping Tool from the user's app.

```csharp

bool result = await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-screensketch:edit?source=MyApp&isTemporary=false&sharedAccessToken=2C37ADDA-B054-40B5-8B38-11CED1E1A2D"));

```

The following example illustrates what a file specified by the **secondarySharedAccessToken** parameter of **ms-screensketch** might contain:

```json
{
  "clipPoints": [
    {
      "x": 0,
      "y": 0
    },
    {
      "x": 2080,
      "y": 0
    },
    {
      "x": 2080,
      "y": 780
    },
    {
      "x": 0,
      "y": 780
    }
  ],
  "userActivity": "{\"$schema\":\"http://activity.windows.com/user-activity.json\",\"UserActivity\":\"type\",\"1.0\":\"version\",\"cross-platform-identifiers\":[{\"platform\":\"windows_universal\",\"application\":\"Microsoft.MicrosoftEdge_8wekyb3d8bbwe!MicrosoftEdge\"},{\"platform\":\"host\",\"application\":\"edge.activity.windows.com\"}],\"activationUrl\":\"microsoft-edge:https://support.microsoft.com/help/13776/windows-use-snipping-tool-to-capture-screenshots\",\"contentUrl\":\"https://support.microsoft.com/help/13776/windows-use-snipping-tool-to-capture-screenshots\",\"visualElements\":{\"attribution\":{\"iconUrl\":\"https://www.microsoft.com/favicon.ico?v2\",\"alternateText\":\"microsoft.com\"},\"description\":\"https://support.microsoft.com/help/13776/windows-use-snipping-tool-to-capture-screenshots\",\"backgroundColor\":\"#FF0078D7\",\"displayText\":\"Use snipping tool to capture screenshots - Windows Help\",\"content\":{\"$schema\":\"http://adaptivecards.io/schemas/adaptive-card.json\",\"type\":\"AdaptiveCard\",\"version\":\"1.0\",\"body\":[{\"type\":\"Container\",\"items\":[{\"type\":\"TextBlock\",\"text\":\"Use snipping tool to capture screenshots - Windows Help\",\"weight\":\"bolder\",\"size\":\"large\",\"wrap\":true,\"maxLines\":3},{\"type\":\"TextBlock\",\"text\":\"https://support.microsoft.com/help/13776/windows-use-snipping-tool-to-capture-screenshots\",\"size\":\"normal\",\"wrap\":true,\"maxLines\":3}]}]}},\"isRoamable\":true,\"appActivityId\":\"https://support.microsoft.com/help/13776/windows-use-snipping-tool-to-capture-screenshots\"}"
}

```
