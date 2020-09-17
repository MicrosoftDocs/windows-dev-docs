---
Description: Whether your app is free or not, you can sell content, other apps, or new app functionality (such as unlocking the next level of a game) from right within the app. Here we show you how to enable these products in your app.
title: Enable in-app product purchases
ms.assetid: D158E9EB-1907-4173-9889-66507957BD6B
keywords: uwp, add-ons, in-app purchases, IAPs, Windows.ApplicationModel.Store
ms.date: 08/25/2017
ms.topic: article


ms.localizationpriority: medium
---
# Enable in-app product purchases

Whether your app is free or not, you can sell content, other apps, or new app functionality (such as unlocking the next level of a game) from right within the app. Here we show you how to enable these products in your app.

> [!IMPORTANT]
> This article demonstrates how to use members of the [Windows.ApplicationModel.Store](/uwp/api/windows.applicationmodel.store) namespace to enable in-app product purchases. This namespace is no longer being updated with new features, and we recommend that you use the [Windows.Services.Store](/uwp/api/windows.services.store) namespace instead. The **Windows.Services.Store** namespace supports the latest add-on types, such as Store-managed consumable add-ons and subscriptions, and is designed to be compatible with future types of products and features supported by Partner Center and the Store. The **Windows.Services.Store** namespace was introduced in Windows 10, version 1607, and it can only be used in projects that target **Windows 10 Anniversary Edition (10.0; Build 14393)** or a later release in Visual Studio. For more information about enabling in-app product purchases using the **Windows.Services.Store** namespace, see [this article](enable-in-app-purchases-of-apps-and-add-ons.md).

> [!NOTE]
> In-app products cannot be offered from a trial version of an app. Customers using a trial version of your app can only buy an in-app product if they purchase a full version of your app.

## Prerequisites

-   A Windows app in which to add features for customers to buy.
-   When you code and test new in-app products for the first time, you must use the [CurrentAppSimulator](/uwp/api/Windows.ApplicationModel.Store.CurrentAppSimulator) object instead of the [CurrentApp](/uwp/api/Windows.ApplicationModel.Store.CurrentApp) object. This way you can verify your license logic using simulated calls to the license server instead of calling the live server. To do this, you need to customize the file named WindowsStoreProxy.xml in %userprofile%\\AppData\\local\\packages\\&lt;package name&gt;\\LocalState\\Microsoft\\Windows Store\\ApiData. The Microsoft Visual Studio simulator creates this file when you run your app for the first time—or you can also load a custom one at runtime. For more info, see [Using the WindowsStoreProxy.xml file with CurrentAppSimulator](in-app-purchases-and-trials-using-the-windows-applicationmodel-store-namespace.md#proxy).
-   This topic also references code examples provided in the [Store sample](https://github.com/Microsoft/Windows-universal-samples/tree/win10-1507/Samples/Store). This sample is a great way to get hands-on experience with the different monetization options provided for Universal Windows Platform (UWP) apps.

## Step 1: Initialize the license info for your app

When your app is initializing, get the [LicenseInformation](/uwp/api/Windows.ApplicationModel.Store.LicenseInformation) object for your app by initializing the [CurrentApp](/uwp/api/Windows.ApplicationModel.Store.CurrentApp) or [CurrentAppSimulator](/uwp/api/Windows.ApplicationModel.Store.CurrentAppSimulator) to enable purchases of an in-app product.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/InAppPurchasesAndLicenses/cs/EnableInAppPurchases.cs" id="InitializeLicenseTest":::

## Step 2: Add the in-app offers to your app

For each feature that you want to make available through an in-app product, create an offer and add it to your app.

> [!IMPORTANT]
> You must add all the in-app products that you want to present to your customers to your app before you submit it to the Store. If you want to add new in-app products later, you must update your app and re-submit a new version.

1.  **Create an in-app offer token**

    You identify each in-app product in your app by a token. This token is a string that you define and use in your app and in the Store to identify a specific in-app product. Give it a unique (to your app) and meaningful name so that you can quickly identify the correct feature it represents while you are coding. Here are some examples of names:

    * "SpaceMissionLevel4"
    * "ContosoCloudSave"
    * "RainbowThemePack"

  > [!NOTE]
  > The in-app offer token that you use in your code must match the [product ID](../publish/set-your-add-on-product-id.md#product-id) value you specify when you [define the corresponding add-on for your app in Partner Center](../publish/add-on-submissions.md).

2.  **Code the feature in a conditional block**

    You must put the code for each feature that is associated with an in-app product in a conditional block that tests to see if the customer has a license to use that feature.

    Here's an example that shows how you can code a product feature named **featureName** in a license-specific conditional block. The string, **featureName**,is the token that uniquely identifies this product within the app and is also used to identify it in the Store.

    > [!div class="tabbedCodeSnippets"]
    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/InAppPurchasesAndLicenses/cs/EnableInAppPurchases.cs" id="CodeFeature":::

3.  **Add the purchase UI for this feature**

    Your app must also provide a way for your customers to purchase the product or feature offered by the in-app product. They can't purchase them through the Store in the same way they purchased the full app.

    Here's how to test to see if your customer already owns an in-app product and, if they don't, displays the purchase dialog so they can buy it. Replace the comment "show the purchase dialog" with your custom code for the purchase dialog (such as a page with a friendly "Buy this app!" button).

    > [!div class="tabbedCodeSnippets"]
    :::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/InAppPurchasesAndLicenses/cs/EnableInAppPurchases.cs" id="BuyFeature":::

## Step 3: Change the test code to the final calls

This is an easy step: change every reference to [CurrentAppSimulator](/uwp/api/Windows.ApplicationModel.Store.CurrentAppSimulator) to [CurrentApp](/uwp/api/Windows.ApplicationModel.Store.CurrentApp) in your app's code. You don't need to provide the WindowsStoreProxy.xml file any longer, so remove it from your app's path (although you may want to save it for reference when you configure the in-app offer in the next step).

## Step 4: Configure the in-app product offer in the Store

In Partner Center, navigate to your app and [create an add-on](../publish/add-on-submissions.md) that matches your in-app product offer. Define the product ID, type, price, and other properties for your add-on. Make sure that you configure it identically to the configuration you set in WindowsStoreProxy.xml when testing.

  > [!NOTE]
  > The in-app offer token that you use in your code must match the [product ID](../publish/set-your-add-on-product-id.md#product-id) value you specify for the corresponding add-on in Partner Center.

## Remarks

If you're interested in providing your customers with consumable in-app product options (items that can be purchased, used up, and then purchased again if desired), move on to the [Enable consumable in-app product purchases](enable-consumable-in-app-product-purchases.md) topic.

If you need to use receipts to verify that user made an in-app purchase, be sure to review [Use receipts to verify product purchases](use-receipts-to-verify-product-purchases.md).

## Related topics


* [Enable consumable in-app product purchases](enable-consumable-in-app-product-purchases.md)
* [Manage a large catalog of in-app products](manage-a-large-catalog-of-in-app-products.md)
* [Use receipts to verify product purchases](use-receipts-to-verify-product-purchases.md)
* [Store sample (demonstrates trials and in-app purchases)](https://github.com/Microsoft/Windows-universal-samples/tree/win10-1507/Samples/Store)
