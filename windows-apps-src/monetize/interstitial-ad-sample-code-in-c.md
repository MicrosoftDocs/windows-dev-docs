---
ms.assetid: 7a16b0ca-6b8e-4ade-9853-85690e06bda6
description: Learn how to display and launch an interstitial ad in a Universal Windows Platform (UWP) app using C# and XAML.
title: Interstitial ad sample code in C#
ms.date: 02/18/2020
ms.topic: article
keywords: windows 10, uwp, ads, advertising, interstitial, c#, sample code
ms.localizationpriority: medium
---
# Interstitial ad sample code in C\# #  

>[!WARNING]
> As of June 1, 2020, the Microsoft Ad Monetization platform for Windows UWP apps will be shut down. [Learn more](https://social.msdn.microsoft.com/Forums/windowsapps/en-US/db8d44cb-1381-47f7-94d3-c6ded3fea36f/microsoft-ad-monetization-platform-shutting-down-june-1st?forum=aiamgr)

This topic provides the complete sample code for a basic C# and XAML Universal Windows Platform (UWP) app that shows an interstitial video ad. For step-by-step instructions that show how to configure your project to use this code, see [Interstitial ads](interstitial-ads.md). For a complete sample project, see the [advertising samples on GitHub](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Advertising).

## Code example

This section shows the contents of the MainPage.xaml and MainPage.xaml.cs files in a basic app that shows an interstitial ad. To use these examples, copy the code into a Visual C# **Blank App (Universal Windows)** project in Visual Studio.

This sample app uses two buttons to request and then launch an interstitial ad. Replace the values of the ```myAppId``` and ```myAdUnitId``` fields with live values from Partner Center before submitting your app to the Store. For more information, see [Set up ad units in your app](set-up-ad-units-in-your-app.md#live-ad-units).

> [!NOTE]
> To alter this example to show an interstitial banner ad instead of an interstitial video ad, pass the value **AdType.Display** to the first parameter of the [RequestAd](/uwp/api/microsoft.advertising.winrt.ui.interstitialad.requestad) method instead of **AdType.Video**. For more information, see [Interstitial ads](interstitial-ads.md).

### MainPage.xaml

> [!div class="tabbedCodeSnippets"]
:::code language="xml" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/InterstitialAdSamples/cs/MainPage.xaml" range="1-13":::

### MainPage.xaml.cs

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/InterstitialAdSamples/cs/MainPage.xaml.cs" id="CompleteSample":::

 
## Related topics

* [Advertising samples on GitHub](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Advertising)
 
