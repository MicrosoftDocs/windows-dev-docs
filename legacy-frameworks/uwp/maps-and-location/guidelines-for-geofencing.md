---
description: See guidelines and best practices for using geofencing to provide geographically contextual experiences in your app.
title: Guidelines for geofencing apps
ms.assetid: F817FA55-325F-4302-81BE-37E6C7ADC281
ms.date: 06/21/2024
ms.topic: article
keywords: windows 10, uwp, map, location, geofencing
ms.localizationpriority: medium
---
# Guidelines for geofencing apps

Follow these best practices for [**geofencing**](/uwp/api/Windows.Devices.Geolocation.Geofencing) in your app.

**Important APIs**

-   [**Geofence class (XAML)**](/uwp/api/Windows.Devices.Geolocation.Geofencing.Geofence)
-   [**Geolocator class (XAML)**](/uwp/api/Windows.Devices.Geolocation.Geolocator)

## Recommendations

-   If your app will need internet access when a [**Geofence**](/uwp/api/Windows.Devices.Geolocation.Geofencing.Geofence) event occurs, check for internet access before creating the geofence.
    -   If the app doesn't currently have internet access, you can prompt the user to connect to the internet before you set up the geofence.
    -   If internet access isn't possible, avoid consuming the power required for the geofencing location checks.
-   Ensure the relevance of geofencing notifications by checking the time stamp and current location when a geofence event indicates changes to an [**Entered**](/uwp/api/Windows.Devices.Geolocation.Geofencing.GeofenceState) or **Exited** state. See [Checking the time stamp and current location](#checking-the-time-stamp-and-current-location) below for more information.
-   Create exceptions to manage cases when a device can't access location info, and notify the user if necessary. Location info may be unavailable because permissions are turned off, the device doesn't contain a GPS radio, the GPS signal is blocked, or the Wi-Fi signal isn't strong enough.
-   In general, it isn't necessary to listen for geofence events in the foreground and background at the same time. However, if your app needs to listen for geofence events in both the foreground and background:

    -   Call the [**ReadReports**](/uwp/api/windows.devices.geolocation.geofencing.geofencemonitor.readreports) method to find out if an event has occurred.
    -   Unregister your foreground event listener when your app isn't visible to the user and re-register when it becomes visible again.

    See [Background and foreground listeners](#background-and-foreground-listeners) for code examples and more information.

-   Don't use more than 1000 geofences per app. The system actually supports thousands of geofences per app, you can maintain good app performance to help reduce the app's memory usage by using no more than 1000.
-   Don't create a geofence with a radius smaller than 50 meters. If your app needs to use a geofence with a small radius, advise users to use your app on a device with a GPS radio to ensure the best performance.

## Additional usage guidance

### Checking the time stamp and current location

When an event indicates a change to an [**Entered**](/uwp/api/Windows.Devices.Geolocation.Geofencing.GeofenceState) or **Exited** state, check both the time stamp of the event and your current location. Various factors, such as the system not having enough resources to launch a background task, the user not noticing the notification, or the device being in standby, may affect when the event is actually processed by the user. For example, the following sequence may occur:

-   Your app creates a geofence and monitors the geofence for enter and exit events.
-   The user moves the device inside of the geofence, causing an enter event to be triggered.
-   Your app sends a notification to the user that they are now inside the geofence.
-   The user was busy and does not notice the notification until 10 minutes later.
-   During that 10 minute delay, the user has moved back outside of the geofence.

From the timestamp, you can tell that the action occurred in the past. From the current location, you can see that the user is now back outside of the geofence. Depending on the functionality of your app, you may want to filter out this event.

### Background and foreground listeners

In general, your app doesn't need to listen for [**Geofence**](/uwp/api/Windows.Devices.Geolocation.Geofencing.Geofence) events both in the foreground and in a background task at the same time. The cleanest method for handling a case where you might need both is to let the background task handle the notifications. If you do set up both foreground and background geofence listeners, there is no guarantee which will be triggered first and so you must always call the [**ReadReports**](/uwp/api/windows.devices.geolocation.geofencing.geofencemonitor.readreports) method to find out if an event has occurred.

If you have set up both foreground and background geofence listeners, you should unregister your foreground event listener whenever your app is not visible to the user and re-register your app when it becomes visible again. Here's some example code that registers for the visibility event.

```csharp
Windows.UI.Core.CoreWindow coreWindow;    

// This needs to be set before InitializeComponent sets up event registration for app visibility
coreWindow = CoreWindow.GetForCurrentThread();
coreWindow.VisibilityChanged += OnVisibilityChanged;
```

When the visibility changes, you can then enable or disable the foreground event handlers as shown here.

```csharp
private void OnVisibilityChanged(CoreWindow sender, VisibilityChangedEventArgs args)
{
    // NOTE: After the app is no longer visible on the screen and before the app is suspended
    // you might want your app to use toast notification for any geofence activity.
    // By registering for VisibiltyChanged the app is notified when the app is no longer visible in the foreground.

    if (args.Visible)
    {
        // register for foreground events
        GeofenceMonitor.Current.GeofenceStateChanged += OnGeofenceStateChanged;
        GeofenceMonitor.Current.StatusChanged += OnGeofenceStatusChanged;
    }
    else
    {
        // unregister foreground events (let background capture events)
        GeofenceMonitor.Current.GeofenceStateChanged -= OnGeofenceStateChanged;
        GeofenceMonitor.Current.StatusChanged -= OnGeofenceStatusChanged;
    }
}
```

### Sizing your geofences

While GPS can provide the most accurate location info, geofencing can also use Wi-Fi or other location sensors to determine the user's current position. But using these other methods can affect the size of the geofences you can create. If the accuracy level is low, creating small geofences won't be useful. In general, it is recommended that you do not create a geofence with a radius smaller than 50 meters. Also, geofence background tasks only run periodically on Windows; if you use a small geofence, there's a possibility that you could miss an [**Enter**](/uwp/api/Windows.Devices.Geolocation.Geofencing.GeofenceState) or **Exit** event entirely.

If your app needs to use a geofence with a small radius, advise users to use your app on a device with a GPS radio to ensure the best performance.

## Related topics

* [Set up a geofence](./set-up-a-geofence.md)
* [Get current location](./get-location.md)
* [UWP location sample (geolocation)](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Geolocation)
