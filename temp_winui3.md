---
title: 'Quickstart: Send local app notifications (Windows App SDK + WinUI 3 apps)'
description: Learn about sending and managing app notifications from a WinUI 3 app using the Windows App SDK. 
ms.topic: article
ms.date: 05/22/2023
keywords: toast, local, notification, app notification, windows app sdk, winappsdk
ms.author: drewbat
author: drewbatgit
ms.localizationpriority: medium
ms.custom: template-quickstart
---

# Quickstart: Send local app notifications (Windows App SDK + WinUI 3 apps)

In this quickstart, you will create a desktop Windows application that sends and handles user interactions with local app notifications from a WinUI 3 app using the [Windows App SDK](../../../windows-app-sdk/index.md).

![A screen capture showing an app notification above the task bar. The notification is a reminder for an event. The app name, event name, event time, and event location are shown. A selection input displays the currently selected value, "Going". There are two buttons labeled "RSVP" and "Dismiss"](../../../design/images/shell-1x.png)

> [!NOTE]
> The term "toast notification" is being replaced with "app notification". These terms both refer to the same feature of Windows, but over time we will phase out the use of "toast notification" in the documentation.

> [!IMPORTANT]
> Notifications for an elevated (admin) app is currently not supported.

## Prerequisites

- Install the required tools for developing Windows App SDK apps by following the steps in [Set up your development environment](../../../windows-app-sdk/set-up-your-development-environment.md).

## Step 1: Create a new project

### [C#](#tab/cs)

Launch Visual Studio. In the **Create New Project** dialog, select C# from the language drop down and then select **Blank App, Packaged (WinUI 3 in Desktop)**. 

The Windows App SDK is referenced by default in the Packaged WinUI 3 project template. For information on Windows App SDK support for different project types, see [Create a new project that uses the Windows App SDK](../../../winui/winui3/create-your-first-winui3-app.md). For information on migrating existing projects to use Windows App SDK, see [Use the Windows App SDK in an existing project](../../use-windows-app-sdk-in-existing-project.md)

### [C++](#tab/cpp)

Launch Visual Studio. In the **Create New Project** dialog, select C++ from the language drop down and then select **Blank App, Packaged (WinUI 3 in Desktop)**. 

The Windows App SDK is referenced by default in the Packaged WinUI 3 project template. For information on Windows App SDK support for different project types, see [Create a new project that uses the Windows App SDK](../../../winui/winui3/create-your-first-winui3-app.md). For information on migrating existing projects to use Windows App SDK, see [Use the Windows App SDK in an existing project](../../use-windows-app-sdk-in-existing-project.md)

---

## Step 2: Reference app notification namespaces

The [Microsoft.Windows.AppNotifications](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications) namespace provides access to the [AppNotificationManager](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager) class that is the primary API for app notification tasks. [Microsoft.Windows.AppNotifications.Builder](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder) namespace provides access to the [AppNotificationBuilder](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder) class which makes it easy to build the XML fragment that defines the UI of an app notification. Add references to these namespaces to your project.

### [C#](#tab/cs)

Add a using directive for the app notifications namespaces to the top of `MainWindow.xaml.cs`

```csharp
\\ MainWindow.xaml.cs
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;
```

### [C++](#tab/cpp)

Include the header files for the app notifications in your precompiled header file, `pch.h`. 

```cpp
\\ phc.h
#include <winrt/Windows.Foundation.Collections.h>
#include <winrt/Windows.ApplicationModel.Activation.h>
```

Add using directives for the app notifications namespaces to the top of `MainWindow.xaml.cpp`.

```cpp
\\ MainWindow.xaml.cpp
using namespace Microsoft::Windows::AppNotifications;
using namespace Microsoft::Windows::AppNotifications::Builder;
```

---

## Step 3: Send a local app notification

