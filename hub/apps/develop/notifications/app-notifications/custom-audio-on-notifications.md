---
description: Learn how to use custom audio on your app notifications to let your app express your brand's unique sound effects.
title: Custom audio on app notifications
label: Custom audio on app notifications
template: detail.hbs
ms.date: 07/28/2025
ms.topic: article
keywords: windows 11, windows app sdk, winappsdk, notification, custom audio, sound
ms.localizationpriority: medium
---
# Custom audio on app notifications

App notifications can use custom audio, which lets your app express your brand's unique sound effects. For example, a messaging app can use its own messaging sound on app notifications so that the user can instantly know the notification came from that app, rather than hearing the generic notification sound.

For more information about app notifications, see [App notifications overview](index.md).

## Add custom audio

Use [**AppNotificationBuilder.SetAudioUri**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder.setaudiouri) to specify a custom audio file for your notification.

```csharp
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;

var notification = new AppNotificationBuilder()
    .AddText("New message")
    .SetAudioUri(new Uri("ms-appx:///Assets/Audio/CustomToastAudio.m4a"))
    .BuildNotification();

AppNotificationManager.Default.Show(notification);
```

Supported audio file types:

- .aac
- .flac
- .m4a
- .mp3
- .wav
- .wma

Supported audio file sources:

- ms-appx:///
- ms-resource

Unsupported file sources:

- ms-appdata
- http://, https://
- C:/, F:/, etc.

## See also

- [App notifications overview](index.md)