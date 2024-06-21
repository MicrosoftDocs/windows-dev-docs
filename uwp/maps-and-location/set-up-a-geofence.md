---
title: Set up a geofence
description: Set up a Geofence in your app, and learn how to handle notifications in the foreground and background.
ms.assetid: A3A46E03-0751-4DBD-A2A1-2323DB09BDBA
ms.date: 06/21/2024
ms.topic: article
keywords: windows 10, uwp, map, location, geofence, notifications
ms.localizationpriority: medium
---
# Set up a geofence

Set up a [**Geofence**](/uwp/api/Windows.Devices.Geolocation.Geofencing.Geofence) in your app, and learn how to handle notifications in the foreground and background.

## Enable the location capability

1.  In **Solution Explorer**, double-click on **package.appxmanifest** and select the **Capabilities** tab.
2.  In the **Capabilities** list, check **Location**. This adds the `Location` device capability to the package manifest file.

```xml
  <Capabilities>
    <!-- DeviceCapability elements must follow Capability elements (if present) -->
    <DeviceCapability Name="location"/>
  </Capabilities>
```

## Geofence set up

### Step 1: Request access to the user's location

> [!IMPORTANT]
> You must request access to the user's location by using the [**RequestAccessAsync**](/uwp/api/windows.devices.geolocation.geolocator.requestaccessasync) method before attempting to access the user's location. You must call the **RequestAccessAsync** method from the UI thread and your app must be in the foreground. Your app will not be able to access the user's location information until after the user grants permission to your app.

```csharp
using Windows.Devices.Geolocation;
...
var accessStatus = await Geolocator.RequestAccessAsync();
```

The [**RequestAccessAsync**](/uwp/api/windows.devices.geolocation.geolocator.requestaccessasync) method prompts the user for permission to access their location. The user is only prompted once (per app). After the first time they grant or deny permission, this method no longer prompts the user for permission. To help the user change location permissions after they've been prompted, we recommend that you provide a link to the location settings as demonstrated later in this topic.

### Step 2: Register for changes in geofence state and location permissions

In this example, a **switch** statement is used with **accessStatus** (from the previous example) to act only when access to the user's location is allowed. If access to the user's location is allowed, the code accesses the current geofences, registers for geofence state changes, and registers for changes in location permissions.

**Tip** When using a geofence, monitor changes in location permissions using the [**StatusChanged**](/uwp/api/windows.devices.geolocation.geofencing.geofencemonitor.statuschanged) event from the GeofenceMonitor class instead of the StatusChanged event from the Geolocator class. A [**GeofenceMonitorStatus**](/uwp/api/Windows.Devices.Geolocation.Geofencing.GeofenceMonitorStatus) of **Disabled** is equivalent to a disabled [**PositionStatus**](/uwp/api/Windows.Devices.Geolocation.PositionStatus) - both indicate that the app does not have permission to access the user's location.

```csharp
switch (accessStatus)
{
    case GeolocationAccessStatus.Allowed:
        geofences = GeofenceMonitor.Current.Geofences;

        FillRegisteredGeofenceListBoxWithExistingGeofences();
        FillEventListBoxWithExistingEvents();

        // Register for state change events.
        GeofenceMonitor.Current.GeofenceStateChanged += OnGeofenceStateChanged;
        GeofenceMonitor.Current.StatusChanged += OnGeofenceStatusChanged;
        break;


    case GeolocationAccessStatus.Denied:
        _rootPage.NotifyUser("Access denied.", NotifyType.ErrorMessage);
        break;

    case GeolocationAccessStatus.Unspecified:
        _rootPage.NotifyUser("Unspecified error.", NotifyType.ErrorMessage);
        break;
}
```

Then, when navigating away from your foreground app, unregister the event listeners.

```csharp
protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
{
    GeofenceMonitor.Current.GeofenceStateChanged -= OnGeofenceStateChanged;
    GeofenceMonitor.Current.StatusChanged -= OnGeofenceStatusChanged;

    base.OnNavigatingFrom(e);
}
```

### Step 3: Create the geofence

Now, you are ready to define and set up a [**Geofence**](/uwp/api/Windows.Devices.Geolocation.Geofencing.Geofence) object. There are several different constructor overloads to choose from, depending on your needs. In the most basic geofence constructor, specify only the [**Id**](/uwp/api/windows.devices.geolocation.geofencing.geofence.id) and the [**Geoshape**](/uwp/api/windows.devices.geolocation.geofencing.geofence.geoshape) as shown here.

```csharp
// Set the fence ID.
string fenceId = "fence1";

// Define the fence location and radius.
BasicGeoposition position;
position.Latitude = 47.6510;
position.Longitude = -122.3473;
position.Altitude = 0.0;
double radius = 10; // in meters

// Set a circular region for the geofence.
Geocircle geocircle = new Geocircle(position, radius);

// Create the geofence.
Geofence geofence = new Geofence(fenceId, geocircle);

```

