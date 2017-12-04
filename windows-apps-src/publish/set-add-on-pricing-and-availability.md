---
author: jnHs
Description: When submitting an add-on, the options on the Pricing and availability page determine what to charge for your add-on and how it should be offered to customers.
title: Set add-on pricing and availability
ms.assetid: B3D4B753-716B-460B-A3B1-ED5712ECD694
ms.author: wdg-dev-content
ms.date: 08/03/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: high
---

# Set add-on pricing and availability


When submitting an add-on, the options on the **Pricing and availability** page determine what to charge for your add-on and how it should be offered to customers.

> [!NOTE]
> We have recently updated the available options on this page. If you had any in-progress submissions from before these options were available, your submission may still show the older options. You can delete that submission and then create a new one if you want to use the newest options. Otherwise, the newest options will become available with the next update after you publish your in-progress submission.

## Markets

By default, your add-on will be listed in all possible markets, including any future markets that we may add later, at its base price.

However, just as with an app, you have the option to choose the markets in which you'd like to offer your add-on. In most cases you'll want to pick the same set of markets as the app, but you have the flexibility to make changes as needed. 

For more info and a full list of the available markets, see [Define market selection](define-pricing-and-market-selection.md).

## Visibility

You can determine whether your add-on should be offered for purchase to customers. 

The default option is **Can be displayed in the parent product’s Store listing**. Leave this option checked for add-ons that will be made available to any customer. 

For add-ons that you don't want to make broadly available, select **Hidden in the Store** and one of the following options:

-   **Available for purchase from within the parent product only**: Choosing this option allows any customer to purchase the add-on from within your app, but the add-on will not be displayed in your app's Store listing. Use this only when the offer is not broadly available, for example during initial periods of internal testing.
-   **Stop acquisition: Any customer with a direct link can see the product’s Store listing, but they can only download it if they owned the product before, or have a promotional code and are using a Windows 10 device. This add-on is not displayed in the parent product's listing**: Choosing this option means that the add-on won't be displayed in your app's listing, and no new customers may purchase the add-on. However, **this option is not supported for customers on Windows 8.1 or earlier**. If your app is available on Windows 8.1 or earlier, the add-on will still be available for purchase to those customers. To stop offering the add-on to customers on Windows 8.1 or earlier, you'll need to update your app to remove the code that offers the add-on, then publish a new submission for the app. This is recommended even if your app doesn't target Windows 8.1 or earlier; it's a better experience for your customers if you never offer them an add-on that you've opted to make unavailable.
    
 > [!NOTE] 
 > Choosing the **Stop acquisition** option, and/or submitting an app update that removes the add-on from your app's code, does not affect customers who have already purchased the add-on, regardless of their operating system.


## Schedule

By default (unless you have selected one of the **Hidden in the Store** options in the **Visibility** section), your add-on will be available to customers as soon as it passes certification and complete the publishing process. To choose other dates, select **Show options** to expand this section. 

For more info, see [Configure precise release scheduling](configure-precise-release-scheduling.md).


## Pricing

You must select a base price for your add-on (unless you have selected the **Stop acquisition** option in the **Visibility** section), choosing either **Free** or one of the available price tiers (starting at .99 USD).

You can also schedule price changes to indicate the date and time at which the add-on’s price should change. Additionally, you have the option to customize these changes for specific markets. 

For more info, see [Set and schedule app pricing](set-and-schedule-app-pricing.md).


## Sale pricing

If you want to offer your add-on at a reduced price for a limited period of time, you can create and schedule a sale. For more info, see [Put apps and add-ons on sale](put-apps-and-add-ons-on-sale.md).


## Publish date

By default, your submission will begin the publishing process as soon as it passes certification, unless you have configured dates in the [**Schedule** section](#schedule) described above. 

To control when your add-on should be published to the Store, use the **Schedule** section. For most submissions, you should use that section to schedule your release, and leave the **Publish date** section set to the default option, **Publish this submission as soon as it passes certification**. This will not cause the submission to be published earlier than the date(s) that you set in the **Schedule** section. The dates you selected in the **Schedule** section will determine when your add-on becomes available to customers in the Store.

If you don’t want to set a release date yet, and you prefer your submission to remain unpublished until you manually decide to start the publishing process, you can choose **Publish this submission manually.** Choosing this option means that your selection won’t be published until you indicate that it should be. After your add-on passes certification, you can publish it by selecting **Publish now** on the certification status page, or by selecting a specific date as described below.

Choose **No sooner than \[date\]** to ensure that the submission is not published until a certain date. With this option, your submission will be released as soon as possible on or after the date you specify. The date must be at least 24 hours in the future. Along with the date, you can also specify the time at which the submission should begin to be published.
 
> [!NOTE]
> Delays during certification or publishing could cause the actual release date to be later than the date you request. The Microsoft Store cannot guarantee that your add-on (or update) will be available on a specific date.  



 




