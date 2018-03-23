---
author: mcleanbyron
ms.assetid: 7a16b0ca-6b8e-4ade-9853-85690e06bda6
description: Learn how to launch an interstitial ad using C#.
title: Interstitial ad sample code in C#
ms.author: mcleans
ms.date: 03/22/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, ads, advertising, interstitial, c#, sample code
ms.localizationpriority: medium
---

# Interstitial ad sample code in C\# #  

This topic provides the complete sample code for a basic C# and XAML Universal Windows Platform (UWP) app that shows an interstitial video ad. For step-by-step instructions that show how to configure your project to use this code, see [Interstitial ads](interstitial-ads.md). For a complete sample project, see the [advertising samples on GitHub](http://aka.ms/githubads).

## Code example

This section shows the contents of the MainPage.xaml and MainPage.xaml.cs files in a basic app that shows an interstitial ad. To use these examples, copy the code into a Visual C# **Blank App (Universal Windows)** project in Visual Studio.

This sample app uses two buttons to request and then launch an interstitial ad. Replace the values of the ```myAppId``` and ```myAdUnitId``` fields with live values from Windows Dev Center before submitting your app to the Store. For more information, see [Set up ad units in your app](set-up-ad-units-in-your-app.md#live-ad-units).

> [!NOTE]
> To alter this example to show an interstitial banner ad instead of an interstitial video ad, pass the value **AdType.Display** to the first parameter of the [RequestAd](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.interstitialad.requestad.aspx) method instead of **AdType.Video**. For more information, see [Interstitial ads](interstitial-ads.md).

### MainPage.xaml

> [!div class="tabbedCodeSnippets"]
[!code-xml[InterstitialAd](./code/AdvertisingSamples/InterstitialAdSamples/cs/MainPage.xaml#L1-L13)]

### MainPage.xaml.cs

> [!div class="tabbedCodeSnippets"]
[!code-cs[InterstitialAd](./code/AdvertisingSamples/InterstitialAdSamples/cs/MainPage.xaml.cs#CompleteSample)]

 
## Related topics

* [Advertising samples on GitHub](http://aka.ms/githubads)
 
