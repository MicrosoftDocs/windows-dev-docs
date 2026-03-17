---
description: This article shows you how to cast media to remote devices from a WinUI app.
title: Media casting
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, winui
ms.localizationpriority: medium
---
# Media casting



This article shows you how to cast media to remote devices from a WinUI app.

## Built-in media casting with MediaPlayerElement

The simplest way to cast media from a WinUI app is to use the built-in casting capability of the [**MediaPlayerElement**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement) control.

In your app's XAML file, add a **MediaPlayerElement** and set [**AreTransportControlsEnabled**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement.aretransportcontrolsenabled) to true.

:::code language="xml" source="~/../snippets-windows/winappsdk/audio-video-camera/media-casting-winui/cs/MediaCasting_WinUI/MainWindow.xaml" id="SnippetMediaElement":::

Add a button to let the user initiate picking a file.

:::code language="xml" source="~/../snippets-windows/winappsdk/audio-video-camera/media-casting-winui/cs/MediaCasting_WinUI/MainWindow.xaml" id="SnippetOpenButton":::

In the [**Click**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.buttonbase.click) event handler for the button, create a new instance of the [**FileOpenPicker**](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.fileopenpicker), add video file types to the [**FileTypeFilter**](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.fileopenpicker.filetypefilter) collection, and set the starting location to the user's videos library.

Call [**PickSingleFileAsync**](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.fileopenpicker.picksinglefileasync) to launch the file picker dialog. When this method returns, the result is a [**StorageFile**](/uwp/api/Windows.Storage.StorageFile) object representing the video file. Check to make sure the file isn't null, which it will be if the user cancels the picking operation. Call the file's [**OpenAsync**](/uwp/api/windows.storage.storagefile.openasync) method to get an [**IRandomAccessStream**](/uwp/api/Windows.Storage.Streams.IRandomAccessStream) for the file. Finally, create a new **MediaSource** object from the selected file by calling [**CreateFromStorageFile**](/uwp/api/windows.media.core.mediasource.createfromstoragefile) and assign it to the **MediaPlayerElement** object's [**Source**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement.source) property to make the video file the video source for the control.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/media-casting-winui/cs/MediaCasting_WinUI/MainWindow.xaml.cs" id="SnippetOpenButtonClick":::

Once the video is loaded in the **MediaPlayerElement**, the user can simply press the casting button on the transport controls to launch a built-in dialog that allows them to choose a device to which the loaded media will be cast.

![mediaelement casting button](images/media-element-casting-button.png)


## Media casting with the CastingDevicePicker

A second way to cast media to a device is to use the [**CastingDevicePicker**](/uwp/api/Windows.Media.Casting.CastingDevicePicker). First, declare a member variable for the **Windows.Media.Casting.CastingDevicePicker** object.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/media-casting-winui/cs/MediaCasting_WinUI/MainWindow.xaml.cs" id="SnippetDeclareCastingPicker":::

When your window is initialized, create a new instance of the casting picker and set the [**Filter**](/uwp/api/windows.media.casting.castingdevicepicker.filter) to [**SupportsVideo**](/uwp/api/Windows.Media.Casting.CastingDevicePickerFilter) property to indicate that the casting devices listed by the picker should support video. Register a handler for the [**CastingDeviceSelected**](/uwp/api/windows.media.casting.castingdevicepicker.castingdeviceselected) event, which is raised when the user picks a device for casting.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/media-casting-winui/cs/MediaCasting_WinUI/MainWindow.xaml.cs" id="SnippetInitCastingPicker":::

In your XAML file, add a button to allow the user to launch the picker.

:::code language="xml" source="~/../snippets-windows/winappsdk/audio-video-camera/media-casting-winui/cs/MediaCasting_WinUI/MainWindow.xaml" id="SnippetCastPickerButton":::

In the **Click** event handler for the button, call [**TransformToVisual**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.transformtovisual) to get the transform of a UI element relative to another element. In this example, the transform is the position of the cast picker button relative to the visual root of the application window. Call the [**Show**](/uwp/api/windows.media.casting.castingdevicepicker.show) method of the [**CastingDevicePicker**](/uwp/api/Windows.Media.Casting.CastingDevicePicker) object to launch the casting picker dialog. Specify the location and dimensions of the cast picker button so that the system can make the dialog fly out from the button that the user pressed.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/media-casting-winui/cs/MediaCasting_WinUI/MainWindow.xaml.cs" id="SnippetCastPickerButtonClick":::

