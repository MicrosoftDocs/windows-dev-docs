---
title: "Quickstart: Send and Handle App Notifications"
description: Send and respond to local app notifications in a WinUI app using the Windows App SDK. Walk through creating notification content, handling foreground and background activation, and configuring the app manifest.
ms.topic: quickstart
ms.date: 07/15/2026
keywords: toast, local, notification, windows app sdk, winappsdk
ms.localizationpriority: medium
ms.custom: template-quickstart
---

# Quickstart: Use app notifications with the Windows App SDK

![A screen capture showing an app notification above the task bar. The notification is a reminder for an event. The app name, event name, event time, and event location are shown. A selection input displays the currently selected value, "Going". There are two buttons labeled "RSVP" and "Dismiss"](images/shell-1x.png)

In this quickstart, you'll create a WinUI app that sends and responds to local app notifications using the [Windows App SDK](../../../windows-app-sdk/index.md).

For complete sample apps that implement app notifications, see the [Windows App SDK Samples repo on GitHub](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Notifications/).

> [!IMPORTANT]
> App notifications aren't supported for elevated (admin) apps.

## Prerequisites

- Install Visual Studio 2026
    - [Download 2026 Community](https://c2rsetup.officeapps.live.com/c2r/downloadVS.aspx?sku=Community&channel=Release&Version=VS2026&source=VSLandingPage&add=Microsoft.VisualStudio.Workload.CoreEditor&add=Microsoft.VisualStudio.Workload.NetCrossPlat;includeRecommended&cid=2302)
    - [Download 2026 Professional](https://c2rsetup.officeapps.live.com/c2r/downloadVS.aspx?sku=Professional&channel=Release&Version=VS2026&source=VSLandingPage&add=Microsoft.VisualStudio.Workload.CoreEditor&add=Microsoft.VisualStudio.Workload.NetCrossPlat;includeRecommended&cid=2302)
    - [Download 2026 Enterprise](https://c2rsetup.officeapps.live.com/c2r/downloadVS.aspx?sku=Enterprise&channel=Release&Version=VS2026&source=VSLandingPage&add=Microsoft.VisualStudio.Workload.CoreEditor&add=Microsoft.VisualStudio.Workload.NetCrossPlat;includeRecommended&cid=2302)
- Include C++ workload for C++ or .NET workloads for C# development.
- Make sure that MSIX Packaging Tools under .NET desktop development is selected.
- Make sure Windows Application Development is selected.
- Make sure Windows UI Application Development is selected.

For more information about managing workloads in Visual Studio, see [Modify Visual Studio workloads, components, and language packs](/visualstudio/install/modify-visual-studio). For more information about getting started with WinUI, see [Get started with WinUI](../../../get-started/start-here.md). To add the Windows App SDK to an existing project, see [Use the Windows App SDK in an existing project](../../../windows-app-sdk/use-windows-app-sdk-in-existing-project.md).

## Create a new WinUI app project in Visual Studio

1. In Visual Studio, create a new project.
2. In the **Create a new project** dialog, set the language filter to "C#" or "C++" and the platform filter to "WinUI", then select the "Blank App, Packaged (WinUI 3 in Desktop)" project template.
3. Name the new project "AppNotificationsExample".

## Send a local app notification

In this section, you'll add a button to your app that sends a local app notification when clicked. The notification will include text content and an app logo image. You'll also add two read-only text boxes that will display the activation arguments when the user clicks on the notification.

First, add a **Button** control and two **TextBox** controls to your `MainWindow.xaml`:

#### [C#](#tab/cs)

```xaml
<!-- MainWindow.xaml -->
<Button x:Name="SendNotificationButton" Content="Send App Notification" Click="SendNotificationButton_Click"/>

<TextBlock Text="Activation arguments:" FontWeight="SemiBold" Margin="0,12,0,0"/>
<TextBox x:Name="ActionTextBox" Header="action" IsReadOnly="True" PlaceholderText="(none)"/>
<TextBox x:Name="ExampleEventIdTextBox" Header="exampleEventId" IsReadOnly="True" PlaceholderText="(none)"/>
```

#### [C++](#tab/cpp)

```xaml
<!-- MainWindow.xaml -->
<Button x:Name="SendNotificationButton" Content="Send App Notification" Click="SendNotificationButton_Click"/>

<TextBlock Text="Activation arguments:" FontWeight="SemiBold" Margin="0,12,0,0"/>
<TextBox x:Name="ActionTextBox" Header="action" IsReadOnly="True" PlaceholderText="(none)"/>
<TextBox x:Name="ExampleEventIdTextBox" Header="exampleEventId" IsReadOnly="True" PlaceholderText="(none)"/>
```

---

The app notification APIs are in the [Microsoft.Windows.AppNotifications](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications) and [Microsoft.Windows.AppNotifications.Builder](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder) namespaces. Add the following references to your project:

#### [C#](#tab/cs)

```csharp
// MainWindow.xaml.cs
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;
```

#### [C++](#tab/cpp)

Add the following include directives to your precompiled header file (`pch.h`):

```cpp
// pch.h
#include <winrt/Microsoft.Windows.AppNotifications.h>
#include <winrt/Microsoft.Windows.AppNotifications.Builder.h>
```

---

Now, add the following code to your button click handler. This example uses [AppNotificationBuilder](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder) to construct notification content, including arguments that will be passed back to the app when the user clicks the notification, an app logo image, and text. The notification also includes a button that demonstrates performing an action without launching the app's UI. The [BuildNotification](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder.buildnotification) method creates the [AppNotification](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotification) object, and [AppNotificationManager.Show](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.show) displays it to the user.

#### [C#](#tab/cs)

```csharp
// MainWindow.xaml.cs
private void SendNotificationButton_Click(object sender, RoutedEventArgs e)
{
    var appNotification = new AppNotificationBuilder()
        .AddArgument("action", "NotificationClick")
        .AddArgument("exampleEventId", "1234")
        .SetAppLogoOverride(new System.Uri("ms-appx:///Assets/Square150x150Logo.png"), AppNotificationImageCrop.Circle)
        .AddText("This is text content for an app notification.")
        .AddButton(new AppNotificationButton("Perform action without launching app")
            .AddArgument("action", "BackgroundAction"))
        .BuildNotification();

    AppNotificationManager.Default.Show(appNotification);
}
```

#### [C++](#tab/cpp)

```cpp
// MainWindow.xaml.cpp
void MainWindow::SendNotificationButton_Click(winrt::Windows::Foundation::IInspectable const&, winrt::Microsoft::UI::Xaml::RoutedEventArgs const&)
{
    auto appNotification{ AppNotificationBuilder()
        .AddArgument(L"action", L"NotificationClick")
        .AddArgument(L"exampleEventId", L"1234")
        .SetAppLogoOverride(winrt::Windows::Foundation::Uri(L"ms-appx:///Assets/Square150x150Logo.png"), AppNotificationImageCrop::Circle)
        .AddText(L"This is text content for an app notification.")
        .AddButton(AppNotificationButton(L"Perform action without launching app")
            .AddArgument(L"action", L"BackgroundAction"))
        .BuildNotification() };

    AppNotificationManager::Default().Show(appNotification);
}
```

---

At this point, you can build and run your app. Click the **Send App Notification** button to display the notification. Note that clicking the notification won't perform any action yet — in the next section, you'll learn how to handle app activation so your app can respond when a user clicks the notification.

> [!NOTE]
> App notifications are not supported when your app is running with administrator privileges (elevated). [**Show**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.show) will fail silently and no notification will be displayed. Make sure you run your app without elevation when testing notifications.

## Update the app package manifest file

The `Package.appmanifest` file provides the details of the MSIX package for an app. To enable your app to be launched when a user interacts with an app notification, you must update your app package manifest file so that your app is registered with the system as a target for app notification activation. For more information about app package manifests, see [App package manifest](/uwp/schemas/appxpackage/appx-package-manifest).

1. Edit the **Package.appxmanifest** file by right-clicking the file in Solution Explorer and selecting **View Code**.
1. Add `xmlns:com="http://schemas.microsoft.com/appx/manifest/com/windows10"` and `xmlns:desktop="http://schemas.microsoft.com/appx/manifest/desktop/windows10"` namespaces to `<Package>`.
1. Add a `<desktop:Extension>` element under `<Extensions>`. Set the `Category` attribute to `"windows.toastNotificationActivation"` to declare that your app can be activated by app notifications.
    - Add a `<desktop:ToastNotificationActivation>` child element and set the `ToastActivatorCLSID` to a GUID that will uniquely identify your app.
    - You can generate a GUID in Visual Studio by going to **Tools > Create GUID**.
1. Add a `<com:Extension>` element under `<Extensions>` and set the `Category` attribute to `"windows.comServer"`. The example manifest file shown below shows the syntax for this element.
    - Update the `Executable` attribute of the `<com:ExeServer>` element with your executable name. For this example, the name will be `"AppNotificationsExample.exe"`.
    - Specify `Arguments="----AppNotificationActivated:"` to ensure that Windows App SDK can process your notification's payload as an AppNotification kind.
    - Set the `Id` attribute of the `<com:Class>` element to the same GUID you used for the `ToastActivatorCLSID` attribute.

```xaml
<!--package.appxmanifest-->

<Package
  xmlns:com="http://schemas.microsoft.com/appx/manifest/com/windows10"
  xmlns:desktop="http://schemas.microsoft.com/appx/manifest/desktop/windows10"
  ...
  <Applications>
    <Application>
      ...
      <Extensions>

        <!--Specify which CLSID to activate when notification is clicked-->   
        <desktop:Extension Category="windows.toastNotificationActivation">
          <desktop:ToastNotificationActivation ToastActivatorCLSID="replaced-with-your-guid-C173E6ADF0C3" />
        </desktop:Extension>

        <!--Register COM CLSID-->    
        <com:Extension Category="windows.comServer">
          <com:ComServer>
            <com:ExeServer Executable="SampleApp.exe" DisplayName="SampleApp" Arguments="----AppNotificationActivated:">
              <com:Class Id="replaced-with-your-guid-C173E6ADF0C3" />
            </com:ExeServer>
          </com:ComServer>
        </com:Extension>
    
      </Extensions>
    </Application>
  </Applications>
 </Package>
```

## Handle activation from an app notification

When a user clicks on an app notification or a button within a notification, your app needs to respond appropriately. There are two common activation scenarios:

1. **Launch with UI** — The user clicks the notification body and your app should launch or come to the foreground, displaying relevant content.
2. **Background action** — The user clicks a button in the notification that triggers an action (such as sending a reply) without showing any app UI.

To support both scenarios, your app's activation flow should create the main window in `OnLaunched` but *not* activate it immediately. Instead, register the [AppNotificationManager.NotificationInvoked](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.notificationinvoked) event, call [AppNotificationManager.Register](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.register), and then check [AppInstance.GetActivatedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.getactivatedeventargs) to determine whether this is a normal launch or a COM activation path that should wait for `NotificationInvoked`. Your code can then decide whether to show the window or handle the action silently and exit.

The `NotificationInvoked` event handles clicks that occur while the app is already running. When the app is not running, Windows launches the app via COM activation and the activation kind is reported as `Launch`, not `AppNotification`. The notification arguments are then delivered through the `NotificationInvoked` event.

> [!IMPORTANT]
> You must call **AppNotificationManager.Register** before calling [AppInstance.GetActivatedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.getactivatedeventargs).

> [!IMPORTANT]
> Setting `activationType="background"` in the notification XML payload is ignored for desktop apps. You must process the activation arguments in your code and decide whether to display a window or not.

#### [C#](#tab/cs)

```csharp
// App.xaml.cs
using Microsoft.UI.Xaml;
using Microsoft.Windows.AppLifecycle;
using Microsoft.Windows.AppNotifications;

namespace AppNotificationsExample;

public partial class App : Application
{
    private Window? _window;

    public App()
    {
        InitializeComponent();
    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        _window = new MainWindow();

        AppNotificationManager.Default.NotificationInvoked += OnNotificationInvoked;
        AppNotificationManager.Default.Register();

        var activatedArgs = AppInstance.GetCurrent().GetActivatedEventArgs();

        if (activatedArgs.Kind == ExtendedActivationKind.AppNotification)
        {
            // App was launched by clicking a notification
            var notificationArgs = (AppNotificationActivatedEventArgs)activatedArgs.Data;
            HandleNotification(notificationArgs);
        }
        else
        {
            // Normal launch
            _window.Activate();
        }
    }

    private void OnNotificationInvoked(AppNotificationManager sender, AppNotificationActivatedEventArgs args)
    {
        // Notification clicked while app is already running
        HandleNotification(args);
    }

    private void HandleNotification(AppNotificationActivatedEventArgs args)
    {
        var action = args.Arguments.ContainsKey("action") ? args.Arguments["action"] : "(none)";
        var exampleEventId = args.Arguments.ContainsKey("exampleEventId") ? args.Arguments["exampleEventId"] : "(none)";

        _window!.DispatcherQueue.TryEnqueue(() =>
        {
            switch (action)
            {
                case "BackgroundAction":
                    // Handle the action without showing the app window.
                    // If the window was never shown, exit the app.
                    if (!_window.Visible)
                    {
                        Application.Current.Exit();
                    }
                    break;

                default:
                    // Bring the app to the foreground and display the notification arguments.
                    _window.Activate();
                    ((MainWindow)_window).UpdateNotificationUI(action, exampleEventId);
                    break;
            }
        });
    }
}
```

#### [C++](#tab/cpp)

Add the `HandleNotification` declaration to the `App` class in `App.xaml.h`:

```cpp
// App.xaml.h
#pragma once

#include "App.xaml.g.h"

namespace winrt::AppNotificationsExample::implementation
{
    struct App : AppT<App>
    {
        App();

        void OnLaunched(Microsoft::UI::Xaml::LaunchActivatedEventArgs const&);
        void OnNotificationInvoked(winrt::Microsoft::Windows::AppNotifications::AppNotificationManager const& sender, winrt::Microsoft::Windows::AppNotifications::AppNotificationActivatedEventArgs const& args);

    private:
        winrt::Microsoft::UI::Xaml::Window window{ nullptr };
        void HandleNotification(winrt::Microsoft::Windows::AppNotifications::AppNotificationActivatedEventArgs const& args);
    };
}
```

Update `App.xaml.cpp` with the activation flow. Add `#include <winrt/Microsoft.Windows.AppLifecycle.h>` to your `pch.h`.

```cpp
// App.xaml.cpp
#include "pch.h"
#include "App.xaml.h"
#include "MainWindow.xaml.h"

using namespace winrt;
using namespace Microsoft::UI::Xaml;
using namespace Microsoft::Windows::AppNotifications;
using namespace Microsoft::Windows::AppLifecycle;

namespace winrt::AppNotificationsExample::implementation
{
    App::App()
    {
        // ...
    }

    void App::OnLaunched([[maybe_unused]] LaunchActivatedEventArgs const& e)
    {
        window = make<MainWindow>();

        AppNotificationManager::Default().NotificationInvoked({ this, &App::OnNotificationInvoked });
        AppNotificationManager::Default().Register();

        auto activatedArgs = AppInstance::GetCurrent().GetActivatedEventArgs();

        if (activatedArgs.Kind() == ExtendedActivationKind::AppNotification)
        {
            // App was launched by clicking a notification
            auto notificationArgs = activatedArgs.Data().as<AppNotificationActivatedEventArgs>();
            HandleNotification(notificationArgs);
        }
        else
        {
            // Normal launch
            window.Activate();
        }
    }

    void App::OnNotificationInvoked(AppNotificationManager const&, AppNotificationActivatedEventArgs const& args)
    {
        // Notification clicked while app is already running
        HandleNotification(args);
    }

    void App::HandleNotification(AppNotificationActivatedEventArgs const& args)
    {
        auto userInput = args.Arguments();
        auto action = userInput.HasKey(L"action") ? userInput.Lookup(L"action") : L"(none)";
        auto exampleEventId = userInput.HasKey(L"exampleEventId") ? userInput.Lookup(L"exampleEventId") : L"(none)";

        window.DispatcherQueue().TryEnqueue([this, action, exampleEventId]()
        {
            if (action == L"BackgroundAction")
            {
                // Handle the action without showing the app window.
                // If the window was never shown, exit the app.
                if (!window.Visible())
                {
                    Application::Current().Exit();
                }
            }
            else
            {
                // Bring the app to the foreground and display the notification arguments.
                window.Activate();
                window.as<MainWindow>()->UpdateNotificationUI(action, exampleEventId);
            }
        });
    }
}
```

---

Add an `UpdateNotificationUI` method to `MainWindow` to display the notification arguments in the text boxes added earlier.

#### [C#](#tab/cs)

```csharp
// MainWindow.xaml.cs
public void UpdateNotificationUI(string action, string exampleEventId)
{
    DispatcherQueue.TryEnqueue(() =>
    {
        ActionTextBox.Text = action;
        ExampleEventIdTextBox.Text = exampleEventId;
    });
}
```

#### [C++](#tab/cpp)

Add the declaration to `MainWindow.xaml.h`:

```cpp
// MainWindow.xaml.h
void UpdateNotificationUI(winrt::hstring const& action, winrt::hstring const& exampleEventId);
```

Implement the method in `MainWindow.xaml.cpp`:

```cpp
// MainWindow.xaml.cpp
void MainWindow::UpdateNotificationUI(winrt::hstring const& action, winrt::hstring const& exampleEventId)
{
    DispatcherQueue().TryEnqueue([this, action, exampleEventId]()
    {
        ActionTextBox().Text(action);
        ExampleEventIdTextBox().Text(exampleEventId);
    });
}
```

---

## Next steps

- [App notification content](app-notifications-content.md) — learn how to add images, buttons, inputs, and other UI elements to your notifications.
- [Remove app notifications](manage-app-notifications.md) — learn how to tag, remove, and set expiration on your notifications.

## See also

- [App notifications overview](index.md)
- [App notification progress bar](app-notifications-progress-bar.md)
- [Notifications code sample on GitHub](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Notifications/)
- [Microsoft.Windows.AppNotifications API reference](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications)
- [Notifications XML schema](/uwp/schemas/tiles/toastschema/schema-root)
