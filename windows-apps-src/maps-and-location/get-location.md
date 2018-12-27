---
title: Get the user's location
description: Find the user's location and respond to changes in location. Access to the user's location is managed by privacy settings in the Settings app. This topic also shows how to check if your app has permission to access the user's location.
ms.assetid: 24DC9A41-8CC1-48B0-BC6D-24BF571AFCC8
ms.date: 11/28/2017
ms.topic: article
keywords: windows 10, uwp, map, location, location capability
ms.localizationpriority: medium
---
# Get the user's location




Find the user's location and respond to changes in location. Access to the user's location is managed by privacy settings in the Settings app. This topic also shows how to check if your app has permission to access the user's location.

**Tip** To learn more about accessing the user's location in your app, download the following sample from the [Windows-universal-samples repo](http://go.microsoft.com/fwlink/p/?LinkId=619979) on GitHub.

-   [Universal Windows Platform (UWP) map sample](http://go.microsoft.com/fwlink/p/?LinkId=619977)

## Enable the location capability


1.  In **Solution Explorer**, double-click **package.appxmanifest** and select the **Capabilities** tab.
2.  In the **Capabilities** list, check the box for **Location**. This adds the `location` device capability to the package manifest file.

```XML
  <Capabilities>
    <!-- DeviceCapability elements must follow Capability elements (if present) -->
    <DeviceCapability Name="location"/>
  </Capabilities>
```

## Get the current location


This section describes how to detect the user's geographic location using APIs in the [**Windows.Devices.Geolocation**](https://msdn.microsoft.com/library/windows/apps/br225603) namespace.

### Step 1: Request access to the user's location

Unless your app has coarse location capability (see note), you must request access to the user's location by using the [**RequestAccessAsync**](https://msdn.microsoft.com/library/windows/apps/dn859152) method before attempting to access the location. You must call the **RequestAccessAsync** method from the UI thread and your app must be in the foreground. Your app will not be able to access the user's location information until after the user grants permission to your app.\*

```csharp
using Windows.Devices.Geolocation;
...
var accessStatus = await Geolocator.RequestAccessAsync();
```



The [**RequestAccessAsync**](https://msdn.microsoft.com/library/windows/apps/dn859152) method prompts the user for permission to access their location. The user is only prompted once (per app). After the first time they grant or deny permission, this method no longer prompts the user for permission. To help the user change location permissions after they've been prompted, we recommend that you provide a link to the location settings as demonstrated later in this topic.

>Note:  The coarse location feature allows your app to obtain an intentionally obfuscated (imprecise) location without getting the user's explicit permission (the system-wide location switch must still be **on**, however). To learn how to utilize coarse location in your app, see the [**AllowFallbackToConsentlessPositions**](https://msdn.microsoft.com/library/windows/apps/Windows.Devices.Geolocation.Geolocator.AllowFallbackToConsentlessPositions) method in the [**Geolocator**](https://msdn.microsoft.com/library/windows/apps/windows.devices.geolocation.geolocator.aspx) class.

### Step 2: Get the user's location and register for changes in location permissions

The [**GetGeopositionAsync**](https://msdn.microsoft.com/library/windows/apps/hh973536) method performs a one-time reading of the current location. Here, a **switch** statement is used with **accessStatus** (from the previous example) to act only when access to the user's location is allowed. If access to the user's location is allowed, the code creates a [**Geolocator**](https://msdn.microsoft.com/library/windows/apps/br225534) object, registers for changes in location permissions, and requests the user's location.

```csharp
switch (accessStatus)
{
    case GeolocationAccessStatus.Allowed:
        _rootPage.NotifyUser("Waiting for update...", NotifyType.StatusMessage);

        // If DesiredAccuracy or DesiredAccuracyInMeters are not set (or value is 0), DesiredAccuracy.Default is used.
        Geolocator geolocator = new Geolocator { DesiredAccuracyInMeters = _desireAccuracyInMetersValue };

        // Subscribe to the StatusChanged event to get updates of location status changes.
        _geolocator.StatusChanged += OnStatusChanged;

        // Carry out the operation.
        Geoposition pos = await geolocator.GetGeopositionAsync();

        UpdateLocationData(pos);
        _rootPage.NotifyUser("Location updated.", NotifyType.StatusMessage);
        break;

    case GeolocationAccessStatus.Denied:
        _rootPage.NotifyUser("Access to location is denied.", NotifyType.ErrorMessage);
        LocationDisabledMessage.Visibility = Visibility.Visible;
        UpdateLocationData(null);
        break;

    case GeolocationAccessStatus.Unspecified:
        _rootPage.NotifyUser("Unspecified error.", NotifyType.ErrorMessage);
        UpdateLocationData(null);
        break;
}
```

### Step 3: Handle changes in location permissions

The [**Geolocator**](https://msdn.microsoft.com/library/windows/apps/br225534) object triggers the [**StatusChanged**](https://msdn.microsoft.com/library/windows/apps/br225542) event to indicate that the user's location settings changed. That event passes the corresponding status via the argument's **Status** property (of type [**PositionStatus**](https://msdn.microsoft.com/library/windows/apps/br225599)). Note that this method is not called from the UI thread and the [**Dispatcher**](https://msdn.microsoft.com/library/windows/apps/br208211) object invokes the UI changes.

```csharp
using Windows.UI.Core;
...
async private void OnStatusChanged(Geolocator sender, StatusChangedEventArgs e)
{
    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
    {
        // Show the location setting message only if status is disabled.
        LocationDisabledMessage.Visibility = Visibility.Collapsed;

        switch (e.Status)
        {
            case PositionStatus.Ready:
                // Location platform is providing valid data.
                ScenarioOutput_Status.Text = "Ready";
                _rootPage.NotifyUser("Location platform is ready.", NotifyType.StatusMessage);
                break;

            case PositionStatus.Initializing:
                // Location platform is attempting to acquire a fix.
                ScenarioOutput_Status.Text = "Initializing";
                _rootPage.NotifyUser("Location platform is attempting to obtain a position.", NotifyType.StatusMessage);
                break;

            case PositionStatus.NoData:
                // Location platform could not obtain location data.
                ScenarioOutput_Status.Text = "No data";
                _rootPage.NotifyUser("Not able to determine the location.", NotifyType.ErrorMessage);
                break;

            case PositionStatus.Disabled:
                // The permission to access location data is denied by the user or other policies.
                ScenarioOutput_Status.Text = "Disabled";
                _rootPage.NotifyUser("Access to location is denied.", NotifyType.ErrorMessage);

                // Show message to the user to go to location settings.
                LocationDisabledMessage.Visibility = Visibility.Visible;

                // Clear any cached location data.
                UpdateLocationData(null);
                break;

            case PositionStatus.NotInitialized:
                // The location platform is not initialized. This indicates that the application
                // has not made a request for location data.
                ScenarioOutput_Status.Text = "Not initialized";
                _rootPage.NotifyUser("No request for location is made yet.", NotifyType.StatusMessage);
                break;

            case PositionStatus.NotAvailable:
                // The location platform is not available on this version of the OS.
                ScenarioOutput_Status.Text = "Not available";
                _rootPage.NotifyUser("Location is not available on this version of the OS.", NotifyType.ErrorMessage);
                break;

            default:
                ScenarioOutput_Status.Text = "Unknown";
                _rootPage.NotifyUser(string.Empty, NotifyType.StatusMessage);
                break;
        }
    });
}
```

## Respond to location updates


This section describes how to use the [**PositionChanged**](https://msdn.microsoft.com/library/windows/apps/br225540) event to receive updates of the user's location over a period of time. Because the user could revoke access to location at any time, it's important call [**RequestAccessAsync**](https://msdn.microsoft.com/library/windows/apps/dn859152) and use the [**StatusChanged**](https://msdn.microsoft.com/library/windows/apps/br225542) event as shown in the previous section.

This section assumes that you've already enabled the location capability and called [**RequestAccessAsync**](https://msdn.microsoft.com/library/windows/apps/dn859152) from the UI thread of your foreground app.

### Step 1: Define the report interval and register for location updates

In this example, a **switch** statement is used with **accessStatus** (from the previous example) to act only when access to the user's location is allowed. If access to the user's location is allowed, the code creates a [**Geolocator**](https://msdn.microsoft.com/library/windows/apps/br225534) object, specifies the tracking type, and registers for location updates.

The [**Geolocator**](https://msdn.microsoft.com/library/windows/apps/br225534) object can trigger the [**PositionChanged**](https://msdn.microsoft.com/library/windows/apps/br225540) event based on a change in position (distance-based tracking) or a change in time (periodic-based tracking).

-   For distance-based tracking, set the [**MovementThreshold**](https://msdn.microsoft.com/library/windows/apps/br225539) property.
-   For periodic-based tracking, set the [**ReportInterval**](https://msdn.microsoft.com/library/windows/apps/br225541) property.

If neither property is set, a position is returned every 1 second (equivalent to `ReportInterval = 1000`). Here, a 2 second (`ReportInterval = 2000`) report interval is used.
```csharp
using Windows.Devices.Geolocation;
...
var accessStatus = await Geolocator.RequestAccessAsync();

switch (accessStatus)
{
    case GeolocationAccessStatus.Allowed:
        // Create Geolocator and define perodic-based tracking (2 second interval).
        _geolocator = new Geolocator { ReportInterval = 2000 };

        // Subscribe to the PositionChanged event to get location updates.
        _geolocator.PositionChanged += OnPositionChanged;

        // Subscribe to StatusChanged event to get updates of location status changes.
        _geolocator.StatusChanged += OnStatusChanged;

        _rootPage.NotifyUser("Waiting for update...", NotifyType.StatusMessage);
        LocationDisabledMessage.Visibility = Visibility.Collapsed;
        StartTrackingButton.IsEnabled = false;
        StopTrackingButton.IsEnabled = true;
        break;

    case GeolocationAccessStatus.Denied:
        _rootPage.NotifyUser("Access to location is denied.", NotifyType.ErrorMessage);
        LocationDisabledMessage.Visibility = Visibility.Visible;
        break;

    case GeolocationAccessStatus.Unspecified:
        _rootPage.NotifyUser("Unspecificed error!", NotifyType.ErrorMessage);
        LocationDisabledMessage.Visibility = Visibility.Collapsed;
        break;
}
```

### Step 2: Handle location updates

The [**Geolocator**](https://msdn.microsoft.com/library/windows/apps/br225534) object triggers the [**PositionChanged**](https://msdn.microsoft.com/library/windows/apps/br225540) event to indicate that the user's location changed or time has passed, depending on how you've configured it. That event passes the corresponding location via the argument's **Position** property (of type [**Geoposition**](https://msdn.microsoft.com/library/windows/apps/br225543)). In this example, the method is not called from the UI thread and the [**Dispatcher**](https://msdn.microsoft.com/library/windows/apps/br208211) object invokes the UI changes.

```csharp
using Windows.UI.Core;
...
async private void OnPositionChanged(Geolocator sender, PositionChangedEventArgs e)
{
    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
    {
        _rootPage.NotifyUser("Location updated.", NotifyType.StatusMessage);
        UpdateLocationData(e.Position);
    });
}
```

## Change the location privacy settings


If the location privacy settings don't allow your app to access the user's location, we recommend that you provide a convenient link to the **location privacy settings** in the **Settings** app. In this example, a Hyperlink control is used navigate to the `ms-settings:privacy-location` URI.

```xml
<!--Set Visibility to Visible when access to location is denied -->  
<TextBlock x:Name="LocationDisabledMessage" FontStyle="Italic"
                 Visibility="Collapsed" Margin="0,15,0,0" TextWrapping="Wrap" >
          <Run Text="This app is not able to access Location. Go to " />
              <Hyperlink NavigateUri="ms-settings:privacy-location">
                  <Run Text="Settings" />
              </Hyperlink>
          <Run Text=" to check the location privacy settings."/>
</TextBlock>
```

Alternatively, your app can call the [**LaunchUriAsync**](https://msdn.microsoft.com/library/windows/apps/hh701476) method to launch the **Settings** app from code. For more info, see [Launch the Windows Settings app](https://msdn.microsoft.com/library/windows/apps/mt228342).

```csharp
using Windows.System;
...
bool result = await Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-location"));
```

## Troubleshoot your app


Before your app can access the user's location, **Location** must be enabled on the device. In the **Settings** app, check that the following **location privacy settings** are turned on:

-   **Location for this device...** is turned **on** (not applicable in Windows 10 Mobile)
-   The location services setting, **Location**, is turned **on**
-   Under **Choose apps that can use your location**, your app is set to **on**

## Related topics

* [UWP geolocation sample](http://go.microsoft.com/fwlink/p/?linkid=533278)
* [Design guidelines for geofencing](https://msdn.microsoft.com/library/windows/apps/dn631756)
* [Design guidelines for location-aware apps](https://msdn.microsoft.com/library/windows/apps/hh465148)