App notifications are defined using a simple XML syntax which is described in detail in the article [App notification content](/windows/apps/design/shell/tiles-and-notifications/adaptive-interactive-toasts). The Windows App SDK provides the [AppNotificationBuilder](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder) class, which makes it simple to generate the app notification XML payload programmatically. 

To generate an app notification, create a new instance of **AppNotificationBuilder**. This class has methods that can be chained together to add elements to a notification. The elements used in this example include the following:

- [AddArgument](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder.addargument) adds a key/value pair that are passed as argument to your app when the user clicks on the notification.
- [AddText](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder.addtext) adds a text element.
- [AddTextBox](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder.addtextbox) adds a text box that the user can type in.
- [AddButton](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder.addbutton) adds a button to the notification.

After adding the elements to the notification, call [BuildNotification](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder.buildnotification) to create an [AppNotification](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotification) object.

Use the [Default](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.default) property to get the default instance of [AppNotificationManager](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager) for your app and then call [Show](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.show) to show the notification.

Paste the following code into `MainWindow.xaml.cs`, replacing the button handler that is included in the project template.

### [C#](#tab/cs)

Paste the following code into `MainWindow.xaml.cs`, replacing the button handler that is included in the project template.

```csharp
private void myButton_Click(object sender, RoutedEventArgs e)
{
    var notification = new AppNotificationBuilder()
        .AddArgument("action", "viewConversation")
        .AddArgument("conversationId", "9813")
        .AddText("Matt sent you a message")
        .AddTextBox("textBox", "Type reply", "Message")
        .AddButton(
            new AppNotificationButton("Reply")
                .AddArgument("action", "sendMessage"))
        .BuildNotification();

    AppNotificationManager.Default.Show(notification);
}
```

### [C++](#tab/cpp)

Paste the following code into `MainWindow.xaml.cpp`, replacing the button handler that is included in the project template.

```cpp
void MainWindow::myButton_Click(IInspectable const&, RoutedEventArgs const&)
{

    auto appNotification{ AppNotificationBuilder()
    .AddArgument(L"action", L"viewConversation")
    .AddArgument(L"conversationId", L"9813")
    .AddText(L"Matt sent you a message")
    .AddTextBox(L"textBox", L"Type reply", L"Message")
    .AddButton(AppNotificationButton(L"Reply")
        .AddArgument(L"action", L"sendMessage"))
    .BuildNotification() };

    AppNotificationManager::Default().Show(appNotification);
}
```

---

Build and run the app. When you click the button in the app, you will see the app notification launch.

:::image type="content" source="images/app-notitfication-quickstart-notification.png" alt-text="A screenshot of an app notification":::

> [!NOTE]
> If your app is packaged (including packaged with external location), then your app's icon in the notification's upper left corner is sourced from the `package.manifest`. If your app is unpackaged, then the icon is sourced by first looking into the shortcut, then looking at the resource file in the app process. If all attempts fail, then the Windows default app icon is used. The supported icon file types are `.jpg`, `.png`, `.bmp`, and `.ico`.


## Step 4: Update your app's manifest to support activation from an app notification

If your app is packaged, including packaged with external location, you need to update your app manifest file to register your app with the system so that it can be activated when the user clicks on an app notification. The first step in handling activation is to update your app manifest. If your app is unpackaged (that is, it lacks package identity at runtime), then skip to **Step 5: Register to handle an app notification**. For information about creating unpackaged apps with Windows App SDK and WinUI 3, see [Create your first WinUI 3 (Windows App SDK) project](/windows/apps/winui/winui3/create-your-first-winui3-app).

> [!IMPORTANT]
> Warning: If you define a [Windows.Protocol](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-protocol) app extensibility type in your appx manifest with `<uap:Protocol>`, then clicking on notifications will launch new processes of the same app, even if your app is already running.

### [C#](#tab/cs)

The steps for updating your manifest are:

