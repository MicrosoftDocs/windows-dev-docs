---
ms.assetid: 63A9EDCF-A418-476C-8677-D8770B45D1D7
description: The Microsoft Advertising SDK gives you several ways to monetize your app with ads.
title: Display ads in your app with the Microsoft Advertising SDK
ms.date: 02/18/2020
ms.topic: article
keywords: windows 10, uwp, ads, advertising, banner, ad control, interstitial
ms.localizationpriority: medium
---
# Display ads in your app with the Microsoft Advertising SDK

>[!WARNING]
> As of June 1, 2020, the Microsoft Ad Monetization platform for Windows UWP apps will be shut down. [Learn more](https://social.msdn.microsoft.com/Forums/windowsapps/en-US/db8d44cb-1381-47f7-94d3-c6ded3fea36f/microsoft-ad-monetization-platform-shutting-down-june-1st?forum=aiamgr)

Increase your revenue opportunities by putting ads in your Universal Windows Platform (UWP) app for Windows 10 or Windows 11 by using the Microsoft Advertising SDK. Our ad monetization platform offers a variety of ad formats that can be seamlessly integrated into your apps and supports mediation with many popular ad networks. Our platform is compliant with the OpenRTB, VAST 2.x, MRAID 2, and VPAID 3 standards and is compatible with MOAT and IAS. 

<br/>

<table>
<colgroup>
<col width="10%" />
<col width="23%" />
<col width="10%" />
<col width="23%" />
<col width="10%" />
<col width="23%" />
</colgroup>
<tbody>
<tr>
<td align="left"><img src="images/install-sdk.png" alt="Install SDK icon" /></td>
<td align="left"><b>Get started</b><br/><br/>
    <a href="https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftAdvertisingSDK">Install the Microsoft Advertising SDK</a>
</td>
<td align="left"><img src="images/write-code.png" alt="Develop icon" /></td>
<td align="left"><b>Developer guides</b><br/><br/>
    <a href="banner-ads.md">Banner ads</a>
    <br/>
    <a href="interstitial-ads.md">Interstitial ads</a>
    <br/>
    <a href="native-ads.md">Native ads</a>
    </td>
<td align="left"><img src="images/api-reference.png" alt="API ref icon" /></td>
<td align="left"><b>Other resources</b><br/><br/>
    <a href="set-up-ad-units-in-your-app.md">Set up ad units in your app</a>
    <br/>
    <a href="best-practices-for-ads-in-apps.md">Best practices</a>
    <br/>
    <a href="/uwp/api/overview/advertising">API reference</a>
    </td>
</tr>
</tbody>
</table>

## Step 1: Install the Microsoft Advertising SDK

To get started, install the [Microsoft Advertising SDK](https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftAdvertisingSDK) on the development computer you use to build your app. For installation instructions, see [this article](install-the-microsoft-advertising-libraries.md).

## Step 2: Implement ads in your app

The Microsoft Advertising SDK provides several different types of ad controls you can use in your app. Choose which types of ads are best for your scenario and then add code to your app to display those ads. During this step, you will use a test ad unit so you can see how your app renders ads during testing.

### Banner ads

These are static display ads that utilize a rectangular portion of a page in your app to display promotional content. These ads can refresh automatically at regular intervals. This is a good place to start if you are new to advertising in your app.

For instructions and code examples, see [this article](adcontrol-in-xaml-and--net.md).

![An image depicting a banner advertisement on a tablet.](images/banner-ad.png)

### Interstitial video and interstitial banner ads

These are full-screen ads that typically require the user to watch a video or click through them to continue in the app or game. We support two types of interstitial ads: video and banner.

For instructions and code examples, see [this article](interstitial-ads.md).

![An image depicting an interstitial advertisement in a game that is being played on a tablet.](images/interstitial-ad.png)

### Native ads

These are component-based ads. Each piece of the ad creative (such as the title, image, description, and call-to-action text) is delivered to your app as an individual element that you can integrate into your app using your own fonts, colors, and other UI components.

For instructions and code examples, see [this article](native-ads.md).

![An image depicting a native advertisement that can be displayed on various devices.](images/native-ad.png)

<span id="ad-mediation"></span>

## Step 3: Create an ad unit and configure mediation

After you finish testing your app and you are ready to submit it to the Store, create an ad unit on the [In-app ads](/windows/apps/publish/in-app-ads) page in Partner Center. Then, update your app code to use this ad unit so that your app will receive live ads. For more information, see [Set up ad units in your app](set-up-ad-units-in-your-app.md#live-ad-units).

By default, your app will show ads from Microsoft's network for paid ads. To maximize your ad revenue, you can enable [ad mediation](ad-mediation-service.md) for your ad unit to display ads from additional paid ad networks such as Taboola and Smaato. You can also increase your app promotion capabilities by serving ads from Microsoft app promotion campaigns.

To start using ad mediation in your UWP app, [configure ad mediation settings](/windows/apps/publish/in-app-ads#mediation-settings) for your ad unit. By default, we automatically configure the mediation settings using machine-learning algorithms to help you maximize your ad revenue across the markets your app supports. However, you also have the option to manually choose the networks you want to use. Either way, the mediation settings are configured entirely on our servers; you do not need to make any code changes in your app.    

## Step 4: Submit your app and review performance

After you finish developing your app with ads, you can [submit your updated app](/windows/apps/publish/publish-your-app/create-app-submission?pivots=store-installer-msix) in Partner Center to make it available in the Store. Apps that display ads must meet the additional requirements that are specified in [section 10.10 of the Microsoft Store Policies](/windows/apps/publish/store-policies-and-code-of-conduct#1010-advertising-conduct-and-content) and [Exhibit E of the App Developer Agreement](/legal/windows/agreements/app-developer-agreement).

After your app is published and available in the Store, you can review your [advertising performance reports](/windows/apps/publish/advertising-performance-report) in Partner Center and continue to make changes to your mediation settings to optimize the performance of your ads. Your advertising revenue is included in your [payout summary](/partner-center/payout-statement).

<span id="additional-help"></span>

## Additional help

For additional help using the Microsoft Advertising SDK, use the following resources.

|  Task    | Resource |               
|----------|-------|
| Report a bug or get assisted support for advertising     | Visit the [support page](https://developer.microsoft.com/windows/support) and choose **Ads-In-Apps**.        |
| Get community support     | Visit the [forum](https://social.msdn.microsoft.com/forums/windowsapps/en-US/home?category=windowsapps).       |
| Download sample projects that demonstrate how to add banner and interstitial ads to apps.     | See the [Advertising samples on GitHub](https://github.com/microsoft/Windows-universal-samples/tree/b1cb20f191d3fd99ce89df50c5b7d1a6e2382c01/archived/Advertising).       |
| Learn about the latest monetization opportunities for Windows apps     | Visit [Monetize your apps](https://developer.microsoft.com/store/monetize).        |

## Windows 8.1 and Windows Phone 8.x apps

For Windows 8.1 and Windows Phone 8.x apps, we provide the [Microsoft Advertising SDK for Windows and Windows Phone 8.x](https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftAdvertisingSDKforWindowsandWindowsPhone8x). For more information about using this SDK to show ads in Windows 8.1 and Windows Phone 8.x apps, see [this article](/previous-versions/windows/apps/dn792120(v=win.10)).

## Related topics

* [Microsoft Advertising SDK](https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftAdvertisingSDK)
* [Advertising performance report](/windows/apps/publish/advertising-performance-report)
* [Windows Premium Ads Publishers Program](windows-premium-ads-publishers-program.md)
