---
title: Programming the Real-Time Activity Service
author: KevinAsgari
description: Learn about programming the Xbox Live Real-Time Activity Service with the C++ APIs.
ms.assetid: 98cdcb1f-41d8-43db-98fc-6647755d3b17
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, real time activity
ms.localizationpriority: low
---
# Programming the Real-Time Activity Service using C++ APIs

This article contains the following sections

* Connecting to the Real-Time Activity Service from Xbox Live
* Disconnected from the Real-Time Activity Service
* Creating a statistic
* Subscribing to a statistic from the Real-Time Activity
* Unsubscribing from a statistic from the Real-Time Activity service
* Sample

## Connecting to the Real-Time Activity Service from Xbox Live

Applications must connect to the Real-Time Activity (RTA) service to get event information from Xbox Live. This topic shows how to make such a connection.

> [!NOTE]
> The examples used in this topic indicate method calls for one user. However, your title must make these calls for all users to connect to and disconnect from the Real-Time Activity (RTA) service.

### Connecting to the Real-Time Activity service

```cpp
void Example_RealTimeActivity_ConnectAsync()
{
    // Get the list of users from the system, and create an Xbox Live context from the first.
    // This user's authentication token will be used for the service requests.

    // Note, only retrieve an XboxLiveContext for a given user *once*.  Otherwise you may encounter unpredictable behavior.
    std::shared_ptr<xbox_live_context> xboxLiveContext = std::make_shared<xbox_live_context>(User::Users->GetAt(0));

    xboxLiveContext->real_time_activity_service()->activate();
}
```

### Creating a statistic

You create statistics on XDP if you are an XDK developer or working on a cross-play title.  You create statistic on Dev Center if you are making a pure UWP running on Windows 10.

#### XDK developers

For information on how to create a stat on XDP, please see the [XDP Documentation](https://developer.xboxlive.com/en-us/xdphelp/development/xdpdocs/Pages/setting_up_service_configuration_10_27_15_a.aspx#events).  After you have created your stat and defined your events, you will need to run the [XCETool](https://developer.xboxlive.com/en-us/platform/development/documentation/software/Pages/atoc_xce_jun15.aspx) to generate a header used by your application.  This header contains functions you can call to send events that modify stats.

#### UWP developers

If you are developing a UWP on Windows 10 that is not a cross-play title, you define your stats on Dev Center.  We will have documentation on how to do this shortly.  In the meantime, if you have any questions, please reach out to your DAM.

### Disconnecting from the Real-Time Activity service

```cpp
void Example_RealTimeActivity_Disconnect()
{
    // Get the list of users from the system, and create an Xbox Live context from the first.
    // This user's authentication token will be used for the service requests.
    std::shared_ptr<xbox_live_context> xboxLiveContext = std::make_shared<xbox_live_context>(User::Users->GetAt(0));

    xboxLiveContext->real_time_activity_service()->deactivate();
}
```

## Subscribing to a statistic from the Real-Time Activity

Applications subscribe to a Real-Time Activity (RTA) to get updates when the statistics configured in Xbox Developer Portal (XDP) change.

### Subscribing to a statistic from the Real-Time Activity service

```cpp
void Example_RealTimeActivity_SubscribeToStatisticChangeAsync()
{
    // Obtain xboxLiveContext as shown in the connect function.  Then add a handler to be called on statistic changes.
    function_context statisticsChangeContext = xboxLiveContext->user_statistics_service().add_statistic_changed_handler(
        [](statistic_change_event_args args )
        {
            // Called on statistic change.  Inspect args to see which one.
            DebugPrint("%S %S", args.latest_statistic().statistic_name().c_str(), args.latest_statistic().value().c_str());
        }
    );

    // Call to subscribe to an individual statistic.  Once the subscription is complete, the handler will be called with the initial value of the statistic.
    auto statisticResults = xboxLiveContext->user_statistics_service().subscribe_to_statistic_change(
        User::Users->GetAt(0)->XboxUserId->Data(),
        L"xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxxx",    // Get SCID from "Product Details" page in XDP
         L"YourStat"
        );

    std::shared_ptr<statistic_change_subscription> statisticsChangeSubscription;

    if(!statisticResults.err())
    {
        statisticsChangeSubscription = statisticResults.payload();
    }
    else
    {
        DebugPrint("Error calling SubscribeToStatistic: %S", statisticResults.err_message().c_str());
    }
}
```

## Unsubscribing from a statistic from the Real-Time Activity service

Applications subscribe to a statistic from the Real-Time Activity (RTA) service to get updates when the statistic changes. When these updates are no longer needed, the subscription can be terminated, and the code in this topic shows how to do that.

### Unsubscribing from a real-time services statistic

```cpp
void Example_RealTimeActivity_UnsubscribeFromStatisticChangeAsync()
{
    // statisticsChangeSubscription from the Example_RealTimeActivity_SubscribeToStatisticChangeAsync function.
    xboxLiveContext->user_statistics_service().unsubscribe_from_statistic_change(
        statisticsChangeSubscription
        );
}
```
