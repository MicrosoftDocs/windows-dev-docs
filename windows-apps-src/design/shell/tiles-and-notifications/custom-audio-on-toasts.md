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

Install [Microsoft.Toolkit.Uwp.Notifications](https://www.nuget.org/packages/Microsoft.Toolkit.Uwp.Notifications/) from NuGet (we're using version 1.0.0 in this documentation).


## Add namespace declarations

`Windows.UI.Notifications` includes the Tile and Toast API's. `Microsoft.Toolkit.Uwp.Notifications` includes the Notifications library.

```csharp
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI.Notifications;
```


## Construct the notification

The toast notification content includes text and images, and also buttons and inputs. Please see [send local toast](send-local-toast.md) to see a full code snippet.

```csharp
ToastContent toastContent = new ToastContent()
{
    Visual = new ToastVisual()
    {
        ... (omitted)
    }
};
```


## Add the custom audio

Windows Mobile has always supported custom audio in Toast notifications. However, Desktop only added support for custom audio in Version 1511 (build 10586). If you send a Toast that contains custom audio to a Desktop device before Version 1511, the toast will be silent. Therefore, for Desktop pre-Version 1511, you should NOT include the custom audio in your Toast notification, so that the notification will at least use the default notification sound.

**Known Issue**: If you're using Desktop Version 1511, the custom toast audio will only work if your app is installed via the Store. That means you cannot locally test your custom audio on Desktop before submitting to the Store - but the audio will work fine once installed from the Store. We fixed this in the Anniversary Update, so that custom audio from your locally deployed app will work correctly.

```csharp
?
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
    toastContent.Audio = new ToastAudio()
    {
        Src = new Uri("ms-appx:///Assets/Audio/CustomToastAudio.m4a")
    };
}
```

Supported audio file types include...

- .aac
- .flac
- .m4a
- .mp3
- .wav
- .wma


## Send the notification

Now that your toast content is complete, sending the notification is quite simple.

```csharp
// Create the toast notification from the previous toast content
ToastNotification notification = new ToastNotification(toastContent.GetXml());
             
// And then send the toast
ToastNotificationManager.CreateToastNotifier().Show(notification);
```


## Related topics

- [Full code sample on GitHub](https://github.com/WindowsNotifications/quickstart-toast-with-custom-audio)
- [Send a local toast](send-local-toast.md)
- [Toast content documentation](adaptive-interactive-toasts.md)