In the **CastingDeviceSelected** event handler, call the [**CreateCastingConnection**](/uwp/api/windows.media.casting.castingdevice.createcastingconnection) method of the [**SelectedCastingDevice**](/uwp/api/windows.media.casting.castingdeviceselectedeventargs.selectedcastingdevice) property of the event args, which represents the casting device selected by the user. Register handlers for the [**ErrorOccurred**](/uwp/api/windows.media.casting.castingconnection.erroroccurred) and [**StateChanged**](/uwp/api/windows.media.casting.castingconnection.statechanged) events. Finally, call [**RequestStartCastingAsync**](/uwp/api/windows.media.casting.castingconnection.requeststartcastingasync) to begin casting, passing in the result to the **MediaPlayerElement** control's **MediaPlayer** object's [**GetAsCastingSource**](/uwp/api/windows.media.playback.mediaplayer.getascastingsource) method to specify that the media to be cast is the content of the **MediaPlayer** associated with the **MediaPlayerElement**.

> [!NOTE] 
> The casting connection must be initiated on the UI thread. Since the **CastingDeviceSelected** is not called on the UI thread, you must place these calls inside a call to [**DispatcherQueue.TryEnqueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.tryenqueue) which causes them to be called on the UI thread.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/media-casting-winui/cs/MediaCasting_WinUI/MainWindow.xaml.cs" id="SnippetCastingDeviceSelected":::

In the **ErrorOccurred** and **StateChanged** event handlers, you should update your UI to inform the user of the current casting status. These events are discussed in detail in the following section on creating a custom casting device picker.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/media-casting-winui/cs/MediaCasting_WinUI/MainWindow.xaml.cs" id="SnippetEmptyStateHandlers":::

## Media casting with a custom device picker

The following section describes how to create your own casting device picker UI by enumerating the casting devices and initiating the connection from your code.

Add the following controls to your XAML page to implement the rudimentary UI for this example:

-   A button to start the device watcher that looks for available casting devices.
-   A [**ProgressRing**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.progressring) control to provide feedback to the user that casting enumeration is ongoing.
-   A [**ListBox**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.listbox) to list the discovered casting devices. Define an [**ItemTemplate**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemscontrol.itemtemplate) for the control so that we can assign the casting device objects directly to the control and still display the [**FriendlyName**](/uwp/api/windows.media.casting.castingdevice.friendlyname) property.
-   A button to allow the user to disconnect the casting device.

:::code language="xml" source="~/../snippets-windows/winappsdk/audio-video-camera/media-casting-winui/cs/MediaCasting_WinUI/MainWindow.xaml" id="SnippetCustomPickerXAML":::

In your code behind, declare member variables for the [**DeviceWatcher**](/uwp/api/Windows.Devices.Enumeration.DeviceWatcher) and the [**CastingConnection**](/uwp/api/Windows.Media.Casting.CastingConnection).

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/media-casting-winui/cs/MediaCasting_WinUI/MainWindow.xaml.cs" id="SnippetDeclareDeviceWatcher":::

In the **Click** handler for the *startWatcherButton*, first update the UI by disabling the button and making the progress ring active while device enumeration is ongoing. Clear the list box of casting devices.

Next, create a device watcher by calling [**DeviceInformation.CreateWatcher**](/uwp/api/windows.devices.enumeration.deviceinformation.createwatcher). This method can be used to watch for many different types of devices. Specify that you want to watch for devices that support video casting by using the device selector string returned by [**CastingDevice.GetDeviceSelector**](/uwp/api/windows.media.casting.castingdevice.getdeviceselector).

Finally, register event handlers for the [**Added**](/uwp/api/windows.devices.enumeration.devicewatcher.added), [**Removed**](/uwp/api/windows.devices.enumeration.devicewatcher.removed), [**EnumerationCompleted**](/uwp/api/windows.devices.enumeration.devicewatcher.enumerationcompleted), and [**Stopped**](/uwp/api/windows.devices.enumeration.devicewatcher.stopped) events.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/media-casting-winui/cs/MediaCasting_WinUI/MainWindow.xaml.cs" id="SnippetStartWatcherButtonClick":::

The **Added** event is raised when a new device is discovered by the watcher. In the handler for this event, create a new [**CastingDevice**](/uwp/api/Windows.Media.Casting.CastingDevice) object by calling [**CastingDevice.FromIdAsync**](/uwp/api/windows.media.casting.castingdevice.fromidasync) and passing in the ID of the discovered casting device, which is contained in the **DeviceInformation** object passed into the handler.

