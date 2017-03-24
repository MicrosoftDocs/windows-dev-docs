---
author: mcleanbyron
ms.assetid: 63A9EDCF-A418-476C-8677-D8770B45D1D7
description: The Microsoft Store Services SDK gives you several ways to monetize your app with ads.
title: Display ads in your app
ms.author: mcleans
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, ads, advertising, banner, interstitial
---

# Display ads in your app


The Universal Windows Platform (UWP) and Windows Store provide several ways to monetize your app with ads.

## Display banner and interstitial ads using the Microsoft advertising libraries

Make more money from your apps by including banner or interstitial ads in your app.

* *Banner ads* are small advertisements that utilize a portion of a page in an app, usually at the top or bottom of the page.
* *Interstitial ads* are full-screen advertisements that typically force the user to watch a video or click through them to continue in the app or game. We support two types of interstitial ads for UWP apps: video and banner.

To include these types of ads in your apps, use the **AdControl** and **InterstitialAd** controls in the advertising libraries that are distributed in the [Microsoft Store Services SDK](http://aka.ms/store-em-sdk) (for UWP apps) and the [Microsoft Advertising SDK for Windows and Windows Phone 8.x](http://aka.ms/store-8-sdk) (for Windows 8.1 and Windows Phone 8.x apps).

You can monitor your ad performance in real time by using the [advertising performance report](../publish/advertising-performance-report.md) in the Windows Dev Center dashboard.

The following topics provide information about common tasks involving the Windows advertising libraries.

|  Task    | Topic |               
|----------|-------|
| Install and get started using the Microsoft advertising libraries.     | See [Get started with Microsoft advertising libraries](get-started-with-microsoft-advertising-libraries.md).        |
| Show banner ads in your XAML/C# app.     | See [AdControl in XAML and .NET](adcontrol-in-xaml-and--net.md).        |
| Show banner ads in your HTML/JavaScript app.     | See [AdControl in HTML 5 and Javascript](adcontrol-in-html-5-and-javascript.md).        |
| Show banner ads in your Windows Phone Silverlight 8.x app.     | See [AdControl in Windows Phone Silverlight](adcontrol-in-windows-phone-silverlight.md).        |
| Show an interstitial ad in your app.     | See [Interstitial ads](interstitial-ads.md).       |
| Add advertisements to video content in a Universal Windows Platform (UWP) app that was written using JavaScript with HTML.   |  See [Add advertisements to video content in HTML 5 and JavaScript](add-advertisements-to-video-content.md).  |
| Download sample projects that demonstrate how to add banner and interstitial ads to apps.     |See the [Advertising samples on GitHub](http://aka.ms/githubads).       |
| Handle [AdControl](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.adcontrol.aspx) errors in your app.     | See [Error handling](error-handling-with-advertising-libraries.md) and the walkthroughs under [AdControl error handling](adcontrol-error-handling.md).       |
| Report a bug in the Microsoft advertising libraries.     | Visit the [support page](https://go.microsoft.com/fwlink/p/?LinkId=331508).        |
| Get community support.     | Visit the [forum](http://go.microsoft.com/fwlink/p/?LinkId=401266).       |

                            

## Use ad mediation for banner ads (Windows 8.1 and Windows Phone 8.x)

For Windows 8.1 and Windows Phone 8.x apps, you can use the **AdMediatorControl** class to optimize your advertising revenue by displaying banner ads from multiple ad networks. After you add this control to your app, you configure your ad mediation settings on the Windows Dev Center dashboard, and we take care of mediating banner ad requests from the ad networks you choose. For more information, see [Use ad mediation to maximize ad revenue](https://msdn.microsoft.com/library/windows/apps/xaml/dn864359.aspx).

> [!NOTE]
> Ad mediation using the **AdMediatorControl** class is currently not supported for UWP apps for Windows 10. Server-side mediation is coming soon for UWP apps using the same APIs for banner ads (**AdControl**) and interstitial ads (**InterstitialAd**). For guidance about migrating from **AdMediatorControl** to **AdControl** in your UWP app, see [Migrate from AdMediatorControl to AdControl for UWP apps](migrate-from-admediatorcontrol-to-adcontrol.md).

<span id="silverlight_support"/>
## Advertising support for Windows Phone 8.x Silverlight projects

Some developer scenarios are no longer supported in Windows Phone 8.x Silverlight projects. For more information, see the following table.

|  Platform version  |  Existing projects    |   New projects  |
|-----------------|----------------|--------------|
| Windows Phone 8.0 Silverlight     |  If you have an existing Windows Phone 8.0 Silverlight project that already uses an **AdControl** or **AdMediatorControl** from an earlier release of the Universal Ad Client SDK or Microsoft Advertising SDK and this app is already published in the Windows Store, you can modify and rebuild the project, and you can debug or test your changes on a device. Debugging or testing the project in the emulator is not supported.  |  Not supported.  |
| Windows Phone 8.1 Silverlight    |  If you have an existing Windows Phone 8.1 Silverlight project that uses an **AdControl** or **AdMediatorControl** from an earlier SDK, you can modify and rebuild the project. However, to debug or test the app, you must run the app in the emulator and use [test mode values](test-mode-values.md) for the application ID and ad unit ID. Debugging or testing the app on a device is not supported.  |   You can add an **AdControl** or **AdMediatorControl** to a new Windows Phone 8.1 Silverlight project. However, to debug or test the app, you must run the app in the emulator and use [test mode values](test-mode-values.md) for the application ID and ad unit ID. Debugging or testing the app on a device is not supported. |

## Related topics

* [Microsoft Store Services SDK](microsoft-store-services-sdk.md)
* [Monetize your app with ads](http://go.microsoft.com/fwlink/p/?LinkId=699559)
* [Advertising performance report](../publish/advertising-performance-report.md)
