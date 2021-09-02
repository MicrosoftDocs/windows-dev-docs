---
title: 'Quickstart: Sending a toast notification from the desktop (Windows)'
TOCTitle: 'Quickstart: Sending a toast notification from the desktop'
ms:assetid: 1D20ED75-E24A-4e60-91AB-CFCBE902A68E
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Hh802768(v=VS.85)
ms:contentKeyID: 44080728
ms.date: 04/30/2018
mtps_version: v=VS.85
dev_langs:
- csharp
---

# Quickstart: Sending a toast notification from the desktop

This quickstart shows how to raise a toast notification from a desktop app.

## Prerequisites

  - Libraries
      - C++: Runtime.object.lib
      - C\#: Windows.Winmd
  - A shortcut to your app, with a [System.AppUserModel.ID](https://msdn.microsoft.com/en-us/library/dd391569\(v=vs.85\)), must be installed to the Start screen. Note, however, that it does not need to be pinned to the Start screen. For more information, see [How to enable desktop toast notifications through an AppUserModelID](hh802762\(v=vs.85\).md).
  - A version of Microsoft Visual Studio that supports at least Windows 8

## Instructions

### 1\. Create your toast content

**Note**  When you specify a toast template that includes an image, be aware that desktop apps can use only local images; web images are not supported. Also, the path to the local image file must be provided as an absolute (not relative) path.

 

``` csharp

// Get a toast XML template
XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText04);

// Fill in the text elements
XmlNodeList stringElements = toastXml.GetElementsByTagName("text");
for (int i = 0; i < stringElements.Length; i++)
{
    stringElements[i].AppendChild(toastXml.CreateTextNode("Line " + i));
}

// Specify the absolute path to an image
String imagePath = "file:///" + Path.GetFullPath("toastImageAndText.png");
XmlNodeList imageElements = toastXml.GetElementsByTagName("image");

ToastNotification toast = new ToastNotification(toastXml);
```

### 2\. Create and attach the event handlers

Register handlers for the toast events: Activated, Dismissed, and Failed. A desktop app must at least subscribe to the Activated event so that it can handle the expected activation of the app from the toast when the user selects it.

``` csharp

toast.Activated += ToastActivated;
toast.Dismissed += ToastDismissed;
toast.Failed += ToastFailed;
```

### 3\. Send the toast

**Important**  You must include the [AppUserModelID](https://msdn.microsoft.com/en-us/library/dd391569\(v=vs.85\)) of your app's shortcut on the Start screen each time that you call [**CreateToastNotifier**](https://msdn.microsoft.com/en-us/library/br208645\(v=vs.85\)). If you fail to do this, your toast will not be displayed.

 

``` csharp
ToastNotificationManager.CreateToastNotifier(appID).Show(toast);
```

### 4\. Handle the callbacks

Bring your app's window to the foreground if it receives an "activated" callback from the toast notification. When a user selects a toast, the expectation is that the app will be launched to a view related to the content of that toast..

## Summary and next steps

## Related topics

[Sending toast notifications from desktop apps sample](/samples/browse/?redirectedfrom=MSDN-samples)

[How to enable desktop toast notifications through an AppUserModelID](hh802762\(v=vs.85\).md)

[Toast notifications sample](/samples/microsoft/windows-universal-samples/notifications/)

[Toast XML schema](https://msdn.microsoft.com/en-us/library/br230849\(v=vs.85\))

[Toast notification overview](https://msdn.microsoft.com/en-us/library/hh779727\(v=vs.85\))

[Quickstart: Sending a toast notification](https://msdn.microsoft.com/en-us/library/hh465448\(v=vs.85\))

[Quickstart: Sending a toast push notification](https://msdn.microsoft.com/en-us/library/hh465450\(v=vs.85\))

[Guidelines and checklist for toast notifications](https://msdn.microsoft.com/en-us/library/hh465391\(v=vs.85\))

[How to add images to a toast template](https://msdn.microsoft.com/en-us/library/hh761480\(v=vs.85\))

[How to check toast notification settings](https://msdn.microsoft.com/en-us/library/hh761466\(v=vs.85\))

[How to choose and use a toast template](https://msdn.microsoft.com/en-us/library/hh465423\(v=vs.85\))

[How to handle activation from a toast notification](https://msdn.microsoft.com/en-us/library/hh761468\(v=vs.85\))

[How to opt in for toast notifications](https://msdn.microsoft.com/en-us/library/hh781238\(v=vs.85\))

[Choosing a toast template](https://msdn.microsoft.com/en-us/library/hh761494\(v=vs.85\))

[Toast audio options](https://msdn.microsoft.com/en-us/library/hh761492\(v=vs.85\))