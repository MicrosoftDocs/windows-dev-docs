---
author: mcleanbyron
Description: Whether your app is free or not, you can sell content, other apps, or new app functionality (such as unlocking the next level of a game) from right within the app. Here we show you how to enable these products in your app.
title: Enable in-app product purchases
ms.assetid: D158E9EB-1907-4173-9889-66507957BD6B
keywords: uwp, add-ons, in-app purchases, IAPs, Windows.ApplicationModel.Store
ms.author: mcleans
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
---

# Enable in-app product purchases

>**Note**&nbsp;&nbsp;This article demonstrates how to use members of the [Windows.ApplicationModel.Store](https://msdn.microsoft.com/library/windows/apps/windows.applicationmodel.store.aspx) namespace. If your app targets Windows 10, version 1607, or later, we recommend that you use members of the [Windows.Services.Store](https://msdn.microsoft.com/library/windows/apps/windows.services.store.aspx) namespace to manage add-ons (also known as in-app products or IAPs) instead of the **Windows.ApplicationModel.Store** namespace. For more information, see [In-app purchases and trials](in-app-purchases-and-trials.md).

Whether your app is free or not, you can sell content, other apps, or new app functionality (such as unlocking the next level of a game) from right within the app. Here we show you how to enable these products in your app.

> **Note**&nbsp;&nbsp;In-app products cannot be offered from a trial version of an app. Customers using a trial version of your app can only buy an in-app product if they purchase a full version of your app.

## Prerequisites

-   A Windows app in which to add features for customers to buy.
-   When you code and test new in-app products for the first time, you must use the [CurrentAppSimulator](https://msdn.microsoft.com/library/windows/apps/hh779766) object instead of the [CurrentApp](https://msdn.microsoft.com/library/windows/apps/hh779765) object. This way you can verify your license logic using simulated calls to the license server instead of calling the live server. To do this, you need to customize the file named WindowsStoreProxy.xml in %userprofile%\\AppData\\local\\packages\\&lt;package name&gt;\\LocalState\\Microsoft\\Windows Store\\ApiData. The Microsoft Visual Studio simulator creates this file when you run your app for the first time—or you can also load a custom one at runtime. For more info, see [Using the WindowsStoreProxy.xml file with CurrentAppSimulator](in-app-purchases-and-trials-using-the-windows-applicationmodel-store-namespace.md#proxy).
-   This topic also references code examples provided in the [Store sample](https://github.com/Microsoft/Windows-universal-samples/tree/win10-1507/Samples/Store). This sample is a great way to get hands-on experience with the different monetization options provided for Universal Windows Platform (UWP) apps.

## Step 1: Initialize the license info for your app

When your app is initializing, get the [LicenseInformation](https://msdn.microsoft.com/library/windows/apps/br225157) object for your app by initializing the [CurrentApp](https://msdn.microsoft.com/library/windows/apps/hh779765) or [CurrentAppSimulator](https://msdn.microsoft.com/library/windows/apps/hh779766) to enable purchases of an in-app product.

> [!div class="tabbedCodeSnippets"]
[!code-cs[EnableInAppPurchases](./code/InAppPurchasesAndLicenses/cs/EnableInAppPurchases.cs#InitializeLicenseTest)]

## Step 2: Add the in-app offers to your app

For each feature that you want to make available through an in-app product, create an offer and add it to your app.

> **Important**&nbsp;&nbsp;You must add all the in-app products that you want to present to your customers to your app before you submit it to the Store. If you want to add new in-app products later, you must update your app and re-submit a new version.

1.  **Create an in-app offer token**

    You identify each in-app product in your app by a token. This token is a string that you define and use in your app and in the Store to identify a specific in-app product. Give it a unique (to your app) and meaningful name so that you can quickly identify the correct feature it represents while you are coding. Here are some examples of names:

    -   "SpaceMissionLevel4"

    -   "ContosoCloudSave"

    -   "RainbowThemePack"

2.  **Code the feature in a conditional block**

    You must put the code for each feature that is associated with an in-app product in a conditional block that tests to see if the customer has a license to use that feature.

    Here's an example that shows how you can code a product feature named **featureName** in a license-specific conditional block. The string, **featureName**, is the token that uniquely identifies this product within the app and is also used to identify it in the Store.

    > [!div class="tabbedCodeSnippets"]
    [!code-cs[EnableInAppPurchases](./code/InAppPurchasesAndLicenses/cs/EnableInAppPurchases.cs#CodeFeature)]

3.  **Add the purchase UI for this feature**

    Your app must also provide a way for your customers to purchase the product or feature offered by the in-app product. They can't purchase them through the Store in the same way they purchased the full app.

    Here's how to test to see if your customer already owns an in-app product and, if they don't, displays the purchase dialog so they can buy it. Replace the comment "show the purchase dialog" with your custom code for the purchase dialog (such as a page with a friendly "Buy this app!" button).

    > [!div class="tabbedCodeSnippets"]
    [!code-cs[EnableInAppPurchases](./code/InAppPurchasesAndLicenses/cs/EnableInAppPurchases.cs#BuyFeature)]

## Step 3: Change the test code to the final calls

This is an easy step: change every reference to [CurrentAppSimulator](https://msdn.microsoft.com/library/windows/apps/hh779766) to [CurrentApp](https://msdn.microsoft.com/library/windows/apps/hh779765) in your app's code. You don't need to provide the WindowsStoreProxy.xml file any longer, so remove it from your app's path (although you may want to save it for reference when you configure the in-app offer in the next step).

## Step 4: Configure the in-app product offer in the Store

In the Dev Center dashboard, define the product ID, type, price, and other properties for your in-app product. Make sure that you configure it identically to the configuration you set in WindowsStoreProxy.xml when testing. For more information, see [IAP submissions](https://msdn.microsoft.com/library/windows/apps/mt148551).

## Remarks

If you're interested in providing your customers with consumable in-app product options (items that can be purchased, used up, and then purchased again if desired), move on to the [Enable consumable in-app product purchases](enable-consumable-in-app-product-purchases.md) topic.

If you need to use receipts to verify that user made an in-app purchase, be sure to review [Use receipts to verify product purchases](use-receipts-to-verify-product-purchases.md).

## Related topics


* [Enable consumable in-app product purchases](enable-consumable-in-app-product-purchases.md)
* [Manage a large catalog of in-app products](manage-a-large-catalog-of-in-app-products.md)
* [Use receipts to verify product purchases](use-receipts-to-verify-product-purchases.md)
* [Store sample (demonstrates trials and in-app purchases)](https://github.com/Microsoft/Windows-universal-samples/tree/win10-1507/Samples/Store)
