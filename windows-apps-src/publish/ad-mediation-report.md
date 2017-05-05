---
author: jnHs
Description: The Ad mediation report lets you see your effective fill rate and the respective fill rates for the ad networks you're using.
title: Ad mediation report
ms.assetid: 18A33928-B9F2-4F76-9A9C-F01FEE42FEA1
ms.author: wdg-dev-content
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Ad mediation report

This report shows ad mediation data for Windows 8.x or Windows Phone 8.x apps that use an **AdMediatorControl** from the [Microsoft Advertising SDK for Windows and Windows Phone 8.x](http://aka.ms/store-8-sdk) to mediate banner ads from multiple ad networks. For these apps, this report lets you see your effective fill rate and the respective fill rates for the ad networks you're using. It also shows the adoption rates of each of your mediation configurations and provides visibility into errors reported by ad networks and the mediator. You can view this data in your dashboard, or [download the report](download-analytic-reports.md) to view offline.

> [!NOTE]
> The **Ad mediation** report only provides data if you are using an **AdMediatorControl** in your app. For more information, see [this article](https://msdn.microsoft.com/library/windows/apps/xaml/dn864359). For a UWP app that uses [ad mediation](monetize-with-ads.md#mediation) in an **AdControl** or **InterstitialAd** control, use the [Advertising performance report](advertising-performance-report.md) to review performance data for the ad networks.

## Page filters

Near the top of the page, you can expand **Page filters** to filter all of the data on this page by date range and/or by market.

-   **Date**: The default filter is **Last 30 days**, but you can expand this up to **Last 12 months**.
-   **Market**: The default setting is **All markets**. You can choose a specific market if you want this page to only show ratings from customers in that market.
-   **Platform**: The default setting is **All platforms**. If your app targets multiple platforms, you can choose a specific platform.

The info in all of the charts listed below will reflect the period of time selected in **Page filters**. By default this will include data from all of the markets and platforms in which your app is listed, unless you've used the **Page filters** to specify a specific market and/or platform.

## Ad mediation performance

The **Ad mediation performance** chart shows the average total fill rate over the selected period of time. This is the average fill rate across all user sessions, regardless of your mediation configuration or how often different ad networks were called.

You can click the **Mediation requests** heading to see the average number of individual mediation requests, or click **Ads delivered** to see the average total number of ads delivered.

## Ad provider fill rates

The **Ad provider fill rates** chart shows the average fill rate of each of your ad networks over the selected period of time.

Info for each ad network is shown together to help you compare each ad network's performance.

## Unique users per mediation configuration

The **Unique users per mediation configuration** chart shows the total number of unique users who received each version of your mediation configuration over the selected period of time.

## Errors by ad network

The **Errors by ad network** chart shows the total number of requests and errors for each of your ad networks, along with the percentage of requests that resulted in an error.

## Errors by type

The **Errors by type** chart shows the specific errors experienced by each ad network. It also shows the percentage of total errors for that network that a specific error represents, so you can get an idea of which errors are coming up frequently per ad network.

 

 
