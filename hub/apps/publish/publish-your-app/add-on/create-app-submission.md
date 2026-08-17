---
title: Create an add-on submission
description: Learn how to create an add-on in Partner Center and start its submission so you can tell the Microsoft Store about your app's add-on.
ms.topic: article
ms.date: 08/05/2026
ai-usage: ai-assisted
---

# Create an app submission for your add-on

Add-ons (also called in-app products) are supplementary items that customers can purchase for your app, such as a new feature, a new game level, or in-app currency.

This article explains how to create an add-on in Partner Center and start its submission to the Microsoft Store.

## Create a new add-on

Before you create an add-on, make sure you have an active developer account and an app that you've already created in [Partner Center](https://partner.microsoft.com/dashboard). You must associate each add-on with one of your apps, and you must enable the add-on in your app's code. To learn more about how to create and configure add-ons, see [In-app purchases and trials](/windows/uwp/monetize/in-app-purchases-and-trials).

:::image type="content" border="true" source="../images/create-add-ons-1.png" lightbox="../images/create-add-ons-1.png" alt-text="Screenshot of the Create a new add-on page in Partner Center.":::

You can find the button to **Create a new add-on** on your app's **add-ons** page. After you select **Create a new add-on**, Partner Center prompts you to specify a product type and assign a product ID for your add-on.

:::image type="content" border="true" source="../images/create-add-ons-2.png" lightbox="../images/create-add-ons-2.png" alt-text="Screenshot of the product type and product ID page in Partner Center.":::

## Product type

First, indicate which type of add-on you're offering. This selection refers to how the customer can use your add-on. Here are the types of add-on products you can create:

### Consumable

If customers can buy the add-on, consume it, and then buy it again, select one of the **consumable** product types. Consumable add-ons often represent things like in-game currency (gold, coins, and so on) that customers buy in set amounts and then use up. For more information, see [Enable consumable add-on purchases](/windows/uwp/monetize/enable-consumable-add-on-purchases).

There are two types of consumable add-ons:

- **Developer-managed consumable**: You must manage balance and fulfillment within your app. Supported on all OS versions.
- **Store-managed consumable**: Microsoft tracks the balance across all of the customer’s devices running Windows 10, version 1607 or later; earlier OS versions don't support this option. To use this option, you must compile the parent product using Windows 10 SDK version 14393 or later. You can't submit a Store-managed consumable add-on to the Store until you publish the parent product, although you can create the submission in Partner Center and begin working on it at any time. Enter the quantity for your Store-managed consumable add-on in the **Properties** step of your submission.

### Durable

Select **Durable** as your product type if your add-on is typically purchased only once. These add-ons often unlock additional functionality in an app.

The default **Product lifetime** for a durable add-on is **Forever**, which means the add-on never expires. You can set the **Product lifetime** to a different duration in the [Properties](./enter-app-properties.md) step of the add-on submission process. If you do so, the add-on expires after the duration you specify (with options from 1 to 365 days), in which case a customer could purchase it again after it expires.

### Subscription

If you want to charge customers on a recurring basis for your add-on, select **Subscription**.

After a customer initially acquires a subscription add-on, they pay at recurring intervals to keep using the add-on. The customer can cancel the subscription at any time to avoid further charges. Specify the subscription period and whether to offer a free trial in the **Properties** step of your submission.

Subscription add-ons work only for customers running Windows 10, version 1607 or later. You must compile the parent app by using Windows 10 SDK version 14393 or later, and the app must use the in-app purchase API in the **Windows.Services.Store** namespace instead of the **Windows.ApplicationModel.Store** namespace. For more information, see [Enable subscription add-ons for your app](/windows/uwp/monetize/enable-subscription-add-ons-for-your-app).

You must submit the parent product before you can publish subscription add-ons to the Store, although you can create the submission in Partner Center and begin working on it at any time.

> [!NOTE]
> You can't change the product type after you save this page to create the add-on. If you choose the wrong product type, you can always delete your in-progress add-on submission and start over by creating a new add-on.

## Product ID

Regardless of the product type you choose, you need to enter a unique product ID for your add-on. This name identifies your add-on in Partner Center, and you can use this identifier to [refer to the add-on in your code](/windows/uwp/monetize/in-app-purchases-and-trials#how-to-use-product-ids-for-add-ons-in-your-code).

Here are a few things to keep in mind when choosing a product ID:

- A product ID must be unique within the parent product.
- You can’t change or delete an add-on’s product ID after you publish it.
- A product ID can't be more than 100 characters in length.
- A product ID can't include any of the following characters: **&lt; &gt; \* % & : \\ ? + ,**
- Customers won't see the product ID. (Later, you can enter a [title and description](./create-app-store-listing.md) to show to customers.)
- If your previously published app supports Windows Phone 8.1 or earlier, you must only use alphanumeric characters, periods, or underscores in your product ID. If you use any other types of characters, the add-on won't be available for purchase to customers running Windows Phone 8.1 or earlier.

After you select the **Create add-on** button, you see the Submissions page.

:::image type="content" border="true" source="../images/create-add-ons-3.png" lightbox="../images/create-add-ons-3.png" alt-text="Screenshot of the Submissions page in Partner Center.":::

Select the **Start your submission** button to proceed.

## Submit to the Store

To publish your add-on, complete each step of the submission process, and then select **Submit to the Store**. In most cases, the certification process takes about an hour. After certification, your add-on appears in the Store, ready for customers to purchase.

## Next steps

Follow these steps to publish your app add-on to the Microsoft Store:

| Topic                                                                   | Description                                                                                                                                  |
| ----------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------- |
| [Set your add-on's price and availability](./price-and-availability.md) | Specify how, when, and where your add-on is available to customers, your add-on's pricing model, and whether you'll offer a free trial. |
| [Specify your add-on's properties](./enter-app-properties.md)           | Add-on properties describe important details about your app including requirements, capabilities, and your contact information.              |
| [Generate your add-on's age ratings](./age-ratings.md)           | Age ratings only apply to the Durable product type.              |
| [Create your add-on's store listing](./create-app-store-listing.md)     | Create your add-on's page in the Microsoft Store.                                                                                            |
| [Submission options](./manage-submission-options.md)     | Set the submission options for your add-on.                                                                                            |
