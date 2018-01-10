---
title: WNS Notification Priorities
description: Description of the various priorities that you can set on a notification
author: Adam Wilson
ms.author: adwilso
ms.date: 1/10/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, WinRT API, WNS
localizationpriority: medium
---

# WNS Notification Priorities
## Power on Windows
As more users are now working only on battery powered devices, it is now important for all developers on Windows to know the limits of these machines. Minimizing power usage has become a standard requirement for all apps because users will uninstall apps they feel are consuming more energy than the value they are providing. 

The operating system will be able to help apps by reducing power usage on the battery where possible, but the final responsibility is up to you as the developer to make sure your app works efficiently. One of the ways the operating system can help you move stop non-critical work off the battery is through WNS priorities. 

The WNS power modes a hint to the system about when a notification should be delivered. It tells the system which notifications should be delivered instantly and which others can wait until the device is plugged in. With these hints, the system can deliver the notifications the exact time they are the most valuable to both the user and the app. 

## Power Modes on the Device

Every Windows device operates through a variety of power modes throughout the day. For example, let’s walk through a day with Janet – a salesperson. Her device starts the day plugged in after charging overnight, and then she goes to customer meetings where it runs on battery for a few hours. Since she is away from the office but still has some work to do, she walks to a cafe to work for the afternoon. After few hours working there, her battery will start running low. This is where she has a chance to pull out the big hammer – enabling Battery Saver. Finally, once all the work for the day has been completed, she puts the device to sleep and heads back home to charge it again. 


During all of these switches, Janet will be expecting a different behavior from the apps. When the device is on in a meeting, she expects to get all her notifications - such as IMs and new emails. During battery saver at the end of the day, she only wants to receive the most important IM notifications to save her precious battery power. And finally, while it is plugged in overnight she wants sync or non-time critical operations to be completed to make sure the device is ready to go for the next day. 


This is where we need the help of all the developers working on Windows apps. Windows does not have known how to choose what notifications are important to any user or app. It relies totally on developers setting the right priority for their notifications. 

## Priorities
In the Fall Creators Update, there are 4 priorities available for an app to use when sending push notifications. Each one sends a hint to the system on how it should manage notifications. The priority is to set on each individual notification, giving you control over exactly which notifications will be delivered. This allows you to choose which notifications needed always to be delivered instantly (e.g., An IM message) and which ones can wait (e.g., contact photo updates)

The priorities are: 

|    Priority    |    User Override    |    Description    |    Example    |
|----------------|---------------------|-------------------|---------------|
|    High    |    Yes – user can block all notifications from an app   OR can prevent an app from being throttled in battery saver mode    |    The most important notifications, these are   notifications that must be delivered right away in any circumstance where the   device can receive notifications. Things like VoIP calls or critical alerts   that should wake the device falls into this category.    |    VoIP calls, time- critical alerts    |
|    Medium    |    Yes – user can block all notifications from an app   OR can prevent an app from being throttled in battery saver mode.    |    These are things that are not asimportant, but the   user would still like them to be up to date. Things in this category don’t   need to happen right away but your users would be annoyed if it is not running   in the background.    |    Secondary Email account sync, live tile updates.    |
|    Low    |    Yes – user can block all notifications from an app   OR can prevent an app from being throttled in battery saver mode.    |    Notifications that only make sense when the user is using   the device or background activity makes sense. These are cached and not   processed until the user gets back to their device.    |    Contact status (online/offline)    |
|    Very Low     |    Yes – user can block all notifications from an app.        No – It cannot prevent very low priority   notifications from being throttled in battery saver mode    |    This is almost the same as low priority except that the   user cannot ovveride the battery saver policy. These notifications will never   be delivered in battery saver.    |    Syncing files for a sync service.    |

Note that many apps will have notifications of different priority throughout their lifecycle. Since the priority is set on a per-notification basis, this isn’t an issue. A VoIP app can send a high priority notification for an incoming call and then follow it up with a low priority one when a contact comes online. 

## Setting the Priority

Setting the priority on the notification request is done through an additional header on the POST request – X-WNS-PRIORITY. This is an integer value between 0 and 3 which maps to a priority: 

| Priority Name | X-WNS-PRIORITY Value | Default for: |
|---------------|----------------------|------------------|
| High | 0 | Toasts |
| Meduim | 1 | Tiles and Badges |
| Low | 2 | Raw |
| Very Low | 3 |  |

To be backward compatible, setting a priority is not required. In case an app doesn’t set the priority to their notifications the system will provide a default priority on behalf of the app. The defaults are shown in in the chart above and match the behavior of existing versions of Windows. That bears repeating the behavior of notifications in the Fall Creators Update will not change, but apps will now have the chance to raise or lower the priority of their notifications as desired. 

## Detailed Listing of Desktop Behavior 

If you are shipping your app across many different SKUs of Windows, it is normally best to take a scenario oriented approach where you pick your priorities based on where your scenarios fall in the above chart. We work hard to make sure the priorities behave reasonably on all device types.
However, in the interest of full transparency, the list of recommended behaviors for each priority is listed below. This is not a guarantee that each device will work exactly according to the chart – OEMs are free to configure the behaviour differently, but most are close to this chart. 
DO NOT BASE YOUR APPS BEHAVIOUR FOR ALL VERSIONS OF WINDOWS ON THIS CHART – IT IS INCLUDED ONLY FOR ILLUSTRATION PURPOSES.

| Device State    | PRIORITY: High    |    PRIORITY: Medium        | PRIORITY: Low    |    PRIORITY: Very Low    |
|-------------------------------------------------------|----------------------------------------------------|----------------------------------------------------|----------------------------------------------------|--------------------------|
|    Screen On OR plugged in    |    Deliver    |    Deliver    |    Deliver    |    Deliver    |
|    Screen Off AND on battery    |    Deliver    |    Batch     |    Cache *    |    Cache    |
|    Battery Saver enabled    |    If user exempted: deliver        Else: cache    |    If user exempted: deliver        Else: cache    |    If user exempted: deliver        Else: cache    |    Cache     |
|    On battery + battery saver enabled + screen off    |    If user exempted: deliver        Else: cache    |    If user exempted: deliver        Else: cache    |    Cache    |    Cache    |

For an app to be user exempted in battery saver, the user must go to the Battery Usage by App page and select Allow the app to run background tasks. This exempts your app from battery saver for high, medium, and low priority notifications. 

## Conclusion
That’s it, by adding a simple header to your WNS POST messages you have full control over how it will work in battery sensitive situations. This header, of course, is optional, but it can give your app an edge over others which aren’t as respectful of the device’s battery. 
We’re looking forward to seeing apps start using this header. If you have any questions about how using the header can help you, please let us know in the comments below.

