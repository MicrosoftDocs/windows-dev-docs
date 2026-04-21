---
description: Learn how to remove app notifications from Notification Center by tag and group, set expiration times, and control notification behavior on reboot with the Windows App SDK.
title: Remove and Manage App Notifications
label: Remove app notifications
template: detail.hbs
ms.date: 07/28/2025
ms.topic: how-to
keywords: windows 11, windows app sdk, winappsdk, notification, remove, delete, clear, expiration, expires on reboot, tag, group
ms.localizationpriority: medium
---
# Remove app notifications

After you send an app notification, you may need to remove it from Notification Center when it's no longer relevant, or control how long it persists. The [AppNotificationManager](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager) class provides methods for removing notifications, and the [AppNotification](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotification) class provides properties for controlling when notifications expire automatically.

For more information about app notifications, see [App notifications overview](index.md).

## Remove notifications from Notification Center

To remove specific notifications, first assign [**Tag**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotification.tag) and [**Group**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotification.group) values when you show them. A **Tag** identifies a specific notification, and a **Group** identifies a set of related notifications. For example, a messaging app could use the chat thread ID as the **Group** and the contact name as the **Tag**.

[**AppNotificationManager**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager) provides several methods for removing notifications from Notification Center:

| Method | Description |
|--------|-------------|
| [**RemoveByTagAsync**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.removebytagasync) | Removes all notifications with the specified tag. |
| [**RemoveByGroupAsync**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.removebygroupasync) | Removes all notifications in the specified group. |
| [**RemoveByTagAndGroupAsync**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.removebytagandgroupasync) | Removes all notifications with the specified tag and group. |
| [**RemoveByIdAsync**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.removebyidasync) | Removes the notification with the specified ID. |
| [**RemoveAllAsync**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.removeallasync) | Removes all notifications for the app. |

The following example shows how to tag notifications when sending them, then remove them later. In this scenario, a messaging app removes all notifications from a group chat after the user reads the conversation, and then removes all notifications from a specific contact after the user mutes them.

#### [C#](#tab/cs)

```csharp
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;

void SendNotification(string message, string contactTag, string chatGroup)
{
    var notification = new AppNotificationBuilder()
        .AddText(message)
        .BuildNotification();

    // Tag and group the notification so it can be removed later
    notification.Tag = contactTag;
    notification.Group = chatGroup;

    AppNotificationManager.Default.Show(notification);
}

// Remove all notifications from a specific group chat
async Task RemoveGroupChatNotifications(string chatGroup)
{
    await AppNotificationManager.Default.RemoveByGroupAsync(chatGroup);
}

// Remove all notifications from a specific contact across all groups
async Task RemoveContactNotifications(string contactTag)
{
    await AppNotificationManager.Default.RemoveByTagAsync(contactTag);
}

// Remove all notifications for the app
async Task RemoveAllNotifications()
{
    await AppNotificationManager.Default.RemoveAllAsync();
}
```

#### [C++](#tab/cpp)

```cpp
using namespace winrt::Microsoft::Windows::AppNotifications;
using namespace winrt::Microsoft::Windows::AppNotifications::Builder;

void SendNotification(winrt::hstring const& message, winrt::hstring const& contactTag, winrt::hstring const& chatGroup)
{
    auto notification{ AppNotificationBuilder()
        .AddText(message)
        .BuildNotification() };

    // Tag and group the notification so it can be removed later
    notification.Tag(contactTag);
    notification.Group(chatGroup);

    AppNotificationManager::Default().Show(notification);
}

// Remove all notifications from a specific group chat
winrt::Windows::Foundation::IAsyncAction RemoveGroupChatNotifications(winrt::hstring chatGroup)
{
    co_await AppNotificationManager::Default().RemoveByGroupAsync(chatGroup);
}

// Remove all notifications from a specific contact across all groups
winrt::Windows::Foundation::IAsyncAction RemoveContactNotifications(winrt::hstring contactTag)
{
    co_await AppNotificationManager::Default().RemoveByTagAsync(contactTag);
}

// Remove all notifications for the app
winrt::Windows::Foundation::IAsyncAction RemoveAllNotifications()
{
    co_await AppNotificationManager::Default().RemoveAllAsync();
}
```

---

## Set an expiration time

Set an expiration time on your notification by using the [**Expiration**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotification.expiration) property if the content is only relevant for a certain period of time. For example, a calendar app sending an event reminder should set the expiration to the end of the event.

> [!NOTE]
> The default and maximum expiration time for a notification is 3 days.

#### [C#](#tab/cs)

```csharp
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;

var notification = new AppNotificationBuilder()
    .AddText("Team standup in 15 minutes")
    .AddText("Conference Room B")
    .BuildNotification();

// Remove the notification from Notification Center after one hour
notification.Expiration = DateTimeOffset.Now.AddHours(1);

AppNotificationManager.Default.Show(notification);
```

#### [C++](#tab/cpp)

```cpp
using namespace winrt::Microsoft::Windows::AppNotifications;
using namespace winrt::Microsoft::Windows::AppNotifications::Builder;

auto notification{ AppNotificationBuilder()
    .AddText(L"Team standup in 15 minutes")
    .AddText(L"Conference Room B")
    .BuildNotification() };

// Remove the notification from Notification Center after one hour
auto expiration = winrt::clock::now() + std::chrono::hours(1);
notification.Expiration(expiration);

AppNotificationManager::Default().Show(notification);
```

---

## Expire notifications on reboot

Set the [**ExpiresOnReboot**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotification.expiresonreboot) property to `true` if you want a notification to be removed from Notification Center when the computer restarts. This is useful for time-sensitive notifications that are no longer meaningful after a reboot, such as an in-progress call or a temporary reminder.

#### [C#](#tab/cs)

```csharp
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;

var notification = new AppNotificationBuilder()
    .AddText("You're sharing your screen")
    .BuildNotification();

notification.ExpiresOnReboot = true;

AppNotificationManager.Default.Show(notification);
```

#### [C++](#tab/cpp)

```cpp
using namespace winrt::Microsoft::Windows::AppNotifications;
using namespace winrt::Microsoft::Windows::AppNotifications::Builder;

auto notification{ AppNotificationBuilder()
    .AddText(L"You're sharing your screen")
    .BuildNotification() };

notification.ExpiresOnReboot(true);

AppNotificationManager::Default().Show(notification);
```

---

## See also

- [App notifications overview](index.md)
- [Quickstart: App notifications](app-notifications-quickstart.md)
- [AppNotificationManager API reference](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager)
- [AppNotification API reference](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotification)
