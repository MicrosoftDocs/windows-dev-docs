---
author: mcleanbyron
description: Learn how to use the Windows.Services.Store namespace to implement subscription add-ons.
title: Enable subscription add-ons for your app
keywords: windows 10, uwp, subscriptions, add-ons, in-app purchases, IAPs, Windows.Services.Store
ms.author: mcleans
ms.date: 08/01/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
---

# Enable subscription add-ons for your app

> [!IMPORTANT]
> Currently, the ability to create subscription add-ons is only available to a set of developer accounts who are participating in an early adoption program. We will make subscription add-ons available to all developer accounts in the future, and we are making this preliminary documentation available now to give developers a preview of this feature.

If your UWP app targets Windows 10, version 1607, or later, you can offer in-app purchases of *subscription* add-ons to your customers. You can use subscriptions to sell digital products in your app (such as app features or digital content) with automated recurring billing periods.

## Feature highlights

Subscription add-ons for UWP apps support the following features:

* You can choose from subscription periods of 1 month, 3 months, 6 months, 1 year, or 2 years. Some apps can also use a 6-hour subscription period for testing purposes only.
* You can add free trial periods of 1 week or 1 month to your subscription.
* The Windows SDK [provides APIs](#code-examples) you can use in your app to get info about available subscription add-ons for the app and enable the purchase of a subscription add-on. We also provide REST APIs you can call from your services to [manage subscriptions for a user](#manage-subscriptions).
* You can view analytic reports that provide the number of subscription acquisitions, active subscribers, and canceled subscriptions in a given time period.
* Customers can manage their subscription on the [http://account.microsoft.com/services](http://account.microsoft.com/services) page for their Microsoft account. Customers can use this page to view all of the subscriptions they have acquired, cancel a subscription, and change the form of payment that is associated with their subscription.

> [!NOTE]
> To enable the purchase of subscription add-ons in your app, your app must target Windows 10, version 1607, or a later version, and it must use the APIs in the **Windows.Services.Store** namespace to implement the in-app purchase experience instead of the **Windows.ApplicationModel.Store** namespace. For more information about the differences between these namespaces, see [In-app purchases and trials](in-app-purchases-and-trials.md).

## Steps to enable a subscription add-on for your app

To enable the purchase of subscription add-ons in your app, follow these steps.

1. [Create an add-on submission](../publish/add-on-submissions.md) for your subscription in the Dev Center dashboard and publish the submission. As you follow the add-on submission process, pay close attention to the following properties:

  * [Product type](../publish/set-your-add-on-product-id.md#product-type): Make sure you select **Subscription**.

  * [Subscription period](../publish/enter-add-on-properties.md#subscription-period): Choose the recurring billing period for your subscription. Each subscription add-on supports a single subscription period and trial period. You must create a different subscription add-on for each type of subscription you want to offer in your app.

    For example, if you wanted to offer a monthly subscription with no trial, a monthly subscription with a one-month trial, an annual subscription with no trial, and an annual subscription with a one-month trial, you would need to create four subscription add-ons.

        > [!NOTE]
        > If you are in the process of implementing the in-app purchase experience for the submission in your app, we recommend that you choose **For testing only – 6 hours** to help you test the subscription experience. You can choose this test period only if you select one of the **Hidden in the Store** [visibility options](../publish/set-add-on-pricing-and-availability.md#visibility) for the add-on.

  * [Trial period](../publish/enter-add-on-properties.md#free-trial): Consider choosing a 1 week or 1 month trial period for your subscription to enable users to try your subscription content before they buy it.

    To acquire a free trial of your subscription, a user must purchase your subscription through the standard in-app purchase process, including a valid form of payment. They are not charged any money during the trial period. At the end of the trial period, the subscription automatically converts to the full subscription and the user's payment instrument will be charged for the first period of the paid subscription. If the user chooses to cancel their subscription during the trial period, the subscription remains active until the end of the trial period.
        > [!NOTE]
        > Some trial durations are not available for certain subscription periods. After you configure a trial period for a subscription add-on and publish the add-on, you cannot change or remove the trial period.

  * [Visibility](../publish/set-add-on-pricing-and-availability.md#visibility): While you are testing your subscription add-on, we recommend that you choose one of the **Hidden in the Store** options. After you are done testing the in-app purchase experience of your subscription in your app, you can change this to **Can be displayed in the parent product’s Store listing**.

  * [Pricing](../publish/set-add-on-pricing-and-availability.md?#pricing): Choose the price of your subscription in this section. After you publish the add-on, you cannot raise the price of the subscription (however, you can lower the price later).
      > [!IMPORTANT]
      > By default, when you create any add-on the price is initially set to **Free**. Because you cannot raise the price of a subscription add-on after you complete the add-on submission in the dashboard, be sure to choose the price of your subscription here.

2. In your app, use APIs in the [**Windows.Services.Store**](https://docs.microsoft.com/uwp/api/windows.services.store) namespace to confirm whether the current user has already acquired your subscription add-on and then offer it for sale to the user as an in-app purchase. See the [code examples](#code-examples) section in this article for more details.

3. Test the in-app purchase implementation of your subscription in your app. You'll need to download your app from the Store to your development device to use its license for testing. For more information, see our [testing guidance](in-app-purchases-and-trials.md#testing) for in-app purchases.  
    > [!NOTE]
    > If your app has access to the **For testing only – 6 hours** subscription period, we recommend that you choose this period for your add-on in the dashboard during testing.

4. Create and publish an app submission that includes your updated app package, including your tested code. For more information, see [App submissions](../publish/app-submissions.md).
    > [!IMPORTANT]
    > If you chose the **For testing only – 6 hours** subscription period for your add-on in the dashboard for testing, make sure that you also update your add-on submission to change your subscription period.

<span id="code-examples"/>
## Code examples

The code examples in this section demonstrate how to use the APIs in the [**Windows.Services.Store**](https://docs.microsoft.com/uwp/api/windows.services.store) namespace to get info about subscription add-ons for the current app and request the purchase a subscription add-on on behalf of the current user.

These examples have the following prerequisites:
* A Visual Studio project for a Universal Windows Platform (UWP) app that targets Windows 10, version 1607, or later.
* You have [created an app submission](https://msdn.microsoft.com/windows/uwp/publish/app-submissions) in the Windows Dev Center dashboard and this app is published in the Store. You can optionally configure the app so it is not discoverable in the Store while you test it. For more information, see the [testing guidance](in-app-purchases-and-trials.md#testing).
* You have [created a subscription add-on for the app](../publish/add-on-submissions.md) in the Dev Center dashboard.

The code in these examples assumes:
* The code runs in the context of a [**Page**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.page.aspx) that contains a [**ProgressRing**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.progressring.aspx) named ```workingProgressRing``` and a [**TextBlock**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.textblock.aspx) named ```textBlock```. These objects are used to indicate that an asynchronous operation is occurring and to display output messages, respectively.
* The code file has a **using** statement for the **Windows.Services.Store** namespace.
* The app is a single-user app that runs only in the context of the user that launched the app. For more information, see [In-app purchases and trials](in-app-purchases-and-trials.md#api_intro).

> [!NOTE]
> If you have a desktop application that uses the [Desktop Bridge](https://developer.microsoft.com/windows/bridges/desktop), you may need to add additional code not shown in these examples to configure the [**StoreContext**](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storecontext.aspx) object. For more information, see [Using the StoreContext class in a desktop application that uses the Desktop Bridge](in-app-purchases-and-trials.md#desktop).

### Get info about subscription add-ons for the current app

This code example demonstrates how to get info for the subscription add-ons that are available in your app. To get this info, first use the [**GetAssociatedStoreProductsAsync**](https://docs.microsoft.com/uwp/api/Windows.Services.Store.StoreContext#Windows_Services_Store_StoreContext_GetAssociatedStoreProductsAsync_) method to get the collection of [**StoreProduct**](https://docs.microsoft.com/uwp/api/Windows.Services.Store.StoreProduct) objects that represent each of the available add-ons for the app. Then, get the [**StoreSku**](https://docs.microsoft.com/uwp/api/windows.services.store.storesku) for each product and use the [**IsSubscription**](https://docs.microsoft.com/uwp/api/windows.services.store.storesku#Windows_Services_Store_StoreSku_IsSubscription_) and [**SubscriptionInfo**](https://docs.microsoft.com/uwp/api/windows.services.store.storesku#Windows_Services_Store_StoreSku_SubscriptionInfo_) properties to access the subscription info.

> [!div class="tabbedCodeSnippets"]
[!code-cs[Subscriptions](./code/InAppPurchasesAndLicenses_RS1/cs/GetSubscriptionAddOnsPage.xaml.cs#GetSubscriptions)]

### Purchase a subscription add-on

This example demonstrates how to use the [**RequestPurchaseAsync**](https://docs.microsoft.com/uwp/api/windows.services.store.storeproduct#Windows_Services_Store_StoreProduct_RequestPurchaseAsync_) method of the [**StoreProduct**](https://docs.microsoft.com/uwp/api/windows.services.store.storeproduct) class to purchase a subscription add-on. This example assumes that you already know the [Store ID](in-app-purchases-and-trials.md#store_ids) of the subscription add-on you want to purchase.

> [!div class="tabbedCodeSnippets"]
[!code-cs[Subscriptions](./code/InAppPurchasesAndLicenses_RS1/cs/PurchaseSubscriptionAddOnPage.xaml.cs#PurchaseSubscription)]

<span id="manage-subscriptions" />
## Manage subscriptions from your services

After your updated app is in the Store and customers can buy your subscription add-on, you may have scenarios where you need to manage the subscription for a customer. We provide REST APIs you can call from your services to perform the following subscription management tasks:

* [Get the subscriptions that a user has an entitlement to use](get-subscriptions-for-a-user.md). If your subscription is part of a cross-platform service, you can call this API to determine whether the specified user has an entitlement for your subscription and the status of their subscription in the context of your UWP app. You can then use this information to update the status of the subscription on other platforms that your service supports.

* [Change the billing state of a subscription for a given user](change-the-billing-state-of-a-subscription-for-a-user.md). Use this API to cancel, extend, or disable automatic renewal for a subscription.

<span id="cancellations" />
## Cancellations

Customers can use the [http://account.microsoft.com/services](http://account.microsoft.com/services) page for their Microsoft account to view all of the subscriptions they have acquired, cancel a subscription, and change the form of payment that is associated with their subscription. When a customer cancels a subscription using this page, they continue to have access to the subscription for the duration of the current billing cycle. They do not get a refund for any part of the current billing cycle. At the end of the current billing cycle, their subscription is deactivated.

You can also cancel a subscription on behalf of a user by using our REST API to [change the billing state of a subscription for a given user](change-the-billing-state-of-a-subscription-for-a-user.md).

## Unsupported scenarios

The following scenarios are not currently supported for subscription add-ons.

* Selling subscriptions to customers directly via the Store is not supported at this time. Subscriptions are available for in-app purchases of digital products only.
* Customers cannot switch subscription periods using the [http://account.microsoft.com/services](http://account.microsoft.com/services) page for their Microsoft account. To switch to a different subscription period, customers much cancel their current subscription and then purchase a subscription with a different subscription period from your app.
* Tier switching is currently not supported for subscription add-ons (for example, switching a customer from a basic subscription to a premium subscription with more features).
* [Sales](../publish/put-apps-and-add-ons-on-sale.md) and [promotional codes](../publish/generate-promotional-codes.md) are currently not supported for subscription add-ons.


## Related topics

* [In-app purchases and trials](in-app-purchases-and-trials.md)
* [Get product info for apps and add-ons](get-product-info-for-apps-and-add-ons.md)
