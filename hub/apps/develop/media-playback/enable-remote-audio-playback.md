---
description: This article shows you how to use AudioPlaybackConnection to enable Bluetooth-connected remote devices to play back audio on the local machine.
title: Enable audio playback from remote Bluetooth-connected devices
ms.date: 08/23/2026
ms.topic: article
keywords: windows, winui
ms.localizationpriority: medium
---

# Enable audio playback from remote Bluetooth-connected devices

This article shows you how to use [AudioPlaybackConnection](/uwp/api/windows.media.audio.audioplaybackconnection) to enable Bluetooth-connected remote devices to play back audio on the local machine.

Audio sources can stream audio to Windows devices, enabling scenarios such as configuring a PC to behave like a Bluetooth speaker and allowing users to hear audio from their phone. The implementation uses the Bluetooth components in the OS to process incoming audio data and play it on the system's audio endpoints on the system such as built-in PC speakers or wired headphones. The enabling of the underlying Bluetooth A2DP sink is managed by apps, which are responsible for the end-user scenario, rather than by the system.

The [AudioPlaybackConnection](/uwp/api/windows.media.audio.audioplaybackconnection) class is used to enable and disable connections from a remote device as well as to create the connection, allowing remote audio playback to begin.

## Add a user interface

For the examples in this article, we will use the following simple XAML UI which defines **ListView** control to display available remote devices, a **TextBlock** to display connection status, and three buttons for enabling, disabling, and opening connections.

```xml
<Grid x:Name="MainGrid" Loaded="MainGrid_Loaded">
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto" />
        <RowDefinition Height="Auto" />
        <RowDefinition Height="Auto" />
    </Grid.RowDefinitions>
    <StackPanel Orientation="Horizontal">
        <TextBlock Text="Connection state: "/>
        <TextBlock x:Name="ConnectionState" Grid.Row="0" Text="Disconnected."/>
    </StackPanel>
    <ListView x:Name="DeviceListView" ItemsSource="{x:Bind devices}" Grid.Row="1">
        <ListView.ItemTemplate>
            <DataTemplate x:DataType="enumeration:DeviceInformation">
                <StackPanel Orientation="Horizontal" Margin="6">
                    <SymbolIcon Symbol="Audio" Margin="0,0,12,0"/>
                    <StackPanel>
                        <TextBlock Text="{x:Bind Name}" FontWeight="Bold"/>
                    </StackPanel>
                </StackPanel>
            </DataTemplate>
        </ListView.ItemTemplate>
    </ListView>
    <StackPanel Orientation="Vertical" Grid.Row="2">
        <Button x:Name="EnableAudioPlaybackConnectionButton" Content="Enable Audio Playback Connection" Click="EnableAudioPlaybackConnectionButton_Click"/>
        <Button x:Name="ReleaseAudioPlaybackConnectionButton" Content="Release Audio Playback Connection" Click="ReleaseAudioPlaybackConnectionButton_Click"/>
        <Button x:Name="OpenAudioPlaybackConnectionButtonButton" Content="Open Connection" Click="OpenAudioPlaybackConnectionButtonButton_Click" IsEnabled="False"/>
    </StackPanel>
</Grid>
```

## Use DeviceWatcher to monitor for remote devices

The [DeviceWatcher](/uwp/api/windows.devices.enumeration.devicewatcher) class allows you to detect connected devices. The [AudioPlaybackConnection.GetDeviceSelector](/uwp/api/windows.media.audio.audioplaybackconnection.getdeviceselector) method returns a string that tells the device watcher what kinds of devices to watch for. Pass this string into the **DeviceWatcher** constructor. 

The [DeviceWatcher.Added](/uwp/api/windows.devices.enumeration.devicewatcher.added) event is raised for each device that is connected when the device watcher is started as well as for any device that is connected while the device watcher is running. The [DeviceWatcher.Removed](/uwp/api/windows.devices.enumeration.devicewatcher.removed) event is raised if a previously connected device disconnects. 

Call [DeviceWatcher.Start](/uwp/api/windows.devices.enumeration.devicewatcher.start) to begin watching for connected devices that support audio playback connections. In this example we will start the device manager when the main **Grid** control in the UI is loaded. For more information on using **DeviceWatcher**, see [Enumerate Devices](/windows/apps/develop/devices-sensors/enumerate-devices).

