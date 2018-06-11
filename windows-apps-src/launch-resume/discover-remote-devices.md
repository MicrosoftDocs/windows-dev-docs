---
author: PatrickFarley
title: Discover remote devices
description: Learn how to discover remote devices from your app using Project Rome.
ms.assetid: 5b4231c0-5060-49e2-a577-b747e20cf633
ms.author: pafarley
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Discover remote devices
Your app can use the wireless network, Bluetooth, and cloud connection to discover Windows devices that are signed on with the same Microsoft account as the discovering device. The remote devices do not need to have any special software installed in order to be discoverable.

> [!NOTE]
> This guide assumes you have already been granted access to the Remote Systems feature by following the steps in [Launch a remote app](launch-a-remote-app.md).

## Filter the set of discoverable devices
You can narrow the set of discoverable devices by using a [**RemoteSystemWatcher**](https://msdn.microsoft.com/library/windows/apps/Windows.System.RemoteSystems.RemoteSystemWatcher) with filters. Filters can detect the discovery type (proximal vs. local network vs. cloud connection), device type (desktop, mobile device, Xbox, Hub, and Holographic), and availability status (the status of a device's availability to use Remote System features).

Filter objects must be constructed before or while the **RemoteSystemWatcher** object is initialized, because they are passed as a parameter into its constructor. The following code creates a filter of each type available and then adds them to a list.

> [!NOTE]
> The code in these examples requires that you have a `using Windows.System.RemoteSystems` statement in your file.

[!code-cs[Main](./code/DiscoverDevices/MainPage.xaml.cs#SnippetMakeFilterList)]

> [!NOTE]
> The "proximal" filter value does not guarantee the degree of physical proximity. For scenarios that require reliable physical proximity, use the value [**RemoteSystemDiscoveryType.SpatiallyProximal**](https://docs.microsoft.com/uwp/api/windows.system.remotesystems.remotesystemdiscoverytype) in your filter. Currently, this filter only allows devices that are discovered by Bluetooth. As new discovery mechanisms and protocols which guarantee physical proximity are supported, they will be included here as well.  
There is also a property in the [**RemoteSystem**](https://msdn.microsoft.com/library/windows/apps/Windows.System.RemoteSystems.RemoteSystem) class that indicates whether a discovered device is in fact within physical proximity: [**RemoteSystem.IsAvailableBySpatialProximity**](https://docs.microsoft.com/uwp/api/Windows.System.RemoteSystems.RemoteSystem.IsAvailableByProximity).

> [!NOTE]
> If you intend to discover devices over a local network (determined by your discovery type filter selection), your network needs to be using a "private" or "domain" profile. Your device will not discover other devices over a "public" network.

Once a list of [**IRemoteSystemFilter**](https://msdn.microsoft.com/library/windows/apps/Windows.System.RemoteSystems.IRemoteSystemFilter) objects is created, it can be passed into the constructor of a **RemoteSystemWatcher**.

[!code-cs[Main](./code/DiscoverDevices/MainPage.xaml.cs#SnippetCreateWatcher)]

When this watcher's [**Start**](https://msdn.microsoft.com/library/windows/apps/Windows.System.RemoteSystems.RemoteSystemWatcher.Start) method is called, it will raise the [**RemoteSystemAdded**](https://msdn.microsoft.com/library/windows/apps/Windows.System.RemoteSystems.RemoteSystemWatcher.RemoteSystemAdded) event only if a device is detected that meets all of the following criteria:
* It is discoverable by proximal connection
* It is a desktop or phone
* It is classified as available

From there, the procedure for handling events, retrieving [**RemoteSystem**](https://msdn.microsoft.com/library/windows/apps/Windows.System.RemoteSystems.RemoteSystem) objects, and connecting to remote devices is exactly the same as in [Launch a remote app](launch-a-remote-app.md). In short, the **RemoteSystem** objects are stored as properties of [**RemoteSystemAddedEventArgs**](https://msdn.microsoft.com/library/windows/apps/Windows.System.RemoteSystems.RemoteSystemAddedEventArgs) objects, which are passed in with each **RemoteSystemAdded** event.

## Discover devices by address input
Some devices may not be associated with a user or discoverable with a scan, but they can still be reached if the discovering app uses a direct address. The [**HostName**](https://msdn.microsoft.com/library/windows/apps/windows.networking.hostname.aspx) class is used to represent the address of a remote device. This is often stored in the form of an IP address, but several other formats are allowed (see the [**HostName constructor**](https://msdn.microsoft.com/library/windows/apps/br207118.aspx) for details).

A **RemoteSystem** object is retrieved if a valid **HostName** object is provided. If the address data is invalid, a `null` object reference is returned.

[!code-cs[Main](./code/DiscoverDevices/MainPage.xaml.cs#SnippetFindByHostName)]

## Querying a capability on a remote system

Although separate from discovery filtering, querying device capabilities can be an important part of the discovery process. Using the [**RemoteSystem.GetCapabilitySupportedAsync**](https://docs.microsoft.com/uwp/api/windows.system.remotesystems.remotesystem#Windows_System_RemoteSystems_RemoteSystem_GetCapabilitySupportedAsync_System_String_) method, you can query discovered remote systems for support of certain capabilities such as remote session connectivity or spatial entity (holographic) sharing. See the [**KnownRemoteSystemCapabilities**](https://docs.microsoft.com/uwp/api/windows.system.remotesystems.knownremotesystemcapabilities) class for the list of queryable capabilities.

```csharp
// Check to see if the given remote system can accept LaunchUri requests
bool isRemoteSystemLaunchUriCapable = remoteSystem.GetCapabilitySupportedAsync(KnownRemoteSystemCapabilities.LaunchUri);
```

## Cross-user discovery

Developers can specify the discovery of _all_ devices in proximity to the client device, not just devices registered to the same user. This is implemented through a special **IRemoteSystemFilter**, [**RemoteSystemAuthorizationKindFilter**](https://docs.microsoft.com/uwp/api/windows.system.remotesystems.remotesystemauthorizationkindfilter). It is implemented like the other filter types:

```csharp
// Construct a user type filter that includes anonymous devices
RemoteSystemAuthorizationKindFilter authorizationKindFilter = new RemoteSystemAuthorizationKindFilter(RemoteSystemAuthorizationKind.Anonymous);
// then add this filter to the RemoteSystemWatcher
```

* A [**RemoteSystemAuthorizationKind**](https://docs.microsoft.com/uwp/api/windows.system.remotesystems.remotesystemauthorizationkind) value of **Anonymous** will allow the discovery of all proximal devices, even those from non-trusted users.
* A value of **SameUser** filters the discovery to only devices registered to the same user as the client device. This is the default behavior.

### Checking the Cross-User Sharing settings

In addition to the above filter being specified in your discovery app, the client device itself must also be configured to allow shared experiences from devices signed in with other users. This is a system setting that can be queried with a static method in the **RemoteSystem** class:

```csharp
if (!RemoteSystem.IsAuthorizationKindEnabled(RemoteSystemAuthorizationKind.Anonymous)) {
	// The system is not authorized to connect to cross-user devices. 
	// Inform the user that they can discover more devices if they
	// update the setting to "Anonymous".
}
```

To change this setting, the user must open the **Settings** app. In the **System** > **Shared experiences** > **Share across devices** menu, there is a drop-down box where the user can specify which devices their system can share with.

![shared experiences settings page](images/shared-experiences-settings.png)

## Related topics
* [Connected apps and devices (Project Rome)](connected-apps-and-devices.md)
* [Launch a remote app](launch-a-remote-app.md)
* [Remote Systems API reference](https://msdn.microsoft.com/library/windows/apps/Windows.System.RemoteSystems)
* [Remote Systems sample](https://github.com/Microsoft/Windows-universal-samples/tree/dev/Samples/RemoteSystems)
