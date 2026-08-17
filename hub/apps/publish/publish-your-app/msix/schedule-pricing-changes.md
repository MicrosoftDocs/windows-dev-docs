---
description: Select the base price for an MSIX app and schedule price changes. You can also customize these options for specific markets.
title: Set app pricing for MSIX app
ms.date: 08/21/2025
ms.topic: article
ms.localizationpriority: medium
---

# Set app pricing for MSIX app

## Configure app pricing

The **Pricing** section of the [Pricing and availability](./price-and-availability.md) page lets you select the base price for an app. You can also [schedule price changes](./configure-release-schedule.md) to indicate the date and time at which your app’s price should change. Additionally, you have the option to [override the base price for specific markets](#override-base-price-for-specific-markets), either by selecting a new price tier or by entering a free-form price in the market's local currency. Please be aware that Microsoft does not alter the product pricing you set without your approval. You’re in charge of making sure the prices match the current market situations, including currency exchange rates.

:::image type="content" source="images/msix-set-app-pricing.png" lightbox="images/msix-set-app-pricing.png" alt-text="A screenshot of the Pricing and availability section showing how to set app pricing.":::

### Base price

When you select your app's **Base price**, that price will be used in every market where your app is sold, unless you override the base price in any market(s).

You can set the **Base price** to **Free**, or you can choose an available price tier, which sets the price in all the countries/regions where you choose to distribute your app. Price tiers start at 0.99 USD, with additional tiers available at increasing increments (1.09 USD, 1.19 USD, and so on). The increments generally increase as the price gets higher.

> [!NOTE]
> These price tiers also apply to add-ons.
> Each price tier has a corresponding value in each of the more than 60 currencies offered by the Store. We use these values to help you sell your apps at a comparable price point worldwide. You can select your base price in any currency, and we’ll automatically use the corresponding value for different markets. Note that at times we may adjust the corresponding value in a certain market to account for changes in currency conversion rates. You can click on Review price per market button to view the prices for each market.

In the **Pricing** section, click **view conversion table** to see the corresponding prices in all currencies. This also displays an ID number associated with each price tier, which you’ll need if you're using the [Microsoft Store submission API](/windows/uwp/monetize/manage-app-submissions#price-tiers) to enter prices. You can click **Download** to download a copy of the price tier table as a .csv file.

Keep in mind that the price tier you select may include sales or value-added tax that your customers must pay. To learn more about your app’s tax implications in selected markets, see [Tax details for paid apps](/partner-center/tax-details-marketplace). You should also review the [price considerations for specific markets](./market-selection.md#microsoft-store-consumer-markets).


> [!NOTE]
> If you choose the **Stop acquisition** option under **Make this product available but not discoverable in the Store** in the [Visibility](./visibility-options.md#discoverability) section), you won't be able to set pricing for your submission (since no one will able to acquire the app unless they use a promotional code to get the app for free).

### Currency conversion rates

The Store automatically converts your retail base price for all markets for you. When customers browse your app, they will see the base price converted into their local currency. 

Exchange rates used to convert base prices (in US dollars) to local prices (in foreign currencies) are adjusted at times based on market conditions: 
* When you configure the base price for a product that previously had none, the latest currency conversion rates in Partner Center will be applied automatically.
* For existing products, updates to exchange rates do not change current wholesale or suggested retail prices. Any adjustments to those prices must be made by you.
* When you edit wholesale or suggested retail pricing, any newly selected price tiers will use the most recent conversion tables. After updating the base price, select **Review price per market** to confirm local prices. 

Before publishing, review pricing conversions to ensure each market aligns with your intended end-user price. 

To override the base price for any specific market, select **Create new market group** and then select the markets for price override. 

### Override base price for specific markets

By default, the options you select above will apply to all markets in which your app is offered. You can optionally change the price for one or more markets, either by choosing a different price tier or entering a free-form price in the market’s local currency. This way, you can maintain your regional pricing strategy and respond more effectively to the changes in the currency exchange rates in each market.

You can override the base price for one market at a time, or for a group of markets together. Once you’ve done so, you can override the base price for an additional market, (or an additional market group) by selecting **Select markets for base price override** again and repeating the process described below. To remove the override pricing you’ve specified for a market (or market group), click **Remove**.

#### Override the base price for a single market

To change the price for one market only, select it and click **Create**. You’ll then see the same **Base price** and **Schedule a price change** options as described above, but the selections you make will be specific to that market. Because you are overriding the base price for one market only, the price tiers will be shown in that market’s local currency. You can click **view conversion table** to see the corresponding prices in all currencies.

Overriding the base price for a single market also gives you the option to enter a free-form price of your choosing in the market’s local currency. You can enter any price you like (within a minimum and maximum range), even if it does not correspond to one of the standard price tiers. This price will be used only for customers on Windows 10 or Windows 11 (including Xbox) in the selected market.

> [!IMPORTANT]
> If you enter a free-form price, that price will not be adjusted (even if conversion rates change) unless you submit an update with a new price.

#### Override the base price for a market group

To override the base price for multiple markets, you’ll create a _market group_. To do so, select the markets you wish to include, then optionally enter a name for the group. (This name is for your reference only and won’t be visible to any customers.) When you’re finished, click **Create**. You’ll then see the same **Base price** and **Schedule a price change** options as described above, but the selections you make will be specific to that market group. Note that free-form prices can’t be used with market groups; you’ll need to select an available price tier.

To change the markets included in a market group, click the name of the market group and add or remove any markets you’d like, then click **OK** to save your changes.

> [!NOTE]
> A market can’t belong to multiple market groups within the **Pricing** section.
