---
description: Learn about raw notifications, which are short, general purpose push notifications that are strictly instructional and don't include a UI component.
title: Raw notification overview
ms.assetid: A867C75D-D16E-4AB5-8B44-614EEB9179C7
template: detail.hbs
ms.date: 05/19/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Raw notification overview


Raw notifications are short, general purpose push notifications. They are strictly instructional and do not include a UI component. As with other push notifications, the Windows Push Notification Services (WNS) feature delivers raw notifications from your cloud service to your app.

You can use raw notifications for a variety of purposes, including to trigger your app to run a background task if the user has given the app permission to do so. By using WNS to communicate with your app, you can avoid the processing overhead of creating persistent socket connections, sending HTTP GET messages, and other service-to-app connections.

> [!IMPORTANT]
> To understand raw notifications, it's best to be familiar with the concepts discussed in the [Windows Push Notification Services (WNS) overview](windows-push-notification-services--wns--overview.md).

 

As with toast, tile, and badge push notifications, a raw notification is pushed from your app's cloud service over an assigned channel Uniform Resource Identifier (URI) to WNS. WNS, in turn, delivers the notification to the device and user account associated with that channel. Unlike other push notifications, raw notifications don't have a specified format. The content of the payload is entirely app-defined.

As an illustration of an app that could benefit from raw notifications, let's look at a theoretical document collaboration app. Consider two users who are editing the same document at the same time. The cloud service, which hosts the shared document, could use raw notifications to notify each user when changes are made by the other user. The raw notifications would not necessarily contain the changes to the document, but instead would signal each user's copy of the app to contact the central location and sync the available changes. By using raw notifications, the app and its cloud service can save the overhead of maintaining persistent connections the entire time the document is open.

## How raw notifications work


All raw notifications are push notifications. Therefore, the setup required to send and receive push notifications applies to raw notifications as well:

-   You must have a valid WNS channel to send raw notifications. For more information about acquiring a push notification channel, see [How to request, create, and save a notification channel](/previous-versions/windows/apps/hh465412(v=win.10)).
-   You must include the **Internet** capability in your app's manifest. In the Microsoft Visual Studio manifest editor, you will find this option under the **Capabilities** tab as **Internet (Client)**. For more information, see [**Capabilities**](/uwp/schemas/appxpackage/appxmanifestschema/element-capabilities).

The body of the notification is in an app-defined format. The client receives the data as a null-terminated string (**HSTRING**) that only needs to be understood by the app.

If the client is offline, raw notifications will be cached by WNS only if the [X-WNS-Cache-Policy](/previous-versions/windows/apps/hh465435(v=win.10)) header is included in the notification. However, only one raw notification will be cached and delivered once the device comes back online.

There are only three possible paths for a raw notification to take on the client: they will be delivered to your running app through a notification delivery event, sent to a background task, or dropped. Therefore, if the client is offline and WNS attempts to deliver a raw notification, the notification is dropped.

## Creating a raw notification


Sending a raw notification is similar to sending a tile, toast, or badge push notification, with these differences:

-   The HTTP Content-Type header must be set to "application/octet-stream".
-   The HTTP [X-WNS-Type](/previous-versions/windows/apps/hh465435(v=win.10)) header must be set to "wns/raw".
-   The notification body can contain any string payload smaller than 5 KB in size, but must not be an empty string.

Raw notifications are intended to be used as short messages that trigger your app to take an action, such as to directly contact the service to sync a larger amount of data or to make a local state modification based on the notification content. Note that WNS push notifications cannot be guaranteed to be delivered, so your app and cloud service must account for the possibility that the raw notification might not reach the client, such as when the client is offline.

For more information on sending push notifications, see [Quickstart: Sending a push notification](/previous-versions/windows/apps/hh868252(v=win.10)).

## Receiving a raw notification


There are two avenues through which your app can be receive raw notifications:

