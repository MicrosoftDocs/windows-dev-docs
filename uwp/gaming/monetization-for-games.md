---
title: Monetization for games
description: Implement banner ads, interstitial video ads, and in-app purchases for Universal Windows Platform (UWP) games on Windows 10.
ms.assetid: 79f4e177-d8e7-45d3-8a78-31d4c2fe298a
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, monetization
ms.localizationpriority: medium
---

#  Monetization for games

As a game developer, you need to know your monetization options so you can sustain your business and keep doing what you're passionate about: creating great games. This article provides an overview of the monetization methods for a Universal Windows Platform (UWP) game and how to implement them.

In the past, you would simply put a price on your game and then wait for people to purchase it at a store. But today you have options. You can choose to distribute a game to "brick-and-mortar" stores, sell the game online (either physical or soft copies), or let everyone play the game for free but incorporate some sort of ads or in-game items that can be purchased. Games are also no longer just standalone products. They often come with extra content that can be purchased in addition to the main game.

You can promote and monetize a UWP game in one or more of these ways:
* Put your game in the Microsoft Store, which is a secured, online store offering [worldwide distribution](#worldwide-distribution-channel). Gamers around the world can buy your game online at the [price you set](#set-a-price-for-your-game).
* Use APIs in the Windows SDK to create [in-game purchases](#in-game-purchases). Gamers can buy items from within your game, or buy additional content such as extra equipment, skins, maps, or game levels.
* Use APIs in the [Microsoft Advertising SDK](https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftAdvertisingSDK) to display ads from ad networks. You can [display ads in your game](#display-ads-in-your-game) and offer the option for gamers to watch video ads in exchange for in-game rewards.
* [Maximize your game's potential through ad campaigns](#maximize-your-games-potential-through-ad-campaigns). Promote your game using paid, community (free), or house (free) ad campaigns to grow its user base.

## Worldwide distribution channel

The Microsoft Store can make your game available for download in more than 200 countries and regions worldwide, with support for billing via various forms of payment including Visa, MasterCard, and PayPal. For a full list of countries and regions, see [Define market selection](/windows/apps/publish/publish-your-app/market-selection?pivots=store-installer-msix).

## Set a price for your game

UWP games published to the Store can be either be _paid_ or _free_. A paid game allows you to charge gamers up front for your game at a price you set, whereas a free game allows users to download and play the game without paying for it.

Here are some important concepts regarding the pricing of your game in the Store.

### Base price

The base price of the game is what determines whether your game is categorized as _paid_ or _free_. You can use [Partner Center](https://partner.microsoft.com/dashboard) to configure the base price based on country and region.
The process of determining the price may include your [tax responsibilities when selling to different countries](/partner-center/tax-details-marketplace)
and [cost considerations for specific markets](/windows/apps/publish/publish-your-app/market-selection?pivots=store-installer-msix). You can also [set custom prices for specific markets](/windows/apps/publish/publish-your-app/schedule-pricing-changes?pivots=store-installer-msix#override-base-price-for-specific-markets).

### Sale price

One way to promote your game is to reduce its price for a limited time. It's also possible to set the sale price to __Free__ to allow your game to be downloaded without payment.
You can schedule sale campaigns in advance by setting both the starting date and ending date of the sale. For more info, see [Put apps and add-ons on sale](/windows/apps/publish/put-apps-and-add-ons-on-sale).

## In-game purchases

In-game purchases are products bought within a game. They're also generically known as _in-app purchases_. In the Microsoft Store, these products are called _add-ons_. [Add-ons are published](/windows/apps/publish/publish-your-app/create-app-submission?pivots=store-installer-add-on) through Partner Center. You'll also need to enable the add-ons in your game's code.

### Types of add-ons

You can create two types of add-ons in the store: _durables_ or _consumables_. Durables are items that persist over for a specified amount of time and can be purchased only once until they expire. Consumables are items that can be purchased and used again and again.

When creating consumables, decide how you want to keep track of them &mdash; that is whether they're _developer managed_ or _Store managed_ (This feature is available starting in Windows 10, version 1607). With a developer-managed consumable, you are responsible for keeping track of the item's balance for the gamer; with a Store-managed consumable, the Microsoft Store keeps track of the item's balance for you. For more info, see [Overview of consumable add-ons](../monetize/enable-consumable-add-on-purchases.md).

### Create in-game purchases

The latest in-app purchases and license info APIs are part of the [Windows.Services.Store](/uwp/api/windows.services.store) namespace in the Windows SDK (starting in Windows 10, version 1607). If you're developing a new game that targets 1607 or later release, we recommend that you use the __Windows.Services.Store__ namespace because it supports the latest add-on types and has better performance.
It's also designed to be compatible with future types of products and features supported by the Partner Center and the Store. When developing for previous versions of Windows 10, use the [Windows.ApplicationModel.Store](/uwp/api/windows.applicationmodel.store) namespace instead.

For more info, go to [In-app purchases and trials](../monetize/in-app-purchases-and-trials.md).

#### Simplified purchase example

This section uses a simplified purchase example to illustrate the use of different method calls to implement the purchase flow.

|In-game actions / activity                                                | Game background tasks                  |
|--------------------------------------------------------------------------|----------------------------------------|
|Gamer enters a shop. Shop menu pops up to display the available add-ons and purchase price |  Game [retrieves the product info](../monetize/get-product-info-for-apps-and-add-ons.md) of the add-ons, [determines whether the add-ons have the proper license](../monetize/get-license-info-for-apps-and-add-ons.md), and displays the add-ons that are available for purchase by the gamer on the shop menu.                           |
|Gamer clicks __Buy__ to purchase an item             |The __Buy__ action sends a request to purchase the item and starts the payment process to acquire it. The implementation varies depending on the item type. If it is [a durable or a one-time purchase item](../monetize/enable-in-app-purchases-of-apps-and-add-ons.md), the customer can own only a single item until it expires. If the item is a [consumable](../monetize/enable-consumable-add-on-purchases.md), the customer can own one or more of it. |

### Test in-game purchases during game development

Because an add-on must be created in association with a game, your game must be published and available in the Store. The steps in this section show how to create add-ons while your game is still in development.
(If your finished game is already live in the Store, you can skip the first three steps and go directly to [Create an add-on in the Store](#create-an-add-on-in-the-store).)

To create add-ons while your game is still in development:
1. [Create a package](#create-a-package)
2. [Publish the game as hidden](#publish-the-game-as-hidden)
3. [Associate your game solution in Visual Studio with the Store](#associate-your-game-solution-with-the-store)
4. [Create an add-on in the Store](#create-an-add-on-in-the-store)

#### Create a package

For any game to be published, it must meet the minimum Windows App Certification requirements. You can use the [Windows App Certification Kit](../debug-test-perf/windows-app-certification-kit.md), which is part of the Windows SDK,
to run tests on the game to help ensure that it's ready for publishing to the Store. If you haven't already downloaded the Windows SDK that includes the Windows App Certification Kit, then go to [Windows SDK](https://developer.microsoft.com/windows/downloads/windows-sdk/).

To create a package that can be uploaded to the Store:

1. Open your game solution in Visual Studio.
2. Within Visual Studio, go to __Project__ > __Store__ > __Create App Packages ...__
3. For the __Do you want to build packages to upload to the Microsoft Store?__ option, select __Yes__.
4. Sign in to your [Partner Center](https://partner.microsoft.com/dashboard) developer account. Or [register as a developer in Partner Center](https://developer.microsoft.com/microsoft-store/register/).
5. Select an app to create the upload package for. If you have not yet created an app submission, provide a new app name to create a new submission. For more info, see [Create your app by reserving a name](/windows/apps/publish/publish-your-app/reserve-your-apps-name?pivots=store-installer-msix).
6. After the package has been created successfully, click __Launch Windows App Certification Kit__ to start the testing process.
7. Fix any errors to create a game package.

#### Publish the game as hidden

1. Go to [Partner Center](https://partner.microsoft.com/dashboard) and sign in.
2. From the __Dashboard overview__ or __All apps__ page, click the app you want to work with. If you have not yet created an app submission, click on __Create a new app__ and reserve a name.
3. On the __App Overview__ page, click __Start your submission__.
4. Configure this new submission. On the submission page:
    * Click __Pricing and availability__. In the __Visibility__ section, choose '__Hide this app and prevent acquisition...__' to ensure only your development team has access to the game. For more details, go to [Distribution and visibility](/windows/apps/publish/publish-your-app/price-and-availability?pivots=store-installer-msix).
    * Click __Properties__. In the __Category and subcategory__ section, choose __Games__ and then a suitable subcategory for your game.
    * Click __Age ratings__. Fill out the questionnaire accurately.
    * Click __Packages__. Upload the game package created in the earlier step.
5. Follow any other submission prompts in the dashboard to allow you to successfully publish this game which remains hidden to the public.
6. Click __Submit to the Store__.

For more info, go to [App submissions](/windows/apps/publish/publish-your-app/create-app-submission?pivots=store-installer-msix).

After your game is submitted to the Store, it enters the [app certification process](/windows/apps/publish/publish-your-app/app-certification-process?pivots=store-installer-msix). This process can take up to 16 hours before the game is listed.

#### Associate your game solution with the Store

With your game solution opened in Visual Studio:

1. Go to __Project__ > __Store__ > __Associate App with the Store ...__
2. Sign in to your Partner Center developer account and select the app name to associate this solution with.
3. Double-click on the __Package.appxmanifest.xml file__ and go to the __Packaging__ tab to check that the game is associated correctly.

If you have associated the solution to a published game that is live and listed in the Store, your solution will have an active license and you are one step closer in creating add-ons for your game. For more info, see [Packaging apps](../packaging/index.md).

#### Create an add-on in the Store

As you create add-ons, make sure you're associating them with the right game submission. For details about how to configure all the various info associated with an add-on, see [Add-on submissions](/windows/apps/publish/publish-your-app/create-app-submission?pivots=store-installer-add-on).

1. Go to [Partner Center](https://partner.microsoft.com/dashboard) and sign in.
2. From the __Dashboard overview__ or __All apps__ page, click the app you want to create the add-on for.
3. On the __App Overview__ page, in the __Add-ons__ section, select __Create a new add-on__.
4. Select the product type for the add-on: __developer-managed consumable__, __store-managed consumable__, or __durable__.
5. Enter a unique product ID which will be used as a string variable when integrating this add-on into your game code. This ID will not be seen by consumers. For more info, see [Set your app product type and product ID](/windows/apps/publish/publish-your-app/create-app-store-listing?pivots=store-installer-add-on).

Other configurations for add-ons include:
* [Properties](/windows/apps/publish/publish-your-app/enter-app-properties?pivots=store-installer-add-on)
* [Pricing and availability](/windows/apps/publish/publish-your-app/price-and-availability?pivots=store-installer-add-on)
* [Store listing](/windows/apps/publish/publish-your-app/create-app-store-listing?pivots=store-installer-add-on)

If your game has many add-ons, you can create them programmatically by using the __Microsoft Store submission API__. For more info, see [Create and manage submissions using Microsoft Store services](../monetize/create-and-manage-submissions-using-windows-store-services.md).

## Display ads in your game

The libraries and tools in the Microsoft Advertising SDK help you set up a service in your game to receive ads from an ad network. Your gamers will be shown live ads and you'll earn money from the advertisers when your gamers view or interact with the displayed ads.
For more info, see [Display ads in your app](../monetize/display-ads-in-your-app.md).

### Ad formats

Several types of ads can be displayed by using the Microsoft Advertising SDK:

* Banner ads &mdash; Ads that take up a part of your gaming screen and are usually placed within a game.
* Interstitial video ads &mdash; Full-screen ads, which can be very effective when used between levels. If implemented properly, they can be less obtrusive than banner ads.
* Native ads &mdash; Component-based ads, where each piece of the ad creative (such as the title, image, description, and call-to-action text) is delivered to your app as an individual element that you can integrate into your app.

### Which ads are displayed?

By default, your app will show ads from Microsoft's network for paid ads. To maximize your ad revenue, you can enable ad mediation for your ad unit to display ads from additional paid ad networks. For more info about current offerings, see our [Mediation settings](/windows/apps/publish/in-app-ads#mediation-settings) guidance.

### Which markets allow ads to be displayed?

For the full list of countries and regions that support ads, see [Supported markets for ad networks](/windows/apps/publish/in-app-ads#supported-markets-for-ad-networks).

### APIs for displaying ads

The [AdControl](/uwp/api/microsoft.advertising.winrt.ui.adcontrol), [InterstitialAd](/uwp/api/microsoft.advertising.winrt.ui.interstitialad), and [NativeAd](/uwp/api/microsoft.advertising.winrt.ui.nativead) classes in the Microsoft Advertising SDK are used to help display ads in games.

To get started, download and install the [Microsoft Advertising SDK](https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftAdvertisingSDK) with Visual Studio 2015 or a later version. For more info, see [Install the Microsoft Advertising SDK](../monetize/install-the-microsoft-advertising-libraries.md).

#### Implementation guides

These walkthroughs show how to implement ads by using __AdControl__, __InterstitialAd__, and __NativeAd__:

* [Create banner ads in XAML and .NET](../monetize/adcontrol-in-xaml-and--net.md)
* [Create banner ads in HTML5 and JavaScript](../monetize/adcontrol-in-html-5-and-javascript.md)
* [Create interstitial ads](../monetize/interstitial-ads.md)
* [Create native ads](../monetize/native-ads.md)

During development, you can make use of the [test ad unit values](../monetize/set-up-ad-units-in-your-app.md) to see how the ads are rendered. These test ad unit values are also used in the walkthroughs above.

Here are some best practices to help you in the design and implementation process.

* [Best practices for banner ads](../monetize/ui-and-user-experience-guidelines.md)
* [Best practices for interstitial ads](../monetize/ui-and-user-experience-guidelines.md)

For solutions to common development issues, like ads not appearing, black box blinking and disappearing, or ads not refreshing, see [Troubleshooting guides](../monetize/known-issues-for-the-advertising-libraries.md).

### Prepare for release by replacing ad unit test values

When you're ready to move to live testing or to receive ads in published games, you must update the test ad unit values to the actual values provided for your game. To create ad units for your game, see [Set up ad units in your app](../monetize/set-up-ad-units-in-your-app.md).

### Other ad networks

These are other ad networks that provide SDKs for serving ads to UWP apps and games.

#### Vungle

The Vungle SDK for Windows offer video ads in apps and games. To download the SDK, go to [Vungle SDK](https://support.vungle.com/hc/en-us/articles/15733752671131).

#### Smaato

Smaato enables banner ads to be incorporated into UWP apps and games. Download the [SDK](https://www.smaato.com/resources/sdks/) and for more info, see the [documentation](https://developers.smaato.com/publishers/?_ga=2.99600542.1486058893.1655998537-855372014.1655998537).

#### AdDuplex

You can use AdDuplex to implement banner or interstitial ads in your game.

To learn more about integrating AdDuplex directly into a Windows 10 XAML project, go to the AdDuplex website:
* Banner ads: [Windows 10 SDK for XAML](https://adduplex.zendesk.com/hc/en-us/articles/204849031-Windows-10-SDK-for-XAML-apps-installation-and-usage)
* Interstitial ads: [Windows 10 XAML AdDuplex Interstitial Ad Installation and Usage](https://adduplex.zendesk.com/hc/en-us/articles/204849091-Windows-10-XAML-AdDuplex-Interstitial-Ad-Installation-and-Usage)

For info about integrating the AdDuplex SDK into Windows 10 UWP games created using Unity, see [Windows 10 SDK for Unity apps installation and usage](https://adduplex.zendesk.com/hc/en-us/articles/207279435-Windows-10-SDK-for-Unity-apps-installation-and-usage).

## Maximize your game's potential through ad campaigns

Take the next step in promoting your game using ads. When you [create an ad campaign](../monetize/index.md) for your game, other apps and games will display ads promoting your game.

Choose from several types of campaigns that can help increase your gamer base.

|Campaign type             | Ads for your game appear in...                                                                                                                                                                   |
|--------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
|Paid                      |Apps that match your game’s device or category.                                                                                                                                                   |
|Free community	           |Apps published by other developers who have also opted in to community ad campaigns. For more info, see [About community ads](../monetize/index.md).|
|Free house	               |Only apps that you've published. For more info, see [About house ads](../monetize/index.md).                                                            |

## Related links

* [Getting paid](/partner-center/marketplace-get-paid)
* [Account types, locations, and fees](/windows/apps/publish/partner-center/account-types-locations-and-fees)
* [Analytics](/windows/apps/publish/analytics)
* [Globalization and localization](/windows/apps/design/globalizing/globalizing-portal)
* [Implement a trial version of your app](../monetize/implement-a-trial-version-of-your-app.md)
* [Run app experiments with A/B testing](../monetize/run-app-experiments-with-a-b-testing.md)
