---
Description: The Xbox analytics report in Partner Center shows you statistics about how your customers are engaging with the Xbox features in your product.
title: Xbox analytics report
ms.date: 03/21/2019
ms.topic: article
keywords: windows 10, uwp, xbox analytics, xbox live analytics, xbox statistics
ms.localizationpriority: medium
---
# Xbox analytics report

The **Xbox analytics** report in [Partner Center](https://partner.microsoft.com/dashboard) shows you statistics about how your customers are engaging with the Xbox features in your game. It also provides service health info to help you address client errors.

> [!IMPORTANT]
> You’ll only see this report if you’re publishing a game for Xbox or a game that uses Xbox Live services. To do so, you must go through the [concept approval process](../gaming/concept-approval.md), which includes games published by [Microsoft partners](/gaming/xbox-live/developer-program-overview#microsoft-partners) and games submitted via the [ID@Xbox program](/gaming/xbox-live/developer-program-overview#id). Games published via the [Xbox Live Creators Program](/gaming/xbox-live/get-started-with-creators/get-started-with-xbox-live-creators) are not currently visible in this report.

You can view the **Xbox analytics** report from the left navigation menu for your game by expanding **Analyze** and selecting **Xbox analytics**.  You can view this data in Partner Center, or [download the report](download-analytic-reports.md) to view offline.


## Overview tab

The sections on the **Overview** tab shows info about who your players are and how they're engaging with Xbox Live features.

For many of these statistics, we also show the **Xbox average** so you can easily see how your customers interact with Xbox compared to the average Xbox customer.

> [!NOTE]
> These statistics are from customers who are connected to Xbox Live, not all Xbox customers.


### Concurrent usage

This section shows near real time usage data (with 5-15 minutes latency) about the average number of customers playing your game each minute or hour. You can choose the time range (from **Last hour** up to **Last 7 days**) by selecting the filter icon in the top right corner of this section.


### Gamerscore distribution

This section shows info about the gamerscore of your customers. You can select **All games** to see the distribution of total gamerscore across your customers, or select **This game** to view the distribution of gamerscore obtained through your game only.


### Achievement unlocks

This section shows the total number of customers who have unlocked each achievement in the specified time range. You can choose the time range (**Last day**, **Last 30 days**, or **Lifetime**) by selecting the filter icon in the top right corner of this section.


### Game statistics

This section includes tabs that you can select to show different data for your game's customers. Note that the statistics in this section refer to feature usage in general and not within your specific product.

- The **Social usage** tab shows data related to how your customers interact socially.
   - **Game invites** shows the percentage of your customers who have sent out invites (for any game).
   - **Party chat** shows the percentage of your customers who use party chat (for any game).
   - **Text messages** shows the percentage of your customers who send messages through the Xbox shell (for any game).
- The **Streaming usage** tab shows the percentages of your game's customers who watch or stream gameplay (for any game) on Twitch and YouTube.
- The **Game DVR usage** tab shows data related to how your customers record and view gameplay. You can see the percentages of customers who have viewed and uploaded game clips and screenshots of gameplay (for any game).


### Friends and followers

This section shows the **Median number of friends** and **Median number of followers** for the customers playing your game.


### Accessory usage

This chart shows the percentages of your game's customers who use external hard drives and who use Xbox Elite Wireless Controllers (on Xbox).

This data does not mean that these customers who installed your product on external hard drives or used an Elite Controller while playing it. It refers to how many of your product's customers use these features in general.


### Connection type

This chart shows the percentages of your product’s customers who use **Wired** internet connections versus **Wireless** (on Xbox).


## Xbox Live service health tab

The sections on the **Xbox Live service health tab** help you understand the impact of any Xbox Live client errors, including rate limiting. It also lets you drill down by endpoint and status code to get info that helps you resolve these issues, and keeps you informed about Xbox Live service availability specific to your product’s calls.

> [!NOTE]
> When reviewing this info and addressing issues, we recommend prioritizing rate limiting, as those errors usually have the greatest customer impact.


### Apply filters

Near the top of the tab, you can select the time period for which you want to show data. The default selection is **30D** (30 days), but you can choose to show data for **7D** (7 days) or a custom date range that you specify (of no more than 30 days). For a custom date range, note that all of the charts will trim the chart range to the first and last day of data provided within the date range you enter.

You can also expand **Filters** to filter all of the data on this page by package version, device type, and/or sandbox.
- **Package version**: The default filter is **All versions**, but you can limit the service health data to a specific package version.
- **Device type**: The default setting is **All devices**, but you can limit the service health data to a specific device type.
- **Sandbox**: The default setting is **RETAIL**, but you can limit the service health data to a specific sandbox.

The info in all of the charts listed below will reflect the date range and any filters you've selected. Some sections also allow you to apply additional filters.


### Client errors by service

The **Client errors by service** chart shows the number of daily client (4xx) errors across each Xbox Live service over the selected period of time.

You can also view only rate limiting errors by selecting **Rate limiting**. This shows the number of daily rate limiting (429) and rate limiting exempt (429E) errors across each Xbox Live service over the selected period of time.

> [!NOTE]
> A 429E status code was actually successfully returned as a 200 status code, but would have been rate-limited if the service was experiencing high volume at the time, so we recommend you treat it exactly the same as if it were enforced (429).

By default, this chart displays the top six services by error count. You can select the filter icon in the top right corner of this section to choose different services. You can view errors for up to six services at once.

> [!NOTE]
> The legend only displays the distinguishing prefix for each service (for example, **presence** instead of **presence.xboxlive.com**). You can find the full service address in the **Client errors by endpoint** table lower in the **Xbox Live service health** tab.


### Service availability

The **Service availability** chart shows the daily availability across each Xbox Live service over the selected period of time. This is calculated as *1-(total server (5xx) errors/total responses)*, and is specific to your product, not Xbox Live as a whole.

By default, this chart displays the six services which have experienced the lowest availability. You can select the filter icon in the top right corner of this section to choose different services. You can view availability for up to six services at once.

> [!NOTE]
> The legend only displays the distinguishing prefix for each service (for example, **presence** instead of **presence.xboxlive.com**). You can find the full service address in the **Client errors by endpoint** table lower in the **Xbox Live service health** tab.


### Client errors by endpoint

The **Client errors by endpoint** table shows the number of daily client (4xx) errors broken down by each Xbox Live service, endpoint, and status code over the selected period of time. By default, the table is sorted by the total number of service responses in descending order, but you can change the sorting order by clicking any of the column headers.

You can also view only rate limiting errors by selecting **Rate limiting**. This shows the number of daily rate limiting (429) and rate limiting exempt (429E) errors across each Xbox Live service, endpoint, and status code over the selected period of time.

> [!NOTE]
> A 429E status code was actually successfully returned as a 200 status code, but would have been rate-limited if the service was experiencing high volume at the time, so we recommend you treat it exactly the same as if it were enforced (429).










 

 