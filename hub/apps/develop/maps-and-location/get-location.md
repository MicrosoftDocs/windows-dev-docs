---
title: Get the user's location
description: Detect the user's geographic position, track location changes, and handle permissions in a Windows App SDK (WinUI 3) app.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/06/2026
---

# Get the user's location

Use the [Windows.Devices.Geolocation](/uwp/api/Windows.Devices.Geolocation) APIs to detect the device's geographic position in a Windows App SDK (WinUI 3) app. You can get a one-time location fix or track the user's position over time. This article covers permission handling, one-time and continuous location reads, and how to update your UI when the location changes.

> [!NOTE]
> The `Windows.Devices.Geolocation` APIs are Windows Runtime (WinRT) APIs that work in both UWP and WinUI 3 desktop apps. The code in this article uses WinUI 3 patterns (such as `DispatcherQueue` for thread marshaling).

## Prerequisites

- A WinUI 3 project created from the **Blank App, Packaged (WinUI 3 in Desktop)** template.
- The **Location** capability declared in the package manifest (see [Enable the location capability](#enable-the-location-capability)).

## Enable the location capability

Your app must declare the **Location** capability before it can access the user's position.

1. In **Solution Explorer**, double-click **Package.appxmanifest** and select the **Capabilities** tab.
2. Check the box for **Location**.

This adds the following entry to the manifest:

```xml
<Capabilities>
    <DeviceCapability Name="location"/>
</Capabilities>
```

> [!TIP]
> For unpackaged apps, the Location capability is not required in a manifest. However, you must still call [RequestAccessAsync](/uwp/api/windows.devices.geolocation.geolocator.requestaccessasync) to prompt the user for permission.

## Get the current location

Follow these steps to perform a one-time position read.

### Request access to the user's location

Call [Geolocator.RequestAccessAsync](/uwp/api/windows.devices.geolocation.geolocator.requestaccessasync) before accessing location data. This method prompts the user for permission the first time it runs. You must call it from the UI thread while your app is in the foreground.

```csharp
using Windows.Devices.Geolocation;

var accessStatus = await Geolocator.RequestAccessAsync();
```

### Read the position

If the user grants permission, create a [Geolocator](/uwp/api/Windows.Devices.Geolocation.Geolocator) and call [GetGeopositionAsync](/uwp/api/windows.devices.geolocation.geolocator.getgeopositionasync) to get a one-time position fix.

```csharp
switch (accessStatus)
{
    case GeolocationAccessStatus.Allowed:
        var geolocator = new Geolocator { DesiredAccuracyInMeters = 50 };
        Geoposition position = await geolocator.GetGeopositionAsync();

        double latitude = position.Coordinate.Point.Position.Latitude;
        double longitude = position.Coordinate.Point.Position.Longitude;

        StatusText.Text = $"Location: {latitude:F4}, {longitude:F4}";
        break;

    case GeolocationAccessStatus.Denied:
        StatusText.Text = "Location access is denied.";
        break;

    case GeolocationAccessStatus.Unspecified:
        StatusText.Text = "An unspecified error occurred.";
        break;
}
```

## Track the user's location over time

To receive periodic location updates, subscribe to the [PositionChanged](/uwp/api/windows.devices.geolocation.geolocator.positionchanged) event. Set [ReportInterval](/uwp/api/windows.devices.geolocation.geolocator.reportinterval) for time-based tracking or [MovementThreshold](/uwp/api/windows.devices.geolocation.geolocator.movementthreshold) for distance-based tracking.

```csharp
if (accessStatus == GeolocationAccessStatus.Allowed)
{
    var geolocator = new Geolocator { ReportInterval = 2000 }; // 2-second interval

    geolocator.PositionChanged += OnPositionChanged;
    geolocator.StatusChanged += OnStatusChanged;
}
```

### Handle position updates

The `PositionChanged` event fires on a background thread. Use `DispatcherQueue` to marshal UI updates back to the main thread.

```csharp
private void OnPositionChanged(Geolocator sender, PositionChangedEventArgs args)
{
    DispatcherQueue.TryEnqueue(() =>
    {
        var position = args.Position.Coordinate.Point.Position;
        StatusText.Text = $"Updated: {position.Latitude:F4}, {position.Longitude:F4}";
    });
}
```

### Handle status changes

Monitor [StatusChanged](/uwp/api/windows.devices.geolocation.geolocator.statuschanged) to detect when the user disables location services or the GPS signal is lost.

```csharp
private void OnStatusChanged(Geolocator sender, StatusChangedEventArgs args)
{
    DispatcherQueue.TryEnqueue(() =>
    {
        switch (args.Status)
        {
            case PositionStatus.Ready:
                StatusText.Text = "Location is available.";
                break;
            case PositionStatus.Disabled:
                StatusText.Text = "Location is disabled. Check Settings.";
                break;
            case PositionStatus.NoData:
                StatusText.Text = "Unable to determine location.";
                break;
            case PositionStatus.NotAvailable:
                StatusText.Text = "Location is not available on this device.";
                break;
        }
    });
}
```

## Direct the user to location settings

If the user denies location access, provide a link to the Windows privacy settings so they can change their preference.

```xaml
<HyperlinkButton Content="Open location settings"
                 NavigateUri="ms-settings:privacy-location" />
```

You can also open the settings page from code:

```csharp
await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-location"));
```

## Show the location on a map

After you retrieve a position, you can display it on a WinUI 3 [MapControl](../ui/controls/map-control.md) by setting its `Center` and adding a `MapIcon`. For a complete working example that combines geolocation, map display, and geofencing, see the [Maps and location overview](index.md#complete-example).

## Troubleshooting

If your app cannot retrieve a location, verify the following in **Settings > Privacy & security > Location**:

- **Location services** is turned **On**.
- Your app is listed and set to **On** under **Let apps access your location**.
- The device has a working GPS receiver or network-based positioning is available.

## Related articles

- [Maps and location overview](index.md)
- [Set up a geofence](geofencing.md)
- [MapControl](../ui/controls/map-control.md)
- [Geolocator class](/uwp/api/Windows.Devices.Geolocation.Geolocator)
- [Windows.Devices.Geolocation namespace](/uwp/api/Windows.Devices.Geolocation)