You can fine-tune your geofence further by using one of the other constructors. In the next example, the geofence constructor specifies these additional parameters:

-   [**MonitoredStates**](/uwp/api/windows.devices.geolocation.geofencing.geofence.monitoredstates) - Indicates what geofence events you want to receive notifications for entering the defined region, leaving the defined region, or removal of the geofence.
-   [**SingleUse**](/uwp/api/windows.devices.geolocation.geofencing.geofence.singleuse) - Removes the geofence once all the states the geofence is being monitored for have been met.
-   [**DwellTime**](/uwp/api/windows.devices.geolocation.geofencing.geofence.dwelltime) - Indicates how long the user must be in or out of the defined area before the enter/exit events are triggered.
-   [**StartTime**](/uwp/api/windows.devices.geolocation.geofencing.geofence.starttime) - Indicates when to start monitoring the geofence.
-   [**Duration**](/uwp/api/windows.devices.geolocation.geofencing.geofence.duration) - Indicates the period for which to monitor the geofence.

```csharp
// Set the fence ID.
string fenceId = "fence2";

// Define the fence location and radius.
BasicGeoposition position;
position.Latitude = 47.6510;
position.Longitude = -122.3473;
position.Altitude = 0.0;
double radius = 10; // in meters

// Set the circular region for geofence.
Geocircle geocircle = new Geocircle(position, radius);

// Remove the geofence after the first trigger.
bool singleUse = true;

// Set the monitored states.
MonitoredGeofenceStates monitoredStates =
                MonitoredGeofenceStates.Entered |
                MonitoredGeofenceStates.Exited |
                MonitoredGeofenceStates.Removed;

// Set how long you need to be in geofence for the enter event to fire.
TimeSpan dwellTime = TimeSpan.FromMinutes(5);

// Set how long the geofence should be active.
TimeSpan duration = TimeSpan.FromDays(1);

// Set up the start time of the geofence.
DateTimeOffset startTime = DateTime.Now;

// Create the geofence.
Geofence geofence = new Geofence(fenceId, geocircle, monitoredStates, singleUse, dwellTime, startTime, duration);
```

After creating, remember to register your new [**Geofence**](/uwp/api/Windows.Devices.Geolocation.Geofencing.Geofence) to the monitor.

```csharp
// Register the geofence
try {
   GeofenceMonitor.Current.Geofences.Add(geofence);
} catch {
   // Handle failure to add geofence
}
```

### Step 4: Handle changes in location permissions

The [**GeofenceMonitor**](/uwp/api/Windows.Devices.Geolocation.Geofencing.GeofenceMonitor) object triggers the [**StatusChanged**](/uwp/api/windows.devices.geolocation.geofencing.geofencemonitor.statuschanged) event to indicate that the user's location settings changed. That event passes the corresponding status via the argument's **sender.Status** property (of type [**GeofenceMonitorStatus**](/uwp/api/Windows.Devices.Geolocation.Geofencing.GeofenceMonitorStatus)). Note that this method is not called from the UI thread and the [**Dispatcher**](/uwp/api/Windows.UI.Core.CoreDispatcher) object invokes the UI changes.

```csharp
using Windows.UI.Core;
...
public async void OnGeofenceStatusChanged(GeofenceMonitor sender, object e)
{
   await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
   {
    // Show the location setting message only if the status is disabled.
    LocationDisabledMessage.Visibility = Visibility.Collapsed;

    switch (sender.Status)
    {
     case GeofenceMonitorStatus.Ready:
      _rootPage.NotifyUser("The monitor is ready and active.", NotifyType.StatusMessage);
      break;

     case GeofenceMonitorStatus.Initializing:
      _rootPage.NotifyUser("The monitor is in the process of initializing.", NotifyType.StatusMessage);
      break;

     case GeofenceMonitorStatus.NoData:
      _rootPage.NotifyUser("There is no data on the status of the monitor.", NotifyType.ErrorMessage);
      break;

     case GeofenceMonitorStatus.Disabled:
      _rootPage.NotifyUser("Access to location is denied.", NotifyType.ErrorMessage);

      // Show the message to the user to go to the location settings.
      LocationDisabledMessage.Visibility = Visibility.Visible;
      break;

     case GeofenceMonitorStatus.NotInitialized:
      _rootPage.NotifyUser("The geofence monitor has not been initialized.", NotifyType.StatusMessage);
      break;

     case GeofenceMonitorStatus.NotAvailable:
      _rootPage.NotifyUser("The geofence monitor is not available.", NotifyType.ErrorMessage);
      break;

     default:
      ScenarioOutput_Status.Text = "Unknown";
      _rootPage.NotifyUser(string.Empty, NotifyType.StatusMessage);
      break;
    }
   });
}
```

