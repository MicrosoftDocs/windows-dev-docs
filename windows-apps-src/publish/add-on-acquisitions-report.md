---
Description: The Add-on acquisitions report in Partner Center lets you see how many add-ons you've sold, along with demographic and platform details.
title: Add-on acquisitions report
ms.assetid: F2DF9188-0A98-4AC3-81C0-3E2C37B15582
ms.date: 10/31/2018
ms.topic: article
keywords: windows 10, uwp, add-on sales, add-on acquisitions, iap sales, in-app products, iaps, add-ons
ms.localizationpriority: medium
---
# Add-on acquisitions report


The **Add-on acquisitions** report in [Partner Center](https://partner.microsoft.com/dashboard) lets you see how many add-ons you've sold, along with demographic and platform details, and shows conversion info for customers on Windows 10 (including Xbox). You can also view near real-time acquisition data for the last hour or seventy-two hour period.

You can view this data in Partner Center, or [download the report](download-analytic-reports.md) to view offline. Alternatively, you can programmatically retrieve this data by using the [get add-on acquisitions](../monetize/get-in-app-acquisitions.md) method in the [Microsoft Store analytics REST API](../monetize/access-analytics-data-using-windows-store-services.md).

In this report, an add-on acquisition means a customer has purchased an add-on from you (or acquired it without paying, if you offered it for free). Multiple purchases of the same consumable add-on by the same customer are counted as separate add-on acquisitions.

> [!IMPORTANT]
> The **Add-on acquisitions** report does not include data about refunds, reversals, chargebacks, etc. To estimate your app proceeds, visit [Payout summary](payout-summary.md). In the **Reserved** section, click the **Download reserved transactions** link.


## Apply filters

Near the top of the page, you can select the time period for which you want to show data. The default selection is **30D** (30 days), but you can choose to show data for 3, 6, or 12 months, or for a custom data range that you specify. You can also select **1H** or **72H** to show acquisition data in near real time for either one hour or seventy-two hours; these time periods only apply to the **Add-on daily** tab of the **Add-on acquisitions** chart and to the **Acquisitions** tab of the **Markets** chart. 

You can also expand **Filters** to filter all of the data on this page by particular add-on(s), by market and/or by device type.

-   **Add-on**: The default filter is **All add-ons**, but you can limit the data to one or more of the app's add-ons.
-   **Market**: The default filter is **All markets**, but you can limit the data to acquisitions in one or more markets.
-   **Device type**: The default setting is **All devices**. If you want to show data for acquisitions from a certain device type only (such as PC, console, or tablet), you can choose a specific one here.

The info in all of the charts listed below will reflect the date range and any filters you've selected. Some sections also allow you to apply additional filters.


## Add-on acquisitions

The **Add-on acquisitions** chart shows the number of daily or weekly acquisitions of your add-ons over the selected period of time. (When you use **Apply filters** to show data for a longer duration, the acquisition data will be grouped by week.)

You can also see the lifetime number of acquisitions for your app by selecting **Add-on cumulative**. This shows the cumulative total of all acquisitions, starting from when your app was first published.

You can optionally filter the results by whether the add-on acquisition originated from the client or web-based Store and/or by OS version.


## Customer demographic

The **Customer demographic** chart shows demographic info about the people who acquired your add-ons. You can see how many acquisitions (over the selected period of time) were made by people in a certain age group and by which gender.

> [!NOTE]
> Some customers have opted not to share this info. If we were unable to determine the age group or gender, the acquisition is categorized as **Unknown**.


## Markets

The **Markets** chart shows the total number of add-on acquisitions over the selected period of time for each market in which your app is available. 

You can view this data in a visual **Map** form, or toggle the setting to view it in **Table** form. Table form will show five markets at a time, sorted either alphabetically or by highest/lowest number of acquisitions or installs. You can also download the data to view info for all markets together.


## Add-on page views and conversions by campaign ID

The **Add-on page views and conversions by campaign ID** chart shows you the total number of add-on conversions (acquisitions) per campaign ID over the selected period of time, helping you track conversions and page views from customers on Windows 10 (including Xbox) for each of your [custom promotion campaigns](create-a-custom-app-promotion-campaign.md). Only add-on conversions are shown in this chart.

> [!NOTE]
> Customers could arrive at your app's listing by clicking on a custom campaign not created by you. We stamp every page view within a session with the campaign ID from which the customer first entered the Store. We then attribute conversions to that campaign ID for all acquisitions within 24 hours. Because of this, you may see a higher number of total conversions than the total conversions for your campaign IDs, and you may have conversions or add-on conversions that have zero page views. 


## Conversions breakdown by campaign ID

The **Conversions breakdown by campaign ID** chart lets you track conversions and page views from customers on Windows 10 for each of your [custom promotion campaigns](create-a-custom-app-promotion-campaign.md). Both app and add-on conversions are shown per campaign ID.

In this chart, a *page view* means that a customer viewed the app's Store listing. A *conversion* means that a customer has newly obtained a license for the app or add-on (whether you charged money or you've offered it for free).

Keep in mind that these page views and conversion numbers are not counts of unique customers. 


## Top add-ons

The **Top add-ons** chart shows the total number of acquisitions for each of your add-ons over the selected period of time, so you can see which of your add-ons are the most popular. 



 

 
