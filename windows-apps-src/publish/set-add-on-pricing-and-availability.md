---
author: jnHs
Description: When submitting an add-on, the options on the Pricing and availability page determine what to charge for your add-on and how it should be offered to customers.
title: Set add-on pricing and availability
ms.assetid: B3D4B753-716B-460B-A3B1-ED5712ECD694
ms.author: wdg-dev-content
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Set add-on pricing and availability


When submitting an add-on, the options on the **Pricing and availability** page determine what to charge for your add-on and how it should be offered to customers.

## Base price


You must select a base price for your add-on. These price tiers are the same as the price tiers for apps, starting at .99 USD. You also have the option to offer your add-on for free.

## Markets and custom prices


By default, your add-on will be listed in all possible markets, including any future markets that we may add later, at its base price.

However, just as with an app, you have the option to choose the markets in which you'd like to offer your add-on. In most cases you'll want to pick the same set of markets as the app, but you have the flexibility to make changes as needed. You can also set custom prices so that you can charge different prices for the add-on in different markets.

For more info and a full list of the available markets, see [Define pricing and market selection](define-pricing-and-market-selection.md).

## Sale pricing


If you want to offer your add-on at a reduced price for a limited period of time, you can create and schedule a sale. For more info, see [Put apps and add-ons on sale](put-apps-and-add-ons-on-sale.md).

## Distribution and visibility


You can determine whether your add-on should be offered for purchase to customers. Choose from one of the following options:

-   **Available for purchase. May be displayed in your app's listing:** This is the default setting and is recommended unless you want to restrict access to your add-on. Leave this option checked for add-ons that will be made available to any customer.
-   **Available for purchase. Not displayed in your app's listing:** Choosing this option allows customers to purchase the add-on from within your app, but the add-on will not be displayed in your app's Store listing. Use this only when the offer is not broadly available, for example during initial periods of internal testing.
-   **No longer available for purchase. Not displayed in your app's listing.** Choosing this option means that the add-on won't be displayed in your app's listing, and no new customers may purchase the add-on. However, **this option is not supported for customers on Windows 8.1 or earlier**. If your app is available on Windows 8.1 or earlier, the add-on will still be available for purchase to those customers. To stop offering the add-on to customers on Windows 8.1 or earlier, you'll need to update your app to remove the code that offers the add-on, then publish a new submission for the app. This is recommended even if your app doesn't target Windows 8.1 or earlier; it's a better experience for your customers if you never offer them an add-on that you've opted to make unavailable.
    
 > **Note**  Choosing this setting, and/or submitting an app update that removes the add-on from your app's code, does not affect any customers who have already purchased the add-on, regardless of their operating system.


## Publish date

You can indicate when your add-on submission should be published by choosing an option in the **Publish date** section.

-   Choose **Publish my add-on as soon as it passes certification** to make this submission available in the Store as soon as possible.
-   Choose **Publish this add-on manually** if you don't want your submission to be published until you indicate that it should be. You can do this from the certification status page by clicking **Publish now**, or by selecting a specific date as described below.
-   Choose **No sooner than \[date\]** to ensure that the submission is not published until a certain date. With this option, your submission will be released as soon as possible on or after the date you specify. The date must be at least 24 hours in the future. Along with the date, you can also specify the time at which the submission should begin to be published.

 > **Note**  Delays during certification or publishing could cause the actual release date to be later than the date you request. The Windows Store cannot guarantee that your add-on (or update) will be available on a specific date.
 

 




