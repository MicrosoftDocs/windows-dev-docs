---
ms.assetid: 
description: This article shows you how to connect to remote cameras and get a MediaFrameSourceGroup to retrieve frames from each camera.
title: Connect to remote cameras
ms.date: 04/19/2019
ms.topic: article
ms.custom: 19H1
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Connect to remote cameras

This article shows you how to connect to one or more remote cameras and get a [**MediaFrameSourceGroup**](/uwp/api/Windows.Media.Capture.Frames.MediaFrameSourceGroup) object that allows you to read frames from each camera. For more information on reading frames from a media source, see [Process media frames with MediaFrameReader](process-media-frames-with-mediaframereader.md). For more information on pairing with devices, see [Pair devices](../devices-sensors/pair-devices.md).

> [!NOTE] 
> The features discussed in this article are only available starting with Windows 10, version 1903.

## Create a DeviceWatcher class to watch for available remote cameras

The [**DeviceWatcher**](/uwp/api/windows.devices.enumeration.devicewatcher) class monitors the devices available to your app and notifies your app when devices are added or removed. Get an instance of **DeviceWatcher** by calling [**DeviceInformation.CreateWatcher**](/uwp/api/windows.devices.enumeration.deviceinformation.createwatcher#Windows_Devices_Enumeration_DeviceInformation_CreateWatcher_System_String_), passing in an Advanced Query Syntax (AQS) string that identifies the type of devices you want to monitor. The AQS string specifying network camera devices is the following:

```
@"System.Devices.InterfaceClassGuid:=""{B8238652-B500-41EB-B4F3-4234F7F5AE99}"" AND System.Devices.InterfaceEnabled:=System.StructuredQueryType.Boolean#True"
```

> [!NOTE] 
> The helper method [**MediaFrameSourceGroup.GetDeviceSelector**](/uwp/api/windows.media.capture.frames.mediaframesourcegroup.getdeviceselector) returns an AQS string that will monitor locally-connected and remote network cameras. To monitor only network cameras, you should use the AQS string shown above.


When you start the returned **DeviceWatcher** by calling the [**Start**](/uwp/api/windows.devices.enumeration.devicewatcher.start) method, it will raise the [**Added**](/uwp/api/windows.devices.enumeration.devicewatcher.added) event for every network camera that is currently available. Until you stop the watcher by calling [**Stop**](/uwp/api/windows.devices.enumeration.devicewatcher.stop), the **Added** event will be raised when new network camera devices become available and the [**Removed**](/uwp/api/windows.devices.enumeration.devicewatcher.removed) event will be raised when a camera device becomes unavailable.

The event args passed into the **Added** and **Removed** event handlers are a [**DeviceInformation**](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) or a [**DeviceInformationUpdate**](/uwp/api/windows.devices.enumeration.deviceinformationupdate) object, respectively. Each of these objects has an **Id** property that is the identifier for the network camera for which the event was fired. Pass this ID into the [**MediaFrameSourceGroup.FromIdAsync**](/uwp/api/windows.media.capture.frames.mediaframesourcegroup.fromidasync) method to get a [**MediaFrameSourceGroup**](/uwp/api/windows.media.capture.frames.mediaframesourcegroup.fromidasync) object that you can use to retrieve frames from the camera.

## Remote camera pairing helper class

The following example shows a helper class that uses a **DeviceWatcher** to create and update an **ObservableCollection** of **MediaFrameSourceGroup** objects to support data binding to the list of cameras. Typical apps would wrap the **MediaFrameSourceGroup** in a custom model class. Note that the helper class maintains a reference to the app's [**CoreDispatcher**](/uwp/api/Windows.UI.Core.CoreDispatcher) and updates the collection of cameras within calls to [**RunAsync**](/uwp/api/windows.ui.core.coredispatcher.runasync) to ensure that the UI bound to the collection is updated on the UI thread.

Also, this example handles the [**DeviceWatcher.Updated**](/uwp/api/windows.devices.enumeration.devicewatcher.updated) event in addition to the **Added** and **Removed** events. In the **Updated** handler, the associated remote camera device is removed from and then added back to the collection.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/audio-video-camera/Frames_Win10/cs/Frames_Win10/RemoteCameraPairingHelper.cs" id="SnippetRemoteCameraPairingHelper":::


## Related topics

* [Camera](camera.md)
* [Basic photo, video, and audio capture with MediaCapture](basic-photo-video-and-audio-capture-with-MediaCapture.md)
* [Camera frames sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CameraFrames)
* [Process media frames with MediaFrameReader](process-media-frames-with-mediaframereader.md)
 

 
