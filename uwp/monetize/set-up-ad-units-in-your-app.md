---
ms.assetid: bb105fbe-bbbd-4d78-899b-345af2757720
description: Learn how to add application ID and ad unit ID values from Partner Center to your app before you submit your app to the Store.
title: Set up ad units in your app
ms.date: 02/18/2020
ms.topic: article
keywords: windows 10, uwp, ads, advertising, ad units, testing
ms.localizationpriority: medium
---
# Set up ad units in your app

>[!WARNING]
> As of June 1, 2020, the Microsoft Ad Monetization platform for Windows UWP apps will be shut down. [Learn more](https://social.msdn.microsoft.com/Forums/windowsapps/en-US/db8d44cb-1381-47f7-94d3-c6ded3fea36f/microsoft-ad-monetization-platform-shutting-down-june-1st?forum=aiamgr)

Every ad control in your Universal Windows Platform (UWP) app has a corresponding *ad unit* that is used by our services to serve ads to the control. Each ad unit consists of an *ad unit ID* and *application ID* that you must assign to code in your app.

We provide [test ad unit values](#test-ad-units) that you can use during testing to confirm that your app shows test ads. These test values can only be used in a test version of your app. If you try to use test values in your app after you publish it, your live app not receive ads.

After you finish testing your UWP app and you are ready to submit it to Partner Center, you must [create a live ad unit](#live-ad-units) from the [In-app ads](/windows/apps/publish/in-app-ads) page in Partner Center and update your app code to use the application ID and ad unit ID values for this ad unit.

For more information about assigning the application ID and ad unit ID values in your app's code, see the following articles:
* [AdControl in XAML and .NET](adcontrol-in-xaml-and--net.md)
* [AdControl in HTML 5 and JavaScript](adcontrol-in-html-5-and-javascript.md)
* [Interstitial ads](../monetize/interstitial-ads.md)
* [Native ads](../monetize/native-ads.md)

<span id="test-ad-units"></span>

## Test ad units

While you are developing your app, use the test application ID and ad unit ID values from this section to see how your app renders ads during testing.

### Banner ads (using the AdControl class)

* Ad unit ID: ```test```
* Application ID:  ```00001111-aaaa-2222-bbbb-3333cccc4444```

    > [!IMPORTANT]
    > For an **AdControl**, the size of a live ad is defined by the **Width** and **Height** properties. For best results, make sure that the **Width** and **Height** properties in your code are one of the [supported ad sizes for banner ads](supported-ad-sizes-for-banner-ads.md). The **Width** and **Height** properties will not change based on the size of a live ad.

### Interstitial ads and native ads

* Ad unit ID: ```test```
* Application ID:  ```11112222-bbbb-3333-cccc-4444dddd5555```

<span id="live-ad-units"></span>

## Live ad units

To get a live ad unit from Partner Center and use it in your app:

1.  [Create an ad unit](/windows/apps/publish/in-app-ads#create-ad-unit) on the **In-app ads** page in Partner Center. Be sure to specify the correct type of ad unit for the ad control you are using in your app.
    > [!NOTE]
    > You can optionally enable ad mediation for your ad unit by configuring the settings in the [Mediation settings](/windows/apps/publish/in-app-ads#mediation) section. Ad mediation enables you to maximize your ad revenue and app promotion capabilities by displaying ads from multiple ad networks, including ads from other paid ad networks and ads for Microsoft app promotion campaigns. By default, we automatically choose the ad mediation settings for your app using machine-learning algorithms to help you maximize your ad revenue across the markets your app supports, but you can optionally manually configure your mediation settings.

2.  After you create the new ad unit, retrieve the **Application ID** and **Ad unit ID** for the ad unit in the table of available ad units in the **Monetize** &gt; **In-app ads** page.
    > [!NOTE]
    > The application ID values for test ad units and live UWP ad units have different formats. Test application ID values are GUIDs. When you create a live UWP ad unit in Partner Center, the application ID value for the ad unit always matches the Store ID for your app (an example Store ID value looks like 9NBLGGH4R315).

3.  Assign the application ID and ad unit ID values in your app's code. For more information, see the following articles:
    * [AdControl in XAML and .NET](adcontrol-in-xaml-and--net.md)
    * [AdControl in HTML 5 and JavaScript](adcontrol-in-html-5-and-javascript.md)
    * [Interstitial ads](../monetize/interstitial-ads.md)
    * [Native ads](../monetize/native-ads.md)

<span id="manage"></span>

## Manage ad units for multiple ad controls in your app

You can use multiple banner, interstitial, and native ad controls in a single app. In this scenario, we recommend that you assign a different ad unit to each control. Using different ad units for each control enables you to separately [configure the mediation settings](/windows/apps/publish/in-app-ads#mediation) and get discrete [reporting data](/windows/apps/publish/advertising-performance-report) for each control. This also enables our services to better optimize the ads we serve to your app.

> [!IMPORTANT]
> You can use each ad unit in only one app. If you use an ad unit in more than one app, ads will not be served for that ad unit.

## Related topics

* [AdControl in XAML and .NET](adcontrol-in-xaml-and--net.md)
* [AdControl in HTML 5 and JavaScript](adcontrol-in-html-5-and-javascript.md)
* [Interstitial ads](interstitial-ads.md)
* [Native ads](native-ads.md)


 

 
