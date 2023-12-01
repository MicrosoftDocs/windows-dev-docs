---
ms.assetid: B356C442-998F-4B2C-B550-70070C5E4487
description: Learn how to use the Windows.Services.Store namespace to purchase an app or one of its add-ons.
title: Enable in-app purchases of apps and add-ons
keywords: windows 10, uwp, add-ons, in-app purchases, IAPs, Windows.Services.Store
ms.date: 08/25/2017
ms.topic: article


ms.localizationpriority: medium
---
# Enable in-app purchases of apps and add-ons

This article demonstrates how to use members in the [Windows.Services.Store](/uwp/api/windows.services.store) namespace to request the purchase the current app or one of its add-ons for the user. For example, if the user currently has a trial version of the app, you can use this process to purchase a full license for the user. Alternatively, you can use this process to purchase an add-on, such as a new game level for the user.

To request the purchase of an app or add-on, the [Windows.Services.Store](/uwp/api/windows.services.store) namespace provides several different methods:
* If you know the [Store ID](in-app-purchases-and-trials.md#store_ids) of the app or add-on, you can use the [RequestPurchaseAsync](/uwp/api/windows.services.store.storecontext.requestpurchaseasync) method of the [StoreContext](/uwp/api/windows.services.store.storecontext) class.
* If you already have a [**StoreProduct**, **StoreSku**, or **StoreAvailability** object](in-app-purchases-and-trials.md#products-skus) that represents the app or add-on, you can use the **RequestPurchaseAsync** methods of these objects. For examples of different ways to retrieve a **StoreProduct** in your code, see [Get product info for apps and add-ons](get-product-info-for-apps-and-add-ons.md).

Each method presents a standard purchase UI to the user and then completes asynchronously after the transaction is complete. The method returns an object that indicates whether the transaction was successful.

> [!NOTE]
> The **Windows.Services.Store** namespace was introduced in Windows 10, version 1607, and it can only be used in projects that target **Windows 10 Anniversary Edition (10.0; Build 14393)** or a later release in Visual Studio. If your app targets an earlier version of Windows 10, you must use the [Windows.ApplicationModel.Store](/uwp/api/windows.applicationmodel.store) namespace instead of the **Windows.Services.Store** namespace. For more information, see [this article](in-app-purchases-and-trials-using-the-windows-applicationmodel-store-namespace.md).

## Prerequisites

This example has the following prerequisites:
* A Visual Studio project for a Universal Windows Platform (UWP) app that targets **Windows 10 Anniversary Edition (10.0; Build 14393)** or a later release.
* You have [created an app submission](/windows/apps/publish/publish-your-app/create-app-submission?pivots=store-installer-msix) in Partner Center and this app is published in the Store. You can optionally configure the app so it is not discoverable in the Store while you test it. For more information, see our [testing guidance](in-app-purchases-and-trials.md#testing).
* If you want to enable in-app purchases for an add-on for the app, you must also [create the add-on in Partner Center](/windows/apps/publish/publish-your-app/create-app-submission?pivots=store-installer-add-on).

The code in this example assumes:
* The code runs in the context of a [Page](/uwp/api/windows.ui.xaml.controls.page) that contains a [ProgressRing](/uwp/api/windows.ui.xaml.controls.progressring) named ```workingProgressRing``` and a [TextBlock](/uwp/api/windows.ui.xaml.controls.textblock) named ```textBlock```. These objects are used to indicate that an asynchronous operation is occurring and to display output messages, respectively.
* The code file has a **using** statement for the **Windows.Services.Store** namespace.
* The app is a single-user app that runs only in the context of the user that launched the app. For more information, see [In-app purchases and trials](in-app-purchases-and-trials.md#api_intro).

> [!NOTE]
> If you have a desktop application that uses the [Desktop Bridge](/windows/msix/desktop/source-code-overview), you may need to add additional code not shown in this example to configure the [StoreContext](/uwp/api/windows.services.store.storecontext) object. For more information, see [Using the StoreContext class in a desktop application that uses the Desktop Bridge](in-app-purchases-and-trials.md#desktop).

## Code example

This example demonstrates how to use the [RequestPurchaseAsync](/uwp/api/windows.services.store.storecontext.requestpurchaseasync) method of the [StoreContext](/uwp/api/windows.services.store.storecontext) class to purchase an app or add-on with a known [Store ID](in-app-purchases-and-trials.md#store-ids). For a complete sample application, see the [Store sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Store).

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/InAppPurchasesAndLicenses_RS1/cs/PurchaseAddOnPage.xaml.cs" id="PurchaseAddOn":::



## Related topics

* [In-app purchases and trials](in-app-purchases-and-trials.md)
* [Get product info for apps and add-ons](get-product-info-for-apps-and-add-ons.md)
* [Get license info for apps and add-ons](get-license-info-for-apps-and-add-ons.md)
* [Enable consumable add-on purchases](enable-consumable-add-on-purchases.md)
* [Implement a trial version of your app](implement-a-trial-version-of-your-app.md)
* [Store sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Store)