1. Open your **Package.appxmanifest**.
1. Add `xmlns:com="http://schemas.microsoft.com/appx/manifest/com/windows10"` and `xmlns:desktop="http://schemas.microsoft.com/appx/manifest/desktop/windows10"` namespaces to `<Package>`.
1. Add **com** and **desktop** to the **IgnorableNamespaces** attribute in `<Package>`.
1. Add `<desktop:Extension>` for `windows.toastNotificationActivation` to declare your COM activator **[CLSID](/uwp/schemas/appxpackage/uapmanifestschema/element-com-exeserver-class)**. You can obtain a CLSID by navigating to **Create GUID** under **Tools** in Visual Studio.
1. Add `<com:Extension>` for the COM activator using the same CLSID.
    1. Specify your .exe file in the `Executable` attribute. The .exe file must be the same process calling `Register()` when registering your app for notifications, which is described more in **Step 3**. In the example below, we use `Executable="SampleApp\SampleApp.exe"`.
    1. Specify `Arguments="----AppNotificationActivated:"` to ensure that Windows App SDK can process your notification's payload as an AppNotification kind.
    1. Specify a `DisplayName`.

```xml
<!--Add these namespaces-->
<Package
  ...
  xmlns:com="http://schemas.microsoft.com/appx/manifest/com/windows10"
  xmlns:desktop="http://schemas.microsoft.com/appx/manifest/desktop/windows10"
  IgnorableNamespaces="... com desktop">
  ...
  <Applications>
    <Application>
      ...
      <Extensions>

        <!--Specify which CLSID to activate when app notification clicked-->
        <desktop:Extension Category="windows.toastNotificationActivation">
          <desktop:ToastNotificationActivation ToastActivatorCLSID="replaced-with-your-guid-C173E6ADF0C3" /> 
        </desktop:Extension>

        <!--Register COM CLSID LocalServer32 registry key-->
        <com:Extension Category="windows.comServer">
          <com:ComServer>
            <com:ExeServer Executable="YourProject.exe" Arguments="----AppNotificationActivated:" DisplayName="App notification activator">
              <com:Class Id="replaced-with-your-guid-C173E6ADF0C3" DisplayName="App notification activator"/>
            </com:ExeServer>
          </com:ComServer>
        </com:Extension>

      </Extensions>
    </Application>
  </Applications>
 </Package>
```

### [C+](#tab/cpp)

The steps for updating your manifest are:

1. Open your **Package.appxmanifest**.
1. Add `xmlns:com="http://schemas.microsoft.com/appx/manifest/com/windows10"` and `xmlns:desktop="http://schemas.microsoft.com/appx/manifest/desktop/windows10"` namespaces to `<Package>`.
1. Add **com** and **desktop** to the **IgnorableNamespaces** attribute in `<Package>`.
1. Add `<desktop:Extension>` for `windows.toastNotificationActivation` to declare your COM activator **[CLSID](/uwp/schemas/appxpackage/uapmanifestschema/element-com-exeserver-class)**. You can obtain a CLSID by navigating to **Create GUID** under **Tools** in Visual Studio.
1. Add `<com:Extension>` for the COM activator using the same CLSID.
    1. Specify your .exe file in the `Executable` attribute. The .exe file must be the same process calling `Register()` when registering your app for notifications, which is described more in **Step 3**. In the example below, we use `Executable="SampleApp\SampleApp.exe"`.
    1. Specify `Arguments="----AppNotificationActivated:"` to ensure that Windows App SDK can process your notification's payload as an AppNotification kind.
    1. Specify a `DisplayName`.


