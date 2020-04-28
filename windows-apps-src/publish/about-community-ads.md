---
Description: You can cross-promote your app with apps published by other developers. We call this feature community ads.
title: About community ads
ms.assetid: F55CE478-99AF-4B70-90D1-D16419562136
ms.date: 02/18/2020
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# About community ads

>[!WARNING]
> As of June 1, 2020, the Microsoft Ad Monetization platform for Windows UWP apps will be shut down. [Learn more](https://social.msdn.microsoft.com/Forums/windowsapps/en-US/db8d44cb-1381-47f7-94d3-c6ded3fea36f/microsoft-ad-monetization-platform-shutting-down-june-1st?forum=aiamgr)

If your app [displays banner or banner interstitial ads](../monetize/display-ads-in-your-app.md), you can cross-promote your app with other developers with apps in the Microsoft Store for free. We call this feature *community ads*.  

Here's how this program works:

* After you opt-in to community ads as described below, you can [create a free community ad campaign](create-an-ad-campaign-for-your-app.md). Your app will then share promotional ad space with other developers who also opt in to community ads. Your app will show ads for apps published by other developers who participate in community ads, and their apps will show ads for your app.
* You earn credits for promotional ad space in other apps by showing community ads in your app. Credits are calculated according to the following process:
  * For each country or region where an app that is serving community ads is available, the current market-rate eCPM (effective cost per thousand impressions) value for the country or region is multiplied by the number of requests for community ads made by your app in that country or region. This value is the credits you have earned for your app in that country or region.
  * Your total credits earned for a given time period is equal to the sum of all credits earned in each country or region for each of your apps that is serving community ads.
* Your credits are divided equally across all active community ad campaigns, and are converted to ad impressions for your app based on the current market-rate eCPM values of the countries your community ad campaigns target.
* To track the performance of the community ads in your app, refer to the [advertising performance report](advertising-performance-report.md).

### Opt in to community ads

Before you can create a community ad campaign for one of your apps, you must opt in on the **Monetize** &gt; **In-app ads** page in [Partner Center](https://partner.microsoft.com/dashboard).

To opt in to community ads for a UWP app:

1. Select an ad unit that you are using in the app and scroll down to **Mediation settings**.
2. If **Let Microsoft optimize my settings** is selected, community ads are enabled for your ad unit automatically. Otherwise, select the baseline configuration or a market-specific configuration in the **Target** drop-down and then check the **Microsoft Community ads** box in the **Other ad networks** list.

    > [!NOTE]
    > You can use the **Weight** fields to specify the ratio of ads you want to show from paid networks and other ad networks including community ads.

You do not need to republish your app after making your selections. Once you've opted in, you'll be able to select **Community ad (free)** as the campaign type when you [create an ad campaign](create-an-ad-campaign-for-your-app.md).

### Related topics

* [In-app ads](in-app-ads.md)
* [Create an ad campaign for your app](create-an-ad-campaign-for-your-app.md)
