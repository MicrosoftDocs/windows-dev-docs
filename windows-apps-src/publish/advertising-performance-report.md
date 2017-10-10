---
author: jnHs
Description: To view performance data for the ad units in your apps, use the app-level and account-level advertising performance reports on the Windows Dev Center dashboard.
title: Advertising performance report
ms.assetid: 32E555C3-C34D-4503-82BB-4C3F5CAE4500
ms.author: wdg-dev-content
ms.date: 10/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Advertising performance report


The **Advertising performance report** shows how your [ad units](in-app-ads.md) are performing, including community ads. This report includes data from multiple ad providers in UWP apps that use [ad mediation](in-app-ads.md#mediation).

To view this report, expand **Analyze** in the left navigation menu and then select **Ad performance**.

To perform a deeper analysis of your data, we provide a **Download report** link you can use to download CSV (comma-separated values) files that you can open in Microsoft Excel or another program. Alternatively, you can programmatically retrieve this data by using the [get ad performance data](../monetize/get-ad-performance-data.md) method in the [Windows Store analytics REST API](../monetize/access-analytics-data-using-windows-store-services.md).

When viewing the advertising performance reports, be aware that reporting data for the last three days might change as we receive and process new data from various sources. Additionally, data restatements can happen up to 90 days in the past.


## Overall performance

At the top of the report, you can use the following filters to adjust the scope of the data shown in the report:

* **Date**: Filter the report to a preset time period or a custom date range. By default, the report shows data for the last 30 days.
* **Aggregation**: Here you can select how this data is aggregated and how it may be filtered further. By default, the report shows data from all ad units, and you'll see a **Choose ad units** link lower in the section, allowing you to select up to six ad units to compare. You can optionally change the **Aggregation** to **All apps** or **All ad providers**. If you do so, the link in this section will change to either **Choose apps** or **Choose ad providers**, allowing you to choose up to six of each to compare. You can also choose to aggregate by a specific app in which you use ads.
* **Ad providers**: Filter the report to performance data for certain ad providers. For more information about the available ad providers, see the [Ad mediation](in-app-ads.md#mediation) section in [In-app ads](in-app-ads.md). By default, the report shows data from all ad providers. This option will be disabled if you chose **All ad providers** in the **Aggregation** drop-down.
* **Device**: Filter the report to performance data for certain device types. By default, the report shows data for all device types.


## Report views

Below the filters, the report displays data from a variety of ad performance metrics in graph, world map, and table form for the ad units that are used in the current app.

To analyze the data for one of these metrics in a graph or world map view, click **Graph** or **Map**. By default, the graph and map views show performance data for all of the ad units, apps, or ad providers (depending on your selections in the **Aggregation** drop-down, but you have the option to select up to six individual ad units, apps, or ad providers to compare.

In the map view, darker shades represent higher values and lighter shades represent lower values. You can hover over a specific country or region on the map to analyze the value of the selected metric. You can also zoom in on any area of the map to view data for smaller countries.

To review all the performance metrics for the ad units in your app, refer to the table below the graph and map views.

> [!NOTE]
> If you had created ad units for an app using Microsoft pubCenter, it’s possible that not all of them were mapped successfully to your apps in Dev Center. In this report, these ad units are associated with app names that you specified in pubCenter, with the string **(pubCenter)** appended to the app name.


## Performance metrics

This report may include data for the following performance metrics. The metrics that are shown in the report vary by ad provider.

|  Metric  |  Description  |
|----------|---------------|
| Estimated revenue  |  The estimated amount of money you received from the ads running on your app. |
| eCPM  |  Effective cost per thousand impressions. |
| Requests  | The number of times an ad request was sent from your app.  |
| Impressions  | The number of times an ad was shown in your app.  |
| Fill rate  | The percentage of ad requests sent from your app in which an ad was shown.  |
| Clicks  |  The number of times someone clicked on an ad in your app. |
| CTR  |  Click-through rate, meaning the number of times an ad was clicked, divided by the number of impressions. |
| Credits earned  | For [community ads](https://docs.microsoft.com/windows/uwp/publish/about-community-ads), this indicates the number of credits you have earned for promotional ad space by showing community ads in your app.  |
| Credits spent  | For [community ads](https://docs.microsoft.com/windows/uwp/publish/about-community-ads), this indicates the number of credits you have spent on ads for your app.  |


 