```xml
<!--Add these namespaces-->
<Package
  ...
  xmlns:com="http://schemas.microsoft.com/appx/manifest/com/windows10"
  xmlns:desktop="http://schemas.microsoft.com/appx/manifest/desktop/windows10"
  IgnorableNamespaces="... com desktop">
  ...
  <Applications>
    <Application>
      ...
      <Extensions>

        <!--Specify which CLSID to activate when app notification clicked-->
        <desktop:Extension Category="windows.toastNotificationActivation">
          <desktop:ToastNotificationActivation ToastActivatorCLSID="replaced-with-your-guid-C173E6ADF0C3" /> 
        </desktop:Extension>

        <!--Register COM CLSID LocalServer32 registry key-->
        <com:Extension Category="windows.comServer">
          <com:ComServer>
            <com:ExeServer Executable="YourProject.exe" Arguments="----AppNotificationActivated:" DisplayName="App notification activator">
              <com:Class Id="replaced-with-your-guid-C173E6ADF0C3" DisplayName="App notification activator"/>
            </com:ExeServer>
          </com:ComServer>
        </com:Extension>

      </Extensions>
    </Application>
  </Applications>
 </Package>
```

---

## Step 5: Register to handle activation from an app notification

This section describes the steps to handle app activation. Your app will need to handle being activated from the user interacting with a notification, but it should also handle activation from the other ways that a user will launch your app, such as from the Start menu. Because you may not know if you want to fully launch your app until you've checked the data passed in from an app notification, some activation steps will be implemented helper methods so that they can be called from multiple places.

### [C#](#tab/cs)

In **OnLaunched**, get the default instance of the [AppNotificationManager](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager) class. Register for the [AppNotificationManager.NotificationInvoked](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.notificationinvoked) event. Call [Microsoft.Windows.AppNotifications.AppNotificationManager.Register](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.register) to register your app to receive notification events. It is important that your call this method after registering the **NotificationInvoked** handler.

Call [AppInstance.GetActivatedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.getactivatedeventargs) and check the [AppActivationArguments.Kind](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appactivationarguments.kind) property of the returned object for the value [ExtendedActivationKind.AppNotification](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.extendedactivationkind). If the activation kind is not **AppNotification**, you should handle your regular app activation tasks. It is recommended that you move your basic activation code into helper method, so that it can be called from multiple places. An example helper method that illustrates this will be shown later in this quickstart. If the activation kind is **AppNotification**, we want to handle notification-specific activation. Cast the [AppActivationArguments.Data](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appactivationarguments.data) property to an [AppNotificationActivatedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationactivatedeventargs) and pass it to the **HandleNotification** helper method, which will also be shown later in this quickstart.

> [!IMPORTANT]
> You must call **AppNotificationManager::Default().Register** before calling [AppInstance.GetCurrent.GetActivatedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.getactivatedeventargs).

The **AppNotificationManager.NotificationInvoked** event handler, which was registered in a previous step, will be called when the user clicks on a notification while your app is already running. In this event handler, we will call the same helper method as the previous step, **HandleNotification**.

Register a handler for the [ProcessExit](/dotnet/api/system.appdomain.processexit) event to unregister for notifications when your app is terminating. In the handler for that event, call [Microsoft.Windows.AppNotifications.AppNotificationManager.Unregister](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.unregister) to free up the COM server and allow for subsequent invokes to launch a new process.

Add the following code to your `App.xaml.cs` file, replacing the existing definition of **OnLaunched**.

```csharp
// App.xaml.cs
protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
{
    m_window = new MainWindow();

    // To ensure all Notification handling happens in this process instance, register for
    // NotificationInvoked before calling Register(). Without this a new process will
    // be launched to handle the notification.
    AppNotificationManager notificationManager = AppNotificationManager.Default;
    notificationManager.NotificationInvoked += NotificationManager_NotificationInvoked;
    notificationManager.Register();

    var activatedArgs = Microsoft.Windows.AppLifecycle.AppInstance.GetCurrent().GetActivatedEventArgs();
    var activationKind = activatedArgs.Kind;
    if (activationKind != ExtendedActivationKind.AppNotification)
    {
        LaunchAndBringToForegroundIfNeeded();
    }
    else
    {
        HandleNotification((AppNotificationActivatedEventArgs)activatedArgs.Data);
    }

    AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
}
private void NotificationManager_NotificationInvoked(AppNotificationManager sender, AppNotificationActivatedEventArgs args)
{
    HandleNotification(args);
}

private void OnProcessExit(object sender, EventArgs e)
{
    AppNotificationManager.Default.Unregister();
}
```

