---
title: Monetization for games
description: Implement banner ads, interstitial video ads, and in-app purchases for games on Windows 10 and Windows 11.
ms.assetid: 79f4e177-d8e7-45d3-8a78-31d4c2fe298a
ms.date: 01/14/2026
ms.topic: article
keywords: windows 10, windows 11, games, monetization
ms.localizationpriority: medium
---

# Monetization for games

As a game developer, you need to know your monetization options so you can sustain your business and keep doing what you're passionate about: creating great games. 

In the past, you would simply put a price on your game and then wait for people to purchase it at a store. But today you have options. You can choose to distribute a game to "brick-and-mortar" stores, sell the game online (either physical or soft copies), or let everyone play the game for free but incorporate some sort of ads or in-game items that can be purchased. Games are also no longer just standalone products. They often come with extra content that players can buy in addition to the main game.

You can promote and monetize a game in one or more of these ways:
* Put your game in the Microsoft Store, which is a secure, online store offering [worldwide distribution](#worldwide-distribution-channel). Gamers around the world can buy your game online at the [price you set](#set-a-price-for-your-game).
* Use APIs in the Windows SDK to create [in-game purchases](#in-game-purchases). Gamers can buy items from within your game, or buy additional content such as extra equipment, skins, maps, or game levels.
* Work with any third party ad provider SDKs, such as Vungle, Rise (Unity), and Pubfinity, to display ads in your game.


## Worldwide distribution channel

The Microsoft Store makes your game available for download in more than 200 countries and regions worldwide. It supports billing through various forms of payment, including Visa, MasterCard, and PayPal. For a full list of countries and regions, see [Define market selection](/windows/apps/publish/publish-your-app/market-selection?pivots=store-installer-msix).

## Set a price for your game

You can publish your game to the Store as either _paid_ or _free_. A paid game lets you charge gamers up front at the price you set. A free game lets users download and play the game without paying.

Here are some important concepts about pricing your game in the Store.

### Base price

The base price of the game determines whether your game is categorized as _paid_ or _free_. You can use [Partner Center](https://partner.microsoft.com/dashboard) to set the base price based on country and region.
The process of determining the price might include your [tax responsibilities when selling to different countries and regions](/partner-center/tax-details-marketplace)
and [cost considerations for specific markets](/windows/apps/publish/publish-your-app/market-selection?pivots=store-installer-msix). You can also [set custom prices for specific markets](/windows/apps/publish/publish-your-app/schedule-pricing-changes?pivots=store-installer-msix#override-base-price-for-specific-markets).

### Sale price

One way to promote your game is to reduce its price for a limited time. You can also set the sale price to **Free** to allow your game to be downloaded without payment.
You can schedule sale campaigns in advance by setting both the starting date and ending date of the sale. For more info, see [Put apps and add-ons on sale](/windows/apps/publish/put-apps-and-add-ons-on-sale).

## In-game purchases

In-game purchases are products bought within a game. They're also generically known as _in-app purchases_. In the Microsoft Store, these products are called _add-ons_. [Add-ons are published](/windows/apps/publish/publish-your-app/create-app-submission?pivots=store-installer-add-on) through Partner Center. You also need to enable the add-ons in your game's code.

### Types of add-ons

You can create two types of add-ons in the store: _durables_ or _consumables_. Durables are items that persist for a specified amount of time and can be purchased only once until they expire. Consumables are items that you can purchase and use again and again.

When creating consumables, decide how you want to keep track of them &mdash; that is whether they're _developer managed_ or _Store managed_ (This feature is available starting in Windows 10, version 1607). With a developer-managed consumable, you are responsible for keeping track of the item's balance for the gamer; with a Store-managed consumable, the Microsoft Store keeps track of the item's balance for you. For more info, see [Overview of consumable add-ons](../monetize/enable-consumable-add-on-purchases.md).

### Create in-game purchases

The latest in-app purchases and license info APIs are part of the [Windows.Services.Store](/uwp/api/windows.services.store) namespace in the Windows SDK (starting in Windows 10, version 1607). If you're developing a new game that targets version 1607 or later, use the __Windows.Services.Store__ namespace because it supports the latest add-on types and has better performance.
It's also designed to be compatible with future types of products and features supported by the Partner Center and the Store. When developing for previous versions of Windows 10, use the [Windows.ApplicationModel.Store](/uwp/api/windows.applicationmodel.store) namespace instead.

For more info, see [In-app purchases and trials](../monetize/in-app-purchases-and-trials.md).

#### Simplified purchase example

This section uses a simplified purchase example to illustrate the use of different method calls to implement the purchase flow.

|In-game actions / activity                                                | Game background tasks                  |
|--------------------------------------------------------------------------|----------------------------------------|
|Gamer enters a shop. Shop menu pops up to display the available add-ons and purchase price |  Game [retrieves the product info](../monetize/get-product-info-for-apps-and-add-ons.md) of the add-ons, [determines whether the add-ons have the proper license](../monetize/get-license-info-for-apps-and-add-ons.md), and displays the add-ons that are available for purchase by the gamer on the shop menu.                           |
|Gamer clicks __Buy__ to purchase an item             |The __Buy__ action sends a request to purchase the item and starts the payment process to acquire it. The implementation varies depending on the item type. If it's [a durable or a one-time purchase item](../monetize/enable-in-app-purchases-of-apps-and-add-ons.md), the customer can own only a single item until it expires. If the item is a [consumable](../monetize/enable-consumable-add-on-purchases.md), the customer can own one or more of it. |

### Test in-game purchases during game development

Because you must create an add-on in association with a game, your game must be published and available in the Store. The steps in this section show how to create add-ons while your game is still in development.
(If your finished game is already live in the Store, you can skip the first three steps and go directly to [Create an add-on in the Store](#create-an-add-on-in-the-store).)

To create add-ons while your game is still in development:
1. [Create a package](#create-a-package)
1. [Publish the game as hidden](#publish-the-game-as-hidden)
1. [Associate your game solution in Visual Studio with the Store](#associate-your-game-solution-with-the-store)
1. [Create an add-on in the Store](#create-an-add-on-in-the-store)

#### Create a package

Your game must meet the minimum Windows App Certification requirements before you can publish it. You can use the [Windows App Certification Kit](../debug-test-perf/windows-app-certification-kit.md), which is part of the Windows SDK,
to run tests on the game to help ensure that it's ready for publishing to the Store. If you haven't already downloaded the Windows SDK that includes the Windows App Certification Kit, go to [Windows SDK](https://developer.microsoft.com/windows/downloads/windows-sdk/).

To create a package that you can upload to the Store:

1. Open your game solution in Visual Studio.
1. Within Visual Studio, go to __Project__ > __Store__ > __Create App Packages ...__
1. For the __Do you want to build packages to upload to the Microsoft Store?__ option, select __Yes__.
1. Sign in to your [Partner Center](https://partner.microsoft.com/dashboard) developer account. Or [register as a developer in Partner Center](https://developer.microsoft.com/microsoft-store/register/).
1. Select an app to create the upload package for. If you didn't yet create an app submission, provide a new app name to create a new submission. For more info, see [Create your app by reserving a name](/windows/apps/publish/publish-your-app/reserve-your-apps-name?pivots=store-installer-msix).
1. After the package is created successfully, select __Launch Windows App Certification Kit__ to start the testing process.
1. Fix any errors to create a game package.

#### Publish the game as hidden

1. Go to [Partner Center](https://partner.microsoft.com/dashboard) and sign in.
1. From the __Dashboard overview__ or __All apps__ page, select the app you want to work with. If you didn't yet create an app submission, select __Create a new app__ and reserve a name.
1. On the __App Overview__ page, select __Start your submission__.
1. Configure this new submission. On the submission page:
    * Select __Pricing and availability__. In the __Visibility__ section, choose '__Hide this app and prevent acquisition...__' to ensure only your development team has access to the game. For more details, see [Distribution and visibility](/windows/apps/publish/publish-your-app/price-and-availability?pivots=store-installer-msix).
    * Select __Properties__. In the __Category and subcategory__ section, choose __Games__ and then a suitable subcategory for your game.
    * Select __Age ratings__. Fill out the questionnaire accurately.
    * Select __Packages__. Upload the game package created in the earlier step.
1. Follow any other submission prompts in the dashboard to successfully publish this game which remains hidden to the public.
1. Select __Submit to the Store__.

For more info, see [App submissions](/windows/apps/publish/publish-your-app/create-app-submission?pivots=store-installer-msix).

After you submit your game to the Store, it enters the [app certification process](/windows/apps/publish/publish-your-app/app-certification-process?pivots=store-installer-msix). This process can take up to 16 hours before the game is listed.

#### Associate your game solution with the Store

With your game solution opened in Visual Studio:

1. Go to __Project__ > __Store__ > __Associate App with the Store ...__
1. Sign in to your Partner Center developer account and select the app name to associate this solution with.
1. Double-click on the __Package.appxmanifest.xml file__ and go to the __Packaging__ tab to check that the game is associated correctly.

If you associate the solution to a published game that is live and listed in the Store, your solution has an active license and you are one step closer to creating add-ons for your game. For more info, see [Packaging apps](../packaging/index.md).

#### Create an add-on in the Store

As you create add-ons, make sure you're associating them with the right game submission. For details about how to configure all the various info associated with an add-on, see [Add-on submissions](/windows/apps/publish/publish-your-app/create-app-submission?pivots=store-installer-add-on).

1. Go to [Partner Center](https://partner.microsoft.com/dashboard) and sign in.
1. From the __Dashboard overview__ or __All apps__ page, select the app you want to create the add-on for.
1. On the __App Overview__ page, in the __Add-ons__ section, select __Create a new add-on__.
1. Select the product type for the add-on: __developer-managed consumable__, __store-managed consumable__, or __durable__.
1. Enter a unique product ID. Use this ID as a string variable when you integrate this add-on into your game code. Consumers don't see this ID. For more info, see [Set your app product type and product ID](/windows/apps/publish/publish-your-app/create-app-store-listing?pivots=store-installer-add-on).

Other configurations for add-ons include:
* [Properties](/windows/apps/publish/publish-your-app/enter-app-properties?pivots=store-installer-add-on)
* [Pricing and availability](/windows/apps/publish/publish-your-app/price-and-availability?pivots=store-installer-add-on)
* [Store listing](/windows/apps/publish/publish-your-app/create-app-store-listing?pivots=store-installer-add-on)

If your game has many add-ons, you can create them programmatically by using the __Microsoft Store submission API__. For more info, see [Create and manage submissions using Microsoft Store services](../monetize/create-and-manage-submissions-using-windows-store-services.md).

## Display ads in your game

Third-party advertising providers such as Vungle, Rise (Unity), and Pubfinity offer SDKs that you can use to display ads in your games and earn revenue.


## Related links

* [Getting paid](/partner-center/marketplace-get-paid)
* [Account types, locations, and fees](/windows/apps/publish/partner-center/account-types-locations-and-fees)
* [Analytics](/windows/apps/publish/analytics)
* [Globalization and localization](/windows/apps/design/globalizing/globalizing-portal)
* [Implement a trial version of your app](../monetize/implement-a-trial-version-of-your-app.md)
