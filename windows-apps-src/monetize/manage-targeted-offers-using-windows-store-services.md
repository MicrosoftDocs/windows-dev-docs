---
author: mcleanbyron
ms.assetid: 9F0A59A1-FAD7-4AD5-B78B-C1280F215D23
description: Use the Windows Store targeted offers API to claim targeted offers that are available for an app.
title: Manage targeted offers using Store services
ms.author: mcleans
ms.date: 05/11/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, Store services, Windows Store targeted offers API, targeted offers
---

# Manage targeted offers using Store services

If you create a *targeted offer* in the **Monetize > Targeted offers** page for your app in the Windows Dev Center dashboard, use the *Windows Store targeted offers API* in your app's code to implement the in-app experience for the targeted offer. For more information about targeted offers and how to create them in the dashboard, see [Use targeted offers to maximize engagement and conversions](../publish/use-targeted-offers-to-maximize-engagement-and-conversions.md).

The targeted offers API is a REST API that you can use to perform these tasks:

* Get the targeted offers that are available for the current user, based on whether or not the user is part of the customer segment for the targeted offer.
* If the user purchases the targeted offer, you can submit a claim to the Store so you can receive the bonus that is associated with the targeted offer. This is only necessary if the targeted offer is associated with a Microsoft-sponsored promotion that pays a bonus to developers for each successful conversion.

To use this API in your app's code, follow these steps:

