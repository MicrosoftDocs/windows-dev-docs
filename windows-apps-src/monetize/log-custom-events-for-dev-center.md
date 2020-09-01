---
Description: You can log custom events from your UWP app and review those events in the Usage report in Partner Center.
title: Log custom events for Partner Center
ms.date: 06/01/2018
ms.topic: article
keywords: windows 10, uwp, Microsoft Store Services SDK, log events
ms.assetid: 4aa591e0-c22a-4c90-b316-0b5d0410af19
ms.localizationpriority: medium
---
# Log custom events for Partner Center

The [Usage report](../publish/usage-report.md) in Partner Center lets you get info about custom events that you've defined in your Universal Windows Platform (UWP) app. A custom event is an arbitrary string that represents an event or activity in your app. For example, a game might define custom events named *firstLevelPassed*, *secondLevelPassed*, and so on, which are logged when the user passes each level in the game.

To log a custom event from your app, pass the custom event string to the [Log](/uwp/api/microsoft.services.store.engagement.storeservicescustomeventlogger.log) method provided by the Microsoft Store Services SDK. You can review the total occurrences for your custom events in the **Custom events** section of the [Usage report](../publish/usage-report.md) in Partner Center.

> [!NOTE]
> Custom events that you log to Partner Center are unrelated to [Windows events](/windows/desktop/Events/windows-events), and they do not appear in **Event Viewer**.

## Prerequisites

Before you can review custom logging events in the **Usage report** for your app in Partner Center, your app must be published in the Store.

## How to log custom events

1. If you have not done so already, [Install the Microsoft Store Services SDK](microsoft-store-services-sdk.md#install-the-sdk) on your development computer.

2. Open your project in Visual Studio.

3. In Solution Explorer, right-click the **References** node for your project and click **Add Reference**.

4. In **Reference Manager**, expand **Universal Windows** and click **Extensions**.

5. In the list of SDKs, click the check box next to **Microsoft Engagement Framework** and click **OK**.

6. Add the following statement to the top of each code file where you want to log custom events.
    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreSDKSamples/cs/LogEvents.cs" id="EngagementNamespace":::

7. In each section of your code where you want to log a custom event, get a [StoreServicesCustomEventLogger](/uwp/api/microsoft.services.store.engagement.storeservicescustomeventlogger.log) object and then call the [Log](/uwp/api/microsoft.services.store.engagement.storeservicescustomeventlogger.log) method. Pass your custom event string to the method.
    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/StoreSDKSamples/cs/LogEvents.cs" id="Log":::

    > [!NOTE]
    > The [Usage report](../publish/usage-report.md) may take a long time to load if your app logs many custom events with long names. We recommend that you use brief names for your custom events. 

## Related topics

* [Usage report](../publish/usage-report.md)
* [Log method](/uwp/api/microsoft.services.store.engagement.storeservicescustomeventlogger.log)
* [Microsoft Store Services SDK](./microsoft-store-services-sdk.md)
