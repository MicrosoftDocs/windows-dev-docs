---
author: jnHs
Description: The Pricing and availability page of the app submission process lets you determine how much your app will cost, whether you'll offer a free trial, and how, when, and where it will be available to customers.
title: Set app pricing and availability
ms.assetid: 37BE7C25-AA74-43CD-8969-CBA3BD481575
ms.author: wdg-dev-content
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Set app pricing and availability


The **Pricing and availability** page of the [app submission process](app-submissions.md) lets you determine how much your app will cost, whether you'll offer a free trial, and how, when, and where it will be available to customers. Here, we'll walk through the options on this page and what you should consider when entering this information.

## Base price


The first item on this page lets you select a base price for your app. You can choose to offer it for free, or you can select one of the available price tiers. Specifying a base price is required in order to submit your app.

For more info, see [Define pricing and market selection](define-pricing-and-market-selection.md).

## Free trial


Many developers choose to allow customers to try out their app for free using the trial functionality provided by the Store. By default, an app will not be available as a free trial, but if you'd like to offer one, select a value from the **Free trial** dropdown.

Choose **Trial never expires** to let customers access your app for free indefinitely. You'll want to encourage them to purchase the full version, so make sure to add code to [exclude or limit features in the trial version](../monetize/in-app-purchases-and-trials.md).

You also have the option to select a time-limited trial of **1 day**, **7 days**, **15 days**, or **30 days**. You can still limit features during the trial period, or you can let customers access the full functionality during that period of time.

> **Note**  Time-limited trials are not shown to customers on Windows Phone 8.1 and earlier.

## Markets and custom prices


By default, your app will be listed in all possible markets at its base price. You can change these settings to include or exclude specific markets, and change the app's price in any market in which you offer it, in the **Markets and custom prices** section. For more info, see [Define pricing and market selection](define-pricing-and-market-selection.md).

## Sale pricing


If you want to offer your app at a reduced price for a limited period of time, you can create and schedule a sale. For more info, see [Put apps and add-ons on sale](put-apps-and-add-ons-on-sale.md).

## Distribution and visibility


The **Distribution and visibility** section allows you to set restrictions on how your app can be discovered and acquired.

The default setting is **Make this app available in the Store**. This means that your app will be listed in the Store for customers to find via the app's direct link and/or by other methods, including searching, browsing, and inclusion in curated lists.

If you want to hide your app in the Store but still make it available to certain people, select one of the following options to limit your app's availability. Note that customers on Windows 8 and Windows 8.1 won't be able to get the app at all if you choose any of these options.

-   **Hide this app and prevent acquisition. Customers with a promotional code can still download it on Windows 10 devices**: No customers can find your app in the Store via searching or browsing, but you can [generate promotional codes](generate-promotional-codes.md) to distribute to specific people on Windows 10. They can use the link and code to get your app for free, even though you aren't offering it to any other customers
-   **Hide this app in the Store. Customers with a direct link to the app’s listing can still download it, except on Windows 8 and Windows 8.1**: No customers can find your app in the Store via searching or browsing, but any customer with the direct link to your app's listing can download your app on devices running Windows 10 or Windows Phone 8.1 and earlier.
-   **Hide this app and make it available only to the people you specify below, who can download this app on Windows Phone 8.x devices. A promotional code may be used to download this app on Windows 10 devices**: No customers can find your app in the Store via searching or browsing, and only the Windows Phone 8.x customers whose email addresses (associated with their Microsoft accounts) that you enter in the box (separated by semicolons) can download your app by using the direct link to its listing. You can also [generate promotional codes](generate-promotional-codes.md) to distribute to specific people on Windows 10. This option is often used for [beta testing](beta-testing-and-targeted-distribution.md) on Windows Phone 8.1 and earlier. Note that this option can only be selected if you have never previously published the app with the **Distribution and visibility** option set to **Anyone can find your app in the Store**.

> **Note**  To completely stop offering an app to new customers, click **Make app unavailable** from the App overview page. After you confirm that you want to make the app unavailable, within a few hours it will no longer be visible in the Store, and no new customers will be able to get it via any method. This action will override any of the options you have chosen here: it won't be available to new customers at all. To make it available to new customers again, you can click **Make app available** from the App overview page at any time. For more info, see [Removing an app from the Store](guidance-for-app-package-management.md#removing-an-app-from-the-store).

## Windows 10 device families

Device family availability is now managed on the **Packages** page of your submission. For more info, see [Device family availability](upload-app-packages.md#device-family-availability).

## Organizational licensing


By default, your app may be offered to organizations to purchase in volume. You can indicate whether and how your app can be offered in this section.

For more info, see [Organizational licensing options](organizational-licensing.md).

## Publish date


You can indicate when your app (or update) will be published by choosing an option in the **Publish date** section.

-   Choose **Publish this submission as soon as it passes certification** to make this submission available in the Store as soon as possible.
-   Choose **Publish this submission manually** if you don't want your submission to be published until you indicate that it should be. You can do this from the certification status page by clicking **Publish now**, or by selecting a specific date as described below.
-   Choose **No sooner than \[date\]** to ensure that the submission is not published until a certain date. With this option, your submission will be released as soon as possible on or after the date you specify. The date must be at least 24 hours in the future. Along with the date, you can also specify the time at which the submission should begin to be published.

   > **Note**  Delays during certification or publishing could cause the actual release date to be later than the date you request. The Windows Store cannot guarantee that your app (or update) will be available on a specific date.

You can also change the release date after submitting your app, as long as it hasn’t entered the **Publish** step yet.
 

 
