---
Description: When you create a new add-on in Partner Center, you need to specify a product type and assign it a product ID.
title: Set your add-on product type and product ID
ms.assetid: 59497B0F-82F0-4CEE-B628-040EF9ED8D3D
ms.date: 10/31/2018
ms.topic: article
keywords: windows 10, uwp, add-ons, iap, durable, consumable, subscription, product type, product id, in-app purchase, in-app product
ms.localizationpriority: medium
---
# Set your add-on product type and product ID

An add-on must be associated with an app that you've created in [Partner Center](https://partner.microsoft.com/dashboard) (even if you haven't submitted it yet). You can find the button to **Create a new add-on** on your app's **Overview** page or on its **Add-ons** page.

After you select **Create a new add-on**, you'll be prompted to specify a product type and assign a product ID for your add-on.

## Product type

First, you'll need to indicate which type of add-on you are offering. This selection refers to how the customer can use your add-on.

> [!NOTE]
> You won't be able to change the product type after you save this page to create the add-on. If you choose the wrong product type, you can always delete your in-progress add-on submission and start over by creating a new add-on.

<span id="durable" />

### Durable

Select **Durable** as your product type if your add-on is typically purchased only once. These add-ons are often used to unlock additional functionality in an app.

The default **Product lifetime** for a durable add-on is **Forever**, which means the add-on never expires. You have the option to set the **Product lifetime** to a different duration in the [Properties](enter-add-on-properties.md) step of the add-on submission process. If you do so, the add-on will expire after the duration you specify (with options from 1-365 days), in which case a customer could purchase it again after it expires.

### Consumable

If the add-on can be purchased, used (consumed), and then purchased again, you'll want to select one of the **consumable** product types. Consumable add-ons are often used for things like in-game currency (gold, coins, etc.) which can be purchased in set amounts and then used up by the customer. For more info, see [Enable consumable add-on purchases](../monetize/enable-consumable-add-on-purchases.md).

There are two types of consumable add-ons:
- **Developer-managed consumable**: Balance and fulfillment must be managed within your app. Supported on all OS versions.
- **Store-managed consumable:** Balance will be tracked by Microsoft across all of the customer’s devices running Windows 10, version 1607 or later; not supported on any earlier OS versions. To use this option, the parent product must be compiled using Windows 10 SDK version 14393 or later. Also note that you can't submit a Store-managed consumable add-on to the Store until the parent product has been published (though you can create the submission in Partner Center and begin working on it at any time). You'll need to enter the quantity for your Store-managed consumable add-on in the **Properties** step of your submission.

### Subscription

If your want to charge customers on a recurring basis for your add-on, choose **Subscription**.

After a subscription add-on is initially acquired by a customer, they will continue to be charged at recurring intervals in order to keep using the add-on. The customer can cancel the subscription at any time to avoid further charges. You'll need to specify the subscription period, and whether or not to offer a free trial, in the **Properties** step of your submission.

Subscription add-ons are only supported for customers running Windows 10, version 1607 or later. The parent app must be compiled using Windows 10 SDK version 14393 or later and it must use the in-app purchase API in the **Windows.Services.Store** namespace instead of the **Windows.ApplicationModel.Store** namespace. For more info, see [Enable subscription add-ons for your app](../monetize/enable-subscription-add-ons-for-your-app.md).

You must submit the parent product before you can publish subscription add-ons to the Store (though you can create the submission in Partner Center and begin working on it at any time).

## Product ID

Regardless of the product type you choose, you will need to enter a unique product ID for your add-on. This name will be used to identify your add-on in Partner Center, and you can use this identifier to [refer to the add-on in your code](../monetize/in-app-purchases-and-trials.md#how-to-use-product-ids-for-add-ons-in-your-code).

Here are a few things to keep in mind when choosing a product ID:

-   A product ID must be unique within the parent product.
-   You can’t change or delete an add-on's product ID after it's been published.
-   A product ID can't be more than 100 characters in length.
-   A product ID cannot include any of the following characters: **&lt; &gt; \* % & : \\ ? + ,**
-   Customers won't see the product ID. (Later, you can enter a [title and description](./create-app-store-listings.md) to be displayed to customers.)
-   If your previously-published app supports Windows Phone 8.1 or earlier, you must only use alphanumeric characters, periods, and/or underscores in your product ID. If you use any other types of characters, the add-on will not be available for purchase to customers running Windows Phone 8.1 or earlier.

 