---
author: mcleanbyron
ms.assetid: 63A9EDCF-A418-476C-8677-D8770B45D1D7
description: The Microsoft Advertising SDK gives you several ways to monetize your app with ads.
title: Display ads in your app
ms.author: mcleans
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, ads, advertising, banner, interstitial
---

# Display ads in your app

Increase your revenue opportunities by putting ads in your apps. Our ad monetization platform offers a variety of ad types and supports mediation with a many popular ad networks.

We support the following type of ads using APIs that are available in the [Microsoft Advertising SDK](http://aka.ms/ads-sdk-uwp) (for UWP apps for Windows 10) and the [Microsoft Advertising SDK for Windows and Windows Phone 8.x](http://aka.ms/store-8-sdk) (for Windows 8.1 and Windows Phone 8.x apps).

<br/>

|  Ad type    | Description |   Supported platforms  |            
|----------|-------|-------|
| Banner ads     |  These are static display ads that utilize a portion of a page in an app, usually at the top or bottom of the page.        |  UWP apps for Windows 10<br/><br/>Windows 8.x and Windows Phone 8.x  |
| Interstitial ads     |  These are full-screen ads that typically require the user to watch a video or click through them to continue in the app or game. We support two types of interstitial ads: video and banner.       |  UWP apps for Windows 10 (video and banner)<br/><br/>Windows 8.x and Windows Phone 8.x (video only)  |
| Native ads    | These are component-based ads. Each piece of the ad creative (such as the title, image, description, and call-to-action text) is delivered to your app as an individual element that you can integrate into your app using your own fonts, colors, and other UI components to stitch together an unobtrusive user experience.        |  UWP apps for Windows 10  |

To get started working with ads in your apps, see the following tasks.

<br/>


|  Task    | Topic |               
|----------|-------|
| Install and get started using the Microsoft Advertising SDK.     | [Get started with the Microsoft Advertising SDK](get-started-with-microsoft-advertising-libraries.md)        |
| Show an interstitial ad in your app.     | [Interstitial ads](interstitial-ads.md)       |
| Show a native ad in your UWP app.       | [Native ads](native-ads.md)  |
| Show banner ads in your XAML/C# app.     | [AdControl in XAML and .NET](adcontrol-in-xaml-and--net.md)        |
| Show banner ads in your HTML/JavaScript app.     | [AdControl in HTML 5 and Javascript](adcontrol-in-html-5-and-javascript.md).       |
| Show banner ads in your Windows Phone Silverlight 8.x app.     | [AdControl in Windows Phone Silverlight](adcontrol-in-windows-phone-silverlight.md)        |
| Enable mediation for an ad unit in your UWP app.     | [Monetize with ads](../publish/monetize-with-ads.md)       |
| Review performance data for ads in your app.     | [Advertising performance report](../publish/advertising-performance-report.md)       |
| Add advertisements to video content in a UWP app that was written using JavaScript with HTML.   |  [Add advertisements to video content in HTML 5 and JavaScript](add-advertisements-to-video-content.md)  |
| Download sample projects that demonstrate how to add banner and interstitial ads to apps.     | [Advertising samples on GitHub](http://aka.ms/githubads)       |
| Report a bug in the Microsoft advertising libraries.     | Visit the [support page](https://go.microsoft.com/fwlink/p/?LinkId=331508)        |
| Get community support.     | Visit the [forum](http://go.microsoft.com/fwlink/p/?LinkId=401266)       |

<span id="ad-mediation"/>
## Ad mediation in UWP apps for Windows 10

By default, banner ads, interstitial ads, and native ads display advertisements from Microsoft's network for paid ads. For UWP apps for Windows 10, we also offer the ability to enable ad mediation for these types of ads. Ad mediation enables you to maximize your ad revenue and app promotion capabilities by displaying ads from multiple ad networks, including ads from other paid ad networks such as Taboola and Smaato and ads for Microsoft app promotion campaigns.

To start using ad mediation with ads in your UWP app, go to the **Monetize with ads** page for your app in the Windows Dev Center dashboard and configure ad mediation settings for the UWP ad unit ID that is associated with the ad control in your app. By default, we automatically configure the mediation settings using machine-learning algorithms to help you maximize your ad revenue across the markets your app supports. However, also have the option to manually choose the networks you want to use. Either way, the mediation settings are configured entirely on the server; you do not need to make any code changes in your app.

For more information, see [Monetize with ads](../publish/monetize-with-ads.md).     

<span id="8.x-mediation"/>
## Mediation in Windows 8.1 and Windows Phone 8.x apps

In Windows 8.1 and Windows Phone 8.x apps, ad mediation is only available for banner ads. To use ad mediation, you must use the **AdMediatorControl** class in the [Microsoft Advertising SDK for Windows and Windows Phone 8.x](http://aka.ms/store-8-sdk) instead of the **AdControl** class. After you add this control to your app, you can manually configure your ad mediation settings on the Windows Dev Center dashboard.

For more information about using ad mediation in a Windows 8.1 or Windows Phone 8.x app, see [this article](https://msdn.microsoft.com/library/windows/apps/xaml/dn864359.aspx).

> [!NOTE]
> Ad mediation for Windows 8.1 and Windows Phone 8.x apps is no longer under active development. To maximize your potential advertising revenue, we recommend that you build UWP apps that use ad mediation with banner ads, interstitial ads, or native ads.

<span id="silverlight_support"/>
## Advertising support for Windows Phone 8.x Silverlight projects

Some developer scenarios are no longer supported in Windows Phone 8.x Silverlight projects. For more information, see the following table.

|  Platform version  |  Existing projects    |   New projects  |
|-----------------|----------------|--------------|
| Windows Phone 8.0 Silverlight     |  If you have an existing Windows Phone 8.0 Silverlight project that already uses an **AdControl** or **AdMediatorControl** from an earlier release of the Universal Ad Client SDK or Microsoft Advertising SDK and this app is already published in the Windows Store, you can modify and rebuild the project, and you can debug or test your changes on a device. Debugging or testing the project in the emulator is not supported.  |  Not supported.  |
| Windows Phone 8.1 Silverlight    |  If you have an existing Windows Phone 8.1 Silverlight project that uses an **AdControl** or **AdMediatorControl** from an earlier SDK, you can modify and rebuild the project. However, to debug or test the app, you must run the app in the emulator and use [test mode values](test-mode-values.md) for the application ID and ad unit ID. Debugging or testing the app on a device is not supported.  |   You can add an **AdControl** or **AdMediatorControl** to a new Windows Phone 8.1 Silverlight project. However, to debug or test the app, you must run the app in the emulator and use [test mode values](test-mode-values.md) for the application ID and ad unit ID. Debugging or testing the app on a device is not supported. |

## Related topics

* [Microsoft Advertising SDK](http://aka.ms/ads-sdk-uwp)
* [Monetize your app with ads](http://go.microsoft.com/fwlink/p/?LinkId=699559)
* [Advertising performance report](../publish/advertising-performance-report.md)
