---
ms.assetid: 1f970d38-2338-470e-b5ba-811402752fc4
description: Learn how to include interstitial ads in a UWP app for Windows 10 using the Microsoft Advertising SDK.
title: Interstitial ads
ms.date: 02/18/2020
ms.topic: article
keywords: windows 10, uwp, ads, advertising, ad control, interstitial
ms.localizationpriority: medium
---
# Interstitial ads

>[!WARNING]
> As of June 1, 2020, the Microsoft Ad Monetization platform for Windows UWP apps will be shut down. [Learn more](https://social.msdn.microsoft.com/Forums/windowsapps/en-US/db8d44cb-1381-47f7-94d3-c6ded3fea36f/microsoft-ad-monetization-platform-shutting-down-june-1st?forum=aiamgr)

This walkthrough shows how to include interstitial ads in Universal Windows Platform (UWP) apps and games for Windows 10. For complete sample projects that demonstrate how to add interstitial ads to JavaScript/HTML apps and XAML apps using C# and C++, see the [advertising samples on GitHub](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Advertising).

<span id="whatareinterstitialads10"/>

## What are interstitial ads?

Unlike standard banner ads, which are confined to a portion of a UI in an app or game, interstitial ads are shown on the entire screen. Two basic forms are frequently used in games.

* With *Paywall* ads, the user must watch an ad at some regular interval. For example between game levels:

    ![whatisaninterstitial](images/13-ed0a333b-0fc8-4ca9-a4c8-11e8b4392831.png)

* With *Rewards Based* ads the user is explicitly seeking some benefit, such as a hint or extra time to complete the level, and initializes the ad through the app’s user interface.

We provide two types of interstitial ads to use in your apps and games: **interstitial video ads** and **interstitial banner ads**.

> [!NOTE]
> The API for interstitial ads does not handle any user interface except at the time of video playback. Refer to the [interstitial best practices](ui-and-user-experience-guidelines.md#interstitialbestpractices10) for guidelines on what to do, and avoid, as you consider how to integrate interstitial ads in your app.

## Prerequisites

* Install the [Microsoft Advertising SDK](https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftAdvertisingSDK) with Visual Studio 2015 or a later release of Visual Studio. For installation instructions, see [this article](install-the-microsoft-advertising-libraries.md).

## Integrate an interstitial ad into your app

To show interstitial ads in your app, follow the instructions for project type:

* [XAML/.NET](#interstitialadsxaml10)
* [HTML/JavaScript](#interstitialadshtml10)
* [C++ (DirectX Interop)](#interstitialadsdirectx10)

<span id="interstitialadsxaml10"/>

### XAML/.NET

This section provides C# examples, but Visual Basic and C++ are also supported for XAML/.NET projects. For a complete C# code example, see [Interstitial ad sample code in C#](interstitial-ad-sample-code-in-c.md).

1. Open your project in Visual Studio.
    > [!NOTE]
    > If you're using an existing project, open the Package.appxmanifest file in your project and ensure that the **Internet (Client)** capability is selected. Your app needs this capability to receive test ads and live ads.

2. If your project targets **Any CPU**, update your project to use an architecture-specific build output (for example, **x86**). If your project targets **Any CPU**, you will not be able to successfully add a reference to the Microsoft advertising library in the following steps. For more information, see [Reference errors caused by targeting Any CPU in your project](known-issues-for-the-advertising-libraries.md#reference_errors).

3. Add a reference to the Microsoft Advertising SDK in your project:

    1. From the **Solution Explorer** window, right click **References**, and select **Add Reference…**
    2.  In **Reference Manager**, expand **Universal Windows**, click **Extensions**, and then select the check box next to **Microsoft Advertising SDK for XAML** (Version 10.0).
    3.  In **Reference Manager**, click OK.

3.  In the appropriate code file in your app (for example, in MainPage.xaml.cs or a code file for some other page), add the following namespace reference.

    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/InterstitialAdSamples/cs/MainPage.xaml.cs" id="Snippet1":::

4.  In an appropriate location in your app (for example, in ```MainPage``` or some other page), declare an [InterstitialAd](/uwp/api/microsoft.advertising.winrt.ui.interstitialad) object and several string fields that represent the application ID and ad unit ID for your interstitial ad. The following code example assigns the `myAppId` and `myAdUnitId` fields to the [test values](set-up-ad-units-in-your-app.md#test-ad-units) for interstitial ads.

    > [!NOTE]
    > Every **InterstitialAd** has a corresponding *ad unit* that is used by our services to serve ads to the control, and every ad unit consists of an *ad unit ID* and *application ID*. In these steps, you assign test ad unit ID and application ID values to your control. These test values can only be used in a test version of your app. Before you publish your app to the Store, you must [replace these test values with live values](#release) from Partner Center.

    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/InterstitialAdSamples/cs/MainPage.xaml.cs" id="Snippet2":::

5.  In code that runs on startup (for example, in the constructor for the page), instantiate the **InterstitialAd** object and wire up event handlers for events of the object.

    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/InterstitialAdSamples/cs/MainPage.xaml.cs" id="Snippet3":::

6.  If you want to show an *interstitial video* ad: Approximately 30-60 seconds before you need the ad, use the [RequestAd](/uwp/api/microsoft.advertising.winrt.ui.interstitialad.requestad) method to pre-fetch the ad. This allows enough time to request and prepare the ad before it should be shown. Be sure to specify **AdType.Video** for the ad type.

    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/InterstitialAdSamples/cs/MainPage.xaml.cs" id="Snippet4":::

    If you want to show an *interstitial banner* ad: Approximately 5-8 seconds before you need the ad, use the [RequestAd](/uwp/api/microsoft.advertising.winrt.ui.interstitialad.requestad) method to pre-fetch the ad. This allows enough time to request and prepare the ad before it should be shown. Be sure to specify **AdType.Display** for the ad type.

    ```csharp
    myInterstitialAd.RequestAd(AdType.Display, myAppId, myAdUnitId);
    ```

6.  At the point in your code where you want to show the interstitial video or interstitial banner ad, confirm that the **InterstitialAd** is ready to be shown and then show it by using the [Show](/uwp/api/microsoft.advertising.winrt.ui.interstitialad.show) method.

    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/InterstitialAdSamples/cs/MainPage.xaml.cs" id="Snippet5":::

7.  Define the event handlers for the **InterstitialAd** object.

    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/InterstitialAdSamples/cs/MainPage.xaml.cs" id="Snippet6":::

8.  Build and test your app to confirm it is showing test ads.

<span id="interstitialadshtml10"/>

### HTML/JavaScript

The following instructions assume you have created a Universal Windows project for JavaScript in Visual Studio and are targeting a specific CPU. For a complete code sample, see [Interstitial ad sample code in JavaScript](interstitial-ad-sample-code-in-javascript.md).

1. Open your project in Visual Studio.

2. If your project targets **Any CPU**, update your project to use an architecture-specific build output (for example, **x86**). If your project targets **Any CPU**, you will not be able to successfully add a reference to the Microsoft advertising library in the following steps. For more information, see [Reference errors caused by targeting Any CPU in your project](known-issues-for-the-advertising-libraries.md#reference_errors).

3. Add a reference to the Microsoft Advertising SDK in your project:

    1. From the **Solution Explorer** window, right click **References**, and select **Add Reference…**
    2.  In **Reference Manager**, expand **Universal Windows**, click **Extensions**, and then select the check box next to **Microsoft Advertising SDK for JavaScript** (Version 10.0).
    3.  In **Reference Manager**, click OK.

3.  In the **&lt;head&gt;** section of the HTML file in the project, after the project’s JavaScript references of default.css and default.js, add the reference to ad.js.

    ``` HTML
    <script src="//Microsoft.Advertising.JavaScript/ad.js"></script>
    ```

4.  In a .js file in your project, declare an [InterstitialAd](/uwp/api/microsoft.advertising.winrt.ui.interstitialad) object and several fields that contain the application ID and ad unit ID for your interstitial ad. The following code example assigns the `applicationId` and `adUnitId` fields to the [test values](set-up-ad-units-in-your-app.md#test-ad-units) for interstitial ads.

    > [!NOTE]
    > Every **InterstitialAd** has a corresponding *ad unit* that is used by our services to serve ads to the control, and every ad unit consists of an *ad unit ID* and *application ID*. In these steps, you assign test ad unit ID and application ID values to your control. These test values can only be used in a test version of your app. Before you publish your app to the Store, you must [replace these test values with live values](#release) from Partner Center.

    :::code language="javascript" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/InterstitialAdSamples/js/script.js" id="Snippet1":::

5.  In code that runs on startup (for example, in the constructor for the page), instantiate the **InterstitialAd** object and wire up event handlers for the object.

    :::code language="javascript" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/InterstitialAdSamples/js/script.js" id="Snippet2":::

5. If you want to show an *interstitial video* ad: Approximately 30-60 seconds before you need the ad, use the [RequestAd](/uwp/api/microsoft.advertising.winrt.ui.interstitialad.requestad) method to pre-fetch the ad. This allows enough time to request and prepare the ad before it should be shown. Be sure to specify **InterstitialAdType.video** for the ad type.

    :::code language="javascript" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/InterstitialAdSamples/js/script.js" id="Snippet3":::

    If you want to show an *interstitial banner* ad: Approximately 5-8 seconds before you need the ad, use the [RequestAd](/uwp/api/microsoft.advertising.winrt.ui.interstitialad.requestad) method to pre-fetch the ad. This allows enough time to request and prepare the ad before it should be shown. Be sure to specify **InterstitialAdType.display** for the ad type.

    ```js
    if (interstitialAd) {
        interstitialAd.requestAd(MicrosoftNSJS.Advertising.InterstitialAdType.display, applicationId, adUnitId);
    }
    ```

6.  At the point in your code where you want to show the ad, confirm that the **InterstitialAd** is ready to be shown and then show it by using the [Show](/uwp/api/microsoft.advertising.winrt.ui.interstitialad.show) method.

    :::code language="javascript" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/InterstitialAdSamples/js/samples.js" id="Snippet4":::

7.  Define the event handlers for the **InterstitialAd** object.

    :::code language="javascript" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/InterstitialAdSamples/js/samples.js" id="Snippet5":::

9.  Build and test your app to confirm it is showing test ads.

<span id="interstitialadsdirectx10"/>

### C++ (DirectX Interop)

This sample assumes you have created a C++ **DirectX and XAML App (Universal Windows)** project in Visual Studio and are targeting a specific CPU architecture.
 
1. Open your project in Visual Studio.

3. Add a reference to the Microsoft Advertising SDK in your project:

    1. From the **Solution Explorer** window, right click **References**, and select **Add Reference…**
    2.  In **Reference Manager**, expand **Universal Windows**, click **Extensions**, and then select the check box next to **Microsoft Advertising SDK for XAML** (Version 10.0).
    3.  In **Reference Manager**, click OK.

2.  In an appropriate header file for your app (for example, DirectXPage.xaml.h), declare an [InterstitialAd](/uwp/api/microsoft.advertising.winrt.ui.interstitialad) object and related event handler methods.  

    :::code language="cpp" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/InterstitialAdSamples/cpp/DirectXPage.xaml.h" id="Snippet1":::

3.  In the same header file, declare several string fields that represent the application ID and ad unit ID for your interstitial ad. The following code example assigns the `myAppId` and `myAdUnitId` fields to the [test values](set-up-ad-units-in-your-app.md#test-ad-units) for interstitial ads.

    > [!NOTE]
    > Every **InterstitialAd** has a corresponding *ad unit* that is used by our services to serve ads to the control, and every ad unit consists of an *ad unit ID* and *application ID*. In these steps, you assign test ad unit ID and application ID values to your control. These test values can only be used in a test version of your app. Before you publish your app to the Store, you must [replace these test values with live values](#release) from Partner Center.

    :::code language="cpp" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/InterstitialAdSamples/cpp/DirectXPage.xaml.h" id="Snippet2":::

4.  In the .cpp file where you want to add code to show an interstitial ad, add the following namespace reference. The following examples assume you are adding the code to DirectXPage.xaml.cpp file in your app.

    :::code language="cpp" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/InterstitialAdSamples/cpp/DirectXPage.xaml.cpp" id="Snippet3":::

6.  In code that runs on startup (for example, in the constructor for the page), instantiate the **InterstitialAd** object and wire up event handlers for events of the object. In the following example, ```InterstitialAdSamplesCpp``` is the namespace for your project; change this name as necessary for your code.

    :::code language="cpp" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/InterstitialAdSamples/cpp/DirectXPage.xaml.cpp" id="Snippet4":::

7. If you want to show an *interstitial video* ad: Approximately 30-60 seconds before you need the interstitial ad, use the [RequestAd](/uwp/api/microsoft.advertising.winrt.ui.interstitialad.requestad) method to pre-fetch the ad. This allows enough time to request and prepare the ad before it should be shown. Be sure to specify **AdType::Video** for the ad type.

    :::code language="cpp" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/InterstitialAdSamples/cpp/DirectXPage.xaml.cpp" id="Snippet5":::

    If you want to show an *interstitial banner* ad: Approximately 5-8 seconds before you need the ad, use the [RequestAd](/uwp/api/microsoft.advertising.winrt.ui.interstitialad.requestad) method to pre-fetch the ad. This allows enough time to request and prepare the ad before it should be shown. Be sure to specify **AdType::Display** for the ad type.

    ```cpp
    m_interstitialAd->RequestAd(AdType::Display, myAppId, myAdUnitId);
    ```

7.  At the point in your code where you want to show the ad, confirm that the **InterstitialAd** is ready to be shown and then show it by using the [Show](/uwp/api/microsoft.advertising.winrt.ui.interstitialad.show) method.

    :::code language="cpp" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/InterstitialAdSamples/cpp/DirectXPage.xaml.cpp" id="Snippet6":::

8.  Define the event handlers for the **InterstitialAd** object.

    :::code language="cpp" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/InterstitialAdSamples/cpp/DirectXPage.xaml.cpp" id="Snippet7":::

9. Build and test your app to confirm it is showing test ads.

<span id="release" />

## Release your app with live ads

1. Make sure your use of interstitial ads in your app follows our [guidelines for interstitial ads](ui-and-user-experience-guidelines.md#interstitialbestpractices10).

2.  In Partner Center, go to the [In-app ads](../publish/in-app-ads.md) page and [create an ad unit](set-up-ad-units-in-your-app.md#live-ad-units). For the ad unit type, choose **Video interstitial** or **Banner interstitial**, depending on what type of interstitial ad you are showing. Make note of both the ad unit ID and the application ID.
    > [!NOTE]
    > The application ID values for test ad units and live UWP ad units have different formats. Test application ID values are GUIDs. When you create a live UWP ad unit in Partner Center, the application ID value for the ad unit always matches the Store ID for your app (an example Store ID value looks like 9NBLGGH4R315).

3. You can optionally enable ad mediation for the **InterstitialAd** by configuring the settings in the [Mediation settings](../publish/in-app-ads.md#mediation) section on the [In-app ads](../publish/in-app-ads.md) page. Ad mediation enables you to maximize your ad revenue and app promotion capabilities by displaying ads from multiple ad networks, including ads from other paid ad networks such as Taboola and Smaato and ads for Microsoft app promotion campaigns.

4.  In your code, replace the test ad unit values with the live values you generated in Partner Center.

5.  [Submit your app](../publish/app-submissions.md) to the Store using Partner Center.

6.  Review your [advertising performance reports](../publish/advertising-performance-report.md) in Partner Center.

<span id="manage" />

## Manage ad units for multiple interstitial ad controls in your app

You can use multiple **InterstitialAd** controls in a single app. In this scenario, we recommend that you assign a different ad unit to each control. Using different ad units for each control enables you to separately [configure the mediation settings](../publish/in-app-ads.md#mediation) and get discrete [reporting data](../publish/advertising-performance-report.md) for each control. This also enables our services to better optimize the ads we serve to your app.

> [!IMPORTANT]
> You can use each ad unit in only one app. If you use an ad unit in more than one app, ads will not be served for that ad unit.

## Related topics

* [Guidelines for interstitial ads](ui-and-user-experience-guidelines.md#interstitialbestpractices10)
* [Interstitial ad sample code in C#](interstitial-ad-sample-code-in-c.md)
* [Interstitial ad sample code in JavaScript](interstitial-ad-sample-code-in-javascript.md)
* [Advertising samples on GitHub](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Advertising)
* [Set up ad units for your app](set-up-ad-units-in-your-app.md)
