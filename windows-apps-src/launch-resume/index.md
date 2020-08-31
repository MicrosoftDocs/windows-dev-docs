---
title: Launching, resuming, and background tasks
description: This section describes what happens when a Universal Windows Platform (UWP) app is started, suspended, resumed, and terminated.
ms.assetid: 75011D52-1511-4ECF-9DF6-52CBBDB15BD7
ms.date: 10/04/2017
ms.topic: article
keywords: windows 10, uwp, background task, app service, connected devices, remote systems
ms.localizationpriority: medium
---
# Launching, resuming, and background tasks


This section includes information on the following:

- What happens when a Universal Windows Platform (UWP) app is started, suspended, resumed, and terminated.
- How to launch apps by using a URI or by file activation.
- How to use App services, which allow your Universal Windows Platform (UWP) app to share data and functionality with other apps.
- How to use background tasks, which allow a UWP app to do work while the app itself is not in the foreground.
- How to discover connected devices, launch an app on another device, and communicate with an app service on a remote device so that you can create user experiences that flow across devices.
- How to choose the right technology to extend and componentize your app.
- How to add and configure a splash screen for your app.
- How to write extend your app via packages from that users can install from the Microsoft Store.

## The app lifecycle

This section details the lifecycle of a Windows 10 Universal Windows Platform (UWP) app, from the time it is activated until it is closed.

| Topic | Description |
|-------|-------------|
| [App lifecycle](app-lifecycle.md)               | Learn about the life cycle of a UWP app and what happens when Windows launches, suspends, and resumes your app. |
| [Handle app prelaunch](handle-app-prelaunch.md) | Learn how to handle app prelaunch.                                                                              |
| [Handle app activation](activate-an-app.md)     | Learn how to handle app activation.                                                                             |
| [Handle app suspend](suspend-an-app.md)         | Learn how to save important application data when the system suspends your app.                                 |
| [Handle app resume](resume-an-app.md)           | Learn how to refresh displayed content when the system resumes your app.                                        |
| [Free memory when your app moves to the background](reduce-memory-usage.md) | Learn how to reduce the amount of memory that your app uses when it is in the background state so that it won't be terminated.|
| [Postpone app suspension with extended execution](run-minimized-with-extended-execution.md) | Learn how to use extended execution to keep your app running when it is minimized |

## Launch apps

| Topic | Description |
|-------|-------------|
| [Create a Universal Windows Platform console app](console-uwp.md) | Learn how to write a Universal Windows Platform app that runs in a console window. |
| [Create a Multi-instance UWP app](multi-instance-uwp.md) | Learn how to write a multi-instance Universal Windows Platform app. |

The [Launch an app with a URI](launch-app-with-uri.md) section details how to use a Uniform Resource Identifier (URI) to launch an app.

| Topic | Description |
|-------|-------------|
| [Launch the default app for a URI](launch-default-app.md) | Learn how to launch the default app for a Uniform Resource Identifier (URI). URIs allow you to launch another app to perform a specific task. This topic also provides an overview of the many URI schemes built into Windows. |
| [Handle URI activation](handle-uri-activation.md) | Learn how to register an app to become the default handler for a Uniform Resource Identifier (URI) scheme name. |
| [Launch an app for results](how-to-launch-an-app-for-results.md) | Learn how to launch an app from another app and exchange data between the two. This is called launching an app for results. |
| [Choose and save tones using the ms-tonepicker URI scheme](launch-ringtone-picker.md) | This topic describes the ms-tonepicker URI scheme and how to use it to display a tone picker to select a tone, save a tone, and get the friendly name for a tone. |
| [Launch the Windows Settings app](launch-settings-app.md) | Learn how to launch the Windows Settings app from your app. This topic describes the ms-settings URI scheme. Use this URI scheme to launch the Windows Settings app to specific settings pages. |
| [Launch the Microsoft Store app](launch-store-app.md) | This topic describes the ms-windows-store URI scheme. Your app can use this URI scheme to launch the UWP app to specific pages in the Store. |
| [Launch the Windows Maps app](launch-maps-app.md) | Learn how to launch the Windows Maps app from your app. |
| [Launch the People app](launch-people-apps.md) | This topic describes the ms-people URI scheme. Your app can use this URI scheme to launch the People app for specific actions. |
| [Support web-to-app linking with app URI handlers](web-to-app-linking.md) | Drive user engagement with your app by using app URI handlers. |

The [Launch an app through file activation](launch-app-from-file.md) section details how to set up your app to launch when a file of a certain type is opened.

| Topic | Description |
|-------|-------------|
| [Launch the default app for a file](launch-the-default-app-for-a-file.md) | Learn how to launch the default app for a file. |
| [Handle file activation](handle-file-activation.md) | Learn how to register your app to become the default handler for a certain file type. |

See other topics related to launching an app below.

| Topic | Description |
|-------|-------------|
| [Continue user activity, even across devices](useractivities.md) | Reengage users with your app, even across devices, by launching your app where the user left off. |
| [Auto-launching with AutoPlay](auto-launching-with-autoplay.md) | You can use AutoPlay to provide your app as an option when a user connects a device to their PC. This includes non-volume devices such as a camera or media player, or volume devices such as a USB thumb drive, SD card, or DVD. |
| [Reserved file and URI scheme names](reserved-uri-scheme-names.md) | This topic lists the reserved file and URI scheme names that are not available to your app. |

## App services and extensions

The [App services and extensions](app-services.md) section describes how to integrate app services into your UWP app to allow the sharing of data and functionality across apps.

