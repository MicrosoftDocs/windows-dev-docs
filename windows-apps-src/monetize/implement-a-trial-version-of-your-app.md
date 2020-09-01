---
ms.assetid: 571697B7-6064-4C50-9A68-1374F2C3F931
description: Learn how to use the Windows.Services.Store namespace to implement a trial version of your app.
title: Implement a trial version of your app
keywords: windows 10, uwp, trial, in-app purchases, Windows.Services.Store
ms.date: 08/25/2017
ms.topic: article


ms.localizationpriority: medium
---
# Implement a trial version of your app

If you [configure your app as a free trial in Partner Center](../publish/set-app-pricing-and-availability.md#free-trial) so that customers can use your app for free during a trial period, you can entice your customers to upgrade to the full version of your app by excluding or limiting some features during the trial period. Determine which features should be limited before you begin coding, then make sure that your app only allows them to work when a full license has been purchased. You can also enable features, such as banners or watermarks, that are shown only during the trial, before a customer buys your app.

This article shows how to use members of the [StoreContext](/uwp/api/windows.services.store.storecontext) class in the [Windows.Services.Store](/uwp/api/windows.services.store) namespace to determine if the user has a trial license for your app and be notified if the state of the license changes while your app is running. 

> [!NOTE]
> The **Windows.Services.Store** namespace was introduced in Windows 10, version 1607, and it can only be used in projects that target **Windows 10 Anniversary Edition (10.0; Build 14393)** or a later release in Visual Studio. If your app targets an earlier version of Windows 10, you must use the [Windows.ApplicationModel.Store](/uwp/api/windows.applicationmodel.store) namespace instead of the **Windows.Services.Store** namespace. For more information, see [this article](exclude-or-limit-features-in-a-trial-version-of-your-app.md).

## Guidelines for implementing a trial version

The current license state of your app is stored as properties of the [StoreAppLicense](/uwp/api/windows.services.store.storeapplicense) class. Typically, you put the functions that depend on the license state in a conditional block, as we describe in the next step. When considering these features, make sure you can implement them in a way that will work in all license states.

Also, decide how you want to handle changes to the app's license while the app is running. Your trial app can be full-featured, but have in-app ad banners where the paid-for version doesn't. Or, your trial app can disable certain features, or display regular messages asking the user to buy it.

Think about the type of app you're making and what a good trial or expiration strategy is for it. For a trial version of a game, a good strategy is to limit the amount of game content that a user can play. For a trial version of a utility, you might consider setting an expiration date, or limiting the features that a potential buyer can use.

For most non-gaming apps, setting an expiration date works well, because users can develop a good understanding of the complete app. Here are a few common expiration scenarios and your options for handling them.

-   **Trial license expires while the app is running**

    If the trial expires while your app is running, your app can:

    -   Do nothing.
    -   Display a message to your customer.
    -   Close.
    -   Prompt your customer to buy the app.

    The best practice is to display a message with a prompt for buying the app, and if the customer buys it, continue with all features enabled. If the user decides not to buy the app, close it or remind them to buy the app at regular intervals.

-   **Trial license expires before the app is launched**

    If the trial expires before the user launches the app, your app won't launch. Instead, users see a dialog box that gives them the option to purchase your app from the Store.

-   **Customer buys the app while it is running**

    If the customer buys your app while it is running, here are some actions your app can take.

    -   Do nothing and let them continue in trial mode until they restart the app.
    -   Thank them for buying or display a message.
    -   Silently enable the features that are available with a full-license (or disable the trial-only notices).

Be sure to explain how your app will behave during and after the free trial period so your customers won't be surprised by your app's behavior. For more info about describing your app, see [Create app descriptions](../publish/create-app-store-listings.md).

## Prerequisites

This example has the following prerequisites:
* A Visual Studio project for a Universal Windows Platform (UWP) app that targets **Windows 10 Anniversary Edition (10.0; Build 14393)** or a later release.
* You have created an app in Partner Center that is configured as a [free trial](../publish/set-app-pricing-and-availability.md) with no time limit and this app is published in the Store. You can optionally configure the app so it is not discoverable in the Store while you test it. For more information, see our [testing guidance](in-app-purchases-and-trials.md#testing).

The code in this example assumes:
* The code runs in the context of a [Page](/uwp/api/windows.ui.xaml.controls.page) that contains a [ProgressRing](/uwp/api/windows.ui.xaml.controls.progressring) named ```workingProgressRing``` and a [TextBlock](/uwp/api/windows.ui.xaml.controls.textblock) named ```textBlock```. These objects are used to indicate that an asynchronous operation is occurring and to display output messages, respectively.
* The code file has a **using** statement for the **Windows.Services.Store** namespace.
* The app is a single-user app that runs only in the context of the user that launched the app. For more information, see [In-app purchases and trials](in-app-purchases-and-trials.md#api_intro).

> [!NOTE]
> If you have a desktop application that uses the [Desktop Bridge](https://developer.microsoft.com/windows/bridges/desktop), you may need to add additional code not shown in this example to configure the [StoreContext](/uwp/api/windows.services.store.storecontext) object. For more information, see [Using the StoreContext class in a desktop application that uses the Desktop Bridge](in-app-purchases-and-trials.md#desktop).

## Code example

When your app is initializing, get the [StoreAppLicense](/uwp/api/windows.services.store.storeapplicense) object for your app and handle the [OfflineLicensesChanged](/uwp/api/windows.services.store.storecontext.offlinelicenseschanged) event to receive notifications when the license changes while the app is running. For example, the app's license could change if the trial period expires or the customer buys the app through a Store. When the license changes, get the new license and enable or disable a feature of your app accordingly.

At this point, if a user bought the app, it is a good practice to provide feedback to the user that the licensing status has changed. You might need to ask the user to restart the app if that's how you've coded it. But make this transition as seamless and painless as possible.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/InAppPurchasesAndLicenses_RS1/cs/ImplementTrialPage.xaml.cs" id="ImplementTrial":::

For a complete sample application, see the [Store sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Store).

## Related topics

* [In-app purchases and trials](in-app-purchases-and-trials.md)
* [Get product info for apps and add-ons](get-product-info-for-apps-and-add-ons.md)
* [Get license info for apps and add-ons](get-license-info-for-apps-and-add-ons.md)
* [Enable in-app purchases of apps and add-ons](enable-in-app-purchases-of-apps-and-add-ons.md)
* [Enable consumable add-on purchases](enable-consumable-add-on-purchases.md)
* [Store sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Store)
