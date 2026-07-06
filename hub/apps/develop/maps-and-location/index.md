---
title: Maps and location overview
description: Display interactive maps, detect the user's geographic position, and set up geofences in Windows App SDK and WinUI 3 apps.
author: GrantMeStrength
ms.author: jken
ms.topic: overview
ms.date: 07/06/2026
---

# Maps and location overview

Windows App SDK and WinUI 3 provide APIs and controls for displaying maps, detecting the user's location, and setting up geofences. Use these capabilities to build apps that show interactive maps with pins, track the user's position, and trigger actions when the user enters or leaves a geographic area.

This article introduces each capability and includes a [complete example](#complete-example) that combines `MapControl`, `Geolocator`, and `GeofenceMonitor` in a single working app.

## Display maps with MapControl

The [MapControl](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mapcontrol) displays an interactive map powered by [Azure Maps](/azure/azure-maps/about-azure-maps). You can add pins, layers, and respond to user interactions like pan, zoom, and click.

MapControl requires an Azure Maps account. See [Manage your Azure Maps account](/azure/azure-maps/how-to-manage-account-keys) to create an account and obtain a service token.

For detailed usage instructions, see [MapControl](../ui/controls/map-control.md).

```xaml
<MapControl x:Name="myMap"
            MapServiceToken="YOUR_AZURE_MAPS_TOKEN"
            Height="400" />
```

> [!NOTE]
> The UWP [MapControl](/uwp/api/windows.ui.xaml.controls.maps.mapcontrol) and [Windows.Services.Maps](/uwp/api/windows.services.maps) APIs are deprecated and may not be available in future versions of Windows. WinUI 3 apps should use the new MapControl described above. For more information, see [Resources for deprecated features](/windows/whats-new/deprecated-features-resources#windows-uwp-map-control-and-windows-maps-platform-apis).

## Detect the user's location

The [Windows.Devices.Geolocation](/uwp/api/Windows.Devices.Geolocation) APIs let you obtain the device's geographic position. These APIs work in both UWP and Windows App SDK (WinUI 3) apps. You can:

- **Get a one-time position** using [Geolocator.GetGeopositionAsync](/uwp/api/windows.devices.geolocation.geolocator.getgeopositionasync).
- **Track position changes** over time using the [Geolocator.PositionChanged](/uwp/api/windows.devices.geolocation.geolocator.positionchanged) event.
- **Monitor visit state changes** using [GeovisitMonitor](/uwp/api/windows.devices.geolocation.geovisitmonitor) for battery-efficient location awareness.

For a step-by-step guide, see [Get the user's location](get-location.md).

## Set up geofences

A [Geofence](/uwp/api/Windows.Devices.Geolocation.Geofencing.Geofence) defines a geographic boundary. Your app receives notifications when the user enters or exits the boundary. Geofences are useful for location-based reminders, alerts, or content delivery.

For instructions on creating and monitoring geofences, see [Set up a geofence](geofencing.md).

## Location capability and privacy

All location APIs require the **Location** capability declared in your app's package manifest. You must also call [Geolocator.RequestAccessAsync](/uwp/api/windows.devices.geolocation.geolocator.requestaccessasync) at runtime before accessing location data.

Windows gives users control over which apps can access their location through **Settings > Privacy & security > Location**. Your app should handle the case where the user denies or revokes location access.

## Complete example

The following example brings together `MapControl`, `Geolocator`, and `GeofenceMonitor` in a single WinUI 3 window. If no Azure Maps key is configured, the map degrades gracefully while geolocation and geofencing continue to work.

### Prerequisites

- Windows App SDK 2.2 or later
- An [Azure Maps key](/azure/azure-maps/how-to-manage-account-keys) — required to display map tiles. Without a valid key, the MapControl renders but shows a blank map.
- The **Location** device capability declared in `Package.appxmanifest`:

```xml
<DeviceCapability Name="location" />
```

Set your Azure Maps key as an environment variable before running the app:

```powershell
$env:AZURE_MAPS_KEY = "your-key-here"
```

### MainWindow.xaml

A 300-pixel control panel on the left with buttons and status text, and a `MapControl` on the right. An overlay appears when the Azure Maps key is missing.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<Window
    x:Class="MapLocationDemo.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    Title="Map Location Demo">
    <Window.SystemBackdrop>
        <MicaBackdrop />
    </Window.SystemBackdrop>

    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto" />
            <RowDefinition Height="*" />
        </Grid.RowDefinitions>

        <TitleBar Title="Map Location Demo" />

        <Grid Grid.Row="1">
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="300" />
                <ColumnDefinition Width="*" />
            </Grid.ColumnDefinitions>

            <Grid Grid.Column="0" Margin="16" RowSpacing="12">
                <Grid.RowDefinitions>
                    <RowDefinition Height="Auto"/>
                    <RowDefinition Height="Auto"/>
                    <RowDefinition Height="Auto"/>
                    <RowDefinition Height="*"/>
                </Grid.RowDefinitions>

                <TextBlock Text="Location Demo" FontSize="20" FontWeight="Bold"/>
                <StackPanel Grid.Row="1" Spacing="8">
                    <Button x:Name="FindMeButton" Content="Find My Location"
                            Click="FindMeButton_Click" HorizontalAlignment="Stretch"/>
                    <Button x:Name="AddGeofenceButton" Content="Add Geofence Here"
                            Click="AddGeofenceButton_Click" HorizontalAlignment="Stretch"/>
                </StackPanel>
                <TextBlock x:Name="StatusText" Grid.Row="2"
                           Text="Click 'Find My Location' to begin." TextWrapping="Wrap"/>
                <ListView x:Name="EventLog" Grid.Row="3" Header="Event Log"/>
            </Grid>

            <Grid Grid.Column="1">
                <MapControl x:Name="MyMap" />
                <StackPanel x:Name="MapKeyMissing" Visibility="Collapsed"
                            HorizontalAlignment="Center" VerticalAlignment="Center"
                            Spacing="8">
                    <FontIcon Glyph="&#xE783;" FontSize="48"
                              HorizontalAlignment="Center"
                              Foreground="{ThemeResource SystemFillColorCautionBrush}" />
                    <TextBlock Text="Azure Maps key not configured"
                               FontSize="18" FontWeight="SemiBold"
                               HorizontalAlignment="Center" />
                    <TextBlock x:Name="MapKeyHint" TextWrapping="Wrap" MaxWidth="400"
                               HorizontalAlignment="Center" TextAlignment="Center"
                               Foreground="{ThemeResource TextFillColorSecondaryBrush}" />
                </StackPanel>
            </Grid>
        </Grid>
    </Grid>
</Window>
```

### MainWindow.xaml.cs

```csharp
using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;

namespace MapLocationDemo;

public sealed partial class MainWindow : Window
{
    private Geolocator? _geolocator;
    private BasicGeoposition _lastPosition;
    private bool _mapAvailable;

    public MainWindow()
    {
        InitializeComponent();
        _mapAvailable = TryConfigureMap();
        GeofenceMonitor.Current.GeofenceStateChanged += OnGeofenceStateChanged;
    }

    // Read the Azure Maps key from an environment variable.
    // If missing, collapse the map and show an informational overlay.
    private bool TryConfigureMap()
    {
        var key = Environment.GetEnvironmentVariable("AZURE_MAPS_KEY");
        if (string.IsNullOrWhiteSpace(key))
        {
            MyMap.Visibility = Visibility.Collapsed;
            MapKeyMissing.Visibility = Visibility.Visible;
            MapKeyHint.Text = "Set the AZURE_MAPS_KEY environment variable "
                + "and restart.\nGeolocation and geofencing still work "
                + "without the map.";
            Log("Azure Maps key not found — map disabled");
            return false;
        }
        MyMap.MapServiceToken = key;
        return true;
    }

    private async void FindMeButton_Click(object sender, RoutedEventArgs e)
    {
        FindMeButton.IsEnabled = false;
        StatusText.Text = "Requesting location access...";

        var access = await Geolocator.RequestAccessAsync();
        if (access != GeolocationAccessStatus.Allowed)
        {
            StatusText.Text =
                "Location access denied. Check Settings > Privacy > Location.";
            FindMeButton.IsEnabled = true;
            return;
        }

        _geolocator = new Geolocator { DesiredAccuracyInMeters = 100 };
        try
        {
            var pos = await _geolocator.GetGeopositionAsync();
            var lat = pos.Coordinate.Point.Position.Latitude;
            var lon = pos.Coordinate.Point.Position.Longitude;
            _lastPosition = new BasicGeoposition
            {
                Latitude = lat, Longitude = lon
            };

            StatusText.Text =
                $"Location: {lat:F5}, {lon:F5}  ({pos.Coordinate.Accuracy:F0} m)";
            Log($"Position: {lat:F5}, {lon:F5}");

            if (_mapAvailable)
            {
                var pt = new Geopoint(_lastPosition);
                MyMap.Center = pt;
                MyMap.ZoomLevel = 15;

                var layer = new MapElementsLayer();
                layer.MapElements = new List<MapElement>
                {
                    new MapIcon { Location = pt }
                };
                MyMap.Layers.Clear();
                MyMap.Layers.Add(layer);
            }
        }
        catch (Exception ex) { StatusText.Text = $"Error: {ex.Message}"; }
        finally { FindMeButton.IsEnabled = true; }
    }

    private void AddGeofenceButton_Click(object sender, RoutedEventArgs e)
    {
        if (_lastPosition.Latitude == 0 && _lastPosition.Longitude == 0)
        {
            StatusText.Text = "Get your location first.";
            return;
        }

        var fence = new Geofence("MyGeofence",
            new Geocircle(_lastPosition, 200),
            MonitoredGeofenceStates.Entered | MonitoredGeofenceStates.Exited,
            false, TimeSpan.FromSeconds(5));
        GeofenceMonitor.Current.Geofences.Add(fence);

        StatusText.Text = $"Geofence added at "
            + $"{_lastPosition.Latitude:F5}, {_lastPosition.Longitude:F5}";
        Log("Geofence registered");
    }

    private void OnGeofenceStateChanged(
        GeofenceMonitor sender, object args)
    {
        var reports = sender.ReadReports();
        DispatcherQueue.TryEnqueue(() =>
        {
            foreach (var r in reports)
            {
                var msg = r.NewState switch
                {
                    GeofenceState.Entered => $"Entered: {r.Geofence.Id}",
                    GeofenceState.Exited  => $"Exited: {r.Geofence.Id}",
                    GeofenceState.Removed => $"Removed: {r.Geofence.Id}",
                    _ => null
                };
                if (msg != null) { StatusText.Text = msg; Log(msg); }
            }
        });
    }

    private void Log(string msg) =>
        EventLog.Items.Insert(0, $"[{DateTime.Now:HH:mm:ss}] {msg}");
}
```

### Key patterns

- **`MapControl`** is built into Windows App SDK 1.6 and later. Set `MapServiceToken` to your Azure Maps key.
- **`TryConfigureMap`** checks the `AZURE_MAPS_KEY` environment variable on startup. If the variable is empty, the map collapses and an overlay explains how to fix it — no crash, no blank map.
- **`DeviceCapability Name="location"`** in `Package.appxmanifest` is required or `Geolocator.RequestAccessAsync` returns `Denied`.
- **`GeofenceMonitor.GeofenceStateChanged`** fires on a background thread, so use `DispatcherQueue.TryEnqueue` to update UI.

## Related articles

- [MapControl](../ui/controls/map-control.md)
- [Get the user's location](get-location.md)
- [Set up a geofence](geofencing.md)
- [Azure Maps documentation](/azure/azure-maps/about-azure-maps)
