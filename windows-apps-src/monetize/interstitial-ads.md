---
author: mcleanbyron
ms.assetid: 1f970d38-2338-470e-b5ba-811402752fc4
description: Learn how to include interstitial ads in a Windows 10, Windows 8.1, or Windows Phone 8.1 app using the Microsoft advertising libraries in the Microsoft Store Services SDK.
title: Interstitial ads
ms.author: mcleans
ms.date: 03/17/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, ads, advertising, interstitial
---

# Interstitial ads

This walkthrough shows how to include interstitial ads in Universal Windows Platform (UWP) apps and games for Windows 10 and in apps for Windows 8.1 or Windows Phone 8.1. For complete sample projects that demonstrate how to add interstitial ads to JavaScript/HTML apps and XAML apps using C# and C++, see the [advertising samples on GitHub](http://aka.ms/githubads).

<span id="whatareinterstitialads10"/>
## What are interstitial ads?

Unlike standard banner ads, which are confined to a portion of a UI in an app or game, interstitial ads are shown on the entire screen. Two basic forms are frequently used in games.

* With *Paywall* ads, the user must watch an ad at some regular interval. For example between game levels:

    ![whatisaninterstitial](images/13-ed0a333b-0fc8-4ca9-a4c8-11e8b4392831.png)

* With *Rewards Based* ads the user is explicitly seeking some benefit, such as a hint or extra time to complete the level, and initializes the ad through the app’s user interface.

We provide two types of interstitial ads to use in your apps and games:

* **Interstitial video ads**: These are available for Universal Windows Platform (UWP) apps for Windows 10 and in apps for Windows 8.1 or Windows Phone 8.1.

* **Interstitial banner ads**: These are only available for UWP apps for Windows 10.