This example defines the helper method **LaunchAndBringToForegroundIfNeeded** to handle general app activation tasks. A helper class, **WindowHelper**, is also shown in the code example below. It uses COM calls to activate the app Window. You will need to add using directives for the **System.Runtime.InteropServices** and **using System.Runtime.InteropServices.WindowsRuntime** namespaces to access these COM functions.

Add the following helper code to your `App.xaml.cs` file, inside the **App** class definition.

```csharp
// App.xaml.cs
private void LaunchAndBringToForegroundIfNeeded()
{
    if (m_window == null)
    {
        m_window = new MainWindow();
        m_window.Activate();

        // Additionally we show using our helper, since if activated via a app notification, it doesn't
        // activate the window correctly
        WindowHelper.ShowWindow(m_window);
    }
    else
    {
        WindowHelper.ShowWindow(m_window);
    }
}

// Add these using directives to the top of the file
// using System.Runtime.InteropServices;
// using System.Runtime.InteropServices.WindowsRuntime;
private static class WindowHelper
{
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    public static void ShowWindow(Window window)
    {
        // Bring the window to the foreground... first get the window handle...
        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

        // Restore window if minimized... requires DLL import above
        ShowWindow(hwnd, 0x00000009);

        // And call SetForegroundWindow... requires DLL import above
        SetForegroundWindow(hwnd);
    }
}
```



### [C++](#tab/cpp)

Update your precompiled header file again to include these headers related to application lifecycle and window management.

```cpp
\\ phc.h
#include <winrt/Microsoft.Windows.AppLifecycle.h>
#include <Microsoft.UI.Xaml.Window.h>
```

Add declarations for the App destructor and some helper methods to your `App.xaml.h` header file.

```cpp
\\ App.xaml.h
...
    ~App();

private: 
    void HandleNotification(Microsoft::Windows::AppNotifications::AppNotificationActivatedEventArgs const&);
    void ActivateAndBringToForegroundIfNeeded();
...
```

Add `using` directives for the App Notifications namespaces from the Windows App SDK to the other directives at the top of `App.xaml.cpp`

```cpp
\\ App.xaml.cpp
using namespace Microsoft::Windows::AppLifecycle;
using namespace Microsoft::Windows::AppNotifications;
```

Update the **App** constructor to register a handler for the [AppNotificationManager.NotificationInvoked](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.notificationinvoked) event that will be raised if the user clicks on an app notification while your app is running. Then, call [Microsoft.Windows.AppNotifications.AppNotificationManager.Register](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.register) to register your app with the system to receive notifications. Make sure that you register the **NotificationInvoked** event handler before calling **Register** or the system will launch a new process to handle notifications instead of using the existing one.  

```cpp
\\ App.xaml.cpp
App::App()
{
    auto notificationManager{ AppNotificationManager::Default() };

    // To ensure all Notification handling happens in this process instance, register for
    // NotificationInvoked before calling Register(). Without this a new process will
    // be launched to handle the notification.
    const auto token{ notificationManager.NotificationInvoked([&](const auto&, AppNotificationActivatedEventArgs  const& notificationActivatedEventArgs)
        {

            HandleNotification(notificationActivatedEventArgs);

        }) };

    AppNotificationManager::Default().Register();
    ...

```

Implement the **App** destructor and call [Microsoft.Windows.AppNotifications.AppNotificationManager.Unregister](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationmanager.unregister) to free up the COM server and allow for subsequent invokes to launch a new process. 

```cpp
\\ App.xaml.cpp
App::~App()
{
    AppNotificationManager::Default().Unregister();
}
```

