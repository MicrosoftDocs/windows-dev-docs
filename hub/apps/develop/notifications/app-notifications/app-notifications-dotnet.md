---
title: Use app notifications with a .NET app
description: Learn how to send a local app notification from a .NET app and handle the user clicking the notification using the Windows App SDK.
ms.date: 04/08/2026
ms.topic: how-to
keywords: windows 11, windows 10, windows app sdk, winappsdk, wpf, send app notifications, notifications, toast notifications, how to, quickstart, c#, csharp
ms.localizationpriority: medium
zone_pivot_groups: dotnet-app-type
---

# Use app notifications with a .NET app

An app notification is a UI popup that appears outside of your app's window, delivering timely information or actions to the user. Notifications can be purely informational, can launch your app when clicked, or can trigger a background action without bringing your app to the foreground.

:::image type="content" source="images/toast-notification.png" alt-text="Screenshot of an app notification":::

This article walks you through the steps to create and send an app notification from a .NET app, and then handle activation when the user interacts with it. This article uses the [Windows App SDK](/windows/apps/windows-app-sdk/) `Microsoft.Windows.AppNotifications` APIs.

For an overview of app notifications and guidance for other frameworks, see [App notifications overview](index.md).

This article covers local notifications. For information about delivering notifications from a cloud service, see [Push notifications](../push-notifications/index.md).

> [!IMPORTANT]
> Notifications for elevated (admin) apps are not currently supported.

## Prerequisites

- A WPF or WinForms app project that targets .NET 6 or later.
- You must configure the project to:
    - [Call Windows Runtime APIs](../../../desktop/modernize/winrt-apis-desktop-apps.md)
    - [Use the Windows App SDK](../../../windows-app-sdk/use-windows-app-sdk-in-existing-project.md)

## Register for app notifications

:::zone pivot="wpf"

In your `App.xaml.cs`, register for notifications in the `Startup` event handler. You must register your [**NotificationInvoked**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.notificationinvoked) handler *before* calling [**Register**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.register).

First, update `App.xaml` to use a `Startup` event handler instead of `StartupUri`:

**App.xaml**

```xml
<Application x:Class="WpfNotifications.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             Startup="OnStartup">
</Application>
```

Then, implement the startup and notification handling logic:

**App.xaml.cs**

```csharp
using System.Windows;
using Microsoft.Windows.AppNotifications;

namespace WpfNotifications;

public partial class App : Application
{
    private void OnStartup(object sender, StartupEventArgs e)
    {
        // Register the notification handler before calling Register
        AppNotificationManager.Default.NotificationInvoked += OnNotificationInvoked;
        AppNotificationManager.Default.Register();

        // Show the main window
        var mainWindow = new MainWindow();
        mainWindow.Show();
    }

    private void OnNotificationInvoked(
        AppNotificationManager sender, 
        AppNotificationActivatedEventArgs args)
    {
        // NotificationInvoked is raised on a background thread,
        // so dispatch to the UI thread for any UI updates
        Current.Dispatcher.Invoke(() =>
        {
            // Parse args.Argument to determine what action to take.
            // args.Argument contains the arguments from the notification
            // or button that was clicked, as key=value pairs separated
            // by '&', for example "action=reply&conversationId=9813".
        });
    }

    protected override void OnExit(ExitEventArgs e)
    {
        AppNotificationManager.Default.Unregister();
        base.OnExit(e);
    }
}
```

:::zone-end

:::zone pivot="winforms"

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

:::zone-end

> [!IMPORTANT]
> You must call `Register` before calling `AppInstance.GetCurrent().GetActivatedEventArgs()`. The `NotificationInvoked` handler must be registered before `Register()` is called.

> [!NOTE]
> For unpackaged apps, `Register()` automatically sets up the COM server registration that allows Windows to launch your app when a notification is clicked. You don't need to configure COM activation or an AUMID manually.

## Send an app notification

Use [**AppNotificationBuilder**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder) to construct notification content and [**AppNotificationManager.Show**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.show) to send a notification.

:::zone pivot="wpf"

**MainWindow.xaml.cs**

:::zone-end

:::zone pivot="winforms"

**Form1.cs**

:::zone-end

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

For unpackaged .NET apps, `Register()` handles COM registration automatically. For packaged apps (MSIX), you need to add the following extensions to your `Package.appxmanifest`:

```xml
<Package
  xmlns:com="http://schemas.microsoft.com/appx/manifest/com/windows10"
  xmlns:desktop="http://schemas.microsoft.com/appx/manifest/desktop/windows10"
  IgnorableNamespaces="... com desktop">

  <Applications>
    <Application>
      <Extensions>

        <!--Specify which CLSID to activate when notification is clicked-->
        <desktop:Extension Category="windows.toastNotificationActivation">
          <desktop:ToastNotificationActivation 
            ToastActivatorCLSID="YOUR-GUID-HERE" />
        </desktop:Extension>

        <!--Register COM CLSID-->
        <com:Extension Category="windows.comServer">
          <com:ComServer>
            <com:ExeServer 
              Executable="YourApp.exe" 
              Arguments="----AppNotificationActivated:" 
              DisplayName="YourApp">
              <com:Class Id="YOUR-GUID-HERE" />
            </com:ExeServer>
          </com:ComServer>
        </com:Extension>

      </Extensions>
    </Application>
  </Applications>
</Package>
```

> [!IMPORTANT]
> The `Executable` attribute should contain only the executable file name (for example, `YourApp.exe`), not a subdirectory path.

## Related content

- [Quickstart: App notifications in the Windows App SDK](app-notifications-quickstart.md)
- [App notification content](app-notifications-content.md)
- [AppNotificationManager Class](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager)
- [AppNotificationBuilder Class](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder)




