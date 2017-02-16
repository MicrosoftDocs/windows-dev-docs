---
author: jnHs
Description: You can cross-promote your app with apps published by other developers. We call this feature community ads.
title: About community ads
ms.assetid: F55CE478-99AF-4B70-90D1-D16419562136
ms.author: wdg-dev-content
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# About community ads

If your app uses an **AdMediatorControl** or **AdControl** to display banner ads, you can cross-promote your app with other developers with apps in the Windows Store for free. We call this feature *community ads*.  

Here's how this program works:

* After you [opt-in to community ads](#how-to-opt-in-to-community-ads) and [create a free community ad campaign](create-an-ad-campaign-for-your-app.md), your app will share promotional ad space with other developers who also opt in to community ads. Your app will show ads for apps published by other developers who participate in community ads, and their apps will show ads for your app.
* You earn credits for promotional ad space in other apps by showing community ads in your app. Credits are calculated according to the following process:
  * For each country or region where an app that is serving community ads is available, the current market-rate eCPM (effective cost per thousand impressions) value for the country or region is multiplied by the number of requests for community ads made by your app in that country or region. This value is the credits you have earned for your app in that country or region.
  * Your total credits earned for a given time period is equal to the sum of all credits earned in each country or region for each of your apps that is serving community ads.
* Your credits are divided equally across all active community ad campaigns, and are converted to ad impressions for your app based on the current market-rate eCPM values of the countries your community ad campaigns target.
* To track the performance of the community ads in your app, refer to the [account-level advertising performance report](advertising-performance-report.md#account-level-advertising-performance-report).

## How to opt in to community ads

To opt in to community ads:

1. Go to the **Monetization** &gt; **Monetize with ads** page in the Windows Dev Center dashboard.
2. In the **Community ads** section, check the **Show community ads in my app** box.
   > **Note**  After you check or uncheck this box, you do not need to republish your app for the changes to take effect.

3. [Create an ad campaign](create-an-ad-campaign-for-your-app.md) for your app. For the campaign type, select **Free community ads**.


## Related topics

* [Monetize with ads](monetize-with-ads.md)
* [Create an ad campaign for your app](create-an-ad-campaign-for-your-app.md)
