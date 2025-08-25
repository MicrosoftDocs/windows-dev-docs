# Quickstart: Push notifications in the Windows App SDK in an existing application

## Prerequisites

- [Start developing Windows apps](../../../get-started/start-here.md)
- Either [Create a new project that uses the Windows App SDK](../../../winui/winui3/create-your-first-winui3-app.md) OR [Use the Windows App SDK in an existing project](../../../windows-app-sdk/use-windows-app-sdk-in-existing-project.md)
- An [Azure Account](https://azure.microsoft.com/free/) is required in order to use Windows App SDK push notifications.
- Read [Push notifications overview](../../../windows-app-sdk/notifications/push-notifications/index.md)
- Install the [Desktop Development with C++ workload](cpp/build/vscpp-step-0-installation#step-4---choose-workloads) to use the C++ console app template

>[!IMPORTANT]
> C# and packaged app examples are available on the [Windows App SDK Samples repository](https://github.com/microsoft/WindowsAppSDK-Samples). Make sure to check out [the branch with your preferred version of the Windows App SDK](https://github.com/microsoft/WindowsAppSDK-Samples/branches) for the guide that best matches your project.

>[!NOTE]
> Something about the other quickstart tutorial

## Configure your app's identity in Azure Active Directory (AAD)

Push notifications in Windows App SDK use identities from Azure Active Directory (AAD). Azure credentials are required when requesting a WNS Channel URI and when requesting access tokens in order to send push notifications. **Note**: We do **NOT** support using Windows App SDK push notifications with Microsoft Partner Center.

See the [Configure your app's identity in Azure Active Directory (AAD)](/push-quickstart#configure-your-apps-identity-in-azure-active-directory-aad)) section of the PushNotification quickstart guide [FULL NAME HERE] for more instructions.

## Configure your app to receive push notifications

### Step 1: Add Windows App SDK and required NuGet packages

Next, right click on the Solution Explorer and select **Manage NuGet Packages**.

In the Package Manager, add the following packages:
* Microsoft.WindowsAppSDK (minimum version 1.1.0)
* Microsoft.Windows.SDK.BuildTools (minimum version 10.0.22000.194)
* Microsoft.Windows.CppWinRT, (minimum version 2.0.210930.14)
* Microsoft.Windows.ImplementationLibrary, (minimum version 1.0.210930.1)

> [!NOTE] 
> If this is the first time you are using Windows App SDK in your project and it is packaged with external location or unpackaged, make sure the Windows App SDK is initialized either with auto-initialization (set the project property `<WindowsPackageType>None</WindowsPackageType>` in the `<PropertyGroup Label="Globals">` of the `{yourprojectname}.vcxproj` file) or use the bootstrapper API.
>
> Otherwise, the app will throw `System.Runtime.InteropServices.COMException (0x80040154): Class not registered (0x80040154 (REGDB_E_CLASSNOTREG))` and will not run.
>
>See [Use the Windows App SDK runtime for apps packaged with external location or unpackaged](/windows/apps/windows-app-sdk/use-windows-app-sdk-run-time) for more details.

### Step 2: Add namespaces

Next, add the namespace for Windows App SDK push notifications `Microsoft.Windows.PushNotifications`.

```cpp
#include <winrt/Microsoft.Windows.PushNotifications.h>

using namespace winrt::Microsoft::Windows::PushNotifications;
```

If you get a "Can't find Microsoft.Windows.PushNotifications" error, that likely means the header files haven't been generated. Build the app without the namespaces first (keep the includes without an error), then try adding them in again.

### Step 2: Add your COM activator to your app's manifest

> [!IMPORTANT]
> If your app is unpackaged (that is, it lacks package identity at runtime), then skip to **Step 3: Register for and respond to push notifications on app startup**.

If your app is packaged (including packaged with external location):
Open your **Package.appxmanifest**. Add the following inside the `<Application>` element. Replace the `Id`, `Executable`, and `DisplayName` values with those specific to your app.


```xaml
<!--Packaged apps only-->
<!--package.appxmanifest-->

<Package
  ...
  xmlns:com="http://schemas.microsoft.com/appx/manifest/com/windows10"
  ...
  <Applications>
    <Application>
      ...
      <Extensions>

        <!--Register COM activator-->    
        <com:Extension Category="windows.comServer">
          <com:ComServer>
              <com:ExeServer Executable="SampleApp\SampleApp.exe" DisplayName="SampleApp" Arguments="----WindowsAppRuntimePushServer:">
                <com:Class Id="[Your app's Azure AppId]" DisplayName="Windows App SDK Push" />
            </com:ExeServer>
          </com:ComServer>
        </com:Extension>
    
      </Extensions>
    </Application>
  </Applications>
 </Package>    
```

### Step 3: Register for and respond to push notifications on app startup

All the code for the rest of the tutorial can be found in the code block below.

Update your app's `main()` method to add the following:

1. Register your app to receive push notifications by calling [PushNotificationManager::Default().Register()](/windows/windows-app-sdk/api/winrt/microsoft.windows.pushnotifications.pushnotificationmanager.register).
1. Check the source of the activation request by calling [AppInstance::GetCurrent().GetActivatedEventArgs()](). If the activation was triggered from a push notification, respond based on the notification's payload.

> [!IMPORTANT]
> You must call **PushNotificationManager::Default().Register** before calling [AppInstance.GetCurrent.GetActivatedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.getactivatedeventargs).

#### Adding foreground event handlers

To handle an event in the foreground, register a handler for [PushNotificationManager.PushReceived](/windows/windows-app-sdk/api/winrt/microsoft.windows.pushnotifications.pushnotificationmanager.pushreceived).

>[!IMPORTANT]
> You also must register any [PushNotificationManager.PushReceived](/windows/windows-app-sdk/api/winrt/microsoft.windows.pushnotifications.pushnotificationmanager.pushreceived) event handlers before calling PushNotificationManager.Register(). Otherwise, the following runtime exception will be thrown:
> ```
> System.Runtime.InteropServices.COMException: Element not found. Must register event handlers before calling Register().
> ```

#### Add the PushNotificationManager::IsSupported() check

Next, add a check if the PushNotification APIs are supported with [PushNotificationManager.IsSupported()](/windows/windows-app-sdk/api/winrt/microsoft.windows.pushnotifications.pushnotificationmanager.issupported). If not, we recommend that you use polling or your own custom socket implementation.

Now that there's confirmed push notification support, add in behavior based on [PushNotificationReceivedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.windows.pushnotifications.pushnotificationreceivedeventargs):

### Step 4: Request a WNS Channel URI and register it with the WNS server

WNS Channel URIs are the HTTP endpoints for sending push notifications. Each client must request a Channel URI and register it with the WNS server to receive push notifications.

> [!NOTE]
> WNS Channel URIs expire after 30 days.

```cpp
auto channelOperation{ PushNotificationManager::Default().CreateChannelAsync(winrt::guid("[Your app's Azure ObjectID]")) };
```

If you're following the tutorial code, add your Azure Object ID here:
```cpp
// To obtain an AAD RemoteIdentifier for your app,
// follow the instructions on https://learn.microsoft.com/azure/active-directory/develop/quickstart-register-app
winrt::guid remoteId{ "00000000-0000-0000-0000-000000000000" }; // Replace this with your own Azure ObjectId
```

The **PushNotificationManager** will attempt to create a Channel URI, retrying automatically for no more than 15 minutes. Create an event handler to wait for the call to complete. Once the call is complete, if it was successful, register the URI with the WNS  server.

> [!NOTE]
> To use LOG_HR_MSG included in the following code chunks, use Package Manager to import `Microsoft.Windows.ImplementationLibrary`, version 1.0.220914.1.

Here's the full sample code:

```cpp
#include <iostream>
#include <winrt/Microsoft.Windows.PushNotifications.h>
#include <winrt/Windows.Foundation.h>
#include <winrt/Microsoft.Windows.AppLifecycle.h>
#include <winrt/Windows.ApplicationModel.Background.h>
#include <wil/cppwinrt.h>
#include <wil/result.h>

using namespace winrt::Microsoft::Windows::PushNotifications;
using namespace winrt::Windows::Foundation;
using namespace winrt::Microsoft::Windows::AppLifecycle;

// To obtain an AAD RemoteIdentifier for your app,
// follow the instructions on https://learn.microsoft.com/azure/active-directory/develop/quickstart-register-app
winrt::guid remoteId{ "00000000-0000-0000-0000-000000000000" }; // Replace this with your own Azure ObjectId

winrt::Windows::Foundation::IAsyncOperation<PushNotificationChannel> RequestChannelAsync()
{
    auto channelOperation = PushNotificationManager::Default().CreateChannelAsync(remoteId);

    // Set up the in-progress event handler
    channelOperation.Progress(
        [](auto&& sender, auto&& args)
        {
            if (args.status == PushNotificationChannelStatus::InProgress)
            {
                // This is basically a noop since it isn't really an error state
                std::cout << "Channel request is in progress." << std::endl << std::endl;
            }
            else if (args.status == PushNotificationChannelStatus::InProgressRetry)
            {
                LOG_HR_MSG(
                    args.extendedError,
                    "The channel request is in back-off retry mode because of a retryable error! Expect delays in acquiring it. RetryCount = %d",
                    args.retryCount);
            }
        });

    auto result = co_await channelOperation;

    if (result.Status() == PushNotificationChannelStatus::CompletedSuccess)
    {
        auto channelUri = result.Channel().Uri();

        std::cout << "channelUri: " << winrt::to_string(channelUri.ToString()) << std::endl << std::endl;

        auto channelExpiry = result.Channel().ExpirationTime();

        // Caller's responsibility to keep the channel alive
        co_return result.Channel();
    }
    else if (result.Status() == PushNotificationChannelStatus::CompletedFailure)
    {
        LOG_HR_MSG(result.ExtendedError(), "We hit a critical non-retryable error with channel request!");
        co_return nullptr;
    }
    else
    {
        LOG_HR_MSG(result.ExtendedError(), "Some other failure occurred.");
        co_return nullptr;
    }

};

PushNotificationChannel RequestChannel()
{
    auto task = RequestChannelAsync();
    if (task.wait_for(std::chrono::seconds(300)) != AsyncStatus::Completed)
    {
        task.Cancel();
        return nullptr;
    }

    auto result = task.GetResults();
    return result;
}

void SubscribeForegroundEventHandler()
{
    winrt::event_token token{ PushNotificationManager::Default().PushReceived([](auto const&, PushNotificationReceivedEventArgs const& args)
    {
        auto payload{ args.Payload() };

        std::string payloadString(payload.begin(), payload.end());
        std::cout << "\nPush notification content received in the FOREGROUND: " << payloadString << std::endl;
    }) };

    std::cout << "Push notification foreground event handler registered." << std::endl;
}

int main()
{
    // Set up an event handler, so we can receive notifications in the foreground while the app is running.
    // You must register notification event handlers before calling Register(). Otherwise, the following runtime
    // exception will be thrown: System.Runtime.InteropServices.COMException: 'Element not found. Must register
    // event handlers before calling Register().'
    SubscribeForegroundEventHandler();

    // Register the app for push notifications.
    PushNotificationManager::Default().Register();

    auto args{ AppInstance::GetCurrent().GetActivatedEventArgs() };
    switch (args.Kind())
    {
        case ExtendedActivationKind::Launch:
        {
            std::cout << "App launched by user or from the debugger." << std::endl;
            if (PushNotificationManager::IsSupported())
            {
                std::cout << "Push notifications are supported on this device." << std::endl;

                // Request a WNS Channel URI which can be passed off to an external app to send notifications to.
                // The WNS Channel URI uniquely identifies this app for this user and device.
                PushNotificationChannel channel{ RequestChannel() };
                if (!channel)
                {
                    std::cout << "\nThere was an error obtaining the WNS Channel URI" << std::endl;

                    if (remoteId == winrt::guid{ "00000000-0000-0000-0000-000000000000" })
                    {
                        std::cout << "\nThe ObjectID has not been set. Refer to the readme file accompanying this sample\nfor the instructions on how to obtain and setup an ObjectID" << std::endl;
                    }
                }

                std::cout << "\nPress 'Enter' at any time to exit App." << std::endl;
                std::cin.ignore();
            }
            else
            {
                std::cout << "Push notifications are NOT supported on this device." << std::endl;
                std::cout << "App implements its own custom socket here to receive messages from the cloud since Push APIs are unsupported." << std::endl;
                std::cin.ignore();
            }
        }
        break;

        case ExtendedActivationKind::Push:
        {
            std::cout << "App activated via push notification." << std::endl;
            PushNotificationReceivedEventArgs pushArgs{ args.Data().as<PushNotificationReceivedEventArgs>() };

            // Call GetDeferral to ensure that code runs in low power
            auto deferral{ pushArgs.GetDeferral() };

            auto payload{ pushArgs.Payload() };

            // Do stuff to process the raw notification payload
            std::string payloadString(payload.begin(), payload.end());
            std::cout << "\nPush notification content received in the BACKGROUND: " << payloadString.c_str() << std::endl;
            std::cout << "\nPress 'Enter' to exit the App." << std::endl;

            // Call Complete on the deferral when finished processing the payload.
            // This removes the override that kept the app running even when the system was in a low power mode.

            deferral.Complete();
            std::cin.ignore();
        }
        break;

        default:
            std::cout << "\nUnexpected activation type" << std::endl;
            std::cout << "\nPress 'Enter' to exit the App." << std::endl;
            std::cin.ignore();
            break;
    }
}

```

### Step 5: Build and install your app

Use Visual Studio to build and install your app. Right-click on the solution file in the Solution Explorer and select **Deploy**. Visual Studio will build your app and install it on your machine. You can run the app by launching it via the Start Menu or the Visual Studio debugger.

The tutorial code's console will look like this:
![working sample console](images/console_complete.png). You'll need the token to [send a push notification to your app](#send-a-push-notification-to-your-app).
