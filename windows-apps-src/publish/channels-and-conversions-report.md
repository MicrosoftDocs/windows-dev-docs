---
author: shawjohn
Description: The Channels and conversions report in the Windows Dev Center dashboard lets you see how customers on Windows 10 have arrived at your app's listing.
title: Channels and conversions report
ms.assetid: C359B9FB-A17B-4A8E-B8EE-19F2F98AA4FF
ms.author: johnshaw
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, channels, conversions, report, analytics
---

# Channels and conversions report


The **Channels and conversions** report in the Windows Dev Center dashboard lets you see how customers on Windows 10 have arrived at your app's listing. It lets you track [custom promotion campaigns](create-a-custom-app-promotion-campaign.md) for your app or its add-ons, and see how many of those visits resulted in new acquisitions. You can view this data in your dashboard, or [download the report](download-analytic-reports.md) to view offline.

> **Important**   This report only shows page view and conversion data from customers on Windows 10.

 

In this report, a *channel* refers to the method in which a customer arrived at your app's listing page (for example, browsing and searching in the Store, a link from an external website, a link from one of your custom campaigns, etc.). The following channel types are included:

-   **Store traffic:** The customer was browsing or searching within the Store when they viewed your app's listing.
-   **Custom campaign:** The customer followed a link that used a [custom campaign ID](create-a-custom-app-promotion-campaign.md).
-   **Other:** The customer followed an external link (without any custom campaign ID) from a website to your app's listing or the customer followed a link from a search engine to your app's listing.

A *page view* means that a customer viewed your app's Store listing page, either via the web-based Store or from within the Store app on Windows 10.

A *conversion* means that a customer has newly obtained a license to your app (whether you charged money or you've offered it for free) or to an add-on.

We don't display a conversion rate in this report because our page views and conversion numbers are not counts of unique customers.

Conversion data is provided only for your custom campaigns. For other channel types, only page view data is included in this report.

> **Note**  Customers might arrive at your app's listing by clicking on a custom campaign not created by you. To account for this possibility, we stamp every page view within a session with the campaign ID from which the user first enters the Store. We then attribute a conversion of your app to that campaign ID if an acquisition of your app occurs within 24 hours. When you view your report, this is why you might see conversions attributed to campaigns that are unfamiliar to you, why you may see a higher number of total conversions than in the conversions breakdown, and why you may have conversions or add-on conversions that have zero page views. You can look at the conversions breakdown by campaign ID to see only the conversions attributed to campaigns created by you to evaluate their effectiveness.


## Apply filters


Near the top of the page, you can expand **Apply filters** to filter all of the data on this page by date range and/or by market.

-   **Date**: The default filter is **Last 30 days**, but you can expand this up to **Last 12 months**.
-   **Market**: The default setting is **All markets**. You can choose a specific market if you want this page to only show details from customers in that market.
-   **Device type**: The default filter is **All devices**. You can choose a specific device type if you want this page to only show data from customers using that type of device.

The info in all of the charts listed below will reflect the period of time selected in the **Apply filters** section, and will reflect any other filters you've chosen here.

## App page views and conversions by channel


The **App page views and conversions by channel** chart shows how often your app's listing page was viewed and how customers arrived there. It also shows the number of conversions from custom campaigns over the selected period of time.

The **Page views** tab of this chart shows the number of times your app's listing page was viewed over the selected period of time. Views are grouped according to the type of channel by which the customer found your app's listing.

The **Conversions** tab of this chart shows the number of conversions (new acquisitions) over the selected period of time for customers who arrived at your app's listing via a custom campaign.

For info about all of your app's acquisitions, including those that did not occur via a custom campaign link and those from customers on other OS versions, see the [Acquisitions report](acquisitions-report.md).

 

## Total campaign conversions


The **Total campaign conversions** chart shows the total number of app and add-on conversions from custom campaigns during the selected period of time.

## App page views and conversions by campaign ID


The **App page views and conversions by campaign ID** chart shows the number of page views and conversions for all [campaign IDs](create-a-custom-app-promotion-campaign.md) during the selected period of time.

##  Add-on conversions by campaign ID


The **Add-on conversions by campaign ID** chart shows the number of add-on conversions per custom campaign ID.

When an app install is counted as a conversion for a custom campaign, any add-on purchases in that app are also counted as conversions for the same custom campaign.

By default, the report includes any add-on which had a conversion that resulted from a link using a custom campaign ID during the selected period of time. To view data for a specific add-on, select it from the **Section filters**.

## Conversions breakdown by campaign ID


The **Conversions breakdown** chart shows the following details about the page views and conversions that resulted from custom campaigns.

-   **ID:** Shows the specific campaign IDs.
-   **Page views:** Shows the count of page views stamped with the campaign ID of the customer's first entry to the Store.
-   **App conversions:** Shows the count of app conversions resulting from the custom campaign.
-   **Add-on conversions:** Shows the count of add-on conversions resulting from the custom campaign.


 

 
