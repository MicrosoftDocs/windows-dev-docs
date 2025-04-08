---
title: Send a local toast notification from a WRL C++ desktop app
label: Send a local toast notification from a WRL C++ desktop app
description: Learn how Win32 C++ WRL apps can send local toast notifications and handle the user clicking the toast.
template: detail.hbs
ms.date: 10/06/2022
ms.topic: article
keywords: windows 11, windows 10, uwp, win32, desktop, toast notifications, send a toast, send local toast, desktop bridge, msix, external location, C++, cpp, cplusplus, WRL
ms.localizationpriority: medium
---

# Send a local toast notification from a WRL C++ desktop app

Packaged and unpackaged desktop apps can send interactive toast notifications just like Universal Windows platform (UWP) apps can. That includes packaged apps (see [Create a new project for a packaged WinUI 3 desktop app](../../../winui/winui3/create-your-first-winui3-app.md#packaged-create-a-new-project-for-a-packaged-c-or-c-winui-3-desktop-app)); packaged apps with external location (see [Grant package identity by packaging with external location](../../../desktop/modernize/grant-identity-to-nonpackaged-apps.md)); and unpackaged apps (see [Create a new project for an unpackaged WinUI 3 desktop app](../../../winui/winui3/create-your-first-winui3-app.md#unpackaged-create-a-new-project-for-an-unpackaged-c-or-c-winui-3-desktop-app)).

However, for an unpackaged desktop app, there are a few special steps. That's due to the different activation schemes, and the lack of package identity at runtime.

> [!IMPORTANT]
> If you're writing a UWP app, please see the [UWP documentation](send-local-toast.md). For other desktop languages, please see [Desktop C#](./send-local-toast.md).

## Step 1: Enable the Windows SDK

If you haven't enabled the Windows SDK for your app, then you must do that first. There are a few key steps.

1. Add `runtimeobject.lib` to **Additional Dependencies**.
2. Target the Windows SDK.

Right click your project and select **Properties**.

In the top **Configuration** menu, select **All Configurations** so that the following change is applied to both Debug and Release.

Under **Linker -> Input**, add `runtimeobject.lib` to the **Additional Dependencies**.

Then under **General**, make sure that the **Windows SDK Version** is set to version 10.0 or later.

## Step 2: Copy compat library code

Copy the [DesktopNotificationManagerCompat.h](https://raw.githubusercontent.com/WindowsNotifications/desktop-toasts/master/CPP-WRL/DesktopToastsCppWrlApp/DesktopNotificationManagerCompat.h) and [DesktopNotificationManagerCompat.cpp](https://raw.githubusercontent.com/WindowsNotifications/desktop-toasts/master/CPP-WRL/DesktopToastsCppWrlApp/DesktopNotificationManagerCompat.cpp) file from GitHub into your project. The compat library abstracts much of the complexity of desktop notifications. The following instructions require the compat library.

If you're using precompiled headers, make sure to `#include "stdafx.h"` as the first line of the DesktopNotificationManagerCompat.cpp file.

## Step 3: Include the header files and namespaces

Include the compat library header file, and the header files and namespaces related to using the Windows toast APIs.

```cpp
#include "DesktopNotificationManagerCompat.h"
#include <NotificationActivationCallback.h>
#include <windows.ui.notifications.h>

using namespace ABI::Windows::Data::Xml::Dom;
using namespace ABI::Windows::UI::Notifications;
using namespace Microsoft::WRL;
```

## Step 4: Implement the activator

You must implement a handler for toast activation, so that when the user clicks on your toast, your app can do something. This is required for your toast to persist in Action Center (since the toast could be clicked days later when your app is closed). This class can be placed anywhere in your project.

Implement the **INotificationActivationCallback** interface as seen below, including a UUID, and also call **CoCreatableClass** to flag your class as COM creatable. For your UUID, create a unique GUID using one of the many online GUID generators. This GUID CLSID (class identifier) is how Action Center knows what class to COM activate.

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
        // TODO: Handle activation
    }
};

// Flag class as COM creatable
CoCreatableClass(NotificationActivator);
```

## Step 5: Register with notification platform

Then, you must register with the notification platform. There are different steps depending on whether your app is packaged or unpackaged. If you support both, then you must perform both sets of steps (however, there's no need to fork your code since our library handles that for you).

### Packaged

If your app is packaged (see [Create a new project for a packaged WinUI 3 desktop app](../../../winui/winui3/create-your-first-winui3-app.md#packaged-create-a-new-project-for-a-packaged-c-or-c-winui-3-desktop-app)) or packaged with external location (see [Grant package identity by packaging with external location](../../../desktop/modernize/grant-identity-to-nonpackaged-apps.md)), or if you support both, then in your **Package.appxmanifest** add:

1. Declaration for **xmlns:com**
2. Declaration for **xmlns:desktop**
3. In the **IgnorableNamespaces** attribute, **com** and **desktop**
4. **com:Extension** for the COM activator using the GUID from step #4. Be sure to include the `Arguments="-ToastActivated"` so that you know your launch was from a toast
5. **desktop:Extension** for **windows.toastNotificationActivation** to declare your toast activator CLSID (the GUID from step #4).

**Package.appxmanifest**

```xml
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

        <!--Register COM CLSID LocalServer32 registry key-->
        <com:Extension Category="windows.comServer">
          <com:ComServer>
            <com:ExeServer Executable="YourProject\YourProject.exe" Arguments="-ToastActivated" DisplayName="Toast activator">
              <com:Class Id="replaced-with-your-guid-C173E6ADF0C3" DisplayName="Toast activator"/>
            </com:ExeServer>
          </com:ComServer>
        </com:Extension>

        <!--Specify which CLSID to activate when toast clicked-->
        <desktop:Extension Category="windows.toastNotificationActivation">
          <desktop:ToastNotificationActivation ToastActivatorCLSID="replaced-with-your-guid-C173E6ADF0C3" /> 
        </desktop:Extension>

      </Extensions>
    </Application>
  </Applications>
 </Package>
```

### Unpackaged

If your app is unpackaged (see [Create a new project for an unpackaged WinUI 3 desktop app](../../../winui/winui3/create-your-first-winui3-app.md#unpackaged-create-a-new-project-for-an-unpackaged-c-or-c-winui-3-desktop-app)), or if you support both, then you have to declare your Application User Model ID (AUMID) and toast activator CLSID (the GUID from step #4) on your app's shortcut in Start.

Pick a unique AUMID that will identify your app. This is typically in the form of [CompanyName].[AppName]. But you want to ensure that it's unique across all apps (so feel free to add some digits at the end).

#### Step 5.1: WiX Installer

If you're using WiX for your installer, edit the **Product.wxs** file to add the two shortcut properties to your Start menu shortcut as seen below. Be sure that your GUID from step #4 is enclosed in `{}` as seen below.

**Product.wxs**

```xml
<Shortcut Id="ApplicationStartMenuShortcut" Name="Wix Sample" Description="Wix Sample" Target="[INSTALLFOLDER]WixSample.exe" WorkingDirectory="INSTALLFOLDER">
                    
    <!--AUMID-->
    <ShortcutProperty Key="System.AppUserModel.ID" Value="YourCompany.YourApp"/>
    
    <!--COM CLSID-->
    <ShortcutProperty Key="System.AppUserModel.ToastActivatorCLSID" Value="{replaced-with-your-guid-C173E6ADF0C3}"/>
    
</Shortcut>
```

> [!IMPORTANT]
> In order to actually use notifications, you must install your app through the installer once before debugging normally, so that the Start shortcut with your AUMID and CLSID is present. After the Start shortcut is present, you can debug using F5 from Visual Studio.

#### Step 5.2: Register AUMID and COM server

Then, regardless of your installer, in your app's startup code (before calling any notification APIs), call the **RegisterAumidAndComServer** method, specifying your notification activator class from step #4 and your AUMID used above.

```cpp
// Register AUMID and COM server (for a packaged app, this is a no-operation)
hr = DesktopNotificationManagerCompat::RegisterAumidAndComServer(L"YourCompany.YourApp", __uuidof(NotificationActivator));
```

If your app supports both packaged and unpackaged deployment, then feel free to call this method regardless. If you're running packaged (that is, with package identity at runtime), then this method will simply return immediately. There's no need to fork your code.

This method allows you to call the compat APIs to send and manage notifications without having to constantly provide your AUMID. And it inserts the LocalServer32 registry key for the COM server.

## Step 6: Register COM activator

For both packaged and unpackaged apps, you must register your notification activator type, so that you can handle toast activations.

In your app's startup code, call the following **RegisterActivator** method. This must be called in order for you to receive any toast activations.

```cpp
// Register activator type
hr = DesktopNotificationManagerCompat::RegisterActivator();
```

## Step 7: Send a notification

Sending a notification is identical to UWP apps, except that you'll use **DesktopNotificationManagerCompat** to create a **ToastNotifier**. The compat library automatically handles the difference between packaged and unpackaged apps, so you don't need to fork your code. For an unpackaged app, the compat library caches the AUMID that you provided when you called **RegisterAumidAndComServer** so that you don't need to worry about when to provide or not provide the AUMID.

Make sure you use the **ToastGeneric** binding as seen below, since the legacy Windows 8.1 toast notification templates won't activate the COM notification activator that you created in step #4.

> [!IMPORTANT]
> Http images are supported only in packaged apps that have the internet capability in their manifest. Unpackaged apps don't support http images; you must download the image to your local app data, and reference it locally.

```cpp
// Construct XML
ComPtr<IXmlDocument> doc;
hr = DesktopNotificationManagerCompat::CreateXmlDocumentFromString(
    L"<toast><visual><binding template='ToastGeneric'><text>Hello world</text></binding></visual></toast>",
    &doc);
if (SUCCEEDED(hr))
{
    // See full code sample to learn how to inject dynamic text, buttons, and more

    // Create the notifier
    // Desktop apps must use the compat method to create the notifier.
    ComPtr<IToastNotifier> notifier;
    hr = DesktopNotificationManagerCompat::CreateToastNotifier(&notifier);
    if (SUCCEEDED(hr))
    {
        // Create the notification itself (using helper method from compat library)
        ComPtr<IToastNotification> toast;
        hr = DesktopNotificationManagerCompat::CreateToastNotification(doc.Get(), &toast);
        if (SUCCEEDED(hr))
        {
            // And show it!
            hr = notifier->Show(toast.Get());
        }
    }
}
```

> [!IMPORTANT]
> Desktop apps can't use legacy toast templates (such as ToastText02). Activation of the legacy templates will fail when the COM CLSID is specified. You must use the Windows ToastGeneric templates, as seen above.

## Step 8: Handling activation

When the user clicks on your toast, or buttons in the toast, the **Activate** method of your **NotificationActivator** class is invoked.

Inside the Activate method, you can parse the args that you specified in the toast and obtain the user input that the user typed or selected, and then activate your app accordingly.

> [!NOTE]
> The **Activate** method is called on a separate thread from your main thread.

```cpp
// The GUID must be unique to your app. Create a new GUID if copying this code.
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
        HRESULT hr = S_OK;

        // Background: Quick reply to the conversation
        if (arguments.find(L"action=reply") == 0)
        {
            // Get the response user typed.
            // We know this is first and only user input since our toasts only have one input
            LPCWSTR response = data[0].Value;

            hr = DesktopToastsApp::SendResponse(response);
        }

        else
        {
            // The remaining scenarios are foreground activations,
            // so we first make sure we have a window open and in foreground
            hr = DesktopToastsApp::GetInstance()->OpenWindowIfNeeded();
            if (SUCCEEDED(hr))
            {
                // Open the image
                if (arguments.find(L"action=viewImage") == 0)
                {
                    hr = DesktopToastsApp::GetInstance()->OpenImage();
                }

                // Open the app itself
                // User might have clicked on app title in Action Center which launches with empty args
                else
                {
                    // Nothing to do, already launched
                }
            }
        }

        if (FAILED(hr))
        {
            // Log failed HRESULT
        }

        return S_OK;
    }

    ~NotificationActivator()
    {
        // If we don't have window open
        if (!DesktopToastsApp::GetInstance()->HasWindow())
        {
            // Exit (this is for background activation scenarios)
            exit(0);
        }
    }
};

