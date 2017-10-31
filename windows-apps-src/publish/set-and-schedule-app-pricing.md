---
author: jnHs
Description: Select the base price for an app and schedule price changes. You can also customize these options for specific markets.
title: Set and schedule app pricing
ms.author: wdg-dev-content
ms.date: 09/28/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
localizationpriority: high
---

# Set and schedule app pricing

The **Pricing** section of the [Pricing and availability](set-app-pricing-and-availability.md) page lets you select the base price for an app. You can also [schedule price changes](#schedule-price-changes) to indicate the date and time at which your app’s price should change. Additionally, you have the option to [customize these changes for specific markets](#customize-pricing-for-specific-markets). 

> [!NOTE]
> Although this topic refers to apps, price selection for add-on submissions uses the same process.

## Base price

When you select your app's **Base price**, that price will be used in every market where your app is sold, unless you specify a custom price for particular market(s).

You can set the **Base price** to **Free**, or you can choose an available price tier, which sets the sales price in all the countries where you choose to distribute your app. Price tiers start at .99 USD, with additional tiers available at increasing increments (1.09 USD, 1.19 USD, and so on). The increments generally increase as the price gets higher. 

> [!NOTE]
> These price tiers also apply to add-ons. 

Each  price tier has a corresponding value in each of the more than 60 currencies offered by the Store. We use these values to help you sell your apps at a comparable price point worldwide. You can select your base price in any currency, and we’ll automatically use the corresponding value for different markets.

In the **Pricing** section, click **view table** to see the corresponding prices in all currencies. This also displays an ID number associated with each price tier, which you’ll need if you're using the [Microsoft Store submission API](../monetize/manage-app-submissions.md#price-tiers) to enter prices. You can click **Download** to download a copy of the price tier table as a .csv file.

Keep in mind that the price tier you select may include sales or value-added tax that your customers must pay. To learn more about your app’s tax implications in selected markets, see [Tax details for paid apps](tax-details-for-paid-apps.md). You should also review the [price considerations for specific markets](define-pricing-and-market-selection.md#price-considerations-for-specific-markets).

## Schedule price changes

You can optionally schedule one or more price changes if you want the base price of your app to change at a specific date and time. 

> [!IMPORTANT]
> Price changes are only shown to customers on Windows 10 devices (including Xbox). If your app supports earlier OS versions, the price changes will not apply. 
>
> - For customers on Windows 8, the app will always be offered at its **Base price** (and not any market-specific price), even if you schedule additional price changes. 
> - For customers on Windows 8.1, and on Windows Phone 8.1 and earlier, the app will always be offered at the initial price for the customer's market, even if you schedule additional price changes in that market.
> 
> Keep this in mind when scheduling price changes. For example, if you initially release your app at a lower price and then schedule a date on which the price should increase, you customers on earlier OS versions who purchase the app would pay the lower (original) price.

Click **Schedule a price change** to see the price change options. Choose the price tier you’d like to use, then select the date, time, and time zone.

You can click **Schedule a price change again** to schedule as many subsequent changes as you’d like.

> [!NOTE]
> Scheduled price changes work differently from [Sale pricing](put-apps-and-add-ons-on-sale.md). When you put an app on sale, customers viewing your Store listing will see that the price has been reduced, and they'll be able to purchase it at the lower price during the time period that you have selected. After the sale period is up, it will return to the base price.
>
> With a scheduled price change, you can adjust the price to be either higher or lower. The change will take place on the date you specify, but it won’t be displayed as a sale in the Store; the app will just have a new base price. 

## Customize pricing for specific markets

By default, the options you select above will apply to all markets in which your app is offered. To customize the price for specific markets, select **Customize for specific markets**. The **Market selection** pop-up window will appear, listing all of the markets in which you’ve chosen to make your app available. If you excluded any markets in the Markets section, those markets won't be shown here. 

> [!IMPORTANT]
> Customers on Windows 8 will always see the app at its **Base price**, even if you select a different price for their market.

To add customize pricing for one market, select it and click **Save**. You’ll then see the same **Base price** and **Schedule a price change** options as described above, but the selections you make will be specific to that market.

To add custom pricing for multiple markets, you’ll create a *market group*. To do so, select the markets you wish to include, then enter a name for the group. (This name is for your reference only and won’t be visible to any customers.) When you’re finished, click **Save**. You’ll then see the same **Base price** and **Schedule a price change** options as described above, but the selections you make will be specific to that market group.

> [!NOTE]
> A market can’t belong to more than one of the market groups you use in the Pricing section.

To add custom pricing for an additional market, or an additional market group, select **Customize for specific markets** again and repeat these steps. To change the markets included in a market group, select its name. To remove the pricing for a market group (or individual market), click **Remove**.