Add the **CastingDevice** to the casting device **ListBox** so that the user can select it. Because of the [**ItemTemplate**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemscontrol.itemtemplate) defined in the XAML, the [**FriendlyName**](/uwp/api/windows.media.casting.castingdevice.friendlyname) property will be used as the item text for in the list box. Because this event handler is not called on the UI thread, you must update the UI from within a call to [**DispatcherQueue.TryEnqueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.tryenqueue).

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/media-casting-winui/cs/MediaCasting_WinUI/MainWindow.xaml.cs" id="SnippetWatcherAdded":::

The **Removed** event is raised when the watcher detects that a casting device is no longer present. Compare the ID property of the **Added** object passed into the handler to the ID of each **Added** in the list box's [**Items**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemscontrol.items) collection. If the ID matches, remove that object from the collection. Again, because the UI is being updated, this call must be made from within a **RunAsync** call.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/media-casting-winui/cs/MediaCasting_WinUI/MainWindow.xaml.cs" id="SnippetWatcherRemoved":::

The **EnumerationCompleted** event is raised when the watcher has finished detecting devices. In the handler for this event, update the UI to let the user know that device enumeration has completed and stop the device watcher by calling [**Stop**](/uwp/api/windows.devices.enumeration.devicewatcher.stop).

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/media-casting-winui/cs/MediaCasting_WinUI/MainWindow.xaml.cs" id="SnippetWatcherEnumerationCompleted":::

The Stopped event is raised when the device watcher has finished stopping. In the handler for this event, stop the [**ProgressRing**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.progressring) control and reenable the *startWatcherButton* so that the user can restart the device enumeration process.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/media-casting-winui/cs/MediaCasting_WinUI/MainWindow.xaml.cs" id="SnippetWatcherStopped":::

When the user selects one of the casting devices from the list box, the [**SelectionChanged**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.selector.selectionchanged) event is raised. It is within this handler that the casting connection will be created and casting will be started.

First, make sure the device watcher is stopped so that device enumeration doesn't interfere with media casting. Create a casting connection by calling [**CreateCastingConnection**](/uwp/api/windows.media.casting.castingdevice.createcastingconnection) on the **CastingDevice** object selected by the user. Add event handlers for the [**StateChanged**](/uwp/api/windows.media.casting.castingconnection.statechanged) and [**ErrorOccurred**](/uwp/api/windows.media.casting.castingconnection.erroroccurred) events.

Start media casting by calling [**RequestStartCastingAsync**](/uwp/api/windows.media.casting.castingconnection.requeststartcastingasync), passing in the casting source returned by calling the **MediaPlayer** method [**GetAsCastingSource**](/uwp/api/windows.media.playback.mediaplayer.getascastingsource). Finally, make the disconnect button visible to allow the user to stop media casting.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/media-casting-winui/cs/MediaCasting_WinUI/MainWindow.xaml.cs" id="SnippetSelectionChanged":::

In the state changed handler, the action you take depends on the new state of the casting connection:

-   If the state is **Connected** or **Rendering**, make sure the **ProgressRing** control is inactive and the disconnect button is visible.
-   If the state is **Disconnected**, unselect the current casting device in the list box, make the **ProgressRing** control inactive, and hide the disconnect button.
-   If the state is **Connecting**, make the **ProgressRing** control active and hide the disconnect button.
-   If the state is **Disconnecting**, make the **ProgressRing** control active and hide the disconnect button.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/media-casting-winui/cs/MediaCasting_WinUI/MainWindow.xaml.cs" id="SnippetStateChanged":::

In the handler for the **ErrorOccurred** event, update your UI to let the user know that a casting error occurred and unselect the current **CastingDevice** object in the list box.

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/media-casting-winui/cs/MediaCasting_WinUI/MainWindow.xaml.cs" id="SnippetErrorOccurred":::

Finally, implement the handler for the disconnect button. Stop media casting and disconnect from the casting device by calling the **CastingConnection** object's [**DisconnectAsync**](/uwp/api/windows.media.casting.castingconnection.disconnectasync) method. This call must be dispatched to the UI thread by calling [**DispatcherQueue.TryEnqueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.tryenqueue).

:::code language="csharp" source="~/../snippets-windows/winappsdk/audio-video-camera/media-casting-winui/cs/MediaCasting_WinUI/MainWindow.xaml.cs" id="SnippetDisconnectButton":::

 

 