## Set up foreground notifications

After your geofences are created, you must add the logic to handle what happens when a geofence event occurs. Depending on the [**MonitoredStates**](/uwp/api/windows.devices.geolocation.geofencing.geofence.monitoredstates) that you have set up, you may receive an event when:

-   The user enters a region of interest.
-   The user leaves a region of interest.
-   The geofence has expired or been removed. Note that a background app is not activated for a removal event.

You can listen for events directly from your app when it is running or register for a background task so that you receive a background notification when an event occurs.

### Step 1: Register for geofence state change events

For your app to receive a foreground notification of a geofence state change, you must register an event handler. This is typically set up when you create the geofence.

```csharp
private void Initialize()
{
    // Other initialization logic

    GeofenceMonitor.Current.GeofenceStateChanged += OnGeofenceStateChanged;
}

```

### Step 2: Implement the geofence event handler

The next step is to implement the event handlers. The action taken here depends on what your app is using the geofence for.

```csharp
public async void OnGeofenceStateChanged(GeofenceMonitor sender, object e)
{
    var reports = sender.ReadReports();

    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
    {
        foreach (GeofenceStateChangeReport report in reports)
        {
            GeofenceState state = report.NewState;

            Geofence geofence = report.Geofence;

            if (state == GeofenceState.Removed)
            {
                // Remove the geofence from the geofences collection.
                GeofenceMonitor.Current.Geofences.Remove(geofence);
            }
            else if (state == GeofenceState.Entered)
            {
                // Your app takes action based on the entered event.

                // NOTE: You might want to write your app to take a particular
                // action based on whether the app has internet connectivity.

            }
            else if (state == GeofenceState.Exited)
            {
                // Your app takes action based on the exited event.

                // NOTE: You might want to write your app to take a particular
                // action based on whether the app has internet connectivity.

            }
        }
    });
}
```

## Set up background notifications

After your geofences are created, you must add the logic to handle what happens when a geofence event occurs. Depending on the [**MonitoredStates**](/uwp/api/windows.devices.geolocation.geofencing.geofence.monitoredstates) that you have set up, you may receive an event when:

-   The user enters a region of interest.
-   The user leaves a region of interest.
-   The geofence has expired or been removed. Note that a background app is not activated for a removal event.

To listen for a geofence event in the background

-   Declare the background task in your app’s manifest.
-   Register the background task in your app. If your app needs internet access, say for accessing a cloud service, you can set a flag for that when the event is triggered. You can also set a flag to make sure that the user is present when the event is triggered so that you are sure that the user gets notified.
-   While your app is running in the foreground, prompt the user to grant your app location permissions.

### Step 1: Register for geofence state changes

In your app's manifest, under the **Declarations** tab, add a declaration for a location background task. To do this:

-   Add a declaration of type **Background Tasks**.
-   Set a property task type of **Location**.
-   Set an entry point into your app to call when the event is triggered.

### Step 2: Register the background task

The code in this step registers the geofencing background task. Recall that when the geofence was created, we checked for location permissions.

```csharp
async private void RegisterBackgroundTask(object sender, RoutedEventArgs e)
{
    // Get permission for a background task from the user. If the user has already answered once,
    // this does nothing and the user must manually update their preference via PC Settings.
    BackgroundAccessStatus backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();

    // Regardless of the answer, register the background task. Note that the user can use
    // the Settings app to prevent your app from running background tasks.
    // Create a new background task builder.
    BackgroundTaskBuilder geofenceTaskBuilder = new BackgroundTaskBuilder();

    geofenceTaskBuilder.Name = SampleBackgroundTaskName;
    geofenceTaskBuilder.TaskEntryPoint = SampleBackgroundTaskEntryPoint;

    // Create a new location trigger.
    var trigger = new LocationTrigger(LocationTriggerType.Geofence);

    // Associate the location trigger with the background task builder.
    geofenceTaskBuilder.SetTrigger(trigger);

    // If it is important that there is user presence and/or
    // internet connection when OnCompleted is called
    // the following could be called before calling Register().
    // SystemCondition condition = new SystemCondition(SystemConditionType.UserPresent | SystemConditionType.InternetAvailable);
    // geofenceTaskBuilder.AddCondition(condition);

    // Register the background task.
    geofenceTask = geofenceTaskBuilder.Register();

    // Associate an event handler with the new background task.
    geofenceTask.Completed += new BackgroundTaskCompletedEventHandler(OnCompleted);

    BackgroundTaskState.RegisterBackgroundTask(BackgroundTaskState.LocationTriggerBackgroundTaskName);

    switch (backgroundAccessStatus)
    {
    case BackgroundAccessStatus.Unspecified:
    case BackgroundAccessStatus.Denied:
        rootPage.NotifyUser("This app is not allowed to run in the background.", NotifyType.ErrorMessage);
        break;

    }
}


```

