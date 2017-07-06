---
author: jnHs
Description: You can cross-promote your app with apps published by other developers. We call this feature community ads.
title: About community ads
ms.assetid: F55CE478-99AF-4B70-90D1-D16419562136
ms.author: wdg-dev-content
ms.date: 07/05/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# About community ads

If your app [displays banner or banner interstitial ads](../monetize/display-ads-in-your-app.md), you can cross-promote your app with other developers with apps in the Windows Store for free. We call this feature *community ads*.  

Here's how this program works:

* After you [opt-in to community ads](#how-to-opt-in-to-community-ads) and [create a free community ad campaign](create-an-ad-campaign-for-your-app.md), your app will share promotional ad space with other developers who also opt in to community ads. Your app will show ads for apps published by other developers who participate in community ads, and their apps will show ads for your app.
* You earn credits for promotional ad space in other apps by showing community ads in your app. Credits are calculated according to the following process:
  * For each country or region where an app that is serving community ads is available, the current market-rate eCPM (effective cost per thousand impressions) value for the country or region is multiplied by the number of requests for community ads made by your app in that country or region. This value is the credits you have earned for your app in that country or region.
  * Your total credits earned for a given time period is equal to the sum of all credits earned in each country or region for each of your apps that is serving community ads.
* Your credits are divided equally across all active community ad campaigns, and are converted to ad impressions for your app based on the current market-rate eCPM values of the countries your community ad campaigns target.
* To track the performance of the community ads in your app, refer to the [advertising performance report](advertising-performance-report.md).

### Opt in to community ads

Before you can create a community ad campaign for one of your apps, you must opt in on the **Monetization** &gt; **Monetize with ads** page for the app in the Windows Dev Center dashboard.

To opt in, do one of the following:
  * If your app is a UWP app that targets Windows 10, go to the **Ad mediation** section on the page and check the **Microsoft Community ads** box in the **Other ad networks** list.
  * If your app targets Windows 8.x or Windows Phone 8.x, go to the **Community ads** section on the page and check the **Show community ads in my app** box.

You do not need to republish your app after making your selections. Once you've opted in, you'll be able to select **Community ad (free)** as the campaign type when you [create an ad campaign](create-an-ad-campaign-for-your-app.md).

### Related topics

* [Monetize with ads](monetize-with-ads.md)
* [Create an ad campaign for your app](create-an-ad-campaign-for-your-app.md)
