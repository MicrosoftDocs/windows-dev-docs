---
author: mcleanbyron
ms.assetid: bb105fbe-bbbd-4d78-899b-345af2757720
description: Learn how to add application ID and ad unit ID values from the Windows Dev Center dashboard to your app before you submit your app to the Store.
title: Set up ad units in your app
ms.author: mcleans
ms.date: 08/23/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, ads, advertising, ad units, testing
---

# Set up ad units in your app

Every ad control in your Universal Windows Platform (UWP) app has a corresponding *ad unit* that is used by our services to serve ads to the control. Each ad unit consists of an *ad unit ID* and *application ID* that you must assign to the [AdControl](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.adcontrol.aspx),  [InterstitialAd](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.interstitialad.aspx), or [NativeAd](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.nativead.aspx) in your app.

We provide [test ad unit values](#test-ad-units) that you can use during testing to confirm that your app shows test ads. These test values can only be used in a test version of your app. If you try to use test values in your app after you publish it, your live app not receive ads.

After you finish testing your UWP app and you are ready to submit it to Windows Dev Center, you must [create a live ad unit](#live-ad-units) from the [Monetize with ads](../publish/monetize-with-ads.md) page in the Windows Dev Center dashboard and update your app code to use the application ID and ad unit ID values for this ad unit.

<span id="test-ad-units" />
## Test ad units

While you are developing your app, use the test application ID and ad unit ID values from this section to see how your app renders ads during testing.

### Banner ads (using the AdControl class)

* Ad unit ID: ```test```
* Application ID:  ```3f83fe91-d6be-434d-a0ae-7351c5a997f1```

    > [!IMPORTANT]
    > For an **AdControl**, the size of a live ad is defined by the **Width** and **Height** properties. For best results, make sure that the **Width** and **Height** properties in your code are one of the [supported ad sizes for banner ads](supported-ad-sizes-for-banner-ads.md). The **Width** and **Height** properties will not change based on the size of a live ad.


### Interstitial ads and native ads

* Ad unit ID: ```test```
* Application ID:  ```d25517cb-12d4-4699-8bdc-52040c712cab```

<span id="live-ad-units" />
## Live ad units

To get a live ad unit from the Dev Center dashboard and use it in your app:

1.  On the Windows Dev Center dashboard, select your app and then click **Monetization > Monetize with ads**.

2.  In the **Create ad units** section, enter a name for the ad unit in the **Ad unit name** field.

3. In the **Ad unit type** drop-down, select the type of ad unit that corresponds to the ads you are showing in your control:

    * If you are using an [AdControl](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.adcontrol.aspx) in your app to show banner ads, select **Banner**.

    * If you are using an [InterstitialAd](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.interstitialad.aspx) in your app to show interstitial video or interstitial banner ads, select **Video interstitial** or **Banner interstitial** (be sure to select the appropriate option for the type of interstitial ad you want to show).

    * If you are using a [NativeAd](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.nativead.aspx) in your app to show native ads, select **Native**.
        > [!NOTE]
        > The ability to create **Native** ad units is currently available only to select developers who are participating in a pilot program, but we intend to make this feature available to all developers soon. If you are interested in joining our pilot program, reach out to us at aiacare@microsoft.com.

4.  In the **Device family** drop-down, select **UWP (Windows 10)**.

5.  Click **Create ad unit**. The new ad unit appears at the top of the list in the **Available ad units** section on this page.

8.  For each generated ad unit, you will see an **Application ID** and an **Ad unit ID**. To show ads in your app, you'll need to use these values in your app's code:

    * If your app shows banner ads, assign these values to the [ApplicationId](https://msdn.microsoft.com/library/mt313174.aspx) and [AdUnitId](https://msdn.microsoft.com/library/mt313171.aspx) properties of your [AdControl](https://msdn.microsoft.com/library/mt313154.aspx) object. For more information, see [AdControl in XAML and .NET](../monetize/adcontrol-in-xaml-and--net.md) and [AdControl in HTML5 and JavaScript](../monetize/adcontrol-in-html-5-and-javascript.md).

    * If your app shows interstitial ads, pass these values to the [RequestAd](https://msdn.microsoft.com/library/mt313192.aspx) method of your [InterstitialAd](https://msdn.microsoft.com/library/mt313189.aspx) object. For more information, see [Interstitial ads](../monetize/interstitial-ads.md).

    * If your app shows native ads, pass these values to the *applicationId* and *adUnitId* parameters of the [NativeAdsManager](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.nativeadsmanager.nativeadsmanager.aspx) constructor. For more information, see [Native ads](../monetize/native-ads.md).

    > [!NOTE]
    > The application ID values for test ad units and live UWP ad units have different formats. Test application ID values are GUIDs. When you create a live UWP ad unit in the dashboard, the application ID value for the ad unit always matches the Store ID for your app (an example Store ID value looks like 9NBLGGH4R315).

7. You can optionally enable ad mediation for the ad unit by configuring the settings in the [Ad mediation](../publish/monetize-with-ads.md#mediation) section. Ad mediation enables you to maximize your ad revenue and app promotion capabilities by displaying ads from multiple ad networks, including ads from other paid ad networks such as Taboola and Smaato and ads for Microsoft app promotion campaigns. By default, we automatically choose the ad mediation settings for your app using machine-learning algorithms to help you maximize your ad revenue across the markets your app supports, but you can optionally manually configure your mediation settings.

<span id="manage" />
## Manage ad units for multiple ad controls in your app

You can use multiple banner, interstitial, and native ad controls in a single app. In this scenario, we recommend that you assign a different ad unit to each control. Using different ad units for each control enables you to separately [configure the mediation settings](../publish/monetize-with-ads.md#mediation) and get discrete [reporting data](../publish/advertising-performance-report.md) for each control. This also enables our services to better optimize the ads we serve to your app.

> [!IMPORTANT]
> You can use each ad unit in only one app. If you use an ad unit in more than one app, ads will not be served for that ad unit.

## Related topics

* [AdControl in XAML and .NET](adcontrol-in-xaml-and--net.md)
* [AdControl in HTML 5 and Javascript](adcontrol-in-html-5-and-javascript.md)
* [Interstitial ads](interstitial-ads.md)
* [Native ads](native-ads.md)


 

 
