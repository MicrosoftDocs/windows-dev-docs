---
ms.assetid: 9630AF6D-6887-4BE3-A3CB-D058F275B58F
description: Learn how to use the Windows.Services.Store namespace to get license info for the current app and its add-ons.
title: Get license info for your app and add-ons
ms.date: 12/04/2017
ms.topic: article
keywords: windows 10, uwp, licenses, apps, add-ons, in-app purchases, IAPs, Windows.Services.Store
ms.localizationpriority: medium
---
# Get license info for apps and add-ons

This article demonstrates how to use methods of the [StoreContext](/uwp/api/windows.services.store.storecontext) class in the [Windows.Services.Store](/uwp/api/windows.services.store) namespace to get license info for the current app and its add-ons. For example, you can use this info to determine if the licenses for the app or its add-ons are active, or if they are trial licenses.

> [!NOTE]
> The **Windows.Services.Store** namespace was introduced in Windows 10, version 1607, and it can only be used in projects that target **Windows 10 Anniversary Edition (10.0; Build 14393)** or a later release in Visual Studio. If your app targets an earlier version of Windows 10, you must use the [Windows.ApplicationModel.Store](/uwp/api/windows.applicationmodel.store) namespace instead of the **Windows.Services.Store** namespace. For more information, see [this article](in-app-purchases-and-trials-using-the-windows-applicationmodel-store-namespace.md).

## Prerequisites

This example has the following prerequisites:
* A Visual Studio project for a Universal Windows Platform (UWP) app that targets **Windows 10 Anniversary Edition (10.0; Build 14393)** or a later release.
* You have [created an app submission](../publish/app-submissions.md) in Partner Center and this app is published in the Store. You can optionally configure the app so it is not discoverable in the Store while you test it. For more information, see our [testing guidance](in-app-purchases-and-trials.md#testing).
* If you want get license info for an add-on for the app, you must also [create the add-on in Partner Center](../publish/add-on-submissions.md).

The code in this example assumes:
* The code runs in the context of a [Page](/uwp/api/windows.ui.xaml.controls.page) that contains a [ProgressRing](/uwp/api/windows.ui.xaml.controls.progressring) named ```workingProgressRing``` and a [TextBlock](/uwp/api/windows.ui.xaml.controls.textblock) named ```textBlock```. These objects are used to indicate that an asynchronous operation is occurring and to display output messages, respectively.
* The code file has a **using** statement for the **Windows.Services.Store** namespace.
* The app is a single-user app that runs only in the context of the user that launched the app. For more information, see [In-app purchases and trials](in-app-purchases-and-trials.md#api_intro).

> [!NOTE]
> If you have a desktop application that uses the [Desktop Bridge](https://developer.microsoft.com/windows/bridges/desktop), you may need to add additional code not shown in this example to configure the [StoreContext](/uwp/api/windows.services.store.storecontext) object. For more information, see [Using the StoreContext class in a desktop application that uses the Desktop Bridge](in-app-purchases-and-trials.md#desktop).

## Code example

To get license info for the current app, use the [GetAppLicenseAsync](/uwp/api/windows.services.store.storecontext.getapplicenseasync) method. This is an asynchronous method that returns a [StoreAppLicense](/uwp/api/windows.services.store.storeapplicense) object that provides license info for the app, including properties that indicate whether the user currently has a valid license to use the app ([IsActive](/uwp/api/windows.services.store.storeapplicense.isactive)) and whether the license is for a trial version ([IsTrial](/uwp/api/windows.services.store.storeapplicense.istrial)).

To access the licenses for durable add-ons of the current app for which the user has an entitlement to use, use the [AddOnLicenses](/uwp/api/windows.services.store.storeapplicense.addonlicenses) property of the [StoreAppLicense](/uwp/api/windows.services.store.storeapplicense) object. This property returns a collection of [StoreLicense](/uwp/api/windows.services.store.storelicense) objects that represent the add-on licenses.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/InAppPurchasesAndLicenses_RS1/cs/GetLicenseInfoPage.xaml.cs" id="GetLicenseInfo":::

For a complete sample application, see the [Store sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Store).

## Related topics

* [In-app purchases and trials](in-app-purchases-and-trials.md)
* [Get product info for apps and add-ons](get-product-info-for-apps-and-add-ons.md)
* [Enable in-app purchases of apps and add-ons](enable-in-app-purchases-of-apps-and-add-ons.md)
* [Enable consumable add-on purchases](enable-consumable-add-on-purchases.md)
* [Implement a trial version of your app](implement-a-trial-version-of-your-app.md)
* [Store sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Store)
