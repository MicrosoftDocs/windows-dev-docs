---
author: drewbatgit
ms.assetid: 40B97E0C-EB1B-40C2-A022-1AB95DFB085E
description: This article shows you how to cast media to remote devices from a Universal Windows app.
title: Media casting
ms.author: drewbat
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Media casting



This article shows you how to cast media to remote devices from a Universal Windows app.

## Built-in media casting with MediaPlayerElement

The simplest way to cast media from a Universal Windows app is to use the built-in casting capability of the [**MediaPlayerElement**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.Controls.MediaPlayerElement) control.

To allow the user to open a video file to be played in the **MediaPlayerElement** control, add the following namespaces to your project.

[!code-cs[BuiltInCastingUsing](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetBuiltInCastingUsing)]

In your app's XAML file, add a **MediaPlayerElement** and set [**AreTransportControlsEnabled**](https://msdn.microsoft.com/library/windows/apps/dn298977) to true.

[!code-xml[MediaElement](./code/MediaCasting_RS1/cs/MainPage.xaml#SnippetMediaElement)]

Add a button to let the user initiate picking a file.

[!code-xml[OpenButton](./code/MediaCasting_RS1/cs/MainPage.xaml#SnippetOpenButton)]

In the [**Click**](https://msdn.microsoft.com/library/windows/apps/br227737) event handler for the button, create a new instance of the [**FileOpenPicker**](https://msdn.microsoft.com/library/windows/apps/br207847), add video file types to the [**FileTypeFilter**](https://msdn.microsoft.com/library/windows/apps/br207850) collection, and set the starting location to the user's videos library.

Call [**PickSingleFileAsync**](https://msdn.microsoft.com/library/windows/apps/jj635275) to launch the file picker dialog. When this method returns, the result is a [**StorageFile**](https://msdn.microsoft.com/library/windows/apps/br227171) object representing the video file. Check to make sure the file isn't null, which it will be if the user cancels the picking operation. Call the file's [**OpenAsync**](https://msdn.microsoft.com/library/windows/apps/br227221.aspx) method to get an [**IRandomAccessStream**](https://msdn.microsoft.com/library/windows/apps/br241731) for the file. Finally, create a new **MediaSource** object from the selected file by calling [**CreateFromStorageFile**](https://msdn.microsoft.com/library/windows/apps/dn930909) and assign it to the **MediaPlayerElement** object's [**Source**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.Controls.MediaPlayerElement.Source) property to make the video file the video source for the control.

[!code-cs[OpenButtonClick](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetOpenButtonClick)]

Once the video is loaded in the **MediaPlayerElement**, the user can simply press the casting button on the transport controls to launch a built-in dialog that allows them to choose a device to which the loaded media will be cast.

![mediaelement casting button](images/media-element-casting-button.png)

> [!NOTE] 
> Starting with Windows 10, version 1607, it is recommended that you use the **MediaPlayer** class to play media items. The **MediaPlayerElement** is a lightweight XAML control that is used to render the content of a **MediaPlayer** in a XAML page. The **MediaElement** control continues to be supported for backwards compatibility. For more information on using **MediaPlayer** and **MediaPlayerElement** to play media content, see [Play audio and video with MediaPlayer](play-audio-and-video-with-mediaplayer.md). For information on using **MediaSource** and related APIs to work with media content, see [Media items, playlists, and tracks](media-playback-with-mediasource.md).

## Media casting with the CastingDevicePicker

A second way to cast media to a device is to use the [**CastingDevicePicker**](https://msdn.microsoft.com/library/windows/apps/dn972525). To use this class, include the [**Windows.Media.Casting**](https://msdn.microsoft.com/library/windows/apps/dn972568) namespace in your project.

[!code-cs[CastingNamespace](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetCastingNamespace)]

Declare a member variable for the **CastingDevicePicker** object.

[!code-cs[DeclareCastingPicker](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetDeclareCastingPicker)]

When you page is initialized, create a new instance of the casting picker and set the [**Filter**](https://msdn.microsoft.com/library/windows/apps/dn972540) to [**SupportsVideo**](https://msdn.microsoft.com/library/windows/apps/dn972526) property to indicate that the casting devices listed by the picker should support video. Register a handler for the [**CastingDeviceSelected**](https://msdn.microsoft.com/library/windows/apps/dn972539) event, which is raised when the user picks a device for casting.

[!code-cs[InitCastingPicker](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetInitCastingPicker)]

In your XAML file, add a button to allow the user to launch the picker.

[!code-xml[CastPickerButton](./code/MediaCasting_RS1/cs/MainPage.xaml#SnippetCastPickerButton)]

In the **Click** event handler for the button, call [**TransformToVisual**](https://msdn.microsoft.com/library/windows/apps/br208986) to get the transform of a UI element relative to another element. In this example, the transform is the position of the cast picker button relative to the visual root of the application window. Call the [**Show**](https://msdn.microsoft.com/library/windows/apps/dn972542) method of the [**CastingDevicePicker**](https://msdn.microsoft.com/library/windows/apps/dn972525) object to launch the casting picker dialog. Specify the location and dimensions of the cast picker button so that the system can make the dialog fly out from the button that the user pressed.

[!code-cs[CastPickerButtonClick](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetCastPickerButtonClick)]

In the **CastingDeviceSelected** event handler, call the [**CreateCastingConnection**](https://msdn.microsoft.com/library/windows/apps/dn972547) method of the [**SelectedCastingDevice**](https://msdn.microsoft.com/library/windows/apps/dn972546) property of the event args, which represents the casting device selected by the user. Register handlers for the [**ErrorOccurred**](https://msdn.microsoft.com/library/windows/apps/dn972519) and [**StateChanged**](https://msdn.microsoft.com/library/windows/apps/dn972523) events. Finally, call [**RequestStartCastingAsync**](https://msdn.microsoft.com/library/windows/apps/dn972520) to begin casting, passing in the result to the **MediaPlayerElement** control's **MediaPlayer** object's [**GetAsCastingSource**](https://msdn.microsoft.com/library/windows/apps/dn920012) method to specify that the media to be cast is the content of the **MediaPlayer** associated with the **MediaPlayerElement**.

> [!NOTE] 
> The casting connection must be initiated on the UI thread. Since the **CastingDeviceSelected** is not called on the UI thread, you must place these calls inside a call to [**CoreDispatcher.RunAsync**](https://msdn.microsoft.com/library/windows/apps/hh750317) which causes them to be called on the UI thread.

[!code-cs[CastingDeviceSelected](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetCastingDeviceSelected)]

In the **ErrorOccurred** and **StateChanged** event handlers, you should update your UI to inform the user of the current casting status. These events are discussed in detail in the following section on creating a custom casting device picker.

[!code-cs[EmptyStateHandlers](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetEmptyStateHandlers)]

## Media casting with a custom device picker

The following section describes how to create your own casting device picker UI by enumerating the casting devices and initiating the connection from your code.

To enumerate the available casting devices, include the [**Windows.Devices.Enumeration**](https://msdn.microsoft.com/library/windows/apps/br225459) namespace in your project.

[!code-cs[EnumerationNamespace](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetEnumerationNamespace)]

Add the following controls to your XAML page to implement the rudimentary UI for this example:

-   A button to start the device watcher that looks for available casting devices.
-   A [**ProgressRing**](https://msdn.microsoft.com/library/windows/apps/br227538) control to provide feedback to the user that casting enumeration is ongoing.
-   A [**ListBox**](https://msdn.microsoft.com/library/windows/apps/br242868) to list the discovered casting devices. Define an [**ItemTemplate**](https://msdn.microsoft.com/library/windows/apps/br242830) for the control so that we can assign the casting device objects directly to the control and still display the [**FriendlyName**](https://msdn.microsoft.com/library/windows/apps/dn972549) property.
-   A button to allow the user to disconnect the casting device.

[!code-xml[CustomPickerXAML](./code/MediaCasting_RS1/cs/MainPage.xaml#SnippetCustomPickerXAML)]

In your code behind, declare member variables for the [**DeviceWatcher**](https://msdn.microsoft.com/library/windows/apps/br225446) and the [**CastingConnection**](https://msdn.microsoft.com/library/windows/apps/dn972510).

[!code-cs[DeclareDeviceWatcher](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetDeclareDeviceWatcher)]

In the **Click** handler for the *startWatcherButton*, first update the UI by disabling the button and making the progress ring active while device enumeration is ongoing. Clear the list box of casting devices.

Next, create a device watcher by calling [**DeviceInformation.CreateWatcher**](https://msdn.microsoft.com/library/windows/apps/br225427). This method can be used to watch for many different types of devices. Specify that you want to watch for devices that support video casting by using the device selector string returned by [**CastingDevice.GetDeviceSelector**](https://msdn.microsoft.com/library/windows/apps/dn972551).

Finally, register event handlers for the [**Added**](https://msdn.microsoft.com/library/windows/apps/br225450), [**Removed**](https://msdn.microsoft.com/library/windows/apps/br225453), [**EnumerationCompleted**](https://msdn.microsoft.com/library/windows/apps/br225451), and [**Stopped**](https://msdn.microsoft.com/library/windows/apps/br225457) events.

[!code-cs[StartWatcherButtonClick](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetStartWatcherButtonClick)]

The **Added** event is raised when a new device is discovered by the watcher. In the handler for this event, create a new [**CastingDevice**](https://msdn.microsoft.com/library/windows/apps/dn972524) object by calling [**CastingDevice.FromIdAsync**](https://msdn.microsoft.com/library/windows/apps/dn972550) and passing in the ID of the discovered casting device, which is contained in the **DeviceInformation** object passed into the handler.

Add the **CastingDevice** to the casting device **ListBox** so that the user can select it. Because of the [**ItemTemplate**](https://msdn.microsoft.com/library/windows/apps/br242830) defined in the XAML, the [**FriendlyName**](https://msdn.microsoft.com/library/windows/apps/dn972549) property will be used as the item text for in the list box. Because this event handler is not called on the UI thread, you must update the UI from within a call to [**CoreDispatcher.RunAsync**](https://msdn.microsoft.com/library/windows/apps/hh750317).

[!code-cs[WatcherAdded](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetWatcherAdded)]

The **Removed** event is raised when the watcher detects that a casting device is no longer present. Compare the ID property of the **Added** object passed into the handler to the ID of each **Added** in the list box's [**Items**](https://msdn.microsoft.com/library/windows/apps/br242823) collection. If the ID matches, remove that object from the collection. Again, because the UI is being updated, this call must be made from within a **RunAsync** call.

[!code-cs[WatcherRemoved](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetWatcherRemoved)]

The **EnumerationCompleted** event is raised when the watcher has finished detecting devices. In the handler for this event, update the UI to let the user know that device enumeration has completed and stop the device watcher by calling [**Stop**](https://msdn.microsoft.com/library/windows/apps/br225456).

[!code-cs[WatcherEnumerationCompleted](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetWatcherEnumerationCompleted)]

The Stopped event is raised when the device watcher has finished stopping. In the handler for this event, stop the [**ProgressRing**](https://msdn.microsoft.com/library/windows/apps/br227538) control and reenable the *startWatcherButton* so that the user can restart the device enumeration process.

[!code-cs[WatcherStopped](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetWatcherStopped)]

When the user selects one of the casting devices from the list box, the [**SelectionChanged**](https://msdn.microsoft.com/library/windows/apps/br209776) event is raised. It is within this handler that the casting connection will be created and casting will be started.

First, make sure the device watcher is stopped so that device enumeration doesn't interfere with media casting. Create a casting connection by calling [**CreateCastingConnection**](https://msdn.microsoft.com/library/windows/apps/dn972547) on the **CastingDevice** object selected by the user. Add event handlers for the [**StateChanged**](https://msdn.microsoft.com/library/windows/apps/dn972523) and [**ErrorOccurred**](https://msdn.microsoft.com/library/windows/apps/dn972519) events.

Start media casting by calling [**RequestStartCastingAsync**](https://msdn.microsoft.com/library/windows/apps/dn972520), passing in the casting source returned by calling the **MediaPlayer** method [**GetAsCastingSource**](https://msdn.microsoft.com/library/windows/apps/dn920012). Finally, make the disconnect button visible to allow the user to stop media casting.

[!code-cs[SelectionChanged](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetSelectionChanged)]

In the state changed handler, the action you take depends on the new state of the casting connection:

-   If the state is **Connected** or **Rendering**, make sure the **ProgressRing** control is inactive and the disconnect button is visible.
-   If the state is **Disconnected**, unselect the current casting device in the list box, make the **ProgressRing** control inactive, and hide the disconnect button.
-   If the state is **Connecting**, make the **ProgressRing** control active and hide the disconnect button.
-   If the state is **Disconnecting**, make the **ProgressRing** control active and hide the disconnect button.

[!code-cs[StateChanged](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetStateChanged)]

In the handler for the **ErrorOccurred** event, update your UI to let the user know that a casting error occurred and unselect the current **CastingDevice** object in the list box.

[!code-cs[ErrorOccurred](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetErrorOccurred)]

Finally, implement the handler for the disconnect button. Stop media casting and disconnect from the casting device by calling the **CastingConnection** object's [**DisconnectAsync**](https://msdn.microsoft.com/library/windows/apps/dn972518) method. This call must be dispatched to the UI thread by calling [**CoreDispatcher.RunAsync**](https://msdn.microsoft.com/library/windows/apps/hh750317).

[!code-cs[DisconnectButton](./code/MediaCasting_RS1/cs/MainPage.xaml.cs#SnippetDisconnectButton)]

 

 




