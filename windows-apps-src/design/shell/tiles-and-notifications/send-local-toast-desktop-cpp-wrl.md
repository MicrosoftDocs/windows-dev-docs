---
Description: Learn how Win32 C++ WRL apps can send local toast notifications and handle the user clicking the toast.
title: Send a local toast notification from desktop C++ WRL apps
label: Send a local toast notification from desktop C++ WRL apps
template: detail.hbs
ms.date: 03/07/2018
ms.topic: article
keywords: windows 10, uwp, win32, desktop, toast notifications, send a toast, send local toast, desktop bridge, msix, sparse package, C++, cpp, cplusplus, WRL
ms.localizationpriority: medium
---
# Send a local toast notification from desktop C++ WRL apps

Desktop apps (including packaged [MSIX](/windows/msix/desktop/source-code-overview) apps, apps that use [sparse packages](/windows/apps/desktop/modernize/grant-identity-to-nonpackaged-apps) to obtain package identity, and classic non-packaged Win32 apps) can send interactive toast notifications just like Windows apps. However, there are a few special steps for desktop apps due to the different activation schemes and the potential lack of package identity if you're not using MSIX or a sparse package.

> [!IMPORTANT]
> If you're writing a UWP app, please see the [UWP documentation](send-local-toast.md). For other desktop languages, please see [Desktop C#](send-local-toast-desktop.md).


## Step 1: Enable the Windows 10 SDK

If you haven't enabled the Windows 10 SDK for your Win32 app, you must do that first. There are a few key steps...

1. Add `runtimeobject.lib` to **Additional Dependencies**
2. Target the Windows 10 SDK

Right click your project and select **Properties**.

In the top **Configuration** menu, select **All Configurations** so that the following change is applied to both Debug and Release.

Under **Linker -> Input**, add `runtimeobject.lib` to the **Additional Dependencies**.

Then under **General**, make sure that the **Windows SDK Version** is set to something 10.0 or higher (not Windows 8.1).


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

Then, you must register with the notification platform. There are different steps depending on whether you are using MSIX/sparse packages or classic Win32. If you support both, you must do both steps (however, no need to fork your code, our library handles that for you!).


### MSIX/sparse package

If you're using [MSIX](/windows/msix/desktop/source-code-overview) or a [sparse package](/windows/apps/desktop/modernize/grant-identity-to-nonpackaged-apps) (or if you support both), in your **Package.appxmanifest**, add:

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


### Classic Win32

If you're using classic Win32 (or if you support both), you have to declare your Application User Model ID (AUMID) and toast activator CLSID (the GUID from step #4) on your app's shortcut in Start.

