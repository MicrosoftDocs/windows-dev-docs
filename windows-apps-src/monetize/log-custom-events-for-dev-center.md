---
author: mcleanbyron
Description: You can log custom events from your UWP app and review those events in the Usage report on the Windows Dev Center dashboard.
title: Log custom events for Dev Center
ms.author: mcleans
ms.date: 06/01/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, Microsoft Store Services SDK, log events
ms.assetid: 4aa591e0-c22a-4c90-b316-0b5d0410af19
ms.localizationpriority: high
---

# Log custom events for Dev Center

The [Usage report](https://msdn.microsoft.com/windows/uwp/publish/usage-report) in the Windows Dev Center dashboard lets you get info about custom events that you've defined in your Universal Windows Platform (UWP) app. A custom event is an arbitrary string that represents an event or activity in your app. For example, a game might define custom events named *firstLevelPassed*, *secondLevelPassed*, and so on, which are logged when the user passes each level in the game.

To log a custom event from your app, pass the custom event string to the [Log](https://msdn.microsoft.com/library/windows/apps/microsoft.services.store.engagement.storeservicescustomeventlogger.log.aspx) method provided by the Microsoft Store Services SDK. You can review the total occurrences for your custom events in the **Custom events** section of the [Usage report](https://msdn.microsoft.com/windows/uwp/publish/usage-report) in the Dev Center dashboard.

> [!NOTE]
> Custom events that you log to Dev Center are unrelated to [Windows events](https://msdn.microsoft.com/library/windows/desktop/aa964766.aspx), and they do not appear in **Event Viewer**.

## Prerequisites

Before you can review custom logging events in the **Usage report** for your app in the dashboard, your app must be published in the Store.

## How to log custom events

1. If you have not done so already, [Install the Microsoft Store Services SDK](microsoft-store-services-sdk.md#install-the-sdk) on your development computer.

2. Open your project in Visual Studio.

3. In Solution Explorer, right-click the **References** node for your project and click **Add Reference**.

4. In **Reference Manager**, expand **Universal Windows** and click **Extensions**.

5. In the list of SDKs, click the check box next to **Microsoft Engagement Framework** and click **OK**.

6. Add the following statement to the top of each code file where you want to log custom events.
    [!code-cs[EventLogger](./code/StoreSDKSamples/cs/LogEvents.cs#EngagementNamespace)]

7. In each section of your code where you want to log a custom event, get a [StoreServicesCustomEventLogger](https://msdn.microsoft.com/library/windows/apps/microsoft.services.store.engagement.storeservicescustomeventlogger.log.aspx) object and then call the [Log](https://msdn.microsoft.com/library/windows/apps/microsoft.services.store.engagement.storeservicescustomeventlogger.log.aspx) method. Pass your custom event string to the method.
    [!code-cs[EventLogger](./code/StoreSDKSamples/cs/LogEvents.cs#Log)]

    > [!NOTE]
    > The [Usage report](https://msdn.microsoft.com/windows/uwp/publish/usage-report) may take a long time to load if your app logs many custom events with long names. We recommend that you use brief names for your custom events. 

## Related topics

* [Usage report](https://msdn.microsoft.com/windows/uwp/publish/usage-report)
* [Log method](https://msdn.microsoft.com/library/windows/apps/microsoft.services.store.engagement.storeservicescustomeventlogger.log.aspx)
* [Microsoft Store Services SDK](https://msdn.microsoft.com/windows/uwp/monetize/microsoft-store-services-sdk)
