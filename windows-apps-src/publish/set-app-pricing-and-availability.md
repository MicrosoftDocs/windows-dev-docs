---
author: jnHs
Description: The Pricing and availability page of the app submission process lets you determine how much your app will cost, whether you'll offer a free trial, and how, when, and where it will be available to customers.
title: Set app pricing and availability
ms.assetid: 37BE7C25-AA74-43CD-8969-CBA3BD481575
ms.author: wdg-dev-content
ms.date: 11/22/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: high
---

# Set app pricing and availability


The **Pricing and availability** page of the [app submission process](app-submissions.md) lets you determine how much your app will cost, whether you'll offer a free trial, and how, when, and where it will be available to customers. Here, we'll walk through the options on this page and what you should consider when entering this information.


## Markets

The Microsoft Store reaches customers in over 200 countries and regions around the world. By default, we’ll offer your app in all possible markets. If you prefer, you can choose the specific markets in which you'd like to offer your app. 

For more info, see [Define market selection](define-pricing-and-market-selection.md).


## Visibility

The **Visibility** section allows you to set restrictions on how your app can be discovered and acquired.

The default setting is **Make this product available and discoverable in the Store**. This means that your app will be listed in the Store for customers to find via the app's direct link and/or by other methods, including searching, browsing, and inclusion in curated lists. 

If you want to hide your app in the Store but still make it available to certain people, select **Show options** to expand the section, then select **Make this product available but not discoverable in the Store**. This means that no customers will be able find your app in the Store via searching or browsing, regardless of their OS version. You must also choose one of the following versions to determine how your app can be acquired.

>[!IMPORTANT]
> Each of these options limits the OS versions on which customers can acquire your app. Please read the descriptions carefully to make sure you are aware which OS versions are supported. Note that customers on Windows 8 and Windows 8.1 won't be able to get the app at all if you choose any of the options under **Make this product available but not discoverable in the Store**. 

- **Direct link only: Any customer with a direct link to the product’s listing can download it, except on Windows 8.x.** Any customer who gets to your app's listing via a direct link can download it on devices running Windows 10 or Windows Phone 8.1 and earlier. Customers on Windows 8.x can’t get the app at all with this option.
- **Stop acquisition: Any customer with a direct link can see the product’s Store listing, but they can only download it if they owned the product before, or have a promotional code and are using a Windows 10 device.** Even if a customer has a direct link, they can't download the app unless they have a [promotional code](generate-promotional-codes.md) and are using a Windows 10 device. When you give a code to a customer, they can use the link and code to get your app for free (on Windows 10 only), even though you aren't offering it to any other customers. Aside from using a promotional code, there is no way to get your app.
- **Individuals on Windows Phone 8.x only: Only people you specify below can download this product on a Windows Phone 8.x device. Anyone with a direct link and a promotional code may download the product on a Windows 10 device.** This option may not appear for all submissions. It only applies if you have packages that can run on Windows Phone 8.x. Only the customers whose email addresses (associated with their Microsoft accounts) you enter in the box (separated by semicolons) can download your app on Windows Phone 8.x by using the direct link to its listing. You can also generate promotional codes to distribute to specific people on Windows 10 as described above. 

