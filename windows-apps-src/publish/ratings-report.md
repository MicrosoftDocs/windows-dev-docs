---
author: jnHs
Description: The Ratings report in the Windows Dev Center dashboard lets you see the distribution of how customers rate your app in the Windows Store.
title: Ratings report
ms.assetid: CAFEC20B-04FB-48C8-B663-1238C0B85ECD
ms.author: wdg-dev-content
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Ratings report


The **Ratings** report in the Windows Dev Center dashboard lets you see the distribution of how customers rate your app in the Windows Store. You can view this data in your dashboard, or [download the report](download-analytic-reports.md) to view offline. Alternatively, you can programmatically retrieve this data by using the [get app ratings](../monetize/get-app-ratings.md) method in the [Windows Store analytics REST API](../monetize/access-analytics-data-using-windows-store-services.md).

In this report, a rating means the number of stars (from 1 to 5) that a customer gave your app when rating it in the Store. The **Ratings** report does not include info on any individual comments left as reviews; those are available in the [Reviews report](reviews-report.md).

## Apply filters


Near the top of the page, you can expand **Apply filters** to filter all of the data on this page by date range and/or by market.

-   **Date**: The default filter is **Last 30 days**, but you can expand this up to **Last 12 months**.
-   **Market**: The default filter is **All markets**. You can choose a specific market if you want this page to only show ratings from customers in that market.
-   **Device type**: The default filter is **All devices**. You can choose a specific device type if you want this page to only show ratings left by customers using that type of device.

The info in all of the charts listed below will reflect the period of time selected in the **Apply filters** section, and will reflect any other filters you've chosen here.

## Average rating


The **Average rating** chart shows the average rating of your app over the selected period of time.

## Number of ratings


The **Number of ratings** chart shows the total number of ratings of your app over the selected period of time.

## New and revised ratings


The **New and revised ratings** chart shows the number of ratings for each type of rating (new or revised) over the selected period of time.

-   **New ratings** are ratings that customers have submitted but haven't changed.
-   **Revised ratings** are ratings that have been changed by the customer.

>**Note**  A rating will appear here as revised even if the customer only changed or added the text or title of their review and left the actual rating the same.

## Average rating over time


The **Average rating over time** chart shows how the app's average rating has changed over the selected period of time.

Rather than calculating the average of all ratings left during the selected period of time (as in the **Average rating** chart), the **Average rating over time** chart shows you how customers rated the app on a given day or week during the period. This helps you identify trends or determine if ratings were affected by updates or other factors.

If you have filtered the info by **Last 30 days** or **Last 3 months**, the chart displays your average rating by day. If you've filtered by **Last 6 months** or **Last 12 months**, the chart displays your average rating by week (with a new week considered to start on Monday; the average rating shown is for the previous week).

## Markets


The **Markets** chart shows average rating and number of ratings over the selected period of time by market.

> **Note**  If you have used the **Page filters** to specify a specific market, you won't see this chart on the **Ratings** report. To see this chart, change the **Page filters** to show all markets.

By default, we show you the market which had the most reviews and continue downward from there, but you can reverse this order by toggling the arrow in the **Number of ratings** column of this chart. You can also sort the data by **Average rating** or **Market** by clicking those columns.

> **Note**  It’s likely that you’ll see a different number of ratings when comparing the **Ratings** report in the Windows Dev Center with the Reviews report in the older Dev Center mobile app. This is because the app only shows data for reviews left from customers on Windows Phone 8.1 and earlier. This may also be a result of work by Microsoft to remove reviews from the Windows Store that have been identified as spam, inappropriate, offensive or have other policy violations. We expect this action will result in a better customer experience.

 

 