Next, implement an override of the **App::OnLaunched** app lifecycle method. Get the activation arguments for the current app instance by calling [GetActivatedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.getactivatedeventargs). Determine if this instance of the app was activated by a user interaction with an app notification by checking the [AppActivationArguments.Kind](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appactivationarguments.kind) property of the returned **AppActivationArguments** object for the value [ExtendedActivationKind.AppNotification](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.extendedactivationkind). 

If the activation is from an app notification, then Cast the [AppActivationArguments.Data](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appactivationarguments.data) property to an [AppNotificationActivatedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationactivatedeventargs) and pass it to the **HandleNotification** helper method, which will be shown later in this quickstart. If it's a regular app activation, call the **ActivateAndBringToForegroundIfNeeded** helper method, which will also be show next.


```cpp
void App::OnLaunched(winrt::Microsoft::UI::Xaml::LaunchActivatedEventArgs const& /*args*/)
{
    // NOTE: AppInstance is ambiguous between
    // Microsoft.Windows.AppLifecycle.AppInstance and Windows.ApplicationModel.AppInstance
    auto currentInstance{ Microsoft::Windows::AppLifecycle::AppInstance::GetCurrent() };
    if (currentInstance)
    {
        AppActivationArguments activationArgs{ currentInstance.GetActivatedEventArgs() };
        if (activationArgs)
        {
            ExtendedActivationKind extendedKind{ activationArgs.Kind() };
            if (extendedKind == winrt::Microsoft::Windows::AppLifecycle::ExtendedActivationKind::AppNotification)
            {
                AppNotificationActivatedEventArgs notificationActivatedEventArgs{ activationArgs.Data().as<AppNotificationActivatedEventArgs>() };
                HandleNotification(notificationActivatedEventArgs);
            }
            else
            {
                ActivateAndBringToForegroundIfNeeded();
            }
        }
    }
}
```

> [!IMPORTANT]
> You must call **AppNotificationManager::Default().Register** before calling [AppInstance.GetCurrent.GetActivatedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.getactivatedeventargs).

Implement the **ActivateAndBringToForegroundIfNeeded** helper method. This method creates the app window and activates it if it doesn't already exist. Then, the app calls the [SwitchToThisWindow](/windows/win32/api/winuser/nf-winuser-switchtothiswindow) win32 function to make sure the window comes to the foreground when activated from a notification.

```cpp
void App::ActivateAndBringToForegroundIfNeeded()
{
    if (window == nullptr)
    {
        window = make<MainWindow>();
        window.Activate();
    }

    HWND hwnd;
    auto windowNative{ window.as<IWindowNative>() };
    if (windowNative && SUCCEEDED(windowNative->get_WindowHandle(&hwnd)))
    {
        SwitchToThisWindow(hwnd, TRUE);
    }
}
```


## Step 6: Handle user interaction with app notifications

The preceding step showed how to handle app activation, both from an app notification or other means. This section shows how to handle the app notification data that is passed to your app either from the **NotificationInvoked** event or from app activation. 

There are two different ways that an app can respond to the user clicking on an app notification or a button within the notification.

1. Update the UI. This could be simply updating the currently displayed content of a running app or it could be launching or navigating the app to a new context.
2. Take some action based on the data passed from the notification, but don't launch the app or update its UI. This appears to the user as a "background" action.

The following guidelines should be considered when designing the behavior of your app in response to user interaction with an app notification.

- If a notification is clicked by the user and your app is not running, it is expected that your app is launched and the user can see the foreground window in a context related to the notification.
- If a notification is selected by the user and your app is minimized, it is expected that your app is brought to the foreground and a new window is rendered in a context related to the notification.
- If a notification background action is invoked by the user, your app processes the payload without rendering a foreground window. An example of this is if the user types a response into a text box on the notification and clicks a **Send** button.


In this example, the code for handling app notifications is implemented in the **HandleNotification** helper method which we have called in previous steps.

