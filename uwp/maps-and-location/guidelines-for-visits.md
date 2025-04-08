---
description: Learn how to use the powerful Visits Tracking feature for more practical location tracking.
title: Guidelines for using Visits tracking
ms.assetid: 0c101684-48a9-4592-9ed5-6c20f3b830f2
ms.date: 06/21/2024
ms.topic: article
keywords: windows 10, uwp, map, location, geovisit, geovisits
ms.localizationpriority: medium
---
# Guidelines for using Visits tracking

The Visits feature streamlines the process of location tracking to make it more efficient for the practical purposes of many apps. A Visit is defined as a significant geographical area that the user enters and exits. Visits are similar to [geofences](guidelines-for-geofencing.md) in that they allow the app to be notified only when the user enters or exits certain areas of interest, eliminating the need for continual location tracking which can be a drain on battery life. However, unlike geofences, Visit areas are dynamically identified at the platform level and do not need to be defined explicitly by individual apps. Also, the selection of which Visits an app will track is handled by a single granularity setting, rather than by subscribing to individual places.

## Preliminary setup

Before going further, make sure your app is capable of accessing the device's location. You will need to declare the `Location` capability in the manifest and call the **[Geolocator.RequestAccessAsync](/uwp/api/Windows.Devices.Geolocation.Geolocator.RequestAccessAsync)** method to ensure that users give the app location permissions. See [Get the user's location](get-location.md) for more information on how to do this. 

Remember to add the `Geolocation` namespace to your class. This will be needed for all of the code snippets in this guide to work.

```csharp
using Windows.Devices.Geolocation;
```

## Check the latest Visit
The simplest way to use the Visits tracking feature is to retrieve the last known Visit-related state change. A state change is a platform-logged event in which either the user enters/exits a location of significance, there is significant movement since the last report, or the user's location is lost (see the **[VisitStateChange](/uwp/api/windows.devices.geolocation.visitstatechange)** enum). State changes are represented by **[Geovisit](/uwp/api/windows.devices.geolocation.geovisit)** instances. To retrieve the **Geovisit** instance for the last recorded state change, simply use the designated method in the **[GeovisitMonitor](/uwp/api/windows.devices.geolocation.geovisitmonitor)** class.

> [!NOTE]
> Checking the last logged Visit does not guarantee that Visits are currently being tracked by the system. In order to track Visits as they happen, you must either be monitoring them in the foreground or register for background tracking (see sections below).

```csharp
private async void GetLatestStateChange() {
    // retrieve the Geovisit instance
    Geovisit latestVisit = await GeovisitMonitor.GetLastReportAsync();

    // Using the properties of "latestVisit", parse out the time that the state 
    // change was recorded, the device's location when the change was recorded,
    // and the type of state change.
}
```

### Parse a Geovisit instance (optional)
The following method converts all of the information stored in a **Geovisit** instance into an easily readable string. It can be used in any of the scenarios in this guide to help provide feedback for the Visits being reported.

```csharp
private string ParseGeovisit(Geovisit visit){
    string visitString = null;

    // Use a DateTimeFormatter object to process the timestamp. The following
    // format configuration will give an intuitive representation of the date/time
    Windows.Globalization.DateTimeFormatting.DateTimeFormatter formatterLongTime;
    
    formatterLongTime = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter(
        "{hour.integer}:{minute.integer(2)}:{second.integer(2)}", new[] { "en-US" }, "US", 
        Windows.Globalization.CalendarIdentifiers.Gregorian, 
        Windows.Globalization.ClockIdentifiers.TwentyFourHour);
    
    // use this formatter to convert the timestamp to a string, and add to "visitString"
    visitString = formatterLongTime.Format(visit.Timestamp);

    // Next, add on the state change type value
    visitString += " " + visit.StateChange.ToString();

    // Next, add the position information (if any is provided; this will be null if 
    // the reported event was "TrackingLost")
    if (visit.Position != null) {
        visitString += " (" +
        visit.Position.Coordinate.Point.Position.Latitude.ToString() + "," +
        visit.Position.Coordinate.Point.Position.Longitude.ToString() + 
        ")";
    }

    return visitString;
}
```

## Monitor Visits in the foreground

The **GeovisitMonitor** class used in the previous section also handles the scenario of listening for state changes over a period of time. You can do this by instantiating this class, registering a handler method for its event, and calling the `Start` method.

```csharp
// this GeovisitMonitor instance will belong to the class scope
GeovisitMonitor monitor;

public void RegisterForVisits() {

    // Create and initialize a new monitor instance.
    monitor = new GeovisitMonitor();
    
    // Attach a handler to receive state change notifications.
    monitor.VisitStateChanged += OnVisitStateChanged;
    
    // Calling the start method will start Visits tracking for a specified scope:
    // For higher granularity such as venue/building level changes, choose Venue.
    // For lower granularity in the range of zipcode level changes, choose City.
    monitor.Start(VisitMonitoringScope.Venue);
}
```

In this example, the `OnVisitStateChanged` method will handle incoming Visit reports. The corresponding **Geovisit** instance is passed in through the event parameter.

```csharp
private void OnVisitStateChanged(GeoVisitWatcher sender, GeoVisitStateChangedEventArgs args) {
    Geovisit visit = args.Visit;
    
    // Using the properties of "visit", parse out the time that the state 
    // change was recorded, the device's location when the change was recorded,
    // and the type of state change.
}
```
When the app is finished monitoring for Visit-related state changes, it should stop the monitor and unregister the event handler(s). This should also be done whenever the app is suspended or closed.

