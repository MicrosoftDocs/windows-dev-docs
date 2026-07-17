---
title: Windows notifications overview
description: Understand which Windows notification API to use for your app type—AppNotificationManager (Windows App SDK) vs ToastNotificationManager (WinRT/UWP)—and choose the right delivery method.
ms.topic: overview
ms.date: 03/19/2026
keywords: toast, notification, AppNotificationManager, ToastNotificationManager, WNS, push notification, Windows App SDK
ms.localizationpriority: medium
---

# Windows notifications overview

Windows provides several notification APIs across different SDK generations. If you're searching online for how to send a notification and finding conflicting examples, this page will help you pick the right API for your app.

## Which API should I use?

The answer depends on which SDK your app targets:

| App type | Recommended API | Namespace |
|---|---|---|
| WinUI 3 / Windows App SDK (new apps) | `AppNotificationManager` | `Microsoft.Windows.AppNotifications` |
| WPF, WinForms, or unpackaged Win32 | `AppNotificationManager` via NuGet | `Microsoft.Windows.AppNotifications` |
| UWP (existing apps, no migration planned) | `ToastNotificationManager` | `Windows.UI.Notifications` |

> [!IMPORTANT]
> Most Stack Overflow answers and older tutorials use `ToastNotificationManager` from the `Windows.UI.Notifications` namespace. This is the **UWP WinRT API**. It works in UWP apps and may work in some desktop scenarios, but it is not the recommended path for new Windows App SDK apps. Use `AppNotificationManager` for new development.

## Notifications API comparison

| Feature | `AppNotificationManager` (Windows App SDK) | `ToastNotificationManager` (WinRT) |
|---|---|---|
| **Recommended for** | WinUI 3, WPF, WinForms, unpackaged Win32 | UWP |
| **NuGet package** | `Microsoft.WindowsAppSDK` | None (inbox) |
| **Package identity required** | No (works packaged and unpackaged) | Required for some features |
| **Push integration** | `PushNotificationManager` (Windows App SDK) | WNS channel APIs (`Windows.Networking.PushNotifications`) |
| **Active development** | Yes | Maintenance only |

## Types of notifications

Once you've chosen the right API, decide how your notification will be delivered:

| Type | Description | Use when |
|---|---|---|
| **Local app notification** | Triggered directly by your app code while running | You want to alert the user of an in-app event |
| **Scheduled** | Set a future time for the notification to appear | Calendar reminders, alarms |
| **Push (WNS)** | Sent from your cloud service via Windows Push Notification Services | Chat messages, breaking news, real-time updates |
| **Badge** | Small overlay on the app's taskbar icon | Unread count, status indicator |

For a full breakdown of delivery methods, see [Choose a notification delivery method](choosing-a-notification-delivery-method.md).

## Next steps

**Building a WinUI 3 or Windows App SDK app?**
- [App notifications overview](app-notifications/index.md) — local and push app notifications using `AppNotificationManager`
- [App notifications quickstart](app-notifications/app-notifications-quickstart.md)
- [Push notifications overview](push-notifications/index.md) — WNS push using `PushNotificationManager`

**Building a WPF or WinForms app?**
- [App notifications quickstart](app-notifications/app-notifications-quickstart.md) — local toast notifications work packaged or unpackaged
- [Push notifications quickstart](push-notifications/push-quickstart.md) — WNS push supports a limited truly unpackaged path, but packaging (MSIX or packaged with external location) is required for background delivery and COM activation

> [!IMPORTANT]
> Windows App SDK push notifications require an [Azure account](https://azure.microsoft.com/pricing/purchase-options/azure-account) and a **Microsoft Entra ID app registration**. If your app is packaged, you also need to submit a Package Family Name (PFN) mapping request by email — allow for **up to one week** of processing time before launch. See the [push notifications quickstart](push-notifications/push-quickstart.md) for full prerequisites.

**Building or maintaining a UWP app?**
- [Send a local app notification from C++ UWP apps](app-notifications/app-notifications-quickstart.md)
- [Windows Push Notification Services (WNS) overview](push-notifications/wns-overview.md)

**Migrating a UWP app to Windows App SDK?**
- [Migrate toast notifications](../../windows-app-sdk/migrate-to-windows-app-sdk/guides/toast-notifications.md)
- [Migrate push notifications](../../windows-app-sdk/migrate-to-windows-app-sdk/guides/notifications.md)
