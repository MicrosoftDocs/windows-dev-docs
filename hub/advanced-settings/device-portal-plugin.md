---
title: Write a custom plugin for Windows Device Portal
description: Write a packaged app (UWP or Windows App SDK) that uses Windows Device Portal to host a web page and provide app diagnostic information.
ms.date: 07/06/2026
ms.topic: how-to
ms.localizationpriority: medium
author: GrantMeStrength
ms.author: jken
---

# Write a custom plugin for Windows Device Portal

You can write a packaged app (UWP or packaged Win32 using Windows App SDK) that uses the Windows Device Portal to host a web page and provide diagnostic information. The Device Portal plug-in functionality is implemented using the `windows.devicePortalProvider` extension, which is available to all packaged apps regardless of the UI framework.

> [!NOTE]
> This article is aimed at developers building diagnostic or management tools for **IoT Enterprise devices, kiosk deployments, or other managed Windows environments** where Device Portal is used for remote monitoring. If you are building a standard Windows 11 desktop app, you are unlikely to need a Device Portal plugin — consider using [App diagnostics](/windows/uwp/launch-resume/app-diagnostics) or Windows Performance Analyzer instead.

Device Portal plugin support is available on Windows 10 and later.

## Create the project

Create a new app project in Microsoft Visual Studio. Go to **File > New > Project** and select one of the following project templates:

- For a UWP app: **Blank App (Windows Universal) for C#**
- For a WinUI 3 desktop app: **Blank App, Packaged (WinUI 3 in Desktop)**

In the **Configure your new project** dialog box, name the project **DevicePortalProvider**, and then click **Create**. This will be the app that contains the app service.

## Add the devicePortalProvider capability

Add the `rescap:devicePortalProvider` capability to your app's `Package.appxmanifest` file.

> [!NOTE]
> The `rescap` (Restricted Capability) namespace must be declared before it can be used. See [App capability declarations](../apps/package-and-deploy/app-capability-declarations.md) for details.

In **Solution Explorer**, double-click `Package.appxmanifest` to open the manifest designer, then switch to the **Capabilities** tab and check **Device Portal Provider** if it's listed, or manually add it by editing the XML.

```xml
<Capabilities>
  <rescap:Capability Name="devicePortalProvider" />
</Capabilities>
```

## Set up the app service

Device Portal plugins use [app services](/windows/uwp/launch-resume/how-to-create-and-consume-an-app-service) to communicate between the plugin host process and Device Portal. Open `Package.appxmanifest`, switch to the **Declarations** tab, add a **Windows Device Portal Provider** declaration, and configure it with the service name.

Alternatively, edit the XML directly:

```xml
<Applications>
  <Application Id="App" ...>
    ...
    <Extensions>
      <uap3:Extension Category="windows.appService" EntryPoint="DevicePortalProvider.DevicePortalPluginService">
        <uap3:AppService Name="com.microsoft.myapp.devicePortalService" SupportsRemoteSystems="true"/>
      </uap3:Extension>
      <uap4:Extension Category="windows.devicePortalProvider">
        <uap4:DevicePortalProvider DisplayName="My App Plugin" AppServiceName="com.microsoft.myapp.devicePortalService" HandlerRoute="/MyCustomApp/"/>
      </uap4:Extension>
    </Extensions>
  </Application>
</Applications>
```

> [!IMPORTANT]
> The `uap3` and `uap4` XML namespace prefixes must be declared in the root `<Package>` element of your manifest, and added to the `IgnorableNamespaces` attribute:
>
> ```xml
> <Package
>   ...
>   xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3"
>   xmlns:uap4="http://schemas.microsoft.com/appx/manifest/uap/windows10/4"
>   IgnorableNamespaces="... uap3 uap4">
> ```

The following attributes require configuration specific to your app:

| Attribute | Description |
|-----------|-------------|
| `EntryPoint` | Specifies the class that implements the app service. |
| `Name` (AppService) | The unique name of the app service. This name is used as the `AppServiceName` attribute in the `DevicePortalProvider` element. |
| `DisplayName` | The name that will appear in the Device Portal UI. |
| `HandlerRoute` | The URL path prefix that Device Portal uses to route requests to your plugin. All requests whose path begins with this value are forwarded to your app service. The route must start and end with slashes. |

