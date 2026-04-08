---
description: Learn how to use custom audio on your app notifications to let your app express your brand's unique sound effects.
title: Custom audio on app notifications
label: Custom audio on app notifications
template: detail.hbs
ms.date: 12/15/2017
ms.topic: article
keywords: windows 10, windows 11, uwp, windows app sdk, winappsdk, toast, custom audio, notification, audio, sound
ms.localizationpriority: medium
---
# Custom audio on app notifications

App notifications can use custom audio, which lets your app express your brand's unique sound effects. For example, a messaging app can use their own messaging sound on their app notifications, so that the user can instantly know that they received a notification from the app, rather than hearing the generic notification sound.

## Add namespace declarations

For Windows App SDK apps, use the `AppNotificationBuilder` from the `Microsoft.Windows.AppNotifications.Builder` namespace, which is included in the Windows App SDK — no additional NuGet packages are required.

```csharp
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;
```


## Add the custom audio

### [Windows App SDK](#tab/appsdk)

```csharp
var builder = new AppNotificationBuilder()
    .AddText("New message")
    .SetAudioUri(new Uri("ms-appx:///Assets/Audio/CustomToastAudio.m4a"));

var notification = builder.BuildNotification();
AppNotificationManager.Default.Show(notification);
```

### [XML](#tab/xml)

```xml
<toast>
  <visual>
    <binding template="ToastGeneric">
      <text>New message</text>
    </binding>
  </visual>
  <audio src="ms-appx:///Assets/Audio/CustomToastAudio.m4a"/>
</toast>
```

---

Supported audio file types include:

- .aac
- .flac
- .m4a
- .mp3
- .wav
- .wma

Supported audio file sources:

- ms-appx:///
- ms-resource

**Not** supported audio file sources:

 - ms-appdata
 - http://, https://
 - C:/, F:/, etc.


## Send the notification

Sending a notification with audio is the same as sending a regular notification. For Windows App SDK apps, use `AppNotificationManager.Default.Show()`. See [Send local toast](app-notifications-csharp-legacy.md) to learn more.


## Related topics

- [Full code sample on GitHub](https://github.com/WindowsNotifications/quickstart-toast-with-custom-audio)
- [Send a local toast](app-notifications-csharp-legacy.md)
- [Toast content documentation](adaptive-interactive-toasts.md)


