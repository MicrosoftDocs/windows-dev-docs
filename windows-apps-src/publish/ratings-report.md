---
Description: The Ratings report in Partner Center lets you see how customers rated your app in the Store.
title: Ratings report
ms.date: 10/31/2018
ms.topic: article
keywords: windows 10, uwp, rating, rate, star, stars, rated
ms.localizationpriority: medium
---
# Ratings report


The **Ratings** report in [Partner Center](https://partner.microsoft.com/dashboard) lets you see how customers rated your app in the Store. 

You can view this data in Partner Center, or [download the report](download-analytic-reports.md) to view offline. Alternatively, you can programmatically retrieve this data by using the [get app reviews](../monetize/get-app-reviews.md) method in the [Microsoft Store analytics REST API](../monetize/access-analytics-data-using-windows-store-services.md).

> [!TIP]
> For a quick look at the reviews, ratings, and user feedback for all of your apps in the last 30 days, expand **Engage** in the left navigation menu and select **Reviews and feedback.** 

## Apply filters

Near the top of the page, you can select the time period for which you want to show reviews. The default selection is **Lifetime**, but you can choose to show reviews for 30 days, 3 months, 6 months, or 12 months, or for a custom data range that you specify. Note that the **Ratings breakdown** and **Average rating over time** charts will always show data for the past 12 months; these time period options will not affect those charts.

You can expand **Filters** to filter the reviews shown on this page by the following options. These filters will not apply to the **Ratings breakdown** and **Average rating over time** charts.

-   **Market**: The default setting is **All markets**. You can choose a specific market if you want this page to only show ratings from customers in that market.
-   **Device type**: The default filter is **All devices**. You can choose a specific device type if you want this page to only show ratings left by customers using that type of device.
-   **OS version**: The default setting is **All**. You can choose a specific OS version if you want this page to only show ratings left by customers on that OS version.
-   **Category name**: The default filter is **All**. You can choose to only show ratings associated with reviews that we've identified as belonging to a specific [review insight category](reviews-report.md#insight-categories) to only show reviews that we’ve associated with that category. 

> [!TIP]
> If you don't see any ratings on the page, check to make sure your filters haven't excluded all of your ratings. For example, if you filter by a Target OS that your app doesn't support, you won't see any ratings.


## Rating breakdown

The **Rating breakdown** chart shows: 
- The average rating star rating for the app.
- The total number of ratings of your app over the past 12 months.
- The total number of ratings for each star rating.
- The number of ratings for each type of rating (new or revised) per star rating over the past 12 months.
 - **New ratings** are ratings that customers have submitted but haven't changed at all.
 - **Revised ratings** are ratings that have been changed by the customer in any way, even just changing the text of the review.

> [!TIP]
> The average rating that a customer sees in the Store takes into account the customer’s market and device type, so it may differ from what you see in this report.


## Average rating

The **Average rating** chart shows how the app's average rating has changed over the past 12 months.

Rather than calculating the average of all ratings left during the past 12 months (as in the **Ratings breakdown** chart), the **Average rating** chart shows you how customers rated the app on a given week. This can help you identify trends or determine if ratings were affected by updates or other factors.

## Rating by market

The **Rating by market** chart shows a breakdown of the average ratings in each market over the time period selected. By default, we show this data in a visual **Map** form, but you can also toggle the control in the upper right corner to view it as a **Table**.

The **Table** view shows five markets at a time, sorted either alphabetically or by highest/lowest average rating. You can also download this chart's data to view info for all markets together.

You can also filter this chart by **Rating**. By default reviews with all star ratings are checked, but you can check and uncheck specific ratings (from 1 to 5 stars) if you want to only see reviews associated with particular star ratings.