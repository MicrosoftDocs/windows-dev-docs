---
ms.assetid: 7a38a352-6e54-4949-87b1-992395a959fd
description: See guidelines for providing great UI and user experiences with banner ads, interstitial ads, and native ads in your apps.
title: UI and user experience guidelines for ads
ms.date: 02/18/2020
ms.topic: article
keywords: windows 10, uwp, ads, advertising, guidelines, best practices
ms.localizationpriority: medium
---
# UI and user experience guidelines for ads

>[!WARNING]
> As of June 1, 2020, the Microsoft Ad Monetization platform for Windows UWP apps will be shut down. [Learn more](https://social.msdn.microsoft.com/Forums/windowsapps/en-US/db8d44cb-1381-47f7-94d3-c6ded3fea36f/microsoft-ad-monetization-platform-shutting-down-june-1st?forum=aiamgr)

This article provides guidelines for providing great experiences with banner ads, interstitial ads, and native ads in your apps. For general guidance about how to design the look and feel for apps, see [Design & UI](https://developer.microsoft.com/windows/apps/design).

> [!IMPORTANT]
> Any use of advertising in your app must comply with the Microsoft Store Policies – including, without limitation, [policy 10.10](/windows/apps/publish/store-policies-and-code-of-conduct#1010-advertising-conduct-and-content) (Advertising Conduct and Content). In particular, your app's implementation of banner ads or interstitial ads must meet the requirements in Microsoft Store [policy 10.10.1](/windows/apps/publish/store-policies-and-code-of-conduct#1010-advertising-conduct-and-content). This article includes examples of implementations that would violate this policy. These examples are provided for informational purposes only, as a way to help you better understand the policy. These examples are not comprehensive, and there may be many other ways to violate the Microsoft Store Policies that are not listed in this article.

## General best practices

Before reviewing our guidelines for different types of ads in this article, first review these general best practices to improve your ad revenue.

* [Plan your ad placements carefully](https://blogs.windows.com/buildingapps/2017/04/10/monetizing-app-advertisement-placement/). See our related guidance about [optimizing the viewability of your ad units](optimize-ad-unit-viewability.md).
* [Use interstitial banner ads as a fallback for interstitial video ads](https://blogs.windows.com/buildingapps/2017/04/17/monetizing-app-use-interstitial-banner-fallback-interstitial-video/).
* [Know your users to serve better targeted ads](https://blogs.windows.com/buildingapps/2017/05/17/monetize-app-know-user-serve-better-targeted-ads/).
* [Use the latest advertising libraries](https://blogs.windows.com/buildingapps/2017/05/22/earn-money-moving-latest-advertising-libraries/).
* [Set the correct COPPA settings for your app](https://blogs.windows.com/buildingapps/2017/06/21/monetizing-app-set-coppa-settings-app/).


## Guidelines for banner ads

The following sections provide recommendations for how to implement [banner ads](banner-ads.md) in your app using [AdControl](/uwp/api/microsoft.advertising.winrt.ui.adcontrol) and examples of implementations that violate [policy 10.10.1](/windows/apps/publish/store-policies-and-code-of-conduct#1010-advertising-conduct-and-content) of the Microsoft Store Policies.

### Best practices

We recommend that you follow these best practices when you implement banner ads in your app:

* [Use Interactive Advertising Bureau sizes](https://blogs.windows.com/buildingapps/2017/04/03/monetizing-app-use-interactive-advertising-bureau-ad-sizes/) that fit well with the layout for the device.

* Devote most of your app's UI to functional controls and content.

* Design advertising into your experience. Give your designers a sample ad to plan how the advertising is going to look. Two examples of well-planned ads in apps are the ads-as-content layout and the split layout.

  To see how different ad sizes will look and function within your app during the development and testing phase, you can utilize our [test ad units](set-up-ad-units-in-your-app.md#test-ad-units). When you’re done with testing, remember to [update your app with live ad units](set-up-ad-units-in-your-app.md#live-ad-units) before submitting the app for certification.

* Plan for times when no ads are available. There may be times when ads aren't being sent to your app. Lay out your pages in such a way that they look great whether they showcase an ad or not. For more information, see [Handle ad errors](error-handling-with-advertising-libraries.md).

* If you have a scenario for alerting the user that is best handled with an overlay, call [AdControl.Suspend](/uwp/api/microsoft.advertising.winrt.ui.adcontrol.suspend) while displaying the overlay and then call [AdControl.Resume](/uwp/api/microsoft.advertising.winrt.ui.adcontrol.resume) when the alert scenario is finished.

### Practices to avoid

We recommend that you avoid these practices when you implement banner ads in your app:

* Don’t bolt advertising into open real estate. Ad space shouldn't be placed into the first open piece of real estate you can find. Instead, it should be incorporated into your app's overall design.

* Don’t over-advertise and saturate your app. Too many ads in your app detract from its appearance and usability. You want to make money with advertising, but not at the expense of the app itself.

* Don’t distract user from their core tasks. The primary focus should always be on the app. The ad space should be incorporated so it remains a secondary focus.

### Examples of policy violations

This section provides examples of banner ad scenarios that violate [policy 10.10.1](/windows/apps/publish/store-policies-and-code-of-conduct#1010-advertising-conduct-and-content) of the Microsoft Store Policies. These examples are provided for instructional purposes only, as a way to help you better understand the policy. These examples are not comprehensive, and there may be many other ways to violate policy 10.10.1 that are not listed here.

* Doing anything to interfere with the user’s ability to view the banner ad, such as changing the opacity of the [AdControl](/uwp/api/microsoft.advertising.winrt.ui.adcontrol) or placing another control on top of the **AdControl** (without first calling [AdControl.Suspend](/uwp/api/microsoft.advertising.winrt.ui.adcontrol.suspend)).

* Requiring users to click on a banner ad to accomplish a task in your app, or forcing users to click on banner ads as a result of the design of your app.

* Bypassing the built-in minimum refresh timer for banner ads by any means, including (but not limited to) swapping [AdControl](/uwp/api/microsoft.advertising.winrt.ui.adcontrol) objects or forcing a page refresh without user interaction.

* Using live ad units (that is, ad units that you obtain from Partner Center) during development and testing, or in an emulator.

* Writing or distributing code that calls ad services through means other than the Microsoft advertising libraries running in the context of your app.

* Interacting with undocumented interfaces or child objects created by the Microsoft advertising libraries, such as **WebView** or **MediaElement**.

* Placing ads in a viewbox to reduce the size of the ads in order to allow more ads on a page than normal.

<span id="interstitialbestpractices10"></span>

## Guidelines for interstitial ads

When used elegantly, [interstitial ads](interstitial-ads.md) can vastly increase your app revenue, without negatively impacting user satisfaction. When used improperly, such ads can have the exact opposite effect.

The following sections provide recommendations for how to implement interstitial video ads and interstitial banner ads in your app using [InterstitialAd](/uwp/api/microsoft.advertising.winrt.ui.interstitialad), and examples of implementations that violate [policy 10.10.1](/windows/apps/publish/store-policies-and-code-of-conduct#1010-advertising-conduct-and-content) of the Microsoft Store Policies. Since you know your app better than anyone, except where policy is concerned, we leave it up to you to make the best final decision. What’s most important to keep in mind is that your app ratings and revenue are tightly coupled.

### Best practices

We recommend that you follow these best practices when you implement interstitial ads in your app:

* Fit interstitial ads within the natural flow of the app, such as between game levels.

* Associate ads with tangible upsides, such as:

    * Hints towards level completion.

    * Extra time to retry a level.

    * Custom avatar features, like a tattoo or hat.

* If your app requires that an interstitial video ad be watched to completion, mention that rule upfront so they aren’t surprised with an error message upon hitting the close button.

* Pre-fetch the ad (by calling [InterstitialAd.RequestAd](/uwp/api/microsoft.advertising.winrt.ui.interstitialad.requestad)) ideally 30-60 seconds before you need to show it.

* Subscribe to all four events exposed in the [InterstitialAd](/uwp/api/microsoft.advertising.winrt.ui.interstitialad) class (**Canceled**, **Completed**, **AdReady**, and **ErrorOccurred**) and use them to make the right decisions for your app.

* Have some built-in experience to use in lieu of a server-matched ad. You’ll find this useful in a few scenarios:

    * Offline mode, when ad servers can’t be reached.

    * When the **ErrorOccurred** event fires.

    * If you opt to save user bandwidth based on [ConnectionProfile](/uwp/api/Windows.Networking.Connectivity.ConnectionProfile), there are APIs in the **ConnectionProfile** class which can help.

* Use the default (30 second) timeout unless you have a valid reason to do otherwise, in which case don’t go below 10 seconds. Interstitial ads take substantially longer to download than standard banner ads, especially in markets that don’t have high speed connections.

* Be mindful of the user’s data plan. For example, either don’t show, or warn user, before serving an interstitial video ad on a mobile device that is near/over its data limit. There are APIs in the [ConnectionProfile](/uwp/api/Windows.Networking.Connectivity.ConnectionProfile) class which can help.

* Continuously improve your app after the initial submission. Look at the [ad reports](/windows/apps/publish/advertising-performance-report) and make design changes to improve fill and interstitial video completion rates.

### Practices to avoid

We recommend that you avoid these practices when you implement interstitial ads in your app:

* Don’t overdo it. Don’t force ads more than every 5 minutes or so, unless the user explicitly engages with an optional tangible benefit, beyond just playing the game.

* Don’t show interstitials at app launch, since users may believe they clicked the wrong tile.

* Don’t show interstitials at exit. This is bad inventory, since completion rates will be near zero.

* Don't show two or more interstitial ads back to back. Users will be frustrated to see the ad progress bar reset to the starting point. Many will think it’s just a coding or ad serving bug.

* Don’t fetch an interstitial video ad more than 5 minutes before calling [InterstitialAd.Show](/uwp/api/microsoft.advertising.winrt.ui.interstitialad.show). Good inventory will maximize the conversion of pre-fetched ads to billable impressions.

* Don’t penalize a user for failures in ad serving, such as no ad available. For example, if you show a UI option to “Watch an ad to get *xxx*”, you should provide *xxx* if the user did her part. Two options to consider:

    * Don’t include the option unless the [InterstitialAd.AdReady](/uwp/api/microsoft.advertising.winrt.ui.interstitialad.adready) event has fired.

    * Have the app include a built-in experience that yields the same benefit as a real ad.

* Don’t use interstitial ads to let a user gain a competitive advantage in a multi-player game. For example, don't entice the user with a better gun in a first-person shooter game if they view an interstitial ad. A custom shirt on the player’s avatar is fine, so long as it doesn’t provide camouflage!

### Examples of policy violations

This section provides examples of interstitial ad scenarios that violate [policy 10.10.1](/windows/apps/publish/store-policies-and-code-of-conduct#1010-advertising-conduct-and-content) of the Microsoft Store Policies. These examples are provided for instructional purposes only, as a way to help you better understand the policy. These examples are not comprehensive, and there may be many other ways to violate policy 10.10.1 that are not listed here.

* Placing a UI element over the interstitial ad container.

* Calling [InterstitialAd.Show](/uwp/api/microsoft.advertising.winrt.ui.interstitialad.show) while the user is engaged with the app.

* Using interstitial ads to obtain anything that may be consumed as a currency or traded with other users.

* Requesting a new interstitial ad in the context of the event handler for the [InterstitialAd.ErrorOccurred](/uwp/api/microsoft.advertising.winrt.ui.interstitialad.erroroccurred) event. This can result in an infinite loop and can cause operational issues for the advertising service.

* Requesting an interstitial ad merely to have a backup ad for a waterfall sequence of ads. If you request an interstitial ad and then receive the [InterstitialAd.AdReady](/uwp/api/microsoft.advertising.winrt.ui.interstitialad.adready) event, the next interstitial ad shown in your app must be the ad that is ready to be shown via the [InterstitialAd.Show](/uwp/api/microsoft.advertising.winrt.ui.interstitialad.show) method.

* Using live ad units (that is, ad units that you obtain from Partner Center) during development and testing, or in an emulator.

* Writing or distributing code that calls ad services through means other than the Microsoft advertising libraries running in the context of your app.

* Interacting with undocumented interfaces or child objects created by the Microsoft advertising libraries, such as **WebView** or **MediaElement**.

## Guidelines for native ads

[Native ads](native-ads.md) give you have a lot of control over how you present advertising content to your users. Follow these requirements and guidelines to help ensure that the advertiser's message reaches your users while also helping to avoid delivering a confusing native ads experience to your users.

### Register the container for your native ad

In your code, you must call the [RegisterAdContainer](/uwp/api/microsoft.advertising.winrt.ui.nativeadv2.registeradcontainer) method of the [NativeAdV2](/uwp/api/microsoft.advertising.winrt.ui.nativeadv2) object to register the UI element that acts as a container for the native ad and optionally any specific controls that you want to register as clickable targets for the ad. This is required to properly track ad impressions and clicks.

There are two overloads for the **RegisterAdContainer** method that you can use:

* If you want the entire container for all the individual native ad elements to be clickable, call the **RegisterAdContainer(FrameworkElement)** method and pass the container control to the method. For example, if you display all of the native ad elements in separate controls that are all hosted in a **StackPanel** and you want the entire **StackPanel** to be clickable, pass the **StackPanel** to this method.

* If you want only certain native ad elements to be clickable, call the **RegisterAdContainer(FrameworkElement, IVector(FrameworkElement))** method. Only the controls that you pass to the second parameter will be clickable.

### Required native ad elements

At a minimum, you must always show the following native ad elements provided by properties of the [NativeAdV2](/uwp/api/microsoft.advertising.winrt.ui.nativeadv2) object to the user in your native ad design. If you fail to include these elements, you may see poor performance and low yields for your ad unit.

1. Always display the title of the native ad (available in the **Title** property). Provide enough space to display at least 25 characters. If the title is longer, replace the additional text with an ellipsis.
2. Always display least one of the following elements to help differentiate the native ad experience from the rest of your app and clearly call out that the content is provided by an advertiser:
    * The distinguishable *ad* icon (available in the **AdIcon** property). This icon is supplied by Microsoft.
    * The *sponsored by* text (available in the **SponsoredBy** property). This text is supplied by the advertiser.
    * As an alternative to the *sponsored by* text, you can choose to display some other text that helps differentiate the native ad experience from the rest of your app, such as "Sponsored content", "Promotional content", "Recommended content", etc.

### User experience

Your native ad should be clearly delineated from the rest of your app and have space around it to prevent accidental clicks. Use borders, different backgrounds, or some other UI to separate the ad content from the rest of your app. Keep in mind that accidental ad clicks are not beneficial for your ad-based revenue or your end user experience in the long term.

### Description

If you choose to show the description for the ad (available in the **Description** property of the **NativeAdV2** object), provide enough space to display at least 75 characters. We recommend that you use an animation to show the full content of the ad description.

### Call to action

The *call to action* text (available in the **CallToAction** property of the **NativeAdV2** object) is a critical component of the ad. If you choose to show this text, follow these guidelines:

* Always display the *call to action* text to the user on a clickable control such as a button or hyperlink.
* Always display the *call to action* text in its entirety.
* Ensure that that the *call to action* text is separated from the rest of the promotional text from the advertiser.