### Step 3: Handling the background notification

The action that you take to notify the user depends on what your app does, but you can display a toast notification, play an audio sound, or update a live tile. The code in this step handles the notification.

```csharp
async private void OnCompleted(IBackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs e)
{
    if (sender != null)
    {
        // Update the UI with progress reported by the background task.
        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        {
            try
            {
                // If the background task threw an exception, display the exception in
                // the error text box.
                e.CheckResult();

                // Update the UI with the completion status of the background task.
                // The Run method of the background task sets the LocalSettings.
                var settings = ApplicationData.Current.LocalSettings;

                // Get the status.
                if (settings.Values.ContainsKey("Status"))
                {
                    rootPage.NotifyUser(settings.Values["Status"].ToString(), NotifyType.StatusMessage);
                }

                // Do your app work here.

            }
            catch (Exception ex)
            {
                // The background task had an error.
                rootPage.NotifyUser(ex.ToString(), NotifyType.ErrorMessage);
            }
        });
    }
}


```

## Change the privacy settings

If the location privacy settings don't allow your app to access the user's location, we recommend that you provide a convenient link to the **location privacy settings** in the **Settings** app. In this example, a Hyperlink control is used navigate to the `ms-settings:privacy-location` URI.

```xaml
<!--Set Visibility to Visible when access to the user's location is denied. -->  
<TextBlock x:Name="LocationDisabledMessage" FontStyle="Italic"
                 Visibility="Collapsed" Margin="0,15,0,0" TextWrapping="Wrap" >
          <Run Text="This app is not able to access Location. Go to " />
              <Hyperlink NavigateUri="ms-settings:privacy-location">
                  <Run Text="Settings" />
              </Hyperlink>
          <Run Text=" to check the location privacy settings."/>
</TextBlock>
```

Alternatively, your app can call the [**LaunchUriAsync**](/uwp/api/windows.system.launcher.launchuriasync) method to launch the **Settings** app from code. For more info, see [Launch the Windows Settings app](../launch-resume/launch-settings-app.md).

```csharp
using Windows.System;
...
bool result = await Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-location"));
```

## Test and debug your app

Testing and debugging geofencing apps can be a challenge because they depend on a device's location. Here, we outline several methods for testing both foreground and background geofences.

**To debug a geofencing app**

1.  Physically move the device to new locations.
2.  Test entering a geofence by creating a geofence region that includes your current physical location, so you're already inside the geofence and the "geofence entered" event is triggered immediately.
3.  Use the Microsoft Visual Studio emulator to simulate locations for the device.

### Test and debug a geofencing app that is running in the foreground

**To test your geofencing app that is running in the foreground**

1.  Build your app in Visual Studio.
2.  Launch your app in the Visual Studio emulator.
3.  Use these tools to simulate various locations inside and outside of your geofence region. Be sure to wait long enough past the time specified by the [**DwellTime**](/uwp/api/windows.devices.geolocation.geofencing.geofence.dwelltime) property to trigger the event. Note that you must accept the prompt to enable location permissions for the app. For more info about simulating locations, see [Set the simulated geolocation of the device](/previous-versions/hh441475(v=vs.120)#BKMK_Set_the_simulated_geo_location_of_the_device).
4.  You can also use the emulator to estimate the size of fences and dwell times approximately needed to be detected at different speeds.

### Test and debug a geofencing app that is running in the background

**To test your geofencing app that is running the background**

1.  Build your app in Visual Studio. Note that your app should set the **Location** background task type.
2.  Deploy the app locally first.
3.  Close your app that is running locally.
4.  Launch your app in the Visual Studio emulator. Note that background geofencing simulation is supported on only one app at a time within the emulator. Do not launch multiple geofencing apps within the emulator.
5.  From the emulator, simulate various locations inside and outside of your geofence region. Be sure to wait long enough past the [**DwellTime**](/uwp/api/windows.devices.geolocation.geofencing.geofence.dwelltime) to trigger the event. Note that you must accept the prompt to enable location permissions for the app.
6.  Use Visual Studio to trigger the location background task. For more info about triggering background tasks in Visual Studio, see [How to trigger background tasks](/previous-versions/hh974425(v=vs.120)#BKMK_Trigger_background_tasks).

## Troubleshoot your app

Before your app can access location, **Location** must be enabled on the device. In the **Settings** app, check that the following **location privacy settings** are turned on:

-   **Location for this device...** is turned **on** (not applicable in Windows 10 Mobile)
-   The location services setting, **Location**, is turned **on**
-   Under **Choose apps that can use your location**, your app is set to **on**

## Related topics

* [UWP geolocation sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Geolocation)
* [Design guidelines for geofencing](./guidelines-for-geofencing.md)
* [Design guidelines for location-aware apps](./guidelines-and-checklist-for-detecting-location.md)