Pick a unique AUMID that will identify your Win32 app. This is typically in the form of [CompanyName].[AppName], but you want to ensure this is unique across all apps (feel free to add some digits at the end).

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
// Register AUMID and COM server (for MSIX/sparse package apps, this no-ops)
hr = DesktopNotificationManagerCompat::RegisterAumidAndComServer(L"YourCompany.YourApp", __uuidof(NotificationActivator));
```

If you support both MSIX/sparse package and classic Win32, feel free to call this method regardless. If you're running under an MSIX or sparse package, this method will simply return immediately. There's no need to fork your code.

This method allows you to call the compat APIs to send and manage notifications without having to constantly provide your AUMID. And it inserts the LocalServer32 registry key for the COM server.


## Step 6: Register COM activator

For both MSIX/sparse package and classic Win32 apps, you must register your notification activator type, so that you can handle toast activations.

In your app's startup code, call the following **RegisterActivator** method. This must be called in order for you to receive any toast activations.

```cpp
// Register activator type
hr = DesktopNotificationManagerCompat::RegisterActivator();
```


## Step 7: Send a notification

Sending a notification is identical to UWP apps, except that you will use **DesktopNotificationManagerCompat** to create a **ToastNotifier**. The compat library automatically handles the difference between MSIX/sparse package and classic Win32 so you do not have to fork your code. For classic Win32, the compat library caches your AUMID you provided when calling **RegisterAumidAndComServer** so that you don't need to worry about when to provide or not provide the AUMID.

Make sure you use the **ToastGeneric** binding as seen below since the legacy Windows 8.1 toast notification templates will not activate your COM notification activator you created in step #4.

> [!IMPORTANT]
> Http images are only supported in MSIX/sparse package apps that have the internet capability in their manifest. Classic Win32 apps do not support http images; you must download the image to your local app data and reference it locally.

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
    // Classic Win32 apps MUST use the compat method to create the notifier
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
> Classic Win32 apps cannot use legacy toast templates (like ToastText02). Activation of the legacy templates will fail when the COM CLSID is specified. You must use the Windows 10 ToastGeneric templates as seen above.


## Step 8: Handling activation

When the user clicks on your toast, or buttons in the toast, the **Activate** method of your **NotificationActivator** class is invoked.

Inside the Activate method, you can parse the args that you specified in the toast and obtain the user input that the user typed or selected, and then activate your app accordingly.

> [!NOTE]
> The **Activate** method is called on a separte thread from your main thread.

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
        // Register AUMID and COM server (for MSIX/sparse package apps, this no-ops)
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
For desktop apps, foreground and background activation is handled identically - your COM activator is called. It's up to your app's code to decide whether to show a window or to simply perform some work and then exit. Therefore, specifying an **activationType** of **background** in your toast content doesn't change the behavior.


## Step 9: Remove and manage notifications

Removing and managing notifications is identical to UWP apps. However, we recommend you use our compat library to obtain a **DesktopNotificationHistoryCompat** so you don't have to worry about providing the AUMID if you're using classic Win32.

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

To deploy and debug your MSIX/sparse package app, see [Run, debug, and test a packaged desktop app](/windows/msix/desktop/desktop-to-uwp-debug).

To deploy and debug your classic Win32 app, you must install your app through the installer once before debugging normally, so that the Start shortcut with your AUMID and CLSID is present. After the Start shortcut is present, you can debug using F5 from Visual Studio.

If your notifications simply fail to appear in your classic Win32 app (and no exceptions are thrown), that likely means the Start shortcut isn't present (install your app via the installer), or the AUMID you used in code doesn't match the AUMID in your Start shortcut.

If your notifications appear but aren't persisted in Action Center (disappearing after the popup is dismissed), that means you haven't implemented the COM activator correctly.

If you've installed both your MSIX/sparse package and classic Win32 app, note that the MSIX/sparse package app will supersede the classic Win32 app when handling toast activations. That means that toasts from the classic Win32 app will still launch the MSIX/sparse package app when clicked. Uninstalling the MSIX/sparse package app will revert activations back to the classic Win32 app.

If you receive `HRESULT 0x800401f0 CoInitialize has not been called.`, be sure to call `CoInitialize(nullptr)` in your app before calling the APIs.

If you receive `HRESULT 0x8000000e A method was called at an unexpected time.` while calling the Compat APIs, that likely means you failed to call the required Register methods (or if a MSIX/sparse package app, you're not currently running your app under the MSIX/sparse context).

If you get numerous `unresolved external symbol` compilation errors, you likely forgot to add `runtimeobject.lib` to the **Additional Dependencies** in step #1 (or you only added it to the Debug configuration and not Release configuration).


## Handling older versions of Windows

If you support Windows 8.1 or lower, you'll want to check at runtime whether you're running on Windows 10 before calling any **DesktopNotificationManagerCompat** APIs or sending any ToastGeneric toasts.

Windows 8 introduced toast notifications, but used the [legacy toast templates](/previous-versions/windows/apps/hh761494(v=win.10)), like ToastText01. Activation was handled by the in-memory **Activated** event on the **ToastNotification** class since toasts were only brief popups that weren't persisted. Windows 10 introduced [interactive ToastGeneric toasts](adaptive-interactive-toasts.md), and also introduced Action Center where notifications are persisted for multiple days. The introduction of Action Center required the introduction of a COM activator, so that your toast can be activated days after you created it.

| OS | ToastGeneric | COM activator | Legacy toast templates |
| -- | ------------ | ------------- | ---------------------- |
| Windows 10 | Supported | Supported | Supported (but won't activate COM server) |
| Windows 8.1 / 8 | N/A | N/A | Supported |
| Windows 7 and lower | N/A | N/A | N/A |

To check if you're running on Windows 10, include the `<VersionHelpers.h>` header and check the **IsWindows10OrGreater** method. If this returns true, continue calling all the methods described in this documentation! 

```cpp
#include <VersionHelpers.h>

if (IsWindows10OrGreater())
{
    // Running Windows 10, continue with sending Windows 10 toasts!
}
```


## Known issues

**FIXED: App doesn't become focused after clicking toast**: In builds 15063 and earlier, foreground rights weren't being transferred to your application when we activated the COM server. Therefore, your app would simply flash when you tried to move it to the foreground. There was no workaround for this issue. We fixed this in builds 16299 and higher.


## Resources

* [Full code sample on GitHub](https://github.com/WindowsNotifications/desktop-toasts)
* [Toast notifications from desktop apps](toast-desktop-apps.md)
* [Toast content documentation](adaptive-interactive-toasts.md)