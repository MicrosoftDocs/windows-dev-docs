---
title: Use app notifications with a WinForms app
description: Learn how to send a local app notification from a WinForms app and handle the user clicking the notification using the Windows App SDK.
ms.date: 04/08/2026
ms.topic: how-to
keywords: windows 11, windows 10, windows app sdk, winappsdk, winforms, windows forms, send app notifications, notifications, toast notifications, how to, quickstart, c#, csharp
ms.localizationpriority: medium
---

# Use app notifications with a WinForms app

An app notification is a UI popup that appears outside of your app's window, delivering timely information or actions to the user. Notifications can be purely informational, can launch your app when clicked, or can trigger a background action without bringing your app to the foreground.

:::image type="content" source="images/toast-notification.png" alt-text="Screenshot of an app notification":::

This article walks you through the steps to create and send an app notification from a WinForms app, and then handle activation when the user interacts with it. This article uses the [Windows App SDK](/windows/apps/windows-app-sdk/) `Microsoft.Windows.AppNotifications` APIs.

For an overview of app notifications and guidance for other frameworks, see [App notifications overview](index.md).

This article covers local notifications. For information about delivering notifications from a cloud service, see [Push notifications](../push-notifications/index.md).

> [!IMPORTANT]
> Notifications for elevated (admin) apps are not currently supported.

## Prerequisites

- A WinForms app targeting .NET 6 or later
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

In `Program.cs`, register for notifications *before* calling `Application.Run()`. You must register your [**NotificationInvoked**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.notificationinvoked) handler before calling [**Register**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.register).

**Program.cs**

```csharp
using Microsoft.Windows.AppNotifications;

namespace WinFormsNotifications;

static class Program
{
    [STAThread]
    static void Main()
    {
        // Register the notification handler before calling Register
        AppNotificationManager.Default.NotificationInvoked += OnNotificationInvoked;
        AppNotificationManager.Default.Register();

        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());

        // Unregister when the app exits
        AppNotificationManager.Default.Unregister();
    }

    private static void OnNotificationInvoked(
        AppNotificationManager sender,
        AppNotificationActivatedEventArgs args)
    {
        // NotificationInvoked is raised on a background thread,
        // so use Control.Invoke to marshal to the UI thread
        var form = Application.OpenForms.Count > 0 
            ? Application.OpenForms[0] as Form1 
            : null;

        form?.Invoke(() =>
        {
            // Parse args.Argument to determine what action to take.
            // args.Argument contains the arguments from the notification
            // or button that was clicked, as key=value pairs separated
            // by '&', for example "action=reply&conversationId=9813".
        });
    }
}
```

> [!NOTE]
> For unpackaged apps, `Register()` automatically sets up the COM server registration that allows Windows to launch your app when a notification is clicked. You don't need to configure COM activation or an AUMID manually.

## Send an app notification

Use [**AppNotificationBuilder**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder) to construct notification content and [**AppNotificationManager.Show**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.show) to send a notification from your form.

**Form1.cs**

```csharp
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;

private void SendNotification()
{
    var notification = new AppNotificationBuilder()
        .AddArgument("action", "viewConversation")
        .AddArgument("conversationId", "9813")
        .AddText("Andrew sent you a picture")
        .AddText("Check this out, The Enchantments in Washington!")
        .BuildNotification();

    AppNotificationManager.Default.Show(notification);
}
```

For information about adding buttons, images, inputs, and other rich content to your notifications, see [App notification content](app-notifications-content.md).

## Packaged app setup

For unpackaged WinForms apps, `Register()` handles COM registration automatically. For packaged apps (MSIX), you need to add extensions to your `Package.appxmanifest`. See [Packaged app setup](app-notifications-wpf.md#packaged-app-setup) in the WPF article for the required manifest entries.

## Related content

- [Quickstart: App notifications in the Windows App SDK](app-notifications-quickstart.md)
- [App notification content](app-notifications-content.md)
- [AppNotificationManager Class](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager)
- [AppNotificationBuilder Class](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder)




