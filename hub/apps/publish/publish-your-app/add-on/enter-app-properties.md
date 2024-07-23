---
description: The App properties page of the app add-on submission process lets you define your app's category and indicate hardware preferences or other declarations.
title: Enter add-on properties
ms.date: 10/30/2022
ms.topic: article
ms.localizationpriority: medium
---

# Enter add-on properties

When submitting an add-on, the options on the **Properties** page help determine the behavior of your add-on when offered to customers.

## Product type

Your product type is selected when you first [create the add-on](./create-app-store-listing.md). The product type you selected is displayed here, but you can't change it.

> [!TIP]
> If you haven't published the add-on, you can delete the submission and start again if you want to choose a different product type.

The fields you see on this page will vary, depending on the product type of your add-on.

## Product lifetime

If you selected **Durable** for your product type, **Product lifetime** is shown here. The default **Product lifetime** for a durable add-on is **Forever**, which means the add-on never expires. If you prefer, you can change the **Product lifetime** so that the add-on expires after a set duration (with options from 1-365 days).

## Quantity

If you selected **Store-managed consumable** for your product type, **Quantity** is shown here. You'll need to enter a number between 1 and 1000000. This quantity will be granted to the customer when they acquire your add-on, and the Store will track the balance as the app reports the customer’s consumption of the add-on.

## Subscription period

If you selected **Subscription** for your product type, **Subscription period** is shown here. Choose an option to specify how frequently a customer will be charged for the subscription. The default option is **Monthly**, but you can also select **3 months**, **6 months**, **Annually**, or **24 months**.

> [!IMPORTANT]
> After your add-on is published, you can't change your **Subscription period** selection.

## Free trial

If you selected **Subscription** for your product type, **Free trial** is also shown here. The default option is **No free trial.** If you prefer, you can let customers use the add-on for free for a set period of time (either **1 week** or **1 month**).

> [!IMPORTANT]
> After your add-on is published, you can't change your **Free trial** selection.

## Content type

Regardless of your add-on's product type, you'll need to indicate the type of content you're offering. For most add-ons, the content type should be **Electronic software download**. If another option from the list describes your add-on better (for example, if you are offering a music download or an e-book), select that option instead.

These are the possible options for an add-on's content type:

- Electronic software download
- Electronic books
- Electronic magazine single issue
- Electronic newspaper single issue
- Music download
- Music streaming
- Online data storage/services
- Software as a service
- Video download
- Video streaming

## Additional properties

These fields are optional for all types of add-ons.

### Keywords

You have the option to provide up to ten keywords of up to 30 characters each for each add-on you submit. Your app can then query for add-ons that match these words. This feature lets you build screens in your app that can load add-ons without you having to directly specify the product ID in your app's code. You can then change the add-on's keywords anytime, without having to make code changes in your app or submit the app again.

To query this field, use the [StoreProduct.Keywords](/uwp/api/windows.services.store.storeproduct.Keywords) property in the [Windows.Services.Store namespace](/uwp/api/Windows.Services.Store). (Or, if you're using the [Windows.ApplicationModel.Store namespace](/uwp/api/Windows.ApplicationModel.Store), use the [ProductListing.Keywords](/uwp/api/windows.applicationmodel.store.productlisting.Keywords) property.)

### Custom developer data

You can enter up to 3000 characters into the **Custom developer data** field (formerly called **Tag**) to provide extra context for your in-app product. Most often, this is in the form of an XML string, but you can enter anything you'd like in this field. Your app can then query this field to read its content (although the app can't edit the data and pass the changes back.)

For example, let’s say you have a game, and you’re selling an add-on which allows the customer to access additional levels. Using the **Custom developer data** field, the app can query to see which levels are available when a customer owns this add-on. You could adjust the value at any time (in this case, the levels which are included), without having to make code changes in your app or submit the app again, by updating the info in the add-on's **Custom developer data** field and then publishing an updated submission for the add-on.

To query this field, use the [StoreSku.CustomDeveloperData](/uwp/api/windows.services.store.storesku.customdeveloperdata#Windows_Services_Store_StoreSku_CustomDeveloperData) property in the [Windows.Services.Store namespace](/uwp/api/Windows.Services.Store). (Or, if you're using the [Windows.ApplicationModel.Store namespace](/uwp/api/Windows.ApplicationModel.Store), use the [ProductListing.Tag](/uwp/api/windows.applicationmodel.store.productlisting.tag#Windows_ApplicationModel_Store_ProductListing_Tag) property.)

