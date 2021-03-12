---
description: Learn how to use custom audio on your toast notifications to let your app express your brand's unique sound effects.
title: Custom audio on toasts
label: Custom audio on toasts
template: detail.hbs
ms.date: 12/15/2017
ms.topic: article
keywords: windows 10, uwp, toast, custom audio, notification, audio, sound
ms.localizationpriority: medium
---
# Custom audio on toasts

Toast notifications can use custom audio, which lets your app express your brand's unique sound effects. For example, a messaging app can use their own messaging sound on their Toast notifications, so that the user can instantly know that they received a notification from the app, rather than hearing the generic notification sound.

## Install UWP Community Toolkit NuGet package

In order to create notifications via code, we strongly recommend using the UWP Community Toolkit Notifications library, which provides an object model for the notification XML content. You could manually construct the notification XML, but that is error-prone and messy. The Notifications library inside UWP Community Toolkit is built and maintained by the team that owns notifications at Microsoft.

Install [Microsoft.Toolkit.Uwp.Notifications](https://www.nuget.org/packages/Microsoft.Toolkit.Uwp.Notifications/) from NuGet.


## Add namespace declarations

```csharp
using Microsoft.Toolkit.Uwp.Notifications;
```


## Add the custom audio

Windows Mobile has always supported custom audio in Toast notifications. However, Desktop only added support for custom audio in Version 1511 (build 10586). If you send a Toast that contains custom audio to a Desktop device before Version 1511, the toast will be silent. Therefore, for Desktop pre-Version 1511, you should NOT include the custom audio in your Toast notification, so that the notification will at least use the default notification sound.

**Known Issue**: If you're using Desktop Version 1511, the custom toast audio will only work if your app is installed via the Store. That means you cannot locally test your custom audio on Desktop before submitting to the Store - but the audio will work fine once installed from the Store. We fixed this in the Anniversary Update, so that custom audio from your locally deployed app will work correctly.

```csharp
var contentBuilder = new ToastContentBuilder()
    .AddText("New message");

    
bool supportsCustomAudio = true;
 
// If we're running on Desktop before Version 1511, do NOT include custom audio
// since it was not supported until Version 1511, and would result in a silent toast.
if (AnalyticsInfo.VersionInfo.DeviceFamily.Equals("Windows.Desktop")
    && !ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 2))
{
    supportsCustomAudio = false;
}
 
if (supportsCustomAudio)
{
    contentBuilder.AddAudio(new Uri("ms-appx:///Assets/Audio/CustomToastAudio.m4a"));
}

// Send the toast
contentBuilder.Show();
```

Supported audio file types include...

- .aac
- .flac
- .m4a
- .mp3
- .wav
- .wma


## Send the notification

Sending a notification with audio is the same as sending a regular notification (just call the Show method). See [Send local toast](send-local-toast.md) to learn more.


## Related topics

- [Full code sample on GitHub](https://github.com/WindowsNotifications/quickstart-toast-with-custom-audio)
- [Send a local toast](send-local-toast.md)
- [Toast content documentation](adaptive-interactive-toasts.md)
