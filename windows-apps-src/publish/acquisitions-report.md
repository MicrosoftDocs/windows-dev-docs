---
Description: The Acquisitions report in Partner Center lets you see who has acquired and installed your app, along with demographic and platform details.
title: Acquisitions report
ms.assetid: 21126362-F3CD-4006-AD3F-82FC88E3B862
ms.date: 10/31/2018
ms.topic: article
keywords: windows 10, uwp, acquisitions, app sales, app downloads, installs, funnel, acquisition, conversions, channel, app page views
ms.localizationpriority: medium
---
# Acquisitions report


The **Acquisitions** report in [Partner Center](https://partner.microsoft.com/dashboard) lets you see who has acquired and installed your app, along with demographic and platform details, and shows info about how customers on Windows 10 (including Xbox) have arrived at your app's listing. You can also view near real-time acquisition data for the last hour or seventy-two hour period. 

You can view this data in Partner Center, or [download the report](download-analytic-reports.md) to view offline. Alternatively, you can programmatically retrieve this data by using our [analytics REST API](../monetize/access-analytics-data-using-windows-store-services.md).

In this report, an **acquisition** means a new customer has obtained a license to your app (whether you charged money or you've offered it for free). An **install** refers to the app being installed on a Windows 10 device.

> [!IMPORTANT]
> The **Acquisitions** report does not include data about refunds, reversals, chargebacks, etc. To estimate your app proceeds, visit [Payout summary](payout-summary.md). In the **Reserved** section, click the **Download reserved transactions** link.
>
> Except for page view data (as described below), this report does not include data related to customers who acquire an app without being signed in to a Microsoft account.


## Apply filters

Near the top of the page, you can select the time period for which you want to show data. The default selection is **30D** (30 days), but you can choose to show data for 3, 6, or 12 months, or for a custom data range that you specify. Near real time data will be shown for all options (except in **App cumulative** data). The **1H** and **72H** time periods only apply to the **App daily** tab of the **Acquisitions** chart and to the **Acquisitions** tab of the **Markets** chart. 

You can also expand **Filters** to filter all of the data on this page by market and/or by device type.

-   **Market**: The default filter is **All markets**, but you can limit the data to acquisitions in one or more markets.
-   **Device type**: The default setting is **All devices**. If you want to show data for acquisitions from a certain device type only (such as PC, console, or tablet), you can choose a specific one here.

The info in all of the charts listed below will reflect the date range and any filters you've selected. Some sections also allow you to apply additional filters.


## Acquisitions

The **Acquisitions** chart shows the number of daily or weekly acquisitions (a new customer obtaining a license for your app) over the selected period of time. (When you use **Apply filters** to show data for a longer duration, the acquisition data will be grouped by week.) Only acquisitions made by customers who are signed in with a valid Microsoft account are included in this chart. 

By default, we show the **App daily** view, which includes near real time data. You can also see the lifetime number of acquisitions for your app by selecting **App cumulative**. This shows the cumulative total of all acquisitions, starting from when your app was first published.

**Gross sales** for your app (from October 2016 - present) are also available in this chart, showing the total amount earned from app sales (in USD). Note that this amount does not account for any refunds,  reversals, chargeback, etc.

You can optionally filter the results by whether the acquisition originated from the client or web-based Store and/or by OS version.

> [!NOTE]
> You can also programmatically retrieve this data by using the [get app acquisitions](../monetize/get-app-acquisitions.md) method in our [analytics REST API](../monetize/access-analytics-data-using-windows-store-services.md).

In the **App daily** view, when the **30D** time period is selected, you may see circle markers. These represent a significant increase or decrease in a given value that we think you'll want to know about. The date on which the circle appears represents the end of the week in which we detected a significant increase or decrease compared to the week before that. To see more details about what's changed, hover over the circle.  

> [!TIP]
> You can view more insights related to significant changes over the last 30 days in the [Insights report](insights-report.md).

## Installs

The **Installs** chart shows how many times we have detected that customers have successfully installed your app on Windows 10 devices (including Xbox One consoles) over the selected period of time. The total number is shown, along with a chart showing installs by day or week (depending on the duration you've selected). You can optionally filter the results by a specific package version.

The install total includes:
-   **Installs on multiple Windows 10 devices.** For example, if the same customer installs your app on two Windows 10 PCs and one Xbox One console, that counts as three installs.
-   **Reinstalls.** For example, if a customer installs your app today, uninstalls your app tomorrow, and then reinstalls your app next month, that counts as two installs.

The install total does not include or reflect:
-   **Installs on non-Windows 10 devices.** If your app supports earlier OS versions such as Windows 8.x or Windows Phone 8.x, we don't count any installs on those devices.
-   **Uninstalls.** When a customer uninstalls your app from their device, we don’t subtract that from the total number of installs.
-   **Updates.** For example, if a customer installs your app today, and then installs an app update a week later, that only counts as one install.
-   **Preinstalls.** If a customer buys a device that has your app preinstalled, we don’t count that as an install.
-   **System-initiated installs.** If Windows installs your app automatically for some reason, we don’t count that as an install.

> [!NOTE]
> You can also programmatically retrieve this data by using the [get app installs](../monetize/get-app-installs.md) method in our [analytics REST API](../monetize/access-analytics-data-using-windows-store-services.md).

## Acquisition funnel

The **Acquisition funnel** shows you how many customers completed each step of the funnel, from viewing the Store page to using the app, along with the conversion rate. This data can help you identify areas where you might want to invest more to increase your acquisitions, installs, or usage.

> [!IMPORTANT]
> The **Acquisition funnel** shows data only for customers on Windows 10 (including Xbox) over the last 90 days.

The steps in the funnel are:

- **Page views**: This number represents the total views of your app's Store listing, including people who aren't signed in with a Microsoft account. This does not include data from customers who have opted out of providing this information to Microsoft.
- **Acquisitions**: The number of new customers who obtained a license to your app (when signed in with their Microsoft account) within 48 hours of viewing its Store listing.
- **Installs**: The number of customers who installed the app after acquiring it.
- **Usage**: The number of customers who used the app after installing it.

You can optionally filter the results by gender and/or age group, as well as by custom campaign ID.

> [!NOTE]
> You can also programmatically retrieve this data by using the [get app acquisition funnel data](../monetize/get-acquisition-funnel-data.md) method in our [analytics REST API](../monetize/access-analytics-data-using-windows-store-services.md).

## Markets

The **Markets** chart shows the total number of acquisitions or installs over the selected period of time for each market in which your app is available. You can choose whether to display data for **Acquisitions** or **Installs**.

You can view this data in a visual **Map** form, or toggle the setting to view it in **Table** form. Table form will show five markets at a time, sorted either alphabetically or by highest/lowest number of acquisitions or installs. You can also download the data to view info for all markets together.


## Customer demographic

The **Customer demographic** chart shows demographic info about the people who acquired your app. You can see how many acquisitions (over the selected period of time) were made by people in a certain age group and by which gender.

> [!NOTE]
> Some customers have opted not to share this info. If we were unable to determine the age group or gender, the acquisition is categorized as **Unknown**.

 

## App page views and conversions by channel

The **App page views and conversions by channel** chart lets you see how customers on Windows 10 arrived at your app's listing over the selected period of time.

In this chart, a *channel* refers to the method in which a customer arrived at your app's listing page (for example, browsing and searching in the Store, a link from an external website, a link from one of your custom campaigns, etc.). The following channel types are included:

-   **Store traffic:** The customer was browsing or searching within the Store when they viewed your app's listing.
-   **Custom campaign:** The customer followed a link that used a [custom campaign ID](create-a-custom-app-promotion-campaign.md).
-   **Other:** The customer followed an external link (without any custom campaign ID) from a website to your app's listing or the customer followed a link from a search engine to your app's listing.

A *page view* means that a customer viewed your app's Store listing page, either via the web-based Store or from within the Store app on Windows 10. This includes views by people who aren't signed in with a Microsoft account. Some customers have opted out of providing this information to Microsoft.

A *conversion* means that a customer (signed in with a Microsoft account) has newly obtained a license to your app (whether you charged money or you've offered it for free).

Page view and conversion numbers are not counts of unique customers. For conversion rate info, see the [Acquisition funnel](#acquisition-funnel) chart.

> [!NOTE]
> Customers could arrive at your app's listing by clicking on a custom campaign not created by you. We stamp every page view within a session with the campaign ID from which the customer first entered the Store. We then attribute conversions to that campaign ID for all acquisitions within 24 hours. Because of this, you may see a higher number of total conversions than the total conversions for your campaign IDs, and you may have conversions or add-on conversions that have zero page views.

> [!NOTE]
> You can also programmatically retrieve this data by using the [get app conversions by channel](../monetize/get-app-conversions-by-channel.md) method in our [analytics REST API](../monetize/access-analytics-data-using-windows-store-services.md).

## App page views and conversions by campaign ID

The **App page views and conversions by campaign ID** chart lets you track conversions and page views, as described above, for each of your [custom promotion campaigns](create-a-custom-app-promotion-campaign.md). The top campaign IDs are shown, and you can use the filters to exclude or include specific campaign IDs.

## Total campaign conversions

The **Total campaign conversions** chart shows the total number of app and add-on conversions from all custom campaigns during the selected period of time.





 

 
