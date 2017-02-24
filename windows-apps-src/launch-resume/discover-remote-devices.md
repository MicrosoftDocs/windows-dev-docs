---
author: PatrickFarley
title: Discover remote devices
description: Learn how to discover remote devices from your app using Project "Rome".
ms.assetid: 5b4231c0-5060-49e2-a577-b747e20cf633
ms.author: pafarley
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Discover remote devices
Your app can use the wireless network, Bluetooth, and a cloud connection to discover Windows devices that are signed on with the same Microsoft account as the discovering device. Communal devices that can accept anonymous connections, such as the Surface Hub and Xbox One, are also discoverable. The remote devices do not need to have any special software installed in order to be discoverable.

> [!NOTE]
> This guide assumes you have already been granted access to the Remote Systems feature by following the steps in [Launch a remote app](launch-a-remote-app.md).

## Filter the set of discoverable devices
You can narrow down the set of discoverable devices by using a [**RemoteSystemWatcher**](https://msdn.microsoft.com/library/windows/apps/Windows.System.RemoteSystems.RemoteSystemWatcher) with filters. Filters can detect the discovery type (local network vs. cloud connection), device type (desktop, mobile device, Xbox, Hub, and Holographic), and availability status (the status of a device's availability to use Remote System features).

Filter objects must be constructed before the **RemoteSystemWatcher** object is initialized, because they are passed as a parameter into its constructor. The following code creates a filter of each type available and then adds them to a list.

> [!NOTE]
> The code in these examples assumes that you have a `using Windows.System.RemoteSystems` statement in your file.

[!code-cs[Main](./code/DiscoverDevices/MainPage.xaml.cs#SnippetMakeFilterList)]

Once a list of [**IRemoteSystemFilter**](https://msdn.microsoft.com/library/windows/apps/Windows.System.RemoteSystems.IRemoteSystemFilter) objects is created, it can be passed into the constructor of a **RemoteSystemWatcher**.

[!code-cs[Main](./code/DiscoverDevices/MainPage.xaml.cs#SnippetCreateWatcher)]

When this watcher's [**Start**](https://msdn.microsoft.com/library/windows/apps/Windows.System.RemoteSystems.RemoteSystemWatcher.Start) method is called, it will raise the [**RemoteSystemAdded**](https://msdn.microsoft.com/library/windows/apps/Windows.System.RemoteSystems.RemoteSystemWatcher.RemoteSystemAdded) event only if a device is detected that meets all of the following criteria:
* It is discoverable by proximal connection
* It is a desktop or phone
* It is classified as available

From there, the procedure for handling events, retrieving [**RemoteSystem**](https://msdn.microsoft.com/library/windows/apps/Windows.System.RemoteSystems.RemoteSystem) objects, and connecting to remote devices is exactly the same as in [Launch a remote app](launch-a-remote-app.md). In short, the **RemoteSystem** objects are stored as properties of [**RemoteSystemAddedEventArgs**](https://msdn.microsoft.com/library/windows/apps/Windows.System.RemoteSystems.RemoteSystemAddedEventArgs) objects, which are parameters of each **RemoteSystemAdded** event.

## Discover devices by address input
Some devices may not be associated with a user or discoverable with a scan, but they can still be reached if the discovering app uses a direct address. The [**HostName**](https://msdn.microsoft.com/library/windows/apps/windows.networking.hostname.aspx) class is used to represent the address of a remote device. This is often stored in the form of an IP address, but several other formats are allowed (see the [**HostName constructor**](https://msdn.microsoft.com/library/windows/apps/br207118.aspx) for details).

A **RemoteSystem** object is retrieved if a valid **HostName** object is provided. If the address data is invalid, a `null` object reference is returned.

[!code-cs[Main](./code/DiscoverDevices/MainPage.xaml.cs#SnippetFindByHostName)]

## Related topics
[Connected apps and devices (Project "Rome")](connected-apps-and-devices.md)  
[Launch a remote app](launch-a-remote-app.md)  
[Remote Systems API reference](https://msdn.microsoft.com/library/windows/apps/Windows.System.RemoteSystems)  
[Remote Systems sample](https://github.com/Microsoft/Windows-universal-samples/tree/dev/Samples/RemoteSystems ) demonstrates how to discover a remote system, launch an app on a remote system, and use app services to send messages between apps running on two systems.
