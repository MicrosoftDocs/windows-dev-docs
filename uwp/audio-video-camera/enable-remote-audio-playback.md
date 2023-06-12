---
description: This article shows you how to use AudioPlaybackConnection to enable Bluetooth-connected remote devices to play back audio on the local machine.
title: Enable audio playback from remote Bluetooth-connected devices
ms.date: 05/03/2020
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Enable audio playback from remote Bluetooth-connected devices

This article shows you how to use [AudioPlaybackConnection](/uwp/api/windows.media.audio.audioplaybackconnection) to enable Bluetooth-connected remote devices to play back audio on the local machine.

Starting with Windows 10, version 2004 remote audio sources can stream audio to Windows devices, enabling scenarios such as configuring a PC to behave like a Bluetooth speaker and allowing users to hear audio from their phone. The implementation uses the Bluetooth components in the OS to process incoming audio data and play it on the system's audio endpoints on the system such as built-in PC speakers or wired headphones. The enabling of the underlying Bluetooth A2DP sink is managed by apps, which are responsible for the end-user scenario, rather than by the system.

The [AudioPlaybackConnection](/uwp/api/windows.media.audio.audioplaybackconnection) class is used to enable and disable connections from a remote device as well as to create the connection, allowing remote audio playback to begin.

## Add a user interface

For the examples in this article, we will use the following simple XAML UI which defines **ListView** control to display available remote devices, a **TextBlock** to display connection status, and three buttons for enabling, disabling, and opening connections.

:::code language="xml" source="~/../snippets-windows/windows-uwp/audio-video-camera/AudioPlaybackConnectionExample/cs/MainPage.xaml" id="snippet_AudioPlaybackConnectionXAML":::

## Use DeviceWatcher to monitor for remote devices

The [DeviceWatcher](/uwp/api/windows.devices.enumeration.devicewatcher) class allows you to detect connected devices. The [AudioPlaybackConnection.GetDeviceSelector](/uwp/api/windows.media.audio.audioplaybackconnection.getdeviceselector) method returns a string that tells the device watcher what kinds of devices to watch for. Pass this string into the **DeviceWatcher** constructor. 

The [DeviceWatcher.Added](/uwp/api/windows.devices.enumeration.devicewatcher.added) event is raised for each device that is connected when the device watcher is started as well as for any device that is connected while the device watcher is running. The [DeviceWatcher.Removed](/uwp/api/windows.devices.enumeration.devicewatcher.removed) event is raised if a previously connected device disconnects. 

Call [DeviceWatcher.Start](/uwp/api/windows.devices.enumeration.devicewatcher.start) to begin watching for connected devices that support audio playback connections. In this example we will start the device manager when the main **Grid** control in the UI is loaded. For more information on using **DeviceWatcher**, see [Enumerate Devices](../devices-sensors/enumerate-devices.md).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AudioPlaybackConnectionExample/cs/MainPage.xaml.cs" id="snippet_MainGridLoaded":::


In the device watcher's **Added** event, each discovered device is represented by a [DeviceInformation](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) object. Add each discovered device to an observable collection that is bound to the **ListView** control in the UI.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AudioPlaybackConnectionExample/cs/MainPage.xaml.cs" id="snippet_DeclareDevices":::


:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AudioPlaybackConnectionExample/cs/MainPage.xaml.cs" id="snippet_DeviceWatcher_Added":::


## Enable and release audio playback connections

Before opening a connection with a device, the connection must be enabled. This informs the system that there is a new application that wants audio from the remote device to be played on the PC, but audio does not begin playing until the connection is opened, which is shown in a later step.

In the click handler for the **Enable Audio Playback Connection** button, get the device ID associated with the currently selected device in the **ListView** control. This example maintains a dictionary of **AudioPlaybackConnection** objects that have been enabled. This method first checks to see if there is already an entry in the dictionary for the selected device. Next, the method attempts to create an **AudioPlaybackConnection** for the selected device by calling [TryCreateFromId](/uwp/api/windows.media.audio.audioplaybackconnection.trycreatefromid) and passing in the selected device ID. 

If the connection is successfully created, add the new **AudioPlaybackConnection** object to the app's dictionary, register a handler for the object's [StateChanged](/uwp/api/windows.media.audio.audioplaybackconnection.statechanged) event, and call[StartAsync](/uwp/api/windows.media.audio.audioplaybackconnection.startasync) to notify the system that the new connection is enabled. 

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AudioPlaybackConnectionExample/cs/MainPage.xaml.cs" id="snippet_DeclareConnections":::

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AudioPlaybackConnectionExample/cs/MainPage.xaml.cs" id="snippet_EnableAudioPlaybackConnection":::


## Open the audio playback connection

In the previous step, an audio playback connection was created, but sound does not begin playing until the connection is opened by calling [Open](/uwp/api/windows.media.audio.audioplaybackconnection.open) or [OpenAsync](/uwp/api/windows.media.audio.audioplaybackconnection.openasync). In the **Open Audio Playback Connection** button click handler, get the currently selected device and use the ID to retrieve the **AudioPlaybackConnection** from the app's dictionary of connections. Await a call to **OpenAsync** and check the **Status** value of the returned [AudioPlaybackConnectionOpenResultStatus](/uwp/api/windows.media.audio.audioplaybackconnectionopenresult) object to see if the connection was opened successfully and, if so, update the connection state text box.


:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AudioPlaybackConnectionExample/cs/MainPage.xaml.cs" id="snippet_OpenAudioPlaybackConnectionButton":::

## Monitor audio playback connection state

The [AudioPlaybackConnection.ConnectionStateChanged](/uwp/api/windows.media.audio.audioplaybackconnection.statechanged) event is raised whenever the state of the connection changes. In this example, the handler for this event updates the status text box. Remember to update the UI inside a call to [Dispatcher.RunAsync](/uwp/api/windows.ui.core.coredispatcher.runasync) to make sure the update is made on the UI thread.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AudioPlaybackConnectionExample/cs/MainPage.xaml.cs" id="snippet_ConnectionStateChanged":::

## Release connections and handle removed devices

This example provides a **Release Audio Playback Connection** button to allow the user to release an audio playback connection. In the handler for this event, we get the currently selected device and use the device's ID to look up the **AudioPlaybackConnection** in the dictionary. Call **Dispose** to release the reference and free any associated resources and remove the connection from the dictionary.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AudioPlaybackConnectionExample/cs/MainPage.xaml.cs" id="snippet_ReleaseAudioPlaybackConnectionButton":::

You should handle the case where a device is removed while a connection is enabled or open. To do this, implement a handler for the device watcher's [DeviceWatcher.Removed](/uwp/api/windows.devices.enumeration.devicewatcher.removed) event. First, the ID of the removed device is used to remove the device from the observable collection bound to the app's **ListView** control. Next, if a connection associated with this device is in the app's dictionary, **Dispose** is called to free the associated resources and then the connection is removed from the dictionary. All of this is done within a call to **Dispatcher.RunAsync** to make sure the UI updates are performed on the UI thread.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/AudioPlaybackConnectionExample/cs/MainPage.xaml.cs" id="snippet_DeviceWatcher_Removed":::

## Related topics

[Media Playback](media-playback.md)


 