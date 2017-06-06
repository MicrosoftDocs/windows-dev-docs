---
author: mcleanbyron
ms.assetid: fcebd659-438b-4d03-bc73-6b662ed6f1f3
description: Learn about the end-to-end process for developing and publishing an app with ads.
title: Workflows for creating apps with ads
ms.author: mcleans
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, ads, advertising, AdControl, InterstitialAd
---

# Workflows for creating apps with ads

To display ads in your apps, your app needs to be able receive ads from an ad network. Microsoft provides a web service that allows Windows app developers to receive ads. When users click the ad in your app, you (being the *publisher* of the ad) earn money from the creator of the ads (the *advertiser*). The money earned from advertisers is paid to you using your account.

The following high-level steps describe the general process of developing and publishing an app with ads.

1.  Development stage:

    * Set up your Windows Dev Center account.
    * Develop your app using test mode advertising values.

2.  Ready to release:

    * Configure your app to receive live ads.
    * Submit your app to Windows Dev Center and review performance data.

For more information about each step, read its corresponding section below.

## Set up your Windows Dev Center account

You need to have an account with Windows Dev Center to publish your app and receive ads. Advertising-related app management is also done in Windows Dev Center. If you have used Microsoft pubCenter to manage advertising in your apps, this has been replaced by the **Monetize with ads** page in Windows Dev Center.

To set up your account with Windows Dev Center, visit [this page](http://go.microsoft.com/fwlink/p/?LinkId=615100).

## Develop your app using test mode values

Use the instructions in the following walkthroughs to add an [AdControl](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.adcontrol.aspx) or [InterstitialAd](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.interstitialad.aspx) to display ads in your app:

-   [Interstitial Ads](interstitial-ads.md)
-   [Native ads](native-ads.md)
-   [AdControl in XAML and .NET](adcontrol-in-xaml-and--net.md)
-   [AdControl in HTML 5 and Javascript](adcontrol-in-html-5-and-javascript.md)
-   [AdControl in Windows Phone Silverlight](adcontrol-in-windows-phone-silverlight.md)

When you display ads in your app, you must specify an application ID and ad unit ID in your code to link your app to your Windows Dev Center account and to serve ads. While you are developing your app, use test application ID and ad unit ID values to see how your app renders ads during testing. This enables you to see how the app is receiving and rendering advertisements during testing. For more information, see [Test mode values](test-mode-values.md).

For complete sample projects that demonstrate how to add banner and interstitial ads to JavaScript/HTML apps and XAML apps using C# and C++, see the [advertising samples on GitHub](http://aka.ms/githubads).

## Configure your app to receive live ads

After you finish testing your app and you are ready to submit it to Windows Dev Center, you must retrieve application ID and ad unit ID values from the **Monetize with ads** page for your app in the Windows Dev Center dashboard and then update your app code to use these values. If you try to use test values in your live app, your app will not receive live ads. For more information, see [Set up ad units in your app](set-up-ad-units-in-your-app.md) and [Monetize with ads](../publish/monetize-with-ads.md).

If your app is a UWP app for Windows 10, you can maximize your ad revenue and app promotion capabilities by enabling ad mediation to display ads from multiple ad networks. To start using ad mediation, go to the **Monetize with ads** page for your app in the dashboard and configure the ad mediation settings for the ad units you are using in your app. For more information, see [Monetize with ads](../publish/monetize-with-ads.md).

## Submit your app and review performance

After you complete development of your app, you can publish your app in the Windows Store by using the Windows Dev Center dashboard. In addition to meeting requirements for all apps in the Windows Store, apps that display ads must meet several additional requirements. For more information, see [Submit an app with ads to the Windows Store](submit-an-app-with-ads-to-the-windows-store.md).

After your app is published and available in the Windows Store, you can review your [advertising performance reports](../publish/advertising-performance-report.md) in the Dev Center dashboard.
