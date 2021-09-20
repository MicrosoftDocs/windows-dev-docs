---
title: Declare background tasks in the application manifest
description: Enable the use of background tasks by declaring them as extensions in the app manifest.
ms.assetid: 6B4DD3F8-3C24-4692-9084-40999A37A200
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, background task
ms.localizationpriority: medium
---
# Declare background tasks in the application manifest




**Important APIs**

-   [**BackgroundTasks Schema**](/uwp/api/Windows.ApplicationModel.Background.IBackgroundTask)
-   [**Windows.ApplicationModel.Background**](/uwp/api/Windows.ApplicationModel.Background)

Enable the use of background tasks by declaring them as extensions in the app manifest.

> [!Important]
>  This article is specific to out-of-process background tasks. In-process background tasks are not declared in the manifest.

Out-of-process background tasks must be declared in the app manifest or else your app will not be able to register them (an exception will be thrown). Additionally, out-of-process background tasks must be declared in the application manifest to pass certification.

This topic assumes you have a created one or more background task classes, and that your app registers each background task to run in response to at least one trigger.

## Add Extensions Manually


Open the application manifest (Package.appxmanifest) and go to the Application element. Create an Extensions element (if one doesn't already exist).

The following snippet is taken from the [background task sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BackgroundTask):

```xml
<Application Id="App"
   ...
   <Extensions>
     <Extension Category="windows.backgroundTasks" EntryPoint="Tasks.SampleBackgroundTask">
       <BackgroundTasks>
         <Task Type="systemEvent" />
         <Task Type="timer" />
       </BackgroundTasks>
     </Extension>
     <Extension Category="windows.backgroundTasks" EntryPoint="Tasks.ServicingComplete">
       <BackgroundTasks>
         <Task Type="systemEvent"/>
       </BackgroundTasks>
     </Extension>
   </Extensions>
 </Application>
```

## Add a Background Task Extension  

Declare your first background task.

Copy this code into the Extensions element (you will add attributes in the following steps).

```xml
<Extensions>
    <Extension Category="windows.backgroundTasks" EntryPoint="">
      <BackgroundTasks>
        <Task Type="" />
      </BackgroundTasks>
    </Extension>
</Extensions>
```

1.  Change the EntryPoint attribute to have the same string used by your code as the entry point when registering your background task (**namespace.classname**).

    In this example, the entry point is ExampleBackgroundTaskNameSpace.ExampleBackgroundTaskClassName:

```xml
<Extensions>
    <Extension Category="windows.backgroundTasks" EntryPoint="Tasks.ExampleBackgroundTaskClassName">
       <BackgroundTasks>
         <Task Type="" />
       </BackgroundTasks>
    </Extension>
</Extensions>
```

2.  Change the list of Task Type attribute to indicate the type of task registration used with this background task. If the background task is registered with multiple trigger types, add additional Task elements and Type attributes for each one.

    **Note**  Make sure to list each of the trigger types you're using, or the background task will not register with the undeclared trigger types (the [**Register**](/uwp/api/windows.applicationmodel.background.backgroundtaskbuilder.register) method will fail and throw an exception).

    This snippet example indicates the use of system event triggers and push notifications:

```xml
<Extension Category="windows.backgroundTasks" EntryPoint="Tasks.BackgroundTaskClass">
    <BackgroundTasks>
        <Task Type="systemEvent" />
        <Task Type="pushNotification" />
    </BackgroundTasks>
</Extension>
```

### Add multiple background task extensions

Repeat step 2 for each additional background task class registered by your app.

The following example is the complete Application element from the [background task sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BackgroundTask). This shows the use of 2 background task classes with a total of 3 trigger types. Copy the Extensions section of this example, and modify it as needed, to declare background tasks in your application manifest.

```xml
<Applications>
    <Application Id="App"
      Executable="$targetnametoken$.exe"
      EntryPoint="BackgroundTask.App">
        <uap:VisualElements
          DisplayName="BackgroundTask"
          Square150x150Logo="Assets\StoreLogo-sdk.png"
          Square44x44Logo="Assets\SmallTile-sdk.png"
          Description="BackgroundTask"

          BackgroundColor="#00b2f0">
          <uap:LockScreen Notification="badgeAndTileText" BadgeLogo="Assets\smalltile-Windows-sdk.png" />
            <uap:SplashScreen Image="Assets\Splash-sdk.png" />
            <uap:DefaultTile DefaultSize="square150x150Logo" Wide310x150Logo="Assets\tile-sdk.png" >
                <uap:ShowNameOnTiles>
                    <uap:ShowOn Tile="square150x150Logo" />
                    <uap:ShowOn Tile="wide310x150Logo" />
                </uap:ShowNameOnTiles>
            </uap:DefaultTile>
        </uap:VisualElements>

      <Extensions>
        <Extension Category="windows.backgroundTasks" EntryPoint="Tasks.SampleBackgroundTask">
          <BackgroundTasks>
            <Task Type="systemEvent" />
            <Task Type="timer" />
          </BackgroundTasks>
        </Extension>
        <Extension Category="windows.backgroundTasks" EntryPoint="Tasks.ServicingComplete">
          <BackgroundTasks>
            <Task Type="systemEvent"/>
          </BackgroundTasks>
        </Extension>
      </Extensions>
    </Application>
</Applications>
```

## Declare where your background task will run

You can specify where your background tasks run:

* By default, they run in the BackgroundTaskHost.exe process.
* In the same process as your foreground application.
* Use `ResourceGroup` to place multiple background tasks into the same hosting process, or to separate them into different processes.
* Use `SupportsMultipleInstances` to run the background process in a new process that gets its own resource limits (memory, cpu) each time a new trigger is fired.

### Run in the same process as your foreground application

Here is example XML that declares a background task that runs in the same process as the foreground application.

```xml
<Extensions>
    <Extension Category="windows.backgroundTasks" EntryPoint="ExecModelTestBackgroundTasks.ApplicationTriggerTask">
        <BackgroundTasks>
            <Task Type="systemEvent" />
        </BackgroundTasks>
    </Extension>
</Extensions>
```

When you specify **EntryPoint**, your application receives a callback to the specified method when the trigger fires. If you do not specify an **EntryPoint**, your application receives the callback via  [OnBackgroundActivated()](/uwp/api/windows.ui.xaml.application.onbackgroundactivated).  See [Create and register an in-process background task](create-and-register-an-inproc-background-task.md) for details.

### Specify where your background task runs with the ResourceGroup attribute.

Here is example XML that declares a background task that runs in a BackgroundTaskHost.exe process, but in a separate one than other instances of background tasks from the same app. Note the `ResourceGroup` attribute, which identifies which background tasks will run together.

```xml
<Extensions>
    <Extension Category="windows.backgroundTasks" EntryPoint="BackgroundTasks.SessionConnectedTriggerTask" ResourceGroup="foo">
      <BackgroundTasks>
        <Task Type="systemEvent" />
      </BackgroundTasks>
    </Extension>
    <Extension Category="windows.backgroundTasks" EntryPoint="BackgroundTasks.TimeZoneTriggerTask" ResourceGroup="foo">
      <BackgroundTasks>
        <Task Type="systemEvent" />
      </BackgroundTasks>
    </Extension>
    <Extension Category="windows.backgroundTasks" EntryPoint="BackgroundTasks.TimerTriggerTask" ResourceGroup="bar">
      <BackgroundTasks>
        <Task Type="timer" />
      </BackgroundTasks>
    </Extension>
    <Extension Category="windows.backgroundTasks" EntryPoint="BackgroundTasks.ApplicationTriggerTask" ResourceGroup="bar">
      <BackgroundTasks>
        <Task Type="general" />
      </BackgroundTasks>
    </Extension>
    <Extension Category="windows.backgroundTasks" EntryPoint="BackgroundTasks.MaintenanceTriggerTask" ResourceGroup="foobar">
      <BackgroundTasks>
        <Task Type="general" />
      </BackgroundTasks>
    </Extension>
</Extensions>
```

### Run in a new process each time a trigger fires with the SupportsMultipleInstances attribute

This example declares a background task that runs in a new process that gets its own resource limits (memory and CPU) every time a new trigger is fired. Note the use of `SupportsMultipleInstances` which enables this behavior. In order to use this attribute you must target SDK version '10.0.15063' (Windows 10 Creators Update) or higher.

```xml
<Package
    xmlns:uap4="https://schemas.microsoft.com/appx/manifest/uap/windows10/4"
    ...
    <Applications>
        <Application ...>
            ...
            <Extensions>
                <Extension Category="windows.backgroundTasks" EntryPoint="BackgroundTasks.TimerTriggerTask">
                    <BackgroundTasks uap4:SupportsMultipleInstances="True">
                        <Task Type="timer" />
                    </BackgroundTasks>
                </Extension>
            </Extensions>
        </Application>
    </Applications>
```

> [!NOTE]
> You cannot specify `ResourceGroup` or `ServerName` in conjunction with `SupportsMultipleInstances`.

## Related topics

* [Debug a background task](debug-a-background-task.md)
* [Register a background task](register-a-background-task.md)
* [Guidelines for background tasks](guidelines-for-background-tasks.md)