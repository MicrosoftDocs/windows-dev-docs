---
author: mcleanbyron
ms.assetid: bb105fbe-bbbd-4d78-899b-345af2757720
description: Learn how to add application ID and ad unit ID values from the Windows Dev Center dashboard to your app before you submit your app to the Store.
title: Set up ad units in your app
ms.author: mcleans
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, ads, advertising, ad units
---

# Set up ad units in your app

When you use an [AdControl](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.adcontrol.aspx) or [InterstitialAd](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.interstitialad.aspx) to display ads in your app, you must specify an application ID and ad unit ID. While you are developing your app, use the appropriate [test application ID and ad unit ID values](test-mode-values.md) to see how your app renders ads during testing.

After you finish testing your app and you are ready to submit it to Windows Dev Center, you must update your app code to use application ID and ad unit ID values from the [Windows Dev Center dashboard](https://msdn.microsoft.com/library/windows/apps/mt170658.aspx). If you try to use test values in your live app, your app will not receive live ads.

To set up the application ID and ad units for your live app:

1.  On the Windows Dev Center dashboard, select your app and then click **Monetization > Monetize with ads**.

2.  In the **Create ad units** section, enter a name for the ad unit in the **Ad unit name** field.

3. In the **Ad unit type** drop-down, select the type of ad unit that corresponds to the ads you are showing in your control:

    -   If you are using an **AdControl** in your app to show banner ads, select **Banner** for the ad unit type.

    -   If you are using an **InterstitialAd** in your app to show interstitial video or interstitial banner ads, select **Video interstitial** or **Banner interstitial** (be sure to select the appropriate option for the type of interstitial ad you want to show).

4.  In the **Device family** drop-down, select the device family targeted by the app in which your ad unit will be used. The available options are: **UWP (Windows 10)**, **PC/Tablet (Windows 8.1)**, or **Mobile (Windows Phone 8.x)**.

5.  Click **Create ad unit**. The new ad unit appears at the top of the list in the **Available ad units** section on this page.

6.  For each generated ad unit, you will see an **Application ID** and an **Ad unit ID**. To show ads in your app, you'll need to use these values in your app's code:

    -   If your app shows banner ads, assign these values to the [ApplicationId](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.adcontrol.applicationid.aspx) and [AdUnitId](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.adcontrol.adunitid.aspx) properties of your **AdControl** object. For more information, see [AdControl in XAML and .NET](adcontrol-in-xaml-and--net.md) and [AdControl in HTML 5 and Javascript](adcontrol-in-html-5-and-javascript.md).

    -   If your app shows interstitial ads, pass these values to the [RequestAd](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.interstitialad.requestad.aspx) method of your **InterstitialAd** object. For more information, see [Interstitial ads](interstitial-ads.md).

7. If your app is a UWP app for Windows 10, you can optionally enable ad mediation for the **AdControl** by configuring the settings in the **Ad mediation** section. Ad mediation enables you to maximize your ad revenue and app promotion capabilities by displaying ads from multiple ad networks, including ads from other paid ad networks such as Taboola and Smaato and ads for Microsoft app promotion campaigns. By default, we automatically choose the ad mediation settings for your app using machine-learning algorithms to help you maximize your ad revenue across the markets your app supports, but you can optionally manually configure your mediation settings.

For more information about the **Monetize with ads** page in the dashboard, see [Monetize with ads](../publish/monetize-with-ads.md).

## Related topics

* [Test mode values](test-mode-values.md)
* [AdControl in XAML and .NET](adcontrol-in-xaml-and--net.md)
* [AdControl in HTML 5 and Javascript](adcontrol-in-html-5-and-javascript.md)
* [Interstitial ads](interstitial-ads.md)


 

 
