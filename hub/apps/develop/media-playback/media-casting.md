---
description: This article shows you how to cast media to remote devices from a WinUI app.
title: Media casting
ms.date: 08/23/2026
ms.topic: article
keywords: windows 10, winui
ms.localizationpriority: medium
---
# Media casting



This article shows you how to cast media to remote devices from a WinUI app.

## Built-in media casting with MediaPlayerElement

The simplest way to cast media from a WinUI app is to use the built-in casting capability of the [**MediaPlayerElement**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement) control.

In your app's XAML file, add a **MediaPlayerElement** and set [**AreTransportControlsEnabled**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement.aretransportcontrolsenabled) to true.

```xml
<MediaPlayerElement Name="mediaPlayerElement"  MinHeight="100" MaxWidth="600" HorizontalAlignment="Stretch" AreTransportControlsEnabled="True"/>
```

Add a button to let the user initiate picking a file.

```xml
<Button x:Name="bOpenButton" Click="bOpenButton_Click" Content="Open"/>
```

In the [**Click**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.buttonbase.click) event handler for the button, create a new instance of the [**FileOpenPicker**](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.fileopenpicker), add video file types to the [**FileTypeFilter**](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.fileopenpicker.filetypefilter) collection, and set the starting location to the user's videos library.

Call [**PickSingleFileAsync**](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.fileopenpicker.picksinglefileasync) to launch the file picker dialog. When this method returns, the result is a [**StorageFile**](/uwp/api/Windows.Storage.StorageFile) object representing the video file. Check to make sure the file isn't null, which it will be if the user cancels the picking operation. Call the file's [**OpenAsync**](/uwp/api/windows.storage.storagefile.openasync) method to get an [**IRandomAccessStream**](/uwp/api/Windows.Storage.Streams.IRandomAccessStream) for the file. Finally, create a new **MediaSource** object from the selected file by calling [**CreateFromStorageFile**](/uwp/api/windows.media.core.mediasource.createfromstoragefile) and assign it to the **MediaPlayerElement** object's [**Source**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement.source) property to make the video file the video source for the control.

```csharp
private async void bOpenButton_Click(object sender, RoutedEventArgs e)
{
    //Create a new picker
    var filePicker = new Microsoft.Windows.Storage.Pickers.FileOpenPicker(this.AppWindow.Id)
    {
        SuggestedStartLocation = PickerLocationId.VideosLibrary,
        FileTypeFilter = { ".wmv", ".mp4", ".mkv" },
    };

    //Retrieve file from picker
    var result = await filePicker.PickSingleFileAsync();

    if (result is not null)
    {
        var storageFile = await Windows.Storage.StorageFile.GetFileFromPathAsync(result.Path);
        mediaPlayerElement.Source = MediaSource.CreateFromStorageFile(storageFile);
        mediaPlayerElement.MediaPlayer.Play();
    }

}
```

Once the video is loaded in the **MediaPlayerElement**, the user can simply press the casting button on the transport controls to launch a built-in dialog that allows them to choose a device to which the loaded media will be cast.

![mediaelement casting button](images/media-element-casting-button.png)


## Media casting with the CastingDevicePicker

A second way to cast media to a device is to use the [**CastingDevicePicker**](/uwp/api/Windows.Media.Casting.CastingDevicePicker). First, declare a member variable for the **Windows.Media.Casting.CastingDevicePicker** object.

```csharp
CastingDevicePicker castingPicker;
```

When your window is initialized, create a new instance of the casting picker and set the [**Filter**](/uwp/api/windows.media.casting.castingdevicepicker.filter) to [**SupportsVideo**](/uwp/api/Windows.Media.Casting.CastingDevicePickerFilter) property to indicate that the casting devices listed by the picker should support video. Register a handler for the [**CastingDeviceSelected**](/uwp/api/windows.media.casting.castingdevicepicker.castingdeviceselected) event, which is raised when the user picks a device for casting.