## Add the app service code

Add a Windows Runtime Component project to your solution to host the background app service:

1. In **Solution Explorer**, right-click the solution and select **Add > New Project**.
2. Select **Windows Runtime Component (Universal Windows)** and name it **DevicePortalProvider**.
3. Right-click the new project and select **Add > New Item > App Service**.

App service code implementation details are in the [Create and consume an app service](/windows/uwp/launch-resume/how-to-create-and-consume-an-app-service) article.

## Implement your Device Portal handler

Here's a sample implementation of a Device Portal plugin app service:

```csharp
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.Storage;
using Windows.System.Diagnostics.DevicePortal;
using Windows.Web.Http;

namespace DevicePortalProvider
{
    public sealed class DevicePortalPluginService : IBackgroundTask
    {
        private BackgroundTaskDeferral _taskDeferral;
        private DevicePortalConnection _connection;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            _taskDeferral = taskInstance.GetDeferral();
            taskInstance.Canceled += OnTaskCanceled;

            var triggerDetails = taskInstance.TriggerDetails as AppServiceTriggerDetails;
            _connection = DevicePortalConnection.GetForAppServiceConnection(triggerDetails.AppServiceConnection);
            _connection.RequestReceived += _connection_RequestReceived;
        }

        private void OnTaskCanceled(IBackgroundTaskInstance taskInstance, BackgroundTaskCancellationReason reason)
        {
            _taskDeferral.Complete();
        }

        private async void _connection_RequestReceived(DevicePortalConnection sender, DevicePortalConnectionRequestReceivedEventArgs args)
        {
            // Handle the incoming request and send a response
        }
    }
}
```

## Handle incoming requests

Your plugin handler receives all GET, POST, DELETE, and PUT requests that Device Portal routes to your `HandlerRoute`. A typical implementation:

```csharp
private async void _connection_RequestReceived(
    DevicePortalConnection sender, 
    DevicePortalConnectionRequestReceivedEventArgs args)
{
    var request = args.RequestMessage;
    var response = args.ResponseMessage;

    // Use the request's path to determine what data to return
    // The path must begin with the HandlerRoute declared in the manifest (e.g. "/MyCustomApp/")
    if (request.RequestUri.LocalPath.Equals("/MyCustomApp/info", StringComparison.OrdinalIgnoreCase))
    {
        response.Content = new HttpStringContent(
            "{ \"appVersion\": \"1.0\" }",
            Windows.Storage.Streams.UnicodeEncoding.Utf8,
            "application/json");
        response.StatusCode = HttpStatusCode.Ok;
    }
}
```

## Serve static content

If you want to serve web content (HTML, CSS, JavaScript) from within your app's package, you can place those files in a folder and respond to requests with the file content.

```csharp
private async void _connection_RequestReceived(
    DevicePortalConnection sender,
    DevicePortalConnectionRequestReceivedEventArgs args)
{
    var storageFile = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(@"data\content.html");
    string htmlContent = await FileIO.ReadTextAsync(storageFile);

    args.ResponseMessage.Content = new HttpStringContent(
        htmlContent,
        Windows.Storage.Streams.UnicodeEncoding.Utf8,
        "text/html");
    args.ResponseMessage.StatusCode = HttpStatusCode.Ok;
}
```

## Run the app service as System

In some cases, for complete testing of the Device Portal plugin, you may need to run the plugin as the System account. See the [MSDN Magazine article](/archive/msdn-magazine/2017/october/windows-device-portal-write-a-windows-device-portal-packaged-plug-in) for details.

## See also

- [Windows Device Portal overview](device-portal.md)
- [App capability declarations](../apps/package-and-deploy/app-capability-declarations.md)
- [Create and consume an app service](/windows/uwp/launch-resume/how-to-create-and-consume-an-app-service)
- [Windows Device Portal core API reference](/windows/uwp/debug-test-perf/device-portal-api-core)