```csharp
private void MainGrid_Loaded(object sender, RoutedEventArgs e)
{
    audioPlaybackConnections = new Dictionary<string, AudioPlaybackConnection>();

    // Start watching for paired Bluetooth devices.
    deviceWatcher = DeviceInformation.CreateWatcher(AudioPlaybackConnection.GetDeviceSelector());

    // Register event handlers before starting the watcher.
    deviceWatcher.Added += DeviceWatcher_Added;
    deviceWatcher.Removed += DeviceWatcher_Removed;

    deviceWatcher.Start();
}
```


In the device watcher's **Added** event, each discovered device is represented by a [DeviceInformation](/uwp/api/Windows.Devices.Enumeration.DeviceInformation) object. Add each discovered device to an observable collection that is bound to the **ListView** control in the UI.

```csharp
private ObservableCollection<Windows.Devices.Enumeration.DeviceInformation> devices =
    new ObservableCollection<Windows.Devices.Enumeration.DeviceInformation>();
```


```csharp
private void DeviceWatcher_Added(DeviceWatcher sender, DeviceInformation args)
{
    // Collections bound to the UI are updated in the UI thread.
    DispatcherQueue.TryEnqueue( () =>
    {
        this.devices.Add(args);
    });
}
```


## Enable and release audio playback connections

Before opening a connection with a device, the connection must be enabled. This informs the system that there is a new application that wants audio from the remote device to be played on the PC, but audio does not begin playing until the connection is opened, which is shown in a later step.

In the click handler for the **Enable Audio Playback Connection** button, get the device ID associated with the currently selected device in the **ListView** control. This example maintains a dictionary of **AudioPlaybackConnection** objects that have been enabled. This method first checks to see if there is already an entry in the dictionary for the selected device. Next, the method attempts to create an **AudioPlaybackConnection** for the selected device by calling [TryCreateFromId](/uwp/api/windows.media.audio.audioplaybackconnection.trycreatefromid) and passing in the selected device ID. 

If the connection is successfully created, add the new **AudioPlaybackConnection** object to the app's dictionary, register a handler for the object's [StateChanged](/uwp/api/windows.media.audio.audioplaybackconnection.statechanged) event, and call [StartAsync](/uwp/api/windows.media.audio.audioplaybackconnection.startasync) to notify the system that the new connection is enabled. 

```csharp
private Dictionary<String, AudioPlaybackConnection> audioPlaybackConnections;
```

```csharp
private async void EnableAudioPlaybackConnectionButton_Click(object sender, RoutedEventArgs e)
{
    if (!(DeviceListView.SelectedItem is null))
    {
        var selectedDeviceId = (DeviceListView.SelectedItem as DeviceInformation).Id;
        if (!audioPlaybackConnections.ContainsKey(selectedDeviceId))
        {
            // Create the audio playback connection from the selected device id and add it to the dictionary.
            // This will result in allowing incoming connections from the remote device.
            var playbackConnection = AudioPlaybackConnection.TryCreateFromId(selectedDeviceId);

            if (playbackConnection != null)
            {
                // The device has an available audio playback connection.
                playbackConnection.StateChanged += PlaybackConnection_StateChanged;
                audioPlaybackConnections.Add(selectedDeviceId, playbackConnection);
                await playbackConnection.StartAsync();
                OpenAudioPlaybackConnectionButtonButton.IsEnabled = true;
            }
        }
    }
}
```


## Open the audio playback connection

In the previous step, an audio playback connection was created, but sound does not begin playing until the connection is opened by calling [Open](/uwp/api/windows.media.audio.audioplaybackconnection.open) or [OpenAsync](/uwp/api/windows.media.audio.audioplaybackconnection.openasync). In the **Open Audio Playback Connection** button click handler, get the currently selected device and use the ID to retrieve the **AudioPlaybackConnection** from the app's dictionary of connections. Await a call to **OpenAsync** and check the **Status** value of the returned [AudioPlaybackConnectionOpenResultStatus](/uwp/api/windows.media.audio.audioplaybackconnectionopenresult) object to see if the connection was opened successfully and, if so, update the connection state text box.


