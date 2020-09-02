---
ms.assetid: 89178FD9-850B-462F-9016-1AD86D1F6F7F
description: Learn how to use the Windows.Services.Store namespace to get Store-related product info for the current app or one of its add-ons.
title: Get product info for apps and add-ons
ms.date: 02/08/2018
ms.topic: article
keywords: windows 10, uwp, in-app purchases, IAPs, add-ons, Windows.Services.Store
ms.localizationpriority: medium
---
# Get product info for apps and add-ons

This article demonstrates how to use methods of the [StoreContext](/uwp/api/windows.services.store.storecontext) class in the [Windows.Services.Store](/uwp/api/windows.services.store) namespace to access Store-related info for the current app or one of its add-ons.

For a complete sample application, see the [Store sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Store).

> [!NOTE]
> The **Windows.Services.Store** namespace was introduced in Windows 10, version 1607, and it can only be used in projects that target **Windows 10 Anniversary Edition (10.0; Build 14393)** or a later release in Visual Studio. If your app targets an earlier version of Windows 10, you must use the [Windows.ApplicationModel.Store](/uwp/api/windows.applicationmodel.store) namespace instead of the **Windows.Services.Store** namespace. For more information, see [this article](in-app-purchases-and-trials-using-the-windows-applicationmodel-store-namespace.md).

## Prerequisites

These examples have the following prerequisites:
* A Visual Studio project for a Universal Windows Platform (UWP) app that targets **Windows 10 Anniversary Edition (10.0; Build 14393)** or a later release.
* You have [created an app submission](../publish/app-submissions.md) in Partner Center and this app is published in the Store. You can optionally configure the app so it is not discoverable in the Store while you test it. For more information, see our [testing guidance](in-app-purchases-and-trials.md#testing).
* If you want to get product info for an add-on for the app, you must also [create the add-on in Partner Center](../publish/add-on-submissions.md).

The code in these examples assume:
* The code runs in the context of a [Page](/uwp/api/windows.ui.xaml.controls.page) that contains a [ProgressRing](/uwp/api/windows.ui.xaml.controls.progressring) named ```workingProgressRing``` and a [TextBlock](/uwp/api/windows.ui.xaml.controls.textblock) named ```textBlock```. These objects are used to indicate that an asynchronous operation is occurring and to display output messages, respectively.
* The code file has a **using** statement for the **Windows.Services.Store** namespace.
* The app is a single-user app that runs only in the context of the user that launched the app. For more information, see [In-app purchases and trials](in-app-purchases-and-trials.md#api_intro).

> [!NOTE]
> If you have a desktop application that uses the [Desktop Bridge](https://developer.microsoft.com/windows/bridges/desktop), you may need to add additional code not shown in these examples to configure the [StoreContext](/uwp/api/windows.services.store.storecontext) object. For more information, see [Using the StoreContext class in a desktop application that uses the Desktop Bridge](in-app-purchases-and-trials.md#desktop).

## Get info for the current app

To get Store product info about the current app, use the [GetStoreProductForCurrentAppAsync](/uwp/api/windows.services.store.storecontext.getstoreproductforcurrentappasync) method. This is an asynchronous method that returns a [StoreProduct](/uwp/api/windows.services.store.storeproduct) object that you can use to get info such as the price.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/InAppPurchasesAndLicenses_RS1/cs/GetAppInfoPage.xaml.cs" id="GetAppInfo":::

## Get info for add-ons with known Store IDs that are associated with the current app

To get Store product info for add-ons that are associated with the current app and for which you already know the [Store IDs](in-app-purchases-and-trials.md#store_ids), use the [GetStoreProductsAsync](/uwp/api/windows.services.store.storecontext.getstoreproductsasync) method. This is an asynchronous method that returns a collection of [StoreProduct](/uwp/api/windows.services.store.storeproduct) objects that represent each of the add-ons. In addition to the Store IDs, you must pass a list of strings to this method that identify the types of the add-ons. For a list of the supported string values, see the [ProductKind](/uwp/api/windows.services.store.storeproduct.productkind) property.

> [!NOTE]
> The **GetStoreProductsAsync** method returns product info for the specified add-ons that are associated with the app, regardless of whether the add-ons are currently available for purchase. To retrieve info for all the add-ons for the current app that can currently be purchased, use the **GetAssociatedStoreProductsAsync** method as described in the [following section](#get-info-for-add-ons-that-are-available-for-purchase-from-the-current-app) instead.

This example retrieves info for durable add-ons with the specified Store IDs that are associated with the current app.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/InAppPurchasesAndLicenses_RS1/cs/GetProductInfoPage.xaml.cs" id="GetProductInfo":::

## Get info for add-ons that are available for purchase from the current app

To get Store product info for the add-ons that are currently available for purchase from the current app, use the [GetAssociatedStoreProductsAsync](/uwp/api/windows.services.store.storecontext.getassociatedstoreproductsasync) method. This is an asynchronous method that returns a collection of [StoreProduct](/uwp/api/windows.services.store.storeproduct) objects that represent each of the available add-ons. You must pass a list of strings to this method that identify the types of add-ons you want to retrieve. For a list of the supported string values, see the [ProductKind](/uwp/api/windows.services.store.storeproduct.productkind) property.

> [!NOTE]
> If the app has many add-ons that are available for purchase, you can alternatively use the [GetAssociatedStoreProductsWithPagingAsync](/uwp/api/Windows.Services.Store.StoreContext.GetAssociatedStoreProductsWithPagingAsync) method to use paging to return the add-on results.

The following example retrieves info for all durable add-ons, Store-managed consumable add-ons, and developer-managed consumable add-ons that are available for purchase from the current app.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/InAppPurchasesAndLicenses_RS1/cs/GetAddOnInfoPage.xaml.cs" id="GetAddOnInfo":::


## Get info for add-ons for the current app that the user has purchased

To get Store product info for add-ons that the current user has purchased, use the [GetUserCollectionAsync](/uwp/api/windows.services.store.storecontext.getusercollectionasync) method. This is an asynchronous method that returns a collection of  [StoreProduct](/uwp/api/windows.services.store.storeproduct) objects that represent each of the add-ons. You must pass a list of strings to this method that identify the types of add-ons you want to retrieve. For a list of the supported string values, see the [ProductKind](/uwp/api/windows.services.store.storeproduct.productkind) property.

> [!NOTE]
> If the app has many add-ons, you can alternatively use the [GetUserCollectionWithPagingAsync](/uwp/api/windows.services.store.storecontext.getusercollectionwithpagingasync) method to use paging to return the add-on results.

The following example retrieves info for durable add-ons with the specified [Store IDs](in-app-purchases-and-trials.md#store_ids).

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/InAppPurchasesAndLicenses_RS1/cs/GetUserCollectionPage.xaml.cs" id="GetUserCollection":::

## Related topics

* [In-app purchases and trials](in-app-purchases-and-trials.md)
* [Get license info for apps and add-ons](get-license-info-for-apps-and-add-ons.md)
* [Enable in-app purchases of apps and add-ons](enable-in-app-purchases-of-apps-and-add-ons.md)
* [Enable consumable add-on purchases](enable-consumable-add-on-purchases.md)
* [Implement a trial version of your app](implement-a-trial-version-of-your-app.md)
* [Store sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Store)