```csharp
public void UnregisterFromVisits() {
    
    // Stop the monitor to stop tracking Visits. Otherwise, tracking will
    // continue until the monitor instance is destroyed.
    monitor.Stop();
    
    // Remove the handler to stop receiving state change events.
    monitor.VisitStateChanged -= OnVisitStateChanged;
}
```

## Monitor Visits in the background

You can also implement Visit monitoring in a background task, so that Visit-related activity can be handled on the device even when your app isn't open. This is the recommended method, as it is more versatile and energy-efficient. 

This guide will use the model in [Create and register an out-of-process background task](../launch-resume/create-and-register-a-background-task.md), in which the main application files live in one project and the background task file lives in a separate project in the same solution. If you are new to implementing background tasks, it is recommended that you follow that guidance primarily, making the necessary substitutions below to create a Visit-handling background task.

> [!NOTE]
> In the following snippets, some important functionality such as error handling and local storage is absent for the sake of simplicity. For a robust implementation of background Visits handling, see the [sample app](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Geolocation).


First, make sure your app has declared background task permissions. In the `Application/Extensions` element of your *Package.appxmanifest* file, add the following extension (add an `Extensions` element if one does not already exist).

```xml
<Extension Category="windows.backgroundTasks" EntryPoint="Tasks.VisitBackgroundTask">
    <BackgroundTasks>
        <Task Type="location" />
    </BackgroundTasks>
</Extension>
```

Next, in the definition of the background task class, paste in the following code. The `Run` method of this background task will simply pass the trigger details (which contain the Visits information) into a separate method.

```csharp
using Windows.ApplicationModel.Background;

namespace Tasks {
    
    public sealed class VisitBackgroundTask : IBackgroundTask {
        
        public void Run(IBackgroundTaskInstance taskInstance) {
            
            // get a deferral
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
            
            // this task's trigger will be a Geovisit trigger
            GeovisitTriggerDetails triggerDetails = taskInstance.TriggerDetails as GeovisitTriggerDetails;

            // Handle Visit reports
            GetVisitReports(triggerDetails);         

            finally {
                deferral.Complete();
            }
        }        
    }
}
```

Define the `GetVisitReports` method somewhere in this same class.

```csharp
private void GetVisitReports(GeovisitTriggerDetails triggerDetails) {

    // Read reports from the triggerDetails. This populates the "reports" variable 
    // with all of the Geovisit instances that have been logged since the previous
    // report reading.
    IReadOnlyList<Geovisit> reports = triggerDetails.ReadReports();

    foreach (Geovisit report in reports) {
        // Using the properties of "visit", parse out the time that the state 
        // change was recorded, the device's location when the change was recorded,
        // and the type of state change.
    }

    // Note: depending on the intent of the app, you many wish to store the
    // reports in the app's local storage so they can be retrieved the next time 
    // the app is opened in the foreground.
}
```

Next, in the main project of your app, you'll need to carry out the registration of this background task. Create a registering method that can be called by some user action or is called each time the class is activated.

```csharp
// a reference to this registration should be declared at the class level
private IBackgroundTaskRegistration visitTask = null;

// The app must call this method at some point to allow future use of 
// the background task. 
private async void RegisterBackgroundTask(object sender, RoutedEventArgs e) {
    
    string taskName = "MyVisitTask";
    string taskEntryPoint = "Tasks.VisitBackgroundTask";

    // First check whether the task in question is already registered
    foreach (var task in BackgroundTaskRegistration.AllTasks) {
        if (task.Value.Name == taskName) {
            // if a task is found with the name of this app's background task, then
            // return and do not attempt to register this task
            return;
        }
    }
    
    // Attempt to register the background task.
    try {
        // Get permission for a background task from the user. If the user has 
        // already responded once, this does nothing and the user must manually 
        // update their preference via Settings.
        BackgroundAccessStatus backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();

        switch (backgroundAccessStatus) {
            case BackgroundAccessStatus.AlwaysAllowed:
            case BackgroundAccessStatus.AllowedSubjectToSystemPolicy:
                // BackgroundTask is allowed
                break;

            default:
                // notify user that background tasks are disabled for this app
                //...
                break;
        }

        // Create a new background task builder
        BackgroundTaskBuilder visitTaskBuilder = new BackgroundTaskBuilder();

        visitTaskBuilder.Name = exampleTaskName;
        visitTaskBuilder.TaskEntryPoint = taskEntryPoint;

        // Create a new Visit trigger
        var trigger = new GeovisitTrigger();

        // Set the desired monitoring scope.
        // For higher granularity such as venue/building level changes, choose Venue.
        // For lower granularity in the range of zipcode level changes, choose City. 
        trigger.MonitoringScope = VisitMonitoringScope.Venue; 

        // Associate the trigger with the background task builder
        visitTaskBuilder.SetTrigger(trigger);

        // Register the background task
        visitTask = visitTaskBuilder.Register();      
    }
    catch (Exception ex) {
        // notify user that the task failed to register, using ex.ToString()
    }
}
```

This establishes that a background task class called `VisitBackgroundTask` in the namespace `Tasks` will do something with the `location` trigger type. 

Your app should now be capable of registering the Visits-handling background task, and this task should be activated whenever the device logs a Visit-related state change. You will need to fill in the logic in your background task class to determine what to do with this state change information.

## Related topics
* [Create and register an out-of-process background task](../launch-resume/create-and-register-a-background-task.md)
* [Get the user's location](get-location.md)
* [Windows.Devices.Geolocation namespace](/uwp/api/windows.devices.geolocation)
