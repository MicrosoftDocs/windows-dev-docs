---
title: Create and consume an app service in Windows App SDK
description: Learn how to create an app service that provides functionality to other apps, and how to consume that service from a Windows App SDK app.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/10/2026
---

# Create and consume an app service in Windows App SDK

App services let your app provide services to other apps. An app service runs as a background task in a separate process and exposes a request/response communication channel. Other apps connect to the service using [AppServiceConnection](/uwp/api/windows.applicationmodel.appservice.appserviceconnection).

> [!IMPORTANT]
> App services require **MSIX package identity**. Both the provider and consumer apps must be packaged. Unpackaged desktop apps cannot host or connect to app services.

> [!NOTE]
> Windows App SDK apps can host an app service only as an **out-of-process** background task. `Microsoft.UI.Xaml.Application` doesn't provide an `OnBackgroundActivated` override, and [ExtendedActivationKind](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.extendedactivationkind) doesn't include an app-service activation kind, so a Windows App SDK app can't receive app-service activation directly in its own process the way a UWP app could. See [In-process app services aren't supported](in-process-app-service.md) for details.

## How app services work

An app service consists of:

1. **A provider app** that declares and implements the service.
2. **A consumer app** that connects to the service and sends requests.

Communication uses [ValueSet](/uwp/api/windows.foundation.collections.valueset) objects for request and response data.

## Create the app service provider

### Step 1: Declare the app service in the manifest

Add an `appService` extension to the provider app's `Package.appxmanifest`, and specify the entry point of the background task project that implements the service:

```xml
<Extensions>
  <uap:Extension Category="windows.appService">
    <uap:AppService Name="com.example.myappservice"
                    EntryPoint="MyAppService.ServiceTask" />
  </uap:Extension>
</Extensions>
```

### Step 2: Implement the service

Create a separate Windows Runtime Component project and implement `IBackgroundTask`. The background task runs out-of-process, so this pattern works whether the provider app is built with the Windows App SDK or UWP:

```csharp
// MyAppService.ServiceTask, in a separate Windows Runtime Component project
public sealed class ServiceTask : IBackgroundTask
{
    private BackgroundTaskDeferral _deferral;

    public void Run(IBackgroundTaskInstance taskInstance)
    {
        _deferral = taskInstance.GetDeferral();
        taskInstance.Canceled += OnTaskCanceled;

        var details = taskInstance.TriggerDetails as AppServiceTriggerDetails;
        if (details != null)
        {
            details.AppServiceConnection.RequestReceived += OnRequestReceived;
        }
    }

    private async void OnRequestReceived(AppServiceConnection sender,
        AppServiceRequestReceivedEventArgs args)
    {
        var deferral = args.GetDeferral();
        var response = new ValueSet();
        response.Add("result", "Hello from the service!");
        await args.Request.SendResponseAsync(response);
        deferral.Complete();
    }

    private void OnTaskCanceled(IBackgroundTaskInstance sender,
        BackgroundTaskCancellationReason reason)
    {
        _deferral?.Complete();
    }
}
```

## Consume the app service

From the consumer app, create an `AppServiceConnection` and send requests:

```csharp
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;

private async Task CallAppServiceAsync()
{
    var connection = new AppServiceConnection
    {
        AppServiceName = "com.example.myappservice",
        PackageFamilyName = "ProviderApp_1234567890abc"
    };

    var status = await connection.OpenAsync();
    if (status != AppServiceConnectionStatus.Success)
    {
        // Handle connection failure
        return;
    }

    var request = new ValueSet
    {
        { "command", "add" },
        { "a", 5 },
        { "b", 3 }
    };

    var response = await connection.SendMessageAsync(request);
    if (response.Status == AppServiceResponseStatus.Success)
    {
        int result = (int)response.Message["result"];
        // result is 8
    }

    connection.Dispose();
}
```

> [!TIP]
> Find the `PackageFamilyName` for the provider app by running `Get-AppxPackage -Name *ProviderApp*` in PowerShell, or check the provider app's package manifest.

## Why the service must run out-of-process

An app service's background task always runs in its own process (or the provider app's process, for UWP providers only). For a Windows App SDK provider app, the background task must run out-of-process because there's no supported way for the app's own process to receive app-service activation. For more information, see [In-process app services aren't supported](in-process-app-service.md).

## Related content

- [In-process app services aren't supported](in-process-app-service.md)
- [App extensions](app-extensions.md)
- [AppServiceConnection class](/uwp/api/windows.applicationmodel.appservice.appserviceconnection)
- [Extensibility overview](extensibility-overview.md)