> [!TIP]
> If you want to stop offering an app to any new customers, you can select **Make app unavailable** from its overview page. After you confirm that you want to make the app unavailable, within a few hours it will no longer be visible in the Store, and no new customers will be able to get it (unless they have a [promotional code](generate-promotional-codes.md) and are on a Windows 10 device). This action will override the **Visibility** selections in your submission. To make the app available to new customers again (per your **Visibility** selections), you can click **Make app available** from the overview page at any time. For more info, see [Removing an app from the Store](guidance-for-app-package-management.md#removing-an-app-from-the-store).


## Schedule

By default (unless you have selected one of the **Make this app available but not discoverable in the Store** options in the **Visibility** section), your app will be available to customers as soon as it passes certification and complete the publishing process. To choose other dates, select **Show options** to expand this section. 

For more info, see [Configure precise release scheduling](configure-precise-release-scheduling.md).


## Display release date

By default, the release date for your app will be the date when it appears in the Store. If you'd like to override this and provide a custom release date, you can check the box in this section, and then enter the release date. Note that the release date is not always displayed in Store listings.


## Pricing

You are required to select a base price for your app (unless you have selected one of the **Make this app available but not discoverable in the Store** options in the [Visibility](#visibility) section), choosing either **Free** or one of the available price tiers. You can also schedule price changes to indicate the date and time at which your app’s price should change. Additionally, you have the option to customize these changes for specific markets. 

For more info, see [Set and schedule app pricing](set-and-schedule-app-pricing.md).


## Free trial

Many developers choose to allow customers to try out their app for free using the trial functionality provided by the Store. By default, **No free trial** is selected, and there will be no trial for your app. If you’d like to offer a trial, you can select a value from the the **Free trial** dropdown.

There are two types of trial you can choose, and you have the option to configure the date and time when the trial should start and stop being offered.

### Time-limited

Choose **Time-limited** to allow customers to try your app for free for a certain number of days: **1 day**, **7 days**, **15 days**, or **30 days**. You can limit features by adding code to [exclude or limit features in the trial version](../monetize/in-app-purchases-and-trials.md), or you can let customers access the full functionality during that period of time. 
> [!NOTE]
> Time-limited trials are not shown to customers on Windows 10 build 10.0.10586 or earlier, or to customers on Windows Phone 8.1 and earlier.

### Unlimited

Choose **Unlimited** to let customers access your app for free indefinitely. You'll want to encourage them to purchase the full version, so make sure to add code to [exclude or limit features in the trial version](../monetize/in-app-purchases-and-trials.md).

### Start and end dates

By default, your trial will be available as soon as your app is published, and it will never stop being offered. If you’d like, you can specify the date and time that your trial should start to be offered and when it should stop being offered. 

>[!NOTE]
> These dates only apply for customers on Windows 10 (including Xbox). If your app is available to customers on earlier OS versions, the trial will be offered to those customers for as long as your product is available. 

To set dates for when your trial should be offered to customers on Windows 10, change the **Starts on** and/or **Ends on** dropdown to **at**, then choose the date and time. If you do so, you can either choose **UTC** so that the time you select will be Universal Coordinated Time (UTC) time, or choose **Local** so that these times will be used in each time zone associated with a market. (Note that for markets that include more than one time zone, only one time zone in that market will be used. For the United States, the Eastern time zone is used.) 

>[!NOTE]
> Unlike the [Schedule](configure-precise-release-scheduling.md) section, the dates you select for your **Free trial** cannot be customized for specific markets. 



## Sale pricing

If you want to offer your app at a reduced price for a limited period of time, you can create and schedule a sale.

For more info, see [Put apps and add-ons on sale](put-apps-and-add-ons-on-sale.md).


## Organizational licensing

By default, your app may be offered to organizations to purchase in volume. You can indicate whether and how your app can be offered in this section.

For more info, see [Organizational licensing options](organizational-licensing.md).


## Publish date

By default, your submission will begin the publishing process as soon as it passes certification, unless you have configured dates in the [**Schedule** section](#schedule) described above. 

To control when your app should be published to the Store, use the **Schedule** section. For most submissions, you should use that section to schedule your app’s release, and leave the **Publish date** section set to the default option, **Publish this submission as soon as it passes certification**. This will not cause the submission to be published earlier than the date(s) that you set in the **Schedule** section. The dates you selected in the **Schedule** section will determine when your app becomes available to customers in the Store.

If you don’t want to set a release date yet, and you prefer your submission to remain unpublished until you manually decide to start the publishing process, you can choose **Publish this submission manually.** Choosing this option means that your selection won’t be published until you indicate that it should be. After your app passes certification, you can publish it by selecting **Publish now** on the certification status page, or by selecting a specific date as described below.

Choose **No sooner than \[date\]** to ensure that the submission is not published until a certain date. With this option, your submission will be released as soon as possible on or after the date you specify. The date must be at least 24 hours in the future. Along with the date, you can also specify the time at which the submission should begin to be published.
 
> [!NOTE]
> Delays during certification or publishing could cause the actual release date to be later than the date you request. The Microsoft Store cannot guarantee that your app (or update) will be available on a specific date.  

You can also change the release date after submitting your app, as long as it hasn’t entered the Publish step yet. 

