---
author: mcleanbyron
ms.assetid: FD381669-F962-465E-940B-AED9C8D19C90
description: Learn how to use the Windows.Services.Store namespace to work with consumable add-ons.
title: Enable consumable add-on purchases
keywords: windows 10, uwp, consumable, add-ons, in-app purchases, IAPs, Windows.Services.Store
ms.author: mcleans
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
---

# Enable consumable add-on purchases

Apps that target Windows 10, version 1607, or later can use methods of the [StoreContext](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storecontext.aspx) class in the [Windows.Services.Store](https://msdn.microsoft.com/library/windows/apps/windows.services.store.aspx) namespace to manage the user's fulfillment of consumable add-ons in your UWP apps (add-ons are also known as in-app products or IAPs). Use consumable add-ons for items that can be purchased, used, and purchased again. This is especially useful for things like in-game currency (gold, coins, etc.) that can be purchased and then used to purchase specific power-ups.

>**Note**&nbsp;&nbsp;This article is applicable to apps that target Windows 10, version 1607, or later. If your app targets an earlier version of Windows 10, you must use the [Windows.ApplicationModel.Store](https://msdn.microsoft.com/library/windows/apps/windows.applicationmodel.store.aspx) namespace instead of the **Windows.Services.Store** namespace. For more information, see [In-app purchases and trials using the Windows.ApplicationModel.Store namespace](in-app-purchases-and-trials-using-the-windows-applicationmodel-store-namespace.md).

## Overview of consumable add-ons

Apps that target Windows 10, version 1607, or later can offer two types of consumable add-ons that differ in the way that fulfillments are managed:

* **Developer-managed consumable**. For this type of consumable, you are responsible for keeping track of the user's balance of items that the add-on represents, and for reporting the purchase of the add-on as fulfilled to the Store after the user has consumed all of the items. The user cannot purchase the add-on again until your app has reported the previous add-on purchase as fulfilled.

  For example, if your add-on represents 100 coins in a game and the user consumes 10 coins, your app or service must maintain the new remaining balance of 90 coins for the user. After the user has consumed all 100 coins, your app must report the add-on as fulfilled, and then the user can purchase the 100 coin add-on again.

* **Store-managed consumable**. For this type of consumable, the Store keeps track of the user's balance of items that the add-on represents. When the user consumes any items, you are responsible for reporting those items as fulfilled to the Store, and the Store updates the user's balance. Your app can query for the current balance for the user at any time. After the user consumes all of the items, the user can purchase the add-on again.

  For example, if your add-on represents an initial quantity of 100 coins in a game and the user consumes 10 coins, your app reports to the Store that 10 units of the add-on were fulfilled, and the Store updates the remaining balance. After the user has consumed all 100 coins, the user can purchase the 100 coin add-on again.

  >**Note**&nbsp;&nbsp;Store-managed consumables are available starting in Windows 10, version 1607. The ability to create a Store-managed consumable in the Windows Dev Center dashboard is coming soon.

To offer a consumable add-on to a user, follow this general process:

1. Enable users to [purchase the add-on](enable-in-app-purchases-of-apps-and-add-ons.md) from your app.
3. When the user consumes the add-on (for example, they spend coins in a game), [report the add-on as fulfilled](enable-consumable-add-on-purchases.md#report_fulfilled).

At any time, you can also [get the remaining balance](enable-consumable-add-on-purchases.md#get_balance) for a Store-managed consumable.

## Prerequisites

These examples have the following prerequisites:
* A Visual Studio project for a Universal Windows Platform (UWP) app that targets Windows 10, version 1607, or later.
* You have created an app in the Windows Dev Center dashboard with consumable add-ons (also known as in-app products or IAPs), and this app is published and available in the Store. This can be an app that you want to release to customers, or it can be a basic app that meets minimum [Windows App Certification Kit](https://developer.microsoft.com/windows/develop/app-certification-kit) requirements that you are using for testing purposes only. For more information, see the [testing guidance](in-app-purchases-and-trials.md#testing).

The code in these examples assume:
* The code runs in the context of a [Page](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.page.aspx) that contains a [ProgressRing](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.progressring.aspx) named ```workingProgressRing``` and a [TextBlock](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.textblock.aspx) named ```textBlock```. These objects are used to indicate that an asynchronous operation is occurring and to display output messages, respectively.
* The code file has a **using** statement for the **Windows.Services.Store** namespace.
* The app is a single-user app that runs only in the context of the user that launched the app. For more information, see [In-app purchases and trials](in-app-purchases-and-trials.md#api_intro).

For a complete sample application, see the [Store sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Store).

>**Note**&nbsp;&nbsp;If you have a desktop application that uses the [Desktop Bridge](https://developer.microsoft.com/windows/bridges/desktop), you may need to add additional code not shown in these examples to configure the [StoreContext](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storecontext.aspx) object. For more information, see [Using the StoreContext class in a desktop application that uses the Desktop Bridge](in-app-purchases-and-trials.md#desktop).

<span id="report_fulfilled" />
## Report a consumable add-on as fulfilled

After the user [purchases the add-on](enable-in-app-purchases-of-apps-and-add-ons.md) from your app and they consume your add-on, your app must report the add-on as fulfilled by calling the [ReportConsumableFulfillmentAsync](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storecontext.reportconsumablefulfillmentasync.aspx) method of the [StoreContext](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storecontext.aspx) class. You must pass the following information to this method:

* The [Store ID](in-app-purchases-and-trials.md#store_ids) of the add-on that you want to report as fulfilled.
* The units of the add-on you want to report as fulfilled.
  * For a developer-managed consumable, specify 1 for the *quantity* parameter. This alerts the Store that the consumable has been fulfilled, and the customer can then purchase the consumable again. The user cannot purchase the consumable again until your app has notified the Store that it was fulfilled.
  * For a Store-managed consumable, specify the actual number of units that have been consumed. The Store will update the remaining balance for the consumable.
* The tracking ID for the fulfillment. This is a developer-supplied GUID that identifies the specific transaction that the fulfillment operation is associated with for tracking purposes. For more information, see the remarks in [ReportConsumableFulfillmentAsync](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storecontext.reportconsumablefulfillmentasync.aspx).

This example demonstrates how to report a Store-managed consumable as fulfilled.

> [!div class="tabbedCodeSnippets"]
[!code-cs[EnableConsumables](./code/InAppPurchasesAndLicenses_RS1/cs/ConsumeAddOnPage.xaml.cs#ConsumeAddOn)]

<span id="get_balance" />
## Get the remaining balance for a Store-managed consumable

This example demonstrates how to use the [GetConsumableBalanceRemainingAsync](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storecontext.getconsumablebalanceremainingasync.aspx) method of the [StoreContext](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storecontext.aspx) class to get the remaining balance for a Store-managed consumable add-on.

> [!div class="tabbedCodeSnippets"]
[!code-cs[EnableConsumables](./code/InAppPurchasesAndLicenses_RS1/cs/GetRemainingAddOnBalancePage.xaml.cs#GetRemainingAddOnBalance)]

## Related topics

* [In-app purchases and trials](in-app-purchases-and-trials.md)
* [Get product info for apps and add-ons](get-product-info-for-apps-and-add-ons.md)
* [Get license info for apps and add-ons](get-license-info-for-apps-and-add-ons.md)
* [Enable in-app purchases of apps and add-ons](enable-in-app-purchases-of-apps-and-add-ons.md)
* [Implement a trial version of your app](implement-a-trial-version-of-your-app.md)
* [Store sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Store)
