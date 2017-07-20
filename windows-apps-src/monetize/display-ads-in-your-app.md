---
author: mcleanbyron
ms.assetid: 63A9EDCF-A418-476C-8677-D8770B45D1D7
description: The Microsoft Advertising SDK gives you several ways to monetize your app with ads.
title: Display ads in your app with the Microsoft Advertising SDK
ms.author: mcleans
ms.date: 07/20/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, ads, advertising, banner, ad control, interstitial
---

# Display ads in your app with the Microsoft Advertising SDK

Increase your revenue opportunities by putting ads in your apps by using the Microsoft Advertising SDK. Our ad monetization platform offers a variety of ad types and supports mediation with many popular ad networks.

To display ads in your apps, follow these steps.

## Step 1: Install the Microsoft Advertising SDK

The Microsoft Advertising SDK provides a variety of controls you can use in your app to show different types of ads. For installation instructions, see [this article](install-the-microsoft-advertising-libraries.md).

## Step 2: Choose your ad type and add code to display test ads in your app

The Microsoft Advertising SDK provides several different types of ads you can display in your app. Choose which types of ads are best for your scenario and then add code to your app to display those ads.

You must specify an application ID and ad unit ID in your code to serve ads to your app. While you are developing your app, you should use [test application ID and ad unit ID values](test-mode-values.md) to see how your app renders ads during testing.

### Banner ads

These are static display ads that utilize a portion of a page in an app, often at the top or bottom of the page.

For instructions and code examples, see [AdControl in XAML and .NET](adcontrol-in-xaml-and--net.md) and [AdControl in HTML 5 and Javascript](adcontrol-in-html-5-and-javascript.md). For complete sample projects that demonstrate how to add banner ads to JavaScript/HTML apps and XAML apps using C# and C++, see the [advertising samples on GitHub](http://aka.ms/githubads).

![addreferences](images/banner-ad.png)

### Interstitial ads

These are full-screen ads that typically require the user to watch a video or click through them to continue in the app or game. We support two types of interstitial ads: video and banner.

For instructions and code examples, see [Interstitial ads](interstitial-ads.md). For complete sample projects that demonstrate how to add interstitial ads to JavaScript/HTML apps and XAML apps using C# and C++, see the [advertising samples on GitHub](http://aka.ms/githubads).

![addreferences](images/interstitial-ad.png)

### Native ads

These are component-based ads. Each piece of the ad creative (such as the title, image, description, and call-to-action text) is delivered to your app as an individual element that you can integrate into your app using your own fonts, colors, and other UI components to stitch together an unobtrusive user experience.

For instructions and code examples, see [Native ads](native-ads.md).

![addreferences](images/native-ad.png)

## Step 3: Get an ad unit from Dev Center and configure your app to receive live ads

After you finish testing your app and you are ready to submit it to the Store, create an ad unit on the [Monetize with ads](../publish/monetize-with-ads.md) page in the Windows Dev Center dashboard. Then, update your app code to use the application ID and ad unit ID values for this ad unit. If you try to use test application ID and ad unit ID values in the published version of your app in the Store, your app will not receive live ads. For more information, see [Set up ad units in your app](set-up-ad-units-in-your-app.md).

<span id="ad-mediation"/>
### Configure ad mediation

By default, banner ads, interstitial ads, and native ads display advertisements from Microsoft's network for paid ads. To maximize your ad revenue, you can enable ad mediation for your ad unit to display ads from additional paid ad networks such as Taboola and Smaato. You can also increase your app promotion capabilities by serving ads from Microsoft app promotion campaigns.

To start using ad mediation in your UWP app, [configure ad mediation settings](../publish/monetize-with-ads.md#mediation) for your ad unit. By default, we automatically configure the mediation settings using machine-learning algorithms to help you maximize your ad revenue across the markets your app supports. However, also have the option to manually choose the networks you want to use. Either way, the mediation settings are configured entirely in the service; you do not need to make any code changes in your app.    

<span id="8.x-mediation"/>
### Mediation in Windows 8.1 and Windows Phone 8.x apps

In Windows 8.1 and Windows Phone 8.x apps, ad mediation is only available for banner ads. To use ad mediation, you must use the **AdMediatorControl** class in the [Microsoft Advertising SDK for Windows and Windows Phone 8.x](http://aka.ms/store-8-sdk) instead of the **AdControl** class. After you add this control to your app, you can manually configure your ad mediation settings on the dashboard.

For more information about using ad mediation in a Windows 8.1 or Windows Phone 8.x app, see [this article](https://msdn.microsoft.com/library/windows/apps/xaml/dn864359.aspx).

> [!NOTE]
> Ad mediation for Windows 8.1 and Windows Phone 8.x apps is no longer under active development. To maximize your potential advertising revenue, we recommend that you build UWP apps that use ad mediation with banner ads, interstitial ads, or native ads.

## Step 4: Submit your app and review performance

After you finish developing your app with ads, you can [submit your updated app](https://msdn.microsoft.com/windows/uwp/publish/app-submissions) to the Dev Center dashboard so it is available in the Store. Apps that display ads must meet the additional requirements that are specified in [section 10.10 of the Windows Store Policies](https://msdn.microsoft.com/library/windows/apps/dn764944.aspx#pol_10_10) and [Exhibit E of the App Developer Agreement](https://msdn.microsoft.com/library/windows/apps/hh694058.aspx).

After your app is published and available in the Store, you can review your [advertising performance reports](../publish/advertising-performance-report.md) in the dashboard and continue to make changes to your mediation settings to optimize the performance of your ads. Your advertising revenue is included in your [payout summary](../publish/payout-summary.md).

<span id="additional-help" />
## Additional help

For additional help using the Microsoft Advertising SDK, use the following resources.

|  Task    | Resource |               
|----------|-------|
| Report a bug or get assisted support for advertising     | Visit the [support page](https://go.microsoft.com/fwlink/p/?LinkId=331508) and choose **In-App Advertising**.        |
| Get community support     | Visit the [forum](http://go.microsoft.com/fwlink/p/?LinkId=401266).       |
| Download sample projects that demonstrate how to add banner and interstitial ads to apps.     | See the [Advertising samples on GitHub](http://aka.ms/githubads).       |
| Learn about the latest monetization opportunities for Windows apps     | Visit [Monetize your apps](https://developer.microsoft.com/store/monetize).        |

## Related topics

* [Microsoft Advertising SDK](http://aka.ms/ads-sdk-uwp)
* [Monetize your app with ads](http://go.microsoft.com/fwlink/p/?LinkId=699559)
* [Advertising performance report](../publish/advertising-performance-report.md)
