---
author: PatrickFarley
title: Launch an app on a remote device
description: Learn how to launch an app on a remote device using Project Rome.
ms.author: pafarley
ms.date: 02/12/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: 54f6a33d-a3b5-4169-8664-653dbab09175
ms.localizationpriority: medium
---

# Launch an app on a remote device

This article explains how to launch a Windows app on a remote device.

Starting in Windows 10, version 1607, a UWP app can launch a UWP app or Windows desktop application remotely on another device that is also running Windows 10, version 1607 or later, provided that both devices are signed on with the same Microsoft Account (MSA). This is the simplest use case of Project Rome.

The remote launch feature enables task-oriented user experiences; a user can start a task on one device and finish it on another. For example, if the user is listening to music on their phone in their car, they could then hand playback functionality over to their Xbox One when they arrive at home. Remote launch allows apps to pass contextual data to the remote app being launched, in order to pick up where the task was left off.

## Preliminary setup

### Add the remoteSystem capability

In order for your app to launch an app on a remote device, you must add the `remoteSystem` capability to your app package manifest. You can use the package manifest designer to add it by selecting **Remote System** on the **Capabilities** tab, or you can manually add the following line to your project's _Package.appxmanifest_ file.

``` xml
<Capabilities>
   <uap3:Capability Name="remoteSystem"/>
</Capabilities>
```

### Enable cross-device sharing

Additionally, the client device must be set to allow cross-device sharing. This setting, which is accessed in **Settings**: **System** > **Shared experiences** > **Share across devices**, is enabled by default. 

![shared experiences settings page](images/shared-experiences-settings.png)

## Find a remote device

You must first find the device that you want to connect with. [Discover remote devices](discover-remote-devices.md) discusses how to do this in detail. We'll use a simple approach here that forgoes filtering by device or connectivity type. We will create a remote system watcher that looks for remote devices, and write handlers for the events that are raised when devices are discovered or removed. This will provide us with a collection of remote devices.

The code in these examples requires that you have a `using Windows.System.RemoteSystems` statement in your class file(s).

[!code-cs[Main](./code/RemoteLaunchScenario/MainPage.xaml.cs#SnippetBuildDeviceList)]

The first thing you must do before making a remote launch is call `RemoteSystem.RequestAccessAsync()`. Check the return value to make sure your app is allowed to access remote devices. One reason this check could fail is if you haven't added the `remoteSystem` capability to your app.

The system watcher event handlers are called when a device that we can connect with is discovered or is no longer available. We will use these event handlers to keep an updated list of devices that we can connect to.

[!code-cs[Main](./code/RemoteLaunchScenario/MainPage.xaml.cs#SnippetEventHandlers)]


We will track the devices by remote system ID using a **Dictionary**. An **ObservableCollection** is used to hold the list of devices that we can enumerate. An **ObservableCollection** also makes it easy to bind the list of devices to UI, though we won't do that in this example.

[!code-cs[Main](./code/RemoteLaunchScenario/MainPage.xaml.cs#SnippetMembers)]

Add a call to `BuildDeviceList()` in your app startup code before you attempt to launch a remote app.

## Launch an app on a remote device

Launch an app remotely by passing the device you wish to connect with to the [**RemoteLauncher.LaunchUriAsync**](https://msdn.microsoft.com/library/windows/apps/windows.system.remotelauncher.launchuriasync.aspx) API. There are three overloads for this method. The simplest, which this example demonstrates, specifies the URI that will activate the app on the remote device. In this example the URI opens the Maps app on the remote machine with a 3D view of the Space Needle.

Other **RemoteLauncher.LaunchUriAsync** overloads allow you to specify options such as the URI of the web site to view if no appropriate app can be launched on the remote device, and an optional list of package family names that could be used to launch the URI on the remote device. You can also provide data in the form of key/value pairs. You might pass data to the app you are activating to provide context to the remote app, such as the name of the song to play and the current playback location when you hand off playback from one device to another.

In practical scenarios, you might provide UI to select the device you want to target. But to simplify this example, we'll just use the first remote device on the list.

[!code-cs[Main](./code/RemoteLaunchScenario/MainPage.xaml.cs#SnippetRemoteUriLaunch)]

The [**RemoteLaunchUriStatus**](https://msdn.microsoft.com/library/windows/apps/windows.system.remotelaunchuristatus.aspx) object that is returned from **RemoteLauncher.LaunchUriAsync()** provides information about whether the remote launch succeeded, and if not, the reason why.

## Related topics

[Remote Systems API reference](https://msdn.microsoft.com/library/windows/apps/Windows.System.RemoteSystems)  
[Connected apps and devices (Project Rome) overview](connected-apps-and-devices.md)  
[Discover remote devices](discover-remote-devices.md)  
[Remote Systems sample](https://github.com/Microsoft/Windows-universal-samples/tree/dev/Samples/RemoteSystems) demonstrates how to discover a remote system, launch an app on a remote system, and use app services to send messages between apps running on two systems.