// Flag class as COM creatable
CoCreatableClass(NotificationActivator);
```

To properly support being launched while your app is closed, in your WinMain function, you'll want to determine whether you're being launched from a toast or not. If launched from a toast, there will be a launch arg of "-ToastActivated". When you see this, you should stop performing any normal launch activation code, and allow your **NotificationActivator** to handle launching windows if needed.

```cpp
// Main function
int WINAPI wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE, _In_ LPWSTR cmdLineArgs, _In_ int)
{
    RoInitializeWrapper winRtInitializer(RO_INIT_MULTITHREADED);

    HRESULT hr = winRtInitializer;
    if (SUCCEEDED(hr))
    {
        // Register AUMID and COM server (for a packaged app, this is a no-operation)
        hr = DesktopNotificationManagerCompat::RegisterAumidAndComServer(L"WindowsNotifications.DesktopToastsCpp", __uuidof(NotificationActivator));
        if (SUCCEEDED(hr))
        {
            // Register activator type
            hr = DesktopNotificationManagerCompat::RegisterActivator();
            if (SUCCEEDED(hr))
            {
                DesktopToastsApp app;
                app.SetHInstance(hInstance);

                std::wstring cmdLineArgsStr(cmdLineArgs);

                // If launched from toast
                if (cmdLineArgsStr.find(TOAST_ACTIVATED_LAUNCH_ARG) != std::string::npos)
                {
                    // Let our NotificationActivator handle activation
                }

                else
                {
                    // Otherwise launch like normal
                    app.Initialize(hInstance);
                }

                app.RunMessageLoop();
            }
        }
    }

    return SUCCEEDED(hr);
}
```

### Activation sequence of events

The activation sequence is the following...

If your app is already running:

1. **Activate** in your **NotificationActivator** is called

If your app is not running:

1. Your app is EXE launched, you get a command line args of "-ToastActivated"
2. **Activate** in your **NotificationActivator** is called

### Foreground vs background activation
For desktop apps, foreground and background activation is handled identically&mdash;your COM activator is called. It's up to your app's code to decide whether to show a window or to simply perform some work and then exit. Therefore, specifying an **activationType** of **background** in your toast content doesn't change the behavior.

## Step 9: Remove and manage notifications

Removing and managing notifications is identical to UWP apps. However, we recommend you use our compat library to obtain a **DesktopNotificationHistoryCompat** so you don't have to worry about providing the AUMID for a desktop app.

```cpp
std::unique_ptr<DesktopNotificationHistoryCompat> history;
auto hr = DesktopNotificationManagerCompat::get_History(&history);
if (SUCCEEDED(hr))
{
    // Remove a specific toast
    hr = history->Remove(L"Message2");

    // Clear all toasts
    hr = history->Clear();
}
```

## Step 10: Deploying and debugging

To deploy and debug your packaged app, see [Run, debug, and test a packaged desktop app](/windows/msix/desktop/desktop-to-uwp-debug).

To deploy and debug your desktop app, you must install your app through the installer once before debugging normally, so that the Start shortcut with your AUMID and CLSID is present. After the Start shortcut is present, you can debug using F5 from Visual Studio.

If your notifications simply fail to appear in your desktop app (and no exceptions are thrown), that likely means the Start shortcut isn't present (install your app via the installer), or the AUMID you used in code doesn't match the AUMID in your Start shortcut.

If your notifications appear but aren't persisted in Action Center (disappearing after the popup is dismissed), that means you haven't implemented the COM activator correctly.

If you've installed both your packaged and unpackaged desktop app, note that the packaged app will supersede the unpackaged app when handling toast activations. That means that toasts from the unpackaged app will launch the packaged app when clicked. Uninstalling the packaged app will revert activations back to the unpackaged app.

If you receive `HRESULT 0x800401f0 CoInitialize has not been called.`, be sure to call `CoInitialize(nullptr)` in your app before calling the APIs.

If you receive `HRESULT 0x8000000e A method was called at an unexpected time.` while calling the Compat APIs, that likely means you failed to call the required Register methods (or if a packaged app, you're not currently running your app under the packaged context).

If you get numerous `unresolved external symbol` compilation errors, you likely forgot to add `runtimeobject.lib` to the **Additional Dependencies** in step #1 (or you only added it to the Debug configuration and not Release configuration).

## Handling older versions of Windows

If you support Windows 8.1 or lower, you'll want to check at runtime whether you're running on Windows before calling any **DesktopNotificationManagerCompat** APIs or sending any ToastGeneric toasts.

Windows 8 introduced toast notifications, but used the [legacy toast templates](/previous-versions/windows/apps/hh761494(v=win.10)), like ToastText01. Activation was handled by the in-memory **Activated** event on the **ToastNotification** class since toasts were only brief popups that weren't persisted. Windows 10 introduced [interactive ToastGeneric toasts](adaptive-interactive-toasts.md), and also introduced Action Center where notifications are persisted for multiple days. The introduction of Action Center required the introduction of a COM activator, so that your toast can be activated days after you created it.

| OS | ToastGeneric | COM activator | Legacy toast templates |
| -- | ------------ | ------------- | ---------------------- |
| Windows 10 and later | Supported | Supported | Supported (but won't activate COM server) |
| Windows 8.1 / 8 | N/A | N/A | Supported |
| Windows 7 and lower | N/A | N/A | N/A |

To check whether you're running on Windows 10 or later, include the `<VersionHelpers.h>` header, and check the **IsWindows10OrGreater** method. If that returns `true`, then continue calling all the methods described in this documentation.

```cpp
#include <VersionHelpers.h>

if (IsWindows10OrGreater())
{
    // Running on Windows 10 or later, continue with sending toasts!
}
```

## Known issues

**FIXED: App doesn't become focused after clicking toast**: In builds 15063 and earlier, foreground rights weren't being transferred to your application when we activated the COM server. Therefore, your app would simply flash when you tried to move it to the foreground. There was no workaround for this issue. We fixed this in builds 16299 or later.

## Resources

* [Full code sample on GitHub](https://github.com/WindowsNotifications/desktop-toasts)
* [Toast notifications from desktop apps](toast-desktop-apps.md)
* [Toast content documentation](adaptive-interactive-toasts.md)
