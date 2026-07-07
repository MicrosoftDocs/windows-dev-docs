---
title: Set up a geofence
description: Define geographic boundaries and receive notifications when the user enters or exits them in a Windows App SDK (WinUI 3) app.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/06/2026
---

# Set up a geofence

A [Geofence](/uwp/api/Windows.Devices.Geolocation.Geofencing.Geofence) defines a circular geographic boundary around a point of interest. Your app receives notifications when the user enters or exits the boundary. Geofences are useful for location-based reminders, alerts, check-ins, and contextual content delivery.

This article shows how to create a geofence, monitor state changes, and handle geofence events in a Windows App SDK (WinUI 3) app.

## Prerequisites

- A WinUI 3 project created from the **Blank App, Packaged (WinUI 3 in Desktop)** template.
- The **Location** capability declared in the package manifest. See [Get the user's location](get-location.md#enable-the-location-capability) for instructions.

## Request location access

Call [Geolocator.RequestAccessAsync](/uwp/api/windows.devices.geolocation.geolocator.requestaccessasync) before creating or monitoring geofences.

```csharp
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;

var accessStatus = await Geolocator.RequestAccessAsync();
if (accessStatus != GeolocationAccessStatus.Allowed)
{
    StatusText.Text = "Location access is required for geofencing.";
    return;
}
```

## Create a geofence

Define a geofence by specifying an identifier, a geographic center, a radius in meters, and which state transitions to monitor (entered, exited, or removed).

```csharp
var position = new BasicGeoposition
{
    Latitude = 47.6062,
    Longitude = -122.3321
};

var geocircle = new Geocircle(position, 200); // 200-meter radius

var geofence = new Geofence(
    "SeattleDowntown",                          // Unique identifier
    geocircle,                                  // Geographic boundary
    MonitoredGeofenceStates.Entered |           // States to monitor
    MonitoredGeofenceStates.Exited,
    false,                                      // Single use: false = persistent
    TimeSpan.FromSeconds(10)                    // Dwell time before triggering
);

GeofenceMonitor.Current.Geofences.Add(geofence);
```

### Geofence parameters

| Parameter | Description |
|-----------|-------------|
| `id` | A unique string that identifies the geofence. Use this to distinguish events from different geofences. |
| `geoshape` | A [Geocircle](/uwp/api/Windows.Devices.Geolocation.Geocircle) that defines the boundary. Only circular boundaries are supported. |
| `monitoredStates` | Which transitions to monitor: `Entered`, `Exited`, or `Removed`. Combine with the `|` operator. |
| `singleUse` | If `true`, the geofence triggers once and is removed automatically. |
| `dwellTime` | How long the user must remain inside (or outside) the boundary before the event fires. Helps filter out brief crossings. |

## Monitor geofence events in the foreground

Subscribe to the [GeofenceMonitor.Current.GeofenceStateChanged](/uwp/api/windows.devices.geolocation.geofencing.geofencemonitor.geofencestatechanged) event to receive notifications while your app is running.

```csharp
GeofenceMonitor.Current.GeofenceStateChanged += OnGeofenceStateChanged;
```

Read the reports and update the UI. The event fires on a background thread, so use `DispatcherQueue` to marshal UI updates.

```csharp
private void OnGeofenceStateChanged(GeofenceMonitor sender, object args)
{
    var reports = sender.ReadReports();

    DispatcherQueue.TryEnqueue(() =>
    {
        foreach (var report in reports)
        {
            var state = report.NewState;
            var id = report.Geofence.Id;

            switch (state)
            {
                case GeofenceState.Entered:
                    StatusText.Text = $"Entered geofence: {id}";
                    break;
                case GeofenceState.Exited:
                    StatusText.Text = $"Exited geofence: {id}";
                    break;
                case GeofenceState.Removed:
                    StatusText.Text = $"Geofence removed: {id}";
                    // Re-add the geofence if it was removed due to expiration
                    break;
            }
        }
    });
}
```

## Monitor geofence status changes

Use [GeofenceMonitor.Current.StatusChanged](/uwp/api/windows.devices.geolocation.geofencing.geofencemonitor.statuschanged) to detect when geofence monitoring is disabled — for example, when the user turns off location services.

```csharp
GeofenceMonitor.Current.StatusChanged += (sender, args) =>
{
    DispatcherQueue.TryEnqueue(() =>
    {
        var status = sender.Status;
        if (status == GeofenceMonitorStatus.Disabled)
        {
            StatusText.Text = "Geofence monitoring is disabled. Check location settings.";
        }
    });
};
```

> [!TIP]
> When using geofences, monitor permission changes through [GeofenceMonitor.StatusChanged](/uwp/api/windows.devices.geolocation.geofencing.geofencemonitor.statuschanged) rather than [Geolocator.StatusChanged](/uwp/api/windows.devices.geolocation.geolocator.statuschanged). A [GeofenceMonitorStatus](/uwp/api/Windows.Devices.Geolocation.Geofencing.GeofenceMonitorStatus) value of `Disabled` is equivalent to a `PositionStatus` of `Disabled`, but `GeofenceMonitorStatus` provides more context for geofencing scenarios.

## Remove a geofence

Remove a geofence by finding it in the [GeofenceMonitor.Current.Geofences](/uwp/api/windows.devices.geolocation.geofencing.geofencemonitor.geofences) collection.

```csharp
var geofences = GeofenceMonitor.Current.Geofences;
var target = geofences.FirstOrDefault(g => g.Id == "SeattleDowntown");

if (target != null)
{
    geofences.Remove(target);
}
```

## Best practices

- **Set a reasonable dwell time.** A dwell time of at least 10 seconds helps filter out GPS jitter and prevents false triggers at boundary edges.
- **Use a radius of at least 50 meters.** GPS accuracy varies by device and environment. A radius smaller than 50 meters may produce unreliable results.
- **Check internet access if needed.** If your app performs network operations when a geofence event fires (such as sending a notification to a server), verify connectivity before creating the geofence.
- **Handle the `Removed` state.** Geofences can be removed by the system if they expire or if the system is under resource pressure. Check for `GeofenceState.Removed` and re-create the geofence if your scenario requires it.
- **Don't monitor foreground and background simultaneously** for the same geofence unless necessary. If you do, unregister the foreground listener when the app is suspended and re-register when it resumes.

## Related articles

- [Maps and location overview](index.md)
- [Get the user's location](get-location.md)
- [MapControl](../ui/controls/map-control.md)
- [Geofence class](/uwp/api/Windows.Devices.Geolocation.Geofencing.Geofence)
- [GeofenceMonitor class](/uwp/api/Windows.Devices.Geolocation.Geofencing.GeofenceMonitor)
