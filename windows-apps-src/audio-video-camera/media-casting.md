---
ms.assetid: 40B97E0C-EB1B-40C2-A022-1AB95DFB085E
description: This article shows you how to cast media to remote devices from a Universal Windows app.
title: Media casting
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Media casting



This article shows you how to cast media to remote devices from a Universal Windows app.

## Built-in media casting with MediaPlayerElement

The simplest way to cast media from a Universal Windows app is to use the built-in casting capability of the [**MediaPlayerElement**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.MediaPlayerElement) control.

To allow the user to open a video file to be played in the **MediaPlayerElement** control, add the following namespaces to your project.

[!code-cs[BuiltInCastingUsing](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetBuiltInCastingUsing)]

In your app's XAML file, add a **MediaPlayerElement** and set [**AreTransportControlsEnabled**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.mediaelement.aretransportcontrolsenabled) to true.

[!code-xml[MediaElement](./code/MediaCasting_RS1/cs/MainPage.xaml#SnippetMediaElement)]

Add a button to let the user initiate picking a file.

[!code-xml[OpenButton](./code/MediaCasting_RS1/cs/MainPage.xaml#SnippetOpenButton)]

In the [**Click**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives.buttonbase.click) event handler for the button, create a new instance of the [**FileOpenPicker**](https://docs.microsoft.com/uwp/api/Windows.Storage.Pickers.FileOpenPicker), add video file types to the [**FileTypeFilter**](https://docs.microsoft.com/uwp/api/windows.storage.pickers.fileopenpicker.filetypefilter) collection, and set the starting location to the user's videos library.

Call [**PickSingleFileAsync**](https://docs.microsoft.com/uwp/api/windows.storage.pickers.fileopenpicker.picksinglefileasync) to launch the file picker dialog. When this method returns, the result is a [**StorageFile**](https://docs.microsoft.com/uwp/api/Windows.Storage.StorageFile) object representing the video file. Check to make sure the file isn't null, which it will be if the user cancels the picking operation. Call the file's [**OpenAsync**](https://docs.microsoft.com/uwp/api/windows.storage.storagefile.openasync) method to get an [**IRandomAccessStream**](https://docs.microsoft.com/uwp/api/Windows.Storage.Streams.IRandomAccessStream) for the file. Finally, create a new **MediaSource** object from the selected file by calling [**CreateFromStorageFile**](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource.createfromstoragefile) and assign it to the **MediaPlayerElement** object's [**Source**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.mediaplayerelement.source) property to make the video file the video source for the control.

[!code-cs[OpenButtonClick](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetOpenButtonClick)]

Once the video is loaded in the **MediaPlayerElement**, the user can simply press the casting button on the transport controls to launch a built-in dialog that allows them to choose a device to which the loaded media will be cast.

![mediaelement casting button](images/media-element-casting-button.png)

> [!NOTE] 
> Starting with Windows 10, version 1607, it is recommended that you use the **MediaPlayer** class to play media items. The **MediaPlayerElement** is a lightweight XAML control that is used to render the content of a **MediaPlayer** in a XAML page. The **MediaElement** control continues to be supported for backwards compatibility. For more information on using **MediaPlayer** and **MediaPlayerElement** to play media content, see [Play audio and video with MediaPlayer](play-audio-and-video-with-mediaplayer.md). For information on using **MediaSource** and related APIs to work with media content, see [Media items, playlists, and tracks](media-playback-with-mediasource.md).

## Media casting with the CastingDevicePicker

A second way to cast media to a device is to use the [**CastingDevicePicker**](https://docs.microsoft.com/uwp/api/Windows.Media.Casting.CastingDevicePicker). To use this class, include the [**Windows.Media.Casting**](https://docs.microsoft.com/uwp/api/Windows.Media.Casting) namespace in your project.

[!code-cs[CastingNamespace](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetCastingNamespace)]

Declare a member variable for the **CastingDevicePicker** object.

[!code-cs[DeclareCastingPicker](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetDeclareCastingPicker)]

When you page is initialized, create a new instance of the casting picker and set the [**Filter**](https://docs.microsoft.com/uwp/api/windows.media.casting.castingdevicepicker.filter) to [**SupportsVideo**](https://docs.microsoft.com/uwp/api/Windows.Media.Casting.CastingDevicePickerFilter) property to indicate that the casting devices listed by the picker should support video. Register a handler for the [**CastingDeviceSelected**](https://docs.microsoft.com/uwp/api/windows.media.casting.castingdevicepicker.castingdeviceselected) event, which is raised when the user picks a device for casting.

[!code-cs[InitCastingPicker](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetInitCastingPicker)]

In your XAML file, add a button to allow the user to launch the picker.

[!code-xml[CastPickerButton](./code/MediaCasting_RS1/cs/MainPage.xaml#SnippetCastPickerButton)]

In the **Click** event handler for the button, call [**TransformToVisual**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement.transformtovisual) to get the transform of a UI element relative to another element. In this example, the transform is the position of the cast picker button relative to the visual root of the application window. Call the [**Show**](https://docs.microsoft.com/uwp/api/windows.media.casting.castingdevicepicker.show) method of the [**CastingDevicePicker**](https://docs.microsoft.com/uwp/api/Windows.Media.Casting.CastingDevicePicker) object to launch the casting picker dialog. Specify the location and dimensions of the cast picker button so that the system can make the dialog fly out from the button that the user pressed.

[!code-cs[CastPickerButtonClick](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetCastPickerButtonClick)]

In the **CastingDeviceSelected** event handler, call the [**CreateCastingConnection**](https://docs.microsoft.com/uwp/api/windows.media.casting.castingdevice.createcastingconnection) method of the [**SelectedCastingDevice**](https://docs.microsoft.com/uwp/api/windows.media.casting.castingdeviceselectedeventargs.selectedcastingdevice) property of the event args, which represents the casting device selected by the user. Register handlers for the [**ErrorOccurred**](https://docs.microsoft.com/uwp/api/windows.media.casting.castingconnection.erroroccurred) and [**StateChanged**](https://docs.microsoft.com/uwp/api/windows.media.casting.castingconnection.statechanged) events. Finally, call [**RequestStartCastingAsync**](https://docs.microsoft.com/uwp/api/windows.media.casting.castingconnection.requeststartcastingasync) to begin casting, passing in the result to the **MediaPlayerElement** control's **MediaPlayer** object's [**GetAsCastingSource**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.mediaelement.getascastingsource) method to specify that the media to be cast is the content of the **MediaPlayer** associated with the **MediaPlayerElement**.

> [!NOTE] 
> The casting connection must be initiated on the UI thread. Since the **CastingDeviceSelected** is not called on the UI thread, you must place these calls inside a call to [**CoreDispatcher.RunAsync**](https://docs.microsoft.com/uwp/api/windows.ui.core.coredispatcher.runasync) which causes them to be called on the UI thread.

[!code-cs[CastingDeviceSelected](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetCastingDeviceSelected)]

In the **ErrorOccurred** and **StateChanged** event handlers, you should update your UI to inform the user of the current casting status. These events are discussed in detail in the following section on creating a custom casting device picker.

[!code-cs[EmptyStateHandlers](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetEmptyStateHandlers)]

## Media casting with a custom device picker

The following section describes how to create your own casting device picker UI by enumerating the casting devices and initiating the connection from your code.

To enumerate the available casting devices, include the [**Windows.Devices.Enumeration**](https://docs.microsoft.com/uwp/api/Windows.Devices.Enumeration) namespace in your project.

[!code-cs[EnumerationNamespace](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetEnumerationNamespace)]

Add the following controls to your XAML page to implement the rudimentary UI for this example:

-   A button to start the device watcher that looks for available casting devices.
-   A [**ProgressRing**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.ProgressRing) control to provide feedback to the user that casting enumeration is ongoing.
-   A [**ListBox**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.ListBox) to list the discovered casting devices. Define an [**ItemTemplate**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.itemscontrol.itemtemplate) for the control so that we can assign the casting device objects directly to the control and still display the [**FriendlyName**](https://docs.microsoft.com/uwp/api/windows.media.casting.castingdevice.friendlyname) property.
-   A button to allow the user to disconnect the casting device.

[!code-xml[CustomPickerXAML](./code/MediaCasting_RS1/cs/MainPage.xaml#SnippetCustomPickerXAML)]

In your code behind, declare member variables for the [**DeviceWatcher**](https://docs.microsoft.com/uwp/api/Windows.Devices.Enumeration.DeviceWatcher) and the [**CastingConnection**](https://docs.microsoft.com/uwp/api/Windows.Media.Casting.CastingConnection).

[!code-cs[DeclareDeviceWatcher](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetDeclareDeviceWatcher)]

In the **Click** handler for the *startWatcherButton*, first update the UI by disabling the button and making the progress ring active while device enumeration is ongoing. Clear the list box of casting devices.

Next, create a device watcher by calling [**DeviceInformation.CreateWatcher**](https://docs.microsoft.com/uwp/api/windows.devices.enumeration.deviceinformation.createwatcher). This method can be used to watch for many different types of devices. Specify that you want to watch for devices that support video casting by using the device selector string returned by [**CastingDevice.GetDeviceSelector**](https://docs.microsoft.com/uwp/api/windows.media.casting.castingdevice.getdeviceselector).

Finally, register event handlers for the [**Added**](https://docs.microsoft.com/uwp/api/windows.devices.enumeration.devicewatcher.added), [**Removed**](https://docs.microsoft.com/uwp/api/windows.devices.enumeration.devicewatcher.removed), [**EnumerationCompleted**](https://docs.microsoft.com/uwp/api/windows.devices.enumeration.devicewatcher.enumerationcompleted), and [**Stopped**](https://docs.microsoft.com/uwp/api/windows.devices.enumeration.devicewatcher.stopped) events.

[!code-cs[StartWatcherButtonClick](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetStartWatcherButtonClick)]

The **Added** event is raised when a new device is discovered by the watcher. In the handler for this event, create a new [**CastingDevice**](https://docs.microsoft.com/uwp/api/Windows.Media.Casting.CastingDevice) object by calling [**CastingDevice.FromIdAsync**](https://docs.microsoft.com/uwp/api/windows.media.casting.castingdevice.fromidasync) and passing in the ID of the discovered casting device, which is contained in the **DeviceInformation** object passed into the handler.

Add the **CastingDevice** to the casting device **ListBox** so that the user can select it. Because of the [**ItemTemplate**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.itemscontrol.itemtemplate) defined in the XAML, the [**FriendlyName**](https://docs.microsoft.com/uwp/api/windows.media.casting.castingdevice.friendlyname) property will be used as the item text for in the list box. Because this event handler is not called on the UI thread, you must update the UI from within a call to [**CoreDispatcher.RunAsync**](https://docs.microsoft.com/uwp/api/windows.ui.core.coredispatcher.runasync).

[!code-cs[WatcherAdded](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetWatcherAdded)]

The **Removed** event is raised when the watcher detects that a casting device is no longer present. Compare the ID property of the **Added** object passed into the handler to the ID of each **Added** in the list box's [**Items**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.itemscontrol.items) collection. If the ID matches, remove that object from the collection. Again, because the UI is being updated, this call must be made from within a **RunAsync** call.

[!code-cs[WatcherRemoved](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetWatcherRemoved)]

The **EnumerationCompleted** event is raised when the watcher has finished detecting devices. In the handler for this event, update the UI to let the user know that device enumeration has completed and stop the device watcher by calling [**Stop**](https://docs.microsoft.com/uwp/api/windows.devices.enumeration.devicewatcher.stop).

[!code-cs[WatcherEnumerationCompleted](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetWatcherEnumerationCompleted)]

The Stopped event is raised when the device watcher has finished stopping. In the handler for this event, stop the [**ProgressRing**](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.ProgressRing) control and reenable the *startWatcherButton* so that the user can restart the device enumeration process.

[!code-cs[WatcherStopped](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetWatcherStopped)]

When the user selects one of the casting devices from the list box, the [**SelectionChanged**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives.selector.selectionchanged) event is raised. It is within this handler that the casting connection will be created and casting will be started.

First, make sure the device watcher is stopped so that device enumeration doesn't interfere with media casting. Create a casting connection by calling [**CreateCastingConnection**](https://docs.microsoft.com/uwp/api/windows.media.casting.castingdevice.createcastingconnection) on the **CastingDevice** object selected by the user. Add event handlers for the [**StateChanged**](https://docs.microsoft.com/uwp/api/windows.media.casting.castingconnection.statechanged) and [**ErrorOccurred**](https://docs.microsoft.com/uwp/api/windows.media.casting.castingconnection.erroroccurred) events.

Start media casting by calling [**RequestStartCastingAsync**](https://docs.microsoft.com/uwp/api/windows.media.casting.castingconnection.requeststartcastingasync), passing in the casting source returned by calling the **MediaPlayer** method [**GetAsCastingSource**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.mediaelement.getascastingsource). Finally, make the disconnect button visible to allow the user to stop media casting.

[!code-cs[SelectionChanged](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetSelectionChanged)]

In the state changed handler, the action you take depends on the new state of the casting connection:

-   If the state is **Connected** or **Rendering**, make sure the **ProgressRing** control is inactive and the disconnect button is visible.
-   If the state is **Disconnected**, unselect the current casting device in the list box, make the **ProgressRing** control inactive, and hide the disconnect button.
-   If the state is **Connecting**, make the **ProgressRing** control active and hide the disconnect button.
-   If the state is **Disconnecting**, make the **ProgressRing** control active and hide the disconnect button.

[!code-cs[StateChanged](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetStateChanged)]

In the handler for the **ErrorOccurred** event, update your UI to let the user know that a casting error occurred and unselect the current **CastingDevice** object in the list box.

[!code-cs[ErrorOccurred](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetErrorOccurred)]

Finally, implement the handler for the disconnect button. Stop media casting and disconnect from the casting device by calling the **CastingConnection** object's [**DisconnectAsync**](https://docs.microsoft.com/uwp/api/windows.media.casting.castingconnection.disconnectasync) method. This call must be dispatched to the UI thread by calling [**CoreDispatcher.RunAsync**](https://docs.microsoft.com/uwp/api/windows.ui.core.coredispatcher.runasync).

[!code-cs[DisconnectButton](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetDisconnectButton)]

 

 




