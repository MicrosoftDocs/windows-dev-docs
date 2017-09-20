﻿---
author: jnHs
Description: The App properties page of the app submission process lets you define your app's category and indicate hardware preferences or other declarations.
title: Enter app properties
ms.assetid: CDE4AF96-95A0-4635-9D07-A27B810CAE26
ms.author: wdg-dev-content
ms.date: 08/22/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Enter app properties

The **Properties** page of the [app submission process](app-submissions.md) lets you define your app's category and indicate hardware preferences or other declarations. Here, we'll walk through the options on this page and what you should consider when entering this information.

## Category and subcategory

In this section, you indicate the category (and subcategory, if applicable) which the Store should use to categorize your app. Specifying a category is required in order to submit your app.

For more info, see [Category and subcategory table](category-and-subcategory-table.md).

## Game settings

This section will only appear if you selected **Games** as your product’s category. Here you can specify which features your game supports. All of the information that you provide in this section will be displayed on the product’s Store listing.

If your game supports any of the multiplayer options, be sure to indicate the minimum and maximum number of players for a session. You can't enter more than 1,000 minimum or maximum players.

**Cross-platform multiplayer** means that the game supports multiplayer sessions between players on Windows 10 PCs and Xbox.


## Product declarations

You can check boxes in this section to indicate if any of the declarations apply to your app. This may affect the way your app is displayed, whether it is offered to certain customers, or how customers can use it.

For more info, see [Product declarations](app-declarations.md).

## System requirements

In this section, you have the option to indicate if certain hardware features are required or recommended to run and interact with your app properly. You can check the box (or indicate the appropriate option) for each hardware item where you would like to specify **Minimum hardware** and/or **Recommended hardware**.

If you make selections for **Recommended hardware**, those items will be displayed in your product's Store listing as recommended hardware for customers on Windows 10, version 1607 or later. Customers on earlier OS versions will not see this info.

If you make selections for **Minimum hardware**, those items will be displayed in your product's Store listing as required hardware for customers on Windows 10, version 1607 or later. Customers on earlier OS versions will not see this info. The Store may also display a warning to customers who are viewing your app's listing on a device that doesn’t have the required hardware. This won't prevent people from downloading your app on devices that don't have the appropriate hardware, but they won't be able to rate or review your app on those devices. 

The behavior for customers will vary depending on the specific requirements and the customer's version of Windows:

- **For customers on Windows 10, version 1607 or later:**
     - All minimum and recommended requirements will be displayed in the Store listing.
     - The Store will check for all minimum requirements and will display a warning to customers on a device that doesn't meet the requirements.
- **For customers on earlier versions of Windows 10:**
     - For most customers, all minimum and recommended hardware requirements will be displayed in the Store listing (though customers viewing an older versions of the Store client will only see the minimum hardware requirements).
     - The Store will attempt to verify items that you designate as **Minimum hardware**, with the exception of **Memory**, **DirectX**, **Video memory**, **Graphics**, and **Processor**; none of those will be verified, and customers won't see any warning on devices which don't meet those requirements. 
- **For customers on Windows 8.x and earlier or Windows Phone 8.x and earlier:**
     - If you check the **Minimum hardware** box for **Touch screen**, this requirement will be displayed in your app's Store listing, and customers on devices without a touch screen will see a warning if they try to download the app. No other requirements will be verified or displayed in your Store listing.

We also recommend adding runtime checks for the specified hardware into your app, since the Store may not always be able to detect that a customer's device is missing the selected feature(s) and they could still be able to download your app even if a warning is displayed. If you want to completely prevent your UWP app from being downloaded on a device which doesn't meet minimum requirements for memory or DirectX level, you can designate the minimum requirements in a [StoreManifest XML file](https://msdn.microsoft.com/library/windows/apps/mt617335).

> [!TIP]
> If your product requires additional items that aren't listed in this section in order to run properly, such as 3D printers or USB devices, you can also enter [additional system requirements](create-app-store-listings.md#additional-system-requirements) when you create your Store listing.