>**Note**&nbsp;&nbsp;The API for interstitial ads does not handle any user interface except at the time of video playback. Refer to the [interstitial best practices](ui-and-user-experience-guidelines.md#interstitialbestpractices10) for guidelines on what to do, and avoid, as you consider how to integrate interstitial ads in your app.

## Build an app with interstitial ads

To show interstitial ads in your app, follow the instructions for project type:

* [XAML/.NET](#interstitialadsxaml10)
* [HTML/JavaScript](#interstitialadshtml10)
* [C++ (DirectX Interop)](#interstitialadsdirectx10)

<span/>
### Prerequisites

* For UWP apps: install the [Microsoft Store Services SDK](http://aka.ms/store-em-sdk) with Visual Studio 2015.
  >**Note**&nbsp;&nbsp;Interstitial banner ads require version 10.0.3 or later of the Microsoft Store Services SDK. Interstitial video ads are supported in all versions of the SDK.
* For Windows 8.1 or Windows Phone 8.1 apps: install the [Microsoft Advertising SDK for Windows and Windows Phone 8.x](http://aka.ms/store-8-sdk) with Visual Studio 2015 or Visual Studio 2013.

<span id="interstitialadsxaml10"/>
### XAML/.NET

This section provides C# examples, but Visual Basic and C++ are also supported for XAML/.NET projects. For a complete C# code example, see [Interstitial ad sample code in C#](interstitial-ad-sample-code-in-c.md).

1. Open your project in Visual Studio.

2. In **Reference Manager**, select one of the following references depending on your project type:

  * For a Universal Windows Platform (UWP) project: Expand **Universal Windows**, click **Extensions**, and then select the check box next to **Microsoft Advertising SDK for XAML** (Version 10.0).

  * For a Windows 8.1 project: Expand **Windows 8.1**, click **Extensions**, and then select the check box next to **Ad Mediator SDK for Windows 8.1 XAML**. This option will add both the Microsoft advertising and ad mediator libraries to your project, but you can ignore the ad mediator libraries.

  * For a Windows Phone 8.1 project: Expand **Windows Phone 8.1**, click **Extensions**, and then select the check box next to **Ad Mediator SDK for Windows Phone 8.1 XAML**. This option will add both the Microsoft advertising and ad mediator libraries to your project, but you can ignore the ad mediator libraries.

3.  In the appropriate code file in your app (for example, in MainPage.xaml.cs or a code file for some other page), add the following namespace reference.

  > [!div class="tabbedCodeSnippets"]
  [!code-cs[InterstitialAd](./code/AdvertisingSamples/InterstitialAdSamples/cs/MainPage.xaml.cs#Snippet1)]

4.  In an appropriate location in your app (for example, in ```MainPage``` or some other page), declare an [InterstitialAd](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.interstitialad.aspx) object and several string fields that represent the application ID and ad unit ID for your interstitial ad. The following code example assigns the `myAppId` and `myAdUnitId` fields to the test values for interstitial video ads provided in [Test mode values](test-mode-values.md). These values are only used for testing; you must replace them with live values from Windows Dev Center before you publish your app.

  > [!div class="tabbedCodeSnippets"]
  [!code-cs[InterstitialAd](./code/AdvertisingSamples/InterstitialAdSamples/cs/MainPage.xaml.cs#Snippet2)]

5.  In code that runs on startup (for example, in the constructor for the page), instantiate the **InterstitialAd** object and wire up event handlers for events of the object.

  > [!div class="tabbedCodeSnippets"]
  [!code-cs[InterstitialAd](./code/AdvertisingSamples/InterstitialAdSamples/cs/MainPage.xaml.cs#Snippet3)]

6.  If you want to show an *interstitial video* ad: Approximately 30-60 seconds before you need the ad, use the [RequestAd](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.interstitialad.requestad.aspx) method to pre-fetch the ad. This allows enough time to request and prepare the ad before it should be shown. Be sure to specify **AdType.Video** for the ad type.

  > [!div class="tabbedCodeSnippets"]
  [!code-cs[InterstitialAd](./code/AdvertisingSamples/InterstitialAdSamples/cs/MainPage.xaml.cs#Snippet4)]

  If you want to show an *interstitial banner* ad (for UWP apps only): Approximately 5-8 seconds before you need the ad, use the [RequestAd](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.interstitialad.requestad.aspx) method to pre-fetch the ad. This allows enough time to request and prepare the ad before it should be shown. Be sure to specify **AdType.Display** for the ad type. This value is available starting in version 10.0.3 of the Microsoft Store Services SDK.

  > [!div class="tabbedCodeSnippets"]
  ```csharp
  myInterstitialAd.RequestAd(AdType.Display, myAppId, myAdUnitId);
  ```

6.  At the point in your code where you want to show the interstitial video or interstitial banner ad, confirm that the **InterstitialAd** is ready to be shown and then show it by using the [Show](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.interstitialad.show.aspx) method.

  > [!div class="tabbedCodeSnippets"]
  [!code-cs[InterstitialAd](./code/AdvertisingSamples/InterstitialAdSamples/cs/MainPage.xaml.cs#Snippet5)]

7.  Define the event handlers for the **InterstitialAd** object.

  > [!div class="tabbedCodeSnippets"]
  [!code-cs[InterstitialAd](./code/AdvertisingSamples/InterstitialAdSamples/cs/MainPage.xaml.cs#Snippet6)]

8.  Build and test your app to confirm it is showing test ads.

<span id="interstitialadshtml10"/>
### HTML/JavaScript

The following instructions assume you have created a Universal Windows project for JavaScript in Visual Studio 2015 and are targeting a specific CPU. For a complete code sample, see [Interstitial ad sample code in JavaScript](interstitial-ad-sample-code-in-javascript.md).

1. Open your project in Visual Studio.

2.  In **Reference Manager**, select one of the following references depending on your project type:

  * For a Universal Windows Platform (UWP) project: Expand **Universal Windows**, click **Extensions**, and then select the check box next to **Microsoft Advertising SDK for JavaScript** (Version 10.0).

  * For a Windows 8.1 project: Expand **Windows 8.1**, click **Extensions**, and then select the check box next to **Microsoft Advertising SDK for Windows 8.1 Native (JS)**.

  * For a Windows 8.1 project: Expand **Windows Phone 8.1**, click **Extensions**, and then select the check box next to **Microsoft Advertising SDK for Windows Phone 8.1 Native (JS)**.

3.  In the **&lt;head&gt;** section of the HTML file in the project, after the project’s JavaScript references of default.css and default.js, add the reference to ad.js. In a UWP project, add the following reference.

  > [!div class="tabbedCodeSnippets"]
  ``` html
  <script src="//Microsoft.Advertising.JavaScript/ad.js"></script>
  ```

  In a Windows 8.1 or Windows Phone 8.1 project, add the following reference.

  > [!div class="tabbedCodeSnippets"]
  ``` html
  <script src="/MSAdvertisingJS/ads/ad.js"></script>
  ```

4.  In a .js file in your project, declare an [InterstitialAd](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.interstitialad.aspx) object and several fields that contain the application ID and ad unit ID for your interstitial ad. The following code example assigns the `applicationId` and `adUnitId` fields to the test values for interstitial video ads provided in [Test mode values](test-mode-values.md). These values are only used for testing; you must replace them with live values from Windows Dev Center before you publish your app.

  > [!div class="tabbedCodeSnippets"]
  [!code-javascript[InterstitialAd](./code/AdvertisingSamples/InterstitialAdSamples/js/script.js#Snippet1)]

5.  In code that runs on startup (for example, in the constructor for the page), instantiate the **InterstitialAd** object and wire up event handlers for the object.

  > [!div class="tabbedCodeSnippets"]
  [!code-javascript[InterstitialAd](./code/AdvertisingSamples/InterstitialAdSamples/js/script.js#Snippet2)]

5. If you want to show an *interstitial video* ad: Approximately 30-60 seconds before you need the ad, use the [RequestAd](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.interstitialad.requestad.aspx) method to pre-fetch the ad. This allows enough time to request and prepare the ad before it should be shown. Be sure to specify **InterstitialAdType.video** for the ad type.

  > [!div class="tabbedCodeSnippets"]
  [!code-javascript[InterstitialAd](./code/AdvertisingSamples/InterstitialAdSamples/js/script.js#Snippet3)]

  If you want to show an *interstitial banner* ad (for UWP apps only): Approximately 5-8 seconds before you need the ad, use the [RequestAd](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.interstitialad.requestad.aspx) method to pre-fetch the ad. This allows enough time to request and prepare the ad before it should be shown. Be sure to specify **InterstitialAdType.display** for the ad type. This value is available starting in version 10.0.3 of the Microsoft Store Services SDK.

  > [!div class="tabbedCodeSnippets"]
  ```js
  if (interstitialAd) {
      interstitialAd.requestAd(MicrosoftNSJS.Advertising.InterstitialAdType.display, applicationId, adUnitId);
  }
  ```

6.  At the point in your code where you want to show the ad, confirm that the **InterstitialAd** is ready to be shown and then show it by using the [Show](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.interstitialad.show.aspx) method.

  > [!div class="tabbedCodeSnippets"]
  [!code-javascript[InterstitialAd](./code/AdvertisingSamples/InterstitialAdSamples/js/samples.js#Snippet4)]

7.  Define the event handlers for the **InterstitialAd** object.

  > [!div class="tabbedCodeSnippets"]
  [!code-javascript[InterstitialAd](./code/AdvertisingSamples/InterstitialAdSamples/js/samples.js#Snippet5)]

9.  Build and test your app to confirm it is showing test ads.

<span id="interstitialadsdirectx10"/>
### C++ (DirectX Interop)

This sample assumes you have created a C++ **DirectX and XAML App (Universal Windows)** project in Visual Studio 2015 and are targeting a specific CPU architecture.
 
1. Open your project in Visual Studio.

1.  In **Reference Manager**, select one of the following references depending on your project type:

  * For a Universal Windows Platform (UWP) project: Expand **Universal Windows**, click **Extensions**, and then select the check box next to **Microsoft Advertising SDK for XAML** (Version 10.0).

  * For a Windows 8.1 project: Expand **Windows 8.1**, click **Extensions**, and then select the check box next to **Ad Mediator SDK for Windows 8.1 XAML**. This option will add both the Microsoft advertising and ad mediator libraries to your project, but you can ignore the ad mediator libraries.

  * For a Windows Phone 8.1 project: Expand **Windows Phone 8.1**, click **Extensions**, and then select the check box next to **Ad Mediator SDK for Windows Phone 8.1 XAML**. This option will add both the Microsoft advertising and ad mediator libraries to your project, but you can ignore the ad mediator libraries.

2.  In an appropriate header file for your app (for example, DirectXPage.xaml.h), declare an [InterstitialAd](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.interstitialad.aspx) object and related event handler methods.  

  > [!div class="tabbedCodeSnippets"]
  [!code-cpp[InterstitialAd](./code/AdvertisingSamples/InterstitialAdSamples/cpp/DirectXPage.xaml.h#Snippet1)]

3.  In the same header file, declare several string fields that represent the application ID and ad unit ID for your interstitial ad. The following code example assigns the `myAppId` and `myAdUnitId` fields to the test values for interstitial video ads provided in [Test mode values](test-mode-values.md). These values are only used for testing; you must replace them with live values from Windows Dev Center before you publish your app.

  > [!div class="tabbedCodeSnippets"]
  [!code-cpp[InterstitialAd](./code/AdvertisingSamples/InterstitialAdSamples/cpp/DirectXPage.xaml.h#Snippet2)]

4.  In the .cpp file where you want to add code to show an interstitial ad, add the following namespace reference. The following examples assume you are adding the code to DirectXPage.xaml.cpp file in your app.

  > [!div class="tabbedCodeSnippets"]
  [!code-cpp[InterstitialAd](./code/AdvertisingSamples/InterstitialAdSamples/cpp/DirectXPage.xaml.cpp#Snippet3)]

6.  In code that runs on startup (for example, in the constructor for the page), instantiate the **InterstitialAd** object and wire up event handlers for events of the object. In the following example, ```InterstitialAdSamplesCpp``` is the namespace for your project; change this name as necessary for your code.

  > [!div class="tabbedCodeSnippets"]
  [!code-cpp[InterstitialAd](./code/AdvertisingSamples/InterstitialAdSamples/cpp/DirectXPage.xaml.cpp#Snippet4)]

7. If you want to show an *interstitial video* ad: Approximately 30-60 seconds before you need the interstitial ad, use the [RequestAd](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.interstitialad.requestad.aspx) method to pre-fetch the ad. This allows enough time to request and prepare the ad before it should be shown. Be sure to specify **AdType::Video** for the ad type.

  > [!div class="tabbedCodeSnippets"]
  [!code-cpp[InterstitialAd](./code/AdvertisingSamples/InterstitialAdSamples/cpp/DirectXPage.xaml.cpp#Snippet5)]

  If you want to show an *interstitial banner* ad (for UWP apps only): Approximately 5-8 seconds before you need the ad, use the [RequestAd](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.interstitialad.requestad.aspx) method to pre-fetch the ad. This allows enough time to request and prepare the ad before it should be shown. Be sure to specify **AdType::Display** for the ad type. This value is available starting in version 10.0.3 of the Microsoft Store Services SDK.

  > [!div class="tabbedCodeSnippets"]
  ```cpp
  m_interstitialAd->RequestAd(AdType::Display, myAppId, myAdUnitId);
  ```

7.  At the point in your code where you want to show the ad, confirm that the **InterstitialAd** is ready to be shown and then show it by using the [Show](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.interstitialad.show.aspx) method.

  > [!div class="tabbedCodeSnippets"]
  [!code-cpp[InterstitialAd](./code/AdvertisingSamples/InterstitialAdSamples/cpp/DirectXPage.xaml.cpp#Snippet6)]

8.  Define the event handlers for the **InterstitialAd** object.

  > [!div class="tabbedCodeSnippets"]
  [!code-cpp[InterstitialAd](./code/AdvertisingSamples/InterstitialAdSamples/cpp/DirectXPage.xaml.cpp#Snippet7)]

9. Build and test your app to confirm it is showing test ads.

<span/>
### Release your app with live ads using Windows Dev Center

1.  In the Dev Center dashboard, go to the **Monetization** &gt; **Monetize with ads** page for your app, and [create an ad unit](../publish/monetize-with-ads.md). For the ad unit type, choose **Video interstitial** or **Banner interstitial**, depending on what type of interstitial ad you are showing. Make note of both the ad unit ID and the application ID.

2.  In your code, replace the test ad unit values with the live values you generated in Dev Center.

3.  [Submit your app](../publish/app-submissions.md) to the Store using the Windows Dev Center dashboard.

4.  Review your [advertising performance reports](../publish/advertising-performance-report.md) in the Dev Center dashboard.

<span id="interstitialbestpractices10"/>
## Interstitial best practices and policies


For more information about how to use interstitial ads effectively and policies that you must follow, see [Interstitial best practices and policies](ui-and-user-experience-guidelines.md#interstitialbestpractices10).

<span id="targetplatform10"/>
## Remove reference errors: target a specific CPU platform (XAML and HTML)


When using the Microsoft advertising libraries, you cannot target **Any CPU** in your project. If your project targets the **Any CPU** platform, you may see a warning in your project after you add a reference to the Microsoft advertising libraries. To remove this warning, update your project to use an architecture-specific build output (for example, **x86**). For more information, see [Known issues](known-issues-for-the-advertising-libraries.md).

## Related topics


* [Interstitial ad sample code in C#](interstitial-ad-sample-code-in-c.md)
* [Interstitial ad sample code in JavaScript](interstitial-ad-sample-code-in-javascript.md)
* [Advertising samples on GitHub](http://aka.ms/githubads)

 

 