```csharp
//Initialize our picker object
castingPicker = new CastingDevicePicker();

//Set the picker to filter to video capable casting devices
castingPicker.Filter.SupportsVideo = true;

//Hook up device selected event
castingPicker.CastingDeviceSelected += CastingPicker_CastingDeviceSelected;
```

In your XAML file, add a button to allow the user to launch the picker.

```xml
<Button x:Name="bCastPickerButton" Content="Cast Button" Click="bCastPickerButton_Click"/>
```

In the **Click** event handler for the button, call [**TransformToVisual**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.transformtovisual) to get the transform of a UI element relative to another element. In this example, the transform is the position of the cast picker button relative to the visual root of the application window. Call the [**Show**](/uwp/api/windows.media.casting.castingdevicepicker.show) method of the [**CastingDevicePicker**](/uwp/api/Windows.Media.Casting.CastingDevicePicker) object to launch the casting picker dialog. Specify the location and dimensions of the cast picker button so that the system can make the dialog fly out from the button that the user pressed.

```csharp
private void bCastPickerButton_Click(object sender, RoutedEventArgs e)
{
    //Retrieve the location of the casting button
    GeneralTransform transform = bCastPickerButton.TransformToVisual(this.Content as UIElement);
    Point pt = transform.TransformPoint(new Point(0, 0));

    //Show the picker above our casting button
    castingPicker.Show(new Rect(pt.X, pt.Y, bCastPickerButton.ActualWidth, bCastPickerButton.ActualHeight),
        Windows.UI.Popups.Placement.Above);
}
```

In the **CastingDeviceSelected** event handler, call the [**CreateCastingConnection**](/uwp/api/windows.media.casting.castingdevice.createcastingconnection) method of the [**SelectedCastingDevice**](/uwp/api/windows.media.casting.castingdeviceselectedeventargs.selectedcastingdevice) property of the event args, which represents the casting device selected by the user. Register handlers for the [**ErrorOccurred**](/uwp/api/windows.media.casting.castingconnection.erroroccurred) and [**StateChanged**](/uwp/api/windows.media.casting.castingconnection.statechanged) events. Finally, call [**RequestStartCastingAsync**](/uwp/api/windows.media.casting.castingconnection.requeststartcastingasync) to begin casting, passing in the result to the **MediaPlayerElement** control's **MediaPlayer** object's [**GetAsCastingSource**](/uwp/api/windows.media.playback.mediaplayer.getascastingsource) method to specify that the media to be cast is the content of the **MediaPlayer** associated with the **MediaPlayerElement**.

> [!NOTE] 
> The casting connection must be initiated on the UI thread. Since the **CastingDeviceSelected** is not called on the UI thread, you must place these calls inside a call to [**DispatcherQueue.TryEnqueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.tryenqueue) which causes them to be called on the UI thread.

```csharp
private void CastingPicker_CastingDeviceSelected(CastingDevicePicker sender, CastingDeviceSelectedEventArgs args)
{
    //Casting must occur from the UI thread.  This dispatches the casting calls to the UI thread.
   DispatcherQueue.TryEnqueue( async () =>
    {
        //Create a casting conneciton from our selected casting device
        CastingConnection connection = args.SelectedCastingDevice.CreateCastingConnection();

        //Hook up the casting events
        connection.ErrorOccurred += Connection_ErrorOccurred;
        connection.StateChanged += Connection_StateChanged;

        //Cast the content loaded in the media element to the selected casting device
        await connection.RequestStartCastingAsync(mediaPlayerElement.MediaPlayer.GetAsCastingSource());
    });
}
```

In the **ErrorOccurred** and **StateChanged** event handlers, you should update your UI to inform the user of the current casting status. These events are discussed in detail in the following section on creating a custom casting device picker.

```csharp
private void Connection_StateChanged(CastingConnection sender, object args)
{
    DispatcherQueue.TryEnqueue( () =>
    {
        ShowMessageToUser("Casting Connection State Changed: " + sender.State);
    });
}

private void Connection_ErrorOccurred(CastingConnection sender, CastingConnectionErrorOccurredEventArgs args)
{
    DispatcherQueue.TryEnqueue(() =>
    {
        ShowMessageToUser("Casting Connection State Changed: " + sender.State);
    });
}
```

## Media casting with a custom device picker

The following section describes how to create your own casting device picker UI by enumerating the casting devices and initiating the connection from your code.

Add the following controls to your XAML page to implement the rudimentary UI for this example:

-   A button to start the device watcher that looks for available casting devices.
-   A [**ProgressRing**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.progressring) control to provide feedback to the user that casting enumeration is ongoing.
-   A [**ListBox**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.listbox) to list the discovered casting devices. Define an [**ItemTemplate**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemscontrol.itemtemplate) for the control so that we can assign the casting device objects directly to the control and still display the [**FriendlyName**](/uwp/api/windows.media.casting.castingdevice.friendlyname) property.
-   A button to allow the user to disconnect the casting device.

```xml
<Button x:Name="bStartWatcherButton" Content="Watcher Button" Click="bStartWatcherButton_Click"/>
<ProgressRing x:Name="prWatcherProgressRing" IsActive="False"/>
<ListBox x:Name="lbCastingDevicesListBox" MaxWidth="300" HorizontalAlignment="Left" SelectionChanged="lbCastingDevicesListBox_SelectionChanged">
    <!--Listbox content is bound to the FriendlyName field of our casting devices-->
    <ListBox.ItemTemplate>
        <DataTemplate>
            <TextBlock Text="{Binding Path=FriendlyName}"/>
        </DataTemplate>
    </ListBox.ItemTemplate>
</ListBox>
<Button x:Name="bDisconnectButton" Content="Disconnect" Click="bDisconnectButton_Click" Visibility="Collapsed"/>

```

In your code behind, declare member variables for the [**DeviceWatcher**](/uwp/api/Windows.Devices.Enumeration.DeviceWatcher) and the [**CastingConnection**](/uwp/api/Windows.Media.Casting.CastingConnection).

```csharp
DeviceWatcher deviceWatcher;
CastingConnection castingConnection;
```

In the **Click** handler for the *startWatcherButton*, first update the UI by disabling the button and making the progress ring active while device enumeration is ongoing. Clear the list box of casting devices.

Next, create a device watcher by calling [**DeviceInformation.CreateWatcher**](/uwp/api/windows.devices.enumeration.deviceinformation.createwatcher). This method can be used to watch for many different types of devices. Specify that you want to watch for devices that support video casting by using the device selector string returned by [**CastingDevice.GetDeviceSelector**](/uwp/api/windows.media.casting.castingdevice.getdeviceselector).

Finally, register event handlers for the [**Added**](/uwp/api/windows.devices.enumeration.devicewatcher.added), [**Removed**](/uwp/api/windows.devices.enumeration.devicewatcher.removed), [**EnumerationCompleted**](/uwp/api/windows.devices.enumeration.devicewatcher.enumerationcompleted), and [**Stopped**](/uwp/api/windows.devices.enumeration.devicewatcher.stopped) events.

```csharp
private void bStartWatcherButton_Click(object sender, RoutedEventArgs e)
{
    bStartWatcherButton.IsEnabled = false;
    prWatcherProgressRing.IsActive = true;

    lbCastingDevicesListBox.Items.Clear();

    //Create our watcher and have it find casting devices capable of video casting
    deviceWatcher = DeviceInformation.CreateWatcher(CastingDevice.GetDeviceSelector(CastingPlaybackTypes.Video));

    //Register for watcher events
    deviceWatcher.Added += DeviceWatcher_Added;
    deviceWatcher.Removed += DeviceWatcher_Removed;
    deviceWatcher.EnumerationCompleted += DeviceWatcher_EnumerationCompleted;
    deviceWatcher.Stopped += DeviceWatcher_Stopped;

    //Start the watcher
    deviceWatcher.Start();
}
```

The **Added** event is raised when a new device is discovered by the watcher. In the handler for this event, create a new [**CastingDevice**](/uwp/api/Windows.Media.Casting.CastingDevice) object by calling [**CastingDevice.FromIdAsync**](/uwp/api/windows.media.casting.castingdevice.fromidasync) and passing in the ID of the discovered casting device, which is contained in the **DeviceInformation** object passed into the handler.

Add the **CastingDevice** to the casting device **ListBox** so that the user can select it. Because of the [**ItemTemplate**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemscontrol.itemtemplate) defined in the XAML, the [**FriendlyName**](/uwp/api/windows.media.casting.castingdevice.friendlyname) property will be used as the item text for in the list box. Because this event handler is not called on the UI thread, you must update the UI from within a call to [**DispatcherQueue.TryEnqueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.tryenqueue).

```csharp
private void DeviceWatcher_Added(DeviceWatcher sender, DeviceInformation args)
{
    DispatcherQueue.TryEnqueue(async () =>
    {
        //Add each discovered device to our listbox
        CastingDevice addedDevice = await CastingDevice.FromIdAsync(args.Id);
        lbCastingDevicesListBox.Items.Add(addedDevice);
    });
}
```

The **Removed** event is raised when the watcher detects that a casting device is no longer present. Compare the ID property of the **Added** object passed into the handler to the ID of each **Added** in the list box's [**Items**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemscontrol.items) collection. If the ID matches, remove that object from the collection. Again, because the UI is being updated, this call must be made from within a **RunAsync** call.

```csharp
private void DeviceWatcher_Removed(DeviceWatcher sender, DeviceInformationUpdate args)
{
    DispatcherQueue.TryEnqueue( () =>
    {
        foreach (CastingDevice currentDevice in lbCastingDevicesListBox.Items)
        {
            if (currentDevice.Id == args.Id)
            {
                lbCastingDevicesListBox.Items.Remove(currentDevice);
            }
        }
    });
}
```

The **EnumerationCompleted** event is raised when the watcher has finished detecting devices. In the handler for this event, update the UI to let the user know that device enumeration has completed and stop the device watcher by calling [**Stop**](/uwp/api/windows.devices.enumeration.devicewatcher.stop).

```csharp
private void DeviceWatcher_EnumerationCompleted(DeviceWatcher sender, object args)
{
    DispatcherQueue.TryEnqueue(() =>
    {
        //If enumeration completes, update UI and transition watcher to the stopped state
        ShowMessageToUser("Watcher completed enumeration of devices");
        deviceWatcher.Stop();
    });
}
```

The Stopped event is raised when the device watcher has finished stopping. In the handler for this event, stop the [**ProgressRing**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.progressring) control and reenable the *startWatcherButton* so that the user can restart the device enumeration process.

```csharp

private void DeviceWatcher_Stopped(DeviceWatcher sender, object args)
{
   DispatcherQueue.TryEnqueue( () =>
    {
        //Update UX when the watcher stops
        bStartWatcherButton.IsEnabled = true;
        prWatcherProgressRing.IsActive = false;
    });
}
```

When the user selects one of the casting devices from the list box, the [**SelectionChanged**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.selector.selectionchanged) event is raised. It is within this handler that the casting connection will be created and casting will be started.

First, make sure the device watcher is stopped so that device enumeration doesn't interfere with media casting. Create a casting connection by calling [**CreateCastingConnection**](/uwp/api/windows.media.casting.castingdevice.createcastingconnection) on the **CastingDevice** object selected by the user. Add event handlers for the [**StateChanged**](/uwp/api/windows.media.casting.castingconnection.statechanged) and [**ErrorOccurred**](/uwp/api/windows.media.casting.castingconnection.erroroccurred) events.

Start media casting by calling [**RequestStartCastingAsync**](/uwp/api/windows.media.casting.castingconnection.requeststartcastingasync), passing in the casting source returned by calling the **MediaPlayer** method [**GetAsCastingSource**](/uwp/api/windows.media.playback.mediaplayer.getascastingsource). Finally, make the disconnect button visible to allow the user to stop media casting.

```csharp
private async void lbCastingDevicesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
{
    if (lbCastingDevicesListBox.SelectedItem != null)
    {
        //When a device is selected, first thing we do is stop the watcher so it's search doesn't conflict with streaming
        if (deviceWatcher.Status != DeviceWatcherStatus.Stopped)
        {
            deviceWatcher.Stop();
        }

        //Create a new casting connection to the device that's been selected
        castingConnection = ((CastingDevice)lbCastingDevicesListBox.SelectedItem).CreateCastingConnection();

        //Register for events
        castingConnection.ErrorOccurred += CastingConnection_ErrorOccurred;
        castingConnection.StateChanged += CastingConnection_StateChanged;

        //Cast the loaded video to the selected casting device.
        await castingConnection.RequestStartCastingAsync(mediaPlayerElement.MediaPlayer.GetAsCastingSource());
        bDisconnectButton.Visibility = Visibility.Visible;
    }
}
```

In the state changed handler, the action you take depends on the new state of the casting connection:

-   If the state is **Connected** or **Rendering**, make sure the **ProgressRing** control is inactive and the disconnect button is visible.
-   If the state is **Disconnected**, unselect the current casting device in the list box, make the **ProgressRing** control inactive, and hide the disconnect button.
-   If the state is **Connecting**, make the **ProgressRing** control active and hide the disconnect button.
-   If the state is **Disconnecting**, make the **ProgressRing** control active and hide the disconnect button.

```csharp
private void CastingConnection_StateChanged(CastingConnection sender, object args)
{
    DispatcherQueue.TryEnqueue( () =>
    {
        //Update the UX based on the casting state
        if (sender.State == CastingConnectionState.Connected || sender.State == CastingConnectionState.Rendering)
        {
            bDisconnectButton.Visibility = Visibility.Visible;
            prWatcherProgressRing.IsActive = false;
        }
        else if (sender.State == CastingConnectionState.Disconnected)
        {
            bDisconnectButton.Visibility = Visibility.Collapsed;
            lbCastingDevicesListBox.SelectedItem = null;
            prWatcherProgressRing.IsActive = false;
        }
        else if (sender.State == CastingConnectionState.Connecting)
        {
            bDisconnectButton.Visibility = Visibility.Collapsed;
            ShowMessageToUser("Connecting");
            prWatcherProgressRing.IsActive = true;
        }
        else
        {
            //Disconnecting is the remaining state
            bDisconnectButton.Visibility = Visibility.Collapsed;
            prWatcherProgressRing.IsActive = true;
        }
    });
}
```

In the handler for the **ErrorOccurred** event, update your UI to let the user know that a casting error occurred and unselect the current **CastingDevice** object in the list box.

```csharp
private void CastingConnection_ErrorOccurred(CastingConnection sender, CastingConnectionErrorOccurredEventArgs args)
{
    DispatcherQueue.TryEnqueue( () =>
    {
        //Clear the selection in the listbox on an error
        ShowMessageToUser("Casting Error: " + args.Message);
        lbCastingDevicesListBox.SelectedItem = null;
    });
}
```

Finally, implement the handler for the disconnect button. Stop media casting and disconnect from the casting device by calling the **CastingConnection** object's [**DisconnectAsync**](/uwp/api/windows.media.casting.castingconnection.disconnectasync) method. This call must be dispatched to the UI thread by calling [**DispatcherQueue.TryEnqueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.tryenqueue).

```csharp
private async void bDisconnectButton_Click(object sender, RoutedEventArgs e)
{
    if (castingConnection != null)
    {
        //When disconnect is clicked, the casting conneciton is disconnected.  The video should return locally to the media element.
        await castingConnection.DisconnectAsync();
    }
}
```

 

 
