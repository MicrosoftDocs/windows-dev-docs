---
title: What are add-ons
description: Learn what add-ons are, including consumable and durable in-app products that customers can purchase to enhance your app experience.
ms.topic: article
ms.date: 07/22/2026
---

# What are add-ons?

Add-ons (also sometimes referred to as in-app products) are supplementary items for your app that can be purchased by customers. An add-on can be a fun new feature, a new game level, or anything else you think will keep users engaged. Not only are add-ons a great way to make money, but they help to drive customer interaction and engagement.

Add-ons are published through Partner Center and require you to have an active developer account. You'll also need to enable the add-ons in your app's code.

> [!NOTE]
> To learn more about how to create and configure add-ons, see [In-app purchases and trials](/windows/uwp/monetize/in-app-purchases-and-trials).

## Product type

Here are the types of add-on products you can create:

### Consumable

If the add-on can be purchased, used (consumed), and then purchased again, you'll want to select one of the **consumable** product types. Consumable add-ons are often used for things like in-game currency (gold, coins, etc.) which can be purchased in set amounts and then used up by the customer. For more info, see [Enable consumable add-on purchases](/windows/uwp/monetize/enable-consumable-add-on-purchases).

There are two types of consumable add-ons:

- **Developer-managed consumable**: Balance and fulfillment must be managed within your app. Supported on all OS versions.
- **Store-managed consumable**: Balance will be tracked by Microsoft across all of the customer’s devices running Windows 10, version 1607 or later; not supported on any earlier OS versions. To use this option, the parent product must be compiled using Windows 10 SDK version 14393 or later. Also note that you can't submit a Store-managed consumable add-on to the Store until the parent product has been published (though you can create the submission in Partner Center and begin working on it at any time). You'll need to enter the quantity for your Store-managed consumable add-on in the **Properties** step of your submission.

<span id="durable"></span>

### Durable

Select **Durable** as your product type if your add-on is typically purchased only once. These add-ons are often used to unlock additional functionality in an app.

The default **Product lifetime** for a durable add-on is **Forever**, which means the add-on never expires. You have the option to set the **Product lifetime** to a different duration in the [Properties](./enter-app-properties.md) step of the add-on submission process. If you do so, the add-on will expire after the duration you specify (with options from 1-365 days), in which case a customer could purchase it again after it expires.

### Subscription

If you want to charge customers on a recurring basis for your add-on, select **Subscription**.

After a subscription add-on is initially acquired by a customer, they will continue to be charged at recurring intervals in order to keep using the add-on. The customer can cancel the subscription at any time to avoid further charges. You'll need to specify the subscription period, and whether or not to offer a free trial, in the **Properties** step of your submission.

Subscription add-ons are only supported for customers running Windows 10, version 1607 or later. The parent app must be compiled using Windows 10 SDK version 14393 or later and it must use the in-app purchase API in the **Windows.Services.Store** namespace instead of the **Windows.ApplicationModel.Store** namespace. For more info, see [Enable subscription add-ons for your app](/windows/uwp/monetize/enable-subscription-add-ons-for-your-app).

You must submit the parent product before you can publish subscription add-ons to the Store, although you can create the submission in Partner Center and begin working on it at any time.

Follow these steps to publish your app add-on to the Microsoft Store:

| Topic                                                                   | Description                                                                                                                                  |
| ----------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------- |
| [Create an app submission for your add-on](./create-app-submission.md)  | Add-on submissions contain all of the information needed to distribute your add-on in the Microsoft Store.                                   |
| [Set your add-on's price and availability](./price-and-availability.md) | Specify how, when, and where your add-on will be available to customers, your add-on's pricing model, and whether you'll offer a free trial. |
| [Specify your add-on's properties](./enter-app-properties.md)           | Add-on properties describe important details about your app including requirements, capabilities, and your contact information.              |
| [Create your add-on's store listing](./create-app-store-listing.md)     | Create your add-on's page in the Microsoft Store.                                                                                            |
