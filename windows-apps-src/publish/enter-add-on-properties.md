﻿---
author: jnHs
Description: When submitting an add-on, the options on the Properties page help determine the behavior of your add-on when offered to customers.
title: Enter add-on properties
ms.assetid: 26D2139F-66FD-479E-940B-7491238ADCAE
ms.author: wdg-dev-content
ms.date: 06/19/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Enter add-on properties


When submitting an add-on, the options on the **Properties** page help determine the behavior of your add-on when offered to customers.

## Product type

Your product type is selected when you first [create the add-on](set-your-add-on-product-id.md). The product type you selected is displayed here, but you can't change it.

> [!TIP]
> If you haven't published the add-on, you can delete the submission and start again if you want to choose a different product type.

The fields you see on this page will vary, depending on the product type of your add-on.

## Product lifetime


If you selected **Durable** for your product type, **Product lifetime** is shown here. The default **Product lifetime** for a durable add-on is **Forever**, which means the add-on never expires. If you prefer, you can set the **Product lifetime** so that the add-on expires after a set duration (with options from 1-365 days).

## Quantity


If you selected **Store-managed consumable** for your product type, **Quantity** is shown here. You'll need to enter a number between 1 and 1000000. This quantity will be granted to the customer when they acquire your add-on, and the Store will track the balance as the app reports the customer’s consumption of the add-on.


## Subscription period

If you selected **Subscription** for your product type, **Subscription period** is shown here. You'll need to choose one of the available options (**Monthly**, **3 months**, **6 months**, **Annually**, or **24 months**) to indicate how frequently a customer will be charged for the subscription. Note that after your add-on is published, you can't change your **Subscription period** selection.

> [!NOTE]
> Currently, the ability to create subscription add-ons is only available to a set of developer accounts who are participating in an early adoption program. We will make subscription add-ons available to all developer accounts in the future, and we are making this preliminary documentation available now to give developers a preview of this feature. For more info, see [Enable subscription add-ons for your app](../monetize/enable-subscription-add-ons-for-your-app.md).


## Free trial

For subscription add-ons, **Free trial** is also shown here. You must select whether to let customers use the add-on for free for a set period of time (either **1 week** or **1 month**), or whether to offer **No free trial**. Note that after your add-on is published, you can't change your **Free trial** selection.


## Content type

Regardless of your add-on's product type, you'll need to indicate the type of content you're offering. For most add-ons, the content type should be **Electronic software download**. If another option from the list describes your add-on better (for example, if you are offering a music download or an e-book), select that option instead.

These are the possible options for an add-on's content type:

-   Electronic software download
-   Electronic books
-   Electronic magazine single issue
-   Electronic newspaper single issue
-   Music download
-   Music streaming
-   Online data storage/services
-   Software as a service
-   Video download
-   Video streaming


## Additional properties

These fields are optional for all types of add-ons.

<span id="keywords" />
### Keywords

You have the option to provide up to ten keywords of up to 30 characters each for each add-on you submit. Your app can then query for add-ons that match these words. This feature lets you build screens in your app that can load add-ons without you having to directly specify the product ID in your app's code. You can then change the add-on's keywords anytime, without having to make code changes in your app or submit the app again.

To query this field, use the [StoreProduct.Keywords](https://docs.microsoft.com/uwp/api/windows.services.store.storeproduct#Windows_Services_Store_StoreProduct_Keywords) property in the [Windows.Services.Store namespace](https://msdn.microsoft.com/en-us/library/windows/apps/windows.services.store.aspx). (Or, if you're using the [Windows.ApplicationModel.Store namespace](https://msdn.microsoft.com/en-us/library/windows/apps/windows.applicationmodel.store.aspx), use the [ProductListing.Keywords](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.productlisting#Windows_ApplicationModel_Store_ProductListing_Keywords) property.)

> [!NOTE]
> Keywords are not available for use in packages targeting Windows 8 and Windows 8.1.

<span id="custom-developer-data" />
### Custom developer data

You can enter up to 3000 characters into the **Custom developer data** field (formerly called **Tag**) to provide extra context for your in-app product. Most often, this is in the form of an XML string, but you can enter anything you'd like in this field. Your app can then query this field to read its content (although the app can't edit the data and pass the changes back.)

For example, let’s say you have a game, and you’re selling an add-on which allows the customer to access additional levels. Using the **Custom developer data** field, the app can query to see which levels are available when a customer owns this add-on. You could adjust the value at any time (in this case, the levels which are included), without having to make code changes in your app or submit the app again, by updating the info in the add-on's **Custom developer data** field and then publishing an updated submission for the add-on.

To query this field, use the [StoreSku.CustomDeveloperData](https://msdn.microsoft.com/en-us/library/windows/apps/windows.services.store.storesku.customdeveloperdata.aspx) property in the [Windows.Services.Store namespace](https://msdn.microsoft.com/en-us/library/windows/apps/windows.services.store.aspx). (Or, if you're using the [Windows.ApplicationModel.Store namespace](https://msdn.microsoft.com/en-us/library/windows/apps/windows.applicationmodel.store.aspx), use the [ProductListing.Tag](https://msdn.microsoft.com/en-us/library/windows/apps/windows.applicationmodel.store.productlisting.tag.aspx) property.)

> [!NOTE]
> The **Custom developer data** field is not available for use in packages targeting Windows 8 and Windows 8.1.

 

 

 
