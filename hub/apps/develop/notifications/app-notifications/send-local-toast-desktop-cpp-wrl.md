---
title: Send a local app notification from a WRL C++ desktop app (legacy)
label: Send a local app notification from a WRL C++ desktop app (legacy)
description: Learn how Win32 C++ WRL apps can send local app notifications and handle the user clicking the notification using a legacy compat library.
template: detail.hbs
ms.date: 04/08/2026
ms.topic: how-to
keywords: windows 11, windows 10, win32, desktop, toast notifications, C++, cpp, cplusplus, WRL, legacy
ms.localizationpriority: medium
no-loc: [toast, Toast, app, App]
---

# Send a local app notification from a WRL C++ desktop app (legacy)

> [!IMPORTANT]
> **This article describes a legacy approach.** For C++ desktop apps using the Windows App SDK, we recommend using the `Microsoft.Windows.AppNotifications` APIs with C++/WinRT. See [Quickstart: App notifications in the Windows App SDK](app-notifications-quickstart.md) for the recommended approach, which is simpler and does not require a separate compat library.

This article describes how to send app notifications from a classic Win32 C++ desktop app using the Windows Runtime C++ Template Library (WRL) and a helper compat library. This approach is for apps that cannot take a dependency on the Windows App SDK.

## Overview

The WRL approach requires:

1. A **compat helper library** (`DesktopNotificationManagerCompat.h` and `.cpp`) that you copy into your project
2. A **COM activator** class that handles notification clicks
3. **COM registration** — done via the app manifest (packaged) or via Start shortcut properties (unpackaged)

## Step 1: Set up your project

1. Add `runtimeobject.lib` to **Linker > Input > Additional Dependencies**.
2. Make sure **Windows SDK Version** is set to 10.0 or later.

## Step 2: Copy the compat helper library