-   Through [notification delivery events](#notification-delivery-events) while your application is running.
-   Through [background tasks triggered by the raw notification](#background-tasks-triggered-by-raw-notifications) if your app is enabled to run background tasks.

An app can use both mechanisms to receive raw notifications. If an app implements both the notification delivery event handler and background tasks that are triggered by raw notifications, the notification delivery event will take priority when the app is running.

-   If the app is running, the notification delivery event will take priority over the background task and the app will have the first opportunity to process the notification.
-   The notification delivery event handler can specify, by setting the event's [**PushNotificationReceivedEventArgs.Cancel**](/uwp/api/Windows.Networking.PushNotifications.PushNotificationReceivedEventArgs.Cancel) property to **true**, that the raw notification should not be passed to its background task once the handler exits. If the **Cancel** property is set to **false** or is not set (the default value is **false**), the raw notification will trigger the background task after the notification delivery event handler has done its work.

### Notification delivery events

Your app can use a notification delivery event ([**PushNotificationReceived**](/uwp/api/Windows.Networking.PushNotifications.PushNotificationChannel.PushNotificationReceived)) to receive raw notifications while the app is in use. When the cloud service sends a raw notification, the running app can receive it by handling the notification delivery event on the channel URI.

If your app is not running and does not use [background tasks](#background-tasks-triggered-by-raw-notifications)), any raw notification sent to that app is dropped by WNS on receipt. To avoid wasting your cloud service's resources, you should consider implementing logic on the service to track whether the app is active. There are two sources of this information: an app can explicitly tell the service that it's ready to start receiving notifications, and WNS can tell the service when to stop.

-   **The app notifies the cloud service**: The app can contact its service to let it know that the app is running in the foreground. The disadvantage of this approach is that the app can end up contacting your service very frequently. However, it has the advantage that the service will always know when the app is ready to receive incoming raw notifications. Another advantage is that when the app contacts its service, the service then knows to send raw notifications to the specific instance of that app rather than broadcast.
-   **The cloud service responds to WNS response messages** : Your app service can use the [X-WNS-NotificationStatus](/previous-versions/windows/apps/hh465435(v=win.10)) and [X-WNS-DeviceConnectionStatus](/previous-versions/windows/apps/hh465435(v=win.10)) information returned by WNS to determine when to stop sending raw notifications to the app. When your service sends a notification to a channel as an HTTP POST, it can receive one of these messages in the response:

    -   **X-WNS-NotificationStatus: dropped**: This indicates that the notification was not received by the client. It's a safe assumption that the **dropped** response is caused by your app no longer being in the foreground on the user's device.
    -   **X-WNS-DeviceConnectionStatus: disconnected** or **X-WNS-DeviceConnectionStatus: tempconnected**: This indicates that the Windows client no longer has a connection to WNS. Note that to receive this message from WNS, you have to ask for it by setting the [X-WNS-RequestForStatus](/previous-versions/windows/apps/hh465435(v=win.10)) header in the notification's HTTP POST.

    Your app's cloud service can use the information in these status messages to cease communication attempts through raw notifications. The service can resume sending raw notifications once it is contacted by the app, when the app switches back into the foreground.

    Note that you should not rely on [X-WNS-NotificationStatus](/previous-versions/windows/apps/hh465435(v=win.10)) to determine whether the notification was successfully delivered to the client.

    For more information, see [Push notification service request and response headers](/previous-versions/windows/apps/hh465435(v=win.10))

### Background tasks triggered by raw notifications

> [!IMPORTANT]
> Before using raw notification background tasks, an app must be granted background access via [**BackgroundExecutionManager.RequestAccessAsync**](/uwp/api/Windows.ApplicationModel.Background.BackgroundExecutionManager#Windows_ApplicationModel_Background_BackgroundExecutionManager_RequestAccessAsync_System_String_).

 

Your background task must be registered with a [**PushNotificationTrigger**](/uwp/api/Windows.ApplicationModel.Background.PushNotificationTrigger). If it is not registered, the task will not run when a raw notification is received.

A background task that is triggered by a raw notification enables your app's cloud service to contact your app, even when the app is not running (though it might trigger it to run). This happens without the app having to maintain a continuous connection. Raw notifications are the only notification type that can trigger background tasks. However, while toast, tile, and badge push notifications cannot trigger background tasks, background tasks triggered by raw notifications can update tiles and invoke toast notifications through local API calls.

As an illustration of how background tasks that are triggered by raw notifications work, let's consider an app used to read e-books. First, a user purchases a book online, possibly on another device. In response, the app's cloud service can send a raw notification to each of the user's devices, with a payload that states that the book was purchased and the app should download it. The app then directly contacts the app's cloud service to begin a background download of the new book so that later, when the user launches the app, the book is already there and ready for reading.

To use a raw notification to trigger a background task, your app must:

1.  Request permission to run tasks in the background (which the user can revoke at any time) by using [**BackgroundExecutionManager.RequestAccessAsync**](/uwp/api/Windows.ApplicationModel.Background.BackgroundExecutionManager#Windows_ApplicationModel_Background_BackgroundExecutionManager_RequestAccessAsync_System_String_).
2.  Implement the background task. For more information, see [Support your app with background tasks](/windows/uwp/launch-resume/support-your-app-with-background-tasks)

Your background task is then invoked in response to the [**PushNotificationTrigger**](/uwp/api/Windows.ApplicationModel.Background.PushNotificationTrigger), each time a raw notification is received for your app. Your background task interprets the raw notification's app-specific payload and acts on it.

For each app, only one background task can run at a time. If a background task is triggered for an app for which a background task is already running, the first background task must complete before the new one is run.

## Other resources


You can learn more by downloading the [Raw notifications sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/Raw%20notifications%20sample%20(Windows%208)) for Windows 8.1, and the [Push and periodic notifications sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/Push%20and%20periodic%20notifications%20client-side%20sample%20(Windows%208)) for Windows 8.1, and re-using their source code in your Windows 10 app.

## Related topics

* [Guidelines for raw notifications]()
* [Quickstart: Creating and registering a raw notification background task](/previous-versions/windows/apps/jj676800(v=win.10))
* [Quickstart: Intercepting push notifications for running apps](/previous-versions/windows/apps/jj709908(v=win.10))
* [**RawNotification**](/uwp/api/Windows.Networking.PushNotifications.RawNotification)
* [**BackgroundExecutionManager.RequestAccessAsync**](/uwp/api/Windows.ApplicationModel.Background.BackgroundExecutionManager#Windows_ApplicationModel_Background_BackgroundExecutionManager_RequestAccessAsync_System_String_)
 

 