1.  [Get a Microsoft Account token](#obtain-a-microsoft-account-token) for the current signed-in user of your app.
2.  [Get the targeted offers for the current user](#get-targeted-offers).
3.  [Purchase the add-on that is associated with a targeted offer](#purchase-add-on).
3.  [Claim the targeted offer](#claim-targeted-offer) (if the targeted offer is associated with a Microsoft-sponsored promotion that pays a bonus to developers for each successful conversion).

> [!NOTE]
> The ability to associate a targeted offer with a Microsoft-sponsored promotion and then submit a claim for the offer is currently not available to all developer accounts.

For a complete code example that demonstrates all of these steps, see the [code example](#code-example) at the end of this article. The following sections provide more details about each step.

<span id="obtain-a-microsoft-account-token" />
## Get a Microsoft Account token for the current user

In your app's code, get a Microsoft Account (MSA) token for the current signed-in user. You must pass this token in the ```Authorization``` request header for each of the methods in the Windows Store targeted offers API. This token is used by the Store to retrieve the targeted offers that are available for the current user.

To get the MSA token, use the [WebAuthenticationCoreManager](https://docs.microsoft.com/uwp/api/windows.security.authentication.web.core.webauthenticationcoremanager) class to request a token using the scope ```devcenter_implicit.basic,wl.basic```. The following example demonstrates how to do this. This example is an excerpt from the [complete example](#code-example), and it requires **using** statements that are provided in the complete example.

[!code-cs[TargetedOffers](./code/StoreServicesExamples_TargetedOffers/cs/TargetedOffers.cs#GetMSAToken)]

For more information about getting MSA tokens, see [Web account manager](../security/web-account-manager.md).

<span id="get-targeted-offers" />
## Get the targeted offers for the current user

After you have an MSA token for the current user, call the GET method of the ```https://manage.devcenter.microsoft.com/v2.0/my/storeoffers/user``` URI to get the available targeted offers for the current user. For more information about this REST method, see [Get targeted offers](get-targeted-offers.md).

This method returns the product IDs of the add-ons that that are associated with the targeted offers that are available for the current user. With this information, you can offer one or more of the targeted offers as an in-app purchase to the user. This method also returns a tracking ID that you can use to [submit a claim](#claim-targeted-offer) to the Store so you can receive any bonus that is associated with one of the targeted offers.

The following example demonstrates how to get the targeted offers for the current user. This example is an excerpt from the [complete example](#code-example). It requires the [Json.NET](http://www.newtonsoft.com/json) library from Newtonsoft and additional classes and **using** statements that are provided in the complete example.

[!code-cs[TargetedOffers](./code/StoreServicesExamples_TargetedOffers/cs/TargetedOffers.cs#GetTargetedOffers)]

<span id="purchase-add-on" />
## Purchase the add-on that is associated with a targeted offer

Next, offer one of the targeted offers for purchase to the user. If the user agrees to purchase the targeted offer, use one of the following methods to programmatically purchase the add-on that is associated with the targeted offer. Use the product ID values that you retrieved in the previous step to identify the add-on you want to purchase.

* If your app targets Windows 10, version 1607, or later, we recommend that you use one of the **RequestPurchaseAsync** methods in the [Windows.Services.Store](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Store) namespace. For more information about using these methods, see [Enable in-app purchases of apps and add-ons](enable-in-app-purchases-of-apps-and-add-ons.md).

* If your app targets an earlier version of Windows 10, use the [RequestProductPurchaseAsync](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Store.CurrentApp#Windows_ApplicationModel_Store_CurrentApp_RequestProductPurchaseAsync_System_String_) method in the [Windows.ApplicationModel.Store](https://msdn.microsoft.com/library/windows/apps/windows.applicationmodel.store.aspx) namespace to purchase the add-on. For more information about using this method, see [Enable in-app product purchases](enable-in-app-product-purchases.md).

For code examples that demonstrate each option, see the [code example](#code-example) at the end of this article.

> [!NOTE]
> There are two different namespaces for implementing in-app purchases in a UWP app: [Windows.Services.Store](https://msdn.microsoft.com/library/windows/apps/windows.services.store.aspx) (introduced in Windows 10, version 1607) and [Windows.ApplicationModel.Store](https://msdn.microsoft.com/library/windows/apps/windows.applicationmodel.store.aspx) (available in all versions of Windows 10). For more information about the differences between these namespaces, see [In-app purchases and trials](in-app-purchases-and-trials.md).

<span id="claim-targeted-offer" />
## Submit a claim for a targeted offer

If the user purchases a targeted offer that is associated with a Microsoft-sponsored promotion that pays a bonus to developers for each successful conversion, you must submit a claim to the Store so you can receive your bonus. When you submit your claim, you must provide a value that represents the purchase confirmation. The way you obtain this purchase confirmation depends on whether your app uses APIs in the **Windows.ApplicationModel.Store** namespace or **Windows.Services.Store** namespace to purchase the add-on.

> [!NOTE]
> The ability to associate a targeted offer with a Microsoft-sponsored promotion and then submit a claim for the offer is currently not available to all developer accounts.

### Apps that use the Windows.ApplicationModel.Store namespace

1. After you purchase the add-on by using the [RequestProductPurchaseAsync](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Store.CurrentApp#Windows_ApplicationModel_Store_CurrentApp_RequestProductPurchaseAsync_System_String_) method in the **Windows.ApplicationModel.Store** namespace, make sure that you retrieve a [purchase receipt](use-receipts-to-verify-product-purchases.md). This receipt is available in the [PurchaseResults.ReceiptXml](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.purchaseresults#Windows_ApplicationModel_Store_PurchaseResults_ReceiptXml_) object that is returned by the **RequestProductPurchaseAsync** method. This receipt represents the purchase confirmation that you will use in the following step.

2. Send a POST message to the ```https://manage.devcenter.microsoft.com/v2.0/my/storeoffers/user``` URI to submit a claim for the conversion of the targeted offer. For more information about this REST method, see [Claim a targeted offer](claim-a-targeted-offer.md). In the request body, pass the following data:

  * *storeOffer* field: Pass one of the JSON objects you received in the response body of the [Get targeted offers](get-targeted-offers.md) method (this object should include *offers* and *trackingId* fields).

  * *receipt* field: Pass the receipt string that you retrieved in the previous step (this is available in the **PurchaseResults.ReceiptXml** object that is returned by the **RequestProductPurchaseAsync** method).

The following example demonstrates how to purchase and claim a targeted offer by using the APIs in the **Windows.ApplicationModel.Store** namespace. This example is an excerpt from the [complete example](#code-example). It requires the [Json.NET](http://www.newtonsoft.com/json) library from Newtonsoft and additional classes and **using** statements that are provided in the complete example.

[!code-cs[TargetedOffers](./code/StoreServicesExamples_TargetedOffers/cs/TargetedOffers.cs#ClaimOfferOnAnyVersionWindows10)]

### Apps that use the Windows.Services.Store namespace

1. After you purchase the add-on by using one of the **RequestPurchaseAsync** methods in the [Windows.Services.Store](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Store) namespace, get the *order ID* for the purchase by following these steps. This value represents the purchase confirmation.

  1. Call the [GetUserCollectionAsync](https://docs.microsoft.com/uwp/api/Windows.Services.Store.StoreContext#Windows_Services_Store_StoreContext_GetUserCollectionAsync_Windows_Foundation_Collections_IIterable_System_String__) method to get the collection of [StoreProduct](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storeproduct.aspx) objects that represent the add-ons that the user has acquired.

  2. In this collection, get the **StoreProduct** object that represents the add-on that was purchased for the targeted offer.

  3. Get the value of the [ExtendedJsonData](https://docs.microsoft.com/uwp/api/Windows.Services.Store.StoreProduct#Windows_Services_Store_StoreProduct_ExtendedJsonData_) property of this **StoreProduct** object. This property returns a JSON-formatted string that contains the complete Store-related data for the add-on.

  4. Parse this JSON string and retrieve the value of the *orderId* field in the string.

2. Send a POST message to the ```https://manage.devcenter.microsoft.com/v2.0/my/storeoffers/user``` URI to submit a claim for the conversion of the targeted offer. For more information about this REST method, see [Claim a targeted offer](claim-a-targeted-offer.md). In the request body, pass the following objects:

  * *storeOffer* field: Pass one of the JSON objects you received in the response body of the [Get targeted offers](get-targeted-offers.md) method (this object should include *offers* and *trackingId* fields).

  * *receipt* field: Pass the *orderId* value you retrieved in the previous step.

The following example demonstrates how to purchase and claim a targeted offer by using the APIs in the **Windows.Services.Store** namespace. This example is an excerpt from the [complete example](#code-example). It requires the [Json.NET](http://www.newtonsoft.com/json) library from Newtonsoft and additional classes and **using** statements that are provided in the complete example.

[!code-cs[TargetedOffers](./code/StoreServicesExamples_TargetedOffers/cs/TargetedOffers.cs#ClaimOfferOnWindows1607AndLater)]

<span id="code-example" />
## Complete code example

The following code example demonstrates the following tasks:

* Get an MSA token for the current user.
* Get all of the targeted offers for the current user by using the [Get targeted offers](get-targeted-offers.md) method.
* Purchase the add-on that is associated with a targeted offer.
* Claim the targeted offer by using the [Claim a targeted offer](claim-a-targeted-offer.md) method.

This example requires the [Json.NET](http://www.newtonsoft.com/json) library from Newtonsoft. The example uses this library to serialize and deserialize JSON-formatted data.

> [!NOTE]
> There are two different namespaces for implementing in-app purchases in a UWP app: [Windows.Services.Store](https://msdn.microsoft.com/library/windows/apps/windows.services.store.aspx) (introduced in Windows 10, version 1607) and [Windows.ApplicationModel.Store](https://msdn.microsoft.com/library/windows/apps/windows.applicationmodel.store.aspx) (available in all versions of Windows 10). This example uses [version adaptive code](../debug-test-perf/version-adaptive-code.md) to use both of these namespaces in the same app to purchase an add-on and submit a claim for the targeted offer. To use this code, the target OS version of your project must be **Windows 10 Anniversary Edition (10.0; Build 14394)** or later, although the minimum OS version can be an earlier version if you want to your app to run on earlier versions of Windows 10. For more information about the differences between these namespaces, see [In-app purchases and trials](in-app-purchases-and-trials.md).

[!code-cs[TargetedOffers](./code/StoreServicesExamples_TargetedOffers/cs/TargetedOffers.cs#GetTargetedOffersSample)]

## Related topics

* [Use targeted offers to maximize engagement and conversions](../publish/use-targeted-offers-to-maximize-engagement-and-conversions.md)
* [Get targeted offers](get-targeted-offers.md)
* [Claim a targeted offer](claim-a-targeted-offer.md)
