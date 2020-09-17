---
Description: To view performance data for the ad units in your apps, use the advertising performance report in Partner Center.
title: Advertising performance report
ms.assetid: 32E555C3-C34D-4503-82BB-4C3F5CAE4500
ms.date: 10/31/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Advertising performance report


The **Advertising performance report** in [Partner Center](https://partner.microsoft.com/dashboard) shows how your [ad units](in-app-ads.md) are performing, including community ads. This report includes data from multiple ad providers in UWP apps that use [ad mediation](in-app-ads.md#mediation).

To view this report, expand **Analyze** in the left navigation menu and then select **Ad performance**. You can view this data in Partner Center, or download the report data to view offline by clicking the arrow icons on the page. Alternatively, you can programmatically retrieve this data by using the [get ad performance data](../monetize/get-ad-performance-data.md) method in our [analytics REST API](../monetize/access-analytics-data-using-windows-store-services.md).

When viewing the advertising performance reports, be aware that reporting data for the last three days might change as we receive and process new data from various sources. Additionally, data restatements can happen up to 90 days in the past.

## Apply filters

Near the top of the page, you can select the time period for which you want to show data. The default selection is 30D (30 days), but you can choose to show data for 3, 6, or 12 months, or for a custom data range that you specify.

You can also expand **Filters** to filter all of the data on this page by ad unit, app, ad provider, and device type. You can choose from the following options:

* **Aggregation**: Choose how the report data is aggregated and how it may be filtered further. By default, this filter is set to **All ad units**. You can optionally change this filter to **All apps** or **All ad providers**, or you can choose to aggregate by a specific app in which you use ads.
* **Ad providers**: Filter the report to performance data for certain [ad providers](in-app-ads.md#paid-networks). By default, the report shows data from all ad providers. This option will be disabled if you chose **All ad providers** in the **Aggregation** drop-down.
* **Device**: Filter the report to performance data for certain device types. By default, the report shows data for all device types.

## Overall performance

This section displays ad performance metrics in a chart or world map view for the ad units, apps, and ad providers you have selected in the report filters.

To switch to a different view of the data, click **Chart** or **Map** at the top of the **Overall performance** section. In the map view, darker shades represent higher values and lighter shades represent lower values. You can hover over a specific country or region on the map to analyze the value of the selected metric. You can also zoom in on any area of the map to view data for smaller countries.

You can refine the data shown in the chart or map by clicking the filter icon next to the **Chart** or **Map** drop-down. This filter enables you to choose from up to six different ad units, apps, or ad providers to compare in the chart or map view. The type of data you can choose from in this filter depends on what you selected for the **Aggregation** top-level filter at the top of the report.


## Overall performance breakdown

This section displays a table that contains all the ad performance metrics for the data set specified by the filters you have selected in the report.

## Performance metrics

The **Advertising performance** report includes data for the following performance metrics. Some metrics are shown only for certain ad providers.

|  Metric  |  Description  |
|----------|---------------|
| Estimated revenue  |  The estimated amount of money you received from the ads running on your app. |
| eCPM  |  Effective cost per thousand impressions. |
| Requests  | The number of times an ad request was sent from your app.  |
| Impressions  | The number of times an ad was shown in your app.  |
| Fill rate  | The percentage of ad requests sent from your app in which an ad was shown.  |
| Clicks  |  The number of times someone clicked on an ad in your app. |
| CTR  |  Click-through rate, meaning the number of times an ad was clicked, divided by the number of impressions. |
| Viewability | The percentage of ad impressions that are viewable in your app. For more details about how this value is calculated, see [Optimize the viewability of your ad units](../monetize/optimize-ad-unit-viewability.md). |
| Credits earned  | If you are running a [community ad](./about-community-ads.md) campaign, this indicates the number of credits you have earned for promotional ad space by showing community ads in your app.  |
| Credits spent  | If you are running a [community ad](./about-community-ads.md) campaign, this indicates the number of credits you have spent on ads for your app.  |

## Related topics

* [In-app ads](in-app-ads.md)
* [Display ads in your app with the Microsoft Advertising SDK](../monetize/display-ads-in-your-app.md)
* [Optimize the viewability of your ad units](../monetize/optimize-ad-unit-viewability.md)


 