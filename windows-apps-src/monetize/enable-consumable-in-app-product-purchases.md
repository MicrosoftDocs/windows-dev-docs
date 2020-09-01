---
Description: Offer consumable in-app products&\#8212;items that can be purchased, used, and purchased again&\#8212;through the Store commerce platform to provide your customers with a purchase experience that is both robust and reliable.
title: Enable consumable in-app product purchases
ms.assetid: F79EE369-ACFC-4156-AF6A-72D1C7D3BDA4
keywords: uwp, consumable, add-ons, in-app purchases, IAPs, Windows.ApplicationModel.Store
ms.date: 08/25/2017
ms.topic: article


ms.localizationpriority: medium
---
# Enable consumable in-app product purchases

Offer consumable in-app products—items that can be purchased, used, and purchased again—through the Store commerce platform to provide your customers with a purchase experience that is both robust and reliable. This is especially useful for things like in-game currency (gold, coins, etc.) that can be purchased and then used to purchase specific power-ups.

> [!IMPORTANT]
> This article demonstrates how to use members of the [Windows.ApplicationModel.Store](/uwp/api/windows.applicationmodel.store) namespace to enable consumable in-app product purchases. This namespace is no longer being updated with new features, and we recommend that you use the [Windows.Services.Store](/uwp/api/windows.services.store) namespace instead. The **Windows.Services.Store** namespace supports the latest add-on types, such as Store-managed consumable add-ons and subscriptions, and is designed to be compatible with future types of products and features supported by Partner Center and the Store. The **Windows.Services.Store** namespace was introduced in Windows 10, version 1607, and it can only be used in projects that target **Windows 10 Anniversary Edition (10.0; Build 14393)** or a later release in Visual Studio. For more information about enabling consumable in-app product purchases using the **Windows.Services.Store** namespace, see [this article](enable-consumable-add-on-purchases.md).

## Prerequisites

-   This topic covers the purchase and fulfillment reporting of consumable in-app products. If you are unfamiliar with in-app products, please review [Enable in-app product purchases](enable-in-app-product-purchases.md) to learn about license information, and how to properly list in-app products in the Store.
-   When you code and test new in-app products for the first time, you must use the [CurrentAppSimulator](/uwp/api/Windows.ApplicationModel.Store.CurrentAppSimulator) object instead of the [CurrentApp](/uwp/api/Windows.ApplicationModel.Store.CurrentApp) object. This way you can verify your license logic using simulated calls to the license server instead of calling the live server. To do this, you need to customize the file named WindowsStoreProxy.xml in %userprofile%\\AppData\\local\\packages\\&lt;package name&gt;\\LocalState\\Microsoft\\Windows Store\\ApiData. The Microsoft Visual Studio simulator creates this file when you run your app for the first time—or you can also load a custom one at runtime. For more info, see [Using the WindowsStoreProxy.xml file with CurrentAppSimulator](in-app-purchases-and-trials-using-the-windows-applicationmodel-store-namespace.md#proxy).
-   This topic also references code examples provided in the [Store sample](https://github.com/Microsoft/Windows-universal-samples/tree/win10-1507/Samples/Store). This sample is a great way to get hands-on experience with the different monetization options provided for Universal Windows Platform (UWP) apps.

## Step 1: Making the purchase request

The initial purchase request is made with [RequestProductPurchaseAsync](/uwp/api/windows.applicationmodel.store.currentapp.requestproductpurchaseasync) like any other purchase made through the Store. The difference for consumable in-app products is that after a successful purchase, a customer cannot purchase the same product again until the app has notified the Store that the previous purchase was successfully fulfilled. It's your app's responsibility to fulfill purchased consumables and notify the Store of the fulfillment.

The following example shows a consumable in-app product purchase request. You'll notice code comments indicating when your app should conduct its local fulfillment of the consumable in-app product for two different scenarios—when the request is successful, and when the request is not successful because of an unfulfilled purchase of that same product.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/InAppPurchasesAndLicenses/cs/EnableConsumablePurchases.cs" id="MakePurchaseRequest":::

## Step 2: Tracking local fulfillment of the consumable

When granting your customer access to the consumable in-app product, it's important to keep track of which product is fulfilled (*productId*), and which transaction that fulfillment is associated with (*transactionId*).

> [!IMPORTANT]
> Your app is responsible for the accurately reporting fulfillment to the Store. This step is essential to maintaining a fair and reliable purchase experience for your customers.

The following example demonstrates use of the [PurchaseResults](/uwp/api/Windows.ApplicationModel.Store.PurchaseResults) properties from the [RequestProductPurchaseAsync](/uwp/api/windows.applicationmodel.store.currentapp.requestproductpurchaseasync) call in the previous step to identify the purchased product for fulfillment. A collection is used to store the product information in a location that can later be referenced to confirm that local fulfillment was successful.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/InAppPurchasesAndLicenses/cs/EnableConsumablePurchases.cs" id="GrantFeatureLocally":::

This next example shows how to use the array from the previous example to access product ID/transaction ID pairs that are later used when reporting fulfillment to the Store.

> [!IMPORTANT]
> Whatever methodology your app uses to track and confirm fulfillment, your app must demonstrate due diligence to ensure that your customers are not charged for items they haven't received.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/InAppPurchasesAndLicenses/cs/EnableConsumablePurchases.cs" id="IsLocallyFulfilled":::

## Step 3: Reporting product fulfillment to the Store

After local fulfillment is completed, your app must make a [ReportConsumableFulfillmentAsync](/uwp/api/windows.applicationmodel.store.currentapp.reportconsumablefulfillmentasync) call that includes the *productId* and the transaction the product purchase is included in.

> [!IMPORTANT]
> Failure to report fulfilled consumable in-app products to the Store will result in the user being unable to purchase that product again until fulfillment for the previous purchase is reported.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/InAppPurchasesAndLicenses/cs/EnableConsumablePurchases.cs" id="ReportFulfillment":::

## Step 4: Identifying unfulfilled purchases

Your app can use the [GetUnfulfilledConsumablesAsync](/uwp/api/windows.applicationmodel.store.currentapp.getunfulfilledconsumablesasync) method to check for unfulfilled consumable in-app products at any time. This method should be called on a regular basis to check for unfulfilled consumables that exist due to unanticipated app events like an interruption in network connectivity or app termination.

The following example demonstrates how [GetUnfulfilledConsumablesAsync](/uwp/api/windows.applicationmodel.store.currentapp.getunfulfilledconsumablesasync) can be used to enumerate unfulfilled consumables, and how your app can iterate through this list to complete local fulfillment.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/InAppPurchasesAndLicenses/cs/EnableConsumablePurchases.cs" id="GetUnfulfilledConsumables":::

## Related topics

* [Enable in-app product purchases](enable-in-app-product-purchases.md)
* [Store sample (demonstrates trials and in-app purchases)](https://github.com/Microsoft/Windows-universal-samples/tree/win10-1507/Samples/Store)
* [Windows.ApplicationModel.Store](/uwp/api/Windows.ApplicationModel.Store)
 

 
