---
title: Use app notifications with a console app
description: Learn how to send a local app notification from a .NET console app and handle the user clicking the notification using the Windows App SDK.
ms.date: 04/08/2026
ms.topic: how-to
keywords: windows 11, windows 10, windows app sdk, winappsdk, console, send app notifications, notifications, toast notifications, how to, quickstart, c#, csharp
ms.localizationpriority: medium
---

# Use app notifications with a console app

An app notification is a UI popup that appears outside of your app's window, delivering timely information or actions to the user. Notifications can be purely informational, can launch your app when clicked, or can trigger a background action without bringing your app to the foreground.

:::image type="content" source="images/toast-notification.png" alt-text="Screenshot of an app notification":::

This article walks you through the steps to create and send an app notification from a .NET console app, and then handle activation when the user interacts with it. This article uses the [Windows App SDK](/windows/apps/windows-app-sdk/) `Microsoft.Windows.AppNotifications` APIs.

For an overview of app notifications and guidance for other frameworks, see [App notifications overview](index.md).

This article covers local notifications. For information about delivering notifications from a cloud service, see [Push notifications](../push-notifications/index.md).

> [!IMPORTANT]
> Notifications for elevated (admin) apps are not currently supported.

## Prerequisites

- A .NET console app targeting .NET 6 or later
- The [Windows App SDK](/windows/apps/windows-app-sdk/) NuGet package (`Microsoft.WindowsAppSDK`)

### Set up your project

In your project file (`.csproj`), make sure the `TargetFramework` includes a Windows target framework:

```xml
<TargetFramework>net9.0-windows10.0.19041.0</TargetFramework>
```

Add the Windows App SDK NuGet package:

```xml
<PackageReference Include="Microsoft.WindowsAppSDK" Version="1.7.250310001" />
```

For unpackaged apps, add:

```xml
<WindowsPackageType>None</WindowsPackageType>
```

## Register for app notifications

In your `Main` method, register the [**NotificationInvoked**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.notificationinvoked) handler *before* calling [**Register**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.register). The console app must remain running to receive activation callbacks when notifications are clicked.

**Program.cs**

```csharp
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;

// Register the notification handler before calling Register
AppNotificationManager.Default.NotificationInvoked += (sender, args) =>
{
    // Handle notification activation.
    // args.Argument contains the arguments from the notification
    // or button that was clicked, as key=value pairs separated
    // by '&', for example "action=acknowledge".
    Console.WriteLine($"Notification activated! Arguments: {args.Argument}");
};

AppNotificationManager.Default.Register();
```

> [!NOTE]
> For unpackaged apps, `Register()` automatically sets up the COM server registration that allows Windows to launch your app when a notification is clicked. You don't need to configure COM activation or an AUMID manually.

## Send an app notification

Use [**AppNotificationBuilder**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder) to construct notification content and [**AppNotificationManager.Show**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.show) to send a notification.

```csharp
var notification = new AppNotificationBuilder()
    .AddArgument("action", "viewItem")
    .AddText("Console Notification")
    .AddText("This was sent from a console app using Windows App SDK.")
    .AddButton(new AppNotificationButton("Acknowledge")
        .AddArgument("action", "acknowledge"))
    .BuildNotification();

AppNotificationManager.Default.Show(notification);
```

## Keep the app running

For the `NotificationInvoked` handler to be called, the console app must still be running when the user clicks the notification. If the app exits before the user interacts with the notification, the next click will cold-launch a new process.

```csharp
Console.WriteLine("Notification sent! Waiting for activation...");
Console.WriteLine("Press Enter to exit.");
Console.ReadLine();

// Unregister when the app exits
AppNotificationManager.Default.Unregister();
```

## Complete example

Here's a complete `Program.cs` that sends a notification and handles activation:

```csharp
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;

Console.WriteLine("Console App Notification Test");

// Step 1: Register for notification activation
AppNotificationManager.Default.NotificationInvoked += (sender, args) =>
{
    Console.WriteLine($"Notification activated! Arguments: {args.Argument}");
};

AppNotificationManager.Default.Register();

// Step 2: Send a notification
var notification = new AppNotificationBuilder()
    .AddArgument("action", "viewItem")
    .AddText("Console Notification")
    .AddText("This was sent from a console app using Windows App SDK.")
    .AddButton(new AppNotificationButton("Acknowledge")
        .AddArgument("action", "acknowledge"))
    .BuildNotification();

AppNotificationManager.Default.Show(notification);

// Step 3: Wait for user interaction
Console.WriteLine("Notification sent! Click it to test activation.");
Console.WriteLine("Press Enter to exit.");
Console.ReadLine();

AppNotificationManager.Default.Unregister();
```

## Related content

- [Quickstart: App notifications in the Windows App SDK](app-notifications-quickstart.md)
- [App notification content](app-notifications-content.md)
- [AppNotificationManager Class](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager)
- [AppNotificationBuilder Class](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder)




