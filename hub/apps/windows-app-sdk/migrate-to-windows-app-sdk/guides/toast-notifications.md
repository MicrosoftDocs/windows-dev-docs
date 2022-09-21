---
title: App notifications from UWP to WinUI 3 migration
description: This topic contains migration guidance in the toast notifications feature area.
ms.topic: article
ms.date: 12/14/2021
keywords: Windows, App, SDK, migrate, migrating, migration, port, porting, push, notifications, toast, toast notifications, uwp
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
---

# App notifications from UWP to WinUI 3 migration

The only difference when migrating app notification code from UWP to WinUI 3 is in handling the activation of notifications. Sending and managing toast notifications remains exactly the same.

> [!NOTE] The term "toast notification" is being replaced with "app notification". These terms both refer to the same feature of Windows, but over time we will phase out the use of "toast notification" in the documentation.

## Activation differences

### [Windows App SDK](#tab/appsdk) 

Category | UWP | WinUI 3
--|--|--
Foreground activation entry point | `OnActivated` method inside `App.xaml.cs` is called | Subscribe to `ToastNotificationManagerCompat.OnActivated` event (or COM class for C++)
Background activation entry point | Handled separately as a background task | Arrives through same `ToastNotificationManagerCompat.OnActivated` event (or COM class for C++)
Window activation | Your window is automatically brought to foreground when foreground activation occurs | You must bring your window to the foreground if desired

### [Windows Community Toolkit](#tab/toolkit) 

Category | UWP | WinUI 3
--|--|--
Foreground activation entry point | `OnActivated` method inside `App.xaml.cs` is called | Subscribe to `ToastNotificationManagerCompat.OnActivated` event (or COM class for C++)
Background activation entry point | Handled separately as a background task | Arrives through same `ToastNotificationManagerCompat.OnActivated` event (or COM class for C++)
Window activation | Your window is automatically brought to foreground when foreground activation occurs | You must bring your window to the foreground if desired

---

## Migration for C# apps

### Step 1: Install NuGet library

#### [Windows App SDK](#tab/appsdk) 

[!INCLUDE [nuget package](../../../design/shell/tiles-and-notifications/includes/nuget-package.md)]

This package adds the `ToastNotificationManagerCompat` API.

#### [Windows Community Toolkit](#tab/toolkit)

[!INCLUDE [nuget package](../../../design/shell/tiles-and-notifications/includes/nuget-package.md)]

This package adds the `ToastNotificationManagerCompat` API.

### Step 2: Update your manifest

In your **Package.appxmanifest**, add:

1. Declaration for **xmlns:com**
1. Declaration for **xmlns:desktop**
1. In the **IgnorableNamespaces** attribute, **com** and **desktop**
1. **desktop:Extension** for **windows.toastNotificationActivation** to declare your toast activator CLSID (using a new GUID of your choice).
1. MSIX only: **com:Extension** for the COM activator using the GUID from step #4. Be sure to include the `Arguments="-ToastActivated"` so that you know your launch was from a notification

#### Package.appxmanifest

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

        <!--Specify which CLSID to activate when toast clicked-->
        <desktop:Extension Category="windows.toastNotificationActivation">
          <desktop:ToastNotificationActivation ToastActivatorCLSID="replaced-with-your-guid-C173E6ADF0C3" /> 
        </desktop:Extension>

        <!--Register COM CLSID LocalServer32 registry key-->
        <com:Extension Category="windows.comServer">
          <com:ComServer>
            <com:ExeServer Executable="YourProject.exe" Arguments="-ToastActivated" DisplayName="Toast activator">
              <com:Class Id="replaced-with-your-guid-C173E6ADF0C3" DisplayName="Toast activator"/>
            </com:ExeServer>
          </com:ComServer>
        </com:Extension>

      </Extensions>
    </Application>
  </Applications>
 </Package>
```

### Step 3: Handle activation

**In your app's startup code** (typically App.xaml.cs), modify your code like the following...

1. Define and grab the app-level `DispatcherQueue`
2. Register for the `ToastNotificationManagerCompat.OnActivated` event
3. Refactor your window launch/activation code into a dedicated `LaunchAndBringToForegroundIfNeeded` method, so you can call it from multiple places
4. Avoid launching your window if `ToastNotificationManagerCompat.WasCurrentProcessToastActivated()` returns true (when that method is true, your `OnActivated` event will be called next and you can choose to show a window within the event callback).
5. Within your `ToastNotificationManagerCompat.OnActivated`, be sure to dispatch to the App or Window dispatcher before executing any UI-related code like showing a window or updating UI
6. **Migrate** your old UWP `OnActivated` code that handled toast activation to your new `ToastNotificationManagerCompat.OnActivated` event handler, and migrate any background task toast activation code to your new `ToastNotificationManagerCompat.OnActivated` event handler.

#### Migrated App.xaml.cs

```cs
public static DispatcherQueue DispatcherQueue { get; private set; }

protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
{
    // Get the app-level dispatcher
    DispatcherQueue = global::Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();

    // Register for toast activation. Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
    ToastNotificationManagerCompat.OnActivated += ToastNotificationManagerCompat_OnActivated;

    // If we weren't launched by a toast, launch our window like normal.
    // Otherwise if launched by a toast, our OnActivated callback will be triggered
    if (!ToastNotificationManagerCompat.WasCurrentProcessToastActivated())
    {
        LaunchAndBringToForegroundIfNeeded();
    }
}

private void LaunchAndBringToForegroundIfNeeded()
{
    if (m_window == null)
    {
        m_window = new MainWindow();
        m_window.Activate();

        // Additionally we show using our helper, since if activated via a toast, it doesn't
        // activate the window correctly
        WindowHelper.ShowWindow(m_window);
    }
    else
    {
        WindowHelper.ShowWindow(m_window);
    }
}

private void ToastNotificationManagerCompat_OnActivated(ToastNotificationActivatedEventArgsCompat e)
{
    // Use the dispatcher from the window if present, otherwise the app dispatcher
    var dispatcherQueue = m_window?.DispatcherQueue ?? App.DispatcherQueue;

    dispatcherQueue.TryEnqueue(delegate
    {
        var args = ToastArguments.Parse(e.Argument);

        switch (args["action"])
        {
            // Send a background message
            case "sendMessage":
                string message = e.UserInput["textBox"].ToString();
                // TODO: Send it

                // If the UI app isn't open
                if (m_window == null)
                {
                    // Close since we're done
                    Process.GetCurrentProcess().Kill();
                }

                break;

            // View a message
            case "viewMessage":

                // Launch/bring window to foreground
                LaunchAndBringToForegroundIfNeeded();

                // TODO: Open the message
                break;
        }
    });
}

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

## Related topics

* [Send a local toast notification from C# apps](../../../design/shell/tiles-and-notifications/send-local-toast.md)
* [Send a local toast notification from Win32 C++ WRL apps](../../../design/shell/tiles-and-notifications/send-local-toast-desktop-cpp-wrl.md)
