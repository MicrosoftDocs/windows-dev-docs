﻿---
author: jnHs
Description: If you've provided packages targeting different operating systems, you have the option to customize parts of your Store listing for different targeted operating systems.
title: Create platform-specific Store listings
ms.assetid: 5BE66BE2-669C-49E0-8915-60F1027EF94A
ms.author: wdg-dev-content
ms.date: 09/13/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Create platform-specific Store listings


If your app has packages that target different operating systems, you have the option to customize parts of your Store listing for customers on earlier OS versions (Windows 8.x or earlier and/or Windows Phone 8.x or earlier). 

> [!IMPORTANT]
> Customers on Windows 10 will always see the default [Store listing](create-app-store-listings.md). You will not see the option to create platform-specific Store listings unless you have already uploaded packages that support one or more earlier OS versions. 

Platform-specific Store listings can be useful if you want to mention features that appear only in one OS version, or want to provide screenshots that are specific to a particular OS (independent of device type), rather than having all customers see the same Store listing.

> [!NOTE]
> Creating a platform-specific Store listing in one language does not create a platform-specific Store listing in other languages that your app supports. You'll need to create the platform-specific Store listing separately for each language. Also note that you cannot import and export Store listing data for platform-specific listings.


## Creating a platform-specific Store listing

Near the top of your **Store listing** page, if your app supports earlier OS versions ((Windows 8.x or earlier and/or Windows Phone 8.x or earlier), you can select **create a platform-specific app Store listing**. 

> [!TIP]
> You won't see the option to create platform-specific Store listings until after you've uploaded packages.

After selecting this option, you'll be prompted to choose from the targeted OS versions that your submission supports. Once you've already created platform-specific Store listings for all of the OS versions your app targets, you won't be able to make another selection. (Windows 10 is not included in the list of choices, because customers on Windows 10 will always see the app's default Store listing.)

You can use your default Store listing as a starting point, which will bring over the applicable text and images you've entered for your default Store listing; you'll then be able to make any changes you'd like before saving. You can also start from a completely blank Store listing if you prefer.

After you click **Continue**, your **Store listing** page will now include a section for the platform-specific Store listing you've just created. This section will include its own set of fields for **Description** (required), **Release notes**, **Screenshots**, **App tile icon**, **App features**, and **Additional system requirements**. Make sure to enter info into each field where you want to display info in the custom Store listing, even if it's the same info as in your default Store listing. If you leave any of these fields blank, no info will appear for that field in the custom Store listing.


> [!IMPORTANT]
> The [Shared fields](create-app-store-listings.md#shared-fields) of the Store listing can't be customized for different OS versions.
> 
> Additionally, because some of the fields in the default [Store listing] page only apply to customers on Windows 10, you won't see all of the same options when creating a platform-specific Store listing. For example, you can't add trailers to a platform-specific Store listing, because trailers are only shown to customers on Windows 10, version 1607 or later. 


## Removing a platform-specific Store listing

If you create a platform-specific Store listing and later decide you'd rather show your default Store listing to customers on that operating system, select the **Delete** link next to that listing.

After confirming that you'd like to show those customers your default Store listing, select **OK**. The platform-specific Store listing will be removed (for all languages in which it existed) and customers on that OS version will now see your default Store listing. If you change your mind, you can create another platform-specific Store listing for that operating system by following the steps listed above.

 

 