| Topic | Description |
|-------|-------------|
| [Create and consume an app service](how-to-create-and-consume-an-app-service.md) | Learn how to write a Universal Windows Platform (UWP) app that can provide services to other UWP apps and how to consume those services. |
| [Convert an app service to run in the same process as its host app](convert-app-service-in-process.md) | Convert app service code that ran in a separate background process into code that runs inside the same process as your app service provider. |
| [Extend your app with app services, extensions, and packages](extend-your-app-with-services-extensions-packages.md) | Determine which technology to use to extend and componentize your app and get a brief overview of each. |
| [Create and consume an app extension](how-to-create-an-extension.md) | Write and host Universal Windows Platform (UWP) app extensions to extend your app via packages that users can install from the Microsoft Store. |

## Background tasks

The [Background tasks](support-your-app-with-background-tasks.md) section shows you how to make lightweight code run in the background in response to triggers.

| Topic | Description |
|-------|-------------|
| [Guidelines for background tasks](guidelines-for-background-tasks.md)                                       | Ensure your app meets the requirements for running background tasks. |
| [Access sensors and devices from a background task](access-sensors-and-devices-from-a-background-task.md)   | [**DeviceUseTrigger**](/uwp/api/Windows.ApplicationModel.Background.DeviceUseTrigger) lets your Universal Windows app access sensors and peripheral devices in the background, even when your foreground app is suspended. |
| [Create and register an in-process background task](create-and-register-an-inproc-background-task.md)       | Create and register a background task that runs in the same process as your foreground app. |
| [Create and register an out-of-process background task](create-and-register-a-background-task.md)           | Create and register a background task that that runs in a separate process from your app, and register it to run when your app is not in the foreground. |
| [Create and register a COM background task for a winmain app](create-and-register-a-winmain-background-task.md) | Create a COM background task that can run in your main process or out-of-process when your packaged winmain app may not be running. |
| [Port an out-of-process background task to an in-process background task](convert-out-of-process-background-task.md) | Learn how to port an out-of-process background task to an in-process background task that runs in the same process as your foreground app.|
| [Debug a background task](debug-a-background-task.md)                                                       | Learn how to debug a background task, including background task activation and debug tracing in the Windows event log. |
| [Declare background tasks in the application manifest](declare-background-tasks-in-the-application-manifest.md) | Enable the use of background tasks by declaring them as extensions in the app manifest. |
| [Group background task registration](group-background-tasks.md)                                             | Isolate background task registration with groups. |
| [Handle a cancelled background task](handle-a-cancelled-background-task.md)                                 | Learn how to make a background task that recognizes cancellation requests and stops work, reporting the cancellation to the app using persistent storage. |
| [Monitor background task progress and completion](monitor-background-task-progress-and-completion.md)       | Learn how your app can recognize background task progress and completion. |
| [Optimize background activity](../debug-test-perf/optimize-background-activity.md) |Learn how to reduce the energy used in the background and interact with user settings for background activity. |
| [Register a background task](register-a-background-task.md)                                                 | Learn how to create a function that can be re-used to safely register most background tasks. |
| [Respond to system events with background tasks](respond-to-system-events-with-background-tasks.md)         | Learn how to create a background task that responds to [**SystemTrigger**](/uwp/api/Windows.ApplicationModel.Background.SystemTriggerType) events. |
| [Run a background task on a timer](run-a-background-task-on-a-timer-.md)                                    | Learn how to schedule a one-time background task, or run a periodic background task. |
| [Run in the background indefinitely](run-in-the-background-indefinetly.md)                                    | Use a capability to run a background task or extended execution session in the background indefinitely. |
| [Trigger a background task from within your app](trigger-background-task-from-app.md) | Learn how to use the [ApplicationTrigger](/uwp/api/Windows.ApplicationModel.Background.ApplicationTrigger) to activate a background task from within your app.|
| [Set conditions for running a background task](set-conditions-for-running-a-background-task.md)             | Learn how to set conditions that control when your background task will run. |
| [Transfer data in the background](../networking/background-transfers.md)                 | Use the background transfer API to copy files in the background. |
| [Update a live tile from a background task](update-a-live-tile-from-a-background-task.md)                   | Use a background task to update your app's live tile with fresh content. |
| [Use a maintenance trigger](use-a-maintenance-trigger.md)                                                   | Learn how to use the [**MaintenanceTrigger**](/uwp/api/Windows.ApplicationModel.Background.MaintenanceTrigger) class to run lightweight code in the background while the device is plugged in. |

## Remote Systems

The [Connected apps and devices (Project Rome)](connected-apps-and-devices.md) section describes how to use the Remote Systems platform to discover remote devices, launch an app on a remote device, and communicate with an app service on a remote device.

| Topic | Description |
|-------|-------------|
| [Discover remote devices](discover-remote-devices.md)  | Learn how to discover devices that you can connect to. |
| [Launch an app on a remote device](launch-a-remote-app.md) | Learn how to launch an app on a remote device.  |
| [Communicate with a remote app service](communicate-with-a-remote-app-service.md) | Learn how to interact with an app on a remote device. |
| [Connect devices through remote sessions](remote-sessions.md) | Create shared experiences across multiple devices by joining them in a remote session. |

## Splash screens

The [Splash screens](splash-screens.md) section describes how to set and configure your app's splash screen.

| Topic | Description |
|-------|-------------|
| [Add a splash screen](add-a-splash-screen.md) | Set your app's splash screen image and background color. |
| [Display a splash screen for more time](create-a-customized-splash-screen.md) | Display a splash screen for more time by creating an extended splash screen for your app. This extended screen imitates the splash screen shown when your app is launched, and can be customized. |