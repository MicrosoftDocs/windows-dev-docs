---
description: Learn how to use the Windows.Services.Store namespace to implement subscription add-ons.
title: Enable subscription add-ons for your app
keywords: windows 10, uwp, subscriptions, add-ons, in-app purchases, IAPs, Windows.Services.Store
ms.date: 12/06/2017
ms.topic: article


ms.localizationpriority: medium
---
# Enable subscription add-ons for your app

Your Universal Windows Platform (UWP) app can offer in-app purchases of *subscription* add-ons to your customers. You can use subscriptions to sell digital products in your app (such as app features or digital content) with automated recurring billing periods.

> [!NOTE]
> To enable the purchase of subscription add-ons in your app, your project must target **Windows 10 Anniversary Edition (10.0; Build 14393)** or a later release in Visual Studio (this corresponds to Windows 10, version 1607), and it must use the APIs in the **Windows.Services.Store** namespace to implement the in-app purchase experience instead of the **Windows.ApplicationModel.Store** namespace. For more information about the differences between these namespaces, see [In-app purchases and trials](in-app-purchases-and-trials.md).

## Feature highlights

Subscription add-ons for UWP apps support the following features:

* You can choose from subscription periods of 1 month, 3 months, 6 months, 1 year, or 2 years.
* You can add free trial periods of 1 week or 1 month to your subscription.
* The Windows SDK [provides APIs](#code-examples) you can use in your app to get info about available subscription add-ons for the app and enable the purchase of a subscription add-on. We also provide REST APIs you can call from your services to [manage subscriptions for a user](#manage-subscriptions).
* You can view analytic reports that provide the number of subscription acquisitions, active subscribers, and canceled subscriptions in a given time period.
* Customers can manage their subscription on the [https://account.microsoft.com/services](https://account.microsoft.com/services) page for their Microsoft account. Customers can use this page to view all of the subscriptions they have acquired, cancel a subscription, and change the form of payment that is associated with their subscription.

## Steps to enable a subscription add-on for your app

To enable the purchase of subscription add-ons in your app, follow these steps.

1. [Create an add-on submission](../publish/add-on-submissions.md) for your subscription in Partner Center and publish the submission. As you follow the add-on submission process, pay close attention to the following properties:

    * [Product type](../publish/set-your-add-on-product-id.md#product-type): Make sure you select **Subscription**.

    * [Subscription period](../publish/enter-add-on-properties.md#subscription-period): Choose the recurring billing period for your subscription. You cannot change the subscription period after you publish your add-on.

        Each subscription add-on supports a single subscription period and trial period. You must create a different subscription add-on for each type of subscription you want to offer in your app. For example, if you wanted to offer a monthly subscription with no trial, a monthly subscription with a one-month trial, an annual subscription with no trial, and an annual subscription with a one-month trial, you would need to create four subscription add-ons.

    * [Trial period](../publish/enter-add-on-properties.md#free-trial): Consider choosing a 1 week or 1 month trial period for your subscription to enable users to try your subscription content before they buy it. You cannot change or remove the trial period after you publish your subscription add-on.

        To acquire a free trial of your subscription, a user must purchase your subscription through the standard in-app purchase process, including a valid form of payment. They are not charged any money during the trial period. At the end of the trial period, the subscription automatically converts to the full subscription and the user's payment instrument will be charged for the first period of the paid subscription. If the user chooses to cancel their subscription during the trial period, the subscription remains active until the end of the trial period. Some trial periods are not available for all subscription periods.

        > [!NOTE]
        > Each customer can acquire a free trial for a subscription add-on only one time. After a customer acquires a free trial for a subscription, the Store prevents the same customer from ever acquiring the same free trial subscription again.

    * [Visibility](../publish/set-add-on-pricing-and-availability.md#visibility): If you are creating a test add-on that you will only use to test the in-app purchase experience for your subscription, we recommend that you select one of the **Hidden in the Store** options. Otherwise, you can select the best visibility option for your scenario.

    * [Pricing](../publish/set-add-on-pricing-and-availability.md?#pricing): Choose the price of your subscription in this section. You cannot raise the price of the subscription after you publish the add-on. However, you can lower the price later.
        > [!IMPORTANT]
        > By default, when you create any add-on the price is initially set to **Free**. Because you cannot raise the price of a subscription add-on after you complete the add-on submission, be sure to choose the price of your subscription here.

2. In your app, use APIs in the [**Windows.Services.Store**](/uwp/api/windows.services.store) namespace to determine whether the current user has already acquired your subscription add-on and then offer it for sale to the user as an in-app purchase. See the [code examples](#code-examples) in this article for more details.

3. Test the in-app purchase implementation of your subscription in your app. You'll need to download your app once from the Store to your development device to use its license for testing. For more information, see our [testing guidance](in-app-purchases-and-trials.md#testing) for in-app purchases.  

4. Create and publish an app submission that includes your updated app package, including your tested code. For more information, see [App submissions](../publish/app-submissions.md).

<span id="code-examples"/>

## Code examples

The code examples in this section demonstrate how to use the APIs in the [**Windows.Services.Store**](/uwp/api/windows.services.store) namespace to get info about subscription add-ons for the current app and request the purchase a subscription add-on on behalf of the current user.

These examples have the following prerequisites:
* A Visual Studio project for a Universal Windows Platform (UWP) app that targets **Windows 10 Anniversary Edition (10.0; Build 14393)** or a later release.
* You have [created an app submission](../publish/app-submissions.md) in Partner Center and this app is published in the Store. You can optionally configure the app so it is not discoverable in the Store while you test it. For more information, see the [testing guidance](in-app-purchases-and-trials.md#testing).
* You have [created a subscription add-on for the app](../publish/add-on-submissions.md) in Partner Center.

The code in these examples assumes:
* The code file has **using** statements for the **Windows.Services.Store** and **System.Threading.Tasks** namespaces.
* The app is a single-user app that runs only in the context of the user that launched the app. For more information, see [In-app purchases and trials](in-app-purchases-and-trials.md#api_intro).

> [!NOTE]
> If you have a desktop application that uses the [Desktop Bridge](https://developer.microsoft.com/windows/bridges/desktop), you may need to add additional code not shown in these examples to configure the [**StoreContext**](/uwp/api/Windows.Services.Store.StoreContext) object. For more information, see [Using the StoreContext class in a desktop application that uses the Desktop Bridge](in-app-purchases-and-trials.md#desktop).

### Purchase a subscription add-on

This example demonstrates how to request the purchase of a known subscription add-on for your app on behalf of the current customer. This example also shows how to handle the case where the subscription has a trial period.

1. The code first determines whether the customer already has an active license for the subscription. If the customer already has an active license, your code should unlock the subscription features as necessary (because this is proprietary to your app, this is identified with a comment in the example).
2. Next, the code gets the [**StoreProduct**](/uwp/api/windows.services.store.storeproduct) object that represents the subscription you want to purchase on behalf of the customer. The code assumes that you already know the [Store ID](in-app-purchases-and-trials.md#store-ids) of the subscription add-on you want to purchase, and that you have assigned this value to the *subscriptionStoreId* variable.
3. The code then determines whether a trial is available for the subscription. Optionally, your app can use this information to display details about the available trial or full subscription to the customer.
4. Finally, the code calls [**RequestPurchaseAsync**](/uwp/api/windows.services.store.storeproduct.RequestPurchaseAsync) method to request the purchase of the subscription. If a trial is available for the subscription, the trial will be offered to the customer for purchase. Otherwise, the full subscription will be offered for purchase.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/InAppPurchasesAndLicenses_RS1/cs/PurchaseSubscriptionAddOnTrialPage.xaml.cs" id="PurchaseTrialSubscription":::

### Get info about subscription add-ons for the current app

This code example demonstrates how to get info for all the subscription add-ons that are available in your app. To get this info, first use the [**GetAssociatedStoreProductsAsync**](/uwp/api/Windows.Services.Store.StoreContext.GetAssociatedStoreProductsAsync) method to get the collection of [**StoreProduct**](/uwp/api/Windows.Services.Store.StoreProduct) objects that represent each of the available add-ons for the app. Then, get the [**StoreSku**](/uwp/api/windows.services.store.storesku) for each product and use the [**IsSubscription**](/uwp/api/windows.services.store.storesku.IsSubscription) and [**SubscriptionInfo**](/uwp/api/windows.services.store.storesku.SubscriptionInfo) properties to access the subscription info.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/InAppPurchasesAndLicenses_RS1/cs/GetSubscriptionAddOnsPage.xaml.cs" id="GetSubscriptions":::

<span id="manage-subscriptions" />

## Manage subscriptions from your services

After your updated app is in the Store and customers can buy your subscription add-on, you may have scenarios where you need to manage the subscription for a customer. We provide REST APIs you can call from your services to perform the following subscription management tasks:

* [Get the subscriptions that a user has an entitlement to use](get-subscriptions-for-a-user.md). If your subscription is part of a cross-platform service, you can call this API to determine whether the specified user has an entitlement for your subscription and the status of their subscription in the context of your UWP app. You can then use this information to update the status of the subscription on other platforms that your service supports.

* [Change the billing state of a subscription for a given user](change-the-billing-state-of-a-subscription-for-a-user.md). Use this API to cancel, extend, or disable automatic renewal for a subscription.

## Cancellations

Customers can use the [https://account.microsoft.com/services](https://account.microsoft.com/services) page for their Microsoft account to view all of the subscriptions they have acquired, cancel a subscription, and change the form of payment that is associated with their subscription. When a customer cancels a subscription using this page, they continue to have access to the subscription for the duration of the current billing period. They do not get a refund for any part of the current billing period. At the end of the current billing period, their subscription is deactivated.

You can also cancel a subscription on behalf of a user by using our REST API to [change the billing state of a subscription for a given user](change-the-billing-state-of-a-subscription-for-a-user.md).

## Subscription renewals and grace periods

At some point during each billing period, we will attempt to charge the customer's credit card for the next billing period. If the charge fails, the customer's subscription enters the *dunning* state. This means that their subscription is still active for the remainder of the current billing period, but we will periodically try to charge their credit card to automatically renew the subscription. This state can last up to two weeks, until the end of the current billing period and the renew date for the next billing period.

We do not offer grace periods for subscription billing. If we are unable to successfully charge the customer's credit card by the end of the current billing period, the subscription will be canceled and the customer will no longer have access to the subscription after the current billing period.

## Unsupported scenarios

The following scenarios are not currently supported for subscription add-ons.

* Selling subscriptions to customers directly via the Store is not supported at this time. Subscriptions are available for in-app purchases of digital products only.
* Customers cannot switch subscription periods using the [https://account.microsoft.com/services](https://account.microsoft.com/services) page for their Microsoft account. To switch to a different subscription period, customers must cancel their current subscription and then purchase a subscription with a different subscription period from your app.
* Tier switching is currently not supported for subscription add-ons (for example, switching a customer from a basic subscription to a premium subscription with more features).
* [Sales](../publish/put-apps-and-add-ons-on-sale.md) and [promotional codes](../publish/generate-promotional-codes.md) are currently not supported for subscription add-ons.
* Renewing existing subscriptions after setting the visibility of your subscription add-on to **Stop acquisition**. See [Set add-on pricing and availability](../publish/set-add-on-pricing-and-availability.md) for more details.

## Related topics

* [In-app purchases and trials](in-app-purchases-and-trials.md)
* [Get product info for apps and add-ons](get-product-info-for-apps-and-add-ons.md)
