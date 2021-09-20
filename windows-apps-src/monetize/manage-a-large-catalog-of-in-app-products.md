---
ms.assetid: 5E722AFF-539D-456E-8C4A-ADE90CF7674A
description: If your app offers a large in-app product catalog, you can optionally follow the process described in this topic to help manage your catalog.
title: Manage a large catalog of in-app products
ms.date: 08/25/2017
ms.topic: article
keywords: windows 10, uwp, in-app purchases, IAPs, add-ons, catalog, Windows.ApplicationModel.Store
ms.localizationpriority: medium
---
# Manage a large catalog of in-app products

If your app offers a large in-app product catalog, you can optionally follow the process described in this topic to help manage your catalog. In releases before Windows 10, the Store has a limit of 200 product listings per developer account, and the process described in this topic can be used to work around that limitation. Starting with Windows 10, the Store has no limit to the number of product listings per developer account, and the process described in this article is no longer necessary.

> [!IMPORTANT]
> This article demonstrates how to use members of the [Windows.ApplicationModel.Store](/uwp/api/windows.applicationmodel.store) namespace. This namespace is no longer being updated with new features, and we recommend that you use the [Windows.Services.Store](/uwp/api/windows.services.store) namespace instead. The **Windows.Services.Store** namespace supports the latest add-on types, such as Store-managed consumable add-ons and subscriptions, and is designed to be compatible with future types of products and features supported by Partner Center and the Store. The **Windows.Services.Store** namespace was introduced in Windows 10, version 1607, and it can only be used in projects that target **Windows 10 Anniversary Edition (10.0; Build 14393)** or a later release in Visual Studio. For more information, see [In-app purchases and trials](in-app-purchases-and-trials.md).

To enable this capability, you will create a handful of product entries for specific price tiers, with each one able to represent hundreds of products within a catalog. Use the [RequestProductPurchaseAsync](/uwp/api/windows.applicationmodel.store.currentapp.requestproductpurchaseasync) method overload that specifies an app-defined offer associated with an in-app product listed in the Store. In addition to specifying an offer and product association during the call, your app should also pass a [ProductPurchaseDisplayProperties](/uwp/api/Windows.ApplicationModel.Store.ProductPurchaseDisplayProperties) object that contains the large catalog offer details. If these details are not provided, the details for the listed product will be used instead.

The Store will only use the *offerId* from the purchase request in the resulting [PurchaseResults](/uwp/api/Windows.ApplicationModel.Store.PurchaseResults). This process does not directly modify the information originally provided when [listing the in-app product in the Store](../publish/add-on-submissions.md).

## Prerequisites

-   This topic covers Store support for the representation of multiple in-app offers using a single in-app product listed in the Store. If you are unfamiliar with in-app purchases please review [Enable in-app product purchases](enable-in-app-product-purchases.md) to learn about license information, and how to properly list your in-app purchase in the Store.
-   When you code and test new in-app offers for the first time, you must use the [CurrentAppSimulator](/uwp/api/Windows.ApplicationModel.Store.CurrentAppSimulator) object instead of the [CurrentApp](/uwp/api/Windows.ApplicationModel.Store.CurrentApp) object. This way you can verify your license logic using simulated calls to the license server instead of calling the live server. To do this, you need to customize the file named WindowsStoreProxy.xml in %userprofile%\\AppData\\local\\packages\\&lt;package name&gt;\\LocalState\\Microsoft\\Windows Store\\ApiData. The Microsoft Visual Studio simulator creates this file when you run your app for the first time—or you can also load a custom one at runtime. For more info, see [Using the WindowsStoreProxy.xml file with CurrentAppSimulator](in-app-purchases-and-trials-using-the-windows-applicationmodel-store-namespace.md#proxy).
-   This topic also references code examples provided in the [Store sample](https://github.com/Microsoft/Windows-universal-samples/tree/win10-1507/Samples/Store). This sample is a great way to get hands-on experience with the different monetization options provided for Universal Windows Platform (UWP) apps.

## Make the purchase request for the in-app product

The purchase request for a specific product within a large catalog is handled in much the same way as any other purchase request within an app. When your app calls the new [RequestProductPurchaseAsync](/uwp/api/windows.applicationmodel.store.currentapp.requestproductpurchaseasync) method overload, your app provides both an *OfferId* and a [ProductPurchaseDisplayProperties](/uwp/api/windows.applicationmodel.store.productpurchasedisplayproperties) object populated with the name of the in-app product.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/InAppPurchasesAndLicenses/cs/ManageCatalog.cs" id="MakePurchaseRequest":::

## Report fulfillment of the in-app offer

Your app will need to report product fulfillment to the Store as soon as the offer has been fulfilled locally. In a large catalog scenario, if your app does not report offer fulfillment, the user will be unable to purchase any in-app offers using that same Store product listing.

As mentioned earlier, the Store only uses provided offer info to populate the [PurchaseResults](/uwp/api/Windows.ApplicationModel.Store.PurchaseResults), and does not create a persistent association between a large catalog offer and Store product listing. As a result you need to track user entitlement for products, and provide product-specific context (such as the name of the item being purchased or details about it) to the user outside of the [RequestProductPurchaseAsync](/uwp/api/windows.applicationmodel.store.currentapp.requestproductpurchaseasync) operation.

The following code demonstrates the fulfillment call, and a UI messaging pattern in which the specific offer info is inserted. In the absence of that specific product info, the example uses info from the product [ListingInformation](/uwp/api/Windows.ApplicationModel.Store.ListingInformation).

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/InAppPurchasesAndLicenses/cs/ManageCatalog.cs" id="ReportFulfillment":::

## Related topics

* [Enable in-app product purchases](enable-in-app-product-purchases.md)
* [Enable consumable in-app product purchases](enable-consumable-in-app-product-purchases.md)
* [Store sample (demonstrates trials and in-app purchases)](https://github.com/Microsoft/Windows-universal-samples/tree/win10-1507/Samples/Store)
* [RequestProductPurchaseAsync](/uwp/api/windows.applicationmodel.store.currentapp.requestproductpurchaseasync)
* [ProductPurchaseDisplayProperties](/uwp/api/Windows.ApplicationModel.Store.ProductPurchaseDisplayProperties)