The arguments passed from the app notification are accessed via the [AppNotificationActivatedEventArgs.Arguments](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.appnotificationactivatedeventargs.arguments) property. In the example app notification we created previously in this quickstart, we assigned an argument named "action" to the notification with a value of "viewConversation". This value will be present if the user clicked on the notification. In this case, we want to continue launching the app, if it was not previously running, and update the UI. 

In our notification payload, we also assigned an "action" argument to the **AppNotificationButton** with a value of "sendMessage". For this scenario, we want to get the value in the notification's text box and send that value in a new message, but this should appear to the user to occur in the background. So, in this case, we kill the current process, causing the app to exit instead of launch.

### [C#](#tab/cs)

Add the following definition of **HandleNotification** to the **App** class definition in your App.xaml.cs file. 

> [!IMPORTANT]
> Within your `HandleNotification` helper method, make sure that dispatch to the App or Window dispatcher before executing any UI-related code like showing a window or updating UI.

```csharp
private async void HandleNotification(AppNotificationActivatedEventArgs args)
{

    switch (args.Arguments["action"])
    {
        // Send a background message
        case "sendMessage":
            string message = args.UserInput["textBox"].ToString();

            // TODO: Send the message
            // await SendMessage(message);

            // If the app isn't open
            if (m_window == null)
            {
                // Close since we're done
                Process.GetCurrentProcess().Kill();
            }

            break;

        case "viewConversation":

            // Use the dispatcher from the window if present, otherwise the app dispatcher
            var dispatcherQueue = m_window?.DispatcherQueue ?? DispatcherQueue.GetForCurrentThread();


            dispatcherQueue.TryEnqueue(async delegate
            {

                // TODO: Update the UI on the UI thread.
                // ((MainWindow)m_window).ShowConversation(args.Arguments["conversationId"]);

            });

            // Launch/bring window to foreground
            LaunchAndBringToForegroundIfNeeded();

            break;
    }
    
}

```


### [C++](#tab/cpp)

Add the following definition of **HandleNotification** to the **App** class definition in your App.xaml.cs file. 

> [!IMPORTANT]
> Within your `HandleNotification` helper method, make sure that dispatch to the App or Window dispatcher before executing any UI-related code like showing a window or updating UI.

```cpp
// App.xaml.cpp

void App::HandleNotification(Microsoft::Windows::AppNotifications::AppNotificationActivatedEventArgs const& args)
{
    auto action = args.Arguments().Lookup(L"action");

    if (action == L"viewConversation") {

        auto dispatcherQueue = window ? window.DispatcherQueue() : Microsoft::UI::Dispatching::DispatcherQueue::GetForCurrentThread();
        auto conversationId = args.Arguments().Lookup(L"conversationId");

        dispatcherQueue.TryEnqueue([strongThis = get_strong(), this, conversationId]
        { 
                // TODO: Update the UI on the UI thread
                // window.as<MainWindow>()->ViewConversation(conversationId); 
        });

        ActivateAndBringToForegroundIfNeeded();

    } else if (action == L"sendMessage") {

        auto message = args.UserInput().Lookup(L"textBox");
        // TODO: Send the message
        // SendMessage(message);


        // If the UI app isn't open
        if (window == nullptr)
        {
            
            // Close since we're done
            this->Exit();
        }

    } 
}
```

---


TBD - Next steps 

## Sample app

This quickstart covers code from the notifications sample apps found on [GitHub](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Notifications/).

> [!div class="button"]
> [Sample App Code](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Notifications/)
 
## API reference

For API reference documentation for app notifications, see [Microsoft.Windows.AppNotifications Namespace](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications).

## Resources

- [Microsoft.Windows.AppNotifications API details](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/AppNotifications/AppNotifications-spec.md#api-details)
- [Notifications code sample on GitHub](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Notifications/)
- [App notifications spec on GitHub](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/AppNotifications/AppNotifications-spec.md)
- [Toast content](/hub/apps/design/shell/tiles-and-notifications/adaptive-interactive-toasts)
- [Notifications XML schema](/uwp/schemas/tiles/toastschema/schema-root)