Copy [DesktopNotificationManagerCompat.h](https://raw.githubusercontent.com/WindowsNotifications/desktop-toasts/master/CPP-WRL/DesktopToastsCppWrlApp/DesktopNotificationManagerCompat.h) and [DesktopNotificationManagerCompat.cpp](https://raw.githubusercontent.com/WindowsNotifications/desktop-toasts/master/CPP-WRL/DesktopToastsCppWrlApp/DesktopNotificationManagerCompat.cpp) from the [desktop-toasts sample on GitHub](https://github.com/WindowsNotifications/desktop-toasts) into your project. These files are self-contained C++ helpers that wrap the Windows notification COM APIs — they have no external dependencies.

If you're using precompiled headers, add `#include "stdafx.h"` as the first line of `DesktopNotificationManagerCompat.cpp`.

## Step 3: Include headers and namespaces

```cpp
#include "DesktopNotificationManagerCompat.h"
#include <NotificationActivationCallback.h>
#include <windows.ui.notifications.h>

using namespace ABI::Windows::Data::Xml::Dom;
using namespace ABI::Windows::UI::Notifications;
using namespace Microsoft::WRL;
```

## Step 4: Implement the COM activator

Implement `INotificationActivationCallback`. This is called when the user clicks a notification.

```cpp
// The UUID CLSID must be unique to your app. Create a new GUID if copying this code.
class DECLSPEC_UUID("replaced-with-your-guid-C173E6ADF0C3") NotificationActivator WrlSealed WrlFinal
    : public RuntimeClass<RuntimeClassFlags<ClassicCom>, INotificationActivationCallback>
{
public:
    virtual HRESULT STDMETHODCALLTYPE Activate(
        _In_ LPCWSTR appUserModelId,
        _In_ LPCWSTR invokedArgs,
        _In_reads_(dataCount) const NOTIFICATION_USER_INPUT_DATA* data,
        ULONG dataCount) override
    {
        std::wstring arguments(invokedArgs);

        if (arguments.find(L"action=reply") == 0)
        {
            // Handle background action (e.g., quick reply)
            LPCWSTR response = data[0].Value;
            // Process the response...
        }
        else
        {
            // Foreground activation — bring window to front
            // ...
        }

        return S_OK;
    }
};

CoCreatableClass(NotificationActivator);
```

## Step 5: Register with the notification platform

### Packaged apps

Add the following to your `Package.appxmanifest`:

```xml
<Package
  xmlns:com="http://schemas.microsoft.com/appx/manifest/com/windows10"
  xmlns:desktop="http://schemas.microsoft.com/appx/manifest/desktop/windows10"
  IgnorableNamespaces="... com desktop">
  <Applications>
    <Application>
      <Extensions>
        <com:Extension Category="windows.comServer">
          <com:ComServer>
            <com:ExeServer Executable="YourApp.exe" Arguments="-ToastActivated" DisplayName="Toast activator">
              <com:Class Id="replaced-with-your-guid-C173E6ADF0C3" DisplayName="Toast activator"/>
            </com:ExeServer>
          </com:ComServer>
        </com:Extension>
        <desktop:Extension Category="windows.toastNotificationActivation">
          <desktop:ToastNotificationActivation ToastActivatorCLSID="replaced-with-your-guid-C173E6ADF0C3" />
        </desktop:Extension>
      </Extensions>
    </Application>
  </Applications>
</Package>
```

### Unpackaged apps

Set your AUMID and CLSID on your app's Start shortcut (e.g., via WiX installer):

```xml
<Shortcut Id="StartMenuShortcut" Name="My App" Target="[INSTALLFOLDER]MyApp.exe" WorkingDirectory="INSTALLFOLDER">
    <ShortcutProperty Key="System.AppUserModel.ID" Value="YourCompany.YourApp"/>
    <ShortcutProperty Key="System.AppUserModel.ToastActivatorCLSID" Value="{replaced-with-your-guid-C173E6ADF0C3}"/>
</Shortcut>
```

Then in your startup code:

```cpp
hr = DesktopNotificationManagerCompat::RegisterAumidAndComServer(
    L"YourCompany.YourApp", __uuidof(NotificationActivator));
hr = DesktopNotificationManagerCompat::RegisterActivator();
```

## Step 6: Send a notification

```cpp
ComPtr<IXmlDocument> doc;
hr = DesktopNotificationManagerCompat::CreateXmlDocumentFromString(
    L"<toast><visual><binding template='ToastGeneric'><text>Hello world</text></binding></visual></toast>",
    &doc);

if (SUCCEEDED(hr))
{
    ComPtr<IToastNotifier> notifier;
    hr = DesktopNotificationManagerCompat::CreateToastNotifier(&notifier);
    if (SUCCEEDED(hr))
    {
        ComPtr<IToastNotification> toast;
        hr = DesktopNotificationManagerCompat::CreateToastNotification(doc.Get(), &toast);
        if (SUCCEEDED(hr))
        {
            hr = notifier->Show(toast.Get());
        }
    }
}
```

> [!IMPORTANT]
> Desktop apps can't use legacy notification templates (such as ToastText02). You must use the ToastGeneric templates.

## Step 7: Handle cold-launch activation

In your `WinMain`, check for the `-ToastActivated` argument to determine if the app was launched from a notification click:

```cpp
int WINAPI wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE, _In_ LPWSTR cmdLineArgs, _In_ int)
{
    RoInitializeWrapper winRtInitializer(RO_INIT_MULTITHREADED);
    HRESULT hr = winRtInitializer;

    if (SUCCEEDED(hr))
    {
        hr = DesktopNotificationManagerCompat::RegisterAumidAndComServer(
            L"YourCompany.YourApp", __uuidof(NotificationActivator));
        hr = DesktopNotificationManagerCompat::RegisterActivator();

        std::wstring cmdLineArgsStr(cmdLineArgs);
        if (cmdLineArgsStr.find(L"-ToastActivated") != std::string::npos)
        {
            // Launched from notification — let COM activator handle it
        }
        else
        {
            // Normal launch — show your main window
        }

        // Run message loop
    }

    return SUCCEEDED(hr);
}
```

## Resources

* [Full code sample on GitHub](https://github.com/WindowsNotifications/desktop-toasts)
* [App notification content documentation](adaptive-interactive-toasts.md)
* [Quickstart: App notifications in the Windows App SDK](app-notifications-quickstart.md)
