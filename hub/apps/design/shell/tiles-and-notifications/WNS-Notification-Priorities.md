---
title: WNS Notification Priorities
description: Description of the various priorities that you can set on a notification
ms.date: 01/10/2017
ms.topic: article
keywords: windows 10, uwp, WinRT API, WNS
localizationpriority: medium
---
# WNS Notification Priorities
By setting a notification's priority with a simple header to WNS POST messages, you can control how notifications are delivered in battery sensitive situations.

## Power on Windows
As more users are working only on battery powered devices, minimizing power usage has become a standard requirement for all apps. If apps consume more energy than the value they provide, users might uninstall the apps. While the Windows operating system reduces power usage on the battery where possible, it is the app's responsibility to work efficiently. 

WNS priorities is one way to move non-critical work off the battery. The WNS priorities tell the system which notifications should be delivered instantly and which can wait until the device is plugged into a power source. With these hints, the system can deliver the notifications the exact time they are the most valuable to both the user and the app. 

## Power modes on the device
Every Windows device operates through a variety of power modes (battery, battery saver, and charge), and users expect different behaviors from apps in different power modes. When the device is on, all notifications should be delivered. In battery saver mode, only the most important notifications should be delivered. While the device is plugged in, sync or non-time critical operations can be completed.

Windows does not know which notifications are important to any user or app, so the system relies totally on apps to set the right priority for their notifications. 

## Priorities
There are four priorities available for an app to use when sending push notifications. The priority is set on individual notifications, allowing you to choose which notifications need to be delivered instantly (for example, an IM message) and which ones can wait (for example, contact photo updates).

The priorities are: 

|    Priority    |    User Override    |    Description    |    Example    |
|----------------|---------------------|-------------------|---------------|
|    High    |    Yes – user can block all notifications from an app   OR can prevent an app from being throttled in battery saver mode.    |    The most important notifications that must be delivered right away in any circumstance when the device can receive notifications. Things like VoIP calls or critical alerts that should wake the device fall into this category.    |    VoIP calls, time- critical alerts    |
|    Medium    |    Yes – user can block all notifications from an app   OR can prevent an app from being throttled in battery saver mode.    |    These are things that are not as important, things that don’t need to happen right away, but users would be annoyed if they are not running in the background.    |    Secondary Email account sync, live tile updates.    |
|    Low    |    Yes – user can block all notifications from an app   OR can prevent an app from being throttled in battery saver mode.    |    Notifications that only make sense when the user is using the device or when background activity makes sense. These are cached and not processed until the user signs in or plugs in their device.    |    Contact status (online/offline)    |

Note that many apps will have notifications of different priority throughout their lifecycle. Since the priority is set on a per-notification basis, this isn’t an issue. A VoIP app can send a high priority notification for an incoming call and then follow it up with a low priority one when a contact comes online. 

## Setting the priority

Setting the priority on the notification request is done through an additional header on the POST request, `X-WNS-PRIORITY`. This is an integer value between 1 and 4 which maps to a priority: 

| Priority Name | X-WNS-PRIORITY Value | Default for: |
|---------------|----------------------|------------------|
| High | 1 | Toasts |
| Medium | 2 | Tiles and Badges |
| Low | 3 | Raw |

To be backward compatible, setting a priority is not required. In case an app doesn’t set the priority of their notifications, the system will provide a default priority. The defaults are shown in the chart above and match the behavior of existing versions of Windows. 

## Detailed listing of desktop behavior 

If you are shipping your app across many different SKUs of Windows, it is normally best to follow the chart in the above section. 

More specific recommended behaviors for each priority are listed below. This is not a guarantee that each device will work exactly according to the chart. OEMs are free to configure the behavior differently, but most are close to this chart. 

| Device State    | PRIORITY: High    |    PRIORITY: Medium        | PRIORITY: Low    |    PRIORITY: Very Low    |
|-------------------------------------------------------|----------------------------------------------------|----------------------------------------------------|----------------------------------------------------|--------------------------|
|    Screen On OR plugged in    |    Deliver    |    Deliver    |    Deliver    |    Deliver    |
|    Screen Off AND on battery    |    Deliver    |    If user exempted: deliver        Else: cache     |    If user exempted: deliver        Else: cache *    |    Cache    |
|    Battery Saver enabled    |    If user exempted: deliver        Else: cache    |    If user exempted: deliver        Else: cache    |    If user exempted: deliver        Else: cache    |    Cache     |
|    On battery + battery saver enabled + screen off    |    If user exempted: deliver        Else: cache    |    If user exempted: deliver        Else: cache    |    If user exempted: deliver        Else: cache    |    Cache    |

Note that low priority notifications will be delivered by default for screen off and battery only for Windows Phone based devices. This is to maintian compatibility with preexisting MPNS policy. Also note that the fourth and fifth rows are the same, just calling out different scenarios.

To exempt an app in battery saver, users must go to the "Battery Usage by App" in Settings and select "Allow the app to run background tasks." This user selection exempts the app from battery saver for high, medium, and low priority notifications. You can also call [BackgroundExecutionManager API](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager.requestaccesskindasync#Windows_ApplicationModel_Background_BackgroundExecutionManager_RequestAccessKindAsync_Windows_ApplicationModel_Background_BackgroundAccessRequestKind_System_String_) to programmatically ask for the user's permission.  

## Related topics
- [Windows Push Notification Services (WNS) overview](windows-push-notification-services--wns--overview.md)
- [Requesting permission to run in the background](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager.requestaccesskindasync#Windows_ApplicationModel_Background_BackgroundExecutionManager_RequestAccessKindAsync_Windows_ApplicationModel_Background_BackgroundAccessRequestKind_System_String_)