```csharp
private async void OpenAudioPlaybackConnectionButtonButton_Click(object sender, RoutedEventArgs e)
{
    var selectedDevice = (DeviceListView.SelectedItem as DeviceInformation).Id;
    AudioPlaybackConnection selectedConnection;

    if (this.audioPlaybackConnections.TryGetValue(selectedDevice, out selectedConnection))
    {
        if ((await selectedConnection.OpenAsync()).Status == AudioPlaybackConnectionOpenResultStatus.Success)
        {
            // Notify that the AudioPlaybackConnection is connected.
            ConnectionState.Text = "Connected";
        }
        else
        {
            // Notify that the connection attempt did not succeed.
            ConnectionState.Text = "Disconnected (attempt failed)";
        }
    }
}
```

## Monitor audio playback connection state

The [AudioPlaybackConnection.ConnectionStateChanged](/uwp/api/windows.media.audio.audioplaybackconnection.statechanged) event is raised whenever the state of the connection changes. In this example, the handler for this event updates the status text box. Remember to update the UI inside a call to [DispatcherQueue.TryEnqueue](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.tryenqueue) to make sure the update is made on the UI thread.

```csharp
private void PlaybackConnection_StateChanged(AudioPlaybackConnection sender, object args)
{
    DispatcherQueue.TryEnqueue( () =>
    {
        if (sender.State == AudioPlaybackConnectionState.Closed)
        {
            ConnectionState.Text = "Disconnected";
        }
        else if (sender.State == AudioPlaybackConnectionState.Opened)
        {
            ConnectionState.Text = "Connected";
        }
        else
        {
            ConnectionState.Text = "Unknown";
        }
    });
}
```

## Release connections and handle removed devices

This example provides a **Release Audio Playback Connection** button to allow the user to release an audio playback connection. In the handler for this event, we get the currently selected device and use the device's ID to look up the **AudioPlaybackConnection** in the dictionary. Call **Dispose** to release the reference and free any associated resources and remove the connection from the dictionary.

```csharp
private void ReleaseAudioPlaybackConnectionButton_Click(object sender, RoutedEventArgs e)
{
    // Check if an audio playback connection was already created for the selected device Id. If it was then release its reference to deactivate it.
    // The underlying transport is deactivated when all references are released.
    if (!(DeviceListView.SelectedItem is null))
    {
        var selectedDeviceId = (DeviceListView.SelectedItem as DeviceInformation).Id;
        if (audioPlaybackConnections.ContainsKey(selectedDeviceId))
        {
            AudioPlaybackConnection connectionToRemove = audioPlaybackConnections[selectedDeviceId];
            connectionToRemove.Dispose();
            this.audioPlaybackConnections.Remove(selectedDeviceId);

            // Notify that the media device has been deactivated.
            ConnectionState.Text = "Disconnected";
            OpenAudioPlaybackConnectionButtonButton.IsEnabled = false;
        }
    }
}
```

You should handle the case where a device is removed while a connection is enabled or open. To do this, implement a handler for the device watcher's [DeviceWatcher.Removed](/uwp/api/windows.devices.enumeration.devicewatcher.removed) event. First, the ID of the removed device is used to remove the device from the observable collection bound to the app's **ListView** control. Next, if a connection associated with this device is in the app's dictionary, **Dispose** is called to free the associated resources and then the connection is removed from the dictionary. All of this is done within a call to **DispatcherQueue.TryEnqueue** to make sure the UI updates are performed on the UI thread.

```csharp
private void DeviceWatcher_Removed(DeviceWatcher sender, DeviceInformationUpdate args)
{
    // Collections bound to the UI are updated in the UI thread.
    DispatcherQueue.TryEnqueue( () =>
    {
        // Find the device for the given id and remove it from the list.
        foreach (DeviceInformation device in this.devices)
        {
            if (device.Id == args.Id)
            {
                devices.Remove(device);
                break;
            }
        }

        if (audioPlaybackConnections.ContainsKey(args.Id))
        {
            AudioPlaybackConnection connectionToRemove = audioPlaybackConnections[args.Id];
            connectionToRemove.Dispose();
            audioPlaybackConnections.Remove(args.Id);
        }
    });
}
```

## Related topics

[Media Playback](media-playback.md)


 