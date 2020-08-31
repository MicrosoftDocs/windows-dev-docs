---
Description: The Usage report in Partner Center lets you see how customers are using your app.
title: Usage report
ms.assetid: 5F0E7F94-D121-4AD3-A6E5-9C0DEC437BD3
ms.date: 10/31/2018
ms.topic: article
keywords: windows 10, uwp, usage, custom event, report, telemetry, user sessions
ms.localizationpriority: medium
---
# Usage report


The **Usage** report in [Partner Center](https://partner.microsoft.com/dashboard) lets you see how customers on Windows 10 (including Xbox) are using your app, and shows info about custom events that you've defined. You can view this data in Partner Center, or [download the report](download-analytic-reports.md) to view offline.


## Apply filters

Near the top of the page, you can select the time period for which you want to show data. The default selection is **30D** (30 days), but you can choose to show data for 3, 6, or 12 months, or for a custom data range that you specify.

You can also expand **Filters** to filter the data on this page by package version, market, and/or device type.

-   **Package version**: The default setting is **All**. If your app includes more than one package, you can choose a specific one here.
-   **Market**: The default filter is **All markets**, but you can limit the data to one or more markets.
-   **Device type**: The default setting is **All**, but you can choose to show data for only one specific device type (PC, console, tablet, etc.).

The info in all of the charts listed below will reflect the date range and any filters you've selected (with the exception of **New users** in the **Usage** chart, which will not appear if any filters are selected). Some sections also allow you to apply additional filters.

> [!IMPORTANT]
> This report only includes usage data from customers on Windows 10 (including Xbox) who have not opted out of providing telemetry info. The usage data for Xbox games is included here regardless of whether the customer was signed into Xbox Live. 


## Usage

The **Usage** chart shows details about how your customers are using your app over the selected period of time. Note this chart does not track unique users for your app or unique user sessions (that is, a user is represented in this chart whether they used your app just once or multiple times).

This chart has separate tabs that you can view, showing usage by day or week (depending on the duration you've selected).

- **Users**: Shows the total number of **user sessions** over the selected period of time. Each user session represents a distinct period of time, starting when the app launches (process start) and ending when it terminates (process end) or after a period of inactivity. Because of this, a single customer could have multiple user sessions over the same day or week. The total number of **Active users** (any customer using the app that day or week) and **New users** (a customer who used your app for the first time that day or week) are also shown. Note that if you have applied any filters to the page, you won't see **New users** in this chart.
- **Devices**: Shows the number of daily devices used to interact with your app by all users.
- **Duration**: Shows the total engagement hours (hours where a user is actively using your app).
- **Engagement**: Shows the average engagement minutes per user (average duration of all user sessions). 
- **Retention**: Shows the total number of **DAU/MAU** (Daily Active Users/Monthly Active Users) over the selected period of time.

When the **30D** time period is selected, you may see circle markers when viewing the **Users**, **Devices**, or **Duration** tabs. These represent a significant increase or decrease in a given value that we think you'll want to know about. The date on which the circle appears represents the end of the week in which we detected a significant increase or decrease compared to the week before that. To see more details about what's changed, hover over the circle.  

> [!TIP]
> You can view more insights related to significant changes over the last 30 days in the [Insights report](insights-report.md).


## User sessions

The **User sessions** chart shows the total number of user sessions for your app per market, over the selected period of time.

As with the **User sessions** info in the **Usage** chart, a user session represents one distinct period of time when a customer interacted with your app, and this chart does not track unique users for your app.

You can view this data in a visual **Map** form, or toggle the setting to view it in **Table** form. Table form will show five markets at a time, sorted either alphabetically or by highest/lowest number of user sessions. You can also download the data to view info for all markets together.


## Package version

The **User sessions** chart shows the total number of daily user sessions for your app per package version over the selected period of time.

As with the **User sessions** chart, a user session represents one distinct period of time when a customer interacted with your app, and this chart does not track unique users for your app.


## Custom events

The **Custom events** chart shows the total occurrences for custom events that you have defined for your app. This may include multiple occurrences for the same customer. You can use the filters to select the specific custom events for which you want to see this data.

Custom events are implemented using the [StoreServicesCustomEventLogger.Log](/uwp/api/microsoft.services.store.engagement.storeservicescustomeventlogger.log) method in the [Microsoft Store Services SDK](../monetize/microsoft-store-services-sdk.md).

For more info, see [Log custom events for Dev Center](../monetize/log-custom-events-for-dev-center.md).


## Custom events breakdown

The **Custom events breakdown** chart shows more details about how often each of your custom events occurred. This can help you determine if events are occurring more often for a particular market, device type, or package versions.

For each event, you will see the event name and an event count that correspond to a specific combination of the user's market, device type, and package version. Typically, you'll see an event listed multiple times along with different combinations of these factors. 




 