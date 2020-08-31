---
Description: When submitting an add-on, the options on the Pricing and availability page determine what to charge for your add-on and how it should be offered to customers.
title: Set add-on pricing and availability
ms.assetid: B3D4B753-716B-460B-A3B1-ED5712ECD694
ms.date: 10/31/2018
ms.topic: article
keywords: windows 10, uwp, add-ons, iap, price
ms.localizationpriority: medium
---
# Set add-on pricing and availability

When submitting an add-on in [Partner Center](https://partner.microsoft.com/dashboard), the options on the **Pricing and availability** page determine how much to charge customers for your add-on and how it should be offered to customers.

## Markets

By default, your add-on will be listed in all possible markets, including any future markets that we may add later, at its base price.

However, just as with an app, you have the option to choose the markets in which you'd like to offer your add-on. In most cases you'll want to pick the same set of markets as the app, but you have the flexibility to make changes as needed. 

For more info and a full list of the available markets, see [Define market selection](./define-market-selection.md).

## Visibility

You can determine whether your add-on should be offered for purchase to customers. 

The default option is **Can be displayed in the parent product’s Store listing**. Leave this option checked for add-ons that will be made available to any customer. 

For add-ons that you don't want to make broadly available, select **Hidden in the Store** and one of the following options:

-   **Available for purchase from within the parent product only**: Choosing this option allows any customer to purchase the add-on from within your app, but the add-on will not be displayed in your app's Store listing or discoverable in the Store. Use this only when the offer is not broadly available, for example during initial periods of internal testing.
-   **Stop acquisition: Any customer with a direct link can see the product’s Store listing, but they can only download it if they owned the product before, or have a promotional code and are using a Windows 10 device. This add-on is not displayed in the parent product's listing**: Choosing this option means that the add-on won't be displayed in your app's listing, and no new customers may purchase the add-on. However, **this option is not supported for customers on Windows 8.1 or earlier**. If your previously-published app is available on Windows 8.1 or earlier, the add-on will still be available for purchase to those customers. To stop offering the add-on to customers on Windows 8.1 or earlier, you'll need to update your app to remove the code that offers the add-on, then publish a new submission for the app. This is recommended even if your app doesn't target Windows 8.1 or earlier; it's a better experience for your customers if you never offer them an add-on that you've opted to make unavailable.
    
 > [!NOTE] 
 > Choosing the **Stop acquisition** option and/or submitting an app update that removes the add-on from your app will not prevent customers from using the add-on if they have already purchased it. Existing subscriptions will fail to renew and subsequently be canceled after the current term ends.


## Schedule

By default (unless you have selected one of the **Hidden in the Store** options in the **Visibility** section), your add-on will be available to customers as soon as it passes certification and complete the publishing process. To choose other dates, select **Show options** to expand this section. 

For more info, see [Configure precise release scheduling](configure-precise-release-scheduling.md).


## Pricing

You must select a base price for your add-on (unless you have selected the **Stop acquisition** option in the **Visibility** section). The default selection is **Free**, so if you want to charge money for the add-on, be sure to choose one of the available price tiers (starting at .99 USD).

You can also schedule price changes to indicate the date and time at which the add-on’s price should change. Additionally, you have the option to customize these changes for specific markets. 

> [!TIP]
> For subscription add-ons, you can't raise the price after you publish the add-on, either by selecting a higher base price in a later submission or by scheduling a price change that increases the price. You can select a lower price using either of these methods, but once the price is lowered you won't be able to raise it higher than that new price. Because of this, it's especially important to be sure you select the appropriate price tier for subscription add-ons. 

For more info, see [Set and schedule app pricing](set-and-schedule-app-pricing.md).


## Sale pricing

If you want to offer your add-on at a reduced price for a limited period of time, you can create and schedule a sale. For more info, see [Put apps and add-ons on sale](put-apps-and-add-ons-on-sale.md).