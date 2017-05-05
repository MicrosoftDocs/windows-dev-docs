---
author: jnHs
Description: To view performance data for the ad units in your apps, use the app-level and account-level advertising performance reports on the Windows Dev Center dashboard.
title: Advertising performance report
ms.assetid: 32E555C3-C34D-4503-82BB-4C3F5CAE4500
ms.author: wdg-dev-content
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Advertising performance report


To view advertising performance data for your apps, you can use the following reports on the Windows Dev Center dashboard:

-   [App-level advertising performance report](#app-level-advertising-performance-report). This report provides performance data for the ad units in the currently selected app in the dashboard.
-   [Account-level advertising performance report](#account-level-advertising-performance-report). This report provides detailed performance data for ad units and community ads for all apps that are registered to your developer account. This report also includes the [affiliates performance report](affiliates-performance-report.md).

These reports show data from ad units that are associated with **AdControl** or **InterstitialAd** controls in your apps. For more information about these controls, see [Display ads in your app](../monetize/display-ads-in-your-app.md).

> [!NOTE]
> We have expanded the advertising performance reports to show ad performance data from multiple ad networks in UWP apps that use [ad mediation](monetize-with-ads.md#mediation) in an **AdControl** or **InterstitialAd** control. These reports provide an **Ad providers** filter that you can use to compare the performance of different ad networks.

To perform a deeper analysis of your data, the advertising performance reports provide a **Download report** link you can use to download a CSV (comma-separated values) file that you can open in Microsoft Excel or a similar program. Alternatively, you can programmatically retrieve this data by using the [get ad performance data](../monetize/get-ad-performance-data.md) method in the [Windows Store analytics REST API](../monetize/access-analytics-data-using-windows-store-services.md).

The following sections provide more details about these reports.

<span id="app-level-advertising-performance-report" />
## App-level advertising performance report

This report provides ad performance data for the currently selected app in the dashboard. To view this report, select one of your apps in the dashboard and click **Analytics** &gt; **Advertising performance** in the left navigation pane.

### Filters

At the top of the report, you can use the following filters to adjust the scope of the data shown in the report:

* **Date**: Filter the report to a preset time period or a custom date range. By default, the report is filtered on ad performance data for the last 30 days.
* **Ad providers**: Filter the report to performance data for certain ad providers. For more information about the available ad providers, see the [Ad mediation](monetize-with-ads.md#mediation) section in [Monetize with ads](monetize-with-ads.md). By default, the report is filtered on all ad providers.
* **Device**: Filter the report to performance data for certain device types. By default, the report is filtered on all device types.

<span id="app-level-views" />
### Report views

Below the filters, the report displays data from a variety of [ad performance metrics](#metrics) in graph, world map, and table form for the ad units that are used in the current app.

To analyze the data for one of these metrics in a graph or world map view, click **Graph** or **Map**. Click the headers above the graph or map to switch between the different metrics. By default, the graph and map views show performance data for all of the ad units in your app, but you can click **Choose ad units** to select up to six individual ad units to compare.

In the map view, darker shades represent higher values and lighter shades represent lower values. You can hover over a specific country or region on the map to analyze the value of the selected metric. You can also zoom in on any area of the map to view data for smaller countries.

To review all the performance metrics for the ad units in your app, refer to the table below the graph and map views.

<span id="account-level-advertising-performance-report" />
## Account-level advertising performance report

This report provides performance data for all the apps that are registered to your developer account. This report also includes performance data for any Microsoft pubCenter ad units that were not successfully mapped to your Dev Center apps. To view this report, click **Analytics** &gt; **Advertising performance** in the navigation menu at the top of any page in the dashboard.

> [!NOTE]
> This report also includes the [affiliates performance report](affiliates-performance-report.md) at the bottom of the page.

### Filters

At the top of the report, you can use the following filters to adjust the scope of the data shown in the report:

* **Date**: Filter the report to a preset time period or a custom date range. By default, the report is filtered on ad performance data for the last 30 days.
* **Aggregation**: Filter the report to performance data for specific apps. By default, the report is filtered on all apps.
* **Ad providers**: Filter the report to performance data for certain ad providers. For more information about the available ad providers, see the [Ad mediation](monetize-with-ads.md#mediation) section in [Monetize with ads](monetize-with-ads.md). By default, the report is filtered on all ad providers.
* **Device**: Filter the report to performance data for certain device types. By default, the report is filtered on all device types.

### Report views

Below the filters, the report displays data from a variety of [ad performance metrics](#metrics) in graph, world map, and table form for the ad units that are used in all the apps that are registered to your account. The graph, world map, and table views behave as described above for the [app-level report](#app-level-advertising-performance-report).

> [!NOTE]
> If you created ad units for an app using Microsoft pubCenter, it’s possible that not all of them were mapped successfully to your apps in Dev Center. In this report, these ad units are associated with app names that you specified in pubCenter, with the string **(pubCenter)** appended to the app name.

<span id="metrics" />
## Performance metrics

The app-level and account-level reports show data from the following performance metrics. The metrics that are shown in the report vary by ad provider.

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

## Notes about the reports

Here are a few things to keep in mind when using the advertising performance reports.

* Reporting data for the last three days might change as we receive and process new data from various sources.
* Data restatements can happen up to 90 days in the past.

## Related topics

* [Display ads in your app](../monetize/display-ads-in-your-app.md)
* [Monetize with ads](monetize-with-ads.md)
* [Affiliates performance report](affiliates-performance-report.md)
 
