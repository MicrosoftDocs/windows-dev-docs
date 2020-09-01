---
title: Communicate with a remote app service
description: Exchange messages with an app service running on a remote device using Project Rome.
ms.assetid: a0261e7a-5706-4f9a-b79c-46a3c81b136f
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, connected devices, remote systems, rome, project rome, background task, app service
ms.localizationpriority: medium
---
# Communicate with a remote app service

In addition to launching an app on a remote device using a URI, you can run and communicate with *app services* on remote devices as well. Any Windows-based device can be used as either the client or host device. This gives you an almost limitless number of ways to interact with connected devices without needing to bring an app to the foreground.

## Set up the app service on the host device
In order to run an app service on a remote device, you must already have a provider of that app service installed on that device. This guide will use CSharp version of the [Random Number Generator app service sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/AppServices), which is available on the [Windows universal samples repo](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/AppServices). For instructions on how to write your own app service, see [Create and consume an app service](how-to-create-and-consume-an-app-service.md).

Whether you are using an already-made app service or writing your own, you will need to make a few edits in order to make the service compatible with remote systems. In Visual Studio, go to the app service provider's project (called "AppServicesProvider" in the sample) and select its _Package.appxmanifest_ file. Right-click and select **View Code** to view the full contents of the file. Create an **Extensions** element inside of the main **Application** element (or find it if it already exists). Then create an **Extension** to define the project as an app service and reference its parent project.

``` xml
...
<Extensions>
    <uap:Extension Category="windows.appService" EntryPoint="RandomNumberService.RandomNumberGeneratorTask">
        <uap3:AppService Name="com.microsoft.randomnumbergenerator"/>
    </uap:Extension>
</Extensions>
...
```

Next to the **AppService** element, add the **SupportsRemoteSystems** attribute:

``` xml
...
<uap3:AppService Name="com.microsoft.randomnumbergenerator" SupportsRemoteSystems="true"/>
...
```

In order to use elements in this **uap3** namespace, you must add the namespace definition at the top of the manifest file if it isn't already there.

```xml
<?xml version="1.0" encoding="utf-8"?>
<Package
  xmlns="http://schemas.microsoft.com/appx/manifest/foundation/windows10"
  xmlns:mp="http://schemas.microsoft.com/appx/2014/phone/manifest"
  xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10"
  xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3">
  ...
</Package>
```

Then build your app service provider project and deploy it to the host device(s).

## Target the app service from the client device
The device from which the remote app service is to be called needs an app with Remote Systems functionality. This can be added into the same app that provides the app service on the host device (in which case you would install the same app on both devices), or implemented in a completely different app.

The following **using** statements are needed for the code in this section to run as-is:

:::code language="csharp" source="~/../snippets-windows/windows-uwp/launch-resume/RemoteAppService/cs/MainPage.xaml.cs" id="SnippetUsings":::


You must first instantiate an [**AppServiceConnection**](/uwp/api/Windows.ApplicationModel.AppService.AppServiceConnection) object, just as if you were to call an app service locally. This process is covered in more detail in [Create and consume an app service](how-to-create-and-consume-an-app-service.md). In this example, the app service to target is the Random Number Generator service.

> [!NOTE]
> It is assumed that a [RemoteSystem](/uwp/api/Windows.System.RemoteSystems.RemoteSystem) object has already been acquired by some means within the code that would call the following method. See [Launch a remote app](launch-a-remote-app.md) for instructions on how to set this up.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/launch-resume/RemoteAppService/cs/MainPage.xaml.cs" id="SnippetAppService":::

Next, a [**RemoteSystemConnectionRequest**](/uwp/api/Windows.System.RemoteSystems.RemoteSystemConnectionRequest) object is created for the intended remote device. It is then used to open the **AppServiceConnection** to that device. Note that in the example below, error handling and reporting is greatly simplified for brevity.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/launch-resume/RemoteAppService/cs/MainPage.xaml.cs" id="SnippetRemoteConnection":::

At this point, you should have an open connection to an app service on a remote machine.

## Exchange service-specific messages over the remote connection

From here, you can send and receive messages to and from the service in the form of [**ValueSet**](/uwp/api/windows.foundation.collections.valueset) objects (for more information, see [Create and consume an app service](how-to-create-and-consume-an-app-service.md)). The Random number generator service takes two integers with the keys `"minvalue"` and `"maxvalue"` as inputs, randomly selects an integer within their range, and returns it to the calling process with the key `"Result"`.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/launch-resume/RemoteAppService/cs/MainPage.xaml.cs" id="SnippetSendMessage":::

Now you have connected to an app service on a targeted host device, run an operation on that device, and received data to your client device in response.

## Related topics

[Connected apps and devices (Project Rome) overview](connected-apps-and-devices.md)  
[Launch a remote app](launch-a-remote-app.md)  
[Create and consume an app service](how-to-create-and-consume-an-app-service.md)  
[Remote Systems API reference](/uwp/api/Windows.System.RemoteSystems)  
[Remote Systems sample](https://github.com/Microsoft/Windows-universal-samples/tree/dev/Samples/RemoteSystems